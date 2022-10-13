using System.Linq.Expressions;

namespace LPWService.BaseRepostiory
{
    public interface IUnitWorkRepository
    {
        #region Read 
        Task<T> GetIdAsync<T>(int id) where T : class;
        Task<T> GetFirstAsync<T>(Expression<Func<T, bool>> exp = null) where T : class;
        IQueryable<T> Get<T>() where T : class;
        Task<List<T>> GetListAsync<T>() where T : class;
        Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> exp = null) where T : class;
        Task<List<F>> GetJoin<Q, T, C, F>(Expression<Func<Q, C>> firstexp, Expression<Func<T, C>> twoexp, Expression<Func<Q, T, F>> exp) where T : class where Q : class;
        IQueryable<F> GetIQueryJoin<Q, T, C, F>(Expression<Func<Q, C>> firstexp, Expression<Func<T, C>> twoexp, Expression<Func<Q, T, F>> exp) where T : class where Q : class;
        #endregion
        #region CU
        Task<int> AddAsync<T>(T entity);
        Task<int> UpdateAsync<T>(T entity);
        #endregion
        void RollbackTransaction();
        void CommitTransaction();
        void BeginTransaction();

    }
}
