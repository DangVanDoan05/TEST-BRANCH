using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LoaiMayTinhDTO
    {
        public LoaiMayTinhDTO(DataRow row)
        {
            this.ID = int.Parse(row["ID"].ToString());
            this.TENLOAIMT = row["TENLOAIMT"].ToString();
            this.GHICHU = row["GHICHU"].ToString();
        }


        private int iD;
        private string tENLOAIMT;
        private string gHICHU;

        public int ID { get => iD; set => iD = value; }
        public string TENLOAIMT { get => tENLOAIMT; set => tENLOAIMT = value; }
        public string GHICHU { get => gHICHU; set => gHICHU = value; }
    }
}
