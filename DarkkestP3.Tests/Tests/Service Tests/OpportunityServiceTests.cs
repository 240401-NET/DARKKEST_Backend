using DarkkestP3.API.DTO;
using DarkkestP3.API.Model;
using DarkkestP3.API.Repository;
using DarkkestP3.API.Service;
using DarkkestP3.API.Utility;
using Moq;

namespace DarkkestP3.Tests;

public class OpportunityServiceTests
{

    [Fact]
    public void GetAllOpps_returns_OpportunityList()
    {
        Mock<IOpportunityRepository> mockRepo = new Mock<IOpportunityRepository>();
        OpportunityService opportunityService = new OpportunityService(mockRepo.Object);
        Opportunity foundOpportunity = new Opportunity
        {
            OppId = 1,
            AppUserId = "user",
            JobTitle = "JobTitle",
            Description = "Description"
        };
        Opportunity updatedOpportunity = new Opportunity
        {
            OppId = 1,
            AppUserId = "user",
            JobTitle = "newJobTitle",
            Description = "UpdatedDescription"
        };
        mockRepo.Setup(repo => repo.GetAllOpps()).Returns(new List<Opportunity> { foundOpportunity, updatedOpportunity });
        
        IEnumerable<Opportunity> returnedOpp = opportunityService.GetAllOpps();
        Assert.Equal(2, returnedOpp.Count());
    }

    [Theory]
    [InlineData("user1")]
    [InlineData("user")]
    public void GetUserOpps_returns_OpportunityListForUser(string userId)
    {
        Mock<IOpportunityRepository> mockRepo = new Mock<IOpportunityRepository>();
        OpportunityService opportunityService = new OpportunityService(mockRepo.Object);

        List<Opportunity> OpportunityList = [
            new Opportunity
            {
                OppId = 1,
                AppUserId = "user1",
                JobTitle = "JobTitle",
                Description = "Description"
            }, 
            new Opportunity
            {
                OppId = 2,
                AppUserId = "user",
                JobTitle = "JobTitle",
                Description = "Description"
            }, 
            new Opportunity
            {
                OppId = 3,
                AppUserId = "user1",
                JobTitle = "JobTitle",
                Description = "Description"
            }, 
            new Opportunity
            {
                OppId = 4,
                AppUserId = "user",
                JobTitle = "JobTitle",
                Description = "Description"
            }, 
            new Opportunity
            {
                OppId = 5,
                AppUserId = "user1",
                JobTitle = "JobTitle",
                Description = "Description"
            }];

        mockRepo.Setup(repo => repo.GetUserOpps(userId)).Returns(OpportunityList.Where(opp => opp.AppUserId == userId).ToList());
        IEnumerable<Opportunity> returnedOpp = opportunityService.GetUserOpps(userId);

        Assert.All(returnedOpp, result => Assert.Equal(userId, result.AppUserId));       
    }

    [Fact]
    public void GetOppById_returns_Opportunity()
    {
        Mock<IOpportunityRepository> mockRepo = new Mock<IOpportunityRepository>();
        OpportunityService opportunityService = new OpportunityService(mockRepo.Object);
        Opportunity foundOpportunity = new Opportunity
        {
            OppId = 1,
            AppUserId = "user",
            JobTitle = "JobTitle",
            Description = "Description"
        };
        mockRepo.Setup(repo => repo.GetOppById(1)).Returns(foundOpportunity);
        
        Opportunity returnedOpp = opportunityService.GetOppById(1);
        

        Assert.Equal(foundOpportunity, returnedOpp);
    }

    [Fact]
    public void GetOppById_returns_Null()
    {
        Mock<IOpportunityRepository> mockRepo = new Mock<IOpportunityRepository>();
        OpportunityService opportunityService = new OpportunityService(mockRepo.Object);
        mockRepo.Setup(repo => repo.GetOppById(1)).Returns((Opportunity)null);
        
        Opportunity returnedOpp = opportunityService.GetOppById(1);

        Assert.Null(returnedOpp);
    }


    [Theory]
    [InlineData(1)]
    public void DeleteOpp_Returns_deletedOpportunity(int oppId)
    {
        Mock<IOpportunityRepository> mockRepo = new Mock<IOpportunityRepository>();
        OpportunityService opportunityService = new OpportunityService(mockRepo.Object);
        Opportunity opportunity = new Opportunity
        {
            OppId = 1,
            AppUserId = "user",
            JobTitle = "JobTitle",
            Description = "Description"
        };
        mockRepo.Setup(repo => repo.GetOppById(oppId)).Returns(opportunity);
        mockRepo.Setup(repo => repo.DeleteOpp(opportunity)).Returns(opportunity);
        
        Opportunity newOpp = opportunityService.DeleteOpp(oppId);

        Assert.Equal(opportunity, newOpp);
    }

