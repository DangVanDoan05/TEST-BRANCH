using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DTO
{
    public class LoaiTKDTO
    {
        public LoaiTKDTO(DataRow row)
        {
            this.MALOAITK = row["MALOAITK"].ToString();
            this.TENLOAITK = row["TENLOAITK"].ToString();

            this.GHICHU = row["GHICHU"].ToString();
        }
        private string mALOAITK;
        private string tENLOAITK;

        private string gHICHU;

        public string MALOAITK { get => mALOAITK; set => mALOAITK = value; }
        public string TENLOAITK { get => tENLOAITK; set => tENLOAITK = value; }

        public string GHICHU { get => gHICHU; set => gHICHU = value; }
    }
}
