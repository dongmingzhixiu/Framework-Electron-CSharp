
using JpFramework.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;

namespace JpFramework
{
    /// <summary>
    /// 历史记录操作类
    /// </summary>
    public class TimeLineServicesr
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public DataTable GetTimeLine(int type)
        {
            var sql =string.Format("select id,title,remake,CONVERT(varchar(100), times, 102) from s_timeline where type='{0}' order by times asc", type);
            return DBHelper.GetTable(sql);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public void AddTimeLine(string title,string remake,string times,string type)
        {
            var sql = "insert into s_timeline (title,remake,times,type) values ('{0}','{1}','{2}','{3}')";
            sql = string.Format(sql,title, remake, times,type);
            DBHelper.ExecuteSql(sql);
        }

    }
}
