using Authorization.Source.Helpers;
using Common.Db;
using UserServiceInterface;
using UserServiceInterface.DTO;

namespace Authorization.Source.Workers;

public sealed class Authenticator : IAuthenticator
{

    private readonly IDataSource _data;
    private readonly SignatureValidator _validator;

    public Authenticator(IDataSource data, SignatureValidator validator) =>
        (_data, _validator) = (data, validator);

    public Result VerifyingCredentials(Credentials credentials) =>
        ResultGetter.Get(() => DoVerifyingCredentials(credentials));

    private void DoVerifyingCredentials(Credentials credentials) {
        var userInfo = _data.GetUserInfo(credentials.Username);
        _validator.Check(credentials.PasswordSignature, userInfo.Password, credentials.TimeStamp);
    }
}