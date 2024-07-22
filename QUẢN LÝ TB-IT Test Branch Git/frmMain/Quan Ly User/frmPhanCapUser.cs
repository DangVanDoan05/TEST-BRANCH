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
using DevExpress.XtraTreeList.Nodes;

namespace frmMain.Du_Lieu_Nguon
{
    public partial class frmPhanCapUser : DevExpress.XtraEditors.XtraForm
    {
        public frmPhanCapUser()
        {
            InitializeComponent();
            LoadControl(); // Nút chức năng phân cấp này chỉ dùng cho QLTC trở lên 
                            // Với id=5 hoặc id=6
        }

      
        int idquyen = CommonUser.Quyen;
        string maNVbang = "";
        string maNVTree = "";
     
        private void LoadControl()
        {

            if (idquyen >= 7) // admin + Tổng giám đốc
            {
                LoadDataAllPB();
            }
            else
            {
                if (idquyen >= 6&&idquyen<7) // QLCC: Load bộ phận
                {
                    LoadDataBP(CommonUser.UserStatic.BOPHAN);
                }
                else
                {
                    if (idquyen >= 1 && idquyen <= 5) // QLTC trở xuống chỉ Load nhân viên của phòng ban
                    {
                        LoadDataOnePB(CommonUser.UserStatic.PHONGBAN);
                    }
                }
            }
            
        }

        private void LoadDataAllPB()
        {
            gcUser.DataSource = UserDAO.Instance.GetLsvUserNoQLTT();
            treeList1.DataSource = UserDAO.Instance.GetLsvUserTreeList();
            treeList1.ExpandAll();
        }

        private void LoadDataBP(string MaBoPhan)
        {
            gcUser.DataSource = UserDAO.Instance.GetLsvUserNoQLTTOfBP(MaBoPhan);
            treeList1.DataSource = UserDAO.Instance.GetUserOfBP(MaBoPhan);
            treeList1.ExpandAll();
        }

        private void LoadDataOnePB(string MaPB)
        {
            gcUser.DataSource = UserDAO.Instance.GetLsvUserNoQLTTOfPB(MaPB);
            int BacCV = ChucVuDAO.Instance.GetChucVuDTO(CommonUser.UserStatic.CHUCVU).BACCV;
            treeList1.DataSource = UserDAO.Instance.GetUserOfPB(MaPB);
            treeList1.ExpandAll();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
               maNVbang = gridView1.GetFocusedRowCellValue("MANV").ToString();
            }
            catch 
            {

            }
        }

        private void btnThemQLTT_Click(object sender, EventArgs e)
        {
            if(idquyen>=3)
            {
                try
                {
                    UserDTO tenNVbang = UserDAO.Instance.GetUserDTO1(maNVbang);
                    UserDTO tenNVtree = UserDAO.Instance.GetUserDTO1(maNVTree);
                    if (tenNVbang.FULLNAME == tenNVtree.FULLNAME)
                    {
                        MessageBox.Show($"Nhân viên và cấp trên trực tiếp bị trùng nhau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        maNVbang = "";
                        maNVTree = "";
                        LoadControl();
                    }
                    else
                    {
                        DialogResult kq = MessageBox.Show($"Bạn muốn cập nhật {tenNVtree.FULLNAME} là QLTT của {tenNVbang.FULLNAME}", "Thông báo:", MessageBoxButtons.YesNo);
                        if (kq == DialogResult.Yes)
                        {
                            UserDAO.Instance.UpdateQLTT(maNVbang, maNVTree);
                            MessageBox.Show($"Cập nhật thành công.", "Thông báo:");
                        }
                        maNVbang = "";
                        maNVTree = "";
                        LoadControl();
                    }
                }
                catch
                {
                    MessageBox.Show($"Hãy chọn cả nhân viên trong 2 danh sách .", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }

        private void treeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {

            List<TreeListNode> lsv = treeList1.GetAllCheckedNodes();
            foreach (TreeListNode item in lsv)
            {
               maNVTree = item.GetValue("MANV").ToString();
            }

        }

        private void btnXoaQLTT_Click(object sender, EventArgs e)
        {

            if(idquyen>=3)
            {
                try
                {
                    UserDTO tenNVtree = UserDAO.Instance.GetUserDTO1(maNVTree);
                    DialogResult kq = MessageBox.Show($"Bạn muốn xóa QLTT của {tenNVtree.FULLNAME}", "Thông báo:", MessageBoxButtons.YesNo);
                    if (kq == DialogResult.Yes)
                    {
                        UserDAO.Instance.UpdateQLTT(maNVTree, "");
                        MessageBox.Show($"Cập nhật thành công.", "Thông báo:");
                    }
                    maNVbang = "";
                    maNVTree = "";
                    LoadControl();
                }
                catch
                {
                    MessageBox.Show($"Lỗi chưa chọn mã nhân viên.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"Bạn chưa được cấp quyền cho chức năng này.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}