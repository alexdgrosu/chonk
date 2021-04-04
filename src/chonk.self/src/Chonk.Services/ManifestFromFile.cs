using System.IO;
using System.Threading.Tasks;
using Chonk.Services.Settings;
using Chonk.Services.Models;
using Chonk.Services.Extensions;

namespace Chonk.Services
{
    public class ManifestFromFile : IManifestReader
    {
        private readonly WorkloadsSettings _settings;

        public ManifestFromFile(WorkloadsSettings settings)
        {
            _settings = settings;
        }

        public async Task<Manifest> Get()
        {
            using var stream = File.OpenRead(_settings.ManifestSource);

            var manifest = await stream
                                .FromJsonToAsync<Manifest>()
                                .ConfigureAwait(false);

            return manifest;
        }
    }
}