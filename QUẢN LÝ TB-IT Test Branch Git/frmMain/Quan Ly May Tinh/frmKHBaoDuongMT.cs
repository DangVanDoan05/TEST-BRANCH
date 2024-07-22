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
    public partial class frmKHBaoDuongMT : DevExpress.XtraEditors.XtraForm
    {

        private static frmKHBaoDuongMT instance;

        public static frmKHBaoDuongMT Instance
        {
            get { if (instance == null) instance = new frmKHBaoDuongMT(); return frmKHBaoDuongMT.instance; }
            private set { frmKHBaoDuongMT.instance = value; }
        }

        int dem = 0;

        public frmKHBaoDuongMT()
        {
            InitializeComponent();
            LoadControl();
           
        }

       public void LoadControl()
        {
            CommonKHBD.NamChon = dtpNam.Value.ToString("yyyy");
            btDD1(CommonKHBD.NamChon);
            btDD2(CommonKHBD.NamChon);
            btDDK(CommonKHBD.NamChon);
        }

        string PhongBanChon = "";
        string ThangChon = "";
        string NamHtai = "";



       

        // lấy ra List phòng ban trong danh sách máy tính
        void btDD1(string Nam) // Hàm vẽ ra Button
        {
            int k = 0;
            int h = 0;
            int cao = 0;
            List<KeHoachBDDTO> Ls = KeHoachBDDAO.Instance.GetKH_NMvaNam("DD1", Nam);
            List<string> LsTenNutLapKH = new List<string>();
            List<string> LsTenNutHoanTat = new List<string>();
            foreach (KeHoachBDDTO item in Ls)
            {
                string TenNut = item.PB + item.THANG;
                if (!item.TRANGTHAI)
                {
                   
                    LsTenNutLapKH.Add(TenNut);
                }
                else
                {
                    LsTenNutHoanTat.Add(TenNut);
                }
            }

            List<string> LsMaPB = QuanLyMayTinhDAO.Instance.GetListMaPBDD1();
            int ChieuDoc = LsMaPB.Count;

            string[] MangPB = new string[ChieuDoc];

            int m = 0;
            foreach (string item in LsMaPB)
            {
                MangPB[m] = item;
                m++;
            }

            // Đang chưa biết tọa độ lưu như thế nào.

            for (int i = 0; i <= ChieuDoc; i++) // Chiều dọc
            {
                for (int j = 0; j < 13; j++)  // Chiều ngang
                {
                    Button bt = new Button();
                    bt.Name = i.ToString() + j.ToString();
                  //  dem++;
                    bt.Size = new Size(this.Width / 13, 20);
                    bt.Location = new Point(k, h);
                    if (pnlDD1.Controls.Find(bt.Name, true).Length != 0)
                    {
                       
                   
                           
                            pnlDD1.Controls.RemoveByKey(bt.Name);
                           // btDD1(CommonKHBD.NamChon);
                       

                    }

                    // Lấy ra List tên trong cơ sở 
                    if (i == 0 && j == 0)
                    {
                        bt.Text = "DD1";

                    }

                    if (i == 0 && j > 0)
                    {
                        bt.Text = "Tháng " + j.ToString();
                    }

                    if (i > 0 && j == 0)
                    {
                        bt.Text = MangPB[i - 1];
                    }

                    // Kiểm tra nút để đổi màu

                    if (i > 0 && j > 0)  // Xét từ hàng 1 và cột 1
                    {
                        PhongBanChon = MangPB[int.Parse(bt.Name.Substring(0, 1)) - 1];
                        int Dodai = bt.Name.Length;
                        if (Dodai >= 3)
                        {
                            ThangChon = bt.Name.Substring(1, 2);
                        }
                        else
                        {
                            ThangChon = bt.Name.Substring(1, 1);
                        }
                        string TenNUT = PhongBanChon + ThangChon;
                        if (LsTenNutLapKH.Contains(TenNUT))
                        {
                            List<KeHoachBDDTO> LsKH = KeHoachBDDAO.Instance.GetKHNMPhongThang("DD1", PhongBanChon, ThangChon, dtpNam.Value.ToString("yyyy"));
                            string NgayKH = "";

                            foreach (KeHoachBDDTO item in LsKH)
                            {
                                NgayKH = NgayKH + item.NGAY + ",";
                            }

                            int dodai = NgayKH.Length;
                            NgayKH = NgayKH.Substring(0, dodai - 1);
                            bt.Text = NgayKH;
                            bt.BackColor = btnLapKH.Appearance.BackColor;
                        }
                        else
                        {
                            if(LsTenNutHoanTat.Contains(TenNUT))
                            {
                                List<KeHoachBDDTO> LsKH = KeHoachBDDAO.Instance.GetKHNMPhongThang("DD1", PhongBanChon, ThangChon, dtpNam.Value.ToString("yyyy"));
                                string NgayKH = "";
                                foreach (KeHoachBDDTO item in LsKH)
                                {
                                    NgayKH = NgayKH + item.NGAY + ",";
                                }

                                int dodai = NgayKH.Length;

                                NgayKH = NgayKH.Substring(0, dodai - 1);
                                bt.Text = NgayKH;
                                bt.BackColor =btnHoantat.Appearance.BackColor;
                            }
                            else
                            {
                                bt.BackColor = Color.White;
                            }                          
                        }
                    }




                    pnlDD1.Controls.Add(bt);
                    k += bt.Width;
                    cao = bt.Height;


                    if (i > 0 && j > 0)
                    {
                        //PhongBanChon = MangPB[int.Parse( bt.Name.Substring(0, 1))-1];
                        //ThangChon = bt.Name.Substring(1, 1);
                        bt.Click += BtDD1_Click;
                    }

                }
                h += cao;
                k = 0;
            }           
        }

        private void BtDD1_Click(object sender, EventArgs e)
        {
            Button a = sender as Button;
          
            List<string> LsMaPB = QuanLyMayTinhDAO.Instance.GetListMaPBDD1();
            int ChieuDoc = LsMaPB.Count;

            string[] MangPB = new string[ChieuDoc];

            int m = 0;
            foreach (string item in LsMaPB)
            {
                MangPB[m] = item;
                m++;
            }

            PhongBanChon = MangPB[int.Parse(a.Name.Substring(0, 1)) - 1];
            int Dodai = a.Name.Length;
            if(Dodai>=3)
            {
                ThangChon = a.Name.Substring(1, 2);
            }
            else
            {
                ThangChon = a.Name.Substring(1, 1);
            }

            CommonKHBD.NhaMay = "DD1";
            CommonKHBD.NamChon = dtpNam.Value.ToString("yyyy");
            CommonKHBD.MaPBchon = PhongBanChon; 
            CommonKHBD.ThangChon = ThangChon; 
          
          

            frmNhapKH f = new frmNhapKH();
            f.ShowDialog();
           
             LoadControl();
            
            
        }

        void btDD2(string Nam) // Hàm vẽ ra Button
        {
            int k = 0;
            int h = 0;
            int cao = 0;

            List<KeHoachBDDTO> Ls = KeHoachBDDAO.Instance.GetKH_NMvaNam("DD2", Nam);
            List<string> LsTenNutLapKH = new List<string>();
            List<string> LsTenNutHoanTat = new List<string>(); 
            foreach (KeHoachBDDTO item in Ls)
            {
                string TenNut = item.PB + item.THANG;
                if (!item.TRANGTHAI)
                {
                   
                    LsTenNutLapKH.Add(TenNut);
                }
                else
                {
                    LsTenNutHoanTat.Add(TenNut);
                }

            }

            List<string> LsMaPB = QuanLyMayTinhDAO.Instance.GetListMaPBDD2();
            int ChieuDoc = LsMaPB.Count;

            string[] MangPB = new string[ChieuDoc];

            int m = 0;
            foreach (string item in LsMaPB)
            {
                MangPB[m] = item;
                m++;
            }

            // Đang chưa biết tọa độ lưu như thế nào.

            for (int i = 0; i <= ChieuDoc; i++) // Chiều dọc
            {
                for (int j = 0; j < 13; j++)  // Chiều ngang
                {
                    Button bt = new Button();
                    bt.Name = i.ToString() + j.ToString();
                    //  dem++;
                    bt.Size = new Size(this.Width / 13, 20);
                    bt.Location = new Point(k, h);
                    if (pnlDD2.Controls.Find(bt.Name, true).Length != 0)
                    {



                        pnlDD2.Controls.RemoveByKey(bt.Name);
                        // btDD1(CommonKHBD.NamChon);


                    }

                    // Lấy ra List tên trong cơ sở 
                    if (i == 0 && j == 0)
                    {
                        bt.Text = "DD2";

                    }

                    if (i == 0 && j > 0)
                    {
                        bt.Text = "Tháng " + j.ToString();
                    }

                    if (i > 0 && j == 0)
                    {
                        bt.Text = MangPB[i - 1];
                    }

                    // Kiểm tra nút để đổi màu

                    if (i > 0 && j > 0)  // Xét từ hàng 1 và cột 1
                    {
                        PhongBanChon = MangPB[int.Parse(bt.Name.Substring(0, 1)) - 1];
                        int Dodai = bt.Name.Length;
                        if (Dodai >= 3)
                        {
                            ThangChon = bt.Name.Substring(1, 2);
                        }
                        else
                        {
                            ThangChon = bt.Name.Substring(1, 1);
                        }

                        string TenNUT = PhongBanChon + ThangChon; // Hiển thị những ngày lập kế hoạch.
                        if (LsTenNutLapKH.Contains(TenNUT))
                        {
                            List<KeHoachBDDTO> LsKH = KeHoachBDDAO.Instance.GetKHNMPhongThang("DD2",PhongBanChon,ThangChon,dtpNam.Value.ToString("yyyy"));
                            string NgayKH = "";

                            foreach (KeHoachBDDTO item in LsKH)
                            {
                                NgayKH = NgayKH + item.NGAY + ",";
                            }

                            int dodai = NgayKH.Length;
                            NgayKH = NgayKH.Substring(0, dodai - 1);
                            bt.Text = NgayKH;
                            bt.BackColor = btnLapKH.Appearance.BackColor;
                        }
                        else
                        {
                            if(LsTenNutHoanTat.Contains(TenNUT))
                            {
                                List<KeHoachBDDTO> LsKH = KeHoachBDDAO.Instance.GetKHNMPhongThang("DD2", PhongBanChon, ThangChon, dtpNam.Value.ToString("yyyy"));
                                string NgayKH = "";

                                foreach (KeHoachBDDTO item in LsKH)
                                {
                                    NgayKH = NgayKH + item.NGAY + ",";
                                }

                                int dodai = NgayKH.Length;
                                NgayKH = NgayKH.Substring(0, dodai - 1);
                                bt.Text = NgayKH;
                                bt.BackColor = btnHoantat.Appearance.BackColor;
                            }
                            else
                            {
                                bt.BackColor = Color.White;
                            }
                            
                        }
                    }




                    pnlDD2.Controls.Add(bt);
                    k += bt.Width;
                    cao = bt.Height;


                    if (i > 0 && j > 0)
                    {
                        //PhongBanChon = MangPB[int.Parse( bt.Name.Substring(0, 1))-1];
                        //ThangChon = bt.Name.Substring(1, 1);
                        bt.Click += BtDD2_Click;
                    }

                }
                h += cao;
                k = 0;
            }
        }

        private void BtDD2_Click(object sender, EventArgs e)
        {
            Button a = sender as Button;

            List<string> LsMaPB = QuanLyMayTinhDAO.Instance.GetListMaPBDD2();
            int ChieuDoc = LsMaPB.Count;

            string[] MangPB = new string[ChieuDoc];

            int m = 0;
            foreach (string item in LsMaPB)
            {
                MangPB[m] = item;
                m++;
            }

            PhongBanChon = MangPB[int.Parse(a.Name.Substring(0, 1)) - 1];
            int Dodai = a.Name.Length;
            if (Dodai >= 3)
            {
                ThangChon = a.Name.Substring(1, 2);
            }
            else
            {
                ThangChon = a.Name.Substring(1, 1);
            }

            CommonKHBD.NhaMay = "DD2";
            CommonKHBD.NamChon = dtpNam.Value.ToString("yyyy");
            CommonKHBD.MaPBchon = PhongBanChon;
            CommonKHBD.ThangChon = ThangChon;



            frmNhapKH f = new frmNhapKH();
            f.ShowDialog();

            LoadControl();


        }

        void btDDK(string Nam) // Hàm vẽ ra Button
        {
            int k = 0;
            int h = 0;
            int cao = 0;
            List<KeHoachBDDTO> Ls = KeHoachBDDAO.Instance.GetKH_NMvaNam("DDK", Nam);
            List<string> LsTenNutLapKH = new List<string>();
            List<string> LsTenNutHoanTat = new List<string>();

            foreach (KeHoachBDDTO item in Ls)
            {
                string TenNut = item.PB + item.THANG;
                if (!item.TRANGTHAI)
                {                  
                    LsTenNutLapKH.Add(TenNut);
                }
                else
                {
                    LsTenNutHoanTat.Add(TenNut);
                }
            }

            List<string> LsMaPB = QuanLyMayTinhDAO.Instance.GetListMaPBDDK();
            int ChieuDoc = LsMaPB.Count;

            string[] MangPB = new string[ChieuDoc];

            int m = 0;
            foreach (string item in LsMaPB)
            {
                MangPB[m] = item;
                m++;
            }

            // Đang chưa biết tọa độ lưu như thế nào.

            for (int i = 0; i <= ChieuDoc; i++) // Chiều dọc
            {
                for (int j = 0; j < 13; j++)  // Chiều ngang
                {
                    Button bt = new Button();
                    bt.Name = i.ToString() + j.ToString();
                    //  dem++;
                    bt.Size = new Size(this.Width / 13, 20);
                    bt.Location = new Point(k, h);
                    if (pnlDDK.Controls.Find(bt.Name, true).Length != 0)
                    {



                        pnlDDK.Controls.RemoveByKey(bt.Name);
                        // btDD1(CommonKHBD.NamChon);


                    }

                    // Lấy ra List tên trong cơ sở 
                    if (i == 0 && j == 0)
                    {
                        bt.Text = "DDK";

                    }

                    if (i == 0 && j > 0)
                    {
                        bt.Text = "Tháng " + j.ToString();
                    }

                    if (i > 0 && j == 0)
                    {
                        bt.Text = MangPB[i - 1];
                    }

                    // Kiểm tra nút để đổi màu

                    if (i > 0 && j > 0)  // Xét từ hàng 1 và cột 1
                    {
                        PhongBanChon = MangPB[int.Parse(bt.Name.Substring(0, 1)) - 1];
                        int Dodai = bt.Name.Length;
                        if (Dodai >= 3)
                        {
                            ThangChon = bt.Name.Substring(1, 2);
                        }
                        else
                        {
                            ThangChon = bt.Name.Substring(1, 1);
                        }
                        string TenNUT = PhongBanChon + ThangChon;
                        if (LsTenNutLapKH.Contains(TenNUT))
                        {
                            List<KeHoachBDDTO> LsKH = KeHoachBDDAO.Instance.GetKHNMPhongThang("DDK", PhongBanChon, ThangChon, dtpNam.Value.ToString("yyyy"));
                            string NgayKH = "";

                            foreach (KeHoachBDDTO item in LsKH)
                            {
                                NgayKH = NgayKH + item.NGAY + ",";
                            }

                            int dodai = NgayKH.Length;
                            NgayKH = NgayKH.Substring(0, dodai - 1);
                            bt.Text = NgayKH;
                            bt.BackColor = btnLapKH.Appearance.BackColor;
                        }
                        else
                        {
                            if (LsTenNutHoanTat.Contains(TenNUT))
                            {
                                List<KeHoachBDDTO> LsKH = KeHoachBDDAO.Instance.GetKHNMPhongThang("DDK", PhongBanChon, ThangChon, dtpNam.Value.ToString("yyyy"));
                                string NgayKH = "";

                                foreach (KeHoachBDDTO item in LsKH)
                                {
                                    NgayKH = NgayKH + item.NGAY + ",";
                                }

                                int dodai = NgayKH.Length;
                                NgayKH = NgayKH.Substring(0, dodai - 1);
                                bt.Text = NgayKH;
                                bt.BackColor = btnHoantat.Appearance.BackColor;
                            }
                            else
                            {
                                bt.BackColor = Color.White;
                            }
                          

                        }


                    }




                    pnlDDK.Controls.Add(bt);
                    k += bt.Width;
                    cao = bt.Height;


                    if (i > 0 && j > 0)
                    {
                        //PhongBanChon = MangPB[int.Parse( bt.Name.Substring(0, 1))-1];
                        //ThangChon = bt.Name.Substring(1, 1);
                        bt.Click += BtDDK_Click;
                    }

                }
                h += cao;
                k = 0;
            }
        }

        private void BtDDK_Click(object sender, EventArgs e)
        {
            Button a = sender as Button;
            List<string> LsMaPB = QuanLyMayTinhDAO.Instance.GetListMaPBDDK();
            int ChieuDoc = LsMaPB.Count;

            string[] MangPB = new string[ChieuDoc];

            int m = 0;
            foreach (string item in LsMaPB)
            {
                MangPB[m] = item;
                m++;
            }

            PhongBanChon = MangPB[int.Parse(a.Name.Substring(0, 1)) - 1];
            int Dodai = a.Name.Length;
            if (Dodai >= 3)
            {
                ThangChon = a.Name.Substring(1, 2);
            }
            else
            {
                ThangChon = a.Name.Substring(1, 1);
            }

            CommonKHBD.NhaMay = "DDK";
            CommonKHBD.NamChon = dtpNam.Value.ToString("yyyy");
            CommonKHBD.MaPBchon = PhongBanChon;
            CommonKHBD.ThangChon = ThangChon;



            frmNhapKH f = new frmNhapKH();
            f.ShowDialog();

            LoadControl();


        }



    }
}