using DarkkestP3.API.DTO;
using DarkkestP3.API.Model;
using DarkkestP3.API.Repository;
using oppDTOUtil = DarkkestP3.API.Utility.OpportunityDTOUtilities;

namespace DarkkestP3.API.Service;

public class OpportunityService : IOpportunityService
{
    private readonly IOpportunityRepository _oppRepo;
    
    public OpportunityService(IOpportunityRepository oppRepo)
    {
        _oppRepo = oppRepo;
    }

    public IEnumerable<Opportunity> GetAllOpps()
    {
        return _oppRepo.GetAllOpps();
    }

    public Opportunity GetOppById(int id)
    {
        return _oppRepo.GetOppById(id);
    }

    public Opportunity CreateOpp(NewOpp newOpp, string userId)
    {
        Opportunity opp = oppDTOUtil.OppFromNewDTO(newOpp, userId);        
        return _oppRepo.CreateOpp(opp);
    }

    public Opportunity UpdateOpp(UpdateOpp updateOpp, string userId)
    {
        int oppId = updateOpp.Id;
        var opp = GetOppById(oppId);        
        if(opp is null) return null!;

        if(opp.AppUserId == userId) 
        {
            Opportunity newOpp = oppDTOUtil.OppFromUpdateDTO(updateOpp, userId);
            return _oppRepo.UpdateOpp(newOpp);
        }
        return null!;
    }

    public Opportunity DeleteOpp(int id)
    {
        var opp = GetOppById(id);
        return _oppRepo.DeleteOpp(opp);
    }
}