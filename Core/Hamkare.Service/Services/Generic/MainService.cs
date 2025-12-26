using Hamkare.Common.Interface.Entities;
using Hamkare.Common.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Service.Services.Generic;

public class MainService<TMainEntity, TDbContext>(IMainRepository<TMainEntity, TDbContext> repository) : RootService<TMainEntity, TDbContext>(repository)
    where TMainEntity : IMainEntity, new() where TDbContext : DbContext;