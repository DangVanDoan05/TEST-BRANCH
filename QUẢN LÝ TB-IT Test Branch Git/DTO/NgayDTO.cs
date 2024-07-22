using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NgayDTO
    {
        public NgayDTO(DataRow row)
        {
            this.NGAY = Convert.ToDateTime(row["NGAY"].ToString());
        }

        private DateTime nGAY;

        public DateTime NGAY { get => nGAY; set => nGAY = value; }
    }

}
