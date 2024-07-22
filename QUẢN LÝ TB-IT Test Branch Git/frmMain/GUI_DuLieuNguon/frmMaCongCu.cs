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
using QuanLyThietBiIT.DAOIT;
using QuanLyThietBiIT.DTOIT;
using QuanLyThietBiIT.GridViewEdit;

namespace QuanLyThietBiIT
{
    public partial class frmMaCongCu : DevExpress.XtraEditors.XtraForm
    {
        public frmMaCongCu()
        {
            InitializeComponent();
            LoadControl();
        }
        bool them;
      
        private void LoadControl()
        {
            LoadData();
            LockControl(true);
            CleanText();
        }


        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMaCC.Enabled = false;
                txtTenCC.Enabled = false;
                txtDvTinh.Enabled = false;
                txtSoLuong.Enabled = false;
                txtGhichu.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
                btnHuy.Enabled = false;
            }
            else
            {

                txtMaCC.Enabled = true;
                txtTenCC.Enabled = true;
                txtDvTinh.Enabled = true;
                txtSoLuong.Enabled = true;
                txtGhichu.Enabled = true;
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = false;
                btnHuy.Enabled = true;

            }
        }

        private void LoadData()
        {
            LoadCBX();
            gridControl1.DataSource = MaCongCuDAO.Instance.GetTable();
        }

        private void LoadCBX()
        {
            cbNCC.DataSource = NhaCungCapDAO.Instance.GetListNCC();
            cbNCC.DisplayMember = "MANCC";
            cbNCC.ValueMember = "MANCC";
        }
        private void CleanText()
        {
            txtMaCC.Clear();
            txtTenCC.Clear();
            txtDvTinh.Clear();
            txtSoLuong.Clear();
            txtGhichu.Clear();
        }
        void Save()
        {
            if (them)
            {
                try
                {
                    string macc = txtMaCC.Text.Trim();
                    string tencc = txtTenCC.Text.Trim();
                    string dvi = txtDvTinh.Text.Trim();
                    int solg = int.Parse(txtSoLuong.Text);
                    string ncc = cbNCC.SelectedValue.ToString();
                    string ghichu = txtGhichu.Text.Trim();
                    int socot = MaLinhKienDAO.Instance.CheckMaLK(macc);
                    if (socot == 1)
                    {
                        MessageBox.Show(" Mã công cụ đã tồn tại!", "Thông Báo");
                    }
                    else
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn thêm mã công cụ {macc}", "Thông Báo:", MessageBoxButtons.YesNo);
                        if (kq == DialogResult.Yes)
                        {
                            MaCongCuDAO.Instance.Insert(macc, tencc, solg, dvi, ncc, ghichu);
                            MessageBox.Show($" Thêm mã công cụ {macc} thành công! ");
                        }
                        them = false;
                        LoadControl();

                    }
                }
                catch
                {
                    MessageBox.Show("Hãy nhập số lượng hoặc kiểm tra lại phần nhà cung cấp!", "Thông Báo:");
                }
            }
            else
            {
                string macc = txtMaCC.Text.Trim();
                string tencc = txtTenCC.Text.Trim();
                string dvi = txtDvTinh.Text.Trim();
                int solg = int.Parse(txtSoLuong.Text);
                string ncc = cbNCC.SelectedValue.ToString();
                string ghichu = txtGhichu.Text.Trim();
                if (macc =="")
                {
                    MessageBox.Show("Bạn chưa chọn Mã Công cụ để sửa! ", "Thông Báo:");
                }
                else
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn sửa thông tin của Mã công cụ {macc}", "Thông Báo:", MessageBoxButtons.YesNo);
                    if (kq == DialogResult.Yes)
                    {
                        MaCongCuDAO.Instance.Update(macc, tencc, solg, dvi, ncc, ghichu);
                        MessageBox.Show($" Sửa thông tin mã công cụ {macc} thành công! ");
                    }
                }



            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Save();
            LoadControl();
        }

     

       

        private void btnXoa_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaCC.Enabled = false;
            string ma = txtMaCC.Text.Trim();
            if (ma == "")
            {
                MessageBox.Show(" Bạn chưa chọn Mã Công cụ để xóa! ", " Thông Báo");
            }
            else
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa mã công cụ {ma} ?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        string ma1 = gridView1.GetRowCellValue(item, "MACC").ToString();
                        MaLinhKienDAO.Instance.DeleteTable(ma1);
                        TonLinhKienDAO.Instance.Delete(ma1);
                        MessageBox.Show($"Xóa thành công mã công cụ: {ma}", "Thông Báo ");
                    }
                    //MaLinhKienDAO.Instance.DeleteTable(ma);
                    //TonLinhKienDAO.Instance.Delete(ma);
                    //MessageBox.Show($"Xóa thành công mã linh kiện: {ma}", "Thông Báo ");
                }
            }
            LoadControl();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            txtMaCC.Text = gridView1.GetFocusedRowCellValue("MACC").ToString();
            txtTenCC.Text = gridView1.GetFocusedRowCellValue("TENCC").ToString();
            txtDvTinh.Text = gridView1.GetFocusedRowCellValue("DVTINH").ToString();
            txtGhichu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
           
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaCC.Enabled = false;
        }

      
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaCC.Enabled = false;
        }

        private void btnXoa_Click_2(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaCC.Enabled = false;
            string ma = txtMaCC.Text.Trim();
            if (ma == "")
            {
                MessageBox.Show(" Bạn chưa chọn Mã Công cụ để xóa! ", " Thông Báo");
            }
            else
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa mã công cụ {ma} ?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        string ma1 = gridView1.GetRowCellValue(item, "MACC").ToString();
                        MaLinhKienDAO.Instance.DeleteTable(ma1);
                        TonLinhKienDAO.Instance.Delete(ma1);
                        MessageBox.Show($"Xóa thành công mã công cụ: {ma}", "Thông Báo ");
                    }
                    //MaLinhKienDAO.Instance.DeleteTable(ma);
                    //TonLinhKienDAO.Instance.Delete(ma);
                    //MessageBox.Show($"Xóa thành công mã linh kiện: {ma}", "Thông Báo ");
                }
            }
            LoadControl();
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            Save();
            LoadControl();
        }

        private void btnCapNhat_Click_1(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void gridControl1_Click_1(object sender, EventArgs e)
        {
            txtMaCC.Text = gridView1.GetFocusedRowCellValue("MACC").ToString();
            txtTenCC.Text = gridView1.GetFocusedRowCellValue("TENCC").ToString();
            txtSoLuong.Text = gridView1.GetFocusedRowCellValue("SOLUONG").ToString();
            txtDvTinh.Text = gridView1.GetFocusedRowCellValue("DVTINH").ToString();
            txtGhichu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
        }
    }
}
