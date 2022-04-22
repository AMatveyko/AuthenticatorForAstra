namespace Irbis64Plus.Helpers;

internal static class SequenceNumberGetter
{
    private static int _number = 38000000;
    private static object _locker = new();

    public static int Get() {
        lock (_locker) {
            return _number++;
        }
    }
}