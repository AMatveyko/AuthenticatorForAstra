using UserServiceInterface.DTO;

namespace UserServiceInterface;

public interface IAuthenticator
{
    Result VerifyingCredentials(Credentials credentials);
}