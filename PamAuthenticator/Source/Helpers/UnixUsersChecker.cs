namespace PamAuthenticator.Helpers;

internal static class UnixUsersChecker
{
    private const string PasswdPath = "/etc/passwd";
    private const string GroupPath = "/etc/group";

    public static bool IsUserExist(string username) => IsNameExist(username, PasswdPath);

    public static bool IsGroupExist(string groupName) => IsNameExist(groupName, GroupPath);

    private static bool IsNameExist(string name, string fileName) {
        var namesFromFiles = ReadFile(fileName).Select(e => e.Split(":")[0]);
        return namesFromFiles.FirstOrDefault(n => n == name) != null;
    }
    
    private static IEnumerable<string> ReadFile(string filePath) {

        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        using var reader = new StreamReader(fileStream);

        string? line;
        while ( (line = reader.ReadLine() ) != null ) {
            yield return line;
        }
    }
}