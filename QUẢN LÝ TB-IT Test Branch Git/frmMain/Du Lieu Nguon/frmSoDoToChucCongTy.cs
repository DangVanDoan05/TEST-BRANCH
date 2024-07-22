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

namespace frmMain.Du_Lieu_Nguon
{
    public partial class frmSoDoToChucCongTy : DevExpress.XtraEditors.XtraForm
    {
        public frmSoDoToChucCongTy()
        {
            InitializeComponent();
        }

        private void btnThemNodeGoc_Click(object sender, EventArgs e)
        {
            bool t = false;
            if (!string.IsNullOrEmpty(txtNode.Text))
            {
                TreeNode Node = new TreeNode();
                Node.Text = txtNode.Text;
                foreach (TreeNode nodex in treeView1.Nodes)
                {
                    if (string.Equals(Node.Text, nodex.Text))
                    {
                        MessageBox.Show("Node đã tồn tại");
                        t = true;
                    }
                }
                if (t == false) treeView1.Nodes.Add(Node);
                txtNode.Clear();
                txtNode.Focus();
            }
            else
                MessageBox.Show("Node không được để trống");
        }

        private void btnThemNodeCon_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNode.Text))
            {
                if (treeView1.SelectedNode != null)
                {
                    TreeNode Subnode = new TreeNode();
                    Subnode.Text = txtNode.Text;
                    treeView1.SelectedNode.Nodes.Add(Subnode);
                    txtNode.Clear();
                    txtNode.Focus();
                }
                else
                    MessageBox.Show("Bạn chưa chọn vị trí tạo Node con");
            }
            else
                MessageBox.Show("Node không được để trống");
        }
    }
}