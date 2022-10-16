using ExpCode.StaticFiled;
using System.Linq.Expressions;
using System.Data.Common;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;

namespace ExpCode.SqlHelp
{
    internal static class ExtensionMethods<T>
    {
        private static Dictionary<Type, Func<SqlDataReader,T>> dicexp;
        private static Dictionary<Type, Func<DataRow,T>> dicrowexp;
        private static Func<DataRow, T> funcc { get; set; }

        static ExtensionMethods()
        {
            dicexp = new Dictionary<Type, Func<SqlDataReader,T>>();
            dicrowexp = new Dictionary<Type, Func<DataRow,T>>();
        }
        public static T SetT(System.Data.SqlClient.SqlDataReader reader)
        {
            if (!dicexp.ContainsKey(typeof(T)))
            {
              dicexp.Add(typeof(T),SetModel(reader));
            }
            return dicexp[typeof(T)](reader);
        }
        public static T SetT(DataRow da_table )
        {
            //if (!dicrowexp.ContainsKey(typeof(T)))
            //{
            //    dicrowexp.Add(typeof(T), SetModel(da_table));
            //}
            //return dicrowexp[typeof(T)](da_table);
            if (funcc == null)
                funcc = SetModel(da_table);
            return funcc(da_table);
        }
        private static string PropertyTypeString(Type type)
        {
         
            switch (type.Name)
            {
                case "Int32": return "ToInt32";
                case "Object": return "Object";
                case "DateTime": return "ToDateTime";

                case "Int64": return "ToInt64";

                case "Decimal": return "ToDecimal";

                case "Double": return "ToDouble";

                default: return "ToString";

            }
        }
        private static Func<SqlDataReader,T> SetModel(System.Data.SqlClient.SqlDataReader reader)
        {
           
            var para = Expression.Parameter(reader.GetType(), "x");
            List<MemberBinding> members = new List<MemberBinding>();
            var type = typeof(T);
            var key = type.TypeHandle.Value.ToString();
            foreach (var item in type.GetProperties())
            {
                var d = GloadStaticFile._model.GetValueOrDefault(key + item.Name);

                //取到用户的数据库字段
                var dbfiledname = GloadStaticFile._model.GetValueOrDefault(key + item.Name);
                var dbfiledvalue = reader[dbfiledname];
                if (dbfiledvalue is DBNull) continue;
                ///设置绑定的字段和类型
                Expression assignment = Expression.Constant(dbfiledvalue, item.PropertyType);
                MemberBinding memberBinding;
                var modelProtype = PropertyTypeString(item.PropertyType);
                ///实体字段位obj就直接绑定
                if (modelProtype == "Object")
                {
                    memberBinding = Expression.Bind(item, assignment);
                }
                else
                {
                  var expcall=  Expression.Call(typeof(Convert).GetMethod(modelProtype, new Type[] { reader.GetFieldType(dbfiledname) }), assignment);
                   memberBinding=Expression.Bind(item, expcall);
                }
                members.Add(memberBinding);
            }
            var InitType = Expression.MemberInit(Expression.New(type), members);
           return Expression.Lambda<Func<SqlDataReader, T>>(InitType, para).Compile();
        }
        private static Func<DataRow,T> SetModel(DataRow da_table)
        {
           
            var para = Expression.Parameter(da_table.GetType(), "x");
            List<MemberBinding> members = new List<MemberBinding>();
            var type = typeof(T);
            var key = type.TypeHandle.Value.ToString();
            foreach (var item in type.GetProperties())
            {

                //取到用户的数据库字段
                var dbfiledname = GloadStaticFile._model.GetValueOrDefault(key + item.Name);
                var dbfiledvalue = da_table[dbfiledname];
                if (dbfiledvalue is DBNull) continue;
                ///设置绑定的字段和类型
                Expression assignment = Expression.Constant(dbfiledvalue, item.PropertyType);
                MemberBinding memberBinding;
                var modelProtype = PropertyTypeString(item.PropertyType);
                ///实体字段位obj就直接绑定
                if (modelProtype == "Object")
                {
                    memberBinding = Expression.Bind(item, assignment);
                }
                else
                {
                  var expcall=  Expression.Call(typeof(Convert).GetMethod(modelProtype, new Type[] { dbfiledvalue.GetType() }), assignment);
                   memberBinding=Expression.Bind(item, expcall);
                }
                members.Add(memberBinding);
            }
            var InitType = Expression.MemberInit(Expression.New(type), members);
           return Expression.Lambda<Func<DataRow, T>>(InitType, para).Compile();
        }

    }
}
