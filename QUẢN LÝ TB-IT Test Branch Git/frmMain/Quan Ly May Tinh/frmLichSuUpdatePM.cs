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
using DAO;
using DTO;

namespace frmMain.Quan_Ly_May_Tinh
{
    public partial class frmLichSuUpdatePM : DevExpress.XtraEditors.XtraForm
    {
        public frmLichSuUpdatePM()
        {
            InitializeComponent();
            LoadControl();
        }

        private void LoadControl()
        {
            LoadData();
        }

        private void LoadData()
        {
            gridControl1.DataSource = LsUpdatePMDAO.Instance.GetLsThongTinCN();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
                
                // Xóa này lại tác động cả lại trong bảng quản lý cài đặt.
           
                int dem = 0;
                List<int> LsID = new List<int>();
                foreach (var item in gridView1.GetSelectedRows())
                {
                    string ID = gridView1.GetRowCellValue(item, "ID").ToString();
                    int ID1 =int.Parse(ID);
                    LsID.Add(ID1);
                    dem++;
                }

                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} thông tin cập nhật phần mềm được chọn được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    foreach (int item in LsID)
                    {
                        LsUpdatePMDTO a = LsUpdatePMDAO.Instance.GetUpdateDTO(item);
                        LsUpdatePMDAO.Instance.DeleteLanCN(item);

                        // Xóa trong bảng chi tiết cài đặt phần mềm: DSCAIDAT

                        DsCaiDatDAO.Instance.DeletePMCD(a.MAPM,a.NGAYCN);
                    
                    }
                    MessageBox.Show($" Đã xóa {dem} thông tin cập nhật được chọn.", "THÀNH CÔNG!");
                }
                LoadControl();                         
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

    }
}