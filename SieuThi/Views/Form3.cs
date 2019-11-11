using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SieuThi.Views
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
    
        private void Form3_Load(object sender, EventArgs e)
        {
            List<String> ls = new List<String>();
            ls.Add("");
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            txtMinConF.Text = trackBar1.Value.ToString();
        }
    }
}
