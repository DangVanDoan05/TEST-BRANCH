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
using PhanQuyenUngDung;

namespace frmMain
{
    public partial class frmDsNhanVien : DevExpress.XtraEditors.XtraForm
    {
        public frmDsNhanVien()
        {
            InitializeComponent();
            LoadControl();
        }
       

            // Phân quyền cho Form

            // ADMIN xem được toàn bộ
            // QLCC xem đc toàn bộ phận
            // QLTC xem đc phòng ban

        bool them=false;
        int idquyen = CommonUser.Quyen;      
        string MaCVUserLogon = CommonUser.UserStatic.CHUCVU;
        
        //  int BacCV = ChucVuDAO.Instance.GetChucVuDTO(MaCV).BACCV;  
        
        // Phân quyền: đưa ra thông báo bạn chưa đủ thẩm quyền

        private void LoadControl()
        {
            LockControl(true);
            LoadGridControl();
            LoadDataEditLookup();
            CleanText();
        }
       
        private void LockControl(bool kt)
        {
            if (kt)
            {
                txtMaNhanVien.Enabled = false;
                txtHoTenNV.Enabled = false;
                sglChucVu.Enabled = false;
                chkThuocNhom.Enabled = false;
                sglNhom.Enabled = false;               
                sglPhongBan.Enabled = false;
                txtBoPhan.Enabled = false;
                txtNhaMay.Enabled = false;
                
                btnThemUser.Enabled = true;
                btnSuaUser.Enabled = true;             
                btnXoaUser.Enabled = true;              
                btnLuuUser.Enabled = false;
                btnCapNhatUser.Enabled = true;
                          
            }
            else
            {
                txtMaNhanVien.Enabled = true;
                txtHoTenNV.Enabled = true;
                sglChucVu.Enabled = true;
                chkThuocNhom.Enabled = true;
                sglNhom.Enabled = true;
                sglPhongBan.Enabled = false;
                txtBoPhan.Enabled = false;
                txtNhaMay.Enabled = false;

                btnThemUser.Enabled = false;
                btnSuaUser.Enabled = false;              
                btnXoaUser.Enabled = false;             
                btnLuuUser.Enabled = true;
                btnCapNhatUser.Enabled = true;                               
            }
        }

        private void LoadGridControl()
        {
            
            if (idquyen >= 1 && idquyen <= 5)  // Từ chỉ xem ---> QLTC
            {
                gridControl1.DataSource = QLNhanVienDAO.Instance.GetNVOfPB(CommonUser.UserStatic.PHONGBAN);
            }
            if (idquyen >= 6)
            {
                if (idquyen == 6)    // QLCC
                {
                    gridControl1.DataSource = QLNhanVienDAO.Instance.GetNVOfBP(CommonUser.UserStatic.BOPHAN);
                }
                else             // TGĐ + ADMIN
                {
                    gridControl1.DataSource = QLNhanVienDAO.Instance.GetTable();
                }
            }

        }

