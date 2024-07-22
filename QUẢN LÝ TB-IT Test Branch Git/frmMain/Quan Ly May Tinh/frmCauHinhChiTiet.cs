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

namespace frmMain
{
    public partial class frmCauHinhChiTiet : DevExpress.XtraEditors.XtraForm
    {
        public frmCauHinhChiTiet()
        {
            InitializeComponent();
            LoadControl();
        }
        bool them;
        private void LoadControl()
        {
            LockControl(true);
            LoadData();
            CleanText();
        }

        private void LoadData()
        {
            sglMaMT.Properties.DataSource =QuanLyMayTinhDAO.Instance.GetListMaMT();
            sglMaMT.Properties.DisplayMember = "MAMT";
            sglMaMT.Properties.ValueMember = "MAMT";

            gridControl1.DataSource = ChiTietCauHinhDAO.Instance.GetTable();
        }


        private void CleanText()
        {          
            txtPhongBan.Clear();
            txtCPU.Clear();
            txtMainBoard.Clear();
            txtNguon.Clear();
            txtRAM.Clear();
            txtROM.Clear();
            txtTanCASE.Clear();
            txtTanCPU.Clear();
            txtVGA.Clear();
            txtVoCase.Clear();
        }

        private void LockControl(bool kt)
        {
           if(kt)
            {
                sglMaMT.Enabled = false;
                txtPhongBan.Enabled = false;
                txtCPU.Enabled = false;
                txtMainBoard.Enabled = false;
                txtNguon.Enabled = false;
                txtRAM.Enabled = false;
                txtROM.Enabled = false;
                txtTanCASE.Enabled = false;
                txtTanCPU.Enabled = false;
                txtVGA.Enabled = false;
                txtVoCase.Enabled = false;
                btnCapNhatCH.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
            }
           else
            {
                sglMaMT.Enabled = true;
                txtPhongBan.Enabled = false;
                txtCPU.Enabled = true;
                txtMainBoard.Enabled = true;
                txtNguon.Enabled = true;
                txtRAM.Enabled = true;
                txtROM.Enabled = true;
                txtTanCASE.Enabled = true;
                txtTanCPU.Enabled = true;
                txtVGA.Enabled = true;
                txtVoCase.Enabled = true;
                btnCapNhatCH.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                sglMaMT.Text = gridView1.GetFocusedRowCellValue("MAMT").ToString();
                txtPhongBan.Text = gridView1.GetFocusedRowCellValue("PHONGBAN").ToString();
                txtCPU.Text = gridView1.GetFocusedRowCellValue("CHIPCPU").ToString();
                txtRAM.Text = gridView1.GetFocusedRowCellValue("RAM").ToString();
                txtROM.Text = gridView1.GetFocusedRowCellValue("ROM").ToString();
                txtMainBoard.Text = gridView1.GetFocusedRowCellValue("MAINBOARD").ToString();
                txtVGA.Text = gridView1.GetFocusedRowCellValue("VGA").ToString();
                txtTanCASE.Text = gridView1.GetFocusedRowCellValue("TANCASE").ToString();
                txtTanCPU.Text = gridView1.GetFocusedRowCellValue("TANCPU").ToString();
                txtNguon.Text = gridView1.GetFocusedRowCellValue("NGUON").ToString();
                txtVoCase.Text = gridView1.GetFocusedRowCellValue("VOCASE").ToString();
            }
            catch
            {
              
            }

        }
      
        private void btnCapNhatCH_Click(object sender, EventArgs e)
        {
            LockControl(false);
           
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maMT = sglMaMT.EditValue.ToString();
            string phongban = txtPhongBan.Text;
            string CPU = txtCPU.Text;
            string RAM = txtRAM.Text;
            string ROM = txtROM.Text;
            string MainBoard=txtMainBoard.Text;
            string VGA= txtVGA.Text;
            string TanCase= txtTanCASE.Text;
            string TANCPU= txtTanCPU.Text;
            string Nguon= txtNguon.Text;
            string VoCase= txtVoCase.Text;
                                    
           if(them)
            {
                bool CheckExist = ChiTietCauHinhDAO.Instance.CheckExist(maMT);
                if(!CheckExist)
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn cập nhật cấu hình mã máy {maMT} ?", "Thông Báo: ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(kq == DialogResult.Yes)
                    {
                        ChiTietCauHinhDAO.Instance.Insert(maMT, phongban, CPU, RAM, ROM, MainBoard, VGA, TanCase, TANCPU, Nguon, VoCase);
                        MessageBox.Show("Cập nhật thông tin thành công!", "Thông Báo:");
                    }
                   
                }
                else
                {
                    MessageBox.Show("Cấu hình máy tính đã tồn tại.", "Lỗi:",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                them = false;
                LoadControl();
            }
           else
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn lưu thay đổi cấu hình mã máy {maMT} ?", "Thông Báo: ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    ChiTietCauHinhDAO.Instance.Update(maMT, CPU, RAM, ROM, MainBoard, VGA, TanCase, TANCPU, Nguon, VoCase);
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông Báo:");
                }
                LoadControl();
            }
               

          

        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void sglMaMT_EditValueChanged(object sender, EventArgs e)
        {
            string maMT = sglMaMT.EditValue.ToString();
            QuanLyMayTinhDTO a = QuanLyMayTinhDAO.Instance.GetMaMT(maMT);
            txtPhongBan.Text = a.PB;
        }

        private void btnCapNhat_Click_1(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {                         
                // cho phép xóa nhiều dòng trong gridview
                int dem = 0;

                //  int demloi = 0;
                List<string> LsMaMTdcChon = new List<string>();
                foreach (var item in gridView1.GetSelectedRows())
                {                   
                    string MaMT = gridView1.GetRowCellValue(item, "MAMT").ToString();                  
                    LsMaMTdcChon.Add(MaMT);
                    dem++;
                }

                if (dem > 0)
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} cấu hình được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {                   
                        foreach (string item in LsMaMTdcChon)
                        {                         
                           ChiTietCauHinhDAO.Instance.Delete(item);                                                        
                        }                       
                        MessageBox.Show($"Đã xóa {dem} cấu hình máy tính được chọn. ", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);                      
                        dem = 0;
                    }
                    LoadControl();
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn cấu hình để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
          
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
          
            int dem = 0;

            //  int demloi = 0;
            List<string> LsMaMTdcChon = new List<string>();
            foreach (var item in gridView1.GetSelectedRows())
            {
                string MaMT = gridView1.GetRowCellValue(item, "MAMT").ToString();
                LsMaMTdcChon.Add(MaMT);
                dem++;
            }
            if(dem!=1)
            {
                MessageBox.Show("Chưa chọn máy tính hoặc chọn quá nhiều mã máy tính để sửa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LockControl(false);
                sglMaMT.Enabled = false;
            }


        }
    }
}