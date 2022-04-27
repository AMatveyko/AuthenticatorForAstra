using Common;
using NUnit.Framework;

namespace ToolTests;

public sealed class SignatureCreatorTests
{
    [Test]
    public void CreateTest() {
        var timestamp = TimeStampHelper.GetTimeStamp();
        var signature1 = SignatureCreator.Create("123", "123123123", "20220426193540");
        // var signature2 = SignatureCreator.Create(Console.ReadLine(), "123123123", "20220426154956");
        foreach (var signature in new List<SignatureInfo> { signature1 }) {
            Console.WriteLine($"Signature: {signature.Signature}, Timestamp: {signature.TimeStamp}");
        }
    }
}