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
using DevExpress.XtraGrid.Views.Grid;
using frmMain.Quan_Ly_May_Tinh;

namespace frmMain
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
            gcDsMayTinh.DataSource = QuanLyMayTinhDAO.Instance.GetListMaMT();
            gcDsPhanMem.DataSource = DanhSachPhanMemDAO.Instance.GetListMaMT();
        }

       

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }


        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            GridView view = sender as GridView;        
            string MaMT = view.GetRowCellValue(e.RowHandle, view.Columns["MAMT"]).ToString();
            bool CheckCDPM = DsCaiDatDAO.Instance.CheckCDPM(MaMT);
          
           
            if (CheckCDPM)          // Đã cập nhật.
            {
                e.Appearance.BackColor = btnDaCaiPM.Appearance.BackColor;
            }
            else                    // Chưa cập nhật.
            {
                e.Appearance.BackColor = btnChuaCaiPM.Appearance.BackColor;
            }


        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

     

        private void btnLichSuUpdatePM_Click(object sender, EventArgs e)
        {
            frmLichSuUpdatePM f = new frmLichSuUpdatePM();
            f.ShowDialog();
            LoadControl();
        }

      

        private void btnCaiDatPM_Click(object sender, EventArgs e)
        {
            #region  Kiểm tra xem Mã phần mềm đã có trên mã máy tính hay chưa. 
            string ngaycaidat = dtpNgayCaiDat.Value.ToString("dd/MM/yyyy");
            int DemMT = 0;
            List<string> ListMaMT = new List<string>();
            foreach (var item in gridView1.GetSelectedRows())
            {
                string ma = gridView1.GetRowCellValue(item, "MAMT").ToString();
                DemMT++;
                ListMaMT.Add(ma);
            }

            int DemPM = 0;
            List<DanhSachPhanMemDTO> ListMaPM = new List<DanhSachPhanMemDTO>();
            foreach (var item in gridView2.GetSelectedRows())
            {
                string ma1 = gridView2.GetRowCellValue(item, "MAPM").ToString();
                DanhSachPhanMemDTO a = DanhSachPhanMemDAO.Instance.GetMaPM(ma1);
                DemPM++;
                ListMaPM.Add(a);
            }

            if (DemMT == 0)
            {
                MessageBox.Show("Chưa chọn máy tính.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (DemPM == 0)
                {
                    MessageBox.Show("Chưa chọn phần mềm.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn lưu {DemPM} phần mềm cài đặt cho {DemMT} máy tính?", "Thông báo: ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                        int dem = 0;
                        foreach (string item in ListMaMT)
                        {
                            QuanLyMayTinhDTO MaMTdto = QuanLyMayTinhDAO.Instance.GetMaMT(item);
                            foreach (DanhSachPhanMemDTO item3 in ListMaPM)
                            {
                                bool CheckCDPM = DsCaiDatDAO.Instance.CheckPMtrenMT(item, item3.MAPM);
                                if(!CheckCDPM)
                                {
                                    DsCaiDatDAO.Instance.Insert(item, MaMTdto.PB, MaMTdto.NHAMAY, item3.MAPM, item3.TENPM, ngaycaidat,ngaycaidat);
                                    dem++;
                                }
                                                                 
                            }
                        }
                        MessageBox.Show($"Đã lưu {dem} thông tin cài đặt phần mềm.", "Thành công:");
                    }
                }
            }
            DemMT = 0;
            DemPM = 0;
            LoadControl();
            #endregion
        }
    }
}