    [Fact]
    public void CreateOpp_returns_Opportunity()
    {
        Mock<IOpportunityRepository> mockRepo = new Mock<IOpportunityRepository>();
        OpportunityService opportunityService = new OpportunityService(mockRepo.Object);
        NewOpp newOpp = new NewOpp{
            JobTitle = "JobTitle",
            Description = "Description"
        };

        Opportunity opportunity = new Opportunity
        {
            AppUserId = "UserId",
            JobTitle = "JobTitle",
            Description = "Description"
        };
        Opportunity CreatedOpportunity = new Opportunity
        {
            OppId = 1,
            AppUserId = "UserId",
            JobTitle = "JobTitle",
            Description = "Description"
        };

        mockRepo.Setup(repo => repo.CreateOpp(It.IsAny<Opportunity>())).Returns(CreatedOpportunity);
        
        Opportunity returnedOpp = opportunityService.CreateOpp(newOpp, "UserId");
        Assert.Equal(CreatedOpportunity, returnedOpp);
    }


    [Theory]
    [InlineData("user")]
    public void UpdateOpp_returnsNull_if_Opportunity_NotFound(string userId)
    {
        Mock<IOpportunityRepository> mockRepo = new Mock<IOpportunityRepository>();
        OpportunityService opportunityService = new OpportunityService(mockRepo.Object);
        UpdateOpp updateOpp = new UpdateOpp
        {
            Id = 2,
            JobTitle = "JobTitle",
            Description = "Description" 
        };
        mockRepo.Setup(repo => repo.GetOppById(updateOpp.Id)).Returns((Opportunity)null);
        
        Opportunity newOpp = opportunityService.UpdateOpp(updateOpp, userId);

        Assert.Null(newOpp);
    }

    [Theory]
    [InlineData("WrongUser")]
    public void UpdateOpp_returns_Null_if_Opportunity_User_Does_Not_Match(string userId)
    {
        Mock<IOpportunityRepository> mockRepo = new Mock<IOpportunityRepository>();
        OpportunityService opportunityService = new OpportunityService(mockRepo.Object);
        Opportunity foundOpportunity = new Opportunity
        {
            OppId = 1,
            AppUserId = "user",
            JobTitle = "JobTitle",
            Description = "Description"
        };
        Opportunity updatedOpportunity = new Opportunity
        {
            OppId = 1,
            AppUserId = "user",
            JobTitle = "newJobTitle",
            Description = "UpdatedDescription"
        };
        UpdateOpp updateOpp = new UpdateOpp
        {
            Id = 1,
            JobTitle = "newJobTitle",
            Description = "UpdatedDescription" 
        };
        mockRepo.Setup(repo => repo.GetOppById(updateOpp.Id)).Returns(foundOpportunity);
        mockRepo.Setup(repo => repo.UpdateOpp(It.IsAny<Opportunity>())).Returns(updatedOpportunity);
        
        Opportunity newOpp = opportunityService.UpdateOpp(updateOpp, userId);
        Assert.Null(newOpp);
    }

    [Theory]
    [InlineData("user")]
    public void UpdateOpp_returns_updatedOpportunity(string userId)
    {
        Mock<IOpportunityRepository> mockRepo = new Mock<IOpportunityRepository>();
        OpportunityService opportunityService = new OpportunityService(mockRepo.Object);
        Opportunity foundOpportunity = new Opportunity
        {
            OppId = 1,
            AppUserId = "user",
            JobTitle = "JobTitle",
            Description = "Description"
        };
        Opportunity updatedOpportunity = new Opportunity
        {
            OppId = 1,
            AppUserId = "user",
            JobTitle = "newJobTitle",
            Description = "UpdatedDescription"
        };
        UpdateOpp updateOpp = new UpdateOpp
        {
            Id = 1,
            JobTitle = "newJobTitle",
            Description = "UpdatedDescription" 
        };
        mockRepo.Setup(repo => repo.GetOppById(updateOpp.Id)).Returns(foundOpportunity);
        mockRepo.Setup(repo => repo.UpdateOpp(It.IsAny<Opportunity>())).Returns(updatedOpportunity);
        
        Opportunity newOpp = opportunityService.UpdateOpp(updateOpp, userId);
        Assert.Equal(updatedOpportunity, newOpp);
    }
}