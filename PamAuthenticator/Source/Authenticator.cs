namespace PamAuthenticator;

internal sealed class Authenticator
{

    private readonly string[] _args;

    public Authenticator(string[] args) => _args = args;
    
    public string CheckCredentials() {
        var argument = ConsoleArgumentsParser.Parse(_args);
        ConsoleArgumentsValidator.Validate(argument);

        throw new NotImplementedException();
    }
}