using Irbis64Plus.DTO;
using Irbis64Plus.Helpers;
using Irbis64Plus.Internal;

namespace Irbis64Plus;

public sealed class IrbisClient
{

    private readonly string _username;
    private readonly string _password;
    private readonly Connection _connection;
    private readonly RequestBuilder _requestBuilder;

    public IrbisClient(string host, string port, string username, string password) =>
        (_username, _password, _connection, _requestBuilder) =
        (username, password, ConnectionBuilder.Create(host, port), new RequestBuilder());

    public Reader GetReaderById(string id) {
        
        Login();
        var searchResult = SearchReader(id);
        
        return GetReader(searchResult);
    }

    private void Login() {
        var request = _requestBuilder.Login(_username, _password);
        SendRequest(request);
    }

    private SearchResult SearchReader(string id) {
        var request = _requestBuilder.SearchReader(id);
        var response = SendRequest(request);
        return new SearchResult(response.Data);
    }

    private Reader GetReader(SearchResult searchResult) {
        var request = _requestBuilder.GetReader(searchResult.Mfn);
        var response = SendRequest(request);
        return new Reader(response.Data);
    }
    
    private Response SendRequest(Request request) {
        var response = _connection.Send(request);
        ResponseHandler.CheckError(response);
        return response;
    }
    
    
}