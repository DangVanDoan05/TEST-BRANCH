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
    public partial class frmNhom : DevExpress.XtraEditors.XtraForm
    {
        public frmNhom()
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
            LoadLookupEdit();
            CleanText();
        }

        private void LoadLookupEdit()
        {
            if (idquyen >= 6) 
            {
                if(idquyen>=7) // Tổng giám đốc + admin
                {
                    sglPhongBan.Properties.DataSource = PhongBanDAO.Instance.GetLsvPB();
                    sglPhongBan.Properties.DisplayMember = "MAPB";
                    sglPhongBan.Properties.ValueMember = "MAPB";
                }
                else  // Quản lý cao cấp
                {
                    sglPhongBan.Properties.DataSource = PhongBanDAO.Instance.GetLsvPbThuocBP(CommonUser.UserStatic.BOPHAN);
                    sglPhongBan.Properties.DisplayMember = "MAPB";
                    sglPhongBan.Properties.ValueMember = "MAPB";
                }             
            }

            else // Quản lý trung cấp trở xuống
            {
                ////  sglPhongBan.Properties.DataSource = PhongBanDAO.Instance.GetPBDTO(CommonUser.UserStatic.PHONGBAN);
                //sglPhongBan.Properties.DataSource = PhongBanDAO.Instance.GetLsvOnePB(CommonUser.UserStatic.PHONGBAN);
                //sglPhongBan.Properties.DisplayMember = "MAPB";
                //sglPhongBan.Properties.ValueMember = "MAPB";
                //sglPhongBan.EditValue = CommonUser.UserStatic.PHONGBAN;
            }

        }

        private void LoadData() 
        {
            
            if (idquyen >= 6)   // Quản lý cao cấp, tổng giám đốc
            {
                if (idquyen >= 7)  // Tổng giám đốc, ADMIN.
                {
                    gridControl2.DataSource = NhomDAO.Instance.GetTable();
                }
                else   // Quản lý cao cấp
                {
                    gridControl2.DataSource = NhomDAO.Instance.GetLsvNhomThuocBP(CommonUser.UserStatic.BOPHAN);
                }
            }
            else // Quản lý trung cấp 
            {
                gridControl2.DataSource = NhomDAO.Instance.GetNhomOfPB(CommonUser.UserStatic.PHONGBAN);               
            }

        }

        private void LockControl(bool kt)
        {
            
            if (kt)
            {
                txtMaNhom.Enabled = false;
                txtTenNhom.Enabled = false;
                txtGhiChu.Enabled = false;
                sglPhongBan.Enabled = false;
                txtBoPhan.Enabled = false;
                txtNhaMay.Enabled = false;

                btnThem.Enabled = true;
              
                btnXoa.Enabled = true;             
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
            }
            else
            {
                txtMaNhom.Enabled = true;
                txtTenNhom.Enabled = true;
                txtGhiChu.Enabled = true;
                sglPhongBan.Enabled = true;
                txtBoPhan.Enabled = false;
                txtNhaMay.Enabled = false;

                btnThem.Enabled = false;
               
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
            }

        }

        private void CleanText()
        {
            txtMaNhom.Clear();
            txtTenNhom.Clear();
            txtGhiChu.Clear();
        }

        void Save()     // **** Thêm theo ID quyền
        {
            if (them)
            {
                string MaNhom = txtMaNhom.Text.Trim();
                string TenNhom = txtTenNhom.Text.Trim();              
                string Ghichu = txtGhiChu.Text.Trim();
                //if(idquyen>=6) // qlcc
                //{
                try
                {
                    string PhongBan = sglPhongBan.EditValue.ToString();
                    string BoPhan = txtBoPhan.Text;
                    string NhaMay = txtNhaMay.Text;

                    bool CheckNhomExist = NhomDAO.Instance.CheckNhomExist(MaNhom,NhaMay,BoPhan,PhongBan);

                    if (CheckNhomExist)
                    {
                        MessageBox.Show($" Mã nhóm đã tồn tại.", " Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {                       
                            NhomDAO.Instance.Insert(MaNhom, TenNhom, Ghichu,NhaMay,BoPhan,PhongBan);
                            MessageBox.Show($"Đã thêm mã nhóm :{MaNhom} vào phòng ban {PhongBan} thuộc bộ phận {BoPhan} của nhà máy {NhaMay}", "THÀNH CÔNG:");
                            them = false;
                            LoadControl();                       
                    }
                }
                catch
                {
                    MessageBox.Show($"Hãy chọn phòng ban trong danh sách.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //}
                //else
                //{

                //        string PhongBan = CommonUser.UserStatic.PHONGBAN;                      
                //        bool CheckExistNhomThuocPB = NhomDAO.Instance.CheckNhomExist(MaNhom);
                //        if (CheckExistNhomThuocPB)
                //        {
                //            MessageBox.Show($" Mã nhóm đã tồn tại ở phòng ban khác.", " Lỗi:" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        }
                //        else
                //        {
                //            DialogResult kq = MessageBox.Show($"Bạn muốn thêm mã nhóm : {MaNhom} .", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //            if (kq == DialogResult.Yes)
                //            {
                //                NhomDAO.Instance.Insert(MaNhom, TenNhom, Ghichu, PhongBan);
                //                MessageBox.Show($"Đã thêm mã nhóm :{MaNhom} .","THÀNH CÔNG:");
                //                them = false;
                //                LoadControl();
                //            }
                //        }


                //}
                them = false;

            }
            else
            {
                string MaNhom = txtMaNhom.Text.Trim();
                string TenNhom = txtTenNhom.Text.Trim();
                string Ghichu = txtGhiChu.Text.Trim();
                string PhongBan = sglPhongBan.EditValue.ToString();
                if (MaNhom == "")
                {
                    MessageBox.Show($"Bạn chưa chọn mã nhóm để sửa thông tin.", " Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //DialogResult kq = MessageBox.Show($"Bạn muốn sửa thông tin mã nhóm : {MaNhom}", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (kq == DialogResult.Yes)
                    //{
                    //    NhomDAO.Instance.Update(MaNhom, TenNhom, Ghichu, PhongBan);
                    //    MessageBox.Show($"Đã sửa thông tin nhóm :{MaNhom} ", "THÀNH CÔNG:");
                    //    LoadControl();
                    //}
                }                                          
            }
        }  
        
        private void btnThem_Click(object sender, EventArgs e)
        {
            if(idquyen>=5)
            {
                LockControl(false);
                them = true;
            }
           else
            {
                MessageBox.Show("Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }

        private void btnSua_Click(object sender, EventArgs e)
        {
          
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (idquyen >= 5)
            {                              
                    int dem = 0;
                    List<NhomDTO> LsNhomdcChon = new List<NhomDTO>();
                    foreach (var item in gridView2.GetSelectedRows())
                    {
                        string MaNhom = gridView2.GetRowCellValue(item, "MANHOM").ToString();
                        string NhaMay= gridView2.GetRowCellValue(item, "NHAMAY").ToString();
                        string BoPhan = gridView2.GetRowCellValue(item, "BOPHAN").ToString();
                        string PhongBan = gridView2.GetRowCellValue(item, "PHONGBAN").ToString();
                        NhomDTO a = NhomDAO.Instance.GetNhomDTO(MaNhom, NhaMay, BoPhan, PhongBan);
                        LsNhomdcChon.Add(a);
                        dem++;
                    }
                    if(dem>0)
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} mã nhóm được chọn.", "Thông báo:", MessageBoxButtons.YesNo);
                        if (kq == DialogResult.Yes)
                        {
                            foreach (NhomDTO item in LsNhomdcChon)
                            {
                               NhomDAO.Instance.Delete(item.MANHOM,item.NHAMAY,item.BOPHAN,item.PHONGBAN);
                            }
                            MessageBox.Show($" Đã xóa {dem} mã nhóm.", " THÀNH CÔNG!");
                        }
                        LoadControl();
                    }
                    else
                    {
                        MessageBox.Show("Chưa chọn mã nhóm muốn xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                                 
            }
            else
            {
              
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (idquyen >= 5)
            {
                Save();
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {

            if (idquyen >= 5)
            {
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }

      
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            try
            {
                txtMaNhom.Text = gridView2.GetFocusedRowCellValue("MANHOM").ToString();
                txtTenNhom.Text = gridView2.GetFocusedRowCellValue("TENNHOM").ToString();
                txtGhiChu.Text = gridView2.GetFocusedRowCellValue("GHICHU").ToString();
                sglPhongBan.Text = gridView2.GetFocusedRowCellValue("PHONGBAN").ToString();
            }
            catch
            {

            }

        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void searchLookUpEdit1View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void searchLookUpEdit1View_Click(object sender, EventArgs e)
        {         
             txtBoPhan.Text = searchLookUpEdit1View.GetFocusedRowCellValue("BOPHAN").ToString();
             txtNhaMay.Text = searchLookUpEdit1View.GetFocusedRowCellValue("NHAMAY").ToString();                        
        }

    
    }
}