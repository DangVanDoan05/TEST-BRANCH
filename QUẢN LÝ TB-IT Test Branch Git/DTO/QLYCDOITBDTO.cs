using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class QLYCDOITBDTO
    {


        // QLYCDOITB(MAYCKT, PB, NGAYYC, TBDOI, SOLUONG, MTSD, LOIHT, VITRISD, PPTT, VDKTT, NGLAPYC, PDPB, PDIT, HTYC)
        public QLYCDOITBDTO(DataRow row)
        {

            this.MAYCKT = row["MAYCKT"].ToString();
            this.PB = row["PB"].ToString();
            this.NGAYYC = row["NGAYYC"].ToString();
            this.TBDOI = row["TBDOI"].ToString();
            this.SOLUONG =int.Parse(row["SOLUONG"].ToString());
            this.MTSD = row["MTSD"].ToString();
            this.LOIHT = row["LOIHT"].ToString();
            this.VITRISD = row["VITRISD"].ToString();          
            this.PPTT = row["PPTT"].ToString();
            this.VDKTT = row["VDKTT"].ToString();
            this.NGLAPYC = row["NGLAPYC"].ToString();          
            this.PDPB = row["PDPB"].ToString();
            this.PDIT = row["PDIT"].ToString();
            this.HTYC = row["HTYC"].ToString();
            
        }

        // QLYCDOITB(MAYCKT, PB, NGAYYC, TBDOI, SOLUONG, MTSD, LOIHT, VITRISD, PPTT, VDKTT, NGLAPYC, PDPB, PDIT, HTYC)

        private string mAYCKT;      
        private string pB;
        private string nGAYYC;
        private string tBDOI;
        private int sOLUONG;
        private string mTSD;
        private string lOIHT;
        private string vITRISD;
        private string pPTT;
        private string vDKTT;
        private string nGLAPYC;
        private string pDPB;
        private string pDIT;
        private string hTYC;
       

        public string MAYCKT { get => mAYCKT; set => mAYCKT = value; }
        public string PB { get => pB; set => pB = value; }
        public string NGAYYC { get => nGAYYC; set => nGAYYC = value; }
        public string TBDOI { get => tBDOI; set => tBDOI = value; }
        public int SOLUONG { get => sOLUONG; set => sOLUONG = value; }
        public string MTSD { get => mTSD; set => mTSD = value; }
        public string LOIHT { get => lOIHT; set => lOIHT = value; }
        public string VITRISD { get => vITRISD; set => vITRISD = value; }
        public string PPTT { get => pPTT; set => pPTT = value; }
        public string VDKTT { get => vDKTT; set => vDKTT = value; }
        public string NGLAPYC { get => nGLAPYC; set => nGLAPYC = value; }
        public string PDPB { get => pDPB; set => pDPB = value; }
        public string PDIT { get => pDIT; set => pDIT = value; }
        public string HTYC { get => hTYC; set => hTYC = value; }
    }
}
