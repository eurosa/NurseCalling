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
    
       

        bool paused = true;
        S1 form1;

        Stopwatch[] myObjects;

        public StopWatchCshartp(S1 form)
        {
            int objectsToCreate = 64;

            myObjects = new Stopwatch[objectsToCreate];

            for (int i = 0; i < objectsToCreate; i++)
            {
                // Instantiate a new object, set it's number and
                // some other properties
                myObjects[i] = new Stopwatch();


            }
   
          
            form1 = form;
           
        }


        public void StopWatchTimer()
        {
            if (myObjects[0].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[0].ElapsedMilliseconds);
                form1.myRjButton1.Text = objTimeSpan.ToString("mm':'ss"); 
                //form1.myRjButton1.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);
               // Console.WriteLine("Running/Stop: " + stopWatchObj().IsRunning);
                Console.WriteLine("Running/Stop2: " + stopWatchObj2().IsRunning);

            }
            if (myObjects[1].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[1].ElapsedMilliseconds);
                form1.myRjButton2.Text = objTimeSpan.ToString("mm':'ss");

                Console.WriteLine("Running/Stop: " + stopWatchObj2().IsRunning);
                // form1.myRjButton2.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myObjects[2].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[2].ElapsedMilliseconds);
                form1.myRjButton3.Text = objTimeSpan.ToString("mm':'ss");
               // form1.myRjButton3.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myObjects[3].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[3].ElapsedMilliseconds);
                form1.myRjButton4.Text = objTimeSpan.ToString("mm':'ss");
               // form1.myRjButton4.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myObjects[4].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[4].ElapsedMilliseconds);
                form1.myRjButton5.Text = objTimeSpan.ToString("mm':'ss");
               // form1.myRjButton5.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myObjects[5].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[5].ElapsedMilliseconds);
                form1.myRjButton6.Text = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton6.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myObjects[6].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[6].ElapsedMilliseconds);
                form1.myRjButton7.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton7.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myObjects[7].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[7].ElapsedMilliseconds);
                form1.myRjButton8.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton8.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myObjects[8].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[8].ElapsedMilliseconds);
                form1.myRjButton9.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton9.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myObjects[9].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[9].ElapsedMilliseconds);
                form1.myRjButton10.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton10.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myObjects[10].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[10].ElapsedMilliseconds);
                form1.myRjButton11.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton11.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myObjects[11].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[11].ElapsedMilliseconds);
                form1.myRjButton12.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton12.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myObjects[12].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[12].ElapsedMilliseconds);
                form1.myRjButton13.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton13.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myObjects[13].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[13].ElapsedMilliseconds);
                form1.myRjButton14.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton14.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myObjects[14].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[14].ElapsedMilliseconds);
                form1.myRjButton15.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton15.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myObjects[15].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myObjects[15].ElapsedMilliseconds);
                form1.myRjButton16.Text = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton16.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }

        }

        public void lnkReset_LinkClicked()
        {
            form1.myRjButton1.Text = "00:00";
            myObjects[0].Reset();
        }

        public void lnkReset_LinkClicked2()
        {
            form1.myRjButton2.Text = "00:00";
            myObjects[1].Reset();
        }

        public void lnkReset_LinkClicked3()
        {
            form1.myRjButton3.Text = "00:00";
            myObjects[2].Reset();
        }

        public void lnkReset_LinkClicked4()
        {
            form1.myRjButton4.Text = "00:00";
            myObjects[3].Reset();
        }

        public void lnkReset_LinkClicked5()
        {
            form1.myRjButton2.Text = "00:00";
            myObjects[4].Reset();
        }
        public void lnkReset_LinkClicked6()
        {
            form1.myRjButton6.Text = "00:00";
            myObjects[5].Reset();
        }

        public void lnkReset_LinkClicked7()
        {
            form1.myRjButton7.Text = "00:00";
            myObjects[6].Reset();
        }

        public void lnkReset_LinkClicked8()
        {
            form1.myRjButton8.Text = "00:00";
            myObjects[7].Reset();
        }

        public void lnkReset_LinkClicked9()
        {
            form1.myRjButton9.Text = "00:00";
            myObjects[8].Reset();
        }

        public void lnkReset_LinkClicked10()
        {
            form1.myRjButton10.Text = "00:00";
            myObjects[9].Reset();
        }

        public void lnkReset_LinkClicked11()
        {
            form1.myRjButton11.Text = "00:00";
            myObjects[10].Reset();
        }

        public void lnkReset_LinkClicked12()
        {
            form1.myRjButton12.Text = "00:00";
            myObjects[11].Reset();
        }

        public void lnkReset_LinkClicked13()
        {
            form1.myRjButton13.Text = "00:00";
            myObjects[12].Reset();
        }

        public void lnkReset_LinkClicked14()
        {
            form1.myRjButton14.Text = "00:00";
            myObjects[13].Reset();
        }

        public void lnkReset_LinkClicked15()
        {
            form1.myRjButton15.Text = "00:00";
            myObjects[14].Reset();
        }

        public void lnkReset_LinkClicked16()
        { 
            form1.myRjButton16.Text = "00:00";
            myObjects[15].Reset();
        }

        public void btnStop_Click()
        {

            myObjects[0].Stop();
        }

        public void btnStop2_Click()
        {

            myObjects[1].Stop();
        }

        public void btnStop3_Click()
        {

            myObjects[2].Stop();
        }
        public void btnStop4_Click()
        {

            myObjects[3].Stop();
        }

        public void btnStop5_Click()
        {

            myObjects[4].Stop();
        }

        public void btnStop6_Click()
        {

            myObjects[5].Stop();
        }

        public void btnStop7_Click()
        {

            myObjects[6].Stop();
        }

        public void btnStop8_Click()
        {

            myObjects[7].Stop();
        }

        public void btnStop9_Click()
        {

            myObjects[8].Stop();
        }

        public void btnStop10_Click()
        {

            myObjects[9].Stop();
        }

        public void btnStop11_Click()
        {

            myObjects[10].Stop();
        }

        public void btnStop12_Click()
        {

            myObjects[11].Stop();
        }

        public void btnStop13_Click()
        {

            myObjects[12].Stop();
        }

        public void btnStop14_Click()
        {

            myObjects[13].Stop();
        }

        public void btnStop15_Click()
        {

            myObjects[14].Stop();
        }

        public void btnStop16_Click()
        {

            myObjects[15].Stop();
        }

        public void btnStart_Click()
        {

            myObjects[0].Start();

        }

        public void btnStart2_Click()
        {

            myObjects[1].Start();

        }

        public void btnStart3_Click()
        {

            myObjects[2].Start();

        }

        public void btnStart4_Click()
        {

            myObjects[3].Start();

        }
        public void btnStart5_Click()
        {

            myObjects[4].Start();

        }

        public void btnStart6_Click()
        {

            myObjects[5].Start();

        }
        public void btnStart7_Click()
        {

            myObjects[6].Start();

        }

        public void btnStart8_Click()
        {

            myObjects[7].Start();

        }
        public void btnStart9_Click()
        {

            myObjects[8].Start();

        }
        public void btnStart10_Click()
        {

            myObjects[9].Start();

        }
        public void btnStart11_Click()
        {

            myObjects[10].Start();

        }
        public void btnStart12_Click()
        {

            myObjects[11].Start();

        }
        public void btnStart13_Click()
        {

            myObjects[12].Start();

        }
        public void btnStart14_Click()
        {

            myObjects[13].Start();

        }
        public void btnStart15_Click()
        {

            myObjects[14].Start();

        }

        public void btnStart16_Click()
        {

            myObjects[15].Start();

        }

        public Stopwatch stopWatchObj()
        {

            // Returning the object
            return myObjects[0];
        }

        public Stopwatch stopWatchObj2()
        {

            // Returning the object
            return myObjects[1];
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
