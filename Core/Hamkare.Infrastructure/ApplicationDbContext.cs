using Hamkare.Common.Entities.General;
using Hamkare.Common.Entities.Identity;
using Hamkare.Common.Entities.News;
using Hamkare.Common.Entities.Persons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using Hamkare.Common.Entities.Generics;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Hamkare.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<User, Role, long>
{
    private readonly IHttpContextAccessor _httpContext;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContext)
        : base(options)
    {
        _httpContext = httpContext;
    }

    protected ApplicationDbContext(DbContextOptions dbContextOptions, IHttpContextAccessor httpContext) : base(
        dbContextOptions)
    {
        _httpContext = httpContext;
    }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<NewsComment> NewsComments { get; set; }

    public virtual DbSet<NewsCategory> NewsGroups { get; set; }

    public virtual DbSet<Person> Persons { get; set; }
    
    public virtual DbSet<Redirect> Redirects { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<UserCategory> UsersGroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(delegate (WarningsConfigurationBuilder warnings)
        {
            warnings.Ignore(CoreEventId.ForeignKeyAttributesOnBothNavigationsWarning);
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ApplyGlobalFilters(modelBuilder);

        modelBuilder.Seed();
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        var date = DateTime.Now;

        var userId = await Users.AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.UserName == (_httpContext.HttpContext == null
                    ? "System"
                    : _httpContext.HttpContext.User.Identity.Name), cancellationToken);

        #region Create Data

        var addedEntities = ChangeTracker.Entries()
            .Where(entry => entry is { Entity: RootEntity, State: EntityState.Added }).ToList();

        addedEntities.ForEach(entry =>
        {
            var baseEntity = entry.Entity as RootEntity ?? new RootEntity();

            baseEntity.CreateDate = date;
            baseEntity.CreatorId = userId?.Id ?? 1;

            entry.CurrentValues.SetValues(baseEntity);
        });

        #endregion

        #region Update Data

        var editedEntities = ChangeTracker.Entries()
            .Where(entry => entry is { Entity: RootEntity, State: EntityState.Modified }).ToList();

        editedEntities.ForEach(entry =>
        {
            var baseEntity = entry.Entity as RootEntity ?? new RootEntity();

            baseEntity.EditorId = userId?.Id ?? 1;
            baseEntity.EditDate = date;

            entry.CurrentValues.SetValues(baseEntity);
        });

        #endregion

        #region Delete Data

        var deletedEntities = ChangeTracker.Entries()
            .Where(entry => entry is { Entity: RootEntity, State: EntityState.Deleted }).ToList();

        deletedEntities.ForEach(entry => { entry.Property(nameof(RootEntity.Deleted)).CurrentValue = true; });

        #endregion

        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private static void ApplyGlobalFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (IsDeletedPropertyExists(entityType))
                ConfigureIsDeletedFilter(entityType, modelBuilder);
        
            if (typeof(RootEntity).IsAssignableFrom(entityType.ClrType))
                ConfigureRootEntityRelationships(entityType, modelBuilder);
        }
    }

    private static bool IsDeletedPropertyExists(IMutableTypeBase entityType)
    {
        return entityType.GetProperties().Any(p => p.Name == "IsDeleted" && p.ClrType == typeof(bool));
    }

    private static void ConfigureIsDeletedFilter(IReadOnlyTypeBase entityType, ModelBuilder modelBuilder)
    {
        var parameter = Expression.Parameter(entityType.ClrType, "e");
        var property = Expression.Property(parameter, "IsDeleted");
        var lambda = Expression.Lambda(Expression.Equal(property, Expression.Constant(false)), parameter);

        modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
    }
    
    private static void ConfigureRootEntityRelationships(IMutableEntityType entityType, ModelBuilder modelBuilder)
    {
        modelBuilder.Entity(entityType.ClrType).HasOne("Creator")
            .WithMany()
            .HasForeignKey("CreatorId")
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity(entityType.ClrType).HasOne("Editor")
            .WithMany()
            .HasForeignKey("EditorId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}