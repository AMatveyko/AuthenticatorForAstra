using System.Security.Authentication;
using Authorization.Source.Helpers;
using Common;
using Common.Db;
using UserServiceInterface;
using UserServiceInterface.DTO;

namespace Authorization.Source.Workers;

public sealed class Authorizer : IAuthenticator
{

    private readonly IDataSource _data;
    private readonly string _secret;

    public Authorizer(IDataSource data, string secret) =>
        (_data, _secret) = (data, secret);

    public Result VerifyingCredentials(Credentials credentials) =>
        ResultGetter.Get(() => DoVerifyingCredentials(credentials));

    private void DoVerifyingCredentials(Credentials credentials) {
        var userInfo = _data.GetUserInfo(credentials.Username);
        CheckTimeStamp(credentials);
        CheckSignature(credentials, userInfo.Password);
    }

    private static void CheckTimeStamp(Credentials credentials) {
        var dateTime = TimeStampHelper.GetDateTime(credentials.TimeStamp);
        if (dateTime < DateTime.UtcNow.AddSeconds(-10)) {
            throw new AuthenticationException("Outdated timestamp.");
        }

        if (dateTime > DateTime.UtcNow.AddSeconds(10)) {
            throw new AuthenticationException("Timestamp from the future.");
        }
    }

    private void CheckSignature(Credentials credentials, string passwordFromBd) {
        var signatureInfo = SignatureCreator.Create(passwordFromBd, _secret, credentials.TimeStamp);
        if (signatureInfo.Signature != credentials.PasswordSignature) {
            throw new AuthenticationException("Invalid password.");
        }
    }
}