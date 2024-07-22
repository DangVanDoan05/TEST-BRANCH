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
using QuanLyThietBiIT.GridViewEdit;

namespace QuanLyThietBiIT
{
    public partial class frmPhongBan : DevExpress.XtraEditors.XtraForm
    {
        public frmPhongBan()
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
            gridControl1.DataSource = PhongBanDAO.Instance.GetTable();
        }

        private void LockControl(bool kt)
        {
           if(kt==true)
            {
                txtMaPB.Enabled = false;
                txtTenPB.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
                btnHuy.Enabled = false;
            }
           else
            {
                txtMaPB.Enabled = true;
                txtTenPB.Enabled = true;
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = false;
                btnHuy.Enabled = true;
            }
        }
        private void CleanText()
        {
            txtMaPB.Clear();
            txtTenPB.Clear();
        }
        private int CheckThem(string MaPB)
        {
            DataTable data = PhongBanDAO.Instance.GetRow_of_Table(MaPB);
            int socot = data.Rows.Count;
            return socot;
        }
        void Save()
        {
            if(them)
            {
                string MaPB = txtMaPB.Text.Trim();
                string TenPB = txtTenPB.Text.Trim();
                int socot = CheckThem(MaPB);
                if (socot > 0)
                {
                    MessageBox.Show(" Mã Phòng Ban đã tồn tại ", " Thông Báo");
                }
                else
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn thêm Phòng ban: {MaPB}","Thông Báo",MessageBoxButtons.YesNo);
                    if (kq == DialogResult.Yes)

                    {
                        PhongBanDAO.Instance.InsertTable(MaPB,TenPB);
                        MessageBox.Show($"Thêm thành công Phòng ban:{MaPB} ", "Thông Báo");
                        them = false;
                       
                    }
                }
            }
            else
            {
                string MaPB = txtMaPB.Text.Trim();
                string TenPB = txtTenPB.Text.Trim();
                if (MaPB == "")
                {
                    MessageBox.Show($"Bạn chưa chọn phòng ban để sửa thông tin. ", "Thông Báo:");
                }
                else
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn sửa thông tin phòng ban: {MaPB}", "Thông Báo", MessageBoxButtons.YesNo);
                    if (kq == DialogResult.Yes)

                    {
                        PhongBanDAO.Instance.UpdateTable(TenPB, MaPB);
                        MessageBox.Show($"Sửa thành công Phòng ban:{MaPB} ", "Thông Báo");

                        LoadControl();
                    }
                }
            }
        }
       

        private void gridControl1_Click(object sender, EventArgs e)
        {
            txtMaPB.Text = gridView1.GetFocusedRowCellValue("MAPB").ToString();
            txtTenPB.Text = gridView1.GetFocusedRowCellValue("TENPB").ToString();
        }
        
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaPB.Enabled = false;
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            LockControl(false);
            txtMaPB.Enabled = false;
            string maPB = txtMaPB.Text.Trim();
            string tenPB = txtTenPB.Text.Trim();
            if (maPB == "")
            {
                MessageBox.Show($" Bạn chưa chọn Mã Phòng để xóa ");
                LoadControl();
            }

            else
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa mã phòng ban: {maPB}", "Thông Báo", MessageBoxButtons.YesNo);
                if (kq == DialogResult.Yes)
                {
                    PhongBanDAO.Instance.DeleteTable(maPB);
                    MessageBox.Show($" Xóa thành công mã phòng: {maPB}", " Thông Báo");
                }
                LoadControl();
            }
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