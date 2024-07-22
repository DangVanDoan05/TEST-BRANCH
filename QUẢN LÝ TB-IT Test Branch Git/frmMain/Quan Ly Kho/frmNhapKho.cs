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
    public partial class frmNhapKho : DevExpress.XtraEditors.XtraForm
    {
        public frmNhapKho()
        {
            InitializeComponent();
            LoadControl();         
        }
        bool them;

        private void LoadControl()
        {
            sglMaLK.Properties.DataSource= MaLinhKienDAO.Instance.GetListMaLK();
            sglMaLK.Properties.DisplayMember = "MALK";
            sglMaLK.Properties.ValueMember = "MALK";
            txtTenLK.Enabled = false;
            txtNCC.Enabled = false;
            txtDonVi.Enabled = false;
          
        }
        void Save()
        {
            string MaLK = sglMaLK.EditValue.ToString();
            if(MaLK != "")
            {
                //  public int Insert(string malk, string tenlk, int slton, string dvtinh, string ngaydat, int sldat, string ngaynhan, int slnhan)
                //  public int UpdateSLTON(string malk, int slton)                             
                string TenLK = txtTenLK.Text;
                string MaTKNhap = MaLK + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("HHmmss");
                int slnhap = int.Parse(txtSoLuong.Text);
                string dvtinh = txtDonVi.Text;
                string ncc = txtNCC.Text;
                string NguoiNhap = CommonUser.UserStatic.MANV+"-" +CommonUser.UserStatic.FULLNAME;
                string ghichu = txtGhiChu.Text;
                if (ghichu == "")
                {
                    MessageBox.Show($"Hãy nhập ghi chú đầy đủ.", "Lỗi;", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Tình trạng kiểm kê ban đầu của linh kiện:
                    TinhTrangKiemKeDTO tinhtrangKKBD = TinhTrangKiemKeDAO.Instance.GetKiemKeDTO(0); // Tình trạng 0: Tình trạng đang không kiểm kê.
                    

                    // TÁC ĐỘNG LÊN BẢNG TỒN LINH KIỆN.

                    bool CheckLKTon = TonLinhKienDAO.Instance.CheckMaLKTon(MaLK);
                    if (CheckLKTon) // Nếu đã tồn thì cập nhật lại số lượng tồn. , thêm vào bảng thống kê nhập
                    {
                        // Nếu tồn thì kiểm tra xem Mã linh kiện đó có đang kiểm kê không ?

                        TonLinhKienDTO MaLkTonDTO = TonLinhKienDAO.Instance.GetMaLKTon(MaLK);
                        int IDttKK = MaLkTonDTO.IDTTKIEMKE;

                        if(IDttKK==0) // Tình trạng 0: đang không kiểm kê.
                        {
                            int sltonMoi = MaLkTonDTO.SLTON + slnhap;
                            TonLinhKienDAO.Instance.UpdateSLTON(MaLK, sltonMoi);
                            // TÁC ĐỘNG LÊN BẢNG THỐNG KÊ NHẬP.
                            ThongKeNhapDAO.Instance.Insert(MaTKNhap, MaLK, TenLK, DateTime.Now.ToString("dd/MM/yyyy"), slnhap, dvtinh, ncc, NguoiNhap, ghichu, tinhtrangKKBD.IDTTKIEMKE, tinhtrangKKBD.CHITIETTTKK);
                            MessageBox.Show($"Nhập kho mã linh kiên {MaLK} thành công! ", " Thông báo: ");
                        }
                        else
                        {
                            MessageBox.Show($"Không thể nhập kho do mã linh kiện {MaLK} đang được kiểm kê.", "Lỗi;", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                                             
                    }
                    else    // Nếu chưa: Insert vào bảng số lượng tồn
                    {
                        TonLinhKienDAO.Instance.Insert(MaLK, TenLK, slnhap, dvtinh,tinhtrangKKBD.IDTTKIEMKE,tinhtrangKKBD.CHITIETTTKK);
                        // TÁC ĐỘNG LÊN BẢNG THỐNG KÊ NHẬP.
                        ThongKeNhapDAO.Instance.Insert(MaTKNhap, MaLK, TenLK, DateTime.Now.ToString("dd/MM/yyyy"), slnhap, dvtinh, ncc, NguoiNhap, ghichu, tinhtrangKKBD.IDTTKIEMKE, tinhtrangKKBD.CHITIETTTKK);
                        MessageBox.Show($"Nhập kho mã linh kiên {MaLK} thành công! ", " Thông báo: ");
                    }                                                                          
                }
            }
            else
            {
                MessageBox.Show($"Chưa chọn mã linh kiện để nhập kho.", "Lỗi;",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }                      
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            Save();
            this.Close();
        }

        private void cbMaLK_SelectedValueChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox cb = sender as System.Windows.Forms.ComboBox;

            cb.ValueMember = "MALK";
            string a = cb.SelectedValue.ToString();


            TonLinhKienDTO malkton = TonLinhKienDAO.Instance.GetMaLKTon(a);
            MaLinhKienDTO malkDTO = MaLinhKienDAO.Instance.GetRowMaLK(a);
            txtTenLK.Text = malkton.TENLK;
            txtNCC.Text = malkDTO.NCC;
        }

        private void sglMaLK_EditValueChanged(object sender, EventArgs e)
        {
            
            string a = sglMaLK.EditValue.ToString();
         
            MaLinhKienDTO malkDTO = MaLinhKienDAO.Instance.GetRowMaLK(a);
            txtTenLK.Text = malkDTO.TENLK;
            txtNCC.Text = malkDTO.NCC;
            txtDonVi.Text = malkDTO.DVTINH;
            
        }

        private void searchLookUpEdit1View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

      
    }
}