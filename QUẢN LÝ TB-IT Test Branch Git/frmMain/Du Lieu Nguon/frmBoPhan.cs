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

namespace frmMain.Du_Lieu_Nguon
{
    public partial class frmBoPhan : DevExpress.XtraEditors.XtraForm
    {
        public frmBoPhan()
        {
            InitializeComponent();
            LoadControl();
        }

        bool them;
        int idquyen = CommonUser.Quyen;
        private void LoadControl()
        {
            LockControl(true);
            LoadData();
            CleanText();
        }
        void CleanText()
        {
            txtMaBP.Clear();
            txtTenBP.Clear();
            txtGhiChu.Clear();
        }
        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMaBP.Enabled = false;
                txtTenBP.Enabled = false;              
                txtGhiChu.Enabled = false;
                sglNhaMay.Enabled = false;

                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
            }
            else
            {
                txtMaBP.Enabled = true;
                txtTenBP.Enabled = true;                          
                txtGhiChu.Enabled = true;
                sglNhaMay.Enabled = true;

                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
            }
        }

        private void LoadData()
        {
            //if(idquyen>=7)    // Tổng giám đốc mới có quyền xem dữ liệu
            //{

                // Load GridControl

                gridControl1.DataSource = BoPhanDAO.Instance.GetTable();

                // Load Edit Lookup

                sglNhaMay.Properties.DataSource = NHAMAYDAO.Instance.GetLsvNM();
                sglNhaMay.Properties.DisplayMember = "MANHAMAY";
                sglNhaMay.Properties.ValueMember = "MANHAMAY";

           // }
        }
        void Save()
        {
            if (them)
            {
                string maBP = txtMaBP.Text.Trim();
                string tenBP =txtTenBP.Text.Trim();
                string ghichu = txtGhiChu.Text.Trim();
                try
                {
                    string NhaMay = sglNhaMay.EditValue.ToString();
                    if (maBP != "")
                    {
                        bool CheckBPExitsNM = BoPhanDAO.Instance.CheckBPExistNM(maBP,NhaMay);
                        if (CheckBPExitsNM)
                        {
                            MessageBox.Show($" Mã bộ phận {maBP} đã tồn tại ở nhà máy {NhaMay}. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {                                                   
                            BoPhanDAO.Instance.Insert(maBP, tenBP, ghichu,NhaMay);
                            MessageBox.Show($" Đã thêm mã bộ phận {maBP} thuộc nhà máy {NhaMay}. ", "THÀNH CÔNG!");                           
                        }
                      
                    }
                    else
                    {
                        MessageBox.Show($"Mã bộ phận không được phép để trống", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch 
                {
                    MessageBox.Show($"Chọn nhà máy trong danh sách.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                them = false;
            }
            else
            {
                string maBP = txtMaBP.Text.Trim();
                string tenBP = txtTenBP.Text.Trim();
                string ghichu = txtGhiChu.Text.Trim();
                try
                {
                    string NhaMay = sglNhaMay.EditValue.ToString();
                 
                    BoPhanDAO.Instance.Update(maBP, tenBP, ghichu,NhaMay);
                    MessageBox.Show($"Đã sửa thông tin mã bộ phận {maBP}", "THÀNH CÔNG!");                   
                }
                catch 
                {
                    MessageBox.Show($"Chưa chọn nhà máy trong danh sách.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void searchLookUpEdit1View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            //if(idquyen>=7)
            //{
                LockControl(false);
                them = true;
            //}
            //else
            //{
            //    MessageBox.Show($"Bạn không đủ thẩm quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //if (idquyen >= 7)
            //{
                LockControl(false);
                txtMaBP.Enabled = false;
            //}
            //else
            //{
            //    MessageBox.Show($"Bạn không đủ thẩm quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}          
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //if (idquyen >= 7)
            //{
                Save();
                LoadControl();
            //}
            //else
            //{
            //    MessageBox.Show($"Bạn không đủ thẩm quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //if (idquyen >= 7)
            //{             
                    // cho phép xóa nhiều dòng trong gridview
                    int dem = 0;
                    List<BoPhanDTO> LsBPdcChon = new List<BoPhanDTO>();

                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        string MaBP = gridView1.GetRowCellValue(item, "MABP").ToString();
                        string MaNM = gridView1.GetRowCellValue(item, "NHAMAY").ToString();
                        BoPhanDTO a = BoPhanDAO.Instance.GetBoPhanDTO(MaBP, MaNM);                       
                        dem++;
                    }

                    DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} mã bộ phận được chọn. ?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                        foreach (BoPhanDTO item in LsBPdcChon)
                        {
                            BoPhanDAO.Instance.Delete(item.MABP,item.NHAMAY);
                        }
                        MessageBox.Show($" Đã xóa thành công {dem} mã bộ phận.", "THÀNH CÔNG!");
                    }
                
                LoadControl();
            //}
            //else
            //{
            //    MessageBox.Show($"Bạn không đủ thẩm quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
          
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                txtMaBP.Text = gridView1.GetFocusedRowCellValue("MABP").ToString();
                txtTenBP.Text = gridView1.GetFocusedRowCellValue("TENBP").ToString();
                txtGhiChu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
                sglNhaMay.EditValue= gridView1.GetFocusedRowCellValue("NHAMAY").ToString();
            }
            catch
            {
            }
        }

        private void searchLookUpEdit1View_CustomDrawRowIndicator_1(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }
    }

}