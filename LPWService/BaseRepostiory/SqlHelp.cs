using Dapper;
using Microsoft.Data.SqlClient;

namespace LPWService.BaseRepostiory
{
    public sealed class SqlHelp:ISqlHelp
    {

        public  async Task<T> Get<T>(string sql, params object[] args)
        {
            var con = new SqlConnection(ConstCode.SqlServer);
            try
            {
                con.Open();
                return (await con.QueryFirstAsync<T>(sql, args));
            }
            catch (Exception)
            {
                return default(T);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
