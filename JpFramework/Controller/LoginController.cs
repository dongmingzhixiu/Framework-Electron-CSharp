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

namespace  JpFramework
{

    public class LoginController
    {
        public string LoginForm(string userName, string userPass)
        {
            var loginServices=new LoginServices();
            var data=  loginServices.Login(userName, userPass);
            if (data.Rows.Count<=0)
            {
                throw new MessageTipShow("用户名或密码错误！");
            }
            return "true";
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public string GetUser(string userId)
        {
            var userJson = JsonTools.SerializeObject(userId);
            return userJson;
        }
    }
}
