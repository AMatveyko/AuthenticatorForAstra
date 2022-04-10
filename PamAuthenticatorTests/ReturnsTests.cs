using NUnit.Framework;
using PamAuthenticator;

namespace PamAuthenticatorTests;

public sealed class ReturnsTests
{
    [Test]
    public void ReturnSuccessTest() {
        Assert.AreEqual(MyConstants.Success, "success");
    }

    [Test]
    public void ReturnDenyTest() {
        Assert.AreEqual(MyConstants.Deny, "deny");
    }
}