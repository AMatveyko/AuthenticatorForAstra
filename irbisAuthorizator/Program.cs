// See https://aka.ms/new-console-template for more information
using irbisAuthorizator;


var (userName, password) = ArgsParser.GetAccountInfo(args);
Console.Write(PasswordCheccker.IsValid(userName, password) ? "1" : "0");
