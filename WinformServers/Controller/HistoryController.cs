
using JpFramework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace JpFramework
{
    /// <summary>
    /// 历史记录操作类
    /// </summary>
    public class HistoryController
    {
        public string title { get; set; }
        public string url { get; set; }
        public string id { get; set; }


        //获取当前请求连接的 地址
        public string Address { get; set; }
        //获取当前请求连接的 端口
        public int Port { get; set; }

        HistoryServices his = new HistoryServices();
       
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public void GetHistory()
        {
            var addr = IPAddress.Parse(Address);
            var client = new IPEndPoint(addr, Port);
            var table= his.GetHistory();
            //由于使用的socket框架原因，防止数据过多，需要数据一条一条发送
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var rows = table.Rows[i].ItemArray;
                var obj = new {id=rows[0],title=rows[1], url = rows[2], times = rows[3] };
                var msg = JsonTools.SerializeObject(obj);

             
                Services.services.SendMsgToClient(msg, client);
            }

        }


        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public string GetHistoryHttp()
        {
            var table = his.GetHistory();
            //由于使用的socket框架原因，防止数据过多，需要数据一条一条发送
            return JsonTools.SerializeObject(table);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public void AddHistory()
        {
            if (!string.IsNullOrEmpty(title)&&!string.IsNullOrEmpty(url)) {
                his.AddHistory(title, url).ToString();
            }
        }


        public void DeleteHistory()
        {
            his.DeleteHistory(id);
            GetHistory();
        }
    }
}
