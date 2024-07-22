using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NgayYcDTO
    {
        public NgayYcDTO(DataRow row)
        {
            this.NGAYYC =Convert.ToDateTime(row["NGAYYC"].ToString());         
        }

        private DateTime nGAYYC;

        public DateTime NGAYYC { get => nGAYYC; set => nGAYYC = value; }

    }
}
