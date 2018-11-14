using JpFramework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JpFramework
{
    class Program
    {
        /// <summary>
        /// 主程序入口
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try { 
                StartHttp();
                var ser = new Services();
                ser.StartServer();
                StartElectronApp();


                Console.ReadKey();
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 开启 http 服务监听 9910
        /// </summary>
        public static void StartHttp()
        {
            var url =string.Format("http://{0}:{1}/", ConfigTools.Get("addr"),ConfigTools.Get("httpPort"));
            HttpServices httpServer;
            httpServer = new HttpServices(url);
            var thread = new Thread(new ThreadStart(httpServer.Run));
            thread.Start();
        }


        /// <summary>
        /// 从控制台启动Electron程序
        /// </summary>
        public static void StartElectronApp()
        {
            var cmd=ConfigTools.Get("ElectronAppPath");
            if (cmd.IndexOf("|DataDirectory|") >= 0) {
                cmd = cmd.Replace("|DataDirectory|",Application.StartupPath);
            }
            var str= CmdTools.RunCmd(cmd);
            Console.WriteLine(str);
        }



    }
}
