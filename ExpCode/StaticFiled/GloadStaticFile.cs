using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpCode.Model;

namespace ExpCode.StaticFiled
{
    internal static class GloadStaticFile
    {
        /// <summary>
        /// 存放实体的字段值 用type的handvalue做key
        /// </summary>
        internal static ConcurrentDictionary<string, string> _model;
        static GloadStaticFile()
        {
            _model = new ConcurrentDictionary<string, string>();
        }
        /// <summary>
        /// 获取这个实体所设置的表明和属性名
        /// </summary>
        /// <param name="type"></param>
        internal static void GetTypeProValue(this Type type)
        {

            var key = type.TypeHandle.Value.ToString();
            if (_model.ContainsKey(key)) return;
            var proarray = type.GetProperties();
            StringBuilder sb = new StringBuilder();
            var table = type.CustomAttributes.Where(d => d.AttributeType.Name.Contains(nameof(ModelTableName))).FirstOrDefault();
            if (table != null)
            {
                var value = table.ConstructorArguments[0].Value.ToString();
                _model.GetOrAdd(key + type.Name, value);
            }
            else
            {
                _model.GetOrAdd(key + type.Name, type.Name);
            }
            foreach (var item in proarray)
            {
                var value = item.CustomAttributes.FirstOrDefault();

                if (value != null)
                {
                    var filedvalue = value.ConstructorArguments[0].Value;
                    sb.Append(filedvalue + ",");
                    _model.GetOrAdd(key + item.Name, filedvalue.ToString());
                }
                else
                {
                    sb.Append(item.Name + ",");
                    _model.GetOrAdd(key + item.Name, item.Name);
                }
            }
            sb.Remove(sb.Length - 1, 1);
            _model.GetOrAdd(key, sb.ToString());

        }
    }
}
