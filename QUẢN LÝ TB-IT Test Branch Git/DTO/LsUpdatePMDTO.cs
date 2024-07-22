using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LsUpdatePMDTO
    {
        public LsUpdatePMDTO(DataRow row)
        {
            this.ID =int.Parse( row["ID"].ToString());
            this.MAPM = row["MAPM"].ToString();
            this.TENPM = row["TENPM"].ToString();
            this.NGAYCN = row["NGAYCN"].ToString();          
        }


        private int iD;
        private string mAPM;
        private string tENPM;
        private string nGAYCN;

        public string MAPM { get => mAPM; set => mAPM = value; }
        public string TENPM { get => tENPM; set => tENPM = value; }
        public string NGAYCN { get => nGAYCN; set => nGAYCN = value; }
        public int ID { get => iD; set => iD = value; }
    }
}
