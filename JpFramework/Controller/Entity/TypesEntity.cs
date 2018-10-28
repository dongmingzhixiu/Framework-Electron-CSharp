namespace  JpFramework
{
    public class TypesEntity : IEntity
    {
        public int? ID { get; set; }
        public string TypeName { get; set; }
        public string Remark { get; set; }

        /// <summary>
        /// 设置数据库表名称 
        /// </summary>
        /// <returns></returns>
        public string TableName()
        {
            return "bus_type";
        }
    }
}