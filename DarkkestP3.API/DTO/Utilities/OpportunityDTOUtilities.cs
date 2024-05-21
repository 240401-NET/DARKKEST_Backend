using DarkkestP3.API.DTO;
using DarkkestP3.API.Model;

namespace DarkkestP3.API.Utility;

public static class OpportunityDTOUtilities
{
    public static Opportunity OppFromDTO(NewOpp newOpp, string userId)
    {
        return new Opportunity(){
            AppUserId = userId,
            JobTitle = newOpp.JobTitle,
            Description = newOpp.Description
        };
    }
}