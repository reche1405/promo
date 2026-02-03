using System.Runtime.CompilerServices;
using Microsoft.Extensions.Options;

namespace RecheApi.Nifty.Database
{
    public class Config
    {
        private static string _connectionString = "Data Source = reche.db";
        public static string ConnectionString
        {
            get
            {
                //var envVal = Environment.GetEnvironmentVariable("DB_CONNECTION");
                var envVal = "";
                return !string.IsNullOrEmpty(envVal) ? envVal : _connectionString;

            }
            set =>_connectionString = value;
            
        } 
    }
}