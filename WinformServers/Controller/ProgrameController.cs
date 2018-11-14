// ========================================================================
// Author              :    Jp
// Email               :    1427953302@qq.com/dongmingzhixiu@outlook.com
// Create Time         :    2018-10-22 15:53:38
// Update Time         :    2018-10-22 15:53:43
// =========================================================================
// CLR Version         :    4.0.30319.42000
// Class Version       :    v1.0.0.0
// Class Description   :    登录 
// Computer Name       :    Jp
// =========================================================================
// Copyright ©JiPanwu 2017 . All rights reserved.
// ==========================================================================

using JpFramework.MVC.Entity;
using JpFramework.Tools;
using System.Net;

namespace  JpFramework
{

    public class ProgrameController
    {

        public string title { get; set; }
        public string img_path { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string id { get; set; }


        public string nameOrTitle { get; set; }


        public string Address { get; set; }
        public int Port { get; set; }

        public ProgrameServices programeServices = new ProgrameServices();
        /// <summary>
        /// 得到程式集合 通过 socket
        /// </summary>
        /// <returns></returns>
        public string GetList()
        {
            var addr = IPAddress.Parse(Address);
            var client = new IPEndPoint(addr, Port);

            var result= programeServices.GetList(nameOrTitle);
            for (var i = 0; i < result.Rows.Count; i++) {
                var json = JsonTools.SerializeObject(result.Rows[i]);
                Services.services.SendMsgToClient(json, client);
            }
            return null;
        }

        /// <summary>
        /// 得到程式集合 通过ajax http调用
        /// </summary>
        /// <returns></returns>
        public string GetListHttp()
        {
            var result = programeServices.GetList(nameOrTitle);
            var json = JsonTools.SerializeObject(result);
            return json;
        }
        /// <summary>
        /// 添加程式
        /// </summary>
        /// <returns></returns>
        public string Add()
        {
            var result = programeServices.Add( title, img_path, name, path); 
            return result;
        }
        /// <summary>
        /// 删除程式
        /// </summary>
        /// <returns></returns>
        public string Delete()
        {
            var result = programeServices.Delete(id);
            return result;
        }
        /// <summary>
        /// 得到程式
        /// </summary>
        /// <returns></returns>
        public string GetPro()
        {
            var result = programeServices.GetPro(id);
            return result;
        }
    }
}
