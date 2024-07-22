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
    public partial class frmGiaiTrinhMuaTBIT : DevExpress.XtraEditors.XtraForm
    {
        public frmGiaiTrinhMuaTBIT()
        {
            InitializeComponent();
        }

        // BẢNG NHÂN VIÊN CHO PHÉP ID LÀ KHÓA CHÍNH, CHO MÃ NHÂN VIÊN TRÙNG
        
        // Có tạo đơn hàng trong mục quản lý đơn hàngs cho mã giải trình mua thiết bị thì mới là hoàn tất 

        // Trong Form quản lý đơn hàng phòng ban thì có thông báo hoặc Combobox đơn hàng cần tạo là đơn hàng đang yêu cầu giải trình.

    }
}