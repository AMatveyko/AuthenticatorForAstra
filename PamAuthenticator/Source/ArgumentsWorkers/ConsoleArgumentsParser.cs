namespace PamAuthenticator.ArgumentsWorkers
{
    internal static class ConsoleArgumentsParser
    {

        private const int ArgumentsCount = 4;
        
        public static DTO.Arguments Parse(string[] args) =>
            args.Length < ArgumentsCount
                ? throw new ArgumentOutOfRangeException($"Console arguments are less than {ArgumentsCount}")
                : new() {
                        AuthenticationType = args[0],
                        PamType = args[1],
                        Username = args[2],
                        Password = args[3]
                    };
    }
}
