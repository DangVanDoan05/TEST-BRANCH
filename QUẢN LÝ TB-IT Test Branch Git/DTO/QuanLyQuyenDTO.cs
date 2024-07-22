using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
  public  class QuanLyQuyenDTO
    {
        public QuanLyQuyenDTO(DataRow row)
        {
            this.IDQUYEN = int.Parse(row["IDQUYEN"].ToString());
            this.CHITIETQUYEN= row["CHITIETQUYEN"].ToString();
           
        }


        private int iDQUYEN;
        private string cHITIETQUYEN;

        public int IDQUYEN { get => iDQUYEN; set => iDQUYEN = value; }
        public string CHITIETQUYEN { get => cHITIETQUYEN; set => cHITIETQUYEN = value; }
    }
}
