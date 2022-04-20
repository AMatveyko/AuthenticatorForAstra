using Common.DTO;
using UserServiceInterface.DTO;

namespace UserServiceInterface;

public interface IAccounting
{
    Result<UserData> GetUser(string username);
}