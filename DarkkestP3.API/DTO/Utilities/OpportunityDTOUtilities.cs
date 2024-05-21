using DarkkestP3.API.DTO;
using DarkkestP3.API.Model;

namespace DarkkestP3.API.Utility;

public static class OpportunityDTOUtilities
{
    public static Opportunity OppFromNewDTO(NewOpp newOpp, string userId)
    {
        return new Opportunity(){
            AppUserId = userId,
            JobTitle = newOpp.JobTitle,
            Description = newOpp.Description
        };
    }

    public static Opportunity OppFromUpdateDTO(UpdateOpp updateOpp, string userId)
    {
        return new Opportunity(){
            OppId = updateOpp.Id,
            AppUserId = userId,
            JobTitle = updateOpp.JobTitle,
            Description = updateOpp.Description
        };
    }
}