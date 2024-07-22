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
    public partial class frmXuatKho : DevExpress.XtraEditors.XtraForm
    {
        public frmXuatKho()
        {
            InitializeComponent();
            LoadControl();
            
        }
        private void LoadControl()
        {
            LockControl();
            LoadData();
        }

        private void LockControl()
        {
            txtNCC.Enabled = false;
            txtTenLK.Enabled = false;
            txtTonHtai.Enabled = false;
        }

        private void LoadData()
        {
            txtMaLK.Text = DHduocchon.MaLKdangchon;
            string MaLK = txtMaLK.Text;
            TonLinhKienDTO malkton = TonLinhKienDAO.Instance.GetMaLKTon(MaLK);
            MaLinhKienDTO malkDTO = MaLinhKienDAO.Instance.GetRowMaLK(MaLK);
            txtTenLK.Text = malkton.TENLK;
            txtNCC.Text = malkDTO.NCC;
            txtTonHtai.Text = malkton.SLTON.ToString();
            txtDonvi.Text = malkDTO.DVTINH;
            txtMaLK.Enabled = false;
            txtTenLK.Enabled = false;
            txtNCC.Enabled = false;
            txtTonHtai.Enabled = false;
            txtDonvi.Enabled = false;
        }

        void Save() // Trừ vào bảng tồn linh kiện.
                    // Thêm vào bảng thống kê xuất.
        {
            //  public int Insert(string MaTKxuat, string malk, string tenlk, string ngayxuat, int slxuat, string dvtinh, string ncc, string nguoixuat, string YCKTSD, string MDSD)
            // BẢNG TỒN LINH KIỆN:
            //  public int UpdateSLTON(string malk, int slton)

            string MaLK = txtMaLK.Text;
            string MaTKxuat = MaLK + DateTime.Now.ToString("-ddMMyyyy-HHmmss");
            string TenLK = txtTenLK.Text;
            string ngayxuat = DateTime.Now.ToString("dd/MM/yyyy");
            string ncc = txtNCC.Text;
            string dvtinh = txtDonvi.Text;
            int TonHtai = int.Parse(txtTonHtai.Text);
            int slxuat = int.Parse(txtSoluongXuat.Text);
            string ghichu = txtMDSD.Text;
            string NguoiXuat = CommonUser.UserStatic.MANV+"-"+ CommonUser.UserStatic.FULLNAME;
            string ycktSD = "";
            string MDSD = txtMDSD.Text;

            if(slxuat>TonHtai||slxuat<0)
            {
                MessageBox.Show("Số lượng xuất vượt quá tồn hiện tại hoặc không hợp lệ.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MDSD=="")
                {
                   MessageBox.Show("Không được để trống mục đích sử dụng.", "Lỗi:", MessageBoxButtons.YesNo, MessageBoxIcon.Error);                   
                }
                else
                {
                    DialogResult kq = MessageBox.Show($"Bạn muốn xuất {slxuat} {dvtinh} linh kiện {MaLK} ?", "Thông báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq == DialogResult.Yes)
                    {
                        // UPDATE SỐ LƯỢNG TỒN
                        TonLinhKienDAO.Instance.UpdateSLTON(MaLK, TonHtai-slxuat);
                        // THÊM VÀO BẢNG THỐNG KÊ XUẤT
                        TinhTrangKiemKeDTO tinhtrangBD = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0); // Thêm vào bảng thống kê xuất
                        ThongKeXuatDAO.Instance.Insert(MaTKxuat,MaLK,TenLK, ngayxuat, slxuat, dvtinh, ncc,NguoiXuat, ycktSD,MDSD,tinhtrangBD.IDTTKIEMKE,tinhtrangBD.CHITIETTTKK);
                        MessageBox.Show($"Xuất linh kiện {MaLK} thành công!");
                    }
                }
            }          
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Save();
            this.Close();
        }

      

        private void searchLookUpEdit1View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

     
    }
}