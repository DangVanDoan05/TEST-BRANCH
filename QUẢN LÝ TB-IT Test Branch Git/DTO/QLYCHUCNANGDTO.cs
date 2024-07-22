using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class QLYCHUCNANGDTO
    {
        public QLYCHUCNANGDTO(DataRow row)
        {
            this.ID = row["ID"].ToString();
            this.IDPARENT = row["IDPARENT"].ToString();
            this.MOTA = row["MOTA"].ToString();
        }
      
        
        private string iD;
        private string iDPARENT;
        private string mOTA;

        public string ID { get => iD; set => iD = value; }
     
        public string MOTA { get => mOTA; set => mOTA = value; }
        public string IDPARENT { get => iDPARENT; set => iDPARENT = value; }
    }
}
