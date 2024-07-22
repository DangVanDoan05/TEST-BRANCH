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
    public partial class frmPhongBan : DevExpress.XtraEditors.XtraForm
    {
        public frmPhongBan()
        {
            InitializeComponent();
            LoadControl();
        }

        bool them;
        int idquyen = CommonUser.Quyen;

        //QLCC trở lên mới có quyền tác động vào phòng ban.

        private void LoadControl()
        {
            LockControl(true);
            LoadData();
            CleanText();
        }
        void CleanText()
        {
            txtMaPB.Clear();
            txtTenPB.Clear();
            txtGhiChu.Clear();
        }

        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMaPB.Enabled = false;
                txtTenPB.Enabled = false;
                sglNhaMay.Enabled = false;
                chkThuocBoPhan.Enabled = false;
                sglBoPhan.Enabled = false;
                txtGhiChu.Enabled = false;

                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
            }
            else
            {
                txtMaPB.Enabled = true;
                txtTenPB.Enabled = true;
                sglNhaMay.Enabled = true;
                if (idquyen >= 7)   // Tổng giám đốc trở lên mới có quyền chọn bộ phận
                {
                    chkThuocBoPhan.Enabled = true;
                }
                else
                {
                    chkThuocBoPhan.Enabled = false;
                }

                sglBoPhan.Enabled = false;
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
            //if (idquyen >= 7) // Tổng giám đốc + ADMIN
            //{
                sglNhaMay.Properties.DataSource = NHAMAYDAO.Instance.GetTable();
                sglNhaMay.Properties.DisplayMember = "MANHAMAY";
                sglNhaMay.Properties.ValueMember = "MANHAMAY";

                chkThuocBoPhan.Checked = false;


                gridControl1.DataSource = PhongBanDAO.Instance.GetTable();

            //}
            //else
            //{
            //    if (idquyen >= 6) // quản lý cao cấp 
            //    {
            //        chkThuocBoPhan.Checked = false;
            //        sglBoPhan.Properties.DataSource = BoPhanDAO.Instance.GetTable();
            //        sglBoPhan.Properties.DisplayMember = "MABOPHAN";
            //        sglBoPhan.Properties.ValueMember = "MABOPHAN";
            //        sglBoPhan.EditValue = CommonUser.UserStatic.BOPHAN;
            //        gridControl1.DataSource = PhongBanDAO.Instance.GetLsvPbThuocBP(CommonUser.UserStatic.BOPHAN);
            //    }
            //}

        }
        void Save()
        {
            if (them)
            {
                string maPB = txtMaPB.Text.Trim();
                string tenBP = txtTenPB.Text.Trim();
                string ghichu = txtGhiChu.Text.Trim();
                try
                {
                    string NhaMay = sglNhaMay.EditValue.ToString();
                    if (maPB != "")
                    {
                        string thuocBP = "";
                        if (chkThuocBoPhan.Checked)
                        {
                            try
                            {
                                thuocBP = sglBoPhan.EditValue.ToString();
                            }
                            catch
                            {
                                thuocBP = "";
                            }
                        }
                        else
                        {
                            //if (idquyen >= 6)
                            //{
                            //    if (idquyen == 6)
                            //    {
                            //        thuocBP = CommonUser.UserStatic.BOPHAN;
                            //    }
                            //    else
                            //    {
                            //        thuocBP = ""; // 7,8 thì không thuộc bộ phận nữa
                            //    }
                            //}

                        }

                        bool CheckPBExits = PhongBanDAO.Instance.CheckPBExist(maPB,NhaMay,thuocBP);

                        if (CheckPBExits)
                        {
                            MessageBox.Show($" Mã phòng ban {maPB} đã tồn tại ở bộ phận {thuocBP} trong nhà máy {NhaMay}.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {                         
                            PhongBanDAO.Instance.Insert(maPB, tenBP, ghichu,NhaMay,thuocBP);
                            MessageBox.Show($"Đã thêm mã phòng ban: {maPB} thuộc bộ phận {thuocBP} trong nhà máy {NhaMay}. ", "Thành công!");                           
                        }                     
                    }
                    else
                    {
                        MessageBox.Show($" Mã phòng ban không được phép để trống.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch 
                {
                    MessageBox.Show($"Hãy chọn nhà máy trong danh sách. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                them = false;

            }
            else   // Sửa thông tin
            {
                string maPB = txtMaPB.Text.Trim();
                string tenBP = txtTenPB.Text.Trim();
                string ghichu = txtGhiChu.Text.Trim();
                string nhamay = sglNhaMay.EditValue.ToString();
                string bophan = sglBoPhan.EditValue.ToString();
              
                if (maPB!="")  
                {                   
                     PhongBanDAO.Instance.Update(maPB, tenBP, ghichu,nhamay,bophan);
                     MessageBox.Show($" Đã sửa thông tin mã phòng ban {maPB}. ", "THÀNH CÔNG: ");                                         
                }
                else                
                {
                    MessageBox.Show($"Chưa chọn phòng ban muốn sửa thông tin.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

     

      

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(idquyen>=6)
            {
                LockControl(false);
                them = true;
            }
            else
            {
                MessageBox.Show($" Bạn không đủ thẩm quyền cho chức năng này. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (idquyen >= 6)
            {
                LockControl(false);
                txtMaPB.Enabled = false;
                sglNhaMay.Enabled = false;
                chkThuocBoPhan.Enabled = false;
                sglBoPhan.Enabled = false;
            }
            else
            {
                MessageBox.Show($" Bạn không đủ thẩm quyền cho chức năng này. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (idquyen >= 6)
            {
                    
                    int dem = 0;
                    List<PhongBanDTO> LsPBdcChon = new List<PhongBanDTO>();

                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        string maPB = gridView1.GetRowCellValue(item, "MAPB").ToString();
                        string NhaMay = gridView1.GetRowCellValue(item, "NHAMAY").ToString();
                        string BoPhan = gridView1.GetRowCellValue(item, "BOPHAN").ToString();
                        PhongBanDTO a = PhongBanDAO.Instance.GetPBDTO(maPB, NhaMay, BoPhan);
                        LsPBdcChon.Add(a);
                        dem++;
                    }
                    DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} mã phòng ban được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                        foreach (PhongBanDTO item in LsPBdcChon)
                        {
                           PhongBanDAO.Instance.Delete(item.MAPB,item.NHAMAY,item.BOPHAN);
                        }
                        MessageBox.Show($" Đã xóa {dem} mã phòng ban.", "THÀNH CÔNG!");
                    }
                
                LoadControl();
            }
            else
            {
                MessageBox.Show($" Bạn không đủ thẩm quyền cho chức năng này. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (idquyen >= 6)
            {
                Save();
                LoadControl();
            }
            else
            {
                MessageBox.Show($" Bạn không đủ thẩm quyền cho chức năng này. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         
        }


        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (idquyen >= 6)
            {
                LoadControl();
            }
            else
            {
                MessageBox.Show($" Bạn không đủ thẩm quyền cho chức năng này. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

            try
            {
                txtMaPB.Text = gridView1.GetFocusedRowCellValue("MAPB").ToString();
                txtTenPB.Text = gridView1.GetFocusedRowCellValue("TENPB").ToString();
                txtGhiChu.Text = gridView1.GetFocusedRowCellValue("GHICHU").ToString();
                sglBoPhan.EditValue = gridView1.GetFocusedRowCellValue("BOPHAN").ToString();
                sglNhaMay.EditValue = gridView1.GetFocusedRowCellValue("NHAMAY").ToString();
            }
            catch
            {

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

        private void chkThuocBoPhan_Click(object sender, EventArgs e)
        {

            if (chkThuocBoPhan.Checked)
            {
                sglBoPhan.Enabled = true;
            }
            else
            {
                sglBoPhan.Enabled = false;
            }
        }

        private void sglNhaMay_EditValueChanged(object sender, EventArgs e)
        {
            string Nhamay = sglNhaMay.EditValue.ToString();          
            sglBoPhan.Properties.DataSource = BoPhanDAO.Instance.GetLsBPdtoOfNM(Nhamay);
            sglBoPhan.Properties.DisplayMember = "MABP";
            sglBoPhan.Properties.ValueMember = "MABP";
        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

       
        
       
        
    }
}