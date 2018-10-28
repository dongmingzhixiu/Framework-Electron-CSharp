using System;
using JpFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProjects
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            Controller controller =  new Controller();
            // 方法体
            var str=controller.EventHander("Test!Test?test1=测试测试&test2=123456&test3=34&test4=false");
        }
    }
}
