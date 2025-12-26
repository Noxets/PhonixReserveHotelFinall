using Hamkare.Common.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Infrastructure.Seed;

public static class IdentitySeed
{
    public static void Identity(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User
        {
            ExpireActivationCode = null,
            ActivationCode = null,
            UserName = "System",
            NormalizedUserName = "SYSTEM",
            Email = "info@hamkare.com",
            NormalizedEmail = "INFO@HAMKARE.COM",
            EmailConfirmed = true,
            PhoneNumber = "09129347829",
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = true,
            SecurityStamp = "HYLXAFSWTNGZVAG52F43F5C5DMPT47TC",
            ConcurrencyStamp = "f77886d5-834d-440d-9900-48687a0d79c2",
            PasswordHash = "AQAAAAIAAYagAAAAEHAqWc2r+andK2goLFbw6fZSFc4Vy8/wRbzCgyLaB2QEaeOqHmnfEKA034rgVGIxFQ==",
            LockoutEnd = null,
            LockoutEnabled = false,
            AccessFailedCount = 0,
            Id = 1,
            Active = true,
            Deleted = false,
            System = true,
            CreatorId = 1,
            CreateDate = DateTime.MinValue
        });

        modelBuilder.Entity<Role>().HasData(
        new Role
        {
            Name = "System",
            NormalizedName = "SYSTEM",
            Id = 1,
            Active = true,
            Deleted = false,
            System = true,
            CreatorId = 1,
            CreateDate = DateTime.Now
        });

        modelBuilder.Entity<IdentityUserRole<long>>().HasData(
        new IdentityUserRole<long>
        {
            UserId = 1,
            RoleId = 1
        });
    }
}