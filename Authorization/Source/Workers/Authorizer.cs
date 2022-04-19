using System.Security.Authentication;
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
    
    public Answer VerifyingCredentials(Credentials credentials) =>
        TryVerifyingCredentials(credentials);

    private Answer TryVerifyingCredentials(Credentials credentials) {
        try {
            var userInfo = _data.GetUserInfo(credentials.Username);
            CheckTimeStamp(credentials);
            CheckSignature(credentials, userInfo.Password);
            return new Answer();
        }
        catch (Exception e) {
            return new Answer {IsError = true, Message = e.Message};
        }
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
        var timeStamp = TimeStampHelper.GetTimeStamp();
        var signatureInfo = SignatureCreator.Create(passwordFromBd, _secret, timeStamp);
        if (signatureInfo.Signature != credentials.PasswordSignature) {
            throw new AuthenticationException("Invalid password.");
        }
    }
}