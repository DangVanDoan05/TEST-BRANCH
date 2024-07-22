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
    public partial class frmMaLinhKien : DevExpress.XtraEditors.XtraForm
    {
        public frmMaLinhKien()
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
                txtMaLk.Enabled = false;
                txtTenLk.Enabled = false;
                txtDvTinh.Enabled = false;
                txtGhiChu.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
               
            }
            else
            {
                txtMaLk.Enabled = true;
                txtTenLk.Enabled = true;
                txtDvTinh.Enabled = true;
                txtGhiChu.Enabled = true;
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
              

            }

        }

        private void LoadData()
        {
            LoadCBX();
            gridControl1.DataSource = MaLinhKienDAO.Instance.GetTable();
        }

        private void LoadCBX()
        {
            cbNCC.DataSource = NhaCungCapDAO.Instance.GetListNCC();
            cbNCC.DisplayMember = "MANCC";
            cbNCC.ValueMember = "MANCC";
        }
        private void CleanText()
        {
            txtMaLk.Clear();
            txtTenLk.Clear();
            txtDvTinh.Clear();
            txtGhiChu.Clear();
        }
        void Save()
        {

            if (them)
            {
                try
                {
                    string malk = txtMaLk.Text.Trim();
                    string tenlk = txtTenLk.Text.Trim();
                    string dvi = txtDvTinh.Text.Trim();
                    string ncc = cbNCC.SelectedValue.ToString();
                    string slmin = (int)(nudMin.Value) + "";
                    string slmax = (int)nudMax.Value + "";
                    string note = txtGhiChu.Text.Trim();                  
                    int socot = MaLinhKienDAO.Instance.CheckMaLK(malk);
                    if (socot == 1)
                    {
                        MessageBox.Show(" Mã linh kiện đã tồn tại!", "Thông báo:");
                    }
                    else
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn thêm mã linh kiện {malk} ", "Thông báo: ", MessageBoxButtons.YesNo);
                        if (kq == DialogResult.Yes)
                        {
                            MaLinhKienDAO.Instance.InsertTable(malk, tenlk, dvi, ncc, slmin, slmax, note);                        
                            MessageBox.Show($" Thêm mã linh kiện {malk} thành công! ","Thông báo:");
                        }
                        them = false;
                        LoadControl();
                    }
                }
                catch
                {
                    MessageBox.Show("Hãy chọn nhà cung cấp trong danh sách!", "Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

            }
            else
            {
                // UPDATE THONG TIN
                string malk = txtMaLk.Text.Trim();
                string tenlk = txtTenLk.Text.Trim();
                string dvi = txtDvTinh.Text.Trim();
                string ncc = cbNCC.SelectedValue.ToString();
                string slmin = (int)(nudMin.Value) + "";
                string slmax = (int)nudMax.Value + "";
                string note = txtGhiChu.Text.Trim();               
                if (malk == "")
                {
                    MessageBox.Show("Bạn chưa chọn mã linh kiện để sửa! ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn sửa thông tin của Mã linh kiện {malk}", "Thông Báo:", MessageBoxButtons.YesNo);
                    if (kq == DialogResult.Yes)
                    {
                        MaLinhKienDAO.Instance.UpdateTable(malk, tenlk, dvi, ncc, slmin, slmax, note);

                        TonLinhKienDAO.Instance.UpdateTenLKDviTinh(malk, tenlk, dvi); // Cứ để đây cũng đc.

                        MessageBox.Show($" Sửa thông tin mã linh kiện {malk} thành công! ","Thông báo:");
                    }
                }               
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            txtMaLk.Text = gridView1.GetFocusedRowCellValue("MALK").ToString();
            txtTenLk.Text = gridView1.GetFocusedRowCellValue("TENLK").ToString();
            txtDvTinh.Text = gridView1.GetFocusedRowCellValue("DVTINH").ToString();
            txtGhiChu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
            cbNCC.SelectedValue = gridView1.GetFocusedRowCellValue("NCC").ToString();
            nudMin.Value = Convert.ToInt32(gridView1.GetFocusedRowCellValue("SLMIN").ToString());
            nudMax.Value = Convert.ToInt32(gridView1.GetFocusedRowCellValue("SLMAX").ToString());
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaLk.Enabled = false;
            string ma = txtMaLk.Text.Trim();
            if (ma == "")
            {
                MessageBox.Show(" Bạn chưa chọn Mã linh kiện để xóa! ", " Thông Báo");
            }
            else
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa mã linh kiện được chọn ?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    int dem = 0;
                    // cho phép xóa nhiều dòng trong gridview
                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        string ma1 = gridView1.GetRowCellValue(item, "MALK").ToString();
                        MaLinhKienDAO.Instance.DeleteTable(ma1);
                        TonLinhKienDAO.Instance.Delete(ma1);
                        dem++;
                       
                    }
                    MessageBox.Show($"Xóa thành công {dem} mã linh kiện được chọn.", "Thông Báo: ");
                    //MaLinhKienDAO.Instance.DeleteTable(ma);
                    //TonLinhKienDAO.Instance.Delete(ma);
                    //MessageBox.Show($"Xóa thành công mã linh kiện: {ma}", "Thông Báo ");
                }
            }
            LoadControl();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaLk.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Save();
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
    }
}