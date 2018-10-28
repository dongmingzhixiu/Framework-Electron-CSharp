// ========================================================================
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
using JpFramework.MVC.Entity;
using JpFramework.Tools;

namespace  JpFramework
{
    public  class LoginServices
    {
        public DataTable Login(string userName,string userPass)
        {
            var sql =
                string.Format(
                    "select * from sys_user where USERNAME='{0}' and  USERPASS='{1}'",// 
                    userName, userPass);
            var table =DBHelper.Query(sql).Tables[0];
            return table;
            
        }

        public static bool Update(string userJson, string Id)
        {
            IEntity account = JsonTools.DeserializeJsonToObject<UserEntity>(userJson);
            var sql = SqlTools.GetSql<IEntity>(account, "ID", Id);
            return DBHelper.ExecuteSql(sql) >= 1;
        }
    }
}
