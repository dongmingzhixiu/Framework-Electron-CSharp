using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using JpFramework.Tools;

namespace  JpFramework
{
    /// <summary>
    ///     数据访问抽象基础类
    ///     Copyright (C) Maticsoft
    /// </summary>
    public class DBHelper
    {
        public static ConType conType { get; set; }

        public DBHelper(ConType _conType)
        {
            conType = _conType;
        }

        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.		
        public static void SetConnection(string connectionStr)
        {
            ReflexTools.ExecuteMethod(conType, "SetConnection", new object[] {connectionStr});
        }

        /// <summary>
        /// 测试是否连接到数据库
        /// </summary>
        /// <returns>连接状态</returns>
        public static bool TestConnect()
        {
            return (bool)ReflexTools.ExecuteMethod(conType, "TestConnect",null);
        }
        /// <summary>
        /// 得到一个数据集合
        /// </summary>
        /// <param name="SQLString">sql语句</param>
        /// <returns>DataTable</returns>
        public static DataTable GetTable(string SQLString)
        {
         
            return (DataTable)ReflexTools.ExecuteMethod(conType, "GetTable", new object[] { SQLString });
        }
//       
        /// <summary>
        ///     执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            return (DataSet)ReflexTools.ExecuteMethod(conType, "Query", new object[] { SQLString });
        }

        /// <summary>
        ///     执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            return (int)ReflexTools.ExecuteMethod(conType, "ExecuteSql", new object[] { SQLString });
        }

        /// <summary>
        ///     执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params DbParameter[] cmdParms)
        {
            return (int)ReflexTools.ExecuteMethod(conType, "ExecuteSqlPar", new object[] { SQLString, cmdParms });
        }

        /// <summary>
        ///     执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content)
        {
            return (int)ReflexTools.ExecuteMethod(conType, "ExecuteSqlCon", new object[] { SQLString, content });
        }

        #region 公用方法

        /// <summary>
        ///     得到最大值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public int GetMaxID(string FieldName, string TableName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public bool Exists(string strSql)
        {
            throw new NotImplementedException();
        }

      
     

       
        #endregion

        #region  执行简单SQL语句


        public int ExecuteSqlByTime(string SQLString, int Times)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public int ExecuteSqlTran(List<string> SQLStringList)
        {
            throw new NotImplementedException();
        }

      

        /// <summary>
        ///     执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public object ExecuteSqlGet(string SQLString, string content)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string SQLString)
        {
            throw new NotImplementedException();
        }

        public object GetSingle(string SQLString, int Times)
        {
            throw new NotImplementedException();
        }

        public object GetSingles(string SQLString, int Times)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
    
    public class HelperTools
    {
        
    }
}
