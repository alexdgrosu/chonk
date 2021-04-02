using System.Collections.Generic;

namespace Chonk.Services.Models
{
    public class Manifest
    {
        public Metadata Metadata { get; set; }

        public IEnumerable<Workload> Workloads { get; set; }

    }
}