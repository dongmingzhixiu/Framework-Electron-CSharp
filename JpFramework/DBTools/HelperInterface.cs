using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace JpFramework
{
    public interface HelperInterface
    {
        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.		
         void SetConnection(string connectionStr);

        /// <summary>
        /// 测试是否连接到oracle
        /// </summary>
        /// <returns>连接状态</returns>
         bool TestConnect();

        /// <summary>
        /// 得到一个数据集合
        /// </summary>
        /// <param name="SQLString">sql语句</param>
        /// <returns>DataTable</returns>
        DataTable GetTable(string SQLString);

        /// <summary>
        ///     执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        DataSet Query(string SQLString);


        #region 公用方法

        /// <summary>
        ///     得到最大值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        int GetMaxID(string FieldName, string TableName);

        /// <summary>
        ///     是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        bool Exists(string strSql);

        /// <summary>
        ///     是否存在（基于SQLiteParameter）
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        bool ExistPar(string strSql, params DbParameter[] cmdParms);

        /// <summary>
        ///     执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        int ExecuteSql(string SQLString);

        /// <summary>
        ///     执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        int ExecuteSqlPar(string SQLString, params DbParameter[] cmdParms);


        /// <summary>
        ///     执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        int ExecuteSqlCon(string SQLString, string content);
        #endregion

        #region  执行简单SQL语句


        int ExecuteSqlByTime(string SQLString, int Times);

        /// <summary>
        ///     执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        int ExecuteSqlTran(List<string> SQLStringList);



        /// <summary>
        ///     执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        object ExecuteSqlGet(string SQLString, string content);

        /// <summary>
        ///     向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        int ExecuteSqlInsertImg(string strSQL, byte[] fs);

        /// <summary>
        ///     执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        object GetSingle(string SQLString);

        object GetSingleTimes(string SQLString, int Times);

        #endregion
    }
}
