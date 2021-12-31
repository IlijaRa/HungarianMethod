using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HungarianMethod
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            progressBar1.Value = 0;
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 4;
            if(progressBar1.Value == 100)
            {
                timer1.Enabled = false;
                Form1 form = new Form1();
                form.Show();
                this.Hide();
            }
        }
        int imgNum = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            // sluzi za promenu slika logoa dok se pokrece
            pictureBox1.Image = imageList1.Images[imgNum];
            if (imgNum == imageList1.Images.Count - 1)
                imgNum = 0;
            else
                imgNum++;
        }
    }
}
