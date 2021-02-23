using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using Actio.Services.Activities.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Actio.Services.Activities.Tests.Unit.Services
{
    public class ActivityServiceTest
    {
        [Fact]
        public async Task Activity_Service_Add_Async_Should_Succeed()
        {
            var category = "test";
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var activityRepositoryMock = new Mock<IActivityRepository>();

            categoryRepositoryMock.Setup(x => x.GetAsync(category)).ReturnsAsync(new Category(category));

            var activityService = new ActivityService(activityRepositoryMock.Object, categoryRepositoryMock.Object);

            var id = Guid.NewGuid();
            await activityService.AddAsync(id, "activity", category, "description", Guid.NewGuid(), DateTime.UtcNow);

            categoryRepositoryMock.Verify(x => x.GetAsync(category), Times.Once);
            activityRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Activity>()), Times.Once);
        }
    }
}
