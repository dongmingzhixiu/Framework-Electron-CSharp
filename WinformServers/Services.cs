using JpFramework.Tools;
using StriveEngine;
using StriveEngine.Core;
using StriveEngine.Tcp.Server;
using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace JpFramework
{
    /// <summary>
    /// 在本地开启服务，监听指定端口
    /// </summary>
    public class Services
    {
        public static Services services;

        private ITcpServerEngine SockterServerEngine;
        private bool IsSocketServerInitialized;
        private static Controller con = new Controller();
        public void StartServer()
        {
            try
            {
                if (SockterServerEngine == null)
                {
                    var port = ConfigTools.Get("port");
                    var _port = port == null || port.Length <= 0 ? 9909 : int.Parse(port);
                    SockterServerEngine = NetworkEngineFactory.CreateTextTcpServerEngine(_port, new DefaultTextContractHelper("\0"));//DefaultTextContractHelper是StriveEngine内置的ITextContractHelper实现。使用UTF-8对EndToken进行编码。 
                }
                //判断 相关的监听事件是否注册
                if (IsSocketServerInitialized)
                {
                    SockterServerEngine.ChangeListenerState(true);
                }
                else
                {
                    InitializeTcpServerEngine();
                }

                //this.ShowListenStatus();
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                if(ee.Message.IndexOf("there's one StriveEngine server instance running")>=0)
                {
                    var res = MessageBox.Show("已打开一个实例或者前面实例的进程为未完全退出，是否重启？\r\n是：重启\r\n否：关闭\r\n取消：不做任何处理", "警告", MessageBoxButtons.YesNoCancel);
                    if (DialogResult.Yes == res)
                    {
                        Application.ExitThread();
                        Application.Exit();
                        Application.Restart();
                        Process.GetCurrentProcess().Kill();
                    }
                    else if (DialogResult.No == res)
                    {
                        Application.ExitThread();
                        Application.Exit();
                        Process.GetCurrentProcess().Kill();
                    }
                }
            }
            services = this;
        }
        /// <summary>
        /// 为 socket 注册 服务事件
        /// </summary>
        private void InitializeTcpServerEngine()
        {
            //客户端连接数量变化时，触发事件
            SockterServerEngine.ClientCountChanged += new CbDelegate<int>(ClientCountChage);
            //客户端与服务端建立连接时， 触发事件
            SockterServerEngine.ClientConnected += new CbDelegate<IPEndPoint>(ClientConnected);
            //客户端断开连接时， 触发事件
            SockterServerEngine.ClientDisconnected += new CbDelegate<IPEndPoint>(ClientDisconnected);
            //接受消息，触发事件
            SockterServerEngine.MessageReceived += new CbDelegate<IPEndPoint, byte[]>(MessageReceived);

            //初始化tcp服务对象
            SockterServerEngine.Initialize();
            //标记tcp 服务已经初始化
            IsSocketServerInitialized = true;
        }

        /// <summary>
        ///  客户端连接数量变化时，触发事件
        /// </summary>
        private void ClientCountChage(int count)
        {
            Console.WriteLine("已连接数量"+count);
            //if (count <= 0) {
            //    System.Environment.Exit(0);
            //}
        }

        /// <summary>
        /// 客户端与服务端建立连接时， 触发事件
        /// </summary>
        /// <param name="IPEndPoint"></param>
        private void ClientConnected(IPEndPoint iPEndPoint)
        {
            var msg = string.Format("{0} 上线", iPEndPoint);
            Console.WriteLine(msg);
        }
        
        /// <summary>
        /// 断开服务时， 触发事件
        /// </summary>
        /// <param name="iPEndPoint"></param>
        private void ClientDisconnected(IPEndPoint iPEndPoint)
        {
            var msg = string.Format("{0} 下线", iPEndPoint);
            Console.WriteLine(msg);
        }

        /// <summary>
        /// 接受消息，触发事件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bMsg"></param>
        private void MessageReceived(IPEndPoint client, byte[] bMsg)
        {
            var msg = Encoding.UTF8.GetString(bMsg); //消息使用UTF-8编码
            //msg = msg.Substring(0, msg.Length - 1); //将结束标记"\0"剔除
            if (msg.IndexOf("!") > 0)
            {
                //判断是请求方法
                var responseText = con.EventHander(msg, msg.IndexOf("isLong=true") > 0?client:null);
                if (responseText != null)
                {
                    Console.WriteLine("得到数据" + responseText);
                    SendMsgToClient(responseText.ToString(), client);
                }
            }
            else if (msg.IndexOf("quit") >= 0)
            {
                Application.ExitThread();
                Application.Exit();
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                SendMsgToClient("经过服务端转播的消息" + msg, client);
            }
            Console.WriteLine("接收到客户端消息：" + msg);

        }

        /// <summary>
        /// 发送消息到指定的客户端
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="client"></param>
        public void SendMsgToClient(string msg, IPEndPoint client)
        {
            var bMsg = System.Text.Encoding.UTF8.GetBytes(msg);//消息使用UTF-8编码
            client = client??(IPEndPoint)SockterServerEngine.GetClientList()[0];
            SockterServerEngine.SendMessageToClient(client, bMsg);
        }
    }
}
