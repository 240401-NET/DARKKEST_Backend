using DarkkestP3.API.DB;
using DarkkestP3.API.DTO;
using DarkkestP3.API.Model;
using DarkkestP3.API.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkkestP3.API.Tests.Service
{
    [TestFixture]
    public class ApplicationServiceTests : IClassFixture<CommunityDBContext>
    {
        private ApplicationService _applicationService;
        private Mock<CommunityDBContext> _mockContext;

        public ApplicationServiceTests()
        {
            // Setup the mock context
            _mockContext = new Mock<CommunityDBContext>();

            // Create an instance of the ApplicationService with the mock context
            _applicationService = new ApplicationService(_mockContext.Object);
        }

        [Fact]
        public async Task GetApplications_ReturnsAllApplications()
        {
            // Arrange
            var applications = new List<Application>
            {
                new Application { AppId = 1, UserId = 1, OppId = 1, AppStatus = ApplicationStatus.NotSubmitted, History = "", Notifications = "" },
                new Application { AppId = 2, UserId = 2, OppId = 1, AppStatus = ApplicationStatus.NotSubmitted, History = "", Notifications = "" },
                new Application { AppId = 3, UserId = 1, OppId = 2, AppStatus = ApplicationStatus.NotSubmitted, History = "", Notifications = "" }
            };
            var expectedDTOs = applications.Select(a => new ApplicationDTO
            {
                AppId = a.AppId,
                UserId = a.UserId,
                OppId = a.OppId,
                AppStatus = a.AppStatus,
                History = a.History,
                Notifications = a.Notifications
            });
            _mockContext.Setup(c => c.Applications).ReturnsDbSet(applications);

            // Act
            var result = await _applicationService.GetApplications();

            // Assert
            Assert.Equal(expectedDTOs, result);
        }

        [Fact]
        public async Task GetApplication_ReturnsApplicationById()
        {
            // Arrange
            var application = new Application { AppId = 1, UserId = 1, OppId = 1, AppStatus = ApplicationStatus.NotSubmitted, History = "", Notifications = "" };
            var expectedDTO = new ApplicationDTO
            {
                AppId = application.AppId,
                UserId = application.UserId,
                OppId = application.OppId,
                AppStatus = application.AppStatus,
                History = application.History,
                Notifications = application.Notifications
            };
            _mockContext.Setup(c => c.Applications.Find(1)).Returns(application);

            // Act
            var result = await _applicationService.GetApplication(1);

            // Assert
            Assert.Equal(expectedDTO, result);
        }

        [Fact]
        public async Task CreateApplication_AddsNewApplication()
        {
            // Arrange
            var createApplication = new CreateApplication { UserId = 1, OppId = 1 };
            _mockContext.Setup(c => c.Applications.Add(It.IsAny<Application>()));
            _mockContext.Setup(c => c.SaveChanges());

            // Act
            var result = await _applicationService.CreateApplication(createApplication);

            // Assert
            Assert.Equal(IdentityResult.Success, result);
            _mockContext.Verify(c => c.Applications.Add(It.IsAny<Application>()), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        // Add more test cases for the other methods in the ApplicationService class
    }
}