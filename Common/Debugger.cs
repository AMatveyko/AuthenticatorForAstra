using System.Text;

namespace Common;

public sealed class Debugger : IDebugger
{

    private readonly Action<string> _write;

    public Debugger(Action<string> write) {
        _write = write;
        Write(nameof(Debugger),"Start");
    }
    
    public void Write(string initiator, params string[] messages) {
        var debugInfo = new StringBuilder($"{DateTime.Now} {initiator}:");
        debugInfo.Append( $" {string.Join("|", messages)}" );
        
        _write(debugInfo.ToString());
    }
}