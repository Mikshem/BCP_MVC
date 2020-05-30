using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OPERACION.INFRASTRUCTURE.Repositories
{
    public class StoreProcedureRepository
    {
        private readonly string _configuration;

        public StoreProcedureRepository(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("BCP");
        }

        public async Task AbonarSaldo(decimal? importe, string nro_cuenta)
        {
            using(SqlConnection sql = new SqlConnection(_configuration))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarSaldo", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add( new SqlParameter("@NRO_CUENTA", nro_cuenta));
                    cmd.Parameters.Add(new SqlParameter("@IMPORTE", importe));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task DebitarSaldo(decimal? importe, string nro_cuenta)
        {
            using (SqlConnection sql = new SqlConnection(_configuration))
            {
                using (SqlCommand cmd = new SqlCommand("sp_RestarSaldo", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NRO_CUENTA", nro_cuenta));
                    cmd.Parameters.Add(new SqlParameter("@IMPORTE", importe));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
