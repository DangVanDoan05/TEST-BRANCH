using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class QlyYcSDvaTaiPMDTO
    {

        //   insert QLYCSDVATAIPM(MAYCKT, PHONGBAN, NGAYYC, MAPM, TENPM, PHIENBANPM, CHUCNANG, MDSD, MTCAIDAT, NGLAPYC, PDPB, PDIT, HTYC)
        public QlyYcSDvaTaiPMDTO(DataRow row)
        {
            this.MAYCKT = row["MAYCKT"].ToString();          
            this.PHONGBAN = row["PHONGBAN"].ToString();
            this.NGAYYC = row["NGAYYC"].ToString();
            this.MAPM = row["MAPM"].ToString();
            this.TENPM = row["TENPM"].ToString();
            this.PHIENBANPM = row["PHIENBANPM"].ToString();
            this.CHUCNANG = row["CHUCNANG"].ToString();
            this.MDSD = row["MDSD"].ToString(); 
            this.MTCAIDAT = row["MTCAIDAT"].ToString();          
            this.NGLAPYC = row["NGLAPYC"].ToString();
            this.PDPB = row["PDPB"].ToString();
            this.PDIT = row["PDIT"].ToString();
            this.HTYC = row["HTYC"].ToString();          
        }

    

        private string mAYCKT;      
        private string pHONGBAN;
        private string nGAYYC;
        private string mAPM;
        private string tENPM;
        private string pHIENBANPM;
        private string cHUCNANG;
        private string mDSD;
        private string mTCAIDAT;       
        private string nGLAPYC;
        private string pDPB;
        private string pDIT;
        private string hTYC;
      

        public string MAYCKT { get => mAYCKT; set => mAYCKT = value; }
      
        public string PHONGBAN { get => pHONGBAN; set => pHONGBAN = value; }
        public string NGAYYC { get => nGAYYC; set => nGAYYC = value; }
        public string TENPM { get => tENPM; set => tENPM = value; }
        public string PHIENBANPM { get => pHIENBANPM; set => pHIENBANPM = value; }
        public string CHUCNANG { get => cHUCNANG; set => cHUCNANG = value; }
        public string MDSD { get => mDSD; set => mDSD = value; }
        public string MTCAIDAT { get => mTCAIDAT; set => mTCAIDAT = value; }     
        public string MAPM { get => mAPM; set => mAPM = value; }
        public string NGLAPYC { get => nGLAPYC; set => nGLAPYC = value; }
        public string PDPB { get => pDPB; set => pDPB = value; }
        public string PDIT { get => pDIT; set => pDIT = value; }
        public string HTYC { get => hTYC; set => hTYC = value; }
    }
}
