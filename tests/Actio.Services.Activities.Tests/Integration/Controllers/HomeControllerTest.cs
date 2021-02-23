using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Actio.Services.Activities.Tests.Integration.Controllers
{
    public class HomeControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public HomeControllerTest()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Home_Controller_Should_Return_String_Content()
        {
            var response = await _client.GetAsync("/api/Home/");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            content.Should().IsSameOrEqualTo("Hello from Actio.Service.Activities Api");
        }
    }
}
