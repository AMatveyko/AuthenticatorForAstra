using NUnit.Framework;
using PamAuthenticator.DTO;
using PamAuthenticator.SpecifiedWorkers;
using PamAuthenticatorTests.Fake;

namespace PamAuthenticatorTests;

public sealed class AuthenticatorTests
{
    [Test]
    public void ValidAuthenticationTest() {
        var secret = "123123";
        var arguments = new Arguments {
            Username = "administrator",
            Password = "administrator"
        };
        var authenticator = new Authenticator(arguments, new FakeUserAuthenticator(secret), secret);

        var actual = authenticator.Do();

        Assert.AreEqual("success", actual);
    }
    
    [Test]
    public void InvalidAuthenticationTest() {
        var secret = "123123";
        var arguments = new Arguments {
            Username = "administrator",
            Password = "anotherPassword"
        };
        var authenticator = new Authenticator(arguments, new FakeUserAuthenticator(secret), secret);

        var actual = authenticator.Do();

        Assert.AreEqual("deny", actual);
    }
}