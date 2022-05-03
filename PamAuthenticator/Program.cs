// See https://aka.ms/new-console-template for more information

using Common.Debugging;
using PamAuthenticator;
using PamAuthenticator.Helpers;

var settings = new Configuration().GetSettings();

var debugger = DebuggersBuilder.Create(settings.Debug, Loggers.Debug().Debug);

var result = ExceptionsLogger.Log( Do, debugger);

Environment.Exit( result == MyConstants.Success ? 0 : 1 );

string Do() {
    var credentials = CredentialsCreator.Create(settings.SignatureSecret);
    using var worker = WorkerBuilder.Create(credentials, settings, debugger);

    return worker.GetResult();
}
