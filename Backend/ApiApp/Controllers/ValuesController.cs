using Microsoft.AspNetCore.Mvc;

namespace ApiApp.Controllers
{
    [Route("lmao")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost, HttpGet]
        public ObjectResult Lmao()
        {
            return Ok("kek");
        }
    }
}
