using Hamkare.Common.Interface.Entities;
using Hamkare.Common.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Service.Services.Generic;

public class CategoryService<TGroupEntity, TDbContext>(ICategoryRepository<TGroupEntity, TDbContext> repository) : MainService<TGroupEntity, TDbContext>(repository)
    where TGroupEntity : class, ICategoryEntity<TGroupEntity>, new() where TDbContext : DbContext;