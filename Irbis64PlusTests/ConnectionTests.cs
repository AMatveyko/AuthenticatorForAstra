using Irbis64Plus.DTO;
using Irbis64Plus.Helpers;
using Irbis64Plus.Internal;
using NUnit.Framework;

namespace Irbis64PlusTests;

public sealed class ConnectionTests
{
    [Test]
    public void IrbisConnectTest() {
        var host = "10.0.0.157";
        var port = 6666;
        var connection = new Connection(host, port);
        var requestBuilder = new RequestBuilder();
        var loginRequest = requestBuilder.Login("1", "1");
        var loginResponse = connection.Send(loginRequest);
        // var searchRequest = requestBuilder.GetReader("005568");
        var searchRequest = requestBuilder.SearchReader("5568");
        var searchResponse = connection.Send(searchRequest);
        var searchResult = new SearchResult(searchResponse.Data);
        var getReaderRequest = requestBuilder.GetReader(searchResult.Mfn);
        var readerResponse = connection.Send(getReaderRequest);
        var reader = new Reader(readerResponse.Data);
    }
}