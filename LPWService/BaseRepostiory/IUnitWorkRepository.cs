using SqlSugar;
using System.Linq.Expressions;

namespace LPWService.BaseRepostiory
{
    public interface IUnitWorkRepository
    {
        #region Read 
        Task<T> GetFirstAsync<T>(Expression<Func<T, bool>> exp ) where T : class;
        ISugarQueryable<T> JoinTableAsync<T, Q, F>(Expression<Func<T, Q, bool>> exp, Expression<Func<T, Q, F, bool>> exp1) where T : class;
        ISugarQueryable<T> Get<T>() where T : class;
        Task<List<T>> GetListAsync<T>() where T : class;
        Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> exp = null) where T : class;
         #endregion
        #region CU
        Task<int> AddAsync<T>(T entity)where T:class,new();
        Task<int> UpdateAsync<T>(T entity)where T:class,new();
        #endregion
      

    }
}
