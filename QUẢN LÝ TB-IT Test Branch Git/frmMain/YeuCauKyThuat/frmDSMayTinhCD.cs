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

namespace frmMain.YeuCauKyThuat
{
    public partial class frmDSMayTinhCD : DevExpress.XtraEditors.XtraForm
    {
        public frmDSMayTinhCD()
        {
            InitializeComponent();
            LoadControl();
        }

        bool them;


      
        public void LoadControl()
        {
            LockControl(true);  
            LoadData();         
        }

        
        private void LoadData()
        {
           
            
        }


        private void LockControl(bool kt)
        {
           if(kt)
            {
                sglMaMT.Enabled = false;
               
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnCapNhat.Enabled = true;
            }
           else
            {
                sglMaMT.Enabled = true;
               
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnCapNhat.Enabled = true;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LockControl(false);
            them = true;

        }

        private void sglMaMT_EditValueChanged(object sender, EventArgs e)
        {

            string MaMT = sglMaMT.EditValue.ToString();
            QuanLyMayTinhDTO a = QuanLyMayTinhDAO.Instance.GetMaMT(MaMT);          
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (them)
            {
                try
                {
                    string MaMT = sglMaMT.EditValue.ToString();

                  //  QuanLyMayTinhDAO.Instance.UpdateDuocChon(MaMT, 1);

                }
                catch
                {
                    MessageBox.Show("Chưa chọn máy tính cần cài đặt.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            them = false;
            LoadControl();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                sglMaMT.EditValue = gridView1.GetFocusedRowCellValue("MAMT").ToString();
                        
            }
            catch
            {

            }

        }

        private void searchLookUpEdit1View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {           
                    LockControl(false);
                    sglMaMT.Enabled = false;                             
                   
                    int dem = 0;
                   
                    List<string> LsMaMTdcChon = new List<string>();
                    foreach (var item in gridView1.GetSelectedRows())
                    {
                        string ma1 = gridView1.GetRowCellValue(item, "MAMT").ToString();
                        LsMaMTdcChon.Add(ma1);
                        dem++;
                    }

                    if(dem>0)
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn xóa {dem} mã máy tính được chọn?", "Thông Báo:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (kq == DialogResult.Yes)
                        {
                            int demXoa = 0;
                            foreach (string item in LsMaMTdcChon)
                            {
                               // QuanLyMayTinhDAO.Instance.UpdateDuocChon(item, 0);                         
                            }
                          
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Chưa chọn mã máy tính để xóa.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                
                LoadControl();         
        }

       
    }
}