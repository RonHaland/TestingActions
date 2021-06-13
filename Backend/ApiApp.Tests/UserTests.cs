using ApiApp.Controllers;
using ApiApp.Model;
using Moq;
using NUnit.Framework;
using Serilog;
using Serilog.Core;

namespace ApiApp.Tests
{
    public class UserTests
    {
        private Logger _log;
        private Mock<IUserRepository> _mockedUserRepo;
        private GetUser _SUT;

        [SetUp]
        public void Setup()
        {
            _log = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.Console().CreateLogger();
            _mockedUserRepo = new Mock<IUserRepository>();
            _SUT = new GetUser(_log, _mockedUserRepo.Object);
        }

        [Test]
        public void Should_Call_Get()
        {
            _log.Information("Should Call Get");
            var userInput = new GetUserInput() { Name = "lmao" };
            _SUT.Get(userInput);

            _mockedUserRepo.Verify(x => x.GetUsersByName(It.Is<string>(s => s == "lmao")), Times.Once);
            _log.Debug("Get was Called once");
        }

        [Test]
        [Ignore("Just a dumb test test")]
        public void Should_Call_Get_Twice()
        {
            _log.Information("Should Call Get");
            var userInput = new GetUserInput() { Name = "lmao" };
            _SUT.Get(userInput);

            _mockedUserRepo.Verify(x => x.GetUsersByName(It.Is<string>(s => s == "lmao")), Times.Exactly(2));
            _log.Debug("Get was Called once");
        }
    }
}