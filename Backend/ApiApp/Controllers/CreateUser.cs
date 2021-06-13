using ApiApp.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace ApiApp.Controllers
{
    public class CreateUserInput
    {
        public string Name { get; set; }
    }

    [Route("create")]
    [ApiController]
    public class CreateUser : ControllerBase
    {
        private readonly ILogger _log;
        private readonly IUserRepository _userRepository;

        public CreateUser(ILogger log, IUserRepository userRepository)
        {
            _log = log;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task Post([FromBody] CreateUserInput input) 
        {
            _log.Debug($"Entered CreateUser-Post with Value {input.Name}");

            var user = new User();
            user.UserName = input.Name;
            user.PartitionKey = input.Name;
            user.RowKey = Guid.NewGuid().ToString();
            await _userRepository.AddUser(user);
        }
    }
}
