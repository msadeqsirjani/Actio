using Actio.Common.Authentication;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;
using Actio.Services.Identity.Services;
using FluentAssertions;
using FluentAssertions.Common;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Actio.Services.Identity.Tests.Unit.Services
{
    public class UserServiceTest
    {
        [Fact]
        public async Task User_Service_Login_Should_Return_Jwt()
        {
            var email = "msadeqesirjani@yahoo.com";
            var password = "secret";
            var name = "msadeqsirjani";
            var salt = "salt";
            var hash = "hash";
            var token = "token";


            var userRepositoryMock = new Mock<IUserRepository>();
            var encryptionMock = new Mock<IEncryption>();
            var jwtHandlerMock = new Mock<IJwtHandler>();

            encryptionMock.Setup(x => x.GetSalt()).Returns(salt);
            encryptionMock.Setup(x => x.GetHash(password, salt)).Returns(hash);

            jwtHandlerMock.Setup(x => x.Create(It.IsAny<Guid>())).Returns(new JsonWebToken
            {
                Token = token
            });

            var user = new User(email, name);

            user.SetPassword(password, encryptionMock.Object);

            userRepositoryMock.Setup(x => x.GetAsync(email)).ReturnsAsync(user);

            var userService = new UserService(userRepositoryMock.Object, encryptionMock.Object, jwtHandlerMock.Object);

            var jwt = await userService.LoginAsync(email, password);

            userRepositoryMock.Verify(x => x.GetAsync(email), Times.Once);
            jwtHandlerMock.Verify(x => x.Create(It.IsAny<Guid>()), Times.Once);

            jwt.Should().NotBeNull();
            jwt.Token.Should().IsSameOrEqualTo(token);
        }
    }
}
