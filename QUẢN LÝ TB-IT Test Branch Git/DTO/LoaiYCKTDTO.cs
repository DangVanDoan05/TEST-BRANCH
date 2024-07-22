using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{

    public class LoaiYCKTDTO
    {
        public LoaiYCKTDTO(DataRow row)
        {
          
            this.lOAIYCKT = row["LOAIYCKT"].ToString();
            this.GHICHU = row["GHICHU"].ToString();
        }

        private string lOAIYCKT;
        private string gHICHU;

        public string LOAIYCKT { get => lOAIYCKT; set => lOAIYCKT = value; }
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
    }
}
