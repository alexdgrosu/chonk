using System.Collections.Generic;
using Chonk.Services.Models;

namespace Chonk.Services
{
    public interface IManifestReader
    {
         IEnumerable<Workload> GetWorkloads();
    }
}