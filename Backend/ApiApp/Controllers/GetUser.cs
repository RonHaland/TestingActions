using ApiApp.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(200)]
    [Produces(MediaTypeNames.Application.Json)]
    public class GetUser : ControllerBase
    {
        private readonly ILogger _log;
        private readonly IUserRepository _userRepository;

        public GetUser(ILogger log, IUserRepository userRepository)
        {
            _log = log;
            _userRepository = userRepository;
        }

        [HttpPost]
        public IEnumerable<User> Get([FromBody] string name) 
        {
            return _userRepository.GetUsersByName(name);
        }
    }
}
