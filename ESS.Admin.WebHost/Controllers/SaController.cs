using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ESS.Admin.WebHost.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SaController : ControllerBase
    {
        /// <summary>
        /// Check that API is working
        /// </summary>
        /// <returns></returns>
        [HttpGet("Check")]
        public ActionResult Check()
        {
            return Ok();
        }
    }
}
