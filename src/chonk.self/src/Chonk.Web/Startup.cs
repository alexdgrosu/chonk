using System.Net;
using Chonk.Services;
using Chonk.Services.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Chonk.Web
{
    public class Startup
    {
        protected IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAppServices();
            services.AddAppConfiguration(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // * Simple HTML page that offers a truncated version of an exception message.
                // Is it a terrible idea? Yes.
                // Does the CHONK like to live dangeriously? Also yes.
                app.UseAppExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
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

    public static class IApplicationBuilderExtensions
    {
        public static void UseAppExceptionPage(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "text/html";

                    var feature = context.Features.Get<IExceptionHandlerFeature>();

                    if (feature is not null)
                    {
                        await context.Response.WriteAsync($@"
<!DOCTYPE html>
<title>Internal Server Error</title>
<h1>🦔: Oops, something went wrong!</h1>
<p> Here's a sneak peek: <pre><code>{truncate(feature.Error.Message)}...</code></pre></p>");
                    }

                    static string truncate(string original, int length = 50) => original?.Length < length ? original : original.Substring(0, length);
                });
            });
        }
    }
}
