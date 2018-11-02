using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JpFramework.Tools
{
    class SQLiteHelper
    {
        public static DbConnection GetDbConnection(string connStr)
        {
            return assebly.CreateInstance("System.Data.SQLite.SQLiteConnection", true, BindingFlags.Default, null, new object[1] { connStr }, null, null) as DbConnection;
        }
        public static DbParameter GetDbParameter(string parameterName, object value)
        {
            return assebly.CreateInstance("System.Data.SQLite.SQLiteParameter", true, BindingFlags.Default, null, new object[2] { parameterName, value }, null, null) as DbParameter;
        }
        ///
        /// 动态加载sqlite程序集，整个程序只需要加载一次
        ///
        private static Assembly assebly;
        ///
        /// 这是静态构造函数。应用程序只要用到了这个类，就肯定有数据库的相关操作，就必须加载System.Data.SQLite.DLL。这是单例模式的一种。
        ///
        static SQLiteHelper()
        {
            assebly = ReflexTools.GetAssemblyDll("System.Data.SQLite.DLL");
            //if (Environment.Is64BitProcess)
            //{
            //    assebly = Assembly.LoadFile(@".\System.Data.SQLite.DLL");//64位
            //}
            //else
            //{
            //    assebly = Assembly.LoadFile(@".\System.Data.SQLite.DLL");//32位
            //}
        }

        //public static SqlHelper Instance = new SqlHelper("data source=ZGC-20130716152;initial catalog=Test;integrated security=True;MultipleActiveResultSets=True;");
        public SQLiteHelper(string connStr)
        {
            this.connStr = connStr;
        }
        ///
        /// 连接字符串
        ///
        private string connStr;
        public int ExecuteNonQuery(string sql, params DbParameter[] parameters)
        {
            using (DbConnection conn = GetDbConnection(connStr))
            //using (DbConnection conn = assebly.CreateInstance("System.Data.SQLite.SQLiteConnection", true, BindingFlags.Default, null, new object[1] { connStr }, null, null) as DbConnection)
            {
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (DbParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public object ExecuteScalar(string sql, params DbParameter[] parameters)
        {
            using (DbConnection conn = GetDbConnection(connStr))
            //using (DbConnection conn = assebly.CreateInstance("System.Data.SQLite.SQLiteConnection", true, BindingFlags.Default, null, new object[1] { connStr }, null, null) as DbConnection)
            {
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (DbParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    return cmd.ExecuteScalar();
                }
            }
        }
        public DataTable ExecuteDataTable(string sql, params DbParameter[] parameters)
        {
            using (DbConnection conn = GetDbConnection(connStr))
            //using (DbConnection conn = assebly.CreateInstance("System.Data.SQLite.SQLiteConnection", true, BindingFlags.Default, null, new object[1] { connStr }, null, null) as DbConnection)
            {

                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (DbParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    DataTable datatable = new DataTable();//1.新建数据集
                    using (DbDataAdapter adapter = assebly.CreateInstance("System.Data.SQLite.SQLiteDataAdapter", true, BindingFlags.Default, null, new object[1] { cmd }, null, null) as DbDataAdapter)
                    {
                        adapter.Fill(datatable);//2.填充数据集
                    }
                    return datatable;//3.返回数据集的一张表，因为这里返回的是一张表，所以没有第4步和第5步了。
                }
            }
        }
    }
}
