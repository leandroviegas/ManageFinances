using Npgsql;
using System.Data;

namespace ManageFinances.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        public IDbConnection CreateMasterConnection()
            => new NpgsqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
    }
}
