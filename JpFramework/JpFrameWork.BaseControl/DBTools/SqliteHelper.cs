using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using JpFramework.Tools;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Data.SQLite;
using System.Data.SQLite;

namespace  JpFramework
{


    /// <summary>
    ///     数据访问抽象基础类
    ///     Copyright (C) Maticsoft
    /// </summary>
    public  class SqliteHelper 
    {
        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.		
        private static string connectionString = ConfigTools.GetApp("Connection"); //数据库连接字符串
        private static Assembly SqLite = ReflexTools.GetAssemblyDll("System.Data.SQLite.dll");
        private static DbConnection conn;
        private static DbDataAdapter dbDataAdapter;
        /// <summary>
        /// 得到connection对象
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public  DbConnection GetDbConnection(string connStr)
        {
            if (conn == null) { 
                Type[] pt = new Type[1];
                pt[0] = typeof(string);
                Type t = null;
                var types = SqLite.GetTypes();
                for (var i = 0; i < types.Length; i++)
                {
                    if (types[i].Name == "SQLiteConnection")
                    {
                        t = types[i];
                        break;
                    }
                }
                ConstructorInfo ci = t.GetConstructor(pt);
                //构造Object数组，作为构造函数的输入参数 
                object[] obj = new object[1] { connectionString };
                //调用构造函数生成对象 
                conn =  ci.Invoke(obj) as DbConnection;
            }
            //conn = conn ?? SqLite.CreateInstance("System.Data.SQLite.SQLiteConnection", true, BindingFlags.Default, null, new string[1] { connectionString }, null, null) as DbConnection;
            conn.ConnectionString = connStr;
            return conn;
        }
        /// <summary>
        /// 得到DbDataAdapter对象
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public DbDataAdapter GetDbDataAdapter(string SQLString)
        {
            conn = conn ?? GetDbConnection(connectionString);
            dbDataAdapter =SqLite.CreateInstance("System.Data.SQLite.SQLiteDataAdapter", true, BindingFlags.Default, null, new object[] { SQLString, conn }, null, null) as DbDataAdapter;
            return dbDataAdapter;
        }


        public void SetConnection(string connectionStr)
        {
            connectionString = connectionStr.Length<=0?ConfigTools.GetApp("Connection"):connectionStr;
        }

        /// <summary>
        /// 测试是否连接 
        /// </summary>
        /// <returns>连接状态</returns>
        public bool TestConnect()
        {
            //var connection = ReflexTools.GetTypeDll(SqLite, "SQLiteConnection", connectionString)
            try
            {
                var conn= GetDbConnection(connectionString);
                conn.Open();
                //ReflexTools.GetResultDll(SqLite, "SQLiteConnection", "Open", null);
                //connection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        /// <summary>
        /// 得到一个数据集合
        /// </summary>
        /// <param name="SQLString">sql语句</param>
        /// <returns>DataTable</returns>
        public DataTable GetTable(string SQLString)
        {
            try
            {
                return Query(SQLString).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        #region  执行简单SQL语句

        /// <summary>
        ///     执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string SQLString)
        {
            //using (var connection = new SQLiteConnection(connectionString))
            //{
            var ds = new DataSet();
            try
            {

                var conn = GetDbConnection(connectionString);
                conn.Open();
                var dataAdapter = GetDbDataAdapter(SQLString);
                dataAdapter.Fill(ds, "ds");
                //= ReflexTools.GetResultDll(SqLite, "SQLiteConnection", "Open", null);
                //connection.Open();

                // var command = ReflexTools.GetTypeDll(SqLite, "SQLiteDataAdapter", new object[] { SQLString, connection });
                //var command = new SQLiteDataAdapter(SQLString, connection);

                //ReflexTools.GetResultsDll(SqLite, command, "Fill", new object[] { ds, "ds" });
                // command.Fill(ds, "ds");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ds;
            //}
        }


        #endregion

    }
}