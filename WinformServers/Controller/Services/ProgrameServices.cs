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

namespace JpFramework
{
    public class ProgrameServices
    {
        /// <summary>
        /// 获取程序列表
        /// </summary>
        /// <param name="nameOrTitle"></param>
        /// <returns></returns>
        public DataTable GetList(string nameOrTitle)
        {
            var sql = "select [id],[name],[img_path],[title],[path] from s_programe where name like '%" + nameOrTitle + "%' or title like '%" + nameOrTitle + "%'";
            var table = DBHelper.GetTable(sql);
            return table;
        }

        /// <summary>
        /// 获取程序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetPro(string id)
        {
            var sql = string.Format("select [id],[name],[img_path],[title],[path] from s_programe where id='{0}'",id);
            var table = DBHelper.Query(sql);
            return JsonTools.SerializeObject(table);
        }
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public string Add(string title, string img_path, string name, string path)
        {
            var sql = "INSERT INTO [dbo].[s_programe] ([name], [img_path], [title], [path]) VALUES ('{0}','{1}','{2}','{3}')";
            sql = string.Format(sql, name,  img_path, title, path);
            return DBHelper.ExecuteSql(sql).ToString();
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public string Delete(string id)
        {
            var sql = "delete from [s_programe] where id='{0}'";
            sql = string.Format(sql, id);
            return DBHelper.ExecuteSql(sql).ToString();
        }

        
    }
}