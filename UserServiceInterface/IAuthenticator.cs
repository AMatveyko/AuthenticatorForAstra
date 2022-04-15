using UserServiceInterface.DTO;

namespace UserServiceInterface;

public interface IAuthenticator
{
    Answer VerifyingCredentials(Credentials credentials);
}