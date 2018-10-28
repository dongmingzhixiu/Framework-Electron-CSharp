namespace  JpFramework
{
    public class AccountEntity:IEntity
    {
        public int? ID { get; set; }
        public string Names { get; set; }
        public string TYPEID { get; set; }
        public string PRICE { get; set; }
        public string ISOUT { get; set; }
        public string Dates { get; set; }
        public string Remark { get; set; }

        /// <summary>
        /// 设置数据库表名称 
        /// </summary>
        /// <returns></returns>
        public string TableName()
        {
            return "bus_account";
        }
    }
}