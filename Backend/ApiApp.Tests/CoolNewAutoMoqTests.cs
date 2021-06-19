using ApiApp.Controllers;
using ApiApp.Model;
using ApiApp.Repository;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;

namespace ApiApp.Tests
{
    public class CoolNewAutoMoqTests : BaseTest
    {
        [Test]
        public void AutoMoqTest_Should_Call_Get()
        {
            using (var mock = AutoMock.GetLoose())
            {
                Logger.Information("Should Call Get");
                var userInput = new GetUserInput() { Name = "lmao" };

                var sut = mock.Create<GetUser>();
                sut.Get(userInput);

                mock.Mock<IUserRepository>().Verify(x => x.GetUsersByName(It.Is<string>(s => s == "lmao")), Times.Once);
                Logger.Debug("Get was Called once");
            }
        }
    }
}
