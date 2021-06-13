using ApiApp.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Net.Mime;

namespace ApiApp.Controllers
{
    public class GetUserInput
    {
        public string Name { get; set; }
    }

    [Route("get")]
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
        public IEnumerable<User> Get([FromBody] GetUserInput name) 
        {
            return _userRepository.GetUsersByName(name.Name);
        }
    }
}
