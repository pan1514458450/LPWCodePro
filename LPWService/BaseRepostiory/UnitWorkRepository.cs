using LPWService.StaticFile;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq.Expressions;

namespace LPWService.BaseRepostiory
{
    public sealed class UnitWorkRepository : IUnitWorkRepository
    {
        private readonly DbContextModule db;

        public UnitWorkRepository(DbContextModule db)
        {
            this.db = db;
        }
        #region 查询
        public async Task<T> GetFirstAsync<T>(Expression<Func<T, bool>> exp = null) where T : class
            => await db.Set<T>().WhereIf(exp != null, exp).FirstOrDefaultAsync();
        public IQueryable<T> Get<T>() where T : class => db.Set<T>().AsNoTracking();
        public async Task<T> GetIdAsync<T>(int id) where T : class => await db.FindAsync<T>(id);
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="Q">第一个泛型</typeparam>
        /// <typeparam name="T">第二个泛型</typeparam>
        /// <typeparam name="C">连接类型</typeparam>
        /// <typeparam name="F">返回泛型</typeparam>
        /// <param name="exp">最后面的Select</param>
        /// <param name="firstexp"></param>
        /// <param name="twoexp"></param>
        /// <returns></returns>
        public async Task<List<F>> GetJoin<Q, T, C, F>(Expression<Func<Q, C>> firstexp, Expression<Func<T, C>> twoexp, Expression<Func<Q, T, F>> exp) where T : class where Q : class
        {
            return await db.Set<Q>().Join(db.Set<T>(), firstexp, twoexp, exp).ToListAsync();
        }
        public IQueryable<F> GetIQueryJoin<Q, T, C, F>(Expression<Func<Q, C>> firstexp, Expression<Func<T, C>> twoexp, Expression<Func<Q, T, F>> exp) where T : class where Q : class
        {

            return db.Set<Q>().Join(db.Set<T>(), firstexp, twoexp, exp);
        }

        public async Task<List<T>> GetListAsync<T>() where T : class
            => await db.Set<T>().ToListAsync();
        public async Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> exp = null) where T : class
            => await db.Set<T>().WhereIf(exp != null, exp).ToListAsync();
        #endregion
        #region CU
        public async Task<int> AddAsync<T>(T entity)
        {
            await db.AddAsync(entity);
            return await db.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync<T>(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return await db.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            db.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            db.Database.CommitTransaction();
        }
        public void RollbackTransaction()
        {
            db.Database.RollbackTransaction();
        }
        #endregion
    }
}
