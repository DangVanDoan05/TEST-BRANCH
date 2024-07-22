using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QuanLyThietBiIT.Common;
using QuanLyThietBiIT.DAOIT;
using QuanLyThietBiIT.DTOIT;
using QuanLyThietBiIT.GridViewEdit;

namespace QuanLyThietBiIT.GUI_QuanLyMayTinh
{
    public partial class frmChiTietCaiDat : DevExpress.XtraEditors.XtraForm
    {
        public frmChiTietCaiDat()
        {
            InitializeComponent();
            LoadControl();
        }

        private void LoadControl()
        {
            gridControl1.DataSource = DanhSachMayTinhDAO.Instance.GetListMaMT();
            gridControl2.DataSource = DanhSachPhanMemDAO.Instance.GetListMaMT();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ngaycaidat = dtpNgayCaiDat.Value.ToString("dd/MM/yyyy");
            // string maMT = gridView1.GetFocusedRowCellValue("MaMT").ToString();
           
            List<DanhSachMayTinhDTO> ListMaMT = new List<DanhSachMayTinhDTO>();
            string maMT = "";
            foreach (var item in gridView1.GetSelectedRows())
            {
                string ma1 = gridView1.GetRowCellValue(item, "MAMT").ToString();
                DanhSachMayTinhDTO dsmt = DanhSachMayTinhDAO.Instance.GetMaMT(ma1);
                ListMaMT.Add(dsmt);
                maMT = ma1;
            }

       





            List<string> ListMaPM = new List<string>();
            foreach (var item in gridView2.GetSelectedRows())
            {
                string ma1 = gridView2.GetRowCellValue(item, "MAPM").ToString();
                ListMaPM.Add(ma1);
            }

           
            DialogResult kq = MessageBox.Show($"Bạn muốn lưu danh sách phần mềm được cài đặt cho máy tính {maMT}?", "Thông Báo: ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(kq==DialogResult.Yes)
            {
                foreach (string item in ListMaPM)
                {
                    DanhSachPhanMemDTO MaPMDTO = DanhSachPhanMemDAO.Instance.GetMaPM(item);
                    DanhSachCaiDatDAO.Instance.Insert(maMT, item, MaPMDTO.TENPM, ngaycaidat);
                   
                }
                MessageBox.Show("Lưu thông tin thành công!", "Thông Báo:");
            }


        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

    }
}