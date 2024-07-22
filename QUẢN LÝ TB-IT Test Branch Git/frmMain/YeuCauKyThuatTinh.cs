using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmMain
{
   class YeuCauKyThuatTinh
    {
        private static YeuCauKyThuatTinh instance;

        public static YeuCauKyThuatTinh Instance
        {
            get { if (instance == null) instance = new YeuCauKyThuatTinh(); return YeuCauKyThuatTinh.instance; }
            private set { YeuCauKyThuatTinh.instance = value; }
        }
        private YeuCauKyThuatTinh(){ }
        public static string MaYCKTtuchoi = "";
    }
}
