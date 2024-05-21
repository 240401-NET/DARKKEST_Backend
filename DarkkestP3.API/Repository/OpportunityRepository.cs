using DarkkestP3.API.DB;
using DarkkestP3.API.Model;

namespace DarkkestP3.API.Repository;

public class OpportunityRepository : IOpportunityRepository
{
    private readonly CommunityDBContext _oppContext;

    public OpportunityRepository(CommunityDBContext oppContext) => _oppContext = oppContext;

    public IEnumerable<Opportunity> GetAllOpps()
    {
        return _oppContext.Opputunities.ToList();
    }

    public Opportunity GetOppById(int id)
    {
        return _oppContext.Opputunities.FirstOrDefault(o => o.OppId == id)!;
    }

    public Opportunity CreateOpp(Opportunity newOpp)
    {
        _oppContext.Opputunities.Add(newOpp);
        _oppContext.SaveChanges();
        return newOpp;
    }

    public Opportunity UpdateOpp(Opportunity updateOpp)
    {
       var opp =  _oppContext.Opputunities.Find(updateOpp.OppId);
       _oppContext.Opputunities.Entry(opp!).CurrentValues.SetValues(updateOpp);
       _oppContext.SaveChanges();
       return opp!;
    }

    public Opportunity DeleteOpp(Opportunity deleteOpp)
    {
        _oppContext.Opputunities.Remove(deleteOpp);
        _oppContext.SaveChanges();
        return deleteOpp;
    }
}