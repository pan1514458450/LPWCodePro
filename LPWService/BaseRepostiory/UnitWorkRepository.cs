using LPWService.StaticFile;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.UserModel;
using SqlSugar;
using System.Linq.Expressions;

namespace LPWService.BaseRepostiory
{
    public sealed class UnitWorkRepository : IUnitWorkRepository
    {
        private readonly ISqlSugarClient _db;

        public UnitWorkRepository(ISqlSugarClient db)
        {
            this._db = db;
        }
        #region 查询
        public ISugarQueryable<T> JoinTableAsync<T,Q,F>(Expression<Func<T,Q,bool>> exp, Expression<Func<T, Q,F, bool>> exp1) where T:class
        {
           return _db.Queryable<T>().InnerJoin<Q>(exp).InnerJoin<F>(exp1);
        }

        public async Task<T> GetFirstAsync<T>(Expression<Func<T, bool>> exp ) where T : class
            => await Get<T>().FirstAsync( exp);
        public ISugarQueryable<T> Get<T>() where T : class => _db.Queryable<T>();
   
        public async Task<List<T>> GetListAsync<T>() where T : class
            => await Get<T>().ToListAsync();
        public async Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> exp = null) where T : class
            => await Get<T>().WhereIF(exp != null, exp).ToListAsync();
        #endregion
        #region CU
        public async Task<int> AddAsync<T>(T entity)where T:class,new()=>await _db.Insertable<T>(entity).ExecuteCommandAsync();

        public async Task<int> UpdateAsync<T>(T entity) where T : class, new()=> await _db.Updateable<T>(entity).ExecuteCommandAsync();
        

      
        #endregion
    }
}
