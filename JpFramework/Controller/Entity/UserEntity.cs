using System;
namespace JpFramework.MVC.Entity
{
    public class UserEntity:IEntity
    {
        public int ID { get; set; }
        public string USERNAME { get; set; }
        public string USERPASS { get; set; }

        public string TableName()
        {
            return "sys_user";
        }
    }
}
