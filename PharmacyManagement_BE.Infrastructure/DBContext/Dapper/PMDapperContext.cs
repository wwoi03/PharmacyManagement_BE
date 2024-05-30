using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.DBContext.Dapper
{
    public class PMDapperContext : IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _context;

        public PMDapperContext(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._context = new SqlConnection(configuration.GetConnectionString("ConnectionStringOnline"));
        }

        public IDbConnection GetConnection => this._context;

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
