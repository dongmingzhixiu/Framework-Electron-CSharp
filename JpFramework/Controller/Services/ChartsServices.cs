using System.Data;

namespace JpFramework
{
    public  class ChartsServices
    {
        /// <summary>
        /// 得到月份
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMonth(string year)
        {
            var sql = string.Format(@"select DISTINCT Cast(replace(SUBSTR(Dates,6,2),'-','') as INTEGER) as month from bus_account 
                                    where SUBSTR(Dates,1,4)='{0}' and dates is  not  null  
                                    order BY Cast(replace(SUBSTR(Dates,6,2),'-','')as INTEGER) asc", year);
            return DBHelper.GetTable(sql);
        }


        /// <summary>
        /// 得到收支类型
        /// </summary>
        /// <returns></returns>
        public static string GetINOUTType()
        {
            return "['支出','收入','信用卡消费','信用卡还款']";
        }

        /// <summary>
        /// 得到消费金额
        /// </summary>
        /// <summary name='yearMonth'>yyyy-MM年月</summary>
        /// <returns></returns>
        public static DataTable GetPrice(string yearMonth,string InOut)
        {
            var sql = string.Format(@"select sum(PRICE) PRICE from bus_account 
                                    where ( dates like  '%{0}-%' or  dates like  '%{1}-%')
                                    and dates is  not  null  and IsOut='{2}' ",
                                    yearMonth.IndexOf("-0")>=0?yearMonth:yearMonth.Replace("-","-0"),
                                    yearMonth.Replace("-0","-"),InOut);
            return DBHelper.GetTable(sql);
        }
       

        /// <summary>
        /// 得到日消费金额
        /// </summary>
        /// <summary name='yearMonth'>yyyy-MM年月</summary>
        /// <returns></returns>
        public static DataTable GetMonthPrices(string yearMonth, string dayCount, string InOut,string userId)
        {
            var date = new string[3];
            var d = yearMonth.Split('-');
            date[0] = d[0];
            date[1] = d[1];


            var dayCounts = int.Parse(dayCount);
            var sqlStr = new string[dayCounts];
            for (var i = 1; i <= dayCounts; i++)
            {

                date[2] = i + "";
                //同一日期符合的四种情况 "2018-4-1" "2018-04-1" "2018-4-01" "2018-04-01"
                var yM1 = date[0] + "-" + int.Parse(date[1]) + "-" + int.Parse(date[2]) + " ";
                var yM2 = date[0] + "-" + date[1].PadLeft(2, '0') + "-" + int.Parse(date[2]) + " ";
                var yM3 = date[0] + "-" + int.Parse(date[1]) + "-" + date[2].PadLeft(2, '0') + " ";
                var yM4 = date[0] + "-" + date[1].PadLeft(2, '0') + "-" + date[2].PadLeft(2, '0') + " ";

                var sql = string.Format(@"select (case when sum(PRICE) is null then 0 else sum(PRICE) end)  PRICE from bus_account 
                                    where ( dates like  '%{0}%' or  dates like  '%{1}%' or  dates like  '%{2}%' or  dates like  '%{3}%')
                                    and dates is  not  null  and IsOut='{4}' and userId='"+ userId + "'",
                                    yM1, yM2, yM3, yM4, InOut);
                sqlStr[i-1] = sql;
            }
            var sqls = string.Join(" UNION ALL ", sqlStr);

            return DBHelper.GetTable(sqls);
        }


        /// <summary>
        /// 得到月消费总金额
        /// </summary>
        /// <summary name='yearMonth'>yyyy-MM年月</summary>
        /// <returns></returns>
        public static DataTable GetMonthSumPrices(string year, string InOut,string userId)
        {
            var date = new string[2];
            date[0] = year;

            var sqlStr=new string[12];
            for (var i = 1; i <= 12; i++)
            {
                date[1] = i + "";
                //同一日期符合的两种情况 "2018-4" "2018-04"
                var yM1 = date[0] + "-" + int.Parse(date[1]) + "-";
                var yM2 = date[0] + "-" + date[1].PadLeft(2, '0') + "-";

                var sql = string.Format(@"select (case when sum(PRICE) is null then 0 else sum(PRICE) end)  PRICE from bus_account 
                                    where ( dates like  '%{0}%' or  dates like  '%{1}%' )
                                    and dates is  not  null  and IsOut='{2}' and userId='"+ userId + "'",
                                    yM1, yM2, InOut);
                sqlStr[i - 1] = sql;
            }
            var sqls = string.Join(" UNION ALL ", sqlStr);

            return DBHelper.GetTable(sqls);
        }

        /// <summary>
        /// 得到月消费总金额
        /// </summary>
        /// <summary name='yearMonth'>yyyy-MM年月</summary>
        /// <returns></returns>
        public static DataTable GetMonthOnlySumPrices(string year, string InOut,string TypeId)
        {
            var date = new string[2];
            date[0] = year;

            var sqlStr = new string[12];
            for (var i = 1; i <= 12; i++)
            {
                date[1] = i + "";
                //同一日期符合的两种情况 "2018-4" "2018-04"
                var yM1 = date[0] + "-" + int.Parse(date[1]) + "-";
                var yM2 = date[0] + "-" + date[1].PadLeft(2, '0') + "-";

                var sql = string.Format(@"select (case when sum(PRICE) is null then 0 else sum(PRICE) end)  PRICE from bus_account 
                                    where ( dates like  '%{0}%' or  dates like  '%{1}%' )
                                    and dates is  not  null  and IsOut='{2}' and TYPEID='{3}'  ",
                                    yM1, yM2, InOut,TypeId);
                sqlStr[i - 1] = sql;
            }
            var sqls = string.Join(" UNION ALL ", sqlStr);

            return DBHelper.GetTable(sqls);
        }


        /// <summary>
        /// 得到日消费金额
        /// </summary>
        /// <summary name='yearMonth'>yyyy-MM年月</summary>
        /// <returns></returns>
        public static DataTable GetDayOnlyPrices(string yearMonth, string dayCount, string InOut,string typeId)
        {
            var date = new string[3];
            var d = yearMonth.Split('-');
            date[0] = d[0];
            date[1] = d[1];


            var dayCounts = int.Parse(dayCount);
            var sqlStr = new string[dayCounts];
            for (var i = 1; i <= dayCounts; i++)
            {

                date[2] = i + "";
                //同一日期符合的四种情况 "2018-4-1" "2018-04-1" "2018-4-01" "2018-04-01"
                var yM1 = date[0] + "-" + int.Parse(date[1]) + "-" + int.Parse(date[2]) + " ";
                var yM2 = date[0] + "-" + date[1].PadLeft(2, '0') + "-" + int.Parse(date[2]) + " ";
                var yM3 = date[0] + "-" + int.Parse(date[1]) + "-" + date[2].PadLeft(2, '0') + " ";
                var yM4 = date[0] + "-" + date[1].PadLeft(2, '0') + "-" + date[2].PadLeft(2, '0') + " ";

                var sql = string.Format(@"select (case when sum(PRICE) is null then 0 else sum(PRICE) end)  PRICE from bus_account 
                                    where ( dates like  '%{0}%' or  dates like  '%{1}%' or  dates like  '%{2}%' or  dates like  '%{3}%')
                                    and dates is  not  null  and IsOut='{4}' and TYPEID='{5}' ",
                                    yM1, yM2, yM3, yM4, InOut, typeId);
                sqlStr[i - 1] = sql;
            }
            var sqls = string.Join(" UNION ALL ", sqlStr);

            return DBHelper.GetTable(sqls);
        }


        /// <summary>
        /// 得到消费总金额
        /// </summary>
        /// <param name="yearMonth"></param>
        /// <returns></returns>
        public static DataTable GetSum(string  yearMonth,string userId)
        {
            var where = "";
            if (yearMonth.IndexOf('-') >= 0)
            {
                var list = yearMonth.Split('-');
                var y1 = list[0] + "-" + int.Parse(list[1]);
                var y2 = list[0] + "-" + list[1].PadLeft(2,'0');
                where = " where (dates like '%" + y1 + "%' or dates like '%" + y2 + "%' )";
            }
            else
            {
                where = " where dates like '%" + yearMonth + "%'";
            }

            where += " and UserId='" + userId + "'";

            var sql =string.Format(
                "select ISOUT,TypeName,TYPEID, sum(price) total from bus_account a left join bus_type t on a.TYPEID=t.ID {0} group by ISOUT,TYPEID", where);
            return DBHelper.GetTable(sql);
        }
    }
}
