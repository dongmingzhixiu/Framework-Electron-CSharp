// ========================================================================
// Author              :    Jp
// Email               :    1427953302@qq.com/dongmingzhixiu@outlook.com
// Create Time         :    2018-4-22 15:53:38
// Update Time         :    2018-4-22 15:53:43
// =========================================================================
// CLR Version         :    4.0.30319.42000
// Class Version       :    v1.0.0.0
// Class Description   :    根据实体类 通过反射 生成执行sql
// Computer Name       :    Jp
// =========================================================================
// Copyright ©JiPanwu 2017 . All rights reserved.
// ==========================================================================

using System;
using System.Linq;
using System.Reflection;

namespace  JpFramework.Tools
{
    public class SqlTools
    {
        /// <summary>
        ///     通过反射得到sql
        /// </summary>
        /// <typeparam name="T">类名称</typeparam>
        /// <param name="t">类对象</param>
        /// <param name="WhereId">判断依据，新增还是修改</param>
        /// <param name="IdValue">Id的值</param>
        /// <returns></returns>
        public static string GetSql<T>(T t, string WhereId = "ID",string IdValue="")
        {
            var type = t.GetType(); //获取类型
            var pr = type.GetProperties();
            var where = "";
            var tableMethod = type.GetMethod("TableName");
            var obj = Activator.CreateInstance(type);
            var tableName = tableMethod.Invoke(obj, null);

            //INSERT INTO `bus_account` (ID,Names,TYPEID,PRICE,ISOUT,Dates,Remark) VALUES ('0', '早晨', '1', '22.22', '0', '', '这是一条备注信息，不知道他有多长，反正就是很长');

            var insertSqlStr = string.Format("INSERT INTO {0} ({1}) values ({2}) ", tableName, "{0}", "{1}");
            var insertColTemp = "";
            var insertValTemp = "";
            var isUpdate = false;

            //UPDATE BUS_ACCOUNT SET NAMES='123',TYPEID='2' WHERE ID='0'
            var updateSqlStr = string.Format("UPDATE {0} set {1} {2}", tableName, "{0}", "{1}");
            var updateSet = "";
            for (var i = 0; i < pr.Length; i++)
            {
                var name = pr[i].Name;
                var value = pr[i].GetValue(t, null);

                //和判断条件一致，并且值不为空，那么为修改语句
                if (name.Trim().ToLower() == WhereId.Trim().ToLower() )
                {
//                    if (IdValue.Trim().Length <= 0) continue;
                    if (string.IsNullOrEmpty(IdValue)) continue;
                    @where = string.Format(" where {0}='{1}'  ", name, value );
                    isUpdate = true;
                }
                else
                {
                    insertColTemp += name + (i < pr.Length - 1 ? "," : "");
                    insertValTemp += name + (i < pr.Length - 1 ? "','" : "");
                    updateSet += string.Format("{0}='{1}'", name, value) + (i >= pr.Length - 1 ? "" : ",");
                }
            }

            return isUpdate
                ? string.Format(updateSqlStr, updateSet, @where)
                : string.Format(insertSqlStr, insertColTemp, "'"+ insertValTemp + "'");
        }

        /// <summary>
        ///     通过反射得到sql
        /// </summary>
        /// <typeparam name="T">类名称</typeparam>
        /// <param name="t">类对象</param>
        /// <param name="idValue">id值</param>
        /// <param name="WhereId">判断条件</param>
        /// <returns></returns>
        public static string GetDeleteSql<T>(T t, string idValue, string WhereId = "ID")
        {
            var type = t.GetType(); //获取类型
            var pr = type.GetProperties();
            var where = "";
            var tableMethod = type.GetMethod("TableName");
            var obj = Activator.CreateInstance(type);
            var tableName = tableMethod.Invoke(obj, null);
            //Delete from `bus_account`;
            var delteSqlStr = string.Format("Delete from {0} {1}", tableName, "{0}");
            foreach (var t1 in pr.Where(t1 => t1.Name.Trim().ToLower() == WhereId.Trim().ToLower()))
            {
                @where = string.Format(" where {0}='{1}' ", t1.Name, idValue);
                break;
            }
            return string.Format(delteSqlStr, where);
        }
    }
}