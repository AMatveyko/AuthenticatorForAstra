namespace Common;

public sealed class EmptyDebugger : IDebugger
{
    public void Write(string initiator, params string[] messages) {}
}