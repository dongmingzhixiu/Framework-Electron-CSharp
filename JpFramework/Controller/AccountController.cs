// ========================================================================
// Author              :    Jp
// Email               :    1427953302@qq.com/dongmingzhixiu@outlook.com
// Create Time         :    2018-4-22 15:53:38
// Update Time         :    2018-4-22 15:53:43
// =========================================================================
// CLR Version         :    4.0.30319.42000
// Class Version       :    v1.0.0.0
// Class Description   :    账单页面
// Computer Name       :    Jp
// =========================================================================
// Copyright ©JiPanwu 2017 . All rights reserved.
// ==========================================================================

using JpFramework.Tools;

namespace  JpFramework
{
    public class AccountController
    {
       

        /// <summary>
        /// 获取账目信息 
        /// </summary>
        /// <returns></returns>
        public string GetTableJson(string typeId,string time,string userId)
        {
            var table=AccountServices.GetAccount(typeId, time, userId);
            var json = JsonTools.SerializeObjectLayui(table);
            return json;
        }

        /// <summary>
        /// 得到今天添加条数
        /// </summary>
        /// <returns></returns>
        public string DayCount()
        {
            var table = AccountServices.DayCount();
            var count = table.Rows[0][0].ToString();
            return count.ToString();
        }


      


        /// <summary>
        /// 绑定编辑内容
        /// </summary>
        /// <returns></returns>
        public string Editor(string id)
        {
            return "";
        }


     


        /// <summary>
        /// 删除信息
        /// </summary>
        /// <returns></returns>
        public string Delete(string Id)
        {
            var flg = AccountServices.Delete(Id);
            return flg.ToString();
        }

       
    }
}