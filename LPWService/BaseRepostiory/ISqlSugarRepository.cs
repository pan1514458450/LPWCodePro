using System.Linq.Expressions;

namespace LPWService.BaseRepostiory
{
    public interface ISqlSugarRepository
    {
        Task<int> UpdateAsync<T>(T t, Expression<Func<T, object>> exp) where T : class, new();
        Task<int> UpdateAsync<T>(Dictionary<string, object> t, string where) where T : class, new();
        Task<int> DeleteAsync<T>(int id) where T : class, new();
    }
}
