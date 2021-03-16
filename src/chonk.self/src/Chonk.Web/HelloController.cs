using Microsoft.AspNetCore.Mvc;

namespace Chonk.Web
{
    [Route("api/{controller}")]
    public class HelloController : ControllerBase
    {
        // TODO: Remove as soon as we're done setting up middleware
        public ActionResult<string[]> Get()
        {
            return new []
            {
                "Hello",
                "Chonk"
            };
        }
    }
}