using JpFramework.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace JpFramework
{
    /// <summary>
    /// 执行并解析cmd返回值
    /// </summary>
    public class CmdTools
    {
        /// <summary>
        ///     执行CMD语句
        /// </summary>
        /// <param name="param">要执行的CMD命令参数</param>
        public static string RunCmd(string param)
        {
           
            var pro = new Process
            {
                StartInfo =
                {
                    CreateNoWindow = true,
                    FileName ="cmd.exe",
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                }
            };
            pro.Start();
            pro.StandardInput.WriteLine(param);
            pro.StandardInput.WriteLine("exit");
            var outStr = pro.StandardOutput.ReadToEnd();
            pro.Close();
            return outStr;
        }

        public static string  RunSQL(string sql)
        {

            var param = "{ConType:'" + Controller.dbConType+"',SQL:'"+sql.Replace("'",@"\'")+"'}";
            param = Encode(param);
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            path = path.Substring(0, path.Replace("/",@"\").LastIndexOf(@"\"));
            var exeSql = path + @"\JpFrameWork.BaseControl.exe" + " " + param + "";
            var result = RunCmd(exeSql).Replace("\r\n", "");
            var startIndex = result.IndexOf("`@`") + 3;
            var endIndex = result.LastIndexOf("`@`");
            result = result.Substring(startIndex, endIndex - startIndex);

            result = Decode(result);
            return result;
        }

        /// <summary>
        /// 加密sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string Encode(String sql) {
            return HttpUtility.UrlEncode(sql);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string Decode(String sql)
        {
            return HttpUtility.UrlDecode(sql);
        }
    }
}
