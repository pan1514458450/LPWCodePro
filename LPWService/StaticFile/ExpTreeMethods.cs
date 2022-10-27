using Microsoft.Data.SqlClient;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LPWService.StaticFile
{
    internal static class ExpTreeMethods<T>
    {
        private static ConcurrentDictionary<string, Func<SqlDataReader, T>> dicrd;

        static ExpTreeMethods()
        {
            dicrd = new ConcurrentDictionary<string, Func<SqlDataReader, T>>();
        }
       internal static T GetDr(SqlDataReader dr,string key)
        {
            if (!dicrd.ContainsKey(key))
            {
                ExpTree(dr);
            }
            return dicrd[key].Invoke(dr);
        }
        
        private static void ExpTree(SqlDataReader reader)
        {
            var paramsname = Expression.Parameter(typeof(SqlDataReader), "x");
            List<MemberBinding> bindings = new List<MemberBinding>();
            var type=typeof(T).GetProperties().Where(d=>d.CanWrite&&d.PropertyType.IsPublic).ToList();

            foreach (var item in type)
            {
                var value = reader[item.Name];

               
                    if (value is DBNull) continue;
                    var cons = Expression.Constant(value );
                    bindings.Add(Expression.Bind(item, cons));
              
            }
            var newtype=Expression.MemberInit(Expression.New(typeof(T)),bindings);
            dicrd.GetOrAdd(typeof(T).FullName, Expression.Lambda<Func<SqlDataReader, T>>(newtype, paramsname).Compile());
        }
    }
}
