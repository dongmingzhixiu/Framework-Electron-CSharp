// ========================================================================
// Author              :    Jp
// Email               :    1427953302@qq.com/dongmingzhixiu@outlook.com
// Create Time         :    2018-4-25 11:50:58
// Update Time         :    2018-4-25 11:50:58
// =========================================================================
// CLR Version         :    4.0.30319.42000
// Class Version       :    v1.0.0.0
// Class Description   :    反射工具类
// Computer Name       :    Jp
// =========================================================================
// Copyright ©JiPanwu 2017 . All rights reserved.
// ==========================================================================

using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace JpFramework.Tools
{
    /// <summary>
    /// 反射工具类
    /// </summary>
    public class ReflexTools
    {


        /// <summary>
        /// 反射得到所有类 
        /// </summary>
        /// <returns></returns>
        public static Type[] GetAllTypes()
        {
            //获取程序集 所有类型
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            var ass = Assembly.LoadFrom(path);
            return ass.GetTypes();
        }
        /// <summary>
        /// 反射执行方法
        /// </summary>
        /// <param name="my"></param>
        /// <param name="functionName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ExecuteMethod(Type my, string functionName, string value, IPEndPoint client)
        {
            //实例化对象  实例参数
            var objName = Activator.CreateInstance(my);
            value += client != null ? (!string.IsNullOrEmpty(value) ? "&" : "") + "Address=" + client.Address + "&Port=" + client.Port : value;
            //为对象属性赋值
            SetProperty(my, objName, value);
            //执行方法
            var objMethod = my.GetMethod(functionName);
            return objMethod.Invoke(objName, null);
        }

        //反射执行方法
        public static object ExecuteMethod(ConType conType, string functionName, object[] obj)
        {
            var className = DBConfig.GetClassName(conType);

            //获取程序集 所有类型
            var exeNames = Application.ExecutablePath.Replace(Application.StartupPath, "").Replace(@"\", "");
            var ass = Assembly.LoadFrom(exeNames);
            var t = ass.GetType(className);
            return ExecuteMethod(t, functionName, obj);
        }

        //反射 为对象属性赋值
        public static void SetProperty(Type type, object objName, string list)
        {
            var dic = new Dictionary<String, String>();
            //拆解参数
            if (!string.IsNullOrEmpty(list)) {
                var paraList = list.Split('&');
                for (var d = 0; d < paraList.Length; d++) {
                    var _d = paraList[d].Split('=');
                    dic[_d[0]] = _d[1];
                }
            }

            //获取当前类的所有属性，并为属性赋值
            var propertyInfo = type.GetProperties();
            for (var i = 0; i < propertyInfo.Length; i++)
            {
                var property = propertyInfo[i];
                var str = "&" + list;
                var p = "&" + property.Name + "=";
                if (!("&" + list).Contains("&" + property.Name + "=")) { continue; }

                var value = dic[property.Name];
                value = value.ToLower() == "true" || value.ToLower() == "false" ? value.ToLower() : value;

                if (!property.PropertyType.IsGenericType)
                {
                    //非泛型
                    property.SetValue(objName, string.IsNullOrEmpty(value) ? null : Convert.ChangeType(value, property.PropertyType), null);
                }
                else
                {
                    //泛型Nullable<>
                    var genericTypeDefinition = property.PropertyType.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(Nullable<>))
                    {
                        property.SetValue(objName, string.IsNullOrEmpty(value) ? null : Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType)), null);
                    }
                }
            }
        }

        /// <summary>
        /// 反射执行方法
        /// </summary>
        /// <param name="my"></param>
        /// <param name="functionName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ExecuteMethod(Type my, string functionName, object[] value)
        {
            //实例化对象  实例参数
            var objName = Activator.CreateInstance(my);
            //执行方法
            var objMethod = my.GetMethod(functionName);
            return objMethod.Invoke(objName, value);
        }


        /// <summary>
        /// 反射调用方法
        /// </summary>
        /// <param name="dllName"></param>
        /// <returns>返回加载dll的程序集</returns>
        public static Assembly GetAssemblyDll(string dllName,string path="") {
            if (string.IsNullOrEmpty(path)) {
                var obj = Assembly.GetExecutingAssembly();
                var nowPath=obj.Location;
                var length = nowPath.LastIndexOf(@"\");
                length=length<=0 ? nowPath.LastIndexOf("/"):length;
                path = nowPath.Substring(0, length);
            }


            path = path + "\\" + dllName;
            var ass = Assembly.UnsafeLoadFrom(path);
            return ass;
        }
        /// <summary>
        /// 根据程序获取类实例
        /// </summary>
        /// <param name="ass"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static object GetTypeDll(Assembly ass, string className,object [] value) {
            var my = ass.GetType(className);
            return Activator.CreateInstance(my, value);
        }

        /// <summary>
        /// 执行dll方法
        /// </summary>
        /// <param name="my"></param>
        /// <param name="objName"></param>
        /// <param name="functionName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object GetResultDll(Assembly ass,string className,string functionName,object [] value) {
            var objName = ass.GetType(className);
            Type t = null;
            var types=ass.GetTypes();
            for (var i = 0; i < types.Length; i++) {
                if (types[i].Name == className) {
                    t = types[i];
                    break;
                }
            }
            if (value == null)
            {
                object objName2 = Activator.CreateInstance(t);
            }
            else
            {
                object objName2 = Activator.CreateInstance(objName.GetType(),value);

            }
            //执行方法
            var objMethod = objName.GetType().GetMethod(functionName);
            return objMethod.Invoke(objName, value);
        }

        /// <summary>
        /// 执行dll方法
        /// </summary>
        /// <param name="my"></param>
        /// <param name="objName"></param>
        /// <param name="functionName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object GetResultsDll(Assembly ass, object objName, string functionName, object [] value)
        {
            //执行方法
            var objMethod = objName.GetType().GetMethod(functionName);
            return objMethod.Invoke(objName, value);
        }
    }
}
