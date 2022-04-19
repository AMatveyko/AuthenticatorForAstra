// See https://aka.ms/new-console-template for more information

using AccountManagerClient;
using Common;
using PamAuthenticator;

var debugger = GetDebugger(args);
debugger.Write(nameof(Program), args);

var url = Configuration.GetServiceUrl();
var client = new ManagerClient(url);
var worker = new Worker(args, client);
ExceptionsLogger.Log(worker.Run);




IDebugger GetDebugger(params string[] args) =>
    args.Length == 5 && args[4] == "debug"
        ? new Debugger(Configuration.GetDebugFilePath())
        : new EmptyDebugger();
