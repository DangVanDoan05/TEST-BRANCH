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
using DevExpress.XtraTreeList.Nodes;

namespace frmMain.Du_Lieu_Nguon
{
    public partial class frmPQChucNang : DevExpress.XtraEditors.XtraForm
    {
        public frmPQChucNang()
        {
            InitializeComponent();
            PhanQuyen();
            
            // Nút chức năng phân cấp này chỉ dùng cho QLTC trở lên 
            //  Với id = 5 hoặc id = 6          
        }

        // Phải phân cấp trước khi phân quyền.

        int idquyen = CommonUser.Quyen;
        string maNV = "";

        // Kiểm tra mã nhân viên: Nếu không là QLTC và không có quản lý trực tiếp thì đưa ra thông báo


        private void PhanQuyen()
        {
            LoadControl();
            LockControl();
        }

        private void LockControl()
        {
          

            if (idquyen == 7)
            {
                mnToanQuyenTGĐ.Visible = false;
            }
            if (idquyen == 6)
            {
                mnToanQuyenTGĐ.Visible = false;
                mnToanQuyenQLCC.Visible = false;
            }
            if (idquyen == 5)
            {
                mnToanQuyenTGĐ.Visible = false;
                mnToanQuyenQLCC.Visible = false;
                mnToanQuyenQLTC.Visible = false;
            }
            if (idquyen < 5)
            {
                mnToanQuyenTGĐ.Visible = false;
                mnToanQuyenQLCC.Visible = false;
                mnToanQuyenQLTC.Visible = false;
                mnPheDuyetSC.Visible = false;
                mnXoaXacNhan.Visible = false;
                mnThemSua.Visible = false;
                mnChiXem.Visible = false;
                mnCam.Visible = false;
            }

        }

        List<string> LsIDquyenChon = new List<string>();

