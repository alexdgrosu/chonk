using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Chonk.Web.Extensions
{
    // TODO: Consider extracting each extension method into its own class
    public static class IApplicationBuilderExtensions
    {
        public static void UseAppExceptionPage(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Text.Html;

                    var feature = context.Features.Get<IExceptionHandlerFeature>();

                    if (feature is not null)
                    {
                        await write(context.Response, "<!DOCTYPE html>");
                        await write(context.Response, "<title>Internal Chonk Error</title>");
                        await write(context.Response, "<h1>&#x1F994: Oops, something went wrong!</h1>");
                        await write(context.Response, $"<p> Here's a sneak peek: <pre><code>{truncate(feature.Error.Message)}</code></pre></p>");
                        await write(context.Response, @"<img src=""/images/500-internal-chonk-error.png"" alt=""A very upside-down chonk"" />");
                    }

                    static async Task write(HttpResponse res, string line) => await res.WriteAsync($"{line}\r\n");
                    static string truncate(string original, int length = 100) => original?.Length < length ? original : $"{original.Substring(0, length)}...";
                });
            });
        }
    }
}
