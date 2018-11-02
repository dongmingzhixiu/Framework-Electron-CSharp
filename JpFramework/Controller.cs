using JpFramework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static JpFramework.DbConType;

namespace JpFramework
{
    public class Controller
    {
        public static ConType dbConType ;
        public Controller() {
            //初始化数据库链接方式
            dbConType = ConType.sqlserver;
        }



        #region 反射 执行方法
        /// <summary>
        ///    反射调用方法入口处理函数
        ///    action!method.do?a=1&b=2
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual object EventHander(string str)
        {
            var Info = str.Split('?');
            var classInfo = Info[0].Split('!');
            var value = Info.Length > 1 && Info[1] != "undefined" && !string.IsNullOrEmpty(Info[1]) ? Info[1] : null;
            var result = ExeMethod(classInfo, value);
            return result;
            //return str+"--------------------";
        }

       
        //public virtual string EventHander(string str)
        //{
        //    var Info = str.Split('|');
        //    var classInfo = Info[0].Split('?');
        //    var value = Info.Length > 1 && Info[1] != "undefined" && !string.IsNullOrEmpty(Info[1]) ? Info[1].Split('&') : null;
        //    var result = ExeMethod(classInfo, value);
        //    return result.ToString();
        //}

        /// <summary>
        ///     执行方法 并返回数据
        /// </summary>
        public object ExeMethod(string[] classInfo, string value)
        {
            try
            {
                var mytypes = GetAllTypes();
                foreach (var my in mytypes.Where(my => my.Name.ToLower().Equals(classInfo[0].ToLower() + "controller")))
                {
                    ////实例化对象  实例参数
                    // var objName = Activator.CreateInstance(my);
                    // //执行方法
                    // var LoginForm = my.GetMethod(classInfo[1]);
                    return ReflexTools.ExecuteMethod(my, classInfo[1], value).ToString();
                }
                throw new Exception("没有找到相关类或方法:" + classInfo[0]);
            }
            catch (Exception em)
            {
                MessageBox.Show(em.Message);
                return em;
            }
        }


        public static Type[] typeInfo;

        /// <summary>
        /// 反射得到所有类 
        /// </summary>
        /// <returns></returns>
        public Type[] GetAllTypes()
        {
            //获取程序集 所有类
            return typeInfo = typeInfo ?? ReflexTools.GetAllTypes();
        }
        #endregion
    }
}
