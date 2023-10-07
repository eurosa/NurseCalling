using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NurseCalling
{
    public partial class S3 : Form
    {
        public S3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
    
            S4 s4 = new S4();
            s4.Show();
            this.Hide();
        }

        private void rjButton5_Click(object sender, EventArgs e)
        {

        }
    }
}
