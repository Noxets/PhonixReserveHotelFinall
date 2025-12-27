using Hamkare.Common.Entities;
using Hamkare.Common.Entities.Identity;
using Hamkare.Common.Entities.Orders;
using Hamkare.Common.Interface.Repositories;
using Hamkare.Infrastructure;
using Hamkare.Service.Services.Generic;
using Hamkare.Service.Services.Identity;
using Hamkare.Utility.Extensions;

namespace Hamkare.Service.Services.Orders;

public class OrderService(
    IRootRepository<Order, ApplicationDbContext> repository,
    UserService userService,
    OrderItemService orderItemService) : RootService<Order, ApplicationDbContext>(repository)
{
        public async Task<Order> GetBasket(User user, List<ShopCartDto> list = null,
        CancellationToken cancellationToken = default)
    {
        var count = await GetCountActiveAsync(x => x.UserId == user.Id && x.State == OrderState.Basket,
            cancellationToken);

        switch (count)
        {
            case 1:
                var order = await GetActiveAsync(x => x.UserId == user.Id && x.State == OrderState.Basket,
                    cancellationToken);
                order.OrderItems = await ShopCartToOrderItems(order.OrderItems.ToList(), list, cancellationToken);
                return order;
            case 0:
                var basket = await CreateBasket(user, list, cancellationToken);
                return basket;
            default:
                var orders = await GetAllActiveAsync(x => x.UserId == user.Id && x.State == OrderState.Basket,
                    cancellationToken);
                foreach (var item in orders)
                    await DeleteAsync(item, cancellationToken);

                var empty = await CreateBasket(user, list, cancellationToken);
                return empty;
        }
    }

    private async Task<Order> CreateBasket(User user, List<ShopCartDto> list = null,
        CancellationToken cancellationToken = default)
    {
        var order = new Order
        {
            Active = true,
            UserId = user.Id,
            State = OrderState.Basket,
        };
        
        await AddOrUpdateAsync(order, cancellationToken);
        return order;
    }

    public async Task<int> GetBasketCount(User user, CancellationToken cancellationToken = default)
    {
        var order = await GetBasket(user, cancellationToken: cancellationToken);
        return order.OrderItems != null && order.OrderItems.GetAnyActive()
            ? order.OrderItems.GetAllActive().Count
            : 0;
    }
    
    private async Task<List<OrderItem>> ShopCartToOrderItems(List<OrderItem> orderItems, List<ShopCartDto> shopCart,
        CancellationToken cancellationToken = default)
    {
        foreach (var shopItem in shopCart ?? [])
        {
        }

        return orderItems;
    }
}