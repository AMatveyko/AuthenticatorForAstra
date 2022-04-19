using System.Security.Cryptography;
using System.Text;

namespace Common;

public static class SignatureCreator
{
    public static SignatureInfo Create(string value, string secret, string timeStamp) {
        var rawSignature = CreateRawSignature(value, secret, timeStamp);
        var hash = CreateHash(rawSignature);
        return new() {Signature = hash, TimeStamp = timeStamp};
    }

    private static string CreateHash(string input) {
        using MD5 md5 = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);
        return Convert.ToHexString(hashBytes);
    }
    
    private static string CreateRawSignature(string value, string secret, string timeStamp) =>
        $"{secret}{value}{timeStamp}";
}