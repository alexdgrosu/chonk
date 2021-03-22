using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chonk.Services.Extensions
{
    public static class StreamJsonExtensions
    {
        // * See: https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-configure-options?pivots=dotnet-5-0
        public static JsonSerializerOptions DefaultOptions { get; set; } = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static ValueTask<T> FromJsonToAsync<T>(this Stream stream, JsonSerializerOptions options = null)
        {
            return JsonSerializer.DeserializeAsync<T>(stream, options ?? DefaultOptions);
        }
    }
}