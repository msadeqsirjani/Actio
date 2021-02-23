using Actio.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Actio.Api.Tests.Unit.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void Home_Controller_Get_Should_Return_String_Content()
        {
            var controller = new HomeController();

            var result = controller.Get();

            var contentResult = result.AsOrDefault<ContentResult>();

            contentResult.Should().NotBeNull();
            result.Should().NotBeNull();
            contentResult.Content.Should().BeSameAs("Hello from Actio Api");
        }
    }
}
