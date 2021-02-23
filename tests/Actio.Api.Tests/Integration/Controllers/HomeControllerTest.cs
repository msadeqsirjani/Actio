using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Actio.Api.Tests.Integration.Controllers
{
    public class HomeControllerTest
    {
        private readonly HttpClient _client;

        public HomeControllerTest()
        {
            var server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact]
        public async Task Home_Controller_Get_Should_Return_String_Content()
        {
            var response = await _client.GetAsync("/api/Home/");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            content.Should().NotBeNullOrEmpty();
            content.Should().IsSameOrEqualTo("Hello from Actio Api");
        }
    }
}