        private void LoadDataEditLookup()
        {
            chkThuocNhom.Checked = true;

            //*** Load chức vụ: Load chức vụ cấp thấp hơn cấp của người đăng nhập vào 
            if(MaCVUserLogon=="ADMIN")
            {
                sglChucVu.Properties.DataSource = ChucVuDAO.Instance.GetLsvCVDTO();
                sglChucVu.Properties.DisplayMember = "MACHUCVU";
                sglChucVu.Properties.ValueMember = "MACHUCVU";
            }
            else
            {
                int BacCV = ChucVuDAO.Instance.GetChucVuDTO(CommonUser.UserStatic.CHUCVU).BACCV;
                sglChucVu.Properties.DataSource = ChucVuDAO.Instance.GetTableBacCVlonhon(BacCV);
                sglChucVu.Properties.DisplayMember = "MACHUCVU";
                sglChucVu.Properties.ValueMember = "MACHUCVU";
            }


            //*** Load Nhóm 
            if (MaCVUserLogon == "ADMIN") // ADMIN load toàn bộ nhóm
            {
                sglNhom.Properties.DataSource = NhomDAO.Instance.GetTable();
                sglNhom.Properties.DisplayMember = "MANHOM";
                sglNhom.Properties.ValueMember = "MANHOM";
            }
            else
            {
                // QLCC <=> Load nhóm của bộ phận mình

                if(idquyen>=6)
                {
                    if(idquyen>=7) // Tổng Giám Đốc
                    {
                        sglNhom.Properties.DataSource = NhomDAO.Instance.GetTable();
                        sglNhom.Properties.DisplayMember = "MANHOM";
                        sglNhom.Properties.ValueMember = "MANHOM";
                    }
                    else // QLCC
                    {
                        sglNhom.Properties.DataSource = NhomDAO.Instance.GetLsvNhomThuocBP(CommonUser.UserStatic.BOPHAN);
                        sglNhom.Properties.DisplayMember = "MANHOM";
                        sglNhom.Properties.ValueMember = "MANHOM";
                    }                  
                }
                else // QLTC trở xuống
                {
                    sglNhom.Properties.DataSource = NhomDAO.Instance.GetNhomOfPB(CommonUser.UserStatic.PHONGBAN);
                    sglNhom.Properties.DisplayMember = "MANHOM";
                    sglNhom.Properties.ValueMember = "MANHOM";
                }              
            }

            //*** Load phòng
            if (MaCVUserLogon == "ADMIN") // ADMIN load toàn bộ nhóm
            {
                sglPhongBan.Properties.DataSource = PhongBanDAO.Instance.GetTable();
                sglPhongBan.Properties.DisplayMember = "MAPB";
                sglPhongBan.Properties.ValueMember = "MAPB";
            }
            else
            {
                // QLCC <=> Load nhóm của bộ phận mình

                if (idquyen >= 6)
                {
                    if (idquyen >= 7) // Tổng Giám Đốc
                    {
                        sglPhongBan.Properties.DataSource = PhongBanDAO.Instance.GetTable();
                        sglPhongBan.Properties.DisplayMember = "MAPB";
                        sglPhongBan.Properties.ValueMember = "MAPB";
                    }
                    else // QLCC : chỉ lấy phòng ban thuộc bộ phận
                    {
                        sglPhongBan.Properties.DataSource = PhongBanDAO.Instance.GetLsvPbThuocBP(CommonUser.UserStatic.BOPHAN);
                        sglPhongBan.Properties.DisplayMember = "MAPB";
                        sglPhongBan.Properties.ValueMember = "MAPB";
                    }
                }
                else // QLTC trở xuống: phòng ban mặc định bằng phòng ban của User đăng nhập.
                {
                    sglPhongBan.Properties.DataSource = PhongBanDAO.Instance.GetLsvPbThuocBP(CommonUser.UserStatic.BOPHAN);
                    sglPhongBan.Properties.DisplayMember = "MAPB";
                    sglPhongBan.Properties.ValueMember = "MAPB";
                    sglPhongBan.EditValue = CommonUser.UserStatic.PHONGBAN;
                }

            }
            

        }
        private void LockControlEditLookup()
        {
           if(idquyen>=1&&idquyen<=5)
            {
             
                sglPhongBan.Enabled = false;
                chkThuocNhom.Enabled = true;
                sglNhom.Enabled = true;
                sglChucVu.Enabled = true;
            }
           else // >=6
            {
                if(idquyen==6)
                {
                  
                    sglPhongBan.Enabled = true;
                    chkThuocNhom.Enabled = true;
                    if (chkThuocNhom.Checked)
                    {
                        sglNhom.Enabled = true;
                    }
                    else
                    {
                        sglNhom.Enabled = false;
                    }
                    sglChucVu.Enabled = true;
                }
                else
                {
                                    
                    sglPhongBan.Enabled = true;
                    chkThuocNhom.Enabled = true;
                    if (chkThuocNhom.Checked)
                    {
                        sglNhom.Enabled = true;
                    }
                    else
                    {
                        sglNhom.Enabled = false;
                    }                 
                    sglChucVu.Enabled = true;
                }
            }
        }

        private void CleanText()
        {           
            txtHoTenNV.Clear();
            txtMaNhanVien.Clear();
        }

