
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
    public class TimeLineController
    {
        public string title { get; set; }
        public string remake { get; set; }
        public string times { get; set; }


        //获取当前请求连接的 地址
        public string Address { get; set; }
        //获取当前请求连接的 端口
        public int Port { get; set; }

        TimeLineServicesr line = new TimeLineServicesr();
       
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public void GetWorkLine()
        {
            var addr = IPAddress.Parse(Address);
            var client = new IPEndPoint(addr, Port);
            var table= line.GetTimeLine(1);
            //由于使用的socket框架原因，防止数据过多，需要数据一条一条发送
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var rows = table.Rows[i].ItemArray;
                var obj = new {id=rows[0],title=rows[1], remake = rows[2], times = rows[3] };
                var msg = JsonTools.SerializeObject(obj);
                Services.services.SendMsgToClient(msg, client);
            }

        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public void AddWorkLine()
        {
            if (!string.IsNullOrEmpty(title)) {
                line.AddTimeLine(title, remake,times,"1");
            }
        }

    }
}
