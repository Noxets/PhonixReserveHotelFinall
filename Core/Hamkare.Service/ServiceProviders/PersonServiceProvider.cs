using Hamkare.Service.Services.Persons;
using Microsoft.Extensions.DependencyInjection;

namespace Hamkare.Service.ServiceProviders;

public static class PersonServiceProvider
{
    internal static void Person(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<PersonService>();
    }
}