using NUnit.Framework;
using PamAuthenticator.ArgumentsWorkers;
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
            PamType = "auth",
            Username = "administrator",
            Password = "administrator"
        };
        
        var actual = ArgumentsParser.Parse();

        Assert.AreEqual(expected, actual);

    }

    [Test]
    public void SmallerNumberOfArguments() {
        var consoleArguments = new [] {
            "auth",
            "administrator"
        };

        Assert.Catch<ArgumentOutOfRangeException>(() => ArgumentsParser.Parse());

    }
}