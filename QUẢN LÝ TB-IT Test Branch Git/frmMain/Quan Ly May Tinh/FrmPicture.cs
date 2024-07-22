using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmMain.Quan_Ly_May_Tinh
{
    public partial class frmPicture : Form
    {
        public frmPicture()
        {
            InitializeComponent();
            bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bm);
            pictureBox1.Image = bm;
           
            
        }

        Bitmap bm;
        bool pain = false;
        Graphics g ;
        Pen pen = new Pen(Color.Black, 2);
        
        Point Px = new Point();
        Point Py = new Point();

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pain = true;
            Py = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(pain==true)
            {
                Px = e.Location;
                g.DrawLine(pen, Px, Py);
               
                Py = Px;
            }
            pictureBox1.Refresh(); 
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pain = false;
        }
    }
}
