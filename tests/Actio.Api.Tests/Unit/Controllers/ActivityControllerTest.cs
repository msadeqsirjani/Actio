using Actio.Api.Controllers;
using Actio.Common.Commands;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RawRabbit;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Actio.Api.Tests.Unit.Controllers
{
    public class ActivityControllerTest
    {
        [Fact]
        public void Activity_Controller_Get_Should_Return_String_Content()
        {
            var busClientMock = new Mock<IBusClient>();

            var controller = new ActivityController(busClientMock.Object);

            var result = controller.Get();

            var contentResult = result.AsOrDefault<ContentResult>();

            contentResult.Should().NotBeNull();
            contentResult.Should().IsSameOrEqualTo("Secured");
        }

        [Fact]
        public async Task Activity_Controller_Post_Should_Return_Accepted()
        {
            var busClientMock = new Mock<IBusClient>();

            var controller = new ActivityController(busClientMock.Object);

            var userId = Guid.NewGuid();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, userId.ToString()),
                    }, "test"))
                }
            };

            var command = new CreateActivity
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };

            var result = await controller.Post(command);

            var contentResult = result.AsOrDefault<AcceptedResult>();

            contentResult.Should().NotBeNull();
            contentResult.Location.Should().IsSameOrEqualTo($"activity/{command.Id}");
        }
    }
}
