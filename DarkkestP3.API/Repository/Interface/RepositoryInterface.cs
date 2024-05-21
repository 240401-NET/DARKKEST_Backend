using DarkkestP3.API.Model;

namespace DarkkestP3.API.Repository;

public interface IUserRepository
{
    string GetUserIdByName(string username);
}

public interface IOpportunityRepository
{
    IEnumerable<Opportunity> GetAllOpps();
    Opportunity GetOppById(int id);
    Opportunity CreateOpp(Opportunity newOpp);
    Opportunity UpdateOpp(Opportunity updateOpp);
}