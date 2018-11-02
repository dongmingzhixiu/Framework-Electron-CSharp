using System;
using JpFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUtil
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Controller con = new Controller();
            con.EventHander("Login!LoginForm?userName=admin&userPass=123456");
        }
    }
}
