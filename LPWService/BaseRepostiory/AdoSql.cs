using Dapper;
using LPWService.StaticFile;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Transactions;

namespace LPWService.BaseRepostiory
{
    public sealed class AdoSql:IAdoSql
    {


        public async Task<List<T>> GetAllAsync<T>(string sql,List<SqlParameter> para)
        {
            using (var con=Con())
            {
                using (var cmd=new SqlCommand(sql,con))
                {
                    if (para != null)
                        cmd.Parameters.AddRange(para.ToArray());
                    using (var read =await cmd.ExecuteReaderAsync())
                    {
                        List<T> list = new List<T>();
                        string key = typeof(T).FullName;
                        while (await read.ReadAsync())
                        {
                           list.Add(ExpTreeMethods<T>.GetDr(read, key));
                        }
                        return list;
                    }
                }
            }
        }
        public async Task<T> GetFirstAsync<T>(string sql, List<SqlParameter> para)
        {
            using (var con = Con())
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    if (para != null)
                        cmd.Parameters.AddRange(para.ToArray());
                    using (var read = await cmd.ExecuteReaderAsync())
                    {
                    
                        string key = typeof(T).FullName;
                        if (await read.ReadAsync())
                        {
                            return ExpTreeMethods<T>.GetDr(read, key);
                        }
                        return Activator.CreateInstance<T>();
                    }
                }
            }
        }
       



        private SqlConnection Con()
        {
            var con= new SqlConnection(ConstCode.SqlServer);
            con.Open();
            return con;
        }

        public async Task<List<T>> GetALL<T>(string sql, object args)
        {
            var con = new SqlConnection(ConstCode.SqlServer);
            try
            {
                con.Open();
                return (await con.QueryAsync<T>(sql, args)).ToList();
            }
            catch (Exception)
            {
                return default(List<T>);
            }
            finally
            {
                con.Close();
            }
          
        }

        public async Task<bool> WirteOrUpdate(string sql,  List<SqlParameter> args)
        {
            using (var con=Con())
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    if (args != null)
                        cmd.Parameters.AddRange(args.ToArray());
                    return await cmd.ExecuteNonQueryAsync() > 0 ? true : false;

                }
                
            }
        }

        public async Task<bool> TranWirteOrUpdate(string sql,  List<SqlParameter> args = null)
        {
            using (var con=Con())
            {
                ///脏读
                var tran= con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                try
                {
                    using (var cmd=new SqlCommand(sql,con))
                    {
                        cmd.Parameters.AddRange(args.ToArray());
                         await cmd.ExecuteNonQueryAsync();
                        tran.Commit();
                        return true;
                    }
                }
                catch (Exception)
                {
                    tran.Rollback();
                    return false;
                }

            }
        }
    }
}
