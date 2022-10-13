using Model.UserModel;
using SqlSugar;
using System.Linq.Expressions;

namespace LPWService.BaseRepostiory
{
    public sealed class SqlSugarRepository : ISqlSugarRepository
    {
        private readonly ISqlSugarClient _client;
        public SqlSugarRepository(ISqlSugarClient client)
        {
            _client = client;
        }

        public async Task<int> DeleteAsync<T>(int id) where T : class, new()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("IsDelete", 1);
            data.Add("Id", id);
            data.Add("UpdateTime", DateTime.Now);
            return await _client.Updateable<T>(data).WhereColumns("Id").ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync<T>(T t, Expression<Func<T, object>> exp) where T : class, new()
        {
            return await _client.Updateable(t).UpdateColumns(exp).ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync<T>(Dictionary<string, object> t, string where) where T : class, new()
        {
            return await _client.Updateable<SysAdminUsers>(t).WhereColumns(where).ExecuteCommandAsync();
        }
    }
}
