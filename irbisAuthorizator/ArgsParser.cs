using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace irbisAuthorizator
{
    internal static class ArgsParser
    {
        public static void Parse(string[] args) => WriteArgs(args);

        public static (string,string) GetAccountInfo(string[] args)
        {
            WriteArgs(args);
            return (args.Length > 1) ? (args[0], args[1]) : ("-1", "-1");
        }

        private static void WriteArgs(string[] args)
        {
            var result = string.Join(" ", args);
            File.WriteAllText("/var/log/args", result + "\n");
        }

    }
}
