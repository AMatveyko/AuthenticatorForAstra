// See https://aka.ms/new-console-template for more information

using AccountManagerClient;
using Common;
using PamAuthenticator;
using PamAuthenticator.ArgumentsWorkers;
using PamAuthenticator.Helpers;
using UserServiceInterface.DTO;

var debugger = GetDebugger(args);

ExceptionsLogger.Log(() => Start(args, debugger), debugger);


void Start(string[] args, IDebugger debugger) {

    var credentials = CreateCredentials(args);
    
    

    var client = new ManagerClient(Configuration.ServiceUrl());
    var groupCreator = new GroupsCreator(Configuration.GroupsToolPath(), debugger);
    var userCreator = new UserCreator(Configuration.UsersToolPath(), debugger);
    var worker = new AccountWorker(credentials, client, groupCreator, userCreator, debugger);
    worker.Do();
    
}

Credentials CreateCredentials(string[] args) {
    var argument = ConsoleArgumentsParser.Parse(args);
    ConsoleArgumentsValidator.Validate(argument);
    var helper = new CredentialsCreator(argument, Configuration.SignatureSecret());
    return helper.Crete();
}

IDebugger GetDebugger(params string[] args) =>
    args.Length == 5 && args[4] == "debug"
        ? new Debugger(s => Loggers.Debug().Debug(s) )
        : new EmptyDebugger();
