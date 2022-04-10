using PamAuthenticator.DTO;
using UserServiceInterface;

namespace PamAuthenticator.SpecifiedWorkers;

internal sealed class Accounting : ISpecificWorker
{

    private readonly Arguments _arguments;
    private readonly IAccounting _accounting;

    public Accounting(Arguments arguments, IAccounting accounting) =>
        (_arguments, _accounting) = (arguments, accounting);
    
    public string Do() {
        throw new NotImplementedException();
    }
}