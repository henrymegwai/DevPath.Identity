using BlinkCash.Data.DapperConnection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetDbConnection()
        {
            string _connectionString = _configuration.GetConnectionString("BlinkCashDbContext");
            return new SqlConnection(_connectionString);
        }
    }
}
