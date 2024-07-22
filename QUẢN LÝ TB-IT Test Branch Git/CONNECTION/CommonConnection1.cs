using System;
using System.Collections.Generic;
using System.Text;

namespace CONNECTION
{
   public class CommonConnection1
    {
        public static bool ConnectDD1 = false;
        public static string strconDD1 = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLyIT_DD1;Integrated Security=True";

        public static bool ConnectDD2 = false;
        public static string strconDD2 = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLyIT_DD2;Integrated Security=True";

        public static bool ConnectDDK = false;
        public static string strconDDK = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLyIT_DDK;Integrated Security=True";
    }
}
