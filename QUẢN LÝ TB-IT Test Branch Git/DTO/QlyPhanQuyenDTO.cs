using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTO
{
   public class QlyPhanQuyenDTO
    {
        public QlyPhanQuyenDTO(DataRow row)
        {

            this.MANHANVIEN = row["MANHANVIEN"].ToString();
            this.ID = row["ID"].ToString();
            this.IDPARENT =  row["IDPARENT"].ToString();
            this.MOTA =  row["MOTA"].ToString();
            this.IDQUYEN =int.Parse(row["IDQUYEN"].ToString());
            this.CHITIETQUYEN = row["CHITIETQUYEN"].ToString();
        }

        private string mANHANVIEN;
        private string iD;
        private string iDPARENT;
        private string mOTA;
        private int iDQUYEN;
        private string cHITIETQUYEN;

        public string MANHANVIEN { get => mANHANVIEN; set => mANHANVIEN = value; }
        public string ID { get => iD; set => iD = value; }
        public string IDPARENT { get => iDPARENT; set => iDPARENT = value; }
        public string MOTA { get => mOTA; set => mOTA = value; }
        public int IDQUYEN { get => iDQUYEN; set => iDQUYEN = value; }
        public string CHITIETQUYEN { get => cHITIETQUYEN; set => cHITIETQUYEN = value; }
    }
}