        private void LoadControl()
        {

            if (idquyen >= 7) // admin + Tổng giám đốc
            {
                LoadDataAllPB();
            }
            else
            {
                if (idquyen >= 6 && idquyen < 7) // QLCC: Load bộ phận
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
            treeList2.DataSource = UserDAO.Instance.GetLsvUserTreeList();
            treeList2.ExpandAll();
        }

        private void LoadDataBP(string MaBoPhan)
        {          
            treeList2.DataSource = UserDAO.Instance.GetUserOfBP(MaBoPhan);
            treeList2.ExpandAll();
        }

        private void LoadDataOnePB(string MaPB)
        {          
            treeList2.DataSource = UserDAO.Instance.GetUserOfPB(MaPB);
            treeList2.ExpandAll();
        }

        private void LoadTreeList()
        {
            //  Load danh sách quyền từ dưới bảng CSDL lên
            var Tulp1 = TablePermission.Instance.GetPermissionOfUser(frmMain.Instance.Ribbon, QlyPhanQuyenDAO.Instance.GetLsTTPQuser(maNV));
            //  var Tulp1 = TablePermission.Instance.GetPhanQuyenOfUser( QlyPhanQuyenDAO.Instance.GetLsTTPQuser(maNV));
            treeList1.DataSource = Tulp1.Item1.DefaultView;
            treeList1.ExpandAll();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            ColumSTT.Instance.CustomDrawRowIndicator(e);
        }

      

        private void mnCam_Click(object sender, EventArgs e)
        {

            //  Lấy ra List quyền được chọn:

            List<object> LstcheckedNodes = new List<object>();
            foreach (TreeListNode node in treeList1.Nodes)
            {
                if (node.CheckState == CheckState.Checked)
                {
                    LstcheckedNodes.Add(node.GetValue("ID"));
                }
                CheckChildNodes(node, LstcheckedNodes);
            }

          
            // **** Xét với mã nhân viên được chọn:

             string MaCV = UserDAO.Instance.GetUserDTO1(maNV).CHUCVU;
            int BacCV = ChucVuDAO.Instance.GetChucVuDTO(MaCV).BACCV;
            UserDTO userDTO = UserDAO.Instance.GetUserDTO1(maNV);
            string MaQLTT1 = userDTO.MAQLTT;

            //*** Yêu cầu phân cấp trước khi phân quyền chức năng:

            bool checkPC;
            if (BacCV >= 2 && MaQLTT1 == "") // *** Chưa phân cấp <==> Bậc chức vụ từ cao cấp trở xuống + chưa có cấp trên trực tiếp
            {
                checkPC = true;
            }
            else
            {
                checkPC = false;
            }
            if (checkPC)
            {
                MessageBox.Show($"Hãy phân cấp cho user {maNV} trước khi phân quyền", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (maNV == CommonUser.UserStatic.MANV)
                {
                    MessageBox.Show("Bạn không được phép phân quyền cho mình.", "Lỗi: ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    LoadTreeList();
                }
                else
                {
                    // **** Xét quyền của User đăng nhập(User đang thực hiện phân quyền) với User được chọn( User được phân quyền):
                    foreach (string item in LstcheckedNodes)
                    {
                        // duyệt trong List quyền được chọn xem có quyền phê duyệt phòng ban hay không
                        QuanLyQuyenDTO quyenDTO = QuanLyQuyenDAO.Instance.GetChiTietQuyen(0); //quyền số 0: quyền cấm 
                        QlyPhanQuyenDAO.Instance.UpdateUserRight(maNV, item, quyenDTO.IDQUYEN, quyenDTO.CHITIETQUYEN);
                    }
                }
                MessageBox.Show("Phân quyền thành công", "THÀNH CÔNG!");
                LoadTreeList();
            }
        }
    

        private void mnChiXem_Click(object sender, EventArgs e)
        {
            // LẤY RA LIST DANH SÁCH QUYỀN ĐƯỢC CHỌN

            List<object> LstcheckedNodes = new List<object>();
            foreach (TreeListNode node in treeList1.Nodes)
            {
                if (node.CheckState == CheckState.Checked)
                {
                    LstcheckedNodes.Add(node.GetValue("ID"));
                }
                CheckChildNodes(node, LstcheckedNodes);
            }

            string MaCV = UserDAO.Instance.GetUserDTO1(maNV).CHUCVU;
            int BacCV = ChucVuDAO.Instance.GetChucVuDTO(MaCV).BACCV;
            UserDTO userDTO = UserDAO.Instance.GetUserDTO1(maNV);
            string MaQLTT1 = userDTO.MAQLTT;

            //*** Yêu cầu phải phân cấp cho User trước:

            bool check;
            if (BacCV >= 2 && MaQLTT1 == "") // *** Chưa phân cấp <==> Bậc chức vụ từ cao cấp trở xuống + chưa có cấp trên trực tiếp
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (check)
            {
                MessageBox.Show($"Hãy phân cấp cho user {maNV} trước khi phân quyền", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                List<string> LsQuyenKoDcUpdate = new List<string>();
                if (maNV == CommonUser.UserStatic.MANV)
                {
                    MessageBox.Show("Bạn không được phép phân quyền cho mình.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    LoadTreeList();
                }
                else
                {
                    // duyêt trong list quyền được chọn.
                    // Chỉ có thể phân quyền nhỏ hơn quyền của người đăng nhập vào.

                    //*** Lấy List quyền của User đăng nhập vào.

                    List<QlyPhanQuyenDTO> LsQuyenUserLogon = QlyPhanQuyenDAO.Instance.GetLsTTPQuser(CommonUser.UserStatic.MANV);
                    foreach (string item in LstcheckedNodes)
                    {
                        foreach (QlyPhanQuyenDTO item1 in LsQuyenUserLogon)
                        {
                            if (item1.ID == item)
                            {
                                int quyenOfUserLogon = item1.IDQUYEN;  // quyền của trưởng phòng tại nút chức năng đó
                                if (quyenOfUserLogon >= 1)  // Nếu quyền của trưởng phòng với chức năng đó đang trên mức chỉ xem thì sẽ Update được lên quyền "chỉ xem."
                                {
                                    QuanLyQuyenDTO quyenDTO = QuanLyQuyenDAO.Instance.GetChiTietQuyen(1);           // Quyền 1: Chỉ xem
                                    QlyPhanQuyenDAO.Instance.UpdateUserRight(maNV, item, quyenDTO.IDQUYEN, quyenDTO.CHITIETQUYEN);
                                }
                                else
                                {
                                    if (LsQuyenKoDcUpdate.Contains(item))
                                    {

                                    }
                                    else
                                    {
                                        LsQuyenKoDcUpdate.Add(item);
                                    }

                                }

                            }

                        }

                    }
                    int dem = LsQuyenKoDcUpdate.Count();
                    if (dem == 0)
                    {
                        MessageBox.Show($"Phân quyền thành công");
                    }
                    else
                    {
                        MessageBox.Show($"Hoàn tất, có {dem} tính năng không thể phân quyền ' Chỉ xem' do bạn chưa đủ thẩm quyền ở chức năng này.");
                    }
                    LoadTreeList();
                }
            }
        }
                       
        private void mnThemSua_Click(object sender, EventArgs e)
        {
            // Lấy ra List quyền được chọn:

            List<object> LstcheckedNodes = new List<object>();
            foreach (TreeListNode node in treeList1.Nodes)
            {
                if (node.CheckState == CheckState.Checked)
                {
                    LstcheckedNodes.Add(node.GetValue("ID"));
                }
                CheckChildNodes(node, LstcheckedNodes);
            }

            string MaCV = UserDAO.Instance.GetUserDTO1(maNV).CHUCVU;
            int BacCV = ChucVuDAO.Instance.GetChucVuDTO(MaCV).BACCV;
            UserDTO userDTO = UserDAO.Instance.GetUserDTO1(maNV);
            string MaQLTT1 = userDTO.MAQLTT;

            //*** Yêu cầu phải phân cấp cho User trước:

            bool check;
            if (BacCV >= 2 && MaQLTT1 == "") // *** Chưa phân cấp <==> Bậc chức vụ từ cao cấp trở xuống + chưa có cấp trên trực tiếp
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (check)
            {
                MessageBox.Show($"Hãy phân cấp cho user {maNV} trước khi phân quyền", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                List<string> LsQuyenKoDcUpdate = new List<string>();
                if (maNV == CommonUser.UserStatic.MANV)
                {
                    MessageBox.Show("Bạn không được phép phân quyền cho mình.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    LoadTreeList();
                }
                else
                {
                    // duyệt trong list quyền được chọn.
                    // Chỉ có thể phân quyền nhỏ hơn quyền của người đăng nhập vào.
                    //*** Lấy List quyền của User đăng nhập vào.

                    List<QlyPhanQuyenDTO> LsQuyenUserLogon = QlyPhanQuyenDAO.Instance.GetLsTTPQuser(CommonUser.UserStatic.MANV);

                    foreach (string item in LstcheckedNodes)
                    {
                        foreach (QlyPhanQuyenDTO item1 in LsQuyenUserLogon)
                        {
                            if (item1.ID == item)
                            {
                                int quyenOfUserLogon = item1.IDQUYEN;
                                if (quyenOfUserLogon >= 2) // quyền của trưởng phòng với Nút đó lớn hơn 2 thì mới phân quyền 2( thêm sửa) cho cấp dưới
                                {
                                    QuanLyQuyenDTO quyenDTO = QuanLyQuyenDAO.Instance.GetChiTietQuyen(2);           // Quyền 2:Thêm sửa
                                    QlyPhanQuyenDAO.Instance.UpdateUserRight(maNV, item, quyenDTO.IDQUYEN, quyenDTO.CHITIETQUYEN);
                                }
                                else
                                {
                                    if (LsQuyenKoDcUpdate.Contains(item))
                                    {

                                    }
                                    else
                                    {
                                        LsQuyenKoDcUpdate.Add(item);
                                    }
                                }

                            }

                        }

                    }
                    int dem = LsQuyenKoDcUpdate.Count();
                    if (dem == 0)
                    {
                        MessageBox.Show($"Phân quyền thành công", "Thông báo:");
                    }
                    else
                    {
                        MessageBox.Show($"Hoàn tất, có {dem} tính năng không thể phân quyền 'Thêm, sửa.' do bạn chưa đủ thẩm quyền ở chức năng này. ", "Thông báo:");
                    }
                    LoadTreeList();
                }
            }
        }

        private void mnXoaXacNhan_Click(object sender, EventArgs e)
        {
            // Lấy ra List quyền được chọn:

            List<object> LstcheckedNodes = new List<object>();
            foreach (TreeListNode node in treeList1.Nodes)
            {
                if (node.CheckState == CheckState.Checked)
                {
                    LstcheckedNodes.Add(node.GetValue("ID"));
                }
                CheckChildNodes(node, LstcheckedNodes);
            }

            string MaCV = UserDAO.Instance.GetUserDTO1(maNV).CHUCVU;
            int BacCV = ChucVuDAO.Instance.GetChucVuDTO(MaCV).BACCV;
            UserDTO userDTO = UserDAO.Instance.GetUserDTO1(maNV);
            string MaQLTT1 = userDTO.MAQLTT;

            //*** Yêu cầu phải phân cấp cho User trước:

            bool check;
            if (BacCV >= 2 && MaQLTT1 == "") // *** Chưa phân cấp <==> Bậc chức vụ từ cao cấp trở xuống + chưa có cấp trên trực tiếp
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (check)
            {
                MessageBox.Show($"Hãy phân cấp cho user {maNV} trước khi phân quyền", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                List<string> LsQuyenKoDcUpdate = new List<string>();
                if (maNV == CommonUser.UserStatic.MANV)
                {
                    MessageBox.Show("Bạn không được phép phân quyền cho mình.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    LoadTreeList();
                }
                else
                {
                    // duyệt trong list quyền được chọn.
                    // Chỉ có thể phân quyền nhỏ hơn quyền của người đăng nhập vào.
                    //*** Lấy List quyền của User đăng nhập vào.

                    List<QlyPhanQuyenDTO> LsQuyenUserLogon = QlyPhanQuyenDAO.Instance.GetLsTTPQuser(CommonUser.UserStatic.MANV);

                    foreach (string item in LstcheckedNodes)
                    {
                        foreach (QlyPhanQuyenDTO item1 in LsQuyenUserLogon)
                        {
                            if (item1.ID == item)
                            {
                                int quyenOfUserLogon = item1.IDQUYEN;
                                if (quyenOfUserLogon >= 3) // quyền của trưởng phòng với Nút đó lớn hơn 3 thì mới phân quyền 3( Xoá, xác nhận.) cho cấp dưới.
                                {
                                    QuanLyQuyenDTO quyenDTO = QuanLyQuyenDAO.Instance.GetChiTietQuyen(3);           // Quyền 3: Xoá, xác nhận.
                                    QlyPhanQuyenDAO.Instance.UpdateUserRight(maNV, item, quyenDTO.IDQUYEN, quyenDTO.CHITIETQUYEN);
                                }
                                else
                                {
                                    if (LsQuyenKoDcUpdate.Contains(item))
                                    {

                                    }
                                    else
                                    {
                                        LsQuyenKoDcUpdate.Add(item);
                                    }
                                }

                            }

                        }

                    }
                    int dem = LsQuyenKoDcUpdate.Count();
                    if (dem == 0)
                    {
                        MessageBox.Show($"Phân quyền thành công", "Thông báo:");
                    }
                    else
                    {
                        MessageBox.Show($"Hoàn tất, có {dem} tính năng không thể phân quyền 'Xoá, xác nhận.' do bạn chưa đủ thẩm quyền ở chức năng này. ", "Thông báo:");
                    }
                    LoadTreeList();
                }
            }
        }

        private void mnPheDuyet_Click(object sender, EventArgs e)
        {

            // Lấy ra List quyền được chọn:

            List<object> LstcheckedNodes = new List<object>();
            foreach (TreeListNode node in treeList1.Nodes)
            {
                if (node.CheckState == CheckState.Checked)
                {
                    LstcheckedNodes.Add(node.GetValue("ID"));
                }
                CheckChildNodes(node, LstcheckedNodes);
            }

            string MaCV = UserDAO.Instance.GetUserDTO1(maNV).CHUCVU;
            int BacCV = ChucVuDAO.Instance.GetChucVuDTO(MaCV).BACCV;
            UserDTO userDTO = UserDAO.Instance.GetUserDTO1(maNV);
            string MaQLTT1 = userDTO.MAQLTT;

            //*** Yêu cầu phải phân cấp cho User trước:

            bool check;
            if (BacCV >= 2 && MaQLTT1 == "") // *** Chưa phân cấp <==> Bậc chức vụ từ cao cấp trở xuống + chưa có cấp trên trực tiếp
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (check)
            {
                MessageBox.Show($"Hãy phân cấp cho user {maNV} trước khi phân quyền", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                List<string> LsQuyenKoDcUpdate = new List<string>();
                if (maNV == CommonUser.UserStatic.MANV)
                {
                    MessageBox.Show("Bạn không được phép phân quyền cho mình.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    LoadTreeList();
                }
                else
                {
                    // duyệt trong list quyền được chọn.
                    // Chỉ có thể phân quyền nhỏ hơn quyền của người đăng nhập vào.
                    //*** Lấy List quyền của User đăng nhập vào.

                    List<QlyPhanQuyenDTO> LsQuyenUserLogon = QlyPhanQuyenDAO.Instance.GetLsTTPQuser(CommonUser.UserStatic.MANV);

                    foreach (string item in LstcheckedNodes)
                    {
                        foreach (QlyPhanQuyenDTO item1 in LsQuyenUserLogon)
                        {
                            if (item1.ID == item)
                            {
                                int quyenOfUserLogon = item1.IDQUYEN;
                                if (quyenOfUserLogon >= 4) // quyền của trưởng phòng với Nút đó lớn hơn 4 thì mới phân quyền 4( Phê duyệt sơ cấp ) cho cấp dưới.
                                {
                                    QuanLyQuyenDTO quyenDTO = QuanLyQuyenDAO.Instance.GetChiTietQuyen(4);           // Quyền 4: Phê duyệt sơ cấp
                                    QlyPhanQuyenDAO.Instance.UpdateUserRight(maNV, item, quyenDTO.IDQUYEN, quyenDTO.CHITIETQUYEN);
                                }
                                else
                                {
                                    if (LsQuyenKoDcUpdate.Contains(item))
                                    {

                                    }
                                    else
                                    {
                                        LsQuyenKoDcUpdate.Add(item);
                                    }
                                }

                            }
                        }
                    }
                    int dem = LsQuyenKoDcUpdate.Count();
                    if (dem == 0)
                    {
                        MessageBox.Show($"Phân quyền thành công", "Thông báo:");
                    }
                    else
                    {
                        MessageBox.Show($"Hoàn tất, có {dem} tính năng không thể phân quyền 'Phê duyệt sơ cấp.' do bạn chưa đủ thẩm quyền ở chức năng này. ", "Thông báo:");
                    }
                    LoadTreeList();
                }
            }
        }

      

      

        private void treeList1_TreeListMenuItemClick(object sender, DevExpress.XtraTreeList.TreeListMenuItemClickEventArgs e)
        {
            MessageBox.Show("Chuc nang dang chon la:" + treeList1.Nodes.ToString());
        }

        private void treeList1_MouseClick(object sender, MouseEventArgs e)
        {
          
        }

        private void treeList1_NodeChanged(object sender, DevExpress.XtraTreeList.NodeChangedEventArgs e)
        {
            
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            
        }


        void CheckedItemCon(TreeListNode node) // Hàm duyệt đệ quy hàm con
        {
            if (node.HasChildren)
            {
                if(node.Checked)
                {
                    LsIDquyenChon.Add(node.GetValue("ID").ToString());
                    foreach (TreeListNode item1 in node.Nodes)     // duyệt vào group rồi // duyệt vào Group 
                    {
                        item1.CheckAll(); //

                        if (item1.HasChildren)
                        {
                            LsIDquyenChon.Add(item1.GetValue("ID").ToString());
                            //CheckedItemCon(item1);
                            // MessageBox.Show(item.GetValue("ID").ToString());
                            foreach (TreeListNode item2 in item1.Nodes)
                            {
                                LsIDquyenChon.Add(item2.GetValue("ID").ToString());
                            }
                        }
                        else
                        {
                            LsIDquyenChon.Add(item1.GetValue("ID").ToString());
                        }
                        //   MessageBox.Show("Node đầu tiên được chọn là: " + item1.GetValue("ID").ToString());
                    }
                }
                else
                {
                    foreach (TreeListNode item1 in node.Nodes)     // duyệt vào group rồi // duyệt vào Group 
                    {
                        item1.UncheckAll(); //

                        if (item1.HasChildren)
                        {
                            LsIDquyenChon.Add(item1.GetValue("ID").ToString());
                            //CheckedItemCon(item1);
                            // MessageBox.Show(item.GetValue("ID").ToString());
                            foreach (TreeListNode item2 in item1.Nodes)
                            {
                                LsIDquyenChon.Add(item2.GetValue("ID").ToString());
                            }
                        }
                        else
                        {
                            LsIDquyenChon.Add(item1.GetValue("ID").ToString());
                        }
                        //   MessageBox.Show("Node đầu tiên được chọn là: " + item1.GetValue("ID").ToString());

                    }
                }
               
            }
            else
            {
                LsIDquyenChon.Add(node.GetValue("ID").ToString());
            }

        }

        // Hàm chọn nút cha chọn toàn bộ được nút con, bỏ chọn nút cha ===> bỏ chọn được toàn bộ nút con

            //Một hàm đệ quy khá lằng nhằng
        private void SetChildNodesCheckedState(TreeListNode parentNode, CheckState checkedState)
        {
            foreach (TreeListNode childNode in parentNode.Nodes)
            {
                childNode.Checked = checkedState == CheckState.Checked;
                if (childNode.HasChildren)
                {
                    SetChildNodesCheckedState(childNode, checkedState); // Sét up trạng thái nút con giống y hệt nút cha                  
                    if(childNode.Checked)
                    {
                        LsIDquyenChon.Add(childNode.GetValue("ID").ToString());
                    }
                }
            }
        }


        //Hàm lấy ra toàn bộ các Nút được trong cây

        private void CheckChildNodes(TreeListNode node, List<object> checkedNodes)
        {
            foreach (TreeListNode childNode in node.Nodes)
            {
                if (childNode.CheckState == CheckState.Checked)
                {
                    checkedNodes.Add(childNode.GetValue("ID"));
                    //checkedNodes.Add(childNode.GetValue("id")); id, code, parent_id, name lấy gì thì truyền vào đó
                }
                CheckChildNodes(childNode, checkedNodes);
            }
        }
        
        //private void simpleButton1_Click(object sender, EventArgs e)
        //{
        //    List<object> checkedNodes = new List<object>();
        //    foreach (TreeListNode node in treeList1.Nodes)
        //    {
        //        if (node.CheckState == CheckState.Checked)
        //        {
        //            checkedNodes.Add(node.GetValue("name"));
        //        }
        //        CheckChildNodes(node, checkedNodes);
        //    }

        //    // Use the checkedNodes list as needed
        //    var message = string.Join(",", checkedNodes);
        //    MessageBox.Show(message);
        //}

        private void treeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            //List<TreeListNode> lsv = treeList1.GetAllCheckedNodes();
            //foreach (TreeListNode item in lsv)
            //{
            //    CheckedItemCon(item);
            //}

            SetChildNodesCheckedState(e.Node, e.Node.CheckState);


        }

        private void mnToanQuyenQLCC_Click(object sender, EventArgs e)
        {
            // Lấy ra List quyền được chọn:

            List<object> LstcheckedNodes = new List<object>();
            foreach (TreeListNode node in treeList1.Nodes)
            {
                if (node.CheckState == CheckState.Checked)
                {
                    LstcheckedNodes.Add(node.GetValue("ID"));
                }
                CheckChildNodes(node, LstcheckedNodes);
            }

            string MaCV = UserDAO.Instance.GetUserDTO1(maNV).CHUCVU;
            int BacCV = ChucVuDAO.Instance.GetChucVuDTO(MaCV).BACCV;
            UserDTO userDTO = UserDAO.Instance.GetUserDTO1(maNV);
            string MaQLTT1 = userDTO.MAQLTT;

            //*** Yêu cầu phải phân cấp cho User trước:

            bool check;
            if (BacCV >= 2 && MaQLTT1 == "") // *** Chưa phân cấp <==> Bậc chức vụ từ cao cấp trở xuống + chưa có cấp trên trực tiếp
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (check)
            {
                MessageBox.Show($"Hãy phân cấp cho user {maNV} trước khi phân quyền", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                List<string> LsQuyenKoDcUpdate = new List<string>();
                if (maNV == CommonUser.UserStatic.MANV)
                {
                    MessageBox.Show("Bạn không được phép phân quyền cho mình.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    LoadTreeList();
                }
                else
                {
                    // duyệt trong list quyền được chọn.
                    // Chỉ có thể phân quyền nhỏ hơn quyền của người đăng nhập vào.
                    //*** Lấy List quyền của User đăng nhập vào.

                    List<QlyPhanQuyenDTO> LsQuyenUserLogon = QlyPhanQuyenDAO.Instance.GetLsTTPQuser(CommonUser.UserStatic.MANV);

                    foreach (string item in LstcheckedNodes)
                    {
                        foreach (QlyPhanQuyenDTO item1 in LsQuyenUserLogon)
                        {
                            if (item1.ID == item)
                            {
                                int quyenOfUserLogon = item1.IDQUYEN;
                                if (quyenOfUserLogon >= 6) // quyền của trưởng phòng với Nút đó lớn hơn 6 thì mới phân quyền 6(Toàn quyền QLCC) cho cấp dưới.
                                {
                                    QuanLyQuyenDTO quyenDTO = QuanLyQuyenDAO.Instance.GetChiTietQuyen(6);           // Quyền 6: Toàn quyền QLCC
                                    QlyPhanQuyenDAO.Instance.UpdateUserRight(maNV, item, quyenDTO.IDQUYEN, quyenDTO.CHITIETQUYEN);
                                }
                                else
                                {
                                    if (LsQuyenKoDcUpdate.Contains(item))
                                    {

                                    }
                                    else
                                    {
                                        LsQuyenKoDcUpdate.Add(item);
                                    }
                                }

                            }
                        }
                    }
                    int dem = LsQuyenKoDcUpdate.Count();
                    if (dem == 0)
                    {
                        MessageBox.Show($"Phân quyền thành công", "Thông báo:");
                    }
                    else
                    {
                        MessageBox.Show($"Hoàn tất, có {dem} tính năng không thể phân quyền 'Toàn quyền QLCC' do bạn chưa đủ thẩm quyền ở chức năng này. ", "Thông báo:");
                    }
                    LoadTreeList();
                }
            }

        }

        private void mnToanQuyenTGĐ_Click(object sender, EventArgs e)
        {
            // Lấy ra List quyền được chọn:

            List<object> LstcheckedNodes = new List<object>();
            foreach (TreeListNode node in treeList1.Nodes)
            {
                if (node.CheckState == CheckState.Checked)
                {
                    LstcheckedNodes.Add(node.GetValue("ID"));
                }
                CheckChildNodes(node, LstcheckedNodes);
            }

            string MaCV = UserDAO.Instance.GetUserDTO1(maNV).CHUCVU;
            int BacCV = ChucVuDAO.Instance.GetChucVuDTO(MaCV).BACCV;
            UserDTO userDTO = UserDAO.Instance.GetUserDTO1(maNV);
            string MaQLTT1 = userDTO.MAQLTT;

            //*** Yêu cầu phải phân cấp cho User trước:

            bool check;
            if (BacCV >= 2 && MaQLTT1 == "") // *** Chưa phân cấp <==> Bậc chức vụ từ cao cấp trở xuống + chưa có cấp trên trực tiếp
            {
                check = true;
            }

            else
            {
                check = false;
            }

            if (check)
            {
                MessageBox.Show($"Hãy phân cấp cho user {maNV} trước khi phân quyền", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                List<string> LsQuyenKoDcUpdate = new List<string>();
                if (maNV == CommonUser.UserStatic.MANV)
                {
                    MessageBox.Show("Bạn không được phép phân quyền cho mình.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    LoadTreeList();
                }
                else
                {
                    // duyệt trong list quyền được chọn.
                    // Chỉ có thể phân quyền nhỏ hơn quyền của người đăng nhập vào.
                    //*** Lấy List quyền của User đăng nhập vào.

                    List<QlyPhanQuyenDTO> LsQuyenUserLogon = QlyPhanQuyenDAO.Instance.GetLsTTPQuser(CommonUser.UserStatic.MANV);

                    foreach (string item in LstcheckedNodes)
                    {
                        foreach (QlyPhanQuyenDTO item1 in LsQuyenUserLogon)
                        {
                            if (item1.ID == item)
                            {
                                int quyenOfUserLogon = item1.IDQUYEN;
                                if (quyenOfUserLogon >= 7) // quyền của trưởng phòng với Nút đó lớn hơn 7 thì mới phân quyền 7(Toàn quyền TGD) cho cấp dưới.
                                {
                                    QuanLyQuyenDTO quyenDTO = QuanLyQuyenDAO.Instance.GetChiTietQuyen(7);           // Quyền 7: Toàn quyền TGĐ
                                    QlyPhanQuyenDAO.Instance.UpdateUserRight(maNV, item, quyenDTO.IDQUYEN, quyenDTO.CHITIETQUYEN);
                                }
                                else
                                {
                                    if (LsQuyenKoDcUpdate.Contains(item))
                                    {

                                    }
                                    else
                                    {
                                        LsQuyenKoDcUpdate.Add(item);
                                    }
                                }

                            }
                        }
                    }
                    int dem = LsQuyenKoDcUpdate.Count();
                    if (dem == 0)
                    {
                        MessageBox.Show($"Phân quyền thành công", "Thông báo:");
                    }
                    else
                    {
                        MessageBox.Show($"Hoàn tất, có {dem} tính năng không thể phân quyền 'Toàn quyền TGĐ' do bạn chưa đủ thẩm quyền ở chức năng này. ", "Thông báo:");
                    }
                    LoadTreeList();
                }
            }
        }

        private void mnToanQuyenQLTC_Click(object sender, EventArgs e)
        {

            // Lấy ra List quyền được chọn:

            List<object> LstcheckedNodes = new List<object>();
            foreach (TreeListNode node in treeList1.Nodes)
            {
                if (node.CheckState == CheckState.Checked)
                {
                    LstcheckedNodes.Add(node.GetValue("ID"));
                }
                CheckChildNodes(node, LstcheckedNodes);
            }

            string MaCV = UserDAO.Instance.GetUserDTO1(maNV).CHUCVU;
            int BacCV = ChucVuDAO.Instance.GetChucVuDTO(MaCV).BACCV;
            UserDTO userDTO = UserDAO.Instance.GetUserDTO1(maNV);
            string MaQLTT1 = userDTO.MAQLTT;

            //*** Yêu cầu phải phân cấp cho User trước:     

            bool check;
            if (BacCV >= 2 && MaQLTT1 == "") // *** Chưa phân cấp <==> Bậc chức vụ từ cao cấp trở xuống + chưa có cấp trên trực tiếp
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (check)
            {
                MessageBox.Show($"Hãy phân cấp cho user {maNV} trước khi phân quyền", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                List<string> LsQuyenKoDcUpdate = new List<string>();
                if (maNV == CommonUser.UserStatic.MANV)
                {
                    MessageBox.Show("Bạn không được phép phân quyền cho mình.", "Lỗi:", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    LoadTreeList();
                }
                else
                {
                    // duyệt trong list quyền được chọn.
                    // Chỉ có thể phân quyền nhỏ hơn quyền của người đăng nhập vào.
                    //*** Lấy List quyền của User đăng nhập vào.

                    List<QlyPhanQuyenDTO> LsQuyenUserLogon = QlyPhanQuyenDAO.Instance.GetLsTTPQuser(CommonUser.UserStatic.MANV);

                    foreach (string item in LstcheckedNodes)
                    {                    
                        foreach (QlyPhanQuyenDTO item1 in LsQuyenUserLogon)
                        {
                            if (item1.ID == item)
                            {
                                int quyenOfUserLogon = item1.IDQUYEN;
                                if (quyenOfUserLogon >= 5) // quyền của trưởng phòng với Nút đó lớn hơn 5 thì mới phân quyền 5(Toàn quyền QLTC) cho cấp dưới.
                                {
                                    QuanLyQuyenDTO quyenDTO = QuanLyQuyenDAO.Instance.GetChiTietQuyen(5);           // Quyền 5: Toàn quyền QLTC
                                    QlyPhanQuyenDAO.Instance.UpdateUserRight(maNV, item, quyenDTO.IDQUYEN, quyenDTO.CHITIETQUYEN);
                                }
                                else
                                {
                                    if (LsQuyenKoDcUpdate.Contains(item))
                                    {

                                    }
                                    else
                                    {
                                        LsQuyenKoDcUpdate.Add(item);
                                    }

                                }

                            }
                        }

                    }

                    int dem = LsQuyenKoDcUpdate.Count();
                    if (dem == 0)
                    {
                        MessageBox.Show($"Phân quyền thành công", "Thông báo:");
                    }
                    else
                    {
                        MessageBox.Show($"Hoàn tất, có {dem} tính năng không thể phân quyền 'Toàn quyền QLTC' do bạn chưa đủ thẩm quyền ở chức năng này. ", "Thông báo:");
                    }
                    LoadTreeList();
                }
            }
        }

        private void treeList2_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            List<TreeListNode> lsv = treeList2.GetAllCheckedNodes();
            foreach (TreeListNode item in lsv)
            {
                maNV = item.GetValue("MANV").ToString();
            }
            LoadTreeList();
        }

       
    }
 }
