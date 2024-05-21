using DarkkestP3.API.DB;

namespace DarkkestP3.API.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserDBContext _userContext;

    public UserRepository(UserDBContext userContext) => _userContext = userContext;

    public string GetUserIdByName(string username)
    {
        var userId =  _userContext.ApplicationUsers
            .Where(user => user.UserName == username)
            .Select(user => user.Id)
            .SingleOrDefault();

        return userId!.ToString();
    }
}