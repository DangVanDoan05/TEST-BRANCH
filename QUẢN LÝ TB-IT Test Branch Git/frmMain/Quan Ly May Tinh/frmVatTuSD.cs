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
    public partial class frmVatTuSD : DevExpress.XtraEditors.XtraForm
    {

        public frmVatTuSD(bool Chixem)
        {
            InitializeComponent();
            if(Chixem)
            {
                LoadControlChiXem();
            }
            else
            {
                LoadControl();
            }          
        }

        string MaBT;
        bool ThemSD;
        bool ThemTH;

        private void LoadControlChiXem()
        {
            LockControl(-1);
            LoadData();
        }

        private void LoadControl()
        {
            LockControl(0);
            LoadData();
        }
        private void LoadData()
        {
            MaBT = CommonMaBaoTri.MaBaoTri;

            // Load Edit Lookup

            sglVTSD.Properties.DataSource = TonLinhKienDAO.Instance.GetListMaLKTon();
            sglVTSD.Properties.DisplayMember = "MALK";
            sglVTSD.Properties.ValueMember = "MALK";

            sglVTTH.Properties.DataSource = MaLinhKienDAO.Instance.GetListMaLK();
            sglVTTH.Properties.DisplayMember = "MALK";
            sglVTTH.Properties.ValueMember = "MALK";

            // Load GridControl
            gcVTSD.DataSource = QLyVTSDDAO.Instance.GetLsVtOfMaBT(MaBT);
            gcVTTH.DataSource = QLyVTTHDAO.Instance.GetLsVtOfMaBT(MaBT);
        }

        private void LockControl(int kt)
        {
            if (kt ==-1) // Khóa toàn bộ
            {
                sglVTSD.Enabled = false;
                txtSLSD.Enabled = false;

                btnThemSD.Enabled = false;
                btnXoaSD.Enabled = false;
                btnLuuSD.Enabled = false;
                btnCapNhatSD.Enabled = false;
                // Thu hồi:
                sglVTTH.Enabled = false;
                txtSLTH.Enabled = false;

                btnThemTH.Enabled = false;
                btnXoaTH.Enabled =false;
                btnLuuTH.Enabled = false;
                btnCapNhatTH.Enabled = false;
            }

            if (kt == 0) // Khóa toàn bộ
            {
                sglVTSD.Enabled = false;
                txtSLSD.Enabled = false;

                btnThemSD.Enabled = true;
                btnXoaSD.Enabled = true;
                btnLuuSD.Enabled = false;
                btnCapNhatSD.Enabled = true;
                // Thu hồi:
                sglVTTH.Enabled = false;
                txtSLTH.Enabled = false;

                btnThemTH.Enabled = true;
                btnXoaTH.Enabled = true;
                btnLuuTH.Enabled = false;
                btnCapNhatTH.Enabled = true;
            }
            if (kt == 1)    // Vật tư sử dụng
            {
                sglVTSD.Enabled = true;
                txtSLSD.Enabled = true;

                btnThemSD.Enabled = false;
                btnXoaSD.Enabled = false;
                btnLuuSD.Enabled = true;
                btnCapNhatSD.Enabled = true;

                // Thu hồi:
                sglVTTH.Enabled = false;
                txtSLTH.Enabled = false;

                btnThemTH.Enabled = true;
                btnXoaTH.Enabled = true;
                btnLuuTH.Enabled = false;
                btnCapNhatTH.Enabled = true;
            }
            if (kt == 2)  // Bảng Thu hồi
            {
                sglVTSD.Enabled = false;
                txtSLSD.Enabled = false;

                btnThemSD.Enabled = true;
                btnXoaSD.Enabled = true;
                btnLuuSD.Enabled = false;
                btnCapNhatSD.Enabled = true;

                // Thu hồi:
                sglVTTH.Enabled = true;
                txtSLTH.Enabled = true;

                btnThemTH.Enabled = false;
                btnXoaTH.Enabled = false;
                btnLuuTH.Enabled = true;
                btnCapNhatTH.Enabled = true;

            }
        }

        private void btnThemSD_Click(object sender, EventArgs e)
        {
            LockControl(1);
            ThemSD = true;
        }

        private void btnThemTH_Click(object sender, EventArgs e)
        {
            LockControl(2);
            ThemTH = true;
        }

        private void btnLuuSD_Click(object sender, EventArgs e)
        {
            if (ThemSD)
            {
                string MaBT = CommonMaBaoTri.MaBaoTri;
                string MaVtu = "";
                try
                {
                    MaVtu = sglVTSD.EditValue.ToString();
                }
                catch
                {
                    MaVtu = "";
                }
                if (MaVtu == "")
                {
                    MessageBox.Show("Chưa chọn mã vật tư sử dụng.", "Lỗi:");
                }
                else
                {
                    TonLinhKienDTO ton = TonLinhKienDAO.Instance.GetMaLKTon(MaVtu);
                    string TenVTSD = ton.TENLK;
                    int SLTON = ton.SLTON;
                    if (txtSLSD.Text != "")
                    {
                        int SLSD = int.Parse(txtSLSD.Text);
                        if (SLSD > SLTON)
                        {
                            MessageBox.Show("Số lượng sử dụng vượt quá số lượng tồn.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            // THÊM VÀO BẢNG VẬT TƯ SỬ DỤNG

                            bool CheckVtuExist = QLyVTSDDAO.Instance.CheckExist(MaBT, MaVtu);
                            if (CheckVtuExist)
                            {
                                MessageBox.Show($"Mã vật tư {MaVtu} đã tồn tại trong danh sách sử dụng.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                TinhTrangKiemKeDTO tinhtrangKKBD = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0); // Tình trạng 0: Tình trạng đang không kiểm kê.
                                QLyVTSDDAO.Instance.Insert(MaBT, MaVtu, TenVTSD, SLSD,tinhtrangKKBD.IDTTKIEMKE,tinhtrangKKBD.CHITIETTTKK);
                                MessageBox.Show("Thêm vật tư thành công.", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ThemSD = false;
                                LoadControl();

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chưa nhập số lượng sử dụng.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

                
            }
        }

        private void searchLookUpEdit1View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void searchLookUpEdit2View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnCapNhatSD_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void btnCapNhatTH_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void btnLuuTH_Click(object sender, EventArgs e)
        {
            if (ThemTH)
            {
                string MaBT = CommonMaBaoTri.MaBaoTri;
                string MaVtu = "";
                try
                {
                    MaVtu = sglVTTH.EditValue.ToString();
                }
                catch
                {

                }

                if (MaVtu == "")
                {
                    MessageBox.Show("Chưa chọn mã vật tư thu hồi.", "Lỗi:",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                MaLinhKienDTO MaLKdto = MaLinhKienDAO.Instance.GetRowMaLK(MaVtu);

                if (txtSLTH.Text != "")
                {
                    int SLTH = int.Parse(txtSLTH.Text);
                  
                    // THÊM VÀO BẢNG VẬT TƯ THU HỒI:

                    QLyVTTHDAO.Instance.Insert(MaBT, MaVtu,MaLKdto.TENLK,SLTH,"Lưu kho.");

                    //// UPDATE SỐ LƯỢNG TỒN TRONG BẢNG TỒN LINH KIỆN THU HỒI

                    //bool CheckVtuTonTH = TonLKThuHoiDAO.Instance.CheckMaLKTHTon(MaVtu);
                    //if(!CheckVtuTonTH) // Nếu chưa tồn tại thì thêm vào bảng.
                    //{
                    //    TinhTrangKiemKeDTO KiemKeBD = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0); // tình trạng 0: đang chưa kiểm kê
                    //    TonLKThuHoiDAO.Instance.Insert(MaVtu, MaLKdto.TENLK, SLTH, MaLKdto.DVTINH, KiemKeBD.IDTTKIEMKE, KiemKeBD.CHITIETTTKK);
                    //}
                    //else
                    //{
                    //    // Nếu đã tồn tại thì lấy số lượng tồn

                    //    TonLKThuHoiDTO a1 = TonLKThuHoiDAO.Instance.GetTonLKTHDTO(MaVtu);
                    //    int TonCu = a1.SLTON;
                    //    TonLKThuHoiDAO.Instance.UpdateSLTON(MaVtu,TonCu+SLTH);

                    //}
                        
                   

                    //// THÊM VÀO BẢNG THỐNG KÊ THU HỒI
                    
                    //string MaTKTH = MaVtu + DateTime.Now.ToString("-ddMMyyyy-HHmmss");
                    //MaLinhKienDTO MaLKDTO = MaLinhKienDAO.Instance.GetRowMaLK(MaVtu);
                    //string MaMT = CommonMaBaoTri.MaMayTinh;
                    //QuanLyMayTinhDTO a = QuanLyMayTinhDAO.Instance.GetMaMT(MaMT);
                    //string PhongBan = a.PB;
                    //string TenLK = MaLKDTO.TENLK;
                    //string ncc = MaLKDTO.NCC;
                    //string ngayTH = DateTime.Now.ToString("dd/MM/yyyy");
                    //string dvtinh = MaLKDTO.DVTINH;
                    //int slTH = SLTH;
                    //string NguoiTH = CommonUser.UserStatic.MANV + "-" + CommonUser.UserStatic.FULLNAME;
                   
                    //TinhTrangKiemKeDTO tinhtrangBD = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0);  // Tình trạng 0: đang chưa kiểm kê.

                    //ThongKeTHDAO.Instance.Insert(MaTKTH, MaVtu, TenLK,ngayTH,PhongBan,SLTH, dvtinh,NguoiTH, tinhtrangBD.IDTTKIEMKE, tinhtrangBD.CHITIETTTKK);
                    MessageBox.Show("Đã thu hồi vật tư.", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ThemTH = false;
                    LoadControl();
                    return;
                }
                MessageBox.Show("Chưa nhập số lượng thu hồi.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaSD_Click(object sender, EventArgs e)
        {                 
            // cho phép xóa nhiều dòng trong gridview
            int dem = 0;
            List<QLyVTSDDTO> LsMaVtuDcChon = new List<QLyVTSDDTO>();
            foreach (var item in gridView1.GetSelectedRows())
            {
                string MaBT = gridView1.GetRowCellValue(item, "MABT").ToString();
                string MaVT = gridView1.GetRowCellValue(item, "MAVTSD").ToString();
                QLyVTSDDTO a = QLyVTSDDAO.Instance.GetVTSDDTO(MaBT, MaVT);
                LsMaVtuDcChon.Add(a);
                dem++;
            }

            if (dem > 0)
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} mã vật tư được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    foreach (QLyVTSDDTO item in LsMaVtuDcChon)
                    {

                      
                            // Xóa trong BẢNG VẬT TƯ SỬ DỤNG

                            QLyVTSDDAO.Instance.DeleteVtu(item.MABT, item.MAVTSD);

                           
                                                                                     
                    }
                    MessageBox.Show($"Đã xóa {dem} mã vật tư thu hồi được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn mã vật tư thu hồi để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnXoaTH_Click(object sender, EventArgs e)
        {
            // cho phép xóa nhiều dòng trong gridview
            int dem = 0;
            List<QLyVTTHDTO> LsMaVtuDcChon = new List<QLyVTTHDTO>();
            foreach (var item in gridView1.GetSelectedRows())
            {
                string MaBT = gridView1.GetRowCellValue(item, "MABT").ToString();
                string MaVT = gridView1.GetRowCellValue(item, "MAVTTH").ToString();
                QLyVTTHDTO a = QLyVTTHDAO.Instance.GetVtTHOfMaBTdto(MaBT, MaVT);
                LsMaVtuDcChon.Add(a);
                dem++;
            }

            if (dem > 0)
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} mã vật tư thu hồi được chọn?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (kq == DialogResult.Yes)
                {
                    foreach (QLyVTTHDTO item in LsMaVtuDcChon)
                    {
                        // Xóa trong BẢNG VẬT TƯ THU HỒI

                        QLyVTTHDAO.Instance.DeleteVtu(item.MABT, item.MAVTTH);

                    }
                    MessageBox.Show($"Đã xóa {dem} mã vật tư thu hồi được chọn.", "THÀNH CÔNG!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadControl();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn mã vật tư thu hồi để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadControl();
        }
    }
}