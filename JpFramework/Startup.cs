using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace JpFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        public static Controller controller;

        /// <summary>
        /// 程序入口
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        public async Task<object> EventHander(string input)
        {
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                controller = controller ?? new Controller();
                // 方法体
                return controller.EventHander(input);

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
