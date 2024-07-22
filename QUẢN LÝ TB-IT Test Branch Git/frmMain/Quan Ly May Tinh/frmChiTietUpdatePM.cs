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
    public partial class frmChiTietUpdatePM : DevExpress.XtraEditors.XtraForm
    {

        public frmChiTietUpdatePM()
        {
            InitializeComponent();
            LoadControl();
        }

        string MaPMdangchon = "";

        private void LoadControl()
        {
            MaPMdangchon = "";
            LoadData();
        }

        private void LoadData()
        {
            // Load danh sách phần mềm.
            gcDsPM.DataSource = DanhSachPhanMemDAO.Instance.GetTable();

        }

        private void btnLichSuUpdate_Click(object sender, EventArgs e)
        {
            frmLichSuUpdatePM f = new frmLichSuUpdatePM();
            f.ShowDialog();
            LoadControl();
        }

        private void gcDsPM_Click(object sender, EventArgs e)
        {
            try
            {
                MaPMdangchon= gridView1.GetFocusedRowCellValue("MAPM").ToString();
                // Load bảng những máy tính cài phần mềm trên.
                gcDsMayTinh.DataSource = DsCaiDatDAO.Instance.GetLsMTcaiPM(MaPMdangchon);
            }
            catch
            {


            }
        }

        private void btnYeuCauUpdate_Click(object sender, EventArgs e)
        {
            #region  Thêm thông tin vào bảng "danh sách cài đặt" và bảng " Lịch sử Update".
            string TgYeucau = dtpTgYeuCau.Value.ToString("dd/MM/yyyy HH:mm:ss:tt");
            int DemMT = 0;
            List<string> ListMaMT = new List<string>();
            foreach (var item in gridView1.GetSelectedRows())
            {
                string ma = gridView2.GetRowCellValue(item, "MAMT").ToString();
                DemMT++;
                ListMaMT.Add(ma);
            }

            if (DemMT == 0)
            {
                MessageBox.Show("Chưa chọn máy tính để cập nhật phần mềm.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               
                
                    DialogResult kq = MessageBox.Show($"Bạn muốn cập nhật phần mềm {MaPMdangchon} cài đặt cho {DemMT} máy tính?", "Thông báo: ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                        int dem = 0;
                        DanhSachPhanMemDTO PhanMemDTO = DanhSachPhanMemDAO.Instance.GetMaPM(MaPMdangchon);
                        foreach (string item in ListMaMT)
                        {
                            QuanLyMayTinhDTO MaMTdto = QuanLyMayTinhDAO.Instance.GetMaMT(item);
                           
                            DsCaiDatDAO.Instance.Insert(item, MaMTdto.PB, MaMTdto.NHAMAY, PhanMemDTO.MAPM, PhanMemDTO.TENPM, TgYeucau, "");

                            // Lưu vào bảng chi tiết Update.

                            dem++;
                                                            
                        }
                        MessageBox.Show($"Thêm thời gian yêu cầu thành công.", "Thành công:");
                    }
                
            }

            DemMT = 0;
          
            LoadControl();
            #endregion
        }
    }
}