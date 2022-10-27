using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPWService.BaseRepostiory
{
    public interface IAdoSql
    {
        Task<List<T>> GetAllAsync<T>(string sql,  List<SqlParameter> para=null);
        Task<T> GetFirstAsync<T>(string sql,  List<SqlParameter> para=null);
        Task<bool> WirteOrUpdate(string sql, List<SqlParameter> args =null);
        Task<bool> TranWirteOrUpdate(string sql, List<SqlParameter> args =null);
    }
}
