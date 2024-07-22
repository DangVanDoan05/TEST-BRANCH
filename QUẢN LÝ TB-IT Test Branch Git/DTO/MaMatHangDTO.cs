using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class MaMatHangDTO
    {
        public MaMatHangDTO(DataRow row)
        {
            this.MAMH = row["MAMH"].ToString();
            this.TENMH = row["TENMH"].ToString();
        }

        private string mAMH;
        private string tENMH;

        public string MAMH { get => mAMH; set => mAMH = value; }
        public string TENMH { get => tENMH; set => tENMH = value; }
    }
}
