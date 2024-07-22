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
using QuanLyThietBiIT.Common;
using QuanLyThietBiIT.DAOIT;
using QuanLyThietBiIT.DTOIT;

namespace QuanLyThietBiIT
{
    public partial class frmXuatKhoLK : DevExpress.XtraEditors.XtraForm
    {
        public frmXuatKhoLK()
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
            cbMaLK.DataSource = TonLinhKienDAO.Instance.GetListMaLKTon();
            cbMaLK.DisplayMember = "MALK";
            cbMaLK.ValueMember = "MALK";
        }

        

        private void cbMaLK_Click(object sender, EventArgs e)
        {
            string malk = cbMaLK.SelectedValue.ToString();
            TonLinhKienDTO malkton = TonLinhKienDAO.Instance.GetMaLKTon(malk);
            MaLinhKienDTO malkDTO = MaLinhKienDAO.Instance.GetRowMaLK(malk);
            txtTenLK.Text = malkton.TENLK;
            txtNCC.Text = malkDTO.NCC;
            txtTonHtai.Text = malkton.SLTON.ToString();
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
            txtTonHtai.Text = malkton.SLTON.ToString();

        }
        void Save()
        {
           
            string a = cbMaLK.SelectedValue.ToString();


            TonLinhKienDTO malkton = TonLinhKienDAO.Instance.GetMaLKTon(a);
            MaLinhKienDTO malkDTO = MaLinhKienDAO.Instance.GetRowMaLK(a);
            txtTenLK.Text = malkton.TENLK;
            txtNCC.Text = malkDTO.NCC;
            txtTonHtai.Text = malkton.SLTON.ToString();
            string tenlk = malkton.TENLK;
            string ncc = malkDTO.NCC;
            
            string dvtinh = malkton.DVTINH;
            int ton = malkton.SLTON;
            int slxuat = int.Parse(txtSoluongXuat.Text);
            string ghichu = txtGhichu.Text;
            string name = CommonUser.UserStatic.Manv;

            if(slxuat>ton)
            {
               DialogResult kq= MessageBox.Show("Số lượng xuất vượt quá tồn hiện tại, Hãy nhập lại! ", "Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if(kq==DialogResult.Yes)
                {
                    frmXuatKhoLK f = new frmXuatKhoLK();
                    f.ShowDialog();
                }
            }
            else
            {
                DialogResult kq = MessageBox.Show($"Bạn muốn xuất {slxuat} {dvtinh} linh kiện {malkton.MALK} ?", "Thông Báo:",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if(kq==DialogResult.Yes)
                {
                    TonLinhKienDAO.Instance.UpdateSLTON(a,ton-slxuat);
                    ThongKeXuatDAO.Instance.Insert(a, tenlk, DateTime.Now.ToString("dd/MM/yyyy"), slxuat, dvtinh,ncc, name,ghichu);
                    MessageBox.Show($"Xuất linh kiện {a} thành công!");
                }
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Save();
            this.Close();
        }
    }
}