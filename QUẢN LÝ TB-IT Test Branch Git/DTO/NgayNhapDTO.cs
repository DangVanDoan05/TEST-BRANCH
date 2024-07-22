using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NgayNhapDTO
    {

        public NgayNhapDTO(DataRow row)
        {
            this.NGAYNHAP = Convert.ToDateTime(row["NGAYNHAP"].ToString());
        }

        private DateTime nGAYNHAP;

        public DateTime NGAYNHAP { get => nGAYNHAP; set => nGAYNHAP = value; }

    }
}
