using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongKeXuatDTO
    {
        public ThongKeXuatDTO(DataRow row)
        {
            this.MATKXUAT = row["MATKXUAT"].ToString();
            this.MALK = row["MALK"].ToString();
            this.TENLK = row["TENLK"].ToString();
            this.NGAYXUAT = row["NGAYXUAT"].ToString();
            this.SLXUAT = int.Parse(row["SLXUAT"].ToString());
            this.DVTINH = row["DVTINH"].ToString();
            this.NCC = row["NCC"].ToString();
            this.NGUOIXUAT = row["NGUOIXUAT"].ToString();
            this.YCKTSD = row["YCKTSD"].ToString();
            this.MDSD = row["MDSD"].ToString();
            this.IDTTKIEMKE = int.Parse(row["IDTTKIEMKE"].ToString());
            this.CHITIETTTKK = row["CHITIETTTKK"].ToString();
        }

        // THONGKEXUAT(MATKXUAT, MALK, TENLK, NGAYXUAT, SLXUAT, DVTINH, NCC, NGUOIXUAT, YCKTSD, MDSD,IDTTKIEMKE,CHITIETTTKK)"

        private string mATKXUAT;
        private string mALK;
        private string tENLK;
        private string nGAYXUAT;
        private int sLXUAT;
        private string dVTINH;
        private string nCC;
        private string nGUOIXUAT;
        private string yCKTSD;
        private string mDSD;
        private int iDTTKIEMKE;
        private string cHITIETTTKK;


        public string MALK { get => mALK; set => mALK = value; }
        public string TENLK { get => tENLK; set => tENLK = value; }
        public string NGAYXUAT { get => nGAYXUAT; set => nGAYXUAT = value; }
        public int SLXUAT { get => sLXUAT; set => sLXUAT = value; }
        public string DVTINH { get => dVTINH; set => dVTINH = value; }
        public string NCC { get => nCC; set => nCC = value; }
        public string NGUOIXUAT { get => nGUOIXUAT; set => nGUOIXUAT = value; }
        public string YCKTSD { get => yCKTSD; set => yCKTSD = value; }
        public string MATKXUAT { get => mATKXUAT; set => mATKXUAT = value; }
        public string MDSD { get => mDSD; set => mDSD = value; }
        public int IDTTKIEMKE { get => iDTTKIEMKE; set => iDTTKIEMKE = value; }
        public string CHITIETTTKK { get => cHITIETTTKK; set => cHITIETTTKK = value; }
    }
}
