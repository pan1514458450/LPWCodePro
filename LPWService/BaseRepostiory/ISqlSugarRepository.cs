using System.Linq.Expressions;

namespace LPWService.BaseRepostiory
{
    public interface ISqlSugarRepository
    {
        Task<int> UpdateAsync<T>(T t, Expression<Func<T, object>> exp) where T : class, new();
        Task<int> UpdateAsync<T>(T t) where T : class, new();
        Task<int> UpdateAsync<T>(Dictionary<string, object> t, string where) where T : class, new();
        Task<int> DeleteAsync<T>(int id) where T : class, new();


        #region Create
        Task<int> AddAsync<T>(T t) where T : class, new();
        #endregion


        #region Read
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>> exp = null);
        Task<List<T>> GetAllPageAsync<T>(Expression<Func<T, bool>> exp = null,int size=20,int page=1);
        Task<T> GetFirstAsync<T>(Expression<Func<T, bool>> exp = null);
        #endregion
    }
}
