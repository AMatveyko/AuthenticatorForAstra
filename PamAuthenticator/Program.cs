// See https://aka.ms/new-console-template for more information

using PamAuthenticator;

var worker = new Worker(args, null);
ExceptionsLogger.Log(worker.Run);
