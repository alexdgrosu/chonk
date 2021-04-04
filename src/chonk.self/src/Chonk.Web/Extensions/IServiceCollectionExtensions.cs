using Chonk.Services;
using Chonk.Services.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Chonk.Web.Extensions
{
    // TODO: Consider extracting each extension method into its own class
    public static class IServiceCollectionExtensions
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IManifestReader, ManifestFromFile>();
        }

        public static void AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<WorkloadsSettings>(config.GetSection("Workloads"));
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<WorkloadsSettings>>().Value);
        }
    }
}
