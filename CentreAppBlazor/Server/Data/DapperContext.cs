using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CentreAppBlazor.Server.Data
{
    public class DapperContext
    {
        public static string ConStr;
        public DapperContext()
        {

        }
        public async Task<int> ExecuteAsync(string sql, object parameters = null, CommandType? CommandType = null)
        {
            using (IDbConnection db = new SqlConnection(ConStr))
            {
                return await db.ExecuteAsync(sql, parameters, commandType: CommandType);
            }
        }

        public async Task<List<T>> QueryAsync<T>(string sql, object parameters = null, CommandType? CommandType = null)
        {
            using (IDbConnection db = new SqlConnection(ConStr))
            {
                return (await db.QueryAsync<T>(sql, parameters, commandType: CommandType)).ToList();
            }
        }
    }
}
