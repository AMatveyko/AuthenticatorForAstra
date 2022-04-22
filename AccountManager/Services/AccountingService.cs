using Grpc.Core;
using UserServiceInterface;

namespace AccountManager.Services;

public sealed class AccountingService : Accounting.AccountingBase
{
    private readonly IAccounting _accounting;

    public AccountingService(IAccounting accounting) => _accounting = accounting;

    public override Task<UserDataAnswerGrpc> GetUserData(UserGrpc request, ServerCallContext context) {
        var result = _accounting.GetUser(request.Name);
        //var result = ResultGetter.Get( () => _accounting.GetUser(request.Name) );
        var userGrpc = result.IsError
            ? new UserDataGrpc {Name = "error", Group = "error", FullName = "error"}
            : new UserDataGrpc {Name = result.Data.Name, Group = result.Data.Group, FullName = result.Data.FullName};
        return Task.FromResult(new UserDataAnswerGrpc { IsError = result.IsError, Message = result.Message, Data = userGrpc});
    }
}