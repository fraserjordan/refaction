using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helpers
{
    public class TestDatabaseHelper
    {
        private static readonly string DatabaseDirectory = System.IO.Directory.GetCurrentDirectory() + "\\Database\\Test.mdf";
        private static readonly string ConnectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={DatabaseDirectory};Integrated Security=True";

        public SqlConnection NewTestConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
