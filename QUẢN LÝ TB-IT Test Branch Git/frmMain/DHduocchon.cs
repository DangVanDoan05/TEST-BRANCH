using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;

namespace frmMain
{
   public class DHduocchon
    {
        private static DHduocchon instance;

        public static DHduocchon Instance
        {
            get { if (instance == null) instance = new DHduocchon(); return DHduocchon.instance; }
            private set { DHduocchon.instance = value; }
        }

        private DHduocchon() { }
     
        public static string MaDHdangchon = "";
        public static string MaLKdangchon = "";
        public static string MaYChientai = "";
    }
}
