using System;
using System.Linq;
using System.Threading.Tasks;
using Chonk.Services;
using Chonk.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chonk.Web.Controllers
{
    [Route("api/[controller]")]
    public class WorkloadsController : ControllerBase
    {
        private readonly IManifestReader _manifestReader;

        public WorkloadsController(IManifestReader manifestReader)
        {
            _manifestReader = manifestReader;
        }

        [HttpGet]
        public async Task<ActionResult<Workload[]>> Get()
        {
            var manifest = await _manifestReader.Get();
            return manifest.Workloads.ToArray();
        }
    }
}