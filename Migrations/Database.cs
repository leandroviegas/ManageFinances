using Dapper;
using ManageFinances.Context;

namespace ManageFinances.Migrations
{
    public class Database
    {
        private readonly DapperContext _context;
        public Database(DapperContext context)
        {
            _context = context;
        }
        public void CreateDatabase(string dbName)
        {
            /*var query = $"SELECT 'CREATE DATABASE {dbName}' WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = {dbName})";
            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);
            using (var connection = _context.CreateMasterConnection())
            {
                var records = connection.Query(query, parameters);
            }*/
        }
    }
}
