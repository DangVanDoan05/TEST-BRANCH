using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
     public class QLyVTTHDTO
    {
        public QLyVTTHDTO(DataRow row)
        {
            this.MABT = row["MABT"].ToString();
            this.MAVTTH = row["MAVTTH"].ToString();
            this.TENVTTH = row["TENVTTH"].ToString();
            this.SLVTTH = int.Parse(row["SLVTTH"].ToString());
            this.TINHTRANG = row["TINHTRANG"].ToString();
        }

        //  QLYVATTUTH(MABT,MAVTTH,TENVTTH,SLVTTH)

        private string mABT;
        private string mAVTTH;
        private string tENVTTH;
        private int sLVTTH;
        private string tINHTRANG;

        public string MABT { get => mABT; set => mABT = value; }
        public string MAVTTH { get => mAVTTH; set => mAVTTH = value; }
        public string TENVTTH { get => tENVTTH; set => tENVTTH = value; }
        public int SLVTTH { get => sLVTTH; set => sLVTTH = value; }
        public string TINHTRANG { get => tINHTRANG; set => tINHTRANG = value; }
    }
}
