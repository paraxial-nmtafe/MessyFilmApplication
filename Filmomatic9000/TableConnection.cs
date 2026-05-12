using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filmomatic9000
{
    internal class TableConnection
    {
        protected MySqlConnection connection;
        private string server = "127.0.0.1";
        private string port = "3306";
        private string username = "root";  // Create a reasonable and limited application user for your app
        private string password = "";
        private string database = "sakila_tafe";
        public TableConnection() {
            string connectionString = $"server={server};port={port};uid={username};pwd={password};database={database};";
            this.connection = new MySqlConnection(connectionString);
        }
    }
}
