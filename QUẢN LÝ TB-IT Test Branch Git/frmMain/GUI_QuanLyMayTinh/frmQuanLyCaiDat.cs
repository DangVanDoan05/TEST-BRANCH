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
    public partial class frmQuanLyCaiDat : DevExpress.XtraEditors.XtraForm
    {
        public frmQuanLyCaiDat()
        {
            InitializeComponent();
            loadControl();
        }

        private void loadControl()
        {
            gridControl1.DataSource = DanhSachCaiDatDAO.Instance.GetTable();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maMT = "";
            foreach (var item in gridView1.GetSelectedRows())
            {
                string ma1 = gridView1.GetRowCellValue(item, "MAMT").ToString();
                DanhSachMayTinhDTO dsmt = DanhSachMayTinhDAO.Instance.GetMaMT(ma1);
               
                maMT = ma1;
            }
            if(maMT=="")
            {
                MessageBox.Show("Bạn chưa chọn thông tin để xóa.", "Thông Báo:");
            }
            else
            {

                DialogResult kq = MessageBox.Show($"Bạn muốn xóa thông tin cài phần mềm cho mã máy tính {maMT} ", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(kq==DialogResult.Yes)
                { 
                    DanhSachCaiDatDTO dto = DanhSachCaiDatDAO.Instance.GetMaMT(maMT);
                    DanhSachCaiDatDAO.Instance.Delete(maMT, dto.MAPM);

                loadControl();
                    MessageBox.Show("Xóa thông tin thành công!");
                }


            }

        }
    }
}