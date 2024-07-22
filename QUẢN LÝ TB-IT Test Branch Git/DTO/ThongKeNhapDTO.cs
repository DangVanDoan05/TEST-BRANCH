using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongKeNhapDTO
    {

        public ThongKeNhapDTO(DataRow row)
        {
            this.MATKNHAP = row["MATKNHAP"].ToString();
            this.MALK = row["MALK"].ToString();
            this.TENLK = row["TENLK"].ToString();
            this.NGAYNHAP = row["NGAYNHAP"].ToString();
            this.SLNHAP = int.Parse(row["SLNHAP"].ToString());
            this.DVTINH = row["DVTINH"].ToString();
            this.NCC = row["NCC"].ToString();
            this.NGUOINHAP = row["NGUOINHAP"].ToString();
            this.GHICHU = row["GHICHU"].ToString();
            this.IDTTKIEMKE =int.Parse(row["IDTTKIEMKE"].ToString());
            this.CHITIETTTKK = row["CHITIETTTKK"].ToString();
        }


        private string mATKNHAP;
        private string mALK;
        private string tENLK;
        private string nGAYNHAP;
        private int sLNHAP;
        private string dVTINH;
        private string nCC;
        private string nGUOINHAP;
        private string gHICHU;
        private int iDTTKIEMKE;
        private string cHITIETTTKK;


        // ,NGUOINHAP,GHICHU,IDTTKIEMKE,CHITIETTTKK

        public string MALK { get => mALK; set => mALK = value; }
        public string TENLK { get => tENLK; set => tENLK = value; }
        public string NGAYNHAP { get => nGAYNHAP; set => nGAYNHAP = value; }
        public int SLNHAP { get => sLNHAP; set => sLNHAP = value; }
        public string DVTINH { get => dVTINH; set => dVTINH = value; }     
        public string NGUOINHAP { get => nGUOINHAP; set => nGUOINHAP = value; }
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
        public string MATKNHAP { get => mATKNHAP; set => mATKNHAP = value; }
        public string NCC { get => nCC; set => nCC = value; }      
        public string CHITIETTTKK { get => cHITIETTTKK; set => cHITIETTTKK = value; }
        public int IDTTKIEMKE { get => iDTTKIEMKE; set => iDTTKIEMKE = value; }
    }
}
