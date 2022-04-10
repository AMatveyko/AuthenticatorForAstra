using NUnit.Framework;
using PamAuthenticator;
using PamAuthenticator.DTO;
using PamAuthenticator.SpecifiedWorkers;
using PamAuthenticatorTests.Fake;

namespace PamAuthenticatorTests;

public sealed class WorkerSelectorTests
{
    [Test]
    public void SelectAuthenticatorTest() {
        var arguments = new Arguments {
            AuthenticationType = "authentication"
        };

        var actual = WorkerSelector.CreateWorker(arguments, new FakeUserAuthenticator(""), "");
        
        Assert.IsInstanceOf<Authenticator>(actual);
    }
    
    [Test]
    public void SelectAccountingTest() {
        var arguments = new Arguments {
            AuthenticationType = "accounting"
        };

        var actual = WorkerSelector.CreateWorker(arguments, new FakeUserAuthenticator(""), "");
        
        Assert.IsInstanceOf<Accounting>(actual);
    }
    
    [Test]
    public void UnsupportedTypeTest() {
        var arguments = new Arguments {
            AuthenticationType = "unsupportedType"
        };

        Assert.Catch<ArgumentOutOfRangeException>(() => WorkerSelector.CreateWorker(arguments, new FakeUserAuthenticator(""), ""));

    }
}