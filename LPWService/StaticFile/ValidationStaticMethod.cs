using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Text;

namespace LPWService.StaticFile
{
    internal static class ValidationStaticMethod
    {
        static Dictionary<string, Func<object, bool>> dic;
        static ValidationStaticMethod()
        {
            dic = new Dictionary<string, Func<object, bool>>();
        }
        
        internal static bool CheckSgReflection(this object obj)
        {
            string sg = string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (var item in obj.GetType().GetProperties())
            {
                if (item.Name == "Sg")
                {
                    sg = item.GetValue(obj).ToString();
                    continue;
                }
                sb.Append(item.GetValue(obj));
            }
            return sg == sb.ToString().Md5Encrypto();
        }
        internal static bool CheckSgSerialize(this object obj)
        {

            var sgmd5 = obj.ToJson().JsonTo<Dictionary<string, string>>();
            StringBuilder sb = new StringBuilder();
            string sg = sgmd5["Sg"];
            sgmd5.Remove("Sg");
            foreach (var item in sgmd5)
            {
                sb.Append(item.Value);
            }
            return sg == sgmd5.ToString().Md5Encrypto();
        }
        internal static bool ChekSgExper(this object? obj)
        {
            var type = obj.GetType();
            if (!dic.ContainsKey(type.FullName))
            {
                var para = Expression.Parameter(type, "x");
                Expression.Parameter(type);
                var inttype = typeof(int);
                Expression exp = null;
                foreach (var item in type.GetProperties())
                {
                    if (item.Name == "Sg")
                    {
                        exp = Md5Entry(exp);
                        exp = Expression.Equal(exp, Expression.Property(para, item.Name));
                    }
                    else
                    {
                        if (item.PropertyType == inttype)
                        {
                            var expcall = Expression.Call(typeof(Convert).GetMethod("ToString", new Type[] { inttype }), Expression.Property(para, item.Name));
                            if (exp == null)
                                exp = expcall;
                            else
                                exp = ToStringAddString(exp, expcall);
                        }
                        else
                        {
                            if (exp == null)
                                exp = Expression.Property(para, item.Name);
                            else
                                exp = ToStringAddString(exp, Expression.Property(para, item.Name));
                        }
                    }
                }
                var c = Expression.Lambda<Func<object, bool>>(exp, para).Compile();
                dic.Add(type.FullName, c);
            }
            return dic[type.FullName](obj);
        }
        private static BinaryExpression ToStringAddString(Expression exp, Expression expcall)
        {
            var concatMethod = typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });
            return Expression.Add(exp, expcall, concatMethod);
        }
        private static Expression Md5Entry(Expression exp)
        {
            var concatMethod = typeof(string).GetMethod("Concat", new[] { typeof(string) });
            return Expression.Call(concatMethod, exp);
        }
    }

}
