using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMart.Infastructure.Model
{
    public static class AppConfig
    {
        public static string GetVariable(string variableName) =>
            Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.Process);

        public static string ShoppingMartDbConnection => GetVariable(nameof(ShoppingMartDbConnection));

        public static string Environ = GetVariable("AppEnviron") ??  "dev";

        public static bool IsDev => Environ == "dev";
    }
}
