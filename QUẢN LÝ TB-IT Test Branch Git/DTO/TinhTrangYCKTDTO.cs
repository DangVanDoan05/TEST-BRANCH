using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
  public  class TinhTrangYCKTDTO
    {
        public TinhTrangYCKTDTO(DataRow row)
        {
            this.IDTINHTRANG = int.Parse(row["IDTINHTRANG"].ToString());
            this.CHITIETTT = row["CHITIETTT"].ToString();

        }


        private int iDTINHTRANG;
        private string cHITIETTT;

        public int IDTINHTRANG { get => iDTINHTRANG; set => iDTINHTRANG = value; }
        public string CHITIETTT { get => cHITIETTT; set => cHITIETTT = value; }


    }
}
