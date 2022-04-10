using NUnit.Framework;
using PamAuthenticator;
using PamAuthenticator.DTO;

namespace PamAuthenticatorTests;

internal sealed class ConsoleArgumentsValidatorTests
{
    [Test]
    public void ValidDataTest() {
        var arguments = new Arguments {
            Type = "auth",
            Username = "username",
            Password = "password"
        };
        
        Validate(arguments);
    }

    [Test]
    public void InvalidTypeTest() {
        var arguments = new Arguments {
            Type = "type",
            Username = "username",
            Password = "password"
        };

        Assert.Catch<ArgumentOutOfRangeException>(() => Validate(arguments));
    }
    
    [Test]
    public void InvalidUsernameTest() {
        var arguments = new Arguments {
            Type = "type",
            Password = "password"
        };

        Assert.Catch<ArgumentOutOfRangeException>(() => Validate(arguments));
    }
    
    [Test]
    public void InvalidPasswordTest() {
        var arguments = new Arguments {
            Type = "type",
            Username = "username",
            Password = ""
        };

        Assert.Catch<ArgumentOutOfRangeException>(() => Validate(arguments));
    }
    
    private static void Validate(Arguments arguments) => ConsoleArgumentsValidator.Validate(arguments); 
}