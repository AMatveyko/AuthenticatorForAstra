using Common;
using NUnit.Framework;

namespace ToolTests;

public sealed class SignatureCreatorTests
{
    [Test]
    public void CreateTest() {
        var timestamp = TimeStampHelper.GetTimeStamp();
        var result = SignatureCreator.Create("test", "secret", timestamp);
    }
}