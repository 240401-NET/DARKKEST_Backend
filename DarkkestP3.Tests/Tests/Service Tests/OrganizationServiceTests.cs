using DarkkestP3.API.DB;
using DarkkestP3.API.DTO;
using DarkkestP3.API.Model;
using DarkkestP3.API.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DarkkestP3.API.Tests.Service
{
    public class OrganizationServiceTests
    {
        private OrganizationService _organizationService;
        private CommunityDBContext _context;

        public OrganizationServiceTests()
        {
            // Create options for DbContext
            var options = new DbContextOptionsBuilder<CommunityDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique name to create a new database for each test
                .Options;

            // Create a real DbContext
            _context = new CommunityDBContext(options);

            // Create an instance of the ApplicationService with the real context
            _organizationService = new OrganizationService(_context);
        }

        [Fact]
        public async Task RegisterOrganization_CreatesOrganization()
        {
            // Arrange
            var createOrganization = new RegisterOrganization { Name = "Test Org", Address = "Test Address" };

            // Act
            var result = await _organizationService.RegisterOrganization(createOrganization);

            // Assert
            Assert.Equal(IdentityResult.Success, result);
            var organization = _context.Organizations.FirstOrDefault(o => o.Name == "Test Org");
            Assert.NotNull(organization);
            Assert.Equal("Test Address", organization.Address);
        }

        [Fact]
        public async Task UpdateOrganization_UpdatesOrganization()
        {
            // Arrange
            var organization = new Organization { Name = "Test Org", Address = "Test Address" };
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();
            var updateOrganization = new UpdateOrganization { OrgId = organization.OrgId, Name = "Updated Org", Address = "Updated Address" };

            // Act
            var result = await _organizationService.UpdateOrganization(updateOrganization);

            // Assert
            Assert.Equal(IdentityResult.Success, result);
            var updatedOrganization = _context.Organizations.Find(organization.OrgId);
            Assert.Equal("Updated Org", updatedOrganization.Name);
            Assert.Equal("Updated Address", updatedOrganization.Address);
        }

        [Fact]
        public async Task DeleteOrganization_DeletesOrganization()
        {
            // Arrange
            var organization = new Organization { Name = "Test Org", Address = "Test Address" };
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();
            var deleteOrganization = new DeleteOrganization { OrgId = organization.OrgId };

            // Act
            var result = await _organizationService.DeleteOrganization(deleteOrganization);

            // Assert
            Assert.Equal(IdentityResult.Success, result);
            var deletedOrganization = _context.Organizations.Find(organization.OrgId);
            Assert.Null(deletedOrganization);
        }

        [Fact]
        public async Task GetOrganization_ReturnsOrganization()
        {
            // Arrange
            var organization = new Organization { Name = "Test Org", Address = "Test Address" };
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();

            // Act
            var result = await _organizationService.GetOrganization(organization.OrgId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(organization.OrgId, result.OrgId);
            Assert.Equal("Test Org", result.Name);
            Assert.Equal("Test Address", result.Address);
        }

        [Fact]
        public async Task GetOrganizationByName_ReturnsOrganization()
        {
            // Arrange
            var organization = new Organization { Name = "Test Org", Address = "Test Address" };
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();

            // Act
            var result = await _organizationService.GetOrganizationByName("Test Org");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(organization.OrgId, result.OrgId);
            Assert.Equal("Test Org", result.Name);
            Assert.Equal("Test Address", result.Address);
        }

        [Fact]
        public async Task GetOrganizations_ReturnsOrganizations()
        {
            // Arrange
            var organizations = new List<Organization>
            {
                new Organization { Name = "Test Org 1", Address = "Test Address 1" },
                new Organization { Name = "Test Org 2", Address = "Test Address 2" },
                new Organization { Name = "Test Org 3", Address = "Test Address 3" }
            };
            foreach (var organization in organizations)
            {
                _context.Organizations.Add(organization);
            }
            await _context.SaveChangesAsync();

            // Act
            var result = await _organizationService.GetOrganizations();

            // Assert
            Assert.Equal(3, result.Count());
            Assert.Contains(result, o => o.Name == "Test Org 1" && o.Address == "Test Address 1");
            Assert.Contains(result, o => o.Name == "Test Org 2" && o.Address == "Test Address 2");
            Assert.Contains(result, o => o.Name == "Test Org 3" && o.Address == "Test Address 3");
        }
    }
}