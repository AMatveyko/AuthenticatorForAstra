namespace Common;

public interface IDebugger
{
    public void Write(string initiator, params string[] messages);
}