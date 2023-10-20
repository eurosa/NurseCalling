using NurseCalling.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseCalling
{
    class StopWatchCshartp
    {

        Stopwatch objStopWatch;
        Stopwatch objStopWatch2;
        Stopwatch objStopWatch3;
        Stopwatch objStopWatch4;
        Stopwatch objStopWatch5;
        Stopwatch objStopWatch6;
        Stopwatch objStopWatch7;
        Stopwatch objStopWatch8;
        Stopwatch objStopWatch9;
        Stopwatch objStopWatch10;
        Stopwatch objStopWatch11;
        Stopwatch objStopWatch12;
        Stopwatch objStopWatch13;
        Stopwatch objStopWatch14;
        Stopwatch objStopWatch15;
        Stopwatch objStopWatch16;

        RJButton myRjButton;
        RJButton myRjButton2;
        RJButton myRjButton3;
        RJButton myRjButton4;
        RJButton myRjButton5;
        RJButton myRjButton6;
        RJButton myRjButton7;
        RJButton myRjButton8;
        RJButton myRjButton9;
        RJButton myRjButton10;
        RJButton myRjButton11;
        RJButton myRjButton12;
        RJButton myRjButton13;
        RJButton myRjButton14;
        RJButton myRjButton15;
        RJButton myRjButton16;

        bool paused = true;
        S1 form1;
        public StopWatchCshartp(S1 form)
        {
            objStopWatch = new Stopwatch();
            objStopWatch2 = new Stopwatch();
            objStopWatch3 = new Stopwatch();
            objStopWatch4 = new Stopwatch();
            objStopWatch5 = new Stopwatch();
            objStopWatch6 = new Stopwatch();
            objStopWatch7 = new Stopwatch();
            objStopWatch8 = new Stopwatch();
            objStopWatch9 = new Stopwatch();
            objStopWatch10 = new Stopwatch();
            objStopWatch11 = new Stopwatch();
            objStopWatch12 = new Stopwatch();
            objStopWatch13 = new Stopwatch();
            objStopWatch14 = new Stopwatch();
            objStopWatch15 = new Stopwatch();
            objStopWatch16 = new Stopwatch();
            form1 = form;
            // form1.lblDisplayTime.Font = new Font("Arial", 72, FontStyle.Bold);
        }


        public void StopWatchTimer()
        {
            if (objStopWatch.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch.ElapsedMilliseconds);
                form1.myRjButton1.Text = objTimeSpan.ToString("mm':'ss"); 
                //form1.myRjButton1.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);
                System.Diagnostics.Debug.WriteLine("Running/Stop: " + stopWatchObj().IsRunning);
                
            }
            if (objStopWatch2.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch2.ElapsedMilliseconds);
                form1.myRjButton2.Text = objTimeSpan.ToString("mm':'ss");
               // form1.myRjButton2.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);
       
            }
            if (objStopWatch3.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch3.ElapsedMilliseconds);
                form1.myRjButton3.Text = objTimeSpan.ToString("mm':'ss");
               // form1.myRjButton3.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (objStopWatch4.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch4.ElapsedMilliseconds);
                form1.myRjButton4.Text = objTimeSpan.ToString("mm':'ss");
               // form1.myRjButton4.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (objStopWatch5.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch5.ElapsedMilliseconds);
                form1.myRjButton5.Text = objTimeSpan.ToString("mm':'ss");
               // form1.myRjButton5.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (objStopWatch6.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch6.ElapsedMilliseconds);
                form1.myRjButton6.Text = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton6.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (objStopWatch7.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch7.ElapsedMilliseconds);
                form1.myRjButton7.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton7.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (objStopWatch8.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch8.ElapsedMilliseconds);
                form1.myRjButton8.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton8.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (objStopWatch9.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch9.ElapsedMilliseconds);
                form1.myRjButton9.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton9.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (objStopWatch10.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch10.ElapsedMilliseconds);
                form1.myRjButton10.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton10.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (objStopWatch11.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch11.ElapsedMilliseconds);
                form1.myRjButton11.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton11.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (objStopWatch12.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch12.ElapsedMilliseconds);
                form1.myRjButton12.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton12.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (objStopWatch13.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch13.ElapsedMilliseconds);
                form1.myRjButton13.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton13.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (objStopWatch14.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch14.ElapsedMilliseconds);
                form1.myRjButton14.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton14.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (objStopWatch15.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch15.ElapsedMilliseconds);
                form1.myRjButton15.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton15.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (objStopWatch16.IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(objStopWatch16.ElapsedMilliseconds);
                form1.myRjButton16.Text = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton16.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }

        }

        public void lnkReset_LinkClicked()
        {
            form1.myRjButton1.Text = "00:00"; 
            objStopWatch.Reset();
        }

        public void lnkReset_LinkClicked2()
        {
            form1.myRjButton2.Text = "00:00:00";
            objStopWatch2.Reset();
        }

        public void lnkReset_LinkClicked3()
        {
            form1.myRjButton3.Text = "00:00:00";
            objStopWatch3.Reset();
        }

        public void lnkReset_LinkClicked4()
        {
            form1.myRjButton4.Text = "00:00:00";
            objStopWatch4.Reset();
        }

        public void lnkReset_LinkClicked5()
        {
            form1.myRjButton2.Text = "00:00:00";
            objStopWatch5.Reset();
        }
        public void lnkReset_LinkClicked6()
        {
            form1.myRjButton6.Text = "00:00:00";
            objStopWatch6.Reset();
        }

        public void lnkReset_LinkClicked7()
        {
            form1.myRjButton7.Text = "00:00:00";
            objStopWatch7.Reset();
        }

        public void lnkReset_LinkClicked8()
        {
            form1.myRjButton8.Text = "00:00:00";
            objStopWatch8.Reset();
        }

        public void lnkReset_LinkClicked9()
        {
            form1.myRjButton9.Text = "00:00:00";
            objStopWatch9.Reset();
        }

        public void lnkReset_LinkClicked10()
        {
            form1.myRjButton10.Text = "00:00:00";
            objStopWatch10.Reset();
        }

        public void lnkReset_LinkClicked11()
        {
            form1.myRjButton11.Text = "00:00:00";
            objStopWatch11.Reset();
        }

        public void lnkReset_LinkClicked12()
        {
            form1.myRjButton12.Text = "00:00:00";
            objStopWatch12.Reset();
        }

        public void lnkReset_LinkClicked13()
        {
            form1.myRjButton13.Text = "00:00:00";
            objStopWatch13.Reset();
        }

        public void lnkReset_LinkClicked14()
        {
            form1.myRjButton14.Text = "00:00:00";
            objStopWatch14.Reset();
        }

        public void lnkReset_LinkClicked15()
        {
            form1.myRjButton15.Text = "00:00:00";
            objStopWatch15.Reset();
        }

        public void lnkReset_LinkClicked16()
        {
            // form1.myRjButton16.Text = "00:00:00";
            form1.myRjButton16.Text = "00:00";
            objStopWatch16.Reset();
        }

        public void btnStop_Click()
        {
           
            objStopWatch.Stop();
        }

        public void btnStop2_Click()
        {

            objStopWatch2.Stop();
        }

        public void btnStop3_Click()
        {

            objStopWatch3.Stop();
        }
        public void btnStop4_Click()
        {

            objStopWatch4.Stop();
        }

        public void btnStop5_Click()
        {

            objStopWatch5.Stop();
        }

        public void btnStop6_Click()
        {

            objStopWatch6.Stop();
        }

        public void btnStop7_Click()
        {

            objStopWatch7.Stop();
        }

        public void btnStop8_Click()
        {

            objStopWatch8.Stop();
        }

        public void btnStop9_Click()
        {

            objStopWatch9.Stop();
        }

        public void btnStop10_Click()
        {

            objStopWatch10.Stop();
        }

        public void btnStop11_Click()
        {

            objStopWatch11.Stop();
        }

        public void btnStop12_Click()
        {

            objStopWatch12.Stop();
        }

        public void btnStop13_Click()
        {

            objStopWatch13.Stop();
        }

        public void btnStop14_Click()
        {

            objStopWatch14.Stop();
        }

        public void btnStop15_Click()
        {

            objStopWatch15.Stop();
        }

        public void btnStop16_Click()
        {

            objStopWatch16.Stop();
        }

        public void btnStart_Click()
        {
          
            objStopWatch.Start();

        }

        public void btnStart2_Click()
        {
           
            objStopWatch2.Start();

        }

        public void btnStart3_Click()
        {

            objStopWatch3.Start();

        }

        public void btnStart4_Click()
        {

            objStopWatch4.Start();

        }
        public void btnStart5_Click()
        {

            objStopWatch5.Start();

        }

        public void btnStart6_Click()
        {

            objStopWatch6.Start();

        }
        public void btnStart7_Click()
        {

            objStopWatch7.Start();

        }

        public void btnStart8_Click()
        {

            objStopWatch8.Start();

        }
        public void btnStart9_Click()
        {

            objStopWatch9.Start();

        }
        public void btnStart10_Click()
        {

            objStopWatch10.Start();

        }
        public void btnStart11_Click()
        {

            objStopWatch11.Start();

        }
        public void btnStart12_Click()
        {

            objStopWatch12.Start();

        }
        public void btnStart13_Click()
        {

            objStopWatch13.Start();

        }
        public void btnStart14_Click()
        {

            objStopWatch14.Start();

        }
        public void btnStart15_Click()
        {

            objStopWatch15.Start();

        }

        public void btnStart16_Click()
        {

            objStopWatch16.Start();

        }

        public Stopwatch stopWatchObj()
        {

            // Returning the object
            return objStopWatch;
        }

        public bool getStatus()
        {
            return paused;
        }
        public void setStatus(bool status)
        {
            paused = status;
        }

    }
}
