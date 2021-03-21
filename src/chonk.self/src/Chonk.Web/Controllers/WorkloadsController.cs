using System.Linq;
using Chonk.Services;
using Chonk.Services.Models;
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
        public ActionResult<Workload[]> Get()
        {
            return _manifestReader
                .GetWorkloads()
                ?.ToArray();
        }
    }
}