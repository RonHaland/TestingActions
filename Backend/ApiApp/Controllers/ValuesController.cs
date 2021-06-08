using Microsoft.AspNetCore.Mvc;

namespace ApiApp.Controllers
{
    [Route("lmao")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost]
        public string Lmao()
        {
            return "kek";


        }
    }
}
