using System;
using System.IO;
using System.Net;

namespace JpFramework
{
    public class HttpServices
    {
        private readonly string URL;
        public HttpServices(string url)
        {
            URL = url;
        }
        public void Run()
        {
            Controller con = new Controller();
            using (var listerner = new HttpListener())
            {
                listerner.AuthenticationSchemes = AuthenticationSchemes.Anonymous;//指定身份验证 Anonymous匿名访问
                                                                                  //   listerner.Prefixes.Add("http://localhost:8080/web/");
                listerner.Prefixes.Add(URL); //http://localhost/web/
                listerner.Start();
                Console.WriteLine("开启监听：" + URL);
                while (true)
                {
                    //等待请求连接
                    //没有请求则GetContext处于阻塞状态
                    var ctx = listerner.GetContext();
                    ctx.Response.StatusCode = 200;//设置返回给客服端http状态代码
                    //Console.WriteLine("url：" + ctx.Request.Url.ToString());
                    //Console.WriteLine("httpMethod：" + ctx.Request.HttpMethod.ToString());
                    //var queryString = ctx.Request.QueryString;
                    //Console.WriteLine("参数个数" + queryString.Count);
                    //Console.WriteLine("第一个参数键" + queryString.Keys[0]);
                    //Console.WriteLine("第一个参数值" + queryString[queryString.Keys[0]]);
                    //Console.WriteLine("rawUrl：" + ctx.Request.RawUrl.ToString());
                    ctx.Response.AddHeader("Access-Control-Allow-Origin", "*");
                    ctx.Response.ContentType = "text/html";
                    ////使用Writer输出http响应代码
                    using (var writer = new StreamWriter(ctx.Response.OutputStream))
                    {
                        var url = ctx.Request.RawUrl.ToString();
                        url = url.Substring(1, url.Length - 1);
                        //判断是请求方法
                        var responseText = con.EventHander(url);

                        writer.WriteLine(responseText);
                    }

                }
            }
        }
    }
}