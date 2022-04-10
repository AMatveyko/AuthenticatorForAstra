using Common;
using NUnit.Framework;

namespace ToolTests;

public sealed class SignatureCreatorTests
{
    [Test]
    public void CreateTest() {
        var result = SignatureCreator.Create("test", "secret");
    }
}