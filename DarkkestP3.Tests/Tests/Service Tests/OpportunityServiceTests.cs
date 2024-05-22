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

    // [Fact]
    // public void CreateOpp_returns_Opportunity()
    // {
    //     Mock<IOpportunityRepository> mockRepo = new Mock<IOpportunityRepository>();
    //     OpportunityService opportunityService = new OpportunityService(mockRepo.Object);
    //     NewOpp newOpp = new NewOpp{
    //         JobTitle = "JobTitle",
    //         Description = "Description"
    //     };

    //     Opportunity opportunity = new Opportunity
    //     {
    //         AppUserId = "UserId",
    //         JobTitle = "JobTitle",
    //         Description = "Description"
    //     };
    //     Opportunity CreatedOpportunity = new Opportunity
    //     {
    //         OppId = 1,
    //         AppUserId = "UserId",
    //         JobTitle = "JobTitle",
    //         Description = "Description"
    //     };


    //     mockRepo.Setup(repo => repo.CreateOpp(opportunity)).Returns(CreatedOpportunity);
        
    //     Opportunity returnedOpp = opportunityService.CreateOpp(newOpp, "UserId");
    //     if(returnedOpp == null){
    //         Console.WriteLine("returned opp is null----------");
    //     }
    //     Assert.Equal(CreatedOpportunity, returnedOpp);
    // }


    // [Theory]
    // [InlineData("user")]
    // public void UpdateOpp_returnsNull_if_Opportunity_NotFound(string userId)
    // {
    //     Mock<IOpportunityRepository> mockRepo = new Mock<IOpportunityRepository>();
    //     OpportunityService opportunityService = new OpportunityService(mockRepo.Object);
    //     UpdateOpp updateOpp = new UpdateOpp
    //     {
    //         Id = 2,
    //         JobTitle = "JobTitle",
    //         Description = "Description" 
    //     };
    //     mockRepo.Setup(repo => repo.GetOppById(updateOpp.Id)).Returns((Opportunity)null);
        
    //     Opportunity newOpp = opportunityService.UpdateOpp(updateOpp, userId);

    //     Assert.Null(newOpp);
    // }

    // [Theory]
    // [InlineData("user")]
    // public void UpdateOpp_returns_updatedOpportunity(string userId)
    // {
    //     Mock<IOpportunityRepository> mockRepo = new Mock<IOpportunityRepository>();
    //     OpportunityService opportunityService = new OpportunityService(mockRepo.Object);
    //     Opportunity foundOpportunity = new Opportunity
    //     {
    //         OppId = 1,
    //         AppUserId = "user",
    //         JobTitle = "JobTitle",
    //         Description = "Description"
    //     };
    //     Opportunity updatedOpportunity = new Opportunity
    //     {
    //         OppId = 1,
    //         AppUserId = "user",
    //         JobTitle = "newJobTitle",
    //         Description = "UpdatedDescription"
    //     };
    //     UpdateOpp updateOpp = new UpdateOpp
    //     {
    //         Id = 1,
    //         JobTitle = "newJobTitle",
    //         Description = "UpdatedDescription" 
    //     };
    //     mockRepo.Setup(repo => repo.GetOppById(updateOpp.Id)).Returns(foundOpportunity);
    //     mockRepo.Setup(repo => repo.UpdateOpp(updatedOpportunity)).Returns(updatedOpportunity);
        
    //     opportunityService.UpdateOpp(updateOpp, userId);
    //     mockRepo.Verify(repo => repo.UpdateOpp(updatedOpportunity), Times.Exactly(1));
    // }
}