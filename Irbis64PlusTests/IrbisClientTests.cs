using Irbis64Plus;
using NUnit.Framework;

namespace Irbis64PlusTests;

public sealed class IrbisClientTests
{
    [Test]
    public void GetReaderTest() {
        var client = new IrbisClient("10.0.0.157", "6666", "1", "1");
        var reader = client.GetReaderById("5568");
    }
}