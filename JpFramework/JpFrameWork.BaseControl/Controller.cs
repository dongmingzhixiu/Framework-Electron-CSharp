using JpFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JpFrameWork.BaseControl
{
    public class Controller
    {
        private ConType ConTypes;
        private String Sql;
        public Controller(ConType conType, string sql) {
            DBHelper.conType = conType;
            Sql = sql;
        }

        /// <summary>
        /// 执行SQL，返回结果 json
        /// </summary>
        /// <returns></returns>
        public string Result() {
           var table= DBHelper.Query(Sql);
            return JsonTools.SerializeObject(table);
        }
    }
}
