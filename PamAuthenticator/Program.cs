// See https://aka.ms/new-console-template for more information

using PamAuthenticator;

var authenticator = new Authenticator(args);
var result = authenticator.CheckCredentials();

Console.Write(result);
