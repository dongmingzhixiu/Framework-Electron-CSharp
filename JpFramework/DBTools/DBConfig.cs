using System;
using System.Collections.Generic;
namespace JpFramework
{
    public enum ConType
    {
        sqlite,
        mysql,
        sqlserver,
        oracle
    }

    public class DBConfig
    {

        public static Dictionary<ConType, string> classDic;

        public static Dictionary<ConType, string> GetClassDic()
        {
            return classDic = classDic ?? new Dictionary<ConType, string>
            {
                {ConType.sqlite,"JpFramework.SqliteHelper"},
                {ConType.mysql,"JpFramework.MySQLHelper"},
                {ConType.sqlserver,"JpFramework.SqliteHelper"},
                {ConType.oracle,"JpFramework.SqliteHelper"},
            };
        }

        public static string GetClassName(ConType key)
        {
            return GetClassDic()[key];
        }
    }
}
