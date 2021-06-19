using ApiApp.Model;
using ApiApp.Repository;
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
            _log.Verbose("Getting user {@name}", name);
            _log.Warning("Make sure user {name} exists", name.Name);
            _log.Error("Just logging a random error :)");
            _log.Fatal("This would normally be very bad :(");
            return _userRepository.GetUsersByName(name.Name);
        }
    }
}
