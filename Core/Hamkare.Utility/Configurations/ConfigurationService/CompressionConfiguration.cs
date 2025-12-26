using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace Hamkare.Utility.Configurations.ConfigurationService;

public static class CompressionConfiguration
{
    public static void BaseCompression(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddResponseCompression(options => {
            options.Providers.Add<GzipCompressionProvider>();
            options.Providers.Add<BrotliCompressionProvider>();
            options.EnableForHttps = true;
        });

        serviceCollection.Configure<BrotliCompressionProviderOptions>(options => {
            options.Level = CompressionLevel.SmallestSize;
        });

        serviceCollection.Configure<GzipCompressionProviderOptions>(options => {
            options.Level = CompressionLevel.SmallestSize;
        });
    }
}