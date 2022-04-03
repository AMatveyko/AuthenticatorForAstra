using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace irbisAuthorizator
{
    internal static class PasswordCheccker
    {
        public static bool IsValid(string userName, string password) => userName == password;
    }
}
