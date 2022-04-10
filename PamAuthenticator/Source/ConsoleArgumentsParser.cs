using PamAuthenticator.DTO;

namespace PamAuthenticator
{
    internal static class ConsoleArgumentsParser
    {
        public static Arguments Parse(string[] args) {
            if (args.Length < 3) {
                throw new ArgumentOutOfRangeException("Console arguments are less than 3");
            }

            return new() {
                Type = args[0],
                Username = args[1],
                Password = args[2]
            };
        }
    }
}
