using LPWService.BaseRepostiory;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LPWService.Desgin
{
    public sealed class RedisOrDb : IRedisOrDb
    {
        private readonly ISqlSugarRepository _context;
        private readonly ICsredisHelp _redis;
        public async Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>> exp = null, string key = null)
        {
            List<T> list = new List<T>();
            if(await _redis.CheckKey(key))
            {
               list=  await _redis.GetRedis<List<T>>(key);
                if(list.Count>0)
              return list;
            }
                list = await _context.GetAllAsync<T>(exp);
                await _redis.SetRedis(key, list, ConstCode.CsRedisTime);
                return list;
        }

        public async Task<T> GetFirstAsync<T>(Expression<Func<T, bool>> exp = null, string key = null)where T:class,new ()
        {
            T t = new T();
            if (await _redis.CheckKey(key))
            {
                t = await _redis.GetRedis<T>(key);
                if (t!=null)
                    return t;
            }
            t = await _context.GetFirstAsync<T>(exp);
            await _redis.SetRedis(key, t, ConstCode.CsRedisTime);
            return t;
        }
    }
}
