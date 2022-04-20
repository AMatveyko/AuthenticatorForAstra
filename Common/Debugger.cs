using System.Text;

namespace Common;

public sealed class Debugger : IDebugger
{

    private readonly string _debugFile;

    public Debugger(string debugFile) {
        _debugFile = debugFile;
        Write(nameof(Debugger),"Start");
    }
    
    public void Write(string initiator, params string[] messages) {
        var debugInfo = new StringBuilder($"{DateTime.Now} {initiator}:");
        debugInfo.AppendLine( $" {string.Join("|", messages)}" );
        File.AppendAllText(_debugFile, debugInfo.ToString());
    }
}