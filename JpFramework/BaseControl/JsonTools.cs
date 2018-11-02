using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace  JpFramework.Tools
{
    /// <summary>
    ///     Json帮助类
    /// </summary>
    public class JsonTools
    {
        /// <summary>
        ///     将对象序列化为JSON格式
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
         /// <summary>
        ///     将对象序列化为JSON格式 专用于layui绑定table表格时的数据
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObjectLayui(object obj,string head="{\"code\": 0,	\"msg\": \"\",	\"count\": 1000,	\"data\": ",string end="}")
        {
            return head+SerializeObject(obj)+end;
        }
        /// <summary>
        ///     解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            var serializer = new JsonSerializer();
            var sr = new StringReader(json);
            var o = serializer.Deserialize(new JsonTextReader(sr), typeof (T));
            var t = o as T;
            return t;
        }

        /// <summary>
        ///     解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            var serializer = new JsonSerializer();
            var sr = new StringReader(json);
            var o = serializer.Deserialize(new JsonTextReader(sr), typeof (List<T>));
            var list = o as List<T>;
            return list;
        }

        /// <summary>
        ///     反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            return JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
        }
    }
}