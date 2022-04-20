using Authorization.Source.Workers;
using Common;
using Common.Db;
using NUnit.Framework;
using UserServiceInterface.DTO;

namespace AuthorizationTests;

public class AuthorizerTests
{
    
    private const string Username = "administrator";
    private const string Password = "administrator";
    private const string Secret = "123123123"; 
    
    [Test]
    public void ValidTest() {
        
        var actual = GetAuthenticationAnswer(GetValidCredentials());
        
        Assert.AreEqual(false, actual.IsError);
    }

    [Test]
    public void InvalidPasswordTest() {
        var actual = GetAuthenticationAnswer(GetInvalidPasswordCredentials());
        
        Assert.AreEqual(true, actual.IsError);
    }
    
    [Test]
    public void OutdatedTimestampTest() {
        var actual = GetAuthenticationAnswer(GetOutdateCredentials());
        
        Assert.AreEqual(true, actual.IsError);
    }
    
    [Test]
    public void FutureTimestampTest() {
        var actual = GetAuthenticationAnswer(GetFutureCredentials());

        Assert.AreEqual(true, actual.IsError);
    }

    private static Result GetAuthenticationAnswer(Credentials credentials) {
        var dataSource = new TestDatabase();
        var authorizer = new Authorizer(dataSource, Secret);
        return authorizer.VerifyingCredentials(credentials);
    }
    
    private static Credentials GetValidCredentials() =>
        GetCredentials(Password, TimeStampHelper.GetTimeStamp());

    private static Credentials GetInvalidPasswordCredentials() =>
        GetCredentials("sdfsdfasdf", TimeStampHelper.GetTimeStamp());
    
    private static Credentials GetOutdateCredentials() =>
        GetCredentials( Password, "21220415164646");
    
    private static Credentials GetFutureCredentials() =>
        GetCredentials( Password, "20220415164646");
    
    private static Credentials GetCredentials(string password, string timestamp) {
        var signatureInfo = SignatureCreator.Create(password, Secret, timestamp);
        return new Credentials {
            PasswordSignature = signatureInfo.Signature,
            TimeStamp = signatureInfo.TimeStamp,
            Username = Username
        };
    }
        
}