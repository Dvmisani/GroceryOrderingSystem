using System;
using MySql.Data.MySqlClient;

namespace GroceryOrderingSystem
{
    public static class DatabaseManager
    {
        // Password and Port successfully integrated!
        private static readonly string ConnectionString = "Server=localhost;Port=3306;Database=GroceryOrderingDB;Uid=root;Pwd=Bumbu2354#;Convert Zero Datetime=True;";

        // Exposes the connection details to BackupManager so that the database
        // credentials are defined in exactly one place.
        public static string GetConnectionString()
        {
            return ConnectionString;
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static DateTime CalculateTargetFriday(DateTime currentDateTime)
        {
            DateTime baseFriday = currentDateTime;
            while (baseFriday.DayOfWeek != DayOfWeek.Friday)
            {
                baseFriday = baseFriday.AddDays(1);
            }

            DateTime cutOffDeadline = baseFriday.AddDays(-1).Date.AddHours(23).AddMinutes(59).AddSeconds(59);

            if (currentDateTime > cutOffDeadline)
            {
                return baseFriday.AddDays(7).Date;
            }

            return baseFriday.Date;
        }
    }
}