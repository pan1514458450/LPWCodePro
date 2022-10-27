using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LPWService.Desgin
{
    public interface IRedisOrDb
    {
        Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>> exp =null,string key=null);
        Task<T> GetFirstAsync<T>(Expression<Func<T, bool>> exp = null, string key = null) where T : class, new();

        //Task<bool> WriteOrUpdate(Func<int,Task<int>> func,string key);
    }
}
