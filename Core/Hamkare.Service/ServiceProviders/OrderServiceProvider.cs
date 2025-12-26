using Hamkare.Service.Services.Orders;
using Microsoft.Extensions.DependencyInjection;

namespace Hamkare.Service.ServiceProviders;

public static class OrderServiceProvider
{
    internal static void Order(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<OrderService>();
        serviceCollection.AddTransient<OrderItemService>();
    }
}