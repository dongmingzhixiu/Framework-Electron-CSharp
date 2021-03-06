﻿// ========================================================================
// Author              :    Jp
// Email               :    1427953302@qq.com/dongmingzhixiu@outlook.com
// Create Time         :    2018-4-22 15:53:38
// Update Time         :    2018-4-22 15:53:43
// =========================================================================
// CLR Version         :    4.0.30319.42000
// Class Version       :    v1.0.0.0
// Class Description   :    登录数据访问服务类
// Computer Name       :    Jp
// =========================================================================
// Copyright ©JiPanwu 2017 . All rights reserved.
// ==========================================================================

using System.Data;
using System.Windows.Forms;
using JpFramework.MVC.Entity;
using JpFramework.Tools;

namespace  JpFramework
{
    public  class LoginServices
    {
        /// <summary>
        /// 简单判断登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userPass">密码</param>
        /// <returns></returns>
        public bool Login(string userName,string userPass)
        {
           
            var sql =
                string.Format(
                    "select * from s_user where userName='{0}' and  userPass='{1}'",// 
                    userName, userPass);
            var result = DBHelper.GetTable(sql);
            return result.Rows.Count > 0;
        }

    }
}
