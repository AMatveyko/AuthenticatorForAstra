using Common;
using Common.Debugging;
using Common.DTO;
using Grpc.Core;
using UserServiceInterface;
using UserServiceInterface.DTO;

namespace AccountManager.Services;

public sealed class AccountingService : Accounting.AccountingBase
{
    private readonly IAccounting _accounting;
    private readonly IDebugger _debugger;

    public AccountingService(IAccounting accounting, IDebugger debugger) =>
        (_accounting, _debugger) = (accounting, debugger);

    public override Task<UserDataAnswerGrpc> GetUserData(UserGrpc request, ServerCallContext context) {
        Debug("GetUserData", request.Name);
        var result = _accounting.GetUser(request.Name);
        var userGrpc = GetUserDataGrpc(result);
        return Task.FromResult(new UserDataAnswerGrpc { IsError = result.IsError, Message = result.Message, Data = userGrpc});
    }

    private UserDataGrpc GetUserDataGrpc(Result<UserData> result) {
        Debug("Answer", $"IsError: {result.IsError}", $"Message: {result.Message}");
        return result.IsError
            ? new UserDataGrpc {Name = "error", Group = "error", FullName = "error"}
            : new UserDataGrpc {Name = result.Data.Name, Group = result.Data.Group, FullName = result.Data.FullName};
    }
    
    private void Debug(string initiator, params string[] args) => _debugger.Write(initiator, args);
}