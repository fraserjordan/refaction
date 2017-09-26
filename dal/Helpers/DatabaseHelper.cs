using System.Configuration;
using System.Data.SqlClient;

namespace Repository.Helpers
{
    public class DatabaseHelper
    {
        private readonly string _defaultConnectionString = ConfigurationManager.AppSettings["defaultConnectionString"];
        public SqlConnection NewConnection()
        {
            return new SqlConnection(_defaultConnectionString);

        }
    }
}
