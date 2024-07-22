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
using QuanLyThietBiIT.Common;
namespace QuanLyThietBiIT
{
    public partial class frmNhapKhoLK : DevExpress.XtraEditors.XtraForm
    {
        public frmNhapKhoLK()
        {
            InitializeComponent();
            LoadControl();
        }
        bool them;

        private void LoadControl()
        {
            cbMaLK.DataSource = MaLinhKienDAO.Instance.GetListMaLK();
            cbMaLK.DisplayMember = "MALK";
            cbMaLK.ValueMember = "MALK";
           

        }
        void Save()
        {
            string malk = cbMaLK.SelectedValue.ToString();
            MaLinhKienDTO MaLkDTO = MaLinhKienDAO.Instance.GetRowMaLK(malk);
            TonLinhKienDTO MaLkTonDTO = TonLinhKienDAO.Instance.GetMaLKTon(malk);
            txtTenLK.Text = MaLkDTO.TENLK;
            txtNCC.Text = MaLkDTO.NCC;
            int slnhap = int.Parse(txtSoLuong.Text);
            string tenlk = MaLkDTO.TENLK;
            int slton = MaLkTonDTO.SLTON;
            string dvtinh = MaLkDTO.DVTINH;
            string name = CommonUser.UserStatic.Manv;
            string ghichu = txtGhiChu.Text;
            DialogResult kq = MessageBox.Show($" Bạn muốn nhập {slnhap} {dvtinh} mã Linh Kiện {malk}", "Thông Báo: ",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(kq==DialogResult.Yes)
            {
                TonLinhKienDAO.Instance.UpdateSLTON(malk, slton + slnhap);
                ThongKeNhapDAO.Instance.Insert(malk, tenlk, DateTime.Now.ToString("dd/MM/yyyy"), slnhap, dvtinh, MaLkDTO.NCC, name, ghichu);
                MessageBox.Show($"Nhập kho mã linh kiên {malk} thành công! ", " Thông Báo: ");
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
    }
}