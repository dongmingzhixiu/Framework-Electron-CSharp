// ========================================================================
// Author              :    Jp
// Email               :    1427953302@qq.com/dongmingzhixiu@outlook.com
// Create Time         :    2018-4-22 15:53:38
// Update Time         :    2018-4-22 15:53:43
// =========================================================================
// CLR Version         :    4.0.30319.42000
// Class Version       :    v1.0.0.0
// Class Description   :    账单数据访问服务类
// Computer Name       :    Jp
// =========================================================================
// Copyright ©JiPanwu 2017 . All rights reserved.
// ==========================================================================

using System;
using System.Data;
using JpFramework.Tools;

namespace  JpFramework
{
    public class AccountServices
    {
        /// <summary>
        ///     得到账目信息
        /// </summary>
        /// <returns></returns>
        public static DataTable DayCount()
        {
            var year = DateTime.Now.Year.ToString();
            var month = DateTime.Now.Month.ToString();
            var day = DateTime.Now.Day.ToString();

            //同一日期符合的四种情况 "2018-4-1" "2018-04-1" "2018-4-01" "2018-04-01"
            var yM1 = year + "-" + int.Parse(month) + "-" + int.Parse(day) + " ";
            var yM2 = year + "-" + month.PadLeft(2, '0') + "-" + int.Parse(day) + " ";
            var yM3 = year + "-" + int.Parse(month) + "-" + day.PadLeft(2, '0') + " ";
            var yM4 = year + "-" + month.PadLeft(2, '0') + "-" + day.PadLeft(2, '0') + " ";



            var sql = "SELECT (case when count(ID) is null then 0 else count(ID)  end)  counts  from bus_account ";
            sql += string.Format(" Where ( dates like  '%{0}%' or  dates like  '%{1}%' or  dates like  '%{2}%' or  dates like  '%{3}%')"
                , yM1, yM2, yM3, yM4);
            return DBHelper.GetTable(sql);
        }

        /// <summary>
        ///     得到账目信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAccount(string typeId,string time,string userId)
        {
            var sql =
                "SELECT a.ID,a.Names,a.PRICE,a.TYPEID,a.ISOUT,a.Dates,a.Remark,t.TypeName from bus_account a left join bus_type t on a.TYPEID=t.ID";
            sql += " Where 1=1  " +(typeId.Trim() == "" ? "" : " and a.TYPEID='" + typeId + "'")+
                " and a.Dates like '%" + time + "%'  and a.UserId='"+ userId + "'";
            sql += " order by a.Dates desc";
            return DBHelper.GetTable(sql);
        }

        /// <summary>
        ///     得到账目信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAccount(string id = null)
        {
            var sql =
                "SELECT a.ID,a.Names,a.PRICE,a.TYPEID,a.ISOUT,a.Dates,a.Remark,t.TypeName from bus_account a left join bus_type t on a.TYPEID=t.ID";
            if (!string.IsNullOrEmpty(id))
            {
                sql += string.Format(" Where a.ID='{0}'", id);
            }
            sql += " order by a.Dates desc";
            return DBHelper.GetTable(sql);
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="json"></param>
        /// <param name="Id">编号值</param>
        /// <returns></returns>
        public static bool Save(string json,string Id)
        {
            IEntity account = JsonTools.DeserializeJsonToObject<AccountEntity>(json);
            var sql = SqlTools.GetSql<IEntity>(account, "ID", Id);
            return DBHelper.ExecuteSql(sql) >= 1;
        }


        /// <summary>
        /// 删除语句
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool Delete(string Id)
        {
            var sql = SqlTools.GetDeleteSql<IEntity>(new AccountEntity(), Id, "ID");
            return DBHelper.ExecuteSql(sql) >= 1;
        }

        /// <summary>
        /// 得到消费信息时间线
        /// </summary>
        /// <param name="month">要查询的月份</param>
        /// <returns></returns>
        public static DataTable TimeLine(string month,string userId)
        {
            var date = month.Split('-');
            //同一日期符合的两种情况 "2018-4" "2018-04"
            var yM1 = date[0] + "-" + int.Parse(date[1]) + "-";
            var yM2 = date[0] + "-" + date[1].PadLeft(2, '0') + "-";

            var sql = @"SELECT
	                        a.ID,
	                        a.Names,
	                        t.TypeName,
	                        a.TYPEID,
	                        a.PRICE,
	                        a.ISOUT,
	                        a.Dates,
	                        a.Remark,
	                        substr(a.Dates,11,18) hours,
	                        substr(a.Dates,1,10) day
                        FROM bus_account a
                        left join bus_type t on t.ID=a.TYPEID
                        where (substr(a.Dates,1,10) like '%" + yM1 + "%' or substr(a.Dates,1,10) like '%" + yM2 + "%') " +
                      " and a.UserID='"+userId+"'"
                      +"order by substr(a.Dates,1,10) desc";
            return DBHelper.GetTable(sql);
        }

    }
}