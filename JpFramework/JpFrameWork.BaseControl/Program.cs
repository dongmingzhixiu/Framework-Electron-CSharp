using JpFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace JpFrameWork.BaseControl
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length <= 0)
            {
                args = new string[1] { "%7bConType%3a%27sqlserver%27%2cSQL%3a%27select+*+from+s_user+where+userName%3d%5c%27admin%5c%27+and++userPass%3d%5c%27123456%5c%27%27%7d" };
            }

            var par = Decode(args[0]);
            //string str = Console.ReadLine();

            //else { 
            //    Console.WriteLine(args[0]);
            //}


            var result = "";
            try {
                //获取参数
                ArgsEntity are = JsonTools.DeserializeJsonToObject<ArgsEntity>(par);
                Controller con = new Controller(are.ConType, are.SQL);
                var str = con.Result();
                result = "{'state':'success','result':" + str + "}";
            } catch (Exception ex) {
                result = "{'state':'error','result':'" + ex.Message.Replace("\"", @"\'") + "'}";
            }
            //Console.WriteLine(result);
            Console.WriteLine(ResultEnCode(result));
            //Console.Read();
            // return;
        }

        /// <summary>
        /// 将返回结果进行装饰
        /// </summary>
        /// <param name="resultJson"></param>
        /// <returns></returns>
        public static string ResultEnCode(string resultJson) {
            return "`@`"+Encode(resultJson) + "`@`";
        }


        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string Encode(String sql)
        {
            return HttpUtility.UrlEncode(sql);
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string Decode(String sql)
        {
            return HttpUtility.UrlDecode(sql);
        }
    }
}
