using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Chonk.Services.Configuration;
using Chonk.Services.Models;

namespace Chonk.Services
{
    public class ManifestFromFile : IManifestReader
    {
        private readonly WorkloadsConfiguration _config;

        public ManifestFromFile(WorkloadsConfiguration config)
        {
            _config = config;
        }
  
        public IEnumerable<Workload> GetWorkloads()
        {
            var jsonString = File.ReadAllText(_config.ManifestPath);
            
            // See: https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-configure-options?pivots=dotnet-5-0
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var manifest = JsonSerializer.Deserialize<Manifest>(jsonString, jsonOptions);

            return manifest.Workloads;
        }
    }
}