        void Save()
        {
            if (them)
            {
                try
                {
                    string manv = txtMaNhanVien.Text.Trim();    
                    string tenht = txtHoTenNV.Text.Trim();                                                         
                    string Nhom = "";
                    if (chkThuocNhom.Checked)
                    {
                        Nhom = sglNhom.EditValue.ToString();
                        sglPhongBan.Enabled = false;                      
                    }
                    string Phongban = sglPhongBan.EditValue.ToString();
                    string Bophan = txtBoPhan.Text;
                    string NhaMay = txtNhaMay.Text;
                    string ChucVu = sglChucVu.EditValue.ToString();

                    bool CheckNVExist = QLNhanVienDAO.Instance.CheckMaNVExist(manv,NhaMay);
                    if (CheckNVExist)
                    {
                        MessageBox.Show($" Nhân viên mã {manv} đã tồn tại ở nhà máy {NhaMay}.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {                     
                            //*** Thêm vào bảng quản lý Nhân viên
                            QLNhanVienDAO.Instance.Insert(manv, tenht,NhaMay, Bophan, Phongban, Nhom, ChucVu);
                            MessageBox.Show($"Đã thêm nhân viên mã {manv} cho nhà máy {NhaMay}.", "THÀNH CÔNG!");
                        
                     }
                    
                }
                catch
                {
                    MessageBox.Show($"Hãy chọn chức vụ nhóm hoặc phòng ban có trong danh sách. ", "Cảnh báo:", MessageBoxButtons.OK, MessageBoxIcon.Stop);                  
                }

                them = false;
            }
            else // Update thông tin nhân viên
            {

                //try
                //{
                //    string manv = txtMaNhanVien.Text.Trim();
                //    string tenht = txtHoTenNV.Text.Trim();
                //    string ChucVu = sglChucVu.EditValue.ToString();
                                                       
                //    string Nhom = "";
                //    if (chkThuocNhom.Checked)
                //    {
                //        Nhom = sglNhom.EditValue.ToString();
                //    }
                //    string Phongban = sglPhongBan.EditValue.ToString();
                //    string Bophan = txtBoPhan.Text;

                //    //bool CheckNVExist = QLNhanVienDAO.Instance.CheckMaNVExist(manv);                
                //    //DialogResult kq = MessageBox.Show($"Bạn muốn thay đổi thông tin nhân viên mã {manv} ?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //    //if (kq == DialogResult.Yes)
                //    // {
                //    //        //*** update thông tin nhân viên

                //    //        QLNhanVienDAO.Instance.Update(manv, tenht, Bophan, Phongban, Nhom, ChucVu);
                //    //        MessageBox.Show($"Đã sửa thông tin nhân viên mã {manv}.", "THÀNH CÔNG!");
                //    //        them = false;
                //    // }                   
                //}
                //catch
                //{

                //    MessageBox.Show($"Hãy chọn chức vụ nhóm hoặc phòng ban có trong danh sách. ", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    them = false;

                //}

            }
        }
        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {               
                txtMaNhanVien.Text = gridView1.GetFocusedRowCellValue("MANV").ToString();
                txtHoTenNV.Text = gridView1.GetFocusedRowCellValue("FULLNAME").ToString();
                txtBoPhan.Text = gridView1.GetFocusedRowCellValue("BOPHAN").ToString();
                sglPhongBan.EditValue=gridView1.GetFocusedRowCellValue("PHONGBAN").ToString();
                sglNhom.EditValue = gridView1.GetFocusedRowCellValue("NHOM").ToString();
                sglChucVu.EditValue = gridView1.GetFocusedRowCellValue("CHUCVU").ToString();
            }
            catch
            {

            }
            
        }

      

                 
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnThemUser_Click(object sender, EventArgs e)
        {

            if (idquyen >= 2)
            {
                LockControl(false);
                them = true;
            }
            else
            {
                MessageBox.Show("Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSuaUser_Click(object sender, EventArgs e)
        {
            //if (idquyen >= 2)
            //{
            //    LockControl(false);
            //    txtMaNhanVien.Enabled = false;
            //}
            //else
            //{
            //    MessageBox.Show("Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}           
        }

        private void btnLuuUser_Click(object sender, EventArgs e)
        {
            if (idquyen >= 2)
            {
                Save();
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void searchLookUpEdit2View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void searchLookUpEdit1View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridView3_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

       

        private void chkThuocNhom_Click(object sender, EventArgs e)
        {
            if (chkThuocNhom.Checked) // Nếu được Check <==> Có thuộc vào nhóm.
            {
                sglNhom.Enabled = true; 
                sglPhongBan.Enabled = false;               
            }
            else       // Nếu không thuộc vào nhóm thì chỉ Load những phòng ban không có chứa nhóm
            {

                sglNhom.Enabled = false;

                if(idquyen>=6)
                {
                    sglPhongBan.Enabled = true;
                }
                else
                {
                    sglPhongBan.Enabled = false;
                }
                                           
            }
        }

        private void btnCapNhatUser_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

     

        private void sglPhongBan_EditValueChanged(object sender, EventArgs e)
        {
          //  string maPB = sglPhongBan.EditValue.ToString();
          //  PhongBanDTO a = PhongBanDAO.Instance.GetPBDTO(maPB);
          //  txtBoPhan.Text = a.BOPHAN;
        }

        private void gridView4_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

      
        private void sglNhom_EditValueChanged(object sender, EventArgs e)
        {
            //string Nhom = sglNhom.EditValue.ToString();
            //NhomDTO nhomDTO = NhomDAO.Instance.GetNhomDTO(Nhom);
            //string MaPB = nhomDTO.PHONGBAN;
          //  PhongBanDTO a = PhongBanDAO.Instance.GetPBDTO(MaPB);
           
            //  Lỗi chưa đổ Datasource cho EditLookup

           // sglPhongBan.Properties.DataSource = PhongBanDAO.Instance.GetTable();
           // sglPhongBan.Properties.DisplayMember = "MAPB";
           // sglPhongBan.Properties.ValueMember = "MAPB";          
           //// sglPhongBan.EditValue = a.MAPB;          
           // txtBoPhan.Text =a.BOPHAN;            
        }

        private void btnXoaUser_Click(object sender, EventArgs e)
        {
            if (idquyen >= 3)
            {
                LockControl(false);
                txtMaNhanVien.Enabled = false;
               
                
                    // cho phép xóa nhiều dòng trong gridview
                    int dem = 0;
                    //  int demloi = 0;
                    List<QLNhanVienDTO> LsMaNVdcChon = new List<QLNhanVienDTO>();
                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        string MaNV = gridView1.GetRowCellValue(item, "MANV").ToString();
                        string MaNM= gridView1.GetRowCellValue(item, "NHAMAY").ToString();
                        QLNhanVienDTO a = QLNhanVienDAO.Instance.GetNhanVienDTO(MaNV, MaNM);
                        LsMaNVdcChon.Add(a);
                        dem++;
                    }

                    if(dem>0)
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} mã nhân viên được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (kq == DialogResult.Yes)
                        {
                            int demXoa = 0;
                            foreach (QLNhanVienDTO item in LsMaNVdcChon)
                            {
                                if (item.MANV != CommonUser.UserStatic.MANV)
                                {
                                    QLNhanVienDAO.Instance.Delete(item.MANV,item.NHAMAY);
                                    demXoa++;
                                }                         
                            }

                            if(demXoa<dem)
                            {
                                 MessageBox.Show($"Đã xóa {demXoa} nhân viên, {dem-demXoa} không thể xóa.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show($"Đã xóa {dem} nhân viên được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            demXoa = 0;
                            dem = 0;
                        }
                        LoadControl();
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa chọn nhân viên để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } 
                    
            }
            else
            {
                MessageBox.Show("Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchLookUpEdit1View_Click(object sender, EventArgs e) // Nhom
        {
            sglPhongBan.EditValue = searchLookUpEdit1View.GetFocusedRowCellValue("PHONGBAN").ToString();
            txtBoPhan.Text = searchLookUpEdit1View.GetFocusedRowCellValue("BOPHAN").ToString();
            txtNhaMay.Text = searchLookUpEdit1View.GetFocusedRowCellValue("NHAMAY").ToString();
        }

        private void gridView3_Click(object sender, EventArgs e) // Phòng ban.
        {
            txtBoPhan.Text = gridView3.GetFocusedRowCellValue("BOPHAN").ToString();
            txtNhaMay.Text = gridView3.GetFocusedRowCellValue("NHAMAY").ToString();
        }
    }
}