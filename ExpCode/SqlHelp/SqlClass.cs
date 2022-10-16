using ExpCode.StaticFiled;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ExpCode.SqlHelp
{
    internal sealed class SqlClass
    {
        public SqlClass(string str)
        {
            SqlString = str;
        }
        private  SqlConnection connection()
        {
            SqlConnection con = null;
            switch (type)
            {
                case SqlType.SqlServer:
                    con = new SqlConnection(SqlString);
                    con.Open();
                    return con;
                default:
                    con = new SqlConnection(SqlString);
                    con.Open();
                    return con;
            }
        }

        private readonly static SqlType type;
        private readonly  string SqlString;
        internal async Task<T> FirstSql<T>(string sql, object para = null)
        {

            var con = new SqlConnection(SqlString);
            try
            {
                con.Open();
                var cmd = new SqlCommand(sql, con);
                //cmd.Parameters.AddRange(parameters);
                var read =  cmd.ExecuteReader();
                read.Read();
                return ExtensionMethods<T>.SetT(read);
              
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }
           
        }
        internal async Task<List<T>> ToListSql<T>(string sql, object para = null)
        {

            var con = new SqlConnection(SqlString);
            try
            {
                con.Open();
                var cmd = new SqlCommand(sql, con);
                //cmd.Parameters.AddRange(parameters);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                //adapter.SelectCommand.Parameters.AddRange(parameters);
                adapter.Fill(dt);
                //定义一个业务对象集合
                List<T> modelList = new List<T>();
                //获取T类型实体的属性类型的值
                await Task.Run(() =>
                {

                   
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        modelList.Add(ExtensionMethods<T>.SetT(dt.Rows[row]));
                    }

                });
                return modelList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }

        }
        public List<T> ExecuteDataTable<T>(string sqlText, params SqlParameter[] parameters) where T : class, new()
        {
            var con = new SqlConnection(SqlString);
            try
            {

                var cmd = new SqlCommand(sqlText, con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                //adapter.SelectCommand.Parameters.AddRange(parameters);
                adapter.Fill(dt);

                //定义一个业务对象集合
                List<T> modelList = new List<T>();
                //获取T类型实体的属性类型的值
                var dad = typeof(T);
                var c = dad.TypeHandle.Value.ToString();
                PropertyInfo[] pis = dad.GetProperties();
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    //定义一个业务对象
                    T model = new T();
                    //为业务对象的属性赋值
                    foreach (var pi in pis)
                    {
                       
                        var value = dt.Rows[row][GloadStaticFile._model.GetValueOrDefault(c + pi.Name)];
                        if (string.IsNullOrEmpty(value.ToString()))
                        {
                            continue;
                        }
                        pi.SetValue(model, value, null);
                    }
                    modelList.Add(model);
                }

                return modelList;
            }
            finally
            {
                con.Close();
            }


        }
    }

    internal enum SqlType
    {
        SqlServer,
        MySql
    }
}
