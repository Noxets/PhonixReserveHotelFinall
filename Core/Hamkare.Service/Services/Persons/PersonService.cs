using Hamkare.Common.Entities.Persons;
using Hamkare.Common.Interface.Repositories;
using Hamkare.Infrastructure;
using Hamkare.Service.Services.Generic;

namespace Hamkare.Service.Services.Persons;

public class PersonService(IRootRepository<Person, ApplicationDbContext> repository) : RootService<Person, ApplicationDbContext>(repository);