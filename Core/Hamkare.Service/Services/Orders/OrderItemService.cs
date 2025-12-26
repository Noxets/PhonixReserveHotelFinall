using Hamkare.Common.Entities.Orders;
using Hamkare.Common.Interface.Repositories;
using Hamkare.Infrastructure;
using Hamkare.Service.Services.Generic;

namespace Hamkare.Service.Services.Orders;

public class OrderItemService(IRootRepository<OrderItem, ApplicationDbContext> repository) : RootService<OrderItem, ApplicationDbContext>(repository);