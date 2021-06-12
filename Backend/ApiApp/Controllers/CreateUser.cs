using ApiApp.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace ApiApp.Controllers
{
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
        public async Task Post([FromBody]string str) 
        {
            _log.Debug($"Entered CreateUser-Post with Value {str}");

            var user = new User();
            user.UserName = str;
            user.PartitionKey = str;
            user.RowKey = Guid.NewGuid().ToString();
            await _userRepository.AddUser(user);
        }
    }
}
