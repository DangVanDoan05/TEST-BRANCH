using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DsWebDTO
    {
        public DsWebDTO(DataRow row)
        {
            this.MAWEB = row["MAWEB"].ToString();
            this.LINKWEB = row["LINKWEB"].ToString();
            this.GHICHU = row["GHICHU"].ToString();
        }

        // DSWEBSITE(MAWEB,LINKWEB,GHICHU)

        private string mAWEB;
        private string lINKWEB;
        private string gHICHU;

        public string MAWEB { get => mAWEB; set => mAWEB = value; }
        public string LINKWEB { get => lINKWEB; set => lINKWEB = value; }
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
    }
}
