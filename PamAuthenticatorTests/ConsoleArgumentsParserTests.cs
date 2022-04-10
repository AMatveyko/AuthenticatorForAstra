using NUnit.Framework;
using PamAuthenticator;
using PamAuthenticator.DTO;

namespace PamAuthenticatorTests;

public sealed class ConsoleArgumentsParserTests
{
    [Test]
    public void ParsingValidArgumentsTest() {
        var consoleArguments = new [] {
            "auth",
            "administrator",
            "administrator"
        };

        var expected = new Arguments {
            Type = "auth",
            Username = "administrator",
            Password = "administrator"
        };
        
        var actual = ConsoleArgumentsParser.Parse(consoleArguments);

        Assert.AreEqual(expected, actual);

    }

    [Test]
    public void SmallerNumberOfArguments() {
        var consoleArguments = new [] {
            "auth",
            "administrator"
        };

        Assert.Catch<ArgumentOutOfRangeException>(() => ConsoleArgumentsParser.Parse(consoleArguments));

    }
}