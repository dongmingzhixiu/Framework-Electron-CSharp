using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JpFramework
{
    public class TestController
    {
        public string test1 { get; set; }
        public double test2 { get; set; }
        public int test3 { get; set; }
        public bool test4 { get; set; }

        public string Test()
        {
            return test2 + "|" + test2;
        }
        
        
    }


}
