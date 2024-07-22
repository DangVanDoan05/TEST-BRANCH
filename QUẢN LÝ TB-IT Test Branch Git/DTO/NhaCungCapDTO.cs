using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhaCungCapDTO
    {
        public NhaCungCapDTO(DataRow row)
        {
            this.ID = int.Parse(row["ID"].ToString());
            this.MANCC = row["MANCC"].ToString();
            this.TENNCC = row["TENNCC"].ToString();
            this.DIACHI = row["DIACHI"].ToString();
            this.DIENTHOAI = row["DIENTHOAI"].ToString();
            this.WEBSITE = row["WEBSITE"].ToString();
        }


        private int iD;
        private string mANCC;
        private string tENNCC;
        private string dIACHI;
        private string dIENTHOAI;
        private string wEBSITE;

        public int ID { get => iD; set => iD = value; }
        public string MANCC { get => mANCC; set => mANCC = value; }
        public string TENNCC { get => tENNCC; set => tENNCC = value; }
        public string DIACHI { get => dIACHI; set => dIACHI = value; }
        public string DIENTHOAI { get => dIENTHOAI; set => dIENTHOAI = value; }
        public string WEBSITE { get => wEBSITE; set => wEBSITE = value; }
    }
}
