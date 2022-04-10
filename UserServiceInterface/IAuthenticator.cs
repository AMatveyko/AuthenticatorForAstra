using UserServiceInterface.DTO;

namespace UserServiceInterface;

public interface IAuthenticator
{
    bool IsValidCredentials(Credentials credentials);
}