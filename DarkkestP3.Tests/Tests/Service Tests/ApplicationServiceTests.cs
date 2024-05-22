using DarkkestP3.API.DB;
using DarkkestP3.API.DTO;
using DarkkestP3.API.Model;
using DarkkestP3.API.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DarkkestP3.API.Tests.Service
{
    public class ApplicationServiceTests
    {
        private ApplicationService _applicationService;
        private CommunityDBContext _context;

        public ApplicationServiceTests()
        {
            // Create options for DbContext
            var options = new DbContextOptionsBuilder<CommunityDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique name to create a new database for each test
                .Options;

            // Create a real DbContext
            _context = new CommunityDBContext(options);

            // Create an instance of the ApplicationService with the real context
            _applicationService = new ApplicationService(_context);
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
            foreach (var application in applications)
            {
                _context.Applications.Add(application);
            }
            _context.SaveChanges(); // Use a method to save changes

            var expectedDTOs = applications.Select(a => new ApplicationDTO
            {
                AppId = a.AppId,
                UserId = a.UserId,
                OppId = a.OppId,
                AppStatus = a.AppStatus,
                History = a.History,
                Notifications = a.Notifications
            }).ToList();

            // Act
            var result = await _applicationService.GetApplications();

            // Assert
            Assert.True(expectedDTOs.SequenceEqual(result, new ApplicationDTOComparer()));
        }

        [Fact]
        public async Task GetApplication_ReturnsApplicationById()
        {
            // Arrange
            var application = new Application { AppId = 1, UserId = 1, OppId = 1, AppStatus = ApplicationStatus.NotSubmitted, History = "", Notifications = "" };
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            var expectedDTO = new ApplicationDTO
            {
                AppId = application.AppId,
                UserId = application.UserId,
                OppId = application.OppId,
                AppStatus = application.AppStatus,
                History = application.History,
                Notifications = application.Notifications
            };

            // Act
            var result = await _applicationService.GetApplication(1);

            // Assert
            var comparer = new ApplicationDTOComparer();
            Assert.True(comparer.Equals(expectedDTO, result));
        }

        [Fact]
        public async Task CreateApplication_AddsNewApplication()
        {
            // Arrange
            var createApplication = new CreateApplication { UserId = 1, OppId = 1 };

            // Act
            var result = await _applicationService.CreateApplication(createApplication);

            // Assert
            Assert.Equal(IdentityResult.Success, result);
            Assert.Single(_context.Applications); // Check that an application was added to the database
        }

        [Fact]
        public async Task UpdateApplication_UpdatesExistingApplication()
        {
            // Arrange
            var application = new Application { AppId = 1, UserId = 1, OppId = 1, AppStatus = ApplicationStatus.NotSubmitted, History = "", Notifications = "" };
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            var updateApplication = new UpdateApplication { UserId = 2, OppId = 2, AppStatus = ApplicationStatus.Pending, History = "Updated", Notifications = "Updated" };

            // Act
            var result = await _applicationService.UpdateApplication(1, updateApplication);

            // Assert
            Assert.Equal(IdentityResult.Success, result);
            var updatedApplication = _context.Applications.Find(1);
            Assert.Equal(updateApplication.UserId, updatedApplication.UserId);
            Assert.Equal(updateApplication.OppId, updatedApplication.OppId);
            Assert.Equal(updateApplication.AppStatus, updatedApplication.AppStatus);
            Assert.Equal(updateApplication.History, updatedApplication.History);
            Assert.Equal(updateApplication.Notifications, updatedApplication.Notifications);
        }

        [Fact]
        public async Task DeleteApplication_DeletesExistingApplication()
        {
            // Arrange
            var application = new Application { AppId = 1, UserId = 1, OppId = 1, AppStatus = ApplicationStatus.NotSubmitted, History = "", Notifications = "" };
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            // Act
            var result = await _applicationService.DeleteApplication(1);

            // Assert
            Assert.Equal(IdentityResult.Success, result);
            Assert.Empty(_context.Applications);
        }

        [Fact]
        public async Task GetApplicationsForUser_ReturnsApplicationsForUser()
        {
            // Arrange
            var applications = new List<Application>
            {
                new Application { AppId = 1, UserId = 1, OppId = 1, AppStatus = ApplicationStatus.NotSubmitted, History = "", Notifications = "" },
                new Application { AppId = 2, UserId = 1, OppId = 2, AppStatus = ApplicationStatus.NotSubmitted, History = "", Notifications = "" },
                new Application { AppId = 3, UserId = 2, OppId = 1, AppStatus = ApplicationStatus.NotSubmitted, History = "", Notifications = "" }
            };
            foreach (var application in applications)
            {
                _context.Applications.Add(application);
            }
            await _context.SaveChangesAsync();

            var expectedDTOs = applications.Where(a => a.UserId == 1).Select(a => new ApplicationDTO
            {
                AppId = a.AppId,
                UserId = a.UserId,
                OppId = a.OppId,
                AppStatus = a.AppStatus,
                History = a.History,
                Notifications = a.Notifications
            }).ToList();

            // Act
            var result = await _applicationService.GetApplicationsForUser(1);

            // Assert
            Assert.True(expectedDTOs.SequenceEqual(result, new ApplicationDTOComparer()));
        }

        [Fact]
        public async Task GetApplicationsForOpportunity_ReturnsApplicationsForOpportunity()
        {
            // Arrange
            var applications = new List<Application>
            {
                new Application { AppId = 1, UserId = 1, OppId = 1, AppStatus = ApplicationStatus.NotSubmitted, History = "", Notifications = "" },
                new Application { AppId = 2, UserId = 2, OppId = 1, AppStatus = ApplicationStatus.NotSubmitted, History = "", Notifications = "" },
                new Application { AppId = 3, UserId = 1, OppId = 2, AppStatus = ApplicationStatus.NotSubmitted, History = "", Notifications = "" }
            };
            foreach (var application in applications)
            {
                _context.Applications.Add(application);
            }
            await _context.SaveChangesAsync();

            var expectedDTOs = applications.Where(a => a.OppId == 1).Select(a => new ApplicationDTO
            {
                AppId = a.AppId,
                UserId = a.UserId,
                OppId = a.OppId,
                AppStatus = a.AppStatus,
                History = a.History,
                Notifications = a.Notifications
            }).ToList();

            // Act
            var result = await _applicationService.GetApplicationsForOpportunity(1);

            // Assert
            Assert.True(expectedDTOs.SequenceEqual(result, new ApplicationDTOComparer()));
        }

        [Fact]
        public async Task SubmitApplication_SubmitsApplication()
        {
            // Arrange
            var application = new Application { AppId = 1, UserId = 1, OppId = 1, AppStatus = ApplicationStatus.NotSubmitted, History = "", Notifications = "" };
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            // Act
            var result = await _applicationService.SubmitApplication(1);

            // Assert
            Assert.Equal(IdentityResult.Success, result);
            var submittedApplication = _context.Applications.Find(1);
            Assert.Equal(ApplicationStatus.Pending, submittedApplication.AppStatus);
        }

        [Fact]
        public async Task ApproveApplication_ApprovesApplication()
        {
            // Arrange
            var application = new Application { AppId = 1, UserId = 1, OppId = 1, AppStatus = ApplicationStatus.Pending, History = "", Notifications = "" };
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            // Act
            var result = await _applicationService.ApproveApplication(1);

            // Assert
            Assert.Equal(IdentityResult.Success, result);
            var approvedApplication = _context.Applications.Find(1);
            Assert.Equal(ApplicationStatus.Approved, approvedApplication.AppStatus);
        }
    }

    public class ApplicationDTOComparer : IEqualityComparer<ApplicationDTO>
    {
        public bool Equals(ApplicationDTO x, ApplicationDTO y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.AppId == y.AppId &&
                x.UserId == y.UserId &&
                x.OppId == y.OppId &&
                x.AppStatus == y.AppStatus &&
                x.History == y.History &&
                x.Notifications == y.Notifications;
        }

        public int GetHashCode(ApplicationDTO obj)
        {
            return HashCode.Combine(obj.AppId, obj.UserId, obj.OppId, obj.AppStatus, obj.History, obj.Notifications);
        }
    }
}
