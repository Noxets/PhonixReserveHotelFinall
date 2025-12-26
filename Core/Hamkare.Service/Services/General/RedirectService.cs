using Hamkare.Common.Entities.General;
using Hamkare.Common.Interface.Repositories;
using Hamkare.Infrastructure;
using Hamkare.Service.Services.Generic;

namespace Hamkare.Service.Services.General;

public class RedirectService(IRootRepository<Redirect, ApplicationDbContext> repository)
    : RootService<Redirect, ApplicationDbContext>(repository);