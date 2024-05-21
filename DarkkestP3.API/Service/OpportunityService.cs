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
        Opportunity opp = oppDTOUtil.OppFromDTO(newOpp, userId);        
        return _oppRepo.CreateOpp(opp);
    }
}