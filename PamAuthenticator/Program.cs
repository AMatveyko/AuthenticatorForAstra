// See https://aka.ms/new-console-template for more information

using Common;
using PamAuthenticator;
using PamAuthenticator.Helpers;


var debugger = DebuggersBuilder.Create(Configuration.Debug());

var result = ExceptionsLogger.Log( () => Do(debugger), debugger);

Environment.Exit( result == MyConstants.Success ? 0 : 1 );

string Do(IDebugger debugger) {
    var credentials = CredentialsCreator.Create(Configuration.SignatureSecret(), debugger);
    var timeOut = Configuration.TimeOut();
    using var worker = WorkerBuilder.Create(credentials, timeOut, debugger);

    return worker.GetResult();
}
