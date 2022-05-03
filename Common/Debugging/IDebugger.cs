namespace Common.Debugging;

public interface IDebugger
{
    public void Write(string initiator, params string[] messages);
}