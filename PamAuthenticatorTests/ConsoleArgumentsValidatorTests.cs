using NUnit.Framework;
using PamAuthenticator;
using PamAuthenticator.ArgumentsWorkers;
using PamAuthenticator.DTO;

namespace PamAuthenticatorTests;

internal sealed class ConsoleArgumentsValidatorTests
{
    [Test]
    public void ValidDataTest() {
        var arguments = new Arguments {
            PamType = "auth",
            Username = "username",
            Password = "password"
        };
        
        Validate(arguments);
    }

    [Test]
    public void InvalidTypeTest() {
        var arguments = new Arguments {
            PamType = "type",
            Username = "username",
            Password = "password"
        };

        Assert.Catch<ArgumentOutOfRangeException>(() => Validate(arguments));
    }
    
    [Test]
    public void InvalidUsernameTest() {
        var arguments = new Arguments {
            PamType = "type",
            Password = "password"
        };

        Assert.Catch<ArgumentOutOfRangeException>(() => Validate(arguments));
    }
    
    [Test]
    public void InvalidPasswordTest() {
        var arguments = new Arguments {
            PamType = "type",
            Username = "username",
            Password = ""
        };

        Assert.Catch<ArgumentOutOfRangeException>(() => Validate(arguments));
    }
    
    private static void Validate(Arguments arguments) => ConsoleArgumentsValidator.Validate(arguments); 
}