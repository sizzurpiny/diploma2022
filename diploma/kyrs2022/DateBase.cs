using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kyrs2022
{
    class DateBase
    {
        //public static string connStr = $@"Data Source=LAPTOP-21PGG45B\IGOR;Initial Catalog=kyrs2022;Integrated Security=True";
        public static string connStr = $@"Data Source=u1701178.plsk.regruhosting.ru;Initial Catalog=u1701178_IgorT;Persist Security Info=True;User ID=u1701178_Igor;Password=Zxcasdqwe1;";
        public static int Id_user;
        public static string Name_User;
        public static string Secname_user;
        public static string Email;
        public static string Pass;
        public static string Phonenum;
        public static int Active;
        public static string Role;
    }
    class MyTable
    {
        public MyTable(string uni, string city, string ball)
        {
            this.uni = uni;
            this.city = city;
            this.ball = ball;
        }
        public string uni { get; set; }
        public string city { get; set; }
        public string ball { get; set; }
    }

}
