using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsOfferApi.Modules.First.Api.Controllers
{
    [Route("[controller]")]
    internal class FirstController : Controller
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<string> Get() => Ok("First module");
    }
}