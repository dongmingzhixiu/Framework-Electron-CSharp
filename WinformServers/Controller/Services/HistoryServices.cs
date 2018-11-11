using JpFramework.Tools;
using System.Data;

namespace JpFramework
{
    public class HistoryServices
    {
        /// <summary>
        /// 得到历史记录
        /// </summary>
        /// <returns></returns>
        public DataTable GetHistory()
        {
            var sql = "select id,title,url,times from s_history";
            return  DBHelper.GetTable(sql);
        }

        /// <summary>
        ///添加历史记录
        /// </summary>
        /// <returns></returns>

        public bool AddHistory(string title,string url)
        {
            var sql = "insert into s_history(title,url) values('{0}','{1}')";
            sql = string.Format(sql, title, url);
            return DBHelper.ExecuteSql(sql)>0;
        }


        /// <summary>
        ///删除历史记录
        /// </summary>
        /// <returns></returns>

        public bool DeleteHistory(string id)
        {
            var sql = "delete from s_history where id='{0}'";
            sql = string.Format(sql,id);
            return DBHelper.ExecuteSql(sql) > 0;
        }
    }
}
