using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection.Emit;

namespace NurseCalling
{
    class SystemClockTimer
    {
       
        Timer t = new Timer();
        S1 form1;

        public SystemClockTimer(S1 form)
        {
            form1 = form;
            form1.timer1.Interval = 1000;
            form1.timer1.Tick += new EventHandler(this.t_Tick);
            form1.timer1.Start();
            // LabelX x = new LabelX();
            // form1.Controls.Add(x);
            // x.Dock = DockStyle.Top;

        }

        private void t_Tick(object sender, EventArgs e)
        {
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;
            string time = "";
            if (hh < 10)
            {
                time += "0" + hh;
            }
            else
            {
                time += hh;
            }
            time += ":";

            if (mm < 10)
            {
                time += "0" + mm;
            }
            else
            {
                time += mm;
            }
            time += ":";

            if (ss < 10)
            {
                time += "0" + ss;
            }
            else
            {
                time += ss;
            }

            // form.rjButton5.Font = new Font("Arial", 90, FontStyle.Bold);

            form1.rjButton5.Text = time;

            // x.Text = "Hello | World";
            // form1.dateShow.Text = DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss");
            // form1.dateShow.Text = DateTime.Now.ToString("dddd, MMM dd yyyy");

        }
    }
}


