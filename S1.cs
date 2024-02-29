// using EasyModbus;
using Modbus.Device;
using NurseCalling.Controls;
using NurseCalling.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using ZedGraph;

namespace NurseCalling
{
    
    public partial class S1 : Form
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
        System.Media.SoundPlayer player;
        System.Media.SoundPlayer playerMale;
        System.Media.SoundPlayer playerFemale;
        bool isSpeakerOn = true;
        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }

        GraphPane myPane;
        SQLiteConnection m_dbConnection;
        //S21 s2 = new S21();
        S2 s2 = new S2();
        S3 s3 = new S3();
        S4 s4 = new S4();
        int sec=0;
        bool toggle = true;

        dbHandler dbHandlr;
        DataModel dataModel;

        public int valueCheck;


        private int _age;

        //#1
        public event System.EventHandler AgeChanged;



        public SerialPort ComPort1, ComPort2;
        public String SerialPortName;
        private BackgroundWorker worker;

        MyToggle[] myToggle;

        SystemClockTimer systemClockTimer1;
        IModbusSerialMaster master;
       // ModbusClient modbusClient;
        ushort[] registers1;
        ushort[] registers2;
        ushort[] registers3;
        ushort[] registers4;
        //  Wrapped<int> iVal;

        Wrapped<int>[] myObjects;
        Wrapped<int>[] myObjects2;
        Wrapped<int>[] myObjects3;
        Wrapped<int>[] myObjects4;
        // StopWatchCshartp[] myStopWatchObjects;

        Stopwatch[] myStopWatchObjects;
        string[] myElapseTime;
        Rough rough;
        Rough1 rough1;
        Rough2 rough2;
        Rough3 rough3;
        public S1()
        {
         
            InitializeComponent();

     
            rough = new Rough();
            rough1 = new Rough1();
            rough2 = new Rough2();
            rough3 = new Rough3();
            this.FormClosed += MyFormClosed;
            // modbusClient = new ModbusClient("COM1");

            // Dock = DockStyle.Fill;
            AutoScroll = true;
            flowLayoutPanel1.AutoScroll = true;
            // flowLayoutPanel1.AutoScrollPosition = new Point(flowLayoutPanel1.MaximumSize.Width, 0);// flowLayoutPanel1.MaximumSize.Width;
            for (int i = 0; i < 30; i++)
            {
                // Button button = new Button() { Height = 241, Width =232 };
                // flowLayoutPanel1.Controls.Add(button);
            }

            dataModel = new DataModel();
            roundPanelWithoutTitle1.Hide();
            roundPanelWithoutTitle2.Hide();

            roundPanelWithoutTitle3.Hide();
            roundPanelWithoutTitle4.Hide();
            roundPanelWithoutTitle5.Hide();
            roundPanelWithoutTitle6.Hide();
            roundPanelWithoutTitle7.Hide();
            roundPanelWithoutTitle8.Hide();

            roundPanelWithoutTitle9.Hide();
            roundPanelWithoutTitle10.Hide();

            roundPanelWithoutTitle11.Hide();
            roundPanelWithoutTitle12.Hide();
            roundPanelWithoutTitle13.Hide();
            roundPanelWithoutTitle14.Hide();
            roundPanelWithoutTitle15.Hide();
            roundPanelWithoutTitle16.Hide();

            s2.roundPanelWithoutTitle1.Hide();
            s2.roundPanelWithoutTitle2.Hide(); 
            s2.roundPanelWithoutTitle3.Hide();
            s2.roundPanelWithoutTitle4.Hide();
            s2.roundPanelWithoutTitle5.Hide();
            s2.roundPanelWithoutTitle6.Hide();
            s2.roundPanelWithoutTitle7.Hide();
            s2.roundPanelWithoutTitle8.Hide(); 
            s2.roundPanelWithoutTitle9.Hide();
            s2.roundPanelWithoutTitle10.Hide(); 
            s2.roundPanelWithoutTitle11.Hide();
            s2.roundPanelWithoutTitle12.Hide();
            s2.roundPanelWithoutTitle13.Hide();
            s2.roundPanelWithoutTitle14.Hide();
            s2.roundPanelWithoutTitle15.Hide();
            s2.roundPanelWithoutTitle16.Hide();

            s3.roundPanelWithoutTitle1.Hide();
            s3.roundPanelWithoutTitle2.Hide(); 
            s3.roundPanelWithoutTitle3.Hide();
            s3.roundPanelWithoutTitle4.Hide();
            s3.roundPanelWithoutTitle5.Hide();
            s3.roundPanelWithoutTitle6.Hide();
            s3.roundPanelWithoutTitle7.Hide();
            s3.roundPanelWithoutTitle8.Hide(); 
            s3.roundPanelWithoutTitle9.Hide();
            s3.roundPanelWithoutTitle10.Hide(); 
            s3.roundPanelWithoutTitle11.Hide();
            s3.roundPanelWithoutTitle12.Hide();
            s3.roundPanelWithoutTitle13.Hide();
            s3.roundPanelWithoutTitle14.Hide();
            s3.roundPanelWithoutTitle15.Hide();
            s3.roundPanelWithoutTitle16.Hide();

            s4.roundPanelWithoutTitle1.Hide();
            s4.roundPanelWithoutTitle2.Hide();
            s4.roundPanelWithoutTitle3.Hide();
            s4.roundPanelWithoutTitle4.Hide();
            s4.roundPanelWithoutTitle5.Hide();
            s4.roundPanelWithoutTitle6.Hide();
            s4.roundPanelWithoutTitle7.Hide();
            s4.roundPanelWithoutTitle8.Hide();
            s4.roundPanelWithoutTitle9.Hide();
            s4.roundPanelWithoutTitle10.Hide();
            s4.roundPanelWithoutTitle11.Hide();
            s4.roundPanelWithoutTitle12.Hide();
            s4.roundPanelWithoutTitle13.Hide();
            s4.roundPanelWithoutTitle14.Hide();
            s4.roundPanelWithoutTitle15.Hide();
            s4.roundPanelWithoutTitle16.Hide();

            //s4.Show();
            /*roundPanelWithoutTitle1.Hide();
            roundPanelWithoutTitle2.Hide();

            roundPanelWithoutTitle3.Hide();
            roundPanelWithoutTitle4.Hide();
            roundPanelWithoutTitle5.Hide();
            roundPanelWithoutTitle6.Hide();
            roundPanelWithoutTitle7.Hide();
            roundPanelWithoutTitle8.Hide();

            roundPanelWithoutTitle9.Hide();
            roundPanelWithoutTitle10.Hide();

            roundPanelWithoutTitle11.Hide();
            roundPanelWithoutTitle12.Hide();
            roundPanelWithoutTitle13.Hide();
            roundPanelWithoutTitle14.Hide();
            roundPanelWithoutTitle15.Hide();
            roundPanelWithoutTitle16.Hide();*/


            int objectsToCreate = 64;
            int objectsToCreate1 = 64;
            int objectsToCreate2 = 16;
            int objectsToCreate3 = 16;
            int objectsToCreate4 = 16;
            // Create an array to hold all your objects
            myObjects = new Wrapped<int>[objectsToCreate1];

            myObjects2 = new Wrapped<int>[objectsToCreate2];

            myObjects3 = new Wrapped<int>[objectsToCreate3];

            myObjects4 = new Wrapped<int>[objectsToCreate4];

            myElapseTime = new string[objectsToCreate];

           // myStopWatchObjects = new StopWatchCshartp[objectsToCreate];

            for (int i = 0; i < objectsToCreate1; i++)
            {
                // Instantiate a new object, set it's number and
                // some other properties
                myObjects[i] = new Wrapped<int>();


            }

            for (int i = 0; i < objectsToCreate2; i++)
            {
                // Instantiate a new object, set it's number and
                // some other properties
                myObjects2[i] = new Wrapped<int>();


            }

            for (int i = 0; i < objectsToCreate3; i++)
            {
                // Instantiate a new object, set it's number and
                // some other properties
                myObjects3[i] = new Wrapped<int>();


            }

            for (int i = 0; i < objectsToCreate4; i++)
            {
                // Instantiate a new object, set it's number and
                // some other properties
                myObjects4[i] = new Wrapped<int>();


            }

            for (int i = 0; i < objectsToCreate; i++)
            {
                // Instantiate a new object, set it's number and
                // some other properties
              //  myToggle[i] = new MyToggle();


            }


            for (int i = 0; i < objectsToCreate; i++)
            {
                // Instantiate a new object, set it's number and
                // some other properties
              //  myStopWatchObjects[i] = new StopWatchCshartp(this);


            }

           

            int[] registerDt1 = { myObjects[0].Value, myObjects[1].Value, myObjects[2].Value, myObjects[3].Value, myObjects[4].Value, myObjects[5].Value, myObjects[6].Value, myObjects[7].Value, myObjects[8].Value, myObjects[9].Value, myObjects[10].Value, myObjects[11].Value, myObjects[12].Value, myObjects[13].Value, myObjects[14].Value, myObjects[15].Value };
            int[] registerDt2 = { myObjects[16].Value, myObjects[17].Value, myObjects[18].Value, myObjects[19].Value, myObjects[20].Value, myObjects[21].Value, myObjects[22].Value, myObjects[23].Value, myObjects[24].Value, myObjects[25].Value, myObjects[26].Value, myObjects[27].Value, myObjects[28].Value, myObjects[29].Value, myObjects[30].Value, myObjects[31].Value };
            int[] registerDt3 = { myObjects[32].Value, myObjects[33].Value, myObjects[34].Value, myObjects[35].Value, myObjects[36].Value, myObjects[37].Value, myObjects[38].Value, myObjects[39].Value, myObjects[40].Value, myObjects[41].Value, myObjects[42].Value, myObjects[43].Value, myObjects[44].Value, myObjects[45].Value, myObjects[46].Value, myObjects[47].Value };
            int[] registerDt4 = { myObjects[48].Value, myObjects[49].Value, myObjects[50].Value, myObjects[51].Value, myObjects[52].Value, myObjects[53].Value, myObjects[54].Value, myObjects[55].Value, myObjects[56].Value, myObjects[57].Value, myObjects[58].Value, myObjects[59].Value, myObjects[60].Value, myObjects[61].Value, myObjects[62].Value, myObjects[63].Value };


            checkStatus();




           /* this.Controls.Add(s2.panel1);
            this.Controls.Add(s3.panel1);
            this.Controls.Add(s4.panel1);
            this.panel1.Show();*/

            //this.panel1.Controls.Add(s2.flowLayoutPanel1);
           // this.panel1.Controls.Add(s3.flowLayoutPanel1);
            //this.panel1.Controls.Add(s4.flowLayoutPanel1);

            dbHandlr = new dbHandler();
            dbHandlr.createDB(dataModel);
            try
            {
                //   stopWatchCshartp1 = new StopWatchCshartp(this);

            }
            catch (Exception ex) { }

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=dscp.sqlite;Version=3;");
                m_dbConnection.Open();
            }
            catch (Exception ex)
            {

            }
            dbHandlr.GetDisplayTxt(m_dbConnection, dataModel);
            dbHandlr.getGeneralData(m_dbConnection, dataModel);
            //  systemClockTimer1 =  new SystemClockTimer(this);

            label3.Text = dataModel.textBox1Display;

           // blinkLabel();

            connect1();

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            // timer.Elapsed += timer_Elapsed1;
            // timer.Start();

            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;  //Tell the user how the process went
            worker.ProgressChanged += backgroundWorker1_ProgressChanged;
            worker.RunWorkerAsync();
            worker.WorkerReportsProgress = true;
            System.Timers.Timer timer1 = new System.Timers.Timer(500);
            timer1.Elapsed += timer_Elapsed;
            timer1.Start();


            

            

            
            dbHandlr.getSettingData(m_dbConnection, dataModel);

            dbHandlr.getImage(m_dbConnection, dataModel);

            myStopWatchObjects = new Stopwatch[objectsToCreate];

            for (int i = 0; i < objectsToCreate; i++)
            {
                // Instantiate a new object, set it's number and
                // some other properties
                myStopWatchObjects[i] = new Stopwatch();


            }

            setImageToButton();

            PreventSleep();

            player = new System.Media.SoundPlayer();
            playerMale = new System.Media.SoundPlayer();
            playerFemale = new System.Media.SoundPlayer();
        }

       public void PreventSleep()
        {
            // Prevent Idle-to-Sleep (monitor not affected) (see note above)
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_AWAYMODE_REQUIRED | EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
        }

        void MyFormClosed(object sender, FormClosedEventArgs e)
        {
            AutoSaveOnClose();
            Console.WriteLine("FormClosed Event");
        }
        public void setImageToButton() {
            if (dataModel.checkBoxRegister1)
            {
                rough.bButton1.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough.bButton1.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
         if (dataModel.checkBoxRegister2)
            {
                rough.bButton2.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough.bButton2.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
         if (dataModel.checkBoxRegister3)
            {
                rough.bButton3.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough.bButton3.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
         if (dataModel.checkBoxRegister4)
            {
                rough.bButton4.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough.bButton4.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
         if (dataModel.checkBoxRegister5)
            {
                rough.bButton5.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough.bButton5.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
         if (dataModel.checkBoxRegister6)
            {
                rough.bButton6.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough.bButton6.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
         if (dataModel.checkBoxRegister7)
            {
                rough.bButton7.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough.bButton7.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
         if (dataModel.checkBoxRegister8)
            {
                rough.bButton8.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough.bButton8.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
         if (dataModel.checkBoxRegister9)
            {
                rough.bButton9.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough.bButton9.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
         if (dataModel.checkBoxRegister10)
            {
                rough.bButton10.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough.bButton10.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
         if (dataModel.checkBoxRegister11)
            {
                rough.bButton11.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough.bButton11.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
         if (dataModel.checkBoxRegister12)
            {
                rough.bButton12.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough.bButton12.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
         if (dataModel.checkBoxRegister13)
            {
                rough.bButton13.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough.bButton13.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
         if (dataModel.checkBoxRegister14)
            {
                rough.bButton14.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough.bButton14.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
         if (dataModel.checkBoxRegister15)
            {
                rough.bButton15.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough.bButton15.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }

            if (dataModel.checkBoxRegister16)
            {
                rough.bButton16.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough.bButton16.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }




            if (dataModel.checkBoxRegister17)
            {
                rough1.bButton1.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton1.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
            if (dataModel.checkBoxRegister18)
            {
                rough1.bButton2.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton2.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
            if (dataModel.checkBoxRegister19)
            {
                rough1.bButton3.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton3.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
            if (dataModel.checkBoxRegister20)
            {
                rough1.bButton4.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton4.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
            if (dataModel.checkBoxRegister21)
            {
                rough1.bButton5.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton5.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
            if (dataModel.checkBoxRegister22)
            {
                rough1.bButton6.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton6.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
            if (dataModel.checkBoxRegister23)
            {
                rough1.bButton7.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton7.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
            if (dataModel.checkBoxRegister24)
            {
                rough1.bButton8.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton8.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
            if (dataModel.checkBoxRegister25)
            {
                rough1.bButton9.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton9.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
            if (dataModel.checkBoxRegister26)
            {
                rough1.bButton10.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton10.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
            if (dataModel.checkBoxRegister27)
            {
                rough1.bButton11.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton11.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
            if (dataModel.checkBoxRegister28)
            {
                rough1.bButton12.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton12.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
            if (dataModel.checkBoxRegister29)
            {
                rough1.bButton13.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton13.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }

            if (dataModel.checkBoxRegister30)
            {
                rough1.bButton14.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton14.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }

            if (dataModel.checkBoxRegister31)
            {
                rough1.bButton15.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton15.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }
            if (dataModel.checkBoxRegister32)
            {
                rough1.bButton16.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough1.bButton16.BackgroundImage = Image.FromFile(dataModel.bed_image);
            }



            if (dataModel.checkBoxRegister33)
            {
                rough2.bButton1.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton1.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            if (dataModel.checkBoxRegister34)
            {
                rough2.bButton2.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton2.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            if (dataModel.checkBoxRegister35)
            {
                rough2.bButton3.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton3.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            if (dataModel.checkBoxRegister36)
            {
                rough2.bButton4.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton4.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            if (dataModel.checkBoxRegister37)
            {
                rough2.bButton5.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton5.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            if (dataModel.checkBoxRegister38)
            {
                rough2.bButton6.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton6.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            if (dataModel.checkBoxRegister39)
            {
                rough2.bButton7.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton7.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            if (dataModel.checkBoxRegister40)
            {
                rough2.bButton8.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton8.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            if (dataModel.checkBoxRegister41)
            {
                rough2.bButton9.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton9.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            if (dataModel.checkBoxRegister42)
            {
                rough2.bButton10.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton10.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            if (dataModel.checkBoxRegister43)
            {
                rough2.bButton11.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton11.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            if (dataModel.checkBoxRegister44)
            {
                rough2.bButton12.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton12.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            if (dataModel.checkBoxRegister45)
            {
                rough2.bButton13.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton13.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
             if (dataModel.checkBoxRegister46)
            {
                rough2.bButton14.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton14.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
             if (dataModel.checkBoxRegister47)
            {
                rough2.bButton15.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton15.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
             if (dataModel.checkBoxRegister48)
            {
                rough2.bButton16.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else {
                rough2.bButton16.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }



            if (dataModel.checkBoxRegister49)
            {
                rough3.bButton1.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton1.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            
            if (dataModel.checkBoxRegister50)
            {
                rough3.bButton2.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton2.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            
            if (dataModel.checkBoxRegister51)
            {
                rough3.bButton3.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton3.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            
            if (dataModel.checkBoxRegister52)
            {
                rough3.bButton4.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton4.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            
            if (dataModel.checkBoxRegister53)
            {
                rough3.bButton5.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton5.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            
            if (dataModel.checkBoxRegister54)
            {
                rough3.bButton6.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton6.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            
            if (dataModel.checkBoxRegister55)
            {
                rough3.bButton7.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton7.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            
            if (dataModel.checkBoxRegister56)
            {
                rough3.bButton8.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton8.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            
            if (dataModel.checkBoxRegister57)
            {
                rough3.bButton9.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton9.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            
            if (dataModel.checkBoxRegister58)
            {
                rough3.bButton10.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton10.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            
            if (dataModel.checkBoxRegister59)
            {
                rough3.bButton11.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton11.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            
            if (dataModel.checkBoxRegister60)
            {
                rough3.bButton12.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton12.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            
            if (dataModel.checkBoxRegister61)
            {
                rough3.bButton13.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton13.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            
            if (dataModel.checkBoxRegister62)
            {
                rough3.bButton14.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton14.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            
            if (dataModel.checkBoxRegister63)
            {
                rough3.bButton15.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton15.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
            
            if (dataModel.checkBoxRegister64)
            {
                rough3.bButton16.BackgroundImage = Image.FromFile(dataModel.toilet_image);
            }
            else
            {
                rough3.bButton16.BackgroundImage = Image.FromFile(dataModel.bed_image);

            }
 
        }


        //#2
        protected virtual void OnAgeChanged()
        {
          //  if (AgeChanged != null) AgeChanged(this, EventArgs.Empty);
            Console.WriteLine("myage", (Age));
        }

        public int Age
        {
            get
            {
                return _age;
            }

            set
            {
                //#3
                _age = value;
                OnAgeChanged();
            }
        }

        void timer_Elapsed1(object sender, System.Timers.ElapsedEventArgs e)
        {
            
           /// BytesToRead();
             
        }


        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (!worker.IsBusy)
                    worker.RunWorkerAsync();
            }
            catch (Exception ex) { }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("background interval running every 1 second");
            //whatever You want the background thread to do...
            try
            {
                BytesToRead();

            }
            catch (Exception Ex) { }
        }


        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
           // if (dataModel.checkBoxHub1 || dataModel.checkBoxHub2 || dataModel.checkBoxHub3 || dataModel.checkBoxHub4)
           // {
                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {

                        string hex_add = "0x0001";// "0x00A4";
                        ushort dec_add = Convert.ToUInt16(hex_add, 16);
                        ushort startAddress1 = dec_add;
                        ushort startAddress2 = dec_add;
                        ushort startAddress3 = dec_add;
                        ushort startAddress4 = dec_add;
                        ushort numRegisters = 16;



                        for (int i = 0; i < numRegisters; i++)
                        {

                            Console.WriteLine("Register {0}={1}", startAddress1 + i, registers1[i]);
                            // Console.WriteLine("Register {0}={1}", startAddress2 + i, registers1[i]);
                            // Console.WriteLine("Register {0}={1}", startAddress3 + i, registers1[i]);
                            // Console.WriteLine("Register {0}={1}", startAddress4 + i, registers1[i]);

                            // Console.WriteLine("Welcome New User " + Properties.Settings.Default.FirstCallTime); 

                            /* if ((startAddress + i) == 1)
                             {
                                // Age = (int)registers[i];
                                // roundPanelWithoutTitle1.Location = new System.Drawing.Point(this.roundPanelWithoutTitle1.Location.X, this.roundPanelWithoutTitle1.Location.Y);
                                myObjects[i].Value = (int)registers1[i];
                                rough.rjButton1.Text = registers1[i].ToString();


                             }
                             if ((startAddress + i) == 2)
                             {
                                 // Age = (int)registers[i];
                                 myObjects[i].Value = (int)registers1[i];
                                 rough.rjButton2.Text = registers1[i].ToString();

                             }*/

                           // if (dataModel.checkBoxHub1)
                           // {

                                switch ((startAddress1 + i))
                                {

                                    case 1:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton1.Text = dataModel.textBoxRegist1;// registers1[i].ToString();
                                                                                        //CallValueSet(rough.rjButton1, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist1);
                                        break;
                                    case 2:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton2.Text = dataModel.textBoxRegist2;// registers1[i].ToString();
                                                                                        //CallValueSet(rough.rjButton2, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist2);
                                        break;
                                    case 3:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton3.Text = dataModel.textBoxRegist3;// registers1[i].ToString();
                                                                                        //CallValueSet(rough.rjButton3, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist3);
                                        break;
                                    case 4:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton4.Text = dataModel.textBoxRegist4;// registers1[i].ToString();
                                                                                        //CallValueSet(rough.rjButton4, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist4);
                                        break;
                                    case 5:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton5.Text = dataModel.textBoxRegist5;// registers1[i].ToString();
                                                                                        //CallValueSet(rough.rjButton5, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist5);
                                        break;
                                    case 6:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton6.Text = dataModel.textBoxRegist6;// registers1[i].ToString();
                                                                                        //CallValueSet(rough.rjButton6, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist6);
                                        break;
                                    case 7:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton7.Text = dataModel.textBoxRegist7;// registers1[i].ToString();
                                                                                        //CallValueSet(rough.rjButton7, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist7);
                                        break;
                                    case 8:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton8.Text = dataModel.textBoxRegist8;// registers1[i].ToString();
                                                                                        //CallValueSet(rough.rjButton8, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist8);
                                        break;
                                    case 9:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton9.Text = dataModel.textBoxRegist9;// registers1[i].ToString();
                                                                                        //CallValueSet(rough.rjButton9, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist9);
                                        break;
                                    case 10:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton10.Text = dataModel.textBoxRegist10;// registers1[i].ToString();
                                                                                          //CallValueSet(rough.rjButton10, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist10);
                                        break;
                                    case 11:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton11.Text = dataModel.textBoxRegist11;// registers1[i].ToString();
                                                                                          //CallValueSet(rough.rjButton11, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist11);
                                        break;
                                    case 12:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton12.Text = dataModel.textBoxRegist12;// registers1[i].ToString();
                                                                                          //CallValueSet(rough.rjButton12, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist12);
                                        break;
                                    case 13:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton13.Text = dataModel.textBoxRegist13;// registers1[i].ToString();
                                                                                          //CallValueSet(rough.rjButton13, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist13);
                                        break;
                                    case 14:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton14.Text = dataModel.textBoxRegist14;// registers1[i].ToString();
                                                                                          //CallValueSet(rough.rjButton14, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist14);
                                        break;
                                    case 15:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton15.Text = dataModel.textBoxRegist15;// registers1[i].ToString();
                                                                                          //CallValueSet(rough.rjButton15, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist15);
                                        break;
                                    case 16:
                                        myObjects[i].Value = (int)registers1[i];
                                        rough.rjButton16.Text = dataModel.textBoxRegist16;// registers1[i].ToString();
                                                                                          //CallValueSet(rough.rjButton16, (int)registers1[i], (startAddress1 + i), dataModel.textBoxRegist16);
                                        break;
                               // }

                            }

                          //  if (dataModel.checkBoxHub2)
                           // {

                                Console.WriteLine("My_val 2 " + (int)registers2[i] + " " + myObjects2[i].Value);
                                // ++++++++++++++++++++++++++++++++++++ s2 +++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                switch ((startAddress2 + i))
                                {
                                    case 1:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton1.Text = dataModel.textBoxRegist17;// registers2[i].ToString();
                                                                                          //CallValueSet(rough1.rjButton1, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist17);
                                        break;
                                    case 2:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton2.Text = dataModel.textBoxRegist18;// registers2[i].ToString();
                                                                                          // CallValueSet(rough1.rjButton2, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist18);
                                        break;
                                    case 3:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton3.Text = dataModel.textBoxRegist19;// registers2[i].ToString();
                                                                                          //CallValueSet(rough1.rjButton3, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist19);
                                        break;
                                    case 4:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton4.Text = dataModel.textBoxRegist20;// registers2[i].ToString();
                                                                                          //CallValueSet(rough1.rjButton4, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist20);
                                        break;
                                    case 5:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton5.Text = dataModel.textBoxRegist21;// registers2[i].ToString();
                                                                                          //CallValueSet(rough1.rjButton5, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist21);
                                        break;
                                    case 6:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton6.Text = dataModel.textBoxRegist22;// registers2[i].ToString();
                                                                                          //CallValueSet(rough1.rjButton6, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist22);
                                        break;
                                    case 7:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton7.Text = dataModel.textBoxRegist23;// registers2[i].ToString();
                                                                                          //CallValueSet(rough1.rjButton7, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist23);
                                        break;
                                    case 8:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton8.Text = dataModel.textBoxRegist24;// registers2[i].ToString();
                                                                                          //CallValueSet(rough1.rjButton8, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist24);
                                        break;
                                    case 9:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton9.Text = dataModel.textBoxRegist25;// registers2[i].ToString();
                                                                                          //CallValueSet(rough1.rjButton9, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist25);
                                        break;
                                    case 10:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton10.Text = dataModel.textBoxRegist26;// registers2[i].ToString();
                                                                                           //CallValueSet(rough1.rjButton10, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist26);
                                        break;
                                    case 11:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton11.Text = dataModel.textBoxRegist27;// registers2[i].ToString();
                                                                                           //CallValueSet(rough1.rjButton11, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist27);
                                        break;
                                    case 12:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton12.Text = dataModel.textBoxRegist28;// registers2[i].ToString();
                                                                                           //CallValueSet(rough1.rjButton12, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist28);
                                        break;
                                    case 13:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton13.Text = dataModel.textBoxRegist29;// registers2[i].ToString();
                                                                                           //CallValueSet(rough1.rjButton13, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist29);
                                        break;
                                    case 14:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton14.Text = dataModel.textBoxRegist30;// registers2[i].ToString();
                                                                                           //CallValueSet(rough1.rjButton14, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist30);
                                        break;
                                    case 15:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton15.Text = dataModel.textBoxRegist31;// registers2[i].ToString();
                                                                                           //CallValueSet(rough1.rjButton15, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist31);
                                        break;
                                    case 16:
                                        myObjects2[i].Value = (int)registers2[i];
                                        rough1.rjButton16.Text = dataModel.textBoxRegist32;// registers2[i].ToString();
                                                                                           //CallValueSet(rough1.rjButton16, (int)registers2[i], (startAddress2 + i), dataModel.textBoxRegist32);
                                        break;

                                }

                           // }
                            // ++++++++++++++++++++++++++++++++++++ s3 +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                           // if (dataModel.checkBoxHub3)
                          //  {


                                switch ((startAddress3 + i))
                                {
                                    case 1:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton1.Text = dataModel.textBoxRegist33;// registers3[i].ToString();
                                                                                          //CallValueSet(rough2.rjButton1, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist33);
                                        break;
                                    case 2:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton2.Text = dataModel.textBoxRegist34;// registers3[i].ToString();
                                                                                          //CallValueSet(rough2.rjButton2, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist34);
                                        break;
                                    case 3:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton3.Text = dataModel.textBoxRegist35;// registers3[i].ToString();
                                                                                          //CallValueSet(rough2.rjButton3, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist35);
                                        break;
                                    case 4:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton4.Text = dataModel.textBoxRegist36;// registers3[i].ToString();
                                                                                          //CallValueSet(rough2.rjButton4, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist36);
                                        break;
                                    case 5:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton5.Text = dataModel.textBoxRegist37;// registers3[i].ToString();
                                                                                          //CallValueSet(rough2.rjButton5, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist37);
                                        break;
                                    case 6:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton6.Text = dataModel.textBoxRegist38;// registers3[i].ToString();
                                                                                          //CallValueSet(rough2.rjButton6, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist38);
                                        break;
                                    case 7:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton7.Text = dataModel.textBoxRegist39;// registers3[i].ToString();
                                                                                          //CallValueSet(rough2.rjButton7, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist39);
                                        break;
                                    case 8:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton8.Text = dataModel.textBoxRegist40;// registers3[i].ToString();
                                                                                          //CallValueSet(rough2.rjButton8, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist40);
                                        break;
                                    case 9:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton9.Text = dataModel.textBoxRegist41;// registers3[i].ToString();
                                                                                          //CallValueSet(rough2.rjButton9, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist41);
                                        break;
                                    case 10:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton10.Text = dataModel.textBoxRegist42;// registers3[i].ToString();
                                                                                           //CallValueSet(rough2.rjButton10, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist42);
                                        break;
                                    case 11:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton11.Text = dataModel.textBoxRegist43;// registers3[i].ToString();
                                                                                           //CallValueSet(rough2.rjButton11, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist43);
                                        break;
                                    case 12:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton12.Text = dataModel.textBoxRegist44;// registers3[i].ToString();
                                                                                           //CallValueSet(rough2.rjButton12, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist44);
                                        break;
                                    case 13:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton13.Text = dataModel.textBoxRegist45;// registers3[i].ToString();
                                                                                           //CallValueSet(rough2.rjButton13, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist45);
                                        break;
                                    case 14:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton14.Text = dataModel.textBoxRegist46;// registers3[i].ToString();
                                                                                           //CallValueSet(rough2.rjButton14, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist46);
                                        break;
                                    case 15:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton15.Text = dataModel.textBoxRegist47;// registers3[i].ToString();
                                                                                           //CallValueSet(rough2.rjButton15, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist47);
                                        break;
                                    case 16:
                                        myObjects3[i].Value = (int)registers3[i];
                                        rough2.rjButton16.Text = dataModel.textBoxRegist48;// registers3[i].ToString();
                                                                                           //CallValueSet(rough2.rjButton16, (int)registers3[i], (startAddress3 + i), dataModel.textBoxRegist48);
                                        break;
                                }

                          //  }
                          //  if (dataModel.checkBoxHub4)
                            //{

                                Console.WriteLine("Register 4 value: " + (int)registers2[i] + " " + myObjects2[i].Value);
                                // ++++++++++++++++++++++++++++++++++++ s4 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                switch ((startAddress4 + i))
                                {
                                    case 1:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton1.Text = dataModel.textBoxRegist49;// registers4[i].ToString();
                                                                                          //CallValueSet(rough3.rjButton1, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist49);
                                        break;
                                    case 2:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton2.Text = dataModel.textBoxRegist50;// registers4[i].ToString();
                                                                                          //CallValueSet(rough3.rjButton2, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist50);
                                        break;
                                    case 3:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton3.Text = dataModel.textBoxRegist51;// registers4[i].ToString();
                                                                                          //CallValueSet(rough3.rjButton3, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist51);
                                        break;
                                    case 4:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton4.Text = dataModel.textBoxRegist52;// registers4[i].ToString();
                                                                                          //CallValueSet(rough3.rjButton4, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist52);
                                        break;
                                    case 5:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton5.Text = dataModel.textBoxRegist53;// registers4[i].ToString();
                                                                                          //CallValueSet(rough3.rjButton5, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist53);
                                        break;
                                    case 6:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton6.Text = dataModel.textBoxRegist54;// registers4[i].ToString();
                                                                                          //CallValueSet(rough3.rjButton6, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist54);
                                        break;
                                    case 7:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton7.Text = dataModel.textBoxRegist55;// registers4[i].ToString();
                                                                                          //CallValueSet(rough3.rjButton7, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist55);
                                        break;
                                    case 8:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton8.Text = dataModel.textBoxRegist56;// registers4[i].ToString();
                                                                                          //CallValueSet(rough3.rjButton8, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist56);
                                        break;
                                    case 9:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton9.Text = dataModel.textBoxRegist57;// registers4[i].ToString();
                                                                                          //CallValueSet(rough3.rjButton9, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist57);
                                        break;
                                    case 10:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton10.Text = dataModel.textBoxRegist58;// registers4[i].ToString();
                                                                                           //CallValueSet(rough3.rjButton10, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist58);
                                        break;
                                    case 11:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton11.Text = dataModel.textBoxRegist59;// registers4[i].ToString();
                                                                                           //CallValueSet(rough3.rjButton11, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist59);
                                        break;
                                    case 12:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton12.Text = dataModel.textBoxRegist60;// registers4[i].ToString();
                                                                                           //CallValueSet(rough3.rjButton12, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist60);
                                        break;
                                    case 13:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton13.Text = dataModel.textBoxRegist61;// registers4[i].ToString();
                                                                                           //CallValueSet(rough3.rjButton13, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist61);
                                        break;
                                    case 14:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton14.Text = dataModel.textBoxRegist62;// registers4[i].ToString();
                                                                                           //CallValueSet(rough3.rjButton14, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist62);
                                        break;
                                    case 15:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton15.Text = dataModel.textBoxRegist63;// registers4[i].ToString();
                                                                                           //CallValueSet(rough3.rjButton15, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist63);
                                        break;
                                    case 16:
                                        myObjects4[i].Value = (int)registers4[i];
                                        rough3.rjButton16.Text = dataModel.textBoxRegist64;// registers4[i].ToString();
                                                                                           //CallValueSet(rough3.rjButton16, (int)registers4[i], (startAddress4 + i), dataModel.textBoxRegist64);
                                        break;

                              //  }

                            }

                        }

                        sec++;
                        if (sec >= 1)
                        {
                            sec = 0;
                            if (toggle == true) toggle = false;          // FOR ALARM BLINKING AFTER EVERY 500 MSEC
                            else toggle = true;                          // FOR ALARM BLINKING AFTER EVERY 500 MSEC
                            checkDigitalInputs();                        // GAS ALARM FUNCTION AFTER EVERY 500 MSEC

                        }

                    });
                }
                catch (Exception ex)
                {

                    Console.WriteLine("serial:" + ex);

                }
            //}
        }

        public void CallValueSet(RJButton myButton,int myValue,int registerNo, string myStatus) { 
        
           // myButton.Text = myValue.ToString();

            if (myValue == 258)
            {
                //  flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle1);
                myButton.Text = myStatus;// "Call From " + registerNo.ToString();
               // rough.rjButton1.BackColor = Color.Red;
            }
            else if (myValue == 262)
            {
                // flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle1);
                myButton.Text = myStatus;// "Care From " + registerNo.ToString();
               // rough.rjButton1.BackColor = Color.Orange;
            }
          /*  else if (myValue == 261)
            {
                rough.rjButton1.BackColor = Color.DarkGreen;
                flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle1);
            }
            else if (myValue == 264)
            {
                rough.rjButton1.BackColor = Color.Blue;
            }*/

        }


        public void BytesToRead()
        {


            if (ComPort1 != null)
            {
                Console.WriteLine("Mycode");
                try
                {
                    //   ComPort1.Write(Globals.Txbuf, 0, Globals.Txbuf.Length);
                    if (ComPort1.IsOpen == true)
                    {

                       

                        string hex_add ="0x0001";// "0x00A4";
                        ushort dec_add = Convert.ToUInt16(hex_add, 16);
                        ushort startAddress = dec_add;
                        ushort numRegisters = 16;


                        Console.WriteLine("checkBoxHub "+ dataModel.checkBoxHub1);
                        if (dataModel.checkBoxHub1) 
                        {
                            byte slaveId = 1;
                            // read five registers
                            registers1 = master.ReadHoldingRegisters(slaveId, 0, numRegisters);
                            wait(500);
                        }
                        // if (dataModel.checkBoxHub1 && dataModel.checkBoxHub2) {
                        /// Thread.Sleep(500);
                        
                       // }

                        if (dataModel.checkBoxHub2) 
                        {
                            
                            byte slaveId = 2;
                            // read five registers
                            registers2 = master.ReadHoldingRegisters(slaveId, 0, numRegisters);
                            wait(500);
                        }

                        // if (dataModel.checkBoxHub2 && dataModel.checkBoxHub3)
                        // {
                        
                           // Thread.Sleep(500);
                           //     }


                        if (dataModel.checkBoxHub3)
                        {
                        
                            byte slaveId = 3;
                            // read five registers
                            registers3 = master.ReadHoldingRegisters(slaveId, 0, numRegisters);
                            wait(500);
                        }
                        
                       // if (dataModel.checkBoxHub3 && dataModel.checkBoxHub4)
                       // {
                       //Thread.Sleep(500);
                       //  }

                        if (dataModel.checkBoxHub4)
                        {
                   
                            byte slaveId = 4;
                            // read five registers
                            registers4 = master.ReadHoldingRegisters(slaveId, 0, numRegisters);
                            wait(500);
                        }
                        // Thread.Sleep(500);

                        
                        // master.Transport.ReadTimeout = 1000; // Set your desired timeout

                        try
                        {

                            // Input Registers (Input Data): Read only single 16 bit references
                            // Read
                            // ushort[] inputRegisterData = master.ReadInputRegisters(slaveId,startRef, noOfRefs);
                            // string registerStr = String.Join(" | ", inputRegisterData);
                            // Console.WriteLine("Input Registers -- " + registerStr);

                            // Modbus operations
                            // ushort [] response = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);
                            // Console.WriteLine("Raw Response: " +  response);

                            // byte[] message = new byte[] { slaveId, 1, 2, 2, 3, 4 }; // Adjust based on your message
                            // ushort crc = CalculateCRC(message);
                            // byte[] crcBytes = BitConverter.GetBytes(crc);
                            // Array.Reverse(crcBytes);

                            // Append the CRC bytes to your message
                            // byte[] completeMessage = message.Concat(crcBytes).ToArray();

                            // Send the complete message to the serial port
                            // ComPort1.Write(completeMessage, 0, completeMessage.Length);
                            // Replace 'ushortData' with the data you want to send

                            byte slaveAddress = 0x0001; // Replace with your slave address
                            byte functionCode = 1; // Replace with your desired function code (e.g., 3 for reading registers)
                            ushort startAddress1 = 0001; // Replace with your starting address
                            ushort numberOfRegisters = 10; // Replace with the number of registers to read

                           // Calculate the CRC (Cyclic Redundancy Check)
                            ushort crc = CalculateCRC(new byte[] { slaveAddress, functionCode, (byte)(startAddress1 >> 8), (byte)startAddress1, (byte)(numberOfRegisters >> 8), (byte)numberOfRegisters });

                           // Construct the Modbus RTU message
                            byte[] message = new byte[] { slaveAddress, functionCode, (byte)(startAddress1 >> 8), (byte)startAddress1, (byte)(numberOfRegisters >> 8), (byte)numberOfRegisters, (byte)(crc >> 8), (byte)crc };

                           // Send the message via your serial port
                           // ComPort1.Write(message, 0, message.Length);


                            ushort[] ushortData = new ushort[] { 1,2,3};

                           // Send data to Modbus registers
                           // master.WriteMultipleRegisters(slaveId, 0001, ushortData);

                           // master.WriteMultipleCoils(slaveAddress, 1, new bool[6] { false, false, false, false, false, false });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Exception: " + ex.Message);
                        }

                        // Replace 'ushortData' with the data you want to send
                        // ushort[] ushortData = new ushort[] { 100, 200, 300 };

                        // Send data to Modbus registers
                        // master.WriteMultipleRegisters(slaveId, 1, ushortData);
                        // write three coils
                        // master.WriteMultipleRegisters(slaveId, startAddress, data);
                        // master.WriteMultipleCoils(slaveId, startAddress, new bool[] { true, false, true });

                        // ComPort1.DiscardInBuffer();
                        // Debug.WriteLine(Rxbuf[0] + " " + Rxbuf[1] + " " + Rxbuf[2] + " " + Rxbuf[3] + " " + Rxbuf[4] + " " + Rxbuf[5] + " " + Rxbuf[6] + " " + Rxbuf[7] + " " + Rxbuf[8] + " " + Rxbuf[9] + " " + Rxbuf[10] + " " + Rxbuf[11] + " " + Rxbuf[12] + " " + Rxbuf[13] + " " + Rxbuf[14] + " " + Rxbuf[15] + " " + Rxbuf[16] + " " + Rxbuf[17] + " " + Rxbuf[18]);
                        // int milliseconds = 2000;

                    }



                }
                catch (Exception ex)
                {

                    if (ex.Message.ToString().Trim().Contains("The port is closed".Trim()))
                    {
                        MessageBox.Show(ex.Message.ToString(), "Controller Communication Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // GetPorts();
                    }
                    // 

                }
                if (ComPort1.IsOpen == false)
                {
                    ComPort1.Dispose();
                    ComPort1 = null;
                    connect1();
                }
            }
            else {
                ComPort1 = null;
                connect1(); }

          
        }

        void checkDigitalInputs()
        {
            
            
            

            Console.WriteLine("Number of Control: "+ flowLayoutPanel1.Controls.Count);

           

            if (myObjects[0].Value != 261)
            {
                if (toggle == true) rough.rjButton1.Visible = true;
                else rough.rjButton1.Visible = false;
            } 
            
            if (myObjects[1].Value != 261) {

                if (toggle == true) rough.rjButton2.Visible = true;
                else rough.rjButton2.Visible = false;
            }

            if (myObjects[2].Value != 261)
            {

                if (toggle == true) rough.rjButton3.Visible = true;
                else rough.rjButton3.Visible = false;
            }
            if (myObjects[3].Value != 261)
            {

                if (toggle == true) rough.rjButton4.Visible = true;
                else rough.rjButton4.Visible = false;
            }
            if (myObjects[4].Value != 261)
            {

                if (toggle == true) rough.rjButton5.Visible = true;
                else rough.rjButton5.Visible = false;
            }
            if (myObjects[5].Value != 261)
            {

                if (toggle == true) rough.rjButton6.Visible = true;
                else rough.rjButton6.Visible = false;
            }
            if (myObjects[6].Value != 261)
            {

                if (toggle == true) rough.rjButton7.Visible = true;
                else rough.rjButton7.Visible = false;
            }
            if (myObjects[7].Value != 261)
            {

                if (toggle == true) rough.rjButton8.Visible = true;
                else rough.rjButton8.Visible = false;
            }
            if (myObjects[8].Value != 261)
            {

                if (toggle == true) rough.rjButton9.Visible = true;
                else rough.rjButton9.Visible = false;
            }
            if (myObjects[9].Value != 261)
            {

                if (toggle == true) rough.rjButton10.Visible = true;
                else rough.rjButton10.Visible = false;
            }
            if (myObjects[10].Value != 261)
            {

                if (toggle == true) rough.rjButton11.Visible = true;
                else rough.rjButton11.Visible = false;
            }
            if (myObjects[11].Value != 261)
            {

                if (toggle == true) rough.rjButton12.Visible = true;
                else rough.rjButton12.Visible = false;
            }
            if (myObjects[12].Value != 261)
            {

                if (toggle == true) rough.rjButton13.Visible = true;
                else rough.rjButton13.Visible = false;
            }
            if (myObjects[13].Value != 261)
            {

                if (toggle == true) rough.rjButton14.Visible = true;
                else rough.rjButton14.Visible = false;
            }
            if (myObjects[14].Value != 261)
            {

                if (toggle == true) rough.rjButton15.Visible = true;
                else rough.rjButton15.Visible = false;
            }
            if (myObjects[15].Value != 261)
            {

                if (toggle == true) rough.rjButton16.Visible = true;
                else rough.rjButton16.Visible = false;
            }

            // ++++++++++++++++++++++++++++++++++++S1++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (myObjects2[0].Value != 261)
            {
                if (toggle == true) rough1.rjButton1.Visible = true;
                else rough1.rjButton1.Visible = false;
            }

            if (myObjects2[1].Value != 261)
            {

                if (toggle == true) rough1.rjButton2.Visible = true;
                else rough1.rjButton2.Visible = false;
            }

            if (myObjects2[2].Value != 261)
            {

                if (toggle == true) rough1.rjButton3.Visible = true;
                else rough1.rjButton3.Visible = false;
            }
            if (myObjects2[3].Value != 261)
            {

                if (toggle == true) rough1.rjButton4.Visible = true;
                else rough1.rjButton4.Visible = false;
            }
            if (myObjects2[4].Value != 261)
            {

                if (toggle == true) rough1.rjButton5.Visible = true;
                else rough1.rjButton5.Visible = false;
            }
            if (myObjects2[5].Value != 261)
            {

                if (toggle == true) rough1.rjButton6.Visible = true;
                else rough1.rjButton6.Visible = false;
            }
            if (myObjects2[6].Value != 261)
            {

                if (toggle == true) rough1.rjButton7.Visible = true;
                else rough1.rjButton7.Visible = false;
            }
            if (myObjects2[7].Value != 261)
            {

                if (toggle == true) rough1.rjButton8.Visible = true;
                else rough1.rjButton8.Visible = false;
            }
            if (myObjects2[8].Value != 261)
            {

                if (toggle == true) rough1.rjButton9.Visible = true;
                else rough1.rjButton9.Visible = false;
            }
            if (myObjects2[9].Value != 261)
            {

                if (toggle == true) rough1.rjButton10.Visible = true;
                else rough1.rjButton10.Visible = false;
            }
            if (myObjects2[10].Value != 261)
            {

                if (toggle == true) rough1.rjButton11.Visible = true;
                else rough1.rjButton11.Visible = false;
            }
            if (myObjects2[11].Value != 261)
            {

                if (toggle == true) rough1.rjButton12.Visible = true;
                else rough1.rjButton12.Visible = false;
            }
            if (myObjects2[12].Value != 261)
            {

                if (toggle == true) rough1.rjButton13.Visible = true;
                else rough1.rjButton13.Visible = false;
            }
            if (myObjects2[13].Value != 261)
            {

                if (toggle == true) rough1.rjButton14.Visible = true;
                else rough1.rjButton14.Visible = false;
            }
            if (myObjects2[14].Value != 261)
            {

                if (toggle == true) rough1.rjButton15.Visible = true;
                else rough1.rjButton15.Visible = false;
            }
            if (myObjects2[15].Value != 261)
            {

                if (toggle == true) rough1.rjButton16.Visible = true;
                else rough1.rjButton16.Visible = false;
            }

            // ++++++++++++++++++++++++++++++++++++S3++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (myObjects3[0].Value != 261)
            {
                if (toggle == true) rough2.rjButton1.Visible = true;
                else rough2.rjButton1.Visible = false;
            }

            if (myObjects3[1].Value != 261)
            {

                if (toggle == true) rough2.rjButton2.Visible = true;
                else rough2.rjButton2.Visible = false;
            }

            if (myObjects3[2].Value != 261)
            {

                if (toggle == true) rough2.rjButton3.Visible = true;
                else rough2.rjButton3.Visible = false;
            }
            if (myObjects3[3].Value != 261)
            {

                if (toggle == true) rough2.rjButton4.Visible = true;
                else rough2.rjButton4.Visible = false;
            }
            if (myObjects3[4].Value != 261)
            {

                if (toggle == true) rough2.rjButton5.Visible = true;
                else rough2.rjButton5.Visible = false;
            }
            if (myObjects3[5].Value != 261)
            {

                if (toggle == true) rough2.rjButton6.Visible = true;
                else rough2.rjButton6.Visible = false;
            }
            if (myObjects3[6].Value != 261)
            {

                if (toggle == true) rough2.rjButton7.Visible = true;
                else rough2.rjButton7.Visible = false;
            }
            if (myObjects3[7].Value != 261)
            {

                if (toggle == true) rough2.rjButton8.Visible = true;
                else rough2.rjButton8.Visible = false;
            }
            if (myObjects3[8].Value != 261)
            {

                if (toggle == true) rough2.rjButton9.Visible = true;
                else rough2.rjButton9.Visible = false;
            }
            if (myObjects3[9].Value != 261)
            {

                if (toggle == true) rough2.rjButton10.Visible = true;
                else rough2.rjButton10.Visible = false;
            }
            if (myObjects3[10].Value != 261)
            {

                if (toggle == true) rough2.rjButton11.Visible = true;
                else rough2.rjButton11.Visible = false;
            }
            if (myObjects3[11].Value != 261)
            {

                if (toggle == true) rough2.rjButton12.Visible = true;
                else rough2.rjButton12.Visible = false;
            }
            if (myObjects3[12].Value != 261)
            {

                if (toggle == true) rough2.rjButton13.Visible = true;
                else rough2.rjButton13.Visible = false;
            }
            if (myObjects3[13].Value != 261)
            {

                if (toggle == true) rough2.rjButton14.Visible = true;
                else rough2.rjButton14.Visible = false;
            }
            if (myObjects3[14].Value != 261)
            {

                if (toggle == true) rough2.rjButton15.Visible = true;
                else rough2.rjButton15.Visible = false;
            }

            if (myObjects3[15].Value != 261)
            {

                if (toggle == true) rough2.rjButton16.Visible = true;
                else rough2.rjButton16.Visible = false;
            }

            // ++++++++++++++++++++++++++++++++++++S4++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (myObjects4[0].Value != 261)
            {
                if (toggle == true) rough3.rjButton1.Visible = true;
                else rough3.rjButton1.Visible = false;
            }

            if (myObjects4[1].Value != 261)
            {

                if (toggle == true) rough3.rjButton2.Visible = true;
                else rough3.rjButton2.Visible = false;
            }

            if (myObjects4[2].Value != 261)
            {

                if (toggle == true) rough3.rjButton3.Visible = true;
                else rough3.rjButton3.Visible = false;
            }
            if (myObjects4[3].Value != 261)
            {

                if (toggle == true) rough3.rjButton4.Visible = true;
                else rough3.rjButton4.Visible = false;
            }
            if (myObjects4[4].Value != 261)
            {

                if (toggle == true) rough3.rjButton5.Visible = true;
                else rough3.rjButton5.Visible = false;
            }
            if (myObjects4[5].Value != 261)
            {

                if (toggle == true) rough3.rjButton6.Visible = true;
                else rough3.rjButton6.Visible = false;
            }
            if (myObjects4[6].Value != 261)
            {

                if (toggle == true) rough3.rjButton7.Visible = true;
                else rough3.rjButton7.Visible = false;
            }
            if (myObjects4[7].Value != 261)
            {

                if (toggle == true) rough3.rjButton8.Visible = true;
                else rough3.rjButton8.Visible = false;
            }
            if (myObjects4[8].Value != 261)
            {

                if (toggle == true) rough3.rjButton9.Visible = true;
                else rough3.rjButton9.Visible = false;
            }
            if (myObjects4[9].Value != 261)
            {

                if (toggle == true) rough3.rjButton10.Visible = true;
                else rough3.rjButton10.Visible = false;
            }
            if (myObjects4[10].Value != 261)
            {

                if (toggle == true) rough3.rjButton11.Visible = true;
                else rough3.rjButton11.Visible = false;
            }
            if (myObjects[11].Value != 261)
            {

                if (toggle == true) rough3.rjButton12.Visible = true;
                else rough3.rjButton12.Visible = false;
            }
            if (myObjects[12].Value != 261)
            {

                if (toggle == true) rough3.rjButton13.Visible = true;
                else rough3.rjButton13.Visible = false;
            }
            if (myObjects[13].Value != 261)
            {

                if (toggle == true) rough3.rjButton14.Visible = true;
                else rough3.rjButton14.Visible = false;
            }
            if (myObjects[14].Value != 261)
            {

                if (toggle == true) rough3.rjButton15.Visible = true;
                else rough3.rjButton15.Visible = false;
            }

            if (myObjects[15].Value != 261)
            {

                if (toggle == true) rough3.rjButton16.Visible = true;
                else rough3.rjButton16.Visible = false;
            }

            Console.WriteLine("toggle " + toggle);
        }

            private static ushort CalculateCRC(byte[] message)
        {
            ushort crc = 0xFFFF;
            foreach (byte b in message)
            {
                crc ^= (ushort)b;
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 1) != 0)
                        crc = (ushort)((crc >> 1) ^ 0xA001); // 0xA001 is the CRC-16 polynomial
                    else
                        crc >>= 1;
                }
            }
            return crc;
        }

        private void button1_Click(object sender, EventArgs e)
        {
       
            this.Hide();
        }

        void blinkLabel()
        {          
            int blink_times = 25;
                   
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
                   
            timer1.Interval = 1000;//every one second

            int uio = 0;
            int uio2 = 0;

            timer1.Tick += new System.EventHandler((s, e) =>
            {

                if (flowLayoutPanel1.Controls.Count > 48)
                {
                  
                   // flowLayoutPanel1.HorizontalScroll.Value = flowLayoutPanel1.HorizontalScroll.Maximum;

                    if (uio == 15) {
                    //    flowLayoutPanel1.HorizontalScroll.Value = 0;
                        // Thread.Sleep(6000);
                      //  wait(3000);
                        uio = 0;
                    }
                    // flowLayoutPanel1.AutoScrollPosition = new Point(20000, 0); 
                    // Thread.Sleep(10000);
                    // flowLayoutPanel1.AutoScrollPosition = new Point(0, 0); 
                }   
                else {
                      
                 //   flowLayoutPanel1.HorizontalScroll.Value = 0;
                    // flowLayoutPanel1.AutoScrollPosition = new Point(0, 0);
                }

                /*if ((new[] { myObjects[0].Value, myObjects[1].Value, myObjects[2].Value, myObjects[3].Value, myObjects[4].Value, myObjects[5].Value, myObjects[6].Value, myObjects[7].Value, myObjects[8].Value, myObjects[9].Value, myObjects[10].Value, myObjects[11].Value, myObjects[12].Value, myObjects[13].Value, myObjects[14].Value, myObjects[15].Value }).Contains(256))
                {
                  //  MessageBox.Show("1");
                    this.panel1.Show();
                    s2.panel1.Hide();
                    s3.panel1.Hide();
                    s4.panel1.Hide();
                  //  Thread.Sleep(1000);
                }
                
                if (!(new[] { myObjects[16].Value, myObjects[17].Value, myObjects[18].Value, myObjects[19].Value, myObjects[20].Value, myObjects[21].Value, myObjects[22].Value, myObjects[23].Value, myObjects[24].Value, myObjects[25].Value, myObjects[26].Value, myObjects[27].Value, myObjects[28].Value, myObjects[29].Value, myObjects[30].Value, myObjects[31].Value }).Contains(256)) 
                {
                    s2.panel1.Show();
                    this.panel1.Hide();
                    s3.panel1.Hide();
                    s4.panel1.Hide();
                  //  Thread.Sleep(1000);
                }

                if (!(new[] { myObjects[32].Value, myObjects[33].Value, myObjects[34].Value, myObjects[35].Value, myObjects[36].Value, myObjects[37].Value, myObjects[38].Value, myObjects[39].Value, myObjects[40].Value, myObjects[41].Value, myObjects[42].Value, myObjects[43].Value, myObjects[44].Value, myObjects[45].Value, myObjects[46].Value, myObjects[47].Value }).Contains(256)) 
                {
                    s3.panel1.Show();
                    this.panel1.Hide();
                    s2.panel1.Hide();
                    s4.panel1.Hide();
                 //   Thread.Sleep(1000);
                }

               
               if (!(new[] { myObjects[48].Value, myObjects[49].Value, myObjects[50].Value, myObjects[51].Value, myObjects[52].Value, myObjects[53].Value, myObjects[54].Value, myObjects[55].Value, myObjects[56].Value, myObjects[57].Value, myObjects[58].Value, myObjects[59].Value, myObjects[60].Value, myObjects[61].Value, myObjects[62].Value, myObjects[63].Value }).Contains(256))
                {
                    s4.panel1.Show();
                    this.panel1.Hide();
                    s2.panel1.Hide();
                    s3.panel1.Hide();
                  //  Thread.Sleep(1000);
                }
                uio++;
                Console.WriteLine(" Hello "+ uio);*/

                // if (blink_times == 25)
                // {   

                // label1.Visible = !label1.Visible;

                //   if((new[] { myObjects[0].Value, myObjects[1].Value, myObjects[2].Value, myObjects[3].Value, myObjects[4].Value, myObjects[5].Value, myObjects[6].Value, myObjects[7].Value, myObjects[8].Value, myObjects[9].Value, myObjects[10].Value, myObjects[11].Value, myObjects[12].Value, myObjects[13].Value, myObjects[14].Value, myObjects[15].Value }).Contains(256)|| (new[] { myObjects[0].Value, myObjects[1].Value, myObjects[2].Value, myObjects[3].Value, myObjects[4].Value, myObjects[5].Value, myObjects[6].Value, myObjects[7].Value, myObjects[8].Value, myObjects[9].Value, myObjects[10].Value, myObjects[11].Value, myObjects[12].Value, myObjects[13].Value, myObjects[14].Value, myObjects[15].Value }).Contains(261)|| (new[] { myObjects[0].Value, myObjects[1].Value, myObjects[2].Value, myObjects[3].Value, myObjects[4].Value, myObjects[5].Value, myObjects[6].Value, myObjects[7].Value, myObjects[8].Value, myObjects[9].Value, myObjects[10].Value, myObjects[11].Value, myObjects[12].Value, myObjects[13].Value, myObjects[14].Value, myObjects[15].Value }).Contains(262)|| (new[] { myObjects[0].Value, myObjects[1].Value, myObjects[2].Value, myObjects[3].Value, myObjects[4].Value, myObjects[5].Value, myObjects[6].Value, myObjects[7].Value, myObjects[8].Value, myObjects[9].Value, myObjects[10].Value, myObjects[11].Value, myObjects[12].Value, myObjects[13].Value, myObjects[14].Value, myObjects[15].Value }).Contains(258))
                // {
                //   uio++;
                // Console.WriteLine("my_visible 1 "+ uio);

                //  flowLayoutPanel1.Show();
                //s2.flowLayoutPanel1.Hide();
                //s3.panel1.Hide();
                //s4.panel1.Hide();

                // }
                // MessageBox.Show("Index: "+ roundPanelWithoutTitle1.TabIndex+" X: "+roundPanelWithoutTitle1.Location.X + " Y: " + roundPanelWithoutTitle1.Location.Y);
                // System.Threading.Thread.Sleep(4000);

                blink_times--;
                //   } 
                //  if (blink_times == 24)
                //{
                //Thread.Sleep(1000);
                    
                //  if((new[] { myObjects2[0].Value, myObjects2[1].Value, myObjects2[2].Value, myObjects2[3].Value, myObjects2[4].Value, myObjects2[5].Value, myObjects2[6].Value, myObjects2[7].Value, myObjects2[8].Value, myObjects2[9].Value, myObjects2[10].Value, myObjects2[11].Value, myObjects2[12].Value, myObjects2[13].Value, myObjects2[14].Value, myObjects2[15].Value }).Contains(256)|| (new[] { myObjects2[0].Value, myObjects2[1].Value, myObjects2[2].Value, myObjects2[3].Value, myObjects2[4].Value, myObjects2[5].Value, myObjects2[6].Value, myObjects2[7].Value, myObjects2[8].Value, myObjects2[9].Value, myObjects2[10].Value, myObjects2[11].Value, myObjects2[12].Value, myObjects2[13].Value, myObjects2[14].Value, myObjects2[15].Value }).Contains(261)|| (new[] { myObjects2[0].Value, myObjects2[1].Value, myObjects2[2].Value, myObjects2[3].Value, myObjects2[4].Value, myObjects2[5].Value, myObjects2[6].Value, myObjects2[7].Value, myObjects2[8].Value, myObjects2[9].Value, myObjects2[10].Value, myObjects2[11].Value, myObjects2[12].Value, myObjects2[13].Value, myObjects2[14].Value, myObjects2[15].Value }).Contains(262)|| (new[] { myObjects2[0].Value, myObjects2[1].Value, myObjects2[2].Value, myObjects2[3].Value, myObjects2[4].Value, myObjects2[5].Value, myObjects2[6].Value, myObjects2[7].Value, myObjects2[8].Value, myObjects2[9].Value, myObjects2[10].Value, myObjects2[11].Value, myObjects2[12].Value, myObjects2[13].Value, myObjects2[14].Value, myObjects2[15].Value }).Contains(258))
                 // {
                //    Console.WriteLine("my_visible 2");
                //    flowLayoutPanel1.Hide();
                  //  s2.flowLayoutPanel1.Show();
                   

                    //s3.panel1.Hide();
                    //s4.panel1.Hide();
              //  }
                // Thread.Sleep(500);
                // timer1.Stop();
                //   blink_times--;
                // }   
                // if (blink_times == 23) 
                //  {
                /* if ((new[] { myObjects[32].Value, myObjects[33].Value, myObjects[34].Value, myObjects[35].Value, myObjects[36].Value, myObjects[37].Value, myObjects[38].Value, myObjects[39].Value, myObjects[40].Value, myObjects[41].Value, myObjects[42].Value, myObjects[43].Value, myObjects[44].Value, myObjects[45].Value, myObjects[46].Value, myObjects[47].Value }).Contains(256)|| (new[] { myObjects[32].Value, myObjects[33].Value, myObjects[34].Value, myObjects[35].Value, myObjects[36].Value, myObjects[37].Value, myObjects[38].Value, myObjects[39].Value, myObjects[40].Value, myObjects[41].Value, myObjects[42].Value, myObjects[43].Value, myObjects[44].Value, myObjects[45].Value, myObjects[46].Value, myObjects[47].Value }).Contains(261)|| (new[] { myObjects[32].Value, myObjects[33].Value, myObjects[34].Value, myObjects[35].Value, myObjects[36].Value, myObjects[37].Value, myObjects[38].Value, myObjects[39].Value, myObjects[40].Value, myObjects[41].Value, myObjects[42].Value, myObjects[43].Value, myObjects[44].Value, myObjects[45].Value, myObjects[46].Value, myObjects[47].Value }).Contains(262)|| (new[] { myObjects[32].Value, myObjects[33].Value, myObjects[34].Value, myObjects[35].Value, myObjects[36].Value, myObjects[37].Value, myObjects[38].Value, myObjects[39].Value, myObjects[40].Value, myObjects[41].Value, myObjects[42].Value, myObjects[43].Value, myObjects[44].Value, myObjects[45].Value, myObjects[46].Value, myObjects[47].Value }).Contains(258))
                 {
                   s3.panel1.Show();
                       this.panel1.Hide();
                       s2.panel1.Hide();
                       s4.panel1.Hide();
                   }

               //  blink_times--;
             //  }   
              //  if (blink_times == 22)
              // {
                if ((new[] { myObjects[48].Value, myObjects[49].Value, myObjects[50].Value, myObjects[51].Value, myObjects[52].Value, myObjects[53].Value, myObjects[54].Value, myObjects[55].Value, myObjects[56].Value, myObjects[57].Value, myObjects[58].Value, myObjects[59].Value, myObjects[60].Value, myObjects[61].Value, myObjects[62].Value, myObjects[63].Value }).Contains(256)|| (new[] { myObjects[48].Value, myObjects[49].Value, myObjects[50].Value, myObjects[51].Value, myObjects[52].Value, myObjects[53].Value, myObjects[54].Value, myObjects[55].Value, myObjects[56].Value, myObjects[57].Value, myObjects[58].Value, myObjects[59].Value, myObjects[60].Value, myObjects[61].Value, myObjects[62].Value, myObjects[63].Value }).Contains(261)|| (new[] { myObjects[48].Value, myObjects[49].Value, myObjects[50].Value, myObjects[51].Value, myObjects[52].Value, myObjects[53].Value, myObjects[54].Value, myObjects[55].Value, myObjects[56].Value, myObjects[57].Value, myObjects[58].Value, myObjects[59].Value, myObjects[60].Value, myObjects[61].Value, myObjects[62].Value, myObjects[63].Value }).Contains(262)|| (new[] { myObjects[48].Value, myObjects[49].Value, myObjects[50].Value, myObjects[51].Value, myObjects[52].Value, myObjects[53].Value, myObjects[54].Value, myObjects[55].Value, myObjects[56].Value, myObjects[57].Value, myObjects[58].Value, myObjects[59].Value, myObjects[60].Value, myObjects[61].Value, myObjects[62].Value, myObjects[63].Value }).Contains(258))
                {
                  s4.panel1.Show();
                       this.panel1.Hide();
                       s2.panel1.Hide();
                       s3.panel1.Hide();
                   }*/


                //  }

                blink_times = 25;
                // Thread.Sleep(10000);
                // flowLayoutPanel1.HorizontalScroll.Value = 0;
                uio++;
            }       

            );


            timer1.Start();
        }

        //Note Forms.Timer and Timer() have similar implementations. 

        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void connectionPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void roundPanelWithoutTitle1_Enter(object sender, EventArgs e)
        {

        }

        private void rjButton2_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                StopWatchTimer();
            }
            catch (Exception ex)
            {
            }
        }

        private void rjButton5_Click(object sender, EventArgs e)
        {

            
        }

        private void rjButton50_Click(object sender, EventArgs e)
        {
            //myStopWatchObjects[0].btnStart2_Click();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            try { 
            int[] data = { 1, 2 };
            // modbusClient.Connect();
            // modbusClient.WriteMultipleRegisters(1, data);
            }catch(Exception ex) {
                Console.WriteLine("Console1: "+ex.Message);
            }
           //  modbusClient.Disconnect();
        }

        public void connect1()
        {


            ComPort1 = new SerialPort();

           /* string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                 Debug.WriteLine("port list: " + port); 
                SerialPortName = port;
            }
            if (String.IsNullOrEmpty(SerialPortName))
            {
                try
                {
                    ComPort1.PortName = SerialPortName;
                }
                catch (Exception ex) { }
            }
            else
            {*/
                try
                {
                    ComPort1.PortName = dataModel.comport;//Properties.Settings.Default["portName"].ToString();// Properties.Settings.Default.portName.ToString();
                }
                catch (Exception ex) { }
            //}


           

            // ComPort1.PortName = SerialPortName;
            ComPort1.DataBits = 8;
            ComPort1.StopBits = StopBits.One;
            ComPort1.BaudRate = 9600;
            ComPort1.Parity = Parity.None;



            bool error = false;

            // Check if all settings have been selected

            if (ComPort1.PortName != null)
            {


                try
                {

                    if (ComPort1.IsOpen == false)

                    {
                        Console.WriteLine("SERIAL PORT:" + dataModel.comport + " dsd " + SerialPortName);

                        ComPort1.Open();

                        master = ModbusSerialMaster.CreateRtu(ComPort1);
                        dataModel.comport_name = SerialPortName;
                        //dbHandlr.updatComport(m_dbConnection, dataModel);
                    
                     Console.WriteLine("SERIAL PORT:" + Properties.Settings.Default["portName"].ToString() + " dsd " + SerialPortName+" is "+ ComPort1.IsOpen);
                     /*this.Invoke((MethodInvoker)delegate
                     {
                        //Globals.logWriter.LogWrite("Open Comport 1: " + ComPort1.IsOpen);
                        connectionStatus.Text = "Connected";
                        connectionStatus.ForeColor = Color.White;
                        //connectionStatus.BackColor = Color.White;
                        connectionPanel.BackColor = Color.Green;
                    
                     });
                     */
                    
                    }

                }
                catch (UnauthorizedAccessException ex) { error = true; ex.Message.ToString(); }
                catch (System.IO.IOException ex) { error = true; ex.Message.ToString(); }
                catch (ArgumentException ex) { error = true; ex.Message.ToString(); }

                if (error)
                {

                    ComPort1.Close();
                    ComPort1 = null;
                    /*this.Invoke((MethodInvoker)delegate
                    {
                        connectionStatus.Text = "Not Connected";
                        connectionStatus.ForeColor = Color.White;
                        //connectionStatus.BackColor = Color.White;
                        connectionPanel.BackColor = Color.Red;
                        

                    });*/

                    // MessageBox.Show("Could not open the "+ Properties.Settings.Default["portName"].ToString() + " port.Most likely it is already in use, has been removed, or is unavailable.", "COM1 Port unavailable");
                }
                // MessageBox.Show("Could not open the COM1 port. Most likely it is already in use, has been removed, or is unavailable.", "COM Port unavailable", MessageBoxButtons.OK, MessageBoxIcon.Stop); }

            }
            else
            {

                MessageBox.Show("COM1 Serial Port does not exist", "Serial Port Interface", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

        }

        private void rjButton2_Click_1(object sender, EventArgs e)
        {

        }

        public void AutoScrollMax() {

            if (flowLayoutPanel1.Controls.Count > 48)
            {
                flowLayoutPanel1.HorizontalScroll.Value = flowLayoutPanel1.HorizontalScroll.Maximum;
            }

        }

        public void AutoSaveOnClose() { 
        if (!string.IsNullOrEmpty(myElapseTime[0])) { 
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[0], "1");
                }
            if (!string.IsNullOrEmpty(myElapseTime[1]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[1], "2");
            }
            if (!string.IsNullOrEmpty(myElapseTime[2]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[2], "3");
            }
            if (!string.IsNullOrEmpty(myElapseTime[3]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[3], "4");
            }
            if (!string.IsNullOrEmpty(myElapseTime[4]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[4], "5");
            }
            if (!string.IsNullOrEmpty(myElapseTime[5]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[5], "6");
            }
            if (!string.IsNullOrEmpty(myElapseTime[6]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[6], "7");
            }
            if (!string.IsNullOrEmpty(myElapseTime[7]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[7], "8");
            }
            if (!string.IsNullOrEmpty(myElapseTime[8]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[8], "9");
            }
            if (!string.IsNullOrEmpty(myElapseTime[9]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[9], "10");
            }
            if (!string.IsNullOrEmpty(myElapseTime[10]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[10], "11");
            }
            if (!string.IsNullOrEmpty(myElapseTime[11]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[11], "12");
            }
            if (!string.IsNullOrEmpty(myElapseTime[12]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[12], "13");
            }
            if (!string.IsNullOrEmpty(myElapseTime[13]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[13], "14");
            }
            if (!string.IsNullOrEmpty(myElapseTime[14]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[14], "15");
            }
            if (!string.IsNullOrEmpty(myElapseTime[15]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[15], "16");
            }
            if (!string.IsNullOrEmpty(myElapseTime[16]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[16], "17");
            }
            if (!string.IsNullOrEmpty(myElapseTime[17]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[17], "18");
            }
            if (!string.IsNullOrEmpty(myElapseTime[18]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[18], "19");

            }
            if (!string.IsNullOrEmpty(myElapseTime[19]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[19], "20");
            }
            if (!string.IsNullOrEmpty(myElapseTime[20]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[20], "21");
            }
            if (!string.IsNullOrEmpty(myElapseTime[21]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[21], "22");
            }
            if (!string.IsNullOrEmpty(myElapseTime[22]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[22], "23");
            }
            if (!string.IsNullOrEmpty(myElapseTime[23]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[23], "24");
            }
            if (!string.IsNullOrEmpty(myElapseTime[24]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[24], "25");
            }
            if (!string.IsNullOrEmpty(myElapseTime[25]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[25], "26");
            }
            if (!string.IsNullOrEmpty(myElapseTime[26]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[26], "27");
            }
            if (!string.IsNullOrEmpty(myElapseTime[27]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[27], "28");
            }
            if (!string.IsNullOrEmpty(myElapseTime[28]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[28], "29");
            }
            if (!string.IsNullOrEmpty(myElapseTime[29]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[29], "30");
            }
            if (!string.IsNullOrEmpty(myElapseTime[30]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[30], "31");
            }
            if (!string.IsNullOrEmpty(myElapseTime[31]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[31], "32");
            }
            if (!string.IsNullOrEmpty(myElapseTime[32]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[32], "33");
            }
            if (!string.IsNullOrEmpty(myElapseTime[33]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[33], "34");
            }
            if (!string.IsNullOrEmpty(myElapseTime[34]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[34], "35");
            }
            if (!string.IsNullOrEmpty(myElapseTime[35]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[35], "36");
            }
            if (!string.IsNullOrEmpty(myElapseTime[36]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[36], "37");
            }
            if (!string.IsNullOrEmpty(myElapseTime[37]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[37], "38");
            }
            if (!string.IsNullOrEmpty(myElapseTime[38]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[38], "39");
            }
            if (!string.IsNullOrEmpty(myElapseTime[39]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[39], "40");
            }
            if (!string.IsNullOrEmpty(myElapseTime[40]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[40], "41");
            }
            if (!string.IsNullOrEmpty(myElapseTime[41]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[41], "42");
            }
            if (!string.IsNullOrEmpty(myElapseTime[42]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[42], "43");
            }
            if (!string.IsNullOrEmpty(myElapseTime[43]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[43], "44");
            }
            if (!string.IsNullOrEmpty(myElapseTime[44]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[44], "45");
            }
            if (!string.IsNullOrEmpty(myElapseTime[45]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[45], "46");
            }
            if (!string.IsNullOrEmpty(myElapseTime[46]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[46], "47");
            }
            if (!string.IsNullOrEmpty(myElapseTime[47]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[47], "48");
            }
            if (!string.IsNullOrEmpty(myElapseTime[48]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[48], "49");
            }
            if (!string.IsNullOrEmpty(myElapseTime[49]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[49], "50");
            }
            if (!string.IsNullOrEmpty(myElapseTime[50]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[50], "51");
            }
            if (!string.IsNullOrEmpty(myElapseTime[51]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[51], "52");
            }
            if (!string.IsNullOrEmpty(myElapseTime[52]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[52], "53");
            }
            if (!string.IsNullOrEmpty(myElapseTime[53]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[53], "54");
            }
            if (!string.IsNullOrEmpty(myElapseTime[54]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[54], "55");
            }
            if (!string.IsNullOrEmpty(myElapseTime[55]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[55], "56");
            }
            if (!string.IsNullOrEmpty(myElapseTime[56]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[56], "57");
            }
            if (!string.IsNullOrEmpty(myElapseTime[57]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[57], "58");
            }
            if (!string.IsNullOrEmpty(myElapseTime[58]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[58], "59");
            }
            if (!string.IsNullOrEmpty(myElapseTime[59]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[59], "60");
            }
            if (!string.IsNullOrEmpty(myElapseTime[60]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[60], "61");
            }
            if (!string.IsNullOrEmpty(myElapseTime[61]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[61], "62");
            }
            if (!string.IsNullOrEmpty(myElapseTime[62]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[62], "63");
            }
            if (!string.IsNullOrEmpty(myElapseTime[63]))
            {
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[63], "64");
            }
        }

        public void checkStatus() 
        {

            myObjects[0].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[0].DidChange += () => {

             

                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                // MessageBox.Show(DateTime.Now.ToString());
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rough.rjButtonTime1.Text = time;

                string regId = "1";
                Console.WriteLine("status_value: "+myElapseTime[0]);
                if (!string.IsNullOrEmpty(myElapseTime[0])) { 
                dbHandlr.update_call_data(m_dbConnection, myElapseTime[0], regId);
                }

                /*if (myObjects[0].Value == 261)
                {

                    myStopWatchObjects[0].Stop();
                    myStopWatchObjects[0].Reset();
                    rough.myRjButton1.Text = "00:00";
                }
                else
                {*/
                myElapseTime[0] = "00:00";
                myStopWatchObjects[0].Stop();
                myStopWatchObjects[0].Reset();
                rough.myRjButton1.Text = "00:00";
                myStopWatchObjects[0].Start();
                //}
                if (myObjects[0].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle1);
                    
                    rough.rjButton1.BackColor = Color.Red;
                    dataModel.lastCallValue = dataModel.textBoxRegist1; 

                    //  flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle1);
                    dataModel.lastCallStatus = myObjects[0].Value.ToString(); // "Call From 1";//258
                    dataModel.registerId = "1";
                    dataModel.dateTime = dateTime;
                    // flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle1);

                    // rough.rjButton1.BackColor = Color.Orange;

                    dbHandlr.insert_call_data(m_dbConnection,dataModel);
                }
                else if (myObjects[0].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle1);
                    rough.rjButton1.BackColor = Color.Orange;

                    dataModel.lastCallValue = dataModel.textBoxRegist1; 
                    dataModel.lastCallStatus = myObjects[0].Value.ToString(); // "Care From 1";//262
                    dataModel.registerId = "1";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);

                }
                else if (myObjects[0].Value == 261)
                {
                    rough.rjButton1.BackColor = Color.DarkGreen;
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle1);
                    
                    //dataModel.lastCallValue = myObjects[0].Value.ToString();
                    //dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[0].Value == 264)
                {
                    // rough.rjButton1.BackColor = Color.Blue;

                    // dataModel.lastCallValue = myObjects[0].Value.ToString();

                    // dataModel.registerId = "1";
                    // dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle1);
                    rough.rjButton1.BackColor = Color.Blue;
                    CallValSet("1", dataModel.textBoxRegist1, myObjects[0].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects[1].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[1].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                string dateTime = DateTime.Now.ToString();
                rough.rjButtonTime2.Text = time;


                string regId = "2";
                if (!string.IsNullOrEmpty(myElapseTime[1]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[1], regId);
                }

                /*if (myObjects[1].Value == 261)
                {

                    myStopWatchObjects[1].Start();
                    rough.myRjButton2.Text = "00:00";
                    myStopWatchObjects[1].Reset();
                }
                else
                {*/
                myElapseTime[1] = "00:00";
                myStopWatchObjects[1].Stop();
                    rough.myRjButton2.Text = "00:00";
                    myStopWatchObjects[1].Reset();
                    myStopWatchObjects[1].Start();
                //}

                if (myObjects[1].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle2);
                    rough.rjButton2.BackColor = Color.Red;

                    dataModel.lastCallValue = dataModel.textBoxRegist2; 
                    dataModel.lastCallStatus = myObjects[1].Value.ToString(); //  "Call From 2";//258
                    dataModel.registerId = "2";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[1].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle2);
                    rough.rjButton2.BackColor = Color.Orange;
                    dataModel.lastCallStatus = myObjects[1].Value.ToString(); // "Care From 2";//262
                    dataModel.registerId = "2";
                    dataModel.lastCallValue = dataModel.textBoxRegist2;
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[1].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle2);
                    rough.rjButton2.BackColor = Color.DarkGreen;

                    
                     
                }
                else if (myObjects[1].Value == 264)
                {
                    // rough.rjButton2.BackColor = Color.Blue;
                    // dataModel.lastCallValue = myObjects[1].Value.ToString();
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle2);
                    rough.rjButton2.BackColor = Color.Blue;
                    CallValSet("2", dataModel.textBoxRegist2, myObjects[1].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects[2].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[2].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                string dateTime = DateTime.Now.ToString();
                rough.rjButtonTime3.Text = time;

                string regId = "3";
                if (!string.IsNullOrEmpty(myElapseTime[2]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[2], regId);
                }
                myElapseTime[2] = "00:00";
                /*if (myObjects[2].Value == 261)
                {

                    myStopWatchObjects[2].Start();
                    rough.myRjButton3.Text = "00:00";
                    myStopWatchObjects[2].Reset();
                }
                else
                {*/
                myElapseTime[2] = "00:00";
                myStopWatchObjects[2].Stop();
                    rough.myRjButton3.Text = "00:00";
                    myStopWatchObjects[2].Reset();
                    myStopWatchObjects[2].Start();
                //}

                if (myObjects[2].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle3);
                    rough.rjButton3.BackColor = Color.Red;
                    dataModel.lastCallStatus = myObjects[2].Value.ToString(); // "Call From 3";// 258
                    dataModel.registerId = "3";
                    dataModel.lastCallValue = dataModel.textBoxRegist3;
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[2].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle3);
                    rough.rjButton3.BackColor = Color.Orange;
                    dataModel.lastCallStatus = myObjects[2].Value.ToString(); // "Care From 3";// 262
                    dataModel.registerId = "3";
                    dataModel.lastCallValue = dataModel.textBoxRegist3; 
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[2].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle3);
                    rough.rjButton3.BackColor = Color.DarkGreen;
             

                }
                else if (myObjects[2].Value == 264)
                {
                    // rough.rjButton3.BackColor = Color.Blue;
                    // dataModel.registerId = "3";
                    // dataModel.lastCallValue = myObjects[2].Value.ToString();
                    // dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);

                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle3);
                    rough.rjButton3.BackColor = Color.Blue;
                    CallValSet("3", dataModel.textBoxRegist3, myObjects[2].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects[3].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[3].DidChange += () => {
                Console.WriteLine("changed!");
              
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                string dateTime = DateTime.Now.ToString();
                rjButtonTime4.Text = time;

                string regId = "4";
                if (!string.IsNullOrEmpty(myElapseTime[3]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[3], regId);
                }
 
                myElapseTime[3] = "00:00";

                /*if (myObjects[3].Value == 261)
                {

                    myStopWatchObjects[3].Start();
                    rough.myRjButton4.Text = "00:00";
                    myStopWatchObjects[3].Reset();

                }
                else
                {*/
                myStopWatchObjects[3].Stop();
                    rough.myRjButton4.Text = "00:00";
                    myStopWatchObjects[3].Reset();
                    myStopWatchObjects[3].Start();
                //}

                if (myObjects[3].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle4);
                    rough.rjButton4.BackColor = Color.Red;
                    dataModel.lastCallStatus = myObjects[3].Value.ToString();// "Call From 4";// 258
                    dataModel.lastCallValue = dataModel.textBoxRegist4;
                    dataModel.registerId = "4";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[3].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle4);
                    rough.rjButton4.BackColor = Color.Orange;
                    dataModel.registerId = "4";
                    dataModel.lastCallStatus = myObjects[3].Value.ToString(); // "Care From 4";// 258
                    dataModel.lastCallValue = dataModel.textBoxRegist4; 
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[3].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle4);
                    rough.rjButton4.BackColor = Color.DarkGreen;
      
                }
                else if (myObjects[3].Value == 264)
                {
                    // rough.rjButton4.BackColor = Color.Blue;
                    // dataModel.lastCallValue = myObjects[3].Value.ToString();
                    // dataModel.registerId = "4";
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle4);
                    rough.rjButton4.BackColor = Color.Blue;
                    CallValSet("4", dataModel.textBoxRegist4, myObjects[3].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects[4].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[4].DidChange += () => {
                Console.WriteLine("changed!");
              
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                string dateTime = DateTime.Now.ToString();
                rough.rjButtonTime5.Text = time;

                string regId = "5";
                if (!string.IsNullOrEmpty(myElapseTime[4]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[4], regId);
                }
 
                myElapseTime[4] = "00:00";

                /*if (myObjects[4].Value == 261)
                {

                    myStopWatchObjects[4].Start();
                    rough.myRjButton5.Text = "00:00";
                    myStopWatchObjects[4].Reset();
                }
                else
                {*/
                myStopWatchObjects[4].Stop();
                    rough.myRjButton5.Text = "00:00";
                    myStopWatchObjects[4].Reset();
                    myStopWatchObjects[4].Start();
                //}

                if (myObjects[4].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle5);
                    rough.rjButton5.BackColor = Color.Red;

                    dataModel.lastCallValue = dataModel.textBoxRegist5;
                    dataModel.lastCallStatus =  myObjects[4].Value.ToString();//"Call From 5";//258
                    dataModel.registerId = "5";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[4].Value == 262)
                {

                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle5);
                    rough.rjButton5.BackColor = Color.Orange;

                    dataModel.lastCallValue = dataModel.textBoxRegist5; 
                    dataModel.lastCallStatus = myObjects[4].Value.ToString(); // "Care From 5";//262
                    dataModel.registerId = "5";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[4].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle5);
                    rough.rjButton5.BackColor = Color.DarkGreen;
                }
                else if (myObjects[4].Value == 264)
                {
                    //  rough.rjButton5.BackColor = Color.Blue;

                    // dataModel.lastCallValue = myObjects[4].Value.ToString();
                    //dataModel.lastCallStatus = "Call From 5";//258
                    // dataModel.registerId = "5";
                    // dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle5);
                    rough.rjButton5.BackColor = Color.Blue;
                    CallValSet("5", dataModel.textBoxRegist5, myObjects[4].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects[5].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[5].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                string dateTime = DateTime.Now.ToString();
                rough.rjButtonTime6.Text = time;


                string regId = "6";
                if (!string.IsNullOrEmpty(myElapseTime[5]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[5], regId);
                }

                myElapseTime[5] = "00:00";


                /*if (myObjects[5].Value == 261)
                {

                    myStopWatchObjects[5].Start();
                    rough.myRjButton6.Text = "00:00";
                    myStopWatchObjects[5].Reset();
                }
                else
                {*/
                myStopWatchObjects[5].Stop();
                    rough.myRjButton6.Text = "00:00";
                myStopWatchObjects[5].Reset();
                myStopWatchObjects[5].Start();
                //}
                
                if (myObjects[5].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle6);
                    rough.rjButton6.BackColor = Color.Red;
                    dataModel.lastCallValue = dataModel.textBoxRegist6; 
                    dataModel.lastCallStatus = myObjects[5].Value.ToString(); // "Call From 6";//258
                    dataModel.registerId = "6";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[5].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle6);
                    rough.rjButton6.BackColor = Color.Orange;
                    dataModel.lastCallValue = dataModel.textBoxRegist6; 
                    dataModel.lastCallStatus = myObjects[5].Value.ToString(); // "Care From 6";//262
                    dataModel.registerId = "6";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[5].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle6);
                    rough.rjButton6.BackColor = Color.DarkGreen;
                }
                else if (myObjects[5].Value == 264)
                {
                    // rough.rjButton6.BackColor = Color.Blue; 
                    // dataModel.lastCallValue = myObjects[5].Value.ToString();
                    // dataModel.lastCallStatus = "Care From 6";//262
                    // dataModel.registerId = "6";
                    // dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle6);
                    rough.rjButton6.BackColor = Color.Blue;
                    CallValSet("6", dataModel.textBoxRegist6, myObjects[5].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects[6].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[6].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough.rjButtonTime7.Text = time;

                string regId = "7";
                if (!string.IsNullOrEmpty(myElapseTime[6]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[6], regId);
                }

                myElapseTime[6] = "00:00";

                /*if (myObjects[6].Value == 261)
                {

                    myStopWatchObjects[6].Start();
                    rough.myRjButton7.Text = "00:00";
                    myStopWatchObjects[6].Reset();
                }
                else
                {*/
                myStopWatchObjects[6].Stop();
                    rough.myRjButton7.Text = "00:00";
                myStopWatchObjects[6].Reset();
                myStopWatchObjects[6].Start();
                //}

                if (myObjects[6].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle7);
                    rough.rjButton7.BackColor = Color.Red;

                    dataModel.lastCallValue = dataModel.textBoxRegist7; 
                    dataModel.lastCallStatus = myObjects[6].Value.ToString();//"Call From 7";//258
                    dataModel.registerId = "7";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[6].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle7);
                    rough.rjButton7.BackColor = Color.Orange;
                    dataModel.lastCallValue = dataModel.textBoxRegist7; 
                    dataModel.lastCallStatus = myObjects[6].Value.ToString(); // "Care From 7";//262
                    dataModel.registerId = "7";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[6].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle7);
                    rough.rjButton7.BackColor = Color.DarkGreen;
                }
                else if (myObjects[6].Value == 264)
                {
                    // rough.rjButton7.BackColor = Color.Blue;
                    // dataModel.lastCallValue = myObjects[6].Value.ToString();
                    // dataModel.lastCallStatus = "Call From 7";//258
                    // dataModel.registerId = "7";
                    // dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);

                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle7);
                    rough.rjButton7.BackColor = Color.Blue;
                    CallValSet("7", dataModel.textBoxRegist7, myObjects[6].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects[7].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[7].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rjButtonTime8.Text = time;

                string regId = "8";
                if (!string.IsNullOrEmpty(myElapseTime[7]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[7], regId);
                }

                myElapseTime[7] = "00:00";

                /*if (myObjects[7].Value == 261)
                {

                    myStopWatchObjects[7].Start();
                    rough.myRjButton8.Text = "00:00";
                    myStopWatchObjects[7].Reset();
                }
                else
                {*/
                myStopWatchObjects[7].Stop();
                    rough.myRjButton8.Text = "00:00";
                    myStopWatchObjects[7].Reset();
                    myStopWatchObjects[7].Start();
                //}

                if (myObjects[7].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle8);
                    rough.rjButton8.BackColor = Color.Red;
                    dataModel.lastCallValue = dataModel.textBoxRegist8; 
                    dataModel.lastCallStatus = myObjects[7].Value.ToString(); //"Call From 8";//258
                    dataModel.registerId = "8";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[7].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle8);
                    rough.rjButton8.BackColor = Color.Orange;
                    dataModel.lastCallValue = dataModel.textBoxRegist8; 
                    dataModel.lastCallStatus = myObjects[7].Value.ToString();//"Care From 8";//264
                    dataModel.registerId = "8";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[7].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle8);
                    rough.rjButton8.BackColor = Color.DarkGreen;
                }
                else if (myObjects[7].Value == 264)
                {
                    // rough.rjButton8.BackColor = Color.Blue;
                    // dataModel.lastCallValue = myObjects[7].Value.ToString();
                    // dataModel.lastCallStatus = "Call From 8";//258
                    // dataModel.registerId = "8";
                    // dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle8);
                    rough.rjButton8.BackColor = Color.Blue;
                    CallValSet("8", dataModel.textBoxRegist8, myObjects[7].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects[8].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[8].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough.rjButtonTime9.Text = time;

                string regId = "9";
                if (!string.IsNullOrEmpty(myElapseTime[8]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[8], regId);
                }

                myElapseTime[8] = "00:00";

                /*if (myObjects[8].Value == 261)
                {

                    myStopWatchObjects[8].Start();
                    rough.myRjButton9.Text = "00:00";
                    myStopWatchObjects[8].Reset();
                }
                else
                {*/
                myStopWatchObjects[8].Stop();
                    rough.myRjButton9.Text = "00:00";
                    myStopWatchObjects[8].Reset();
                    myStopWatchObjects[8].Start();
               // }

                if (myObjects[8].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle9);
                    rough.rjButton9.BackColor = Color.Red;

                    dataModel.lastCallValue = dataModel.textBoxRegist9; 
                    dataModel.lastCallStatus = myObjects[8].Value.ToString(); // "Call From 9";//258
                    dataModel.registerId = "9";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[8].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle9);
                    rough.rjButton9.BackColor = Color.Orange;
                    dataModel.lastCallValue = dataModel.textBoxRegist9; 
                    dataModel.lastCallStatus = myObjects[8].Value.ToString(); //"Care From 9";//262
                    dataModel.registerId = "9";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[8].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle9);
                    rough.rjButton9.BackColor = Color.DarkGreen;
                }
                else if (myObjects[8].Value == 264)
                {
                    // rough.rjButton9.BackColor = Color.Blue;
                    // dataModel.lastCallValue = myObjects[8].Value.ToString();
                    // dataModel.lastCallStatus = "Call From 9";//258
                    // dataModel.registerId = "9";
                    // dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle9);
                    rough.rjButton9.BackColor = Color.Blue;
                    CallValSet("9", dataModel.textBoxRegist9, myObjects[8].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects[9].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[9].DidChange += () => {
                Console.WriteLine("changed!");
      
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough.rjButtonTime10.Text = time;

                string regId = "10";
                if (!string.IsNullOrEmpty(myElapseTime[9]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[9], regId);
                }

                myElapseTime[9] = "00:00";

                /*if (myObjects[9].Value == 261)
                {

                    myStopWatchObjects[9].Start();
                    rough.myRjButton10.Text = "00:00";
                    myStopWatchObjects[9].Reset();
                }
                else
                {*/
                myStopWatchObjects[9].Stop();
                    rough.myRjButton10.Text = "00:00";
                    myStopWatchObjects[9].Reset();
                    myStopWatchObjects[9].Start();
                //}

                if (myObjects[9].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle10);
                    rough.rjButton10.BackColor = Color.Red;
                    dataModel.lastCallValue = dataModel.textBoxRegist10; 
                    dataModel.lastCallStatus = myObjects[9].Value.ToString(); // "Call From 10";//258
                    dataModel.registerId = "10";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[9].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle10);
                    rough.rjButton10.BackColor = Color.Orange;
                    dataModel.lastCallValue = dataModel.textBoxRegist10; 
                    dataModel.lastCallStatus = myObjects[9].Value.ToString(); // "Care From 10";//258
                    dataModel.registerId = "10";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[9].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle10);
                    rough.rjButton10.BackColor = Color.DarkGreen;
                }
                else if (myObjects[9].Value == 264)
                {
                    // rough.rjButton10.BackColor = Color.Blue;
                    // dataModel.lastCallValue = myObjects[9].Value.ToString();
                    // dataModel.lastCallStatus = "Care From 10";//258
                    // dataModel.registerId = "10";
                    // dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle10);
                    rough.rjButton10.BackColor = Color.Blue;
                    CallValSet("10", dataModel.textBoxRegist10, myObjects[9].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects[10].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[10].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough.rjButtonTime11.Text = time;

                string regId = "11";
                if (!string.IsNullOrEmpty(myElapseTime[10]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[10], regId);
                }

                myElapseTime[10] = "00:00";

                /*if (myObjects[10].Value == 261)
                {

                    myStopWatchObjects[10].Start();
                    rough.myRjButton11.Text = "00:00";
                    myStopWatchObjects[10].Reset();
                }
                else
                {*/
                myStopWatchObjects[10].Stop();
                rough.myRjButton11.Text = "00:00";
                myStopWatchObjects[10].Reset();
                myStopWatchObjects[10].Start();
                //}

                if (myObjects[10].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle11);
                    rough.rjButton11.BackColor = Color.Red;
                    dataModel.lastCallValue = dataModel.textBoxRegist11; 
                    dataModel.lastCallStatus = myObjects[10].Value.ToString(); //"Call From 11";//258
                    dataModel.registerId = "11";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[10].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle11);
                    rough.rjButton11.BackColor = Color.Orange;
                    dataModel.lastCallValue = dataModel.textBoxRegist11; 
                    dataModel.lastCallStatus = myObjects[10].Value.ToString(); // "Care From 11";//258
                    dataModel.registerId = "11";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[10].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle11);
                    rough.rjButton11.BackColor = Color.DarkGreen;
                }
                else if (myObjects[10].Value == 264)
                {
                    // rough.rjButton11.BackColor = Color.Blue;
                    // dataModel.lastCallValue = myObjects[10].Value.ToString();
                    // dataModel.lastCallStatus = "Call From 11";//258
                    // dataModel.registerId = "11";
                    // dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle11);
                    rough.rjButton11.BackColor = Color.Blue;
                    CallValSet("11", dataModel.textBoxRegist11, myObjects[10].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects[11].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[11].DidChange += () => {
                
                Console.WriteLine("changed!");
             
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough.rjButtonTime12.Text = time;

                string regId = "12";
                if (!string.IsNullOrEmpty(myElapseTime[11]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[11], regId);
                }

                myElapseTime[11] = "00:00";

                /*if (myObjects[11].Value == 261)
                {

                    myStopWatchObjects[11].Start();
                    rough.myRjButton12.Text = "00:00";
                    myStopWatchObjects[11].Reset();
                }
                else
                {*/
                myStopWatchObjects[11].Stop();
                    rough.myRjButton12.Text = "00:00";
                    myStopWatchObjects[11].Reset();
                    myStopWatchObjects[11].Start();
                //}

                if (myObjects[11].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle12);
                    rough.rjButton12.BackColor = Color.Red;
                    dataModel.lastCallValue = dataModel.textBoxRegist12; 
                    dataModel.lastCallStatus = myObjects[11].Value.ToString(); // "Call From 12";//258
                    dataModel.registerId = "12";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[11].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle12);
                    rough.rjButton12.BackColor = Color.Orange;
                    dataModel.lastCallValue = dataModel.textBoxRegist12; 
                    dataModel.lastCallStatus = myObjects[11].Value.ToString(); // "Care From 12";//262
                    dataModel.registerId = "12";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[11].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle12);
                    rough.rjButton12.BackColor = Color.DarkGreen;
                }
                else if (myObjects[11].Value == 264)
                {
                    // rough.rjButton12.BackColor = Color.Blue;
                    // dataModel.lastCallValue = myObjects[11].Value.ToString();
                    // dataModel.lastCallStatus = "Call From 12";//264
                    // dataModel.registerId = "12";
                    // dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle12);
                    rough.rjButton12.BackColor = Color.Blue;
                    CallValSet("12", dataModel.textBoxRegist12, myObjects[11].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects[12].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[12].DidChange += () => {
                Console.WriteLine("changed!");
             
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough.rjButtonTime13.Text = time;

                string regId = "13";
                if (!string.IsNullOrEmpty(myElapseTime[12]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[12], regId);
                }

                myElapseTime[12] = "00:00";

                /*if (myObjects[12].Value == 261)
                {

                    myStopWatchObjects[12].Start();
                    rough.myRjButton13.Text = "00:00";
                    myStopWatchObjects[12].Reset();
                }
                else
                {*/
                myStopWatchObjects[12].Stop();
                    rough.myRjButton13.Text = "00:00";
                myStopWatchObjects[12].Reset();
                myStopWatchObjects[12].Start();
                //}

                if (myObjects[12].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle13);
                    rough.rjButton13.BackColor = Color.Red;
                    dataModel.lastCallValue = dataModel.textBoxRegist13; 
                    dataModel.lastCallStatus = myObjects[12].Value.ToString(); // "Call From 13";//258
                    dataModel.registerId = "13";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[12].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle13);
                    rough.rjButton13.BackColor = Color.Orange;
                    dataModel.lastCallValue = dataModel.textBoxRegist13; 
                    dataModel.lastCallStatus = myObjects[12].Value.ToString(); //"Care From 13";//258
                    dataModel.registerId = "13";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[12].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle13);
                    rough.rjButton13.BackColor = Color.DarkGreen;
                }
                else if (myObjects[12].Value == 264)
                {
                    // rough.rjButton13.BackColor = Color.Blue;
                    // dataModel.lastCallValue = myObjects[12].Value.ToString();
                    // dataModel.lastCallStatus = "Call From 12";//258
                    // dataModel.registerId = "13";
                    // dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle13);
                    rough.rjButton13.BackColor = Color.Blue;
                    CallValSet("13", dataModel.textBoxRegist13, myObjects[12].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects[13].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[13].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough.rjButtonTime14.Text = time;

                string regId = "14";
                if (!string.IsNullOrEmpty(myElapseTime[13]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[13], regId);
                }

                myElapseTime[13] = "00:00";

                /*if (myObjects[13].Value == 261)
                {

                    myStopWatchObjects[13].Start();
                    rough.myRjButton14.Text = "00:00";
                    myStopWatchObjects[13].Reset();
                }
                else
                {*/
                myStopWatchObjects[13].Stop();
                    rough.myRjButton14.Text = "00:00";
                    myStopWatchObjects[13].Reset();
                    myStopWatchObjects[13].Start();
                //}

                if (myObjects[13].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle14);
                    rough.rjButton14.BackColor = Color.Red;
                    dataModel.lastCallValue = dataModel.textBoxRegist14; 
                    dataModel.lastCallStatus = myObjects[13].Value.ToString(); // "Call From 14";//258
                    dataModel.registerId = "14";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[13].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle14);
                    rough.rjButton14.BackColor = Color.Orange;
                    dataModel.lastCallValue = dataModel.textBoxRegist14; 
                    dataModel.lastCallStatus = myObjects[13].Value.ToString(); //"Care From 14";//258
                    dataModel.registerId = "14";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[13].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle14);
                    rough.rjButton14.BackColor = Color.DarkGreen;
                }
                else if (myObjects[13].Value == 264)
                {
                    // rough.rjButton14.BackColor = Color.Blue;
                    // dataModel.lastCallValue = myObjects[13].Value.ToString();
                    // dataModel.lastCallStatus = "Call From 14";//258
                    // dataModel.registerId = "14";
                    // dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle14);
                    rough.rjButton14.BackColor = Color.Blue;
                    CallValSet("14", dataModel.textBoxRegist14, myObjects[13].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects[14].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[14].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough.rjButtonTime15.Text = time;

                string regId = "15";
                if (!string.IsNullOrEmpty(myElapseTime[14]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[14], regId);
                }

                myElapseTime[14] = "00:00";

                /*if (myObjects[14].Value == 261)
                {

                    myStopWatchObjects[14].Start();
                    rough.myRjButton15.Text = "00:00";
                    myStopWatchObjects[14].Reset();
                }
                else
                {*/
                myStopWatchObjects[14].Stop();
                    rough.myRjButton15.Text = "00:00";
                myStopWatchObjects[14].Reset();
                myStopWatchObjects[14].Start();
                //}

                if (myObjects[14].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle15);
                    rough.rjButton15.BackColor = Color.Red;
                    dataModel.lastCallValue = dataModel.textBoxRegist15; 
                    dataModel.lastCallStatus = myObjects[14].Value.ToString(); //"Call From 15";//258
                    dataModel.registerId = "15";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[14].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle15);
                    rough.rjButton15.BackColor = Color.Orange;
                    dataModel.lastCallValue = dataModel.textBoxRegist15; 
                    dataModel.lastCallStatus = myObjects[14].Value.ToString(); //"Care From 15";//258
                    dataModel.registerId = "15";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[14].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle15);
                    rough.rjButton15.BackColor = Color.DarkGreen;
                }
                else if (myObjects[14].Value == 264)
                {
                    // rough.rjButton15.BackColor = Color.Blue;
                    // dataModel.lastCallValue = myObjects[14].Value.ToString();
                    // dataModel.lastCallStatus = "Call From 15";//258
                    // dataModel.registerId = "15";
                    // dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle15);
                    rough.rjButton15.BackColor = Color.Blue;
                    CallValSet("15", dataModel.textBoxRegist15, myObjects[14].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects[15].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[15].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough.rjButtonTime16.Text = time;

                string regId = "16";
                if (!string.IsNullOrEmpty(myElapseTime[15]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[15], regId);
                }

                myElapseTime[15] = "00:00";

                /*if (myObjects[15].Value == 261)
                { 
                    myStopWatchObjects[15].Start();
                    rough.myRjButton16.Text = "00:00";
                    myStopWatchObjects[15].Reset();
                }
                else
                {*/
                myStopWatchObjects[15].Stop();
                    rough.myRjButton16.Text = "00:00";
                myStopWatchObjects[15].Reset();
                myStopWatchObjects[15].Start();
                //}

                if (myObjects[15].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle16);
                    rough.rjButton16.BackColor = Color.Red;
                    dataModel.lastCallValue = dataModel.textBoxRegist16; 
                    dataModel.lastCallStatus = myObjects[15].Value.ToString(); //"Call From 16";//258
                    dataModel.registerId = "16";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[15].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle16);
                    rough.rjButton16.BackColor = Color.Orange;
                    dataModel.lastCallValue = dataModel.textBoxRegist16; 
                    dataModel.lastCallStatus = myObjects[15].Value.ToString(); //"Care From 16";//258
                    dataModel.registerId = "16";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects[15].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough.roundPanelWithoutTitle16);
                    rough.rjButton16.BackColor = Color.DarkGreen;
        
                }
                else if (myObjects[15].Value == 264)
                {
                    //rough.rjButton16.BackColor = Color.Blue;
                    //dataModel.lastCallValue = myObjects[15].Value.ToString();
                    //dataModel.lastCallStatus = dataModel.textBoxRegist16;// "Call From 16";//258
                    //dataModel.registerId = "16";
                    //dataModel.dateTime = dateTime;
                    //dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough.roundPanelWithoutTitle16);
                    rough.rjButton16.BackColor = Color.Blue;
                    CallValSet("16", dataModel.textBoxRegist16, myObjects[15].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };



            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++S2+++++++++++++++++++++++++++++++++++++++++++++++++++++++
            myObjects2[0].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[0].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime1.Text = time;

                string regId = "17";
                if (!string.IsNullOrEmpty(myElapseTime[16]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[16], regId);
                }

                myElapseTime[16] = "00:00";

                /*if (myObjects[16].Value == 261)
                {

                    myStopWatchObjects[16].Stop();
                    myStopWatchObjects[16].Reset();
                    rough1.myRjButton1.Text = "00:00";
                }
                else
                {*/
                myStopWatchObjects[16].Stop();
                    myStopWatchObjects[16].Reset();
                    rough1.myRjButton1.Text = "00:00"; 
                    myStopWatchObjects[16].Start();
                //}
                if (myObjects2[0].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle1);
                    rough1.rjButton1.BackColor = Color.Red;
                    dataModel.lastCallValue = dataModel.textBoxRegist17; 
                    dataModel.lastCallStatus = myObjects2[0].Value.ToString(); //"Call From 17";//258
                    dataModel.registerId = "17";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects2[0].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle1);
                    rough1.rjButton1.BackColor = Color.Orange;
                    dataModel.lastCallValue = dataModel.textBoxRegist17; 
                    dataModel.lastCallStatus = myObjects2[0].Value.ToString(); // "Care From 17";//258
                    dataModel.registerId = "17";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects2[0].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle1);
                    rough1.rjButton1.BackColor = Color.DarkGreen;
                }
                else if (myObjects2[0].Value == 264)
                {
                    // rough1.rjButton1.BackColor = Color.Blue;
                    // dataModel.lastCallValue = myObjects2[0].Value.ToString();
                    // dataModel.lastCallStatus = "Care From 17";//258
                    // dataModel.registerId = "17";
                    // dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle1);
                    rough1.rjButton1.BackColor = Color.Blue;
                    CallValSet("17", dataModel.textBoxRegist17, myObjects2[0].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects2[1].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[1].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime2.Text = time;

                string regId = "18";
                if (!string.IsNullOrEmpty(myElapseTime[17]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[17], regId);
                }

                myElapseTime[17] = "00:00";

                /*if (myObjects[17].Value == 261)
                {

                    myStopWatchObjects[17].Start();
                    rough1.myRjButton2.Text = "00:00";
                    myStopWatchObjects[17].Reset();
                }
                else
                {*/
                myStopWatchObjects[17].Stop();
                    rough1.myRjButton2.Text = "00:00";
                    myStopWatchObjects[17].Reset();
                    myStopWatchObjects[17].Start();
                //}

                if (myObjects2[1].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle2);
                    rough1.rjButton2.BackColor = Color.Red;
                    dataModel.lastCallValue = dataModel.textBoxRegist18; 
                    dataModel.lastCallStatus = myObjects2[1].Value.ToString(); // "Call From 18";//258
                    dataModel.registerId = "18";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects2[1].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle2);
                    rough1.rjButton2.BackColor = Color.Orange;
                    dataModel.lastCallValue = dataModel.textBoxRegist18; 
                    dataModel.lastCallStatus = myObjects2[1].Value.ToString(); //"Care From 18";//258
                    dataModel.registerId = "18";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects2[1].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle2);
                    rough1.rjButton2.BackColor = Color.DarkGreen;
                }
                else if (myObjects2[1].Value == 264)
                {
                    //  rough1.rjButton2.BackColor = Color.Blue;
                    //  dataModel.lastCallValue = myObjects2[1].Value.ToString();
                    //dataModel.lastCallStatus = "Care From 18";//258
                    //  dataModel.registerId = "18";
                    //  dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle2);
                    rough1.rjButton2.BackColor = Color.Blue;
                    CallValSet("18", dataModel.textBoxRegist18, myObjects2[1].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects2[2].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[2].DidChange += () => {
                Console.WriteLine("changed!");
              
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime3.Text = time;

                string regId = "19";
                if (!string.IsNullOrEmpty(myElapseTime[18]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[18], regId);
                }

                myElapseTime[18] = "00:00";

                /*if (myObjects[18].Value == 261)
                {

                    myStopWatchObjects[18].Start();
                    rough1.myRjButton3.Text = "00:00";
                    myStopWatchObjects[18].Reset();
                }
                else
                {*/
                myStopWatchObjects[18].Stop();
                    rough1.myRjButton3.Text = "00:00";
                    myStopWatchObjects[18].Reset();
                    myStopWatchObjects[18].Start();
                //}

                if (myObjects2[2].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle3);
                    rough1.rjButton3.BackColor = Color.Red;
                    dataModel.lastCallValue = dataModel.textBoxRegist19; 
                    dataModel.lastCallStatus = myObjects[2].Value.ToString(); // "Call From 19";//258
                    dataModel.registerId = "19";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects2[2].Value == 262)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle3);
                    rough1.rjButton3.BackColor = Color.Orange;
                    dataModel.lastCallValue = dataModel.textBoxRegist19; 
                    dataModel.lastCallStatus = myObjects2[2].Value.ToString(); //"Care From 19";//258
                    dataModel.registerId = "19";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);

                   // CallValSet("19","Care From 19", myObjects[18].Value.ToString(), dateTime);
                }
                else if (myObjects2[2].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle3);
                    rough1.rjButton3.BackColor = Color.DarkGreen;
                }
                else if (myObjects2[2].Value == 264)
                {
                    //  rough1.rjButton3.BackColor = Color.Blue;
                    // dataModel.lastCallValue = myObjects2[2].Value.ToString();
                    //dataModel.lastCallStatus = "Care From 19";//258
                    // dataModel.registerId = "19";
                    //  dataModel.dateTime = dateTime;
                    // dbHandlr.insert_call_data(m_dbConnection, dataModel);
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle3);
                    rough1.rjButton3.BackColor = Color.Blue;
                    CallValSet("19", dataModel.textBoxRegist19, myObjects2[2].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects2[3].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[3].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime4.Text = time;

                string regId = "20";
                if (!string.IsNullOrEmpty(myElapseTime[19]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[19], regId);
                }

                myElapseTime[19] = "00:00";

                /*if (myObjects[19].Value == 261)
                {

                    myStopWatchObjects[19].Start();
                    rough1.myRjButton4.Text = "00:00";
                    myStopWatchObjects[19].Reset();
                }
                else
                {*/
                myStopWatchObjects[19].Stop();
                    rough1.myRjButton4.Text = "00:00";
                    myStopWatchObjects[19].Reset();
                    myStopWatchObjects[19].Start();
                //}

                if (myObjects2[3].Value == 258)
                {
                    PlayingSound();

                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle4);
                    rough1.rjButton4.BackColor = Color.Red;
                    dataModel.lastCallValue = dataModel.textBoxRegist20; 
                    dataModel.lastCallStatus = myObjects2[3].Value.ToString(); //"Call From 20";//258
                    dataModel.registerId = "20";
                    dataModel.dateTime = dateTime;
                    dbHandlr.insert_call_data(m_dbConnection, dataModel);
                }
                else if (myObjects2[3].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle4);
                    rough1.rjButton4.BackColor = Color.Orange;
                    CallValSet("20", dataModel.textBoxRegist20, myObjects2[3].Value.ToString(), dateTime);
                }
                else if (myObjects2[3].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle4);
                    rough1.rjButton4.BackColor = Color.DarkGreen;
                }
                else if (myObjects2[3].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle4);
                    rough1.rjButton4.BackColor = Color.Blue;
                    CallValSet("20", dataModel.textBoxRegist20, myObjects2[3].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects2[4].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[4].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime5.Text = time;

                string regId = "21";
                if (!string.IsNullOrEmpty(myElapseTime[20]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[20], regId);
                }

                myElapseTime[20] = "00:00";

                /*if (myObjects[20].Value == 261)
                {

                    myStopWatchObjects[20].Start();
                    rough1.myRjButton5.Text = "00:00";
                    myStopWatchObjects[20].Reset();
                }
                else
                {*/
                myStopWatchObjects[20].Stop();
                    rough1.myRjButton5.Text = "00:00";
                    myStopWatchObjects[20].Reset();
                    myStopWatchObjects[20].Start();
                //}

                if (myObjects2[4].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle5);
                    rough1.rjButton5.BackColor = Color.Red;
                    CallValSet("21", dataModel.textBoxRegist21, myObjects2[4].Value.ToString(), dateTime);
                }
                else if (myObjects2[4].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle5);
                    rough1.rjButton5.BackColor = Color.Orange;
                    CallValSet("21", dataModel.textBoxRegist21, myObjects2[4].Value.ToString(), dateTime);
                }
                else if (myObjects2[4].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle5);
                    rough1.rjButton5.BackColor = Color.DarkGreen;
                }
                else if (myObjects2[4].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle5);
                    rough1.rjButton5.BackColor = Color.Blue;
                    CallValSet("21", dataModel.textBoxRegist21, myObjects2[4].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects2[5].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[5].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime6.Text = time;

                string regId = "22";
                if(!string.IsNullOrEmpty(myElapseTime[21]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[21], regId);
                }

                myElapseTime[21] = "00:00";

                /*if (myObjects[21].Value == 261)
                {

                    myStopWatchObjects[21].Start();
                    rough1.myRjButton6.Text = "00:00";
                    myStopWatchObjects[21].Reset();
                }
                else
                {*/
                myStopWatchObjects[21].Stop();
                    rough1.myRjButton6.Text = "00:00";
                    myStopWatchObjects[21].Reset();
                    myStopWatchObjects[21].Start();
                //}

                if (myObjects2[5].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle6);
                    rough1.rjButton6.BackColor = Color.Red;
                    CallValSet("22", dataModel.textBoxRegist22, myObjects2[5].Value.ToString(), dateTime);
                }
                else if (myObjects2[5].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle6);
                    rough1.rjButton6.BackColor = Color.Orange;
                    CallValSet("22", dataModel.textBoxRegist22, myObjects2[5].Value.ToString(), dateTime);
                }
                else if (myObjects2[5].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle6);
                    rough1.rjButton6.BackColor = Color.DarkGreen;
                }
                else if (myObjects2[5].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle6);
                    rough1.rjButton6.BackColor = Color.Blue;
                    CallValSet("22", dataModel.textBoxRegist22, myObjects2[5].Value.ToString(), dateTime);
                }
                 AutoScrollMax();
            };

            myObjects2[6].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[6].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime7.Text = time;

                string regId = "23";
                if (!string.IsNullOrEmpty(myElapseTime[22]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[22], regId);
                }

                myElapseTime[22] = "00:00";

                /*if (myObjects[22].Value == 261)
                {

                    myStopWatchObjects[22].Start();
                    rough1.myRjButton7.Text = "00:00";
                    myStopWatchObjects[22].Reset();
                }
                else
                {*/
                myStopWatchObjects[22].Stop();
                    rough1.myRjButton7.Text = "00:00";
                    myStopWatchObjects[22].Reset();
                    myStopWatchObjects[22].Start();
                //}

                if (myObjects2[6].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle7);
                    rough1.rjButton7.BackColor = Color.Red;
                    CallValSet("23", dataModel.textBoxRegist23, myObjects2[6].Value.ToString(), dateTime);
                }
                else if (myObjects2[6].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle7);
                    rough1.rjButton7.BackColor = Color.Orange;
                    CallValSet("23", dataModel.textBoxRegist23, myObjects2[6].Value.ToString(), dateTime);
                }
                else if (myObjects2[6].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle7);
                    rough1.rjButton7.BackColor = Color.DarkGreen;
                }
                else if (myObjects2[6].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle7);
                    rough1.rjButton7.BackColor = Color.Blue;
                    CallValSet("23", dataModel.textBoxRegist23, myObjects2[6].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects2[7].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[7].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime8.Text = time;

                string regId = "24";
                if (!string.IsNullOrEmpty(myElapseTime[23]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[23], regId);
                }

                myElapseTime[23] = "00:00";

                /*if (myObjects[23].Value == 261)
                {

                    myStopWatchObjects[23].Start();
                    rough1.myRjButton8.Text = "00:00";
                    myStopWatchObjects[23].Reset();
                }
                else
                {*/
                myStopWatchObjects[23].Stop();
                    rough1.myRjButton8.Text = "00:00";
                    myStopWatchObjects[23].Reset();
                    myStopWatchObjects[23].Start();
                //}

                if (myObjects2[7].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle8);
                    rough1.rjButton8.BackColor = Color.Red;
                    CallValSet("24", dataModel.textBoxRegist24, myObjects2[7].Value.ToString(), dateTime);
                }
                else if (myObjects2[7].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle8);
                    rough1.rjButton8.BackColor = Color.Orange;
                    CallValSet("24", dataModel.textBoxRegist24, myObjects2[7].Value.ToString(), dateTime);
                }
                else if (myObjects2[7].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle8);
                    rough1.rjButton8.BackColor = Color.DarkGreen;
                }
                else if (myObjects2[7].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle8);
                    rough1.rjButton8.BackColor = Color.Blue;
                    CallValSet("24", dataModel.textBoxRegist24, myObjects2[7].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects2[8].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[8].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime9.Text = time;

                string regId = "25";
                if (!string.IsNullOrEmpty(myElapseTime[24]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[24], regId);
                }

                myElapseTime[24] = "00:00";

                /*if (myObjects[24].Value == 261)
                {

                    myStopWatchObjects[24].Start();
                    rough1.myRjButton9.Text = "00:00";
                    myStopWatchObjects[24].Reset();
                }
                else
                {*/
                myStopWatchObjects[24].Stop();
                rough1.myRjButton9.Text = "00:00";
                    myStopWatchObjects[24].Reset();
                    myStopWatchObjects[24].Start();
                //}

                if (myObjects2[8].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle9);
                    rough1.rjButton9.BackColor = Color.Red;
                    CallValSet("25", dataModel.textBoxRegist25, myObjects2[8].Value.ToString(), dateTime);
                }
                else if (myObjects2[8].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle9);
                    rough1.rjButton9.BackColor = Color.Orange;
                    CallValSet("25", dataModel.textBoxRegist25, myObjects2[8].Value.ToString(), dateTime);
                }
                else if (myObjects2[8].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle9);
                    rough1.rjButton9.BackColor = Color.DarkGreen;
                }
                else if (myObjects2[8].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle9);
                    rough1.rjButton9.BackColor = Color.Blue;
                    CallValSet("25", dataModel.textBoxRegist25, myObjects2[8].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects2[9].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[9].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime10.Text = time;

                string regId = "26";
                if (!string.IsNullOrEmpty(myElapseTime[25]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[25], regId);
                }

                myElapseTime[25] = "00:00";

                /*if (myObjects[25].Value == 261)
                {

                    myStopWatchObjects[25].Start();
                    rough1.myRjButton10.Text = "00:00";
                    myStopWatchObjects[25].Reset();
                }
                else
                {*/
                myStopWatchObjects[25].Stop();
                    rough1.myRjButton10.Text = "00:00";
                    myStopWatchObjects[25].Reset();
                    myStopWatchObjects[25].Start();
                //}

                if (myObjects2[9].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle10);
                    rough1.rjButton10.BackColor = Color.Red;
                    CallValSet("26", dataModel.textBoxRegist26, myObjects2[9].Value.ToString(), dateTime);
                }
                else if (myObjects2[9].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle10);
                    rough1.rjButton10.BackColor = Color.Orange;
                    CallValSet("26", dataModel.textBoxRegist26, myObjects2[9].Value.ToString(), dateTime);
                }
                else if (myObjects2[9].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle10);
                    rough1.rjButton10.BackColor = Color.DarkGreen;
                }
                else if (myObjects2[9].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle10);
                    rough1.rjButton10.BackColor = Color.Blue;
                    CallValSet("26", dataModel.textBoxRegist26, myObjects2[9].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects2[10].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[10].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime11.Text = time;

                string regId = "27";
                if (!string.IsNullOrEmpty(myElapseTime[26]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[26], regId);
                }

                myElapseTime[26] = "00:00";

                /*if (myObjects[26].Value == 261)
                {
                    myStopWatchObjects[26].Start();
                    rough1.myRjButton11.Text = "00:00";
                    myStopWatchObjects[26].Reset();
                }
                else
                {*/
                myStopWatchObjects[26].Stop();
                    rough1.myRjButton11.Text = "00:00";
                    myStopWatchObjects[26].Reset();
                    myStopWatchObjects[26].Start();
                //}

                if (myObjects2[10].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle11);
                    rough1.rjButton11.BackColor = Color.Red;
                    CallValSet("27", dataModel.textBoxRegist27, myObjects2[10].Value.ToString(), dateTime);
                }
                else if (myObjects2[10].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle11);
                    rough1.rjButton11.BackColor = Color.Orange;
                    CallValSet("27", dataModel.textBoxRegist27, myObjects2[10].Value.ToString(), dateTime);
                }
                else if (myObjects2[10].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle11);
                    rough1.rjButton11.BackColor = Color.DarkGreen;
                }
                else if (myObjects2[10].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle11);
                    rough1.rjButton11.BackColor = Color.Blue;
                    CallValSet("27", dataModel.textBoxRegist27, myObjects2[10].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects2[11].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[11].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime12.Text = time;

                string regId = "28";
                if (!string.IsNullOrEmpty(myElapseTime[27]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[27], regId);
                }

                myElapseTime[27] = "00:00";

                /*if (myObjects[27].Value == 261)
                {

                    myStopWatchObjects[27].Start();
                    rough1.myRjButton12.Text = "00:00";
                    myStopWatchObjects[27].Reset();
                }
                else
                {*/
                myStopWatchObjects[27].Stop();
                    rough1.myRjButton12.Text = "00:00";
                myStopWatchObjects[27].Reset();
                myStopWatchObjects[27].Start();
                //}

                if (myObjects2[11].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle12);
                    rough1.rjButton12.BackColor = Color.Red;
                    CallValSet("28", dataModel.textBoxRegist28, myObjects2[11].Value.ToString(), dateTime);
                }
                else if (myObjects2[11].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle12);
                    rough1.rjButton12.BackColor = Color.Orange;
                    CallValSet("28", dataModel.textBoxRegist28, myObjects2[11].Value.ToString(), dateTime);
                }
                else if (myObjects2[11].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle12);
                    rough1.rjButton12.BackColor = Color.DarkGreen;
                }
                else if (myObjects[27].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle12);
                    rough1.rjButton12.BackColor = Color.Blue;
                    CallValSet("28", dataModel.textBoxRegist28, myObjects2[11].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects2[12].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[12].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime13.Text = time;

                string regId = "29";
                if (!string.IsNullOrEmpty(myElapseTime[28]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[28], regId);
                }

                myElapseTime[28] = "00:00";

                /*if (myObjects[28].Value == 261)
                {

                    myStopWatchObjects[28].Start();
                    rough1.myRjButton13.Text = "00:00";
                    myStopWatchObjects[28].Reset();
                }
                else
                {*/
                myStopWatchObjects[28].Stop();
                    rough1.myRjButton13.Text = "00:00";
                    myStopWatchObjects[28].Reset();
                    myStopWatchObjects[28].Start();
                //}

                if (myObjects2[12].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle13);
                    rough1.rjButton13.BackColor = Color.Red;
                    CallValSet("29", dataModel.textBoxRegist29, myObjects2[12].Value.ToString(), dateTime);
                }
                else if (myObjects2[12].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle13);
                    rough1.rjButton13.BackColor = Color.Orange;
                    CallValSet("29", dataModel.textBoxRegist29, myObjects2[12].Value.ToString(), dateTime);
                }
                else if (myObjects2[12].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle13);
                    rough1.rjButton13.BackColor = Color.DarkGreen;
                }
                else if (myObjects2[12].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle13);
                    rough1.rjButton13.BackColor = Color.Blue;
                    CallValSet("29", dataModel.textBoxRegist29, myObjects2[12].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects2[13].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[13].DidChange += () => {
                Console.WriteLine("changed!");
            
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime14.Text = time;

                string regId = "30";
                if (!string.IsNullOrEmpty(myElapseTime[29]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[29], regId);
                }

                myElapseTime[29] = "00:00";

                /*if (myObjects[29].Value == 261)
                {

                    myStopWatchObjects[29].Start();
                    rough1.myRjButton14.Text = "00:00";
                    myStopWatchObjects[29].Reset();
                }
                else
                {*/
                myStopWatchObjects[29].Stop();
                    rough1.myRjButton14.Text = "00:00";
                    myStopWatchObjects[29].Reset();
                    myStopWatchObjects[29].Start();
                //}

                if (myObjects2[13].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle14);
                    rough1.rjButton14.BackColor = Color.Red;
                    CallValSet("30", dataModel.textBoxRegist30, myObjects2[13].Value.ToString(), dateTime);
                }
                else if (myObjects2[13].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle14);
                    rough1.rjButton14.BackColor = Color.Orange;
                    CallValSet("30", dataModel.textBoxRegist30, myObjects2[13].Value.ToString(), dateTime);
                }
                else if (myObjects2[13].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle14);
                    rough1.rjButton14.BackColor = Color.DarkGreen;
                }
                else if (myObjects2[13].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle14);
                    rough1.rjButton14.BackColor = Color.Blue;
                    CallValSet("30", dataModel.textBoxRegist30, myObjects2[13].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects2[14].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[14].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime15.Text = time;

                string regId = "31";
                if (!string.IsNullOrEmpty(myElapseTime[30]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[30], regId);
                }

                myElapseTime[30] = "00:00";

                /*if (myObjects[30].Value == 261)
                {

                    myStopWatchObjects[30].Start();
                    rough1.myRjButton15.Text = "00:00";
                    myStopWatchObjects[30].Reset();
                }
                else
                {*/
                myStopWatchObjects[30].Stop();
                    rough1.myRjButton15.Text = "00:00";
                    myStopWatchObjects[30].Reset();
                    myStopWatchObjects[30].Start();
               // }

                if (myObjects2[14].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle15);
                    rough1.rjButton15.BackColor = Color.Red;
                    CallValSet("31", dataModel.textBoxRegist31, myObjects2[14].Value.ToString(), dateTime);
                }
                else if (myObjects2[14].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle15);
                    rough1.rjButton15.BackColor = Color.Orange;
                    CallValSet("31", dataModel.textBoxRegist31, myObjects2[14].Value.ToString(), dateTime);
                }
                else if (myObjects2[14].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle15);
                    rough1.rjButton15.BackColor = Color.DarkGreen;
                }
                else if (myObjects2[14].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle15);
                    rough1.rjButton15.BackColor = Color.Blue;
                    CallValSet("31", dataModel.textBoxRegist31, myObjects2[14].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects2[15].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects2[15].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough1.rjButtonTime16.Text = time;

                string regId = "32";
                if (!string.IsNullOrEmpty(myElapseTime[31]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[31], regId);
                }

                myElapseTime[31] = "00:00";

                /*if (myObjects[31].Value == 261)
                {
                    myStopWatchObjects[31].Start();
                    rough1.myRjButton16.Text = "00:00";
                    myStopWatchObjects[31].Reset();
                }
                else
                {*/
                myStopWatchObjects[31].Stop();
                    rough1.myRjButton16.Text = "00:00";
                    myStopWatchObjects[31].Reset();
                    myStopWatchObjects[31].Start();
                //}

                if (myObjects2[15].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle16);
                    rough1.rjButton16.BackColor = Color.Red;
                    CallValSet("32", dataModel.textBoxRegist32, myObjects2[15].Value.ToString(), dateTime);

                }
                else if (myObjects2[15].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle16);
                    rough1.rjButton16.BackColor = Color.Orange;
                    CallValSet("32", dataModel.textBoxRegist32, myObjects2[15].Value.ToString(), dateTime);
                }
                else if (myObjects2[15].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough1.roundPanelWithoutTitle16);
                    rough1.rjButton16.BackColor = Color.DarkGreen;
                }
                else if (myObjects2[15].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough1.roundPanelWithoutTitle16);
                    rough1.rjButton16.BackColor = Color.Blue;
                    CallValSet("32", dataModel.textBoxRegist32, myObjects2[15].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++S4+++++++++++++++++++++++++++++++++++++++++++++++++++++++
            myObjects3[0].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[0].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime1.Text = time;

                string regId = "33";
                if (!string.IsNullOrEmpty(myElapseTime[32]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[32], regId);
                }

                myElapseTime[32] = "00:00";

                /*if (myObjects[32].Value == 261)
                {

                    myStopWatchObjects[32].Stop();
                    myStopWatchObjects[32].Reset();
                    rough2.myRjButton1.Text = "00:00";
                }
                else
                {*/
                myStopWatchObjects[32].Stop();
                    myStopWatchObjects[32].Reset();
                    rough2.myRjButton1.Text = "00:00";
                    myStopWatchObjects[32].Start();
                //}
                if (myObjects3[0].Value == 258)
                {
                    PlayingSound();
                    // This is the last one i have changed ------------------------
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle1);
                    rough2.rjButton1.BackColor = Color.Red;
                    CallValSet("33", dataModel.textBoxRegist33, myObjects3[0].Value.ToString(), dateTime);
                }
                else if (myObjects3[0].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle1);
                    rough2.rjButton1.BackColor = Color.Orange;
                    CallValSet("33", dataModel.textBoxRegist33, myObjects3[0].Value.ToString(), dateTime);
                }
                else if (myObjects3[0].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle1);
                    rough2.rjButton1.BackColor = Color.DarkGreen;
                }
                else if (myObjects3[0].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle1);
                    rough2.rjButton1.BackColor = Color.Blue;
                    CallValSet("33", dataModel.textBoxRegist33, myObjects[32].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects3[1].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[1].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime2.Text = time;

                string regId = "34";
                if (!string.IsNullOrEmpty(myElapseTime[33]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[33], regId);
                }

                myElapseTime[33] = "00:00";

                /*if (myObjects[33].Value == 261)
                {

                    myStopWatchObjects[33].Start();
                    rough2.myRjButton2.Text = "00:00";
                    myStopWatchObjects[33].Reset();
                }
                else
                {*/
                myStopWatchObjects[33].Stop();
                    rough2.myRjButton2.Text = "00:00";
                    myStopWatchObjects[33].Reset();
                    myStopWatchObjects[33].Start();
                //}

                if (myObjects3[1].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle2);
                    rough2.rjButton2.BackColor = Color.Red;
                    CallValSet("34", dataModel.textBoxRegist34, myObjects3[1].Value.ToString(), dateTime);
                }
                else if (myObjects3[1].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle2);
                    rough2.rjButton2.BackColor = Color.Orange;
                    CallValSet("34", dataModel.textBoxRegist34, myObjects3[1].Value.ToString(), dateTime);
                }
                else if (myObjects3[1].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle2);
                    rough2.rjButton2.BackColor = Color.DarkGreen;
                }
                else if (myObjects3[1].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle2);
                    rough2.rjButton2.BackColor = Color.Blue;
                    CallValSet("34", dataModel.textBoxRegist34, myObjects3[1].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects3[2].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[2].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime3.Text = time;

                string regId = "35";
                if (!string.IsNullOrEmpty(myElapseTime[34]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[34], regId);
                }

                myElapseTime[34] = "00:00";

                /*if (myObjects[34].Value == 261)
                {

                    myStopWatchObjects[34].Start();
                    rough2.myRjButton3.Text = "00:00";
                    myStopWatchObjects[34].Reset();
                }
                else
                {*/
                myStopWatchObjects[34].Stop();
                    rough2.myRjButton3.Text = "00:00";
                    myStopWatchObjects[34].Reset();
                    myStopWatchObjects[34].Start();
                //}

                if (myObjects3[2].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle3);
                    rough2.rjButton3.BackColor = Color.Red;
                    CallValSet("35", dataModel.textBoxRegist35, myObjects3[2].Value.ToString(), dateTime);
                }
                else if (myObjects3[2].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle3);
                    rough2.rjButton3.BackColor = Color.Orange;
                    CallValSet("35", dataModel.textBoxRegist35, myObjects3[2].Value.ToString(), dateTime);
                }
                else if (myObjects3[2].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle3);
                    rough2.rjButton3.BackColor = Color.DarkGreen;
                }
                else if (myObjects3[2].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle3);
                    rough2.rjButton3.BackColor = Color.Blue;
                    CallValSet("35", dataModel.textBoxRegist35, myObjects3[2].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects3[3].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[3].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime4.Text = time;

                string regId = "36";
                if (!string.IsNullOrEmpty(myElapseTime[35]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[35], regId);
                }

                myElapseTime[35] = "00:00";

                /*if (myObjects[35].Value == 261)
                {

                    myStopWatchObjects[35].Start();
                    rough2.myRjButton4.Text = "00:00";
                    myStopWatchObjects[35].Reset();
                }
                else
                {*/
                myStopWatchObjects[35].Stop();
                    rough2.myRjButton4.Text = "00:00";
                    myStopWatchObjects[35].Reset();
                    myStopWatchObjects[35].Start();
                //}

                if (myObjects3[3].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle4);
                    rough2.rjButton4.BackColor = Color.Red;
                    CallValSet("36", dataModel.textBoxRegist36, myObjects3[3].Value.ToString(), dateTime);
                }
                else if (myObjects3[3].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle4);
                    rough2.rjButton4.BackColor = Color.Orange;
                    CallValSet("36", dataModel.textBoxRegist36, myObjects3[3].Value.ToString(), dateTime);
                }
                else if (myObjects3[3].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle4);
                    rough2.rjButton4.BackColor = Color.DarkGreen;
                }
                else if (myObjects3[3].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle4);
                    rough2.rjButton4.BackColor = Color.Blue;
                    CallValSet("36", dataModel.textBoxRegist36, myObjects3[3].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects3[4].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[4].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime5.Text = time;

                string regId = "37";
                if (!string.IsNullOrEmpty(myElapseTime[36]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[36], regId);
                }

                myElapseTime[36] = "00:00";
                /*if (myObjects[36].Value == 261)
                {

                    myStopWatchObjects[36].Start();
                    rough2.myRjButton5.Text = "00:00";
                    myStopWatchObjects[36].Reset();
                }
                else
                {*/
                    myStopWatchObjects[36].Stop();
                    rough2.myRjButton5.Text = "00:00";
                    myStopWatchObjects[36].Reset();
                    myStopWatchObjects[36].Start();
                //}

                if (myObjects3[4].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle5);
                    rough2.rjButton5.BackColor = Color.Red;
                    CallValSet("37", dataModel.textBoxRegist37, myObjects3[4].Value.ToString(), dateTime);
                }
                else if (myObjects3[4].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle5);
                    rough2.rjButton5.BackColor = Color.Orange;
                    CallValSet("37", dataModel.textBoxRegist37, myObjects3[4].Value.ToString(), dateTime);
                }
                else if (myObjects3[4].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle5);
                    rough2.rjButton5.BackColor = Color.DarkGreen;
                }
                else if (myObjects3[4].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle5);
                    rough2.rjButton5.BackColor = Color.Blue;
                    CallValSet("37", dataModel.textBoxRegist37, myObjects3[4].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects3[5].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[5].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime6.Text = time;

                string regId = "38";
                if (!string.IsNullOrEmpty(myElapseTime[37]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[37], regId);
                }

                myElapseTime[37] = "00:00";

                /*if (myObjects[37].Value == 261)
                {

                    myStopWatchObjects[37].Start();
                    rough2.myRjButton6.Text = "00:00";
                    myStopWatchObjects[37].Reset();
                }
                else
                {*/
                myStopWatchObjects[37].Stop();
                    rough2.myRjButton6.Text = "00:00";
                    myStopWatchObjects[37].Reset();
                    myStopWatchObjects[37].Start();
                //}

                if (myObjects3[5].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle6);
                    rough2.rjButton6.BackColor = Color.Red;
                    CallValSet("38", dataModel.textBoxRegist38, myObjects3[5].Value.ToString(), dateTime);
                }
                else if (myObjects3[5].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle6);
                    rough2.rjButton6.BackColor = Color.Orange;
                    CallValSet("38", dataModel.textBoxRegist38, myObjects3[5].Value.ToString(), dateTime);
                }
                else if (myObjects3[5].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle6);
                    rough2.rjButton6.BackColor = Color.DarkGreen;
                }
                else if (myObjects3[5].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle6);
                    rough2.rjButton6.BackColor = Color.Blue;
                    CallValSet("38", dataModel.textBoxRegist38, myObjects3[5].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects3[6].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[6].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime7.Text = time;

                string regId = "39";
                if (!string.IsNullOrEmpty(myElapseTime[38]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[38], regId);
                }

                myElapseTime[38] = "00:00";

                /*if (myObjects[38].Value == 261)
                {

                    myStopWatchObjects[38].Start();
                    rough2.myRjButton7.Text = "00:00";
                    myStopWatchObjects[38].Reset();
                }
                else
                {*/
                myStopWatchObjects[38].Stop();
                    rough2.myRjButton7.Text = "00:00";
                    myStopWatchObjects[38].Reset();
                    myStopWatchObjects[38].Start();
                //}

                if (myObjects3[6].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle7);
                    rough2.rjButton7.BackColor = Color.Red;
                    CallValSet("39", dataModel.textBoxRegist39, myObjects3[6].Value.ToString(), dateTime);
                }
                else if (myObjects3[6].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle7);
                    rough2.rjButton7.BackColor = Color.Orange;
                    CallValSet("39", dataModel.textBoxRegist39, myObjects3[6].Value.ToString(), dateTime);
                }
                else if (myObjects3[6].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle7);
                    rough2.rjButton7.BackColor = Color.DarkGreen;
                }
                else if (myObjects[38].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle7);
                    rough2.rjButton7.BackColor = Color.Blue;
                    CallValSet("39", dataModel.textBoxRegist39, myObjects3[6].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects3[7].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[7].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime8.Text = time;

                string regId = "40";
                if (!string.IsNullOrEmpty(myElapseTime[39]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[39], regId);
                }

                myElapseTime[39] = "00:00";

                /*if (myObjects[39].Value == 261)
                {

                    myStopWatchObjects[39].Start();
                    rough2.myRjButton8.Text = "00:00";
                    myStopWatchObjects[39].Reset();
                }
                else
                {*/
                myStopWatchObjects[39].Stop();
                    rough2.myRjButton8.Text = "00:00";
                    myStopWatchObjects[39].Reset();
                    myStopWatchObjects[39].Start();
               // }

                if (myObjects3[7].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle8);
                    rough2.rjButton8.BackColor = Color.Red;
                    CallValSet("40", dataModel.textBoxRegist40, myObjects3[7].Value.ToString(), dateTime);
                }
                else if (myObjects3[7].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle8);
                    rough2.rjButton8.BackColor = Color.Orange;
                    CallValSet("40", dataModel.textBoxRegist40, myObjects3[7].Value.ToString(), dateTime);
                }
                else if (myObjects3[7].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle8);
                    rough2.rjButton8.BackColor = Color.DarkGreen;
                }
                else if (myObjects3[7].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle8);
                    rough2.rjButton8.BackColor = Color.Blue;
                    CallValSet("40", dataModel.textBoxRegist40, myObjects3[7].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects3[8].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[8].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime9.Text = time;

                string regId = "41";
                if (!string.IsNullOrEmpty(myElapseTime[40]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[40], regId);
                }

                myElapseTime[40] = "00:00";

                /*if (myObjects[40].Value == 261)
                {

                    myStopWatchObjects[40].Start();
                    rough2.myRjButton9.Text = "00:00";
                    myStopWatchObjects[40].Reset();
                }
                else
                {*/
                myStopWatchObjects[40].Stop();
                    rough2.myRjButton9.Text = "00:00";
                    myStopWatchObjects[40].Reset();
                    myStopWatchObjects[40].Start();
                //}

                if (myObjects3[8].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle9);
                    rough2.rjButton9.BackColor = Color.Red;
                    CallValSet("41", dataModel.textBoxRegist41, myObjects3[8].Value.ToString(), dateTime);
                }
                else if (myObjects3[8].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle9);
                    rough2.rjButton9.BackColor = Color.Orange;
                    CallValSet("41", dataModel.textBoxRegist41, myObjects3[8].Value.ToString(), dateTime);
                }
                else if (myObjects3[8].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle9);
                    rough2.rjButton9.BackColor = Color.DarkGreen;
                }
                else if (myObjects3[8].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle9);
                    rough2.rjButton9.BackColor = Color.Blue;
                    CallValSet("41", dataModel.textBoxRegist41, myObjects3[8].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects3[9].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[9].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime10.Text = time;

                string regId = "42";
                if (!string.IsNullOrEmpty(myElapseTime[41]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[41], regId);
                }

                myElapseTime[41] = "00:00";

                /*if (myObjects[41].Value == 261)
                {

                    myStopWatchObjects[41].Start();
                    rough2.myRjButton10.Text = "00:00";
                    myStopWatchObjects[41].Reset();
                }
                else
                {*/
                myStopWatchObjects[41].Stop();
                    rough2.myRjButton10.Text = "00:00";
                    myStopWatchObjects[41].Reset();
                    myStopWatchObjects[41].Start();
                //}

                if (myObjects3[9].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle10);
                    rough2.rjButton10.BackColor = Color.Red;
                    CallValSet("42", dataModel.textBoxRegist42, myObjects3[9].Value.ToString(), dateTime);
                }
                else if (myObjects3[9].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle10);
                    rough2.rjButton10.BackColor = Color.Orange;
                    CallValSet("42", dataModel.textBoxRegist42, myObjects3[9].Value.ToString(), dateTime);
                }
                else if (myObjects3[9].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle10);
                    rough2.rjButton10.BackColor = Color.DarkGreen;
                }
                else if (myObjects3[9].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle10);
                    rough2.rjButton10.BackColor = Color.Blue;
                    CallValSet("42", dataModel.textBoxRegist42, myObjects3[9].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects3[10].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[10].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime11.Text = time;

                string regId = "43";
                if (!string.IsNullOrEmpty(myElapseTime[42]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[42], regId);
                }

                myElapseTime[42] = "00:00";

                /*if (myObjects[42].Value == 261)
                {

                    myStopWatchObjects[42].Start();
                    rough2.myRjButton11.Text = "00:00";
                    myStopWatchObjects[42].Reset();
                }
                else
                {*/
                myStopWatchObjects[42].Stop();
                    rough2.myRjButton11.Text = "00:00";
                    myStopWatchObjects[42].Reset();
                    myStopWatchObjects[42].Start();
                //}

                if (myObjects3[10].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle11);
                    rough2.rjButton11.BackColor = Color.Red;
                    CallValSet("43", dataModel.textBoxRegist43, myObjects3[10].Value.ToString(), dateTime);
                }
                else if (myObjects3[10].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle11);
                    rough2.rjButton11.BackColor = Color.Orange;
                    CallValSet("43", dataModel.textBoxRegist43, myObjects3[10].Value.ToString(), dateTime);
                }
                else if (myObjects3[10].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle11);
                    rough2.rjButton11.BackColor = Color.DarkGreen;
                }
                else if (myObjects3[10].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle11);
                    rough2.rjButton11.BackColor = Color.Blue;
                    CallValSet("43", dataModel.textBoxRegist43, myObjects3[10].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects3[11].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[11].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime12.Text = time;

                string regId = "44";
                if (!string.IsNullOrEmpty(myElapseTime[43]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[43], regId);
                }

                myElapseTime[43] = "00:00";


                /*if (myObjects[43].Value == 261)
                {

                    myStopWatchObjects[43].Start();
                    rough2.myRjButton12.Text = "00:00";
                    myStopWatchObjects[43].Reset();
                }
                else
                {*/
                myStopWatchObjects[43].Stop();
                    rough2.myRjButton12.Text = "00:00";
                    myStopWatchObjects[43].Reset();
                    myStopWatchObjects[43].Start();
                //}

                if (myObjects3[11].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle12);
                    rough2.rjButton12.BackColor = Color.Red;
                    CallValSet("44", dataModel.textBoxRegist44, myObjects3[11].Value.ToString(), dateTime);
                }
                else if (myObjects3[11].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle12);
                    rough2.rjButton12.BackColor = Color.Orange;
                    CallValSet("44", dataModel.textBoxRegist44, myObjects3[11].Value.ToString(), dateTime);
                }
                else if (myObjects3[11].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle12);
                    rough2.rjButton12.BackColor = Color.DarkGreen;
                }
                else if (myObjects3[11].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle12);
                    rough2.rjButton12.BackColor = Color.Blue;
                    CallValSet("44", dataModel.textBoxRegist44, myObjects3[11].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects3[12].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[12].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime13.Text = time;

                string regId = "45";
                if (!string.IsNullOrEmpty(myElapseTime[44]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[44], regId);
                }

                myElapseTime[44] = "00:00";

                /*if (myObjects[44].Value == 261)
                {

                    myStopWatchObjects[44].Start();
                    rough2.myRjButton13.Text = "00:00";
                    myStopWatchObjects[44].Reset();
                }
                else
                {*/
                myStopWatchObjects[44].Stop();
                    rough2.myRjButton13.Text = "00:00";
                    myStopWatchObjects[44].Reset();
                    myStopWatchObjects[44].Start();
                //}

                if (myObjects3[12].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle13);
                    rough2.rjButton13.BackColor = Color.Red;
                    CallValSet("45", dataModel.textBoxRegist45, myObjects3[12].Value.ToString(), dateTime);
                }
                else if (myObjects3[12].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle13);
                    rough2.rjButton13.BackColor = Color.Orange;
                    CallValSet("45", dataModel.textBoxRegist45, myObjects3[12].Value.ToString(), dateTime);
                }
                else if (myObjects3[12].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle13);
                    rough2.rjButton13.BackColor = Color.DarkGreen;
                }
                else if (myObjects3[12].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle13);
                    rough2.rjButton13.BackColor = Color.Blue;
                    CallValSet("45", dataModel.textBoxRegist45, myObjects3[12].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects3[13].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[13].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime14.Text = time;

                string regId = "46";
                if (!string.IsNullOrEmpty(myElapseTime[45]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[45], regId);
                }

                myElapseTime[45] = "00:00";

                /*if (myObjects[45].Value == 261)
                {

                    myStopWatchObjects[45].Start();
                    rough2.myRjButton14.Text = "00:00";
                    myStopWatchObjects[45].Reset();
                }
                else
                {*/
                myStopWatchObjects[45].Stop();
                    rough2.myRjButton14.Text = "00:00";
                    myStopWatchObjects[45].Reset();
                    myStopWatchObjects[45].Start();
                //}

                if (myObjects3[13].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle14);
                    rough2.rjButton14.BackColor = Color.Red;
                    CallValSet("46", dataModel.textBoxRegist46, myObjects3[13].Value.ToString(), dateTime);
                }
                else if (myObjects3[13].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle14);
                    rough2.rjButton14.BackColor = Color.Orange;
                    CallValSet("46", dataModel.textBoxRegist46, myObjects3[13].Value.ToString(), dateTime);
                }
                else if (myObjects3[13].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle14);
                    rough2.rjButton14.BackColor = Color.DarkGreen;
                }
                else if (myObjects3[13].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle14);
                    rough2.rjButton14.BackColor = Color.Blue;
                    CallValSet("46", dataModel.textBoxRegist46, myObjects3[13].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects3[14].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[14].DidChange += () => {
                Console.WriteLine("changed!");
       
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime15.Text = time;

                string regId = "47";
                if (!string.IsNullOrEmpty(myElapseTime[46]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[46], regId);
                }

                myElapseTime[46] = "00:00";

                /*if (myObjects[46].Value == 261)
                {

                    myStopWatchObjects[46].Start();
                    rough2.myRjButton15.Text = "00:00";
                    myStopWatchObjects[46].Reset();
                }
                else
                {*/
                myStopWatchObjects[46].Stop();
                    rough2.myRjButton15.Text = "00:00";
                    myStopWatchObjects[46].Reset();
                    myStopWatchObjects[46].Start();
                //}

                if (myObjects3[14].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle15);
                    rough2.rjButton15.BackColor = Color.Red;
                    CallValSet("47", dataModel.textBoxRegist47, myObjects3[14].Value.ToString(), dateTime);
                }
                else if (myObjects3[14].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle15);
                    rough2.rjButton15.BackColor = Color.Orange;
                    CallValSet("47", dataModel.textBoxRegist47, myObjects3[14].Value.ToString(), dateTime);
                }
                else if (myObjects3[14].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle15);
                    rough2.rjButton15.BackColor = Color.DarkGreen;
                }
                else if (myObjects3[14].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle15);
                    rough2.rjButton15.BackColor = Color.Blue;
                    CallValSet("47", dataModel.textBoxRegist47, myObjects3[14].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects3[15].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects3[15].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough2.rjButtonTime16.Text = time;

                string regId = "48";
                if (!string.IsNullOrEmpty(myElapseTime[47]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[47], regId);
                }

                myElapseTime[47] = "00:00";

                /*if (myObjects[47].Value == 261)
                {
                    myStopWatchObjects[47].Start();
                    rough2.myRjButton16.Text = "00:00";
                    myStopWatchObjects[47].Reset();
                }
                else
                {*/
                myStopWatchObjects[47].Stop();
                    rough2.myRjButton16.Text = "00:00";
                    myStopWatchObjects[47].Reset();
                    myStopWatchObjects[47].Start();
                //}

                if (myObjects3[15].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle16);
                    rough2.rjButton16.BackColor = Color.Red;
                    CallValSet("48", dataModel.textBoxRegist48, myObjects3[15].Value.ToString(), dateTime);
                }
                else if (myObjects3[15].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle16);
                    rough2.rjButton16.BackColor = Color.Orange;
                    CallValSet("48", dataModel.textBoxRegist48, myObjects3[15].Value.ToString(), dateTime);
                }
                else if (myObjects3[15].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough2.roundPanelWithoutTitle16);
                    rough2.rjButton16.BackColor = Color.DarkGreen;
                }
                else if (myObjects3[15].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough2.roundPanelWithoutTitle16);
                    rough2.rjButton16.BackColor = Color.Blue;
                    CallValSet("48", dataModel.textBoxRegist48, myObjects3[15].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++S4+++++++++++++++++++++++++++++++++++++++++++++++++++++++
            myObjects4[0].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[0].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime1.Text = time;

                string regId = "49";
                if (!string.IsNullOrEmpty(myElapseTime[48]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[48], regId);
                }

                myElapseTime[48] = "00:00";

                /*if (myObjects[48].Value == 261)
                {

                    myStopWatchObjects[48].Stop();
                    myStopWatchObjects[48].Reset();
                    rough3.myRjButton1.Text = "00:00";
                }
                else
                {*/
                myStopWatchObjects[48].Stop();
                    myStopWatchObjects[48].Reset();
                    rough3.myRjButton1.Text = "00:00";
                    myStopWatchObjects[48].Start();
                //}
                if (myObjects4[0].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle1);
                    rough3.rjButton1.BackColor = Color.Red;
                    CallValSet("49", dataModel.textBoxRegist49, myObjects4[0].Value.ToString(), dateTime);
                }
                else if (myObjects4[0].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle1);
                    rough3.rjButton1.BackColor = Color.Orange;
                    CallValSet("49", dataModel.textBoxRegist49, myObjects4[0].Value.ToString(), dateTime);
                }
                else if (myObjects4[0].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle1);
                    rough3.rjButton1.BackColor = Color.DarkGreen;
                }
                else if (myObjects4[0].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle1);
                    rough3.rjButton1.BackColor = Color.Blue;
                    CallValSet("49", dataModel.textBoxRegist49, myObjects4[0].Value.ToString(), dateTime);

                }
                AutoScrollMax();
            };

            myObjects4[1].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[1].DidChange += () => {
                Console.WriteLine("changed!");
             
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime2.Text = time;

                string regId = "50";
                if (!string.IsNullOrEmpty(myElapseTime[49]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[49], regId);
                }

                myElapseTime[49] = "00:00";

                /*if (myObjects[49].Value == 261)
                {

                    myStopWatchObjects[49].Start();
                    rough3.myRjButton2.Text = "00:00";
                    myStopWatchObjects[49].Reset();
                }
                else
                {*/
                myStopWatchObjects[49].Stop();
                    rough3.myRjButton2.Text = "00:00";
                    myStopWatchObjects[49].Reset();
                    myStopWatchObjects[49].Start();
               // }

                if (myObjects4[1].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle2);
                    rough3.rjButton2.BackColor = Color.Red;
                    CallValSet("50", dataModel.textBoxRegist50, myObjects4[1].Value.ToString(), dateTime);
                }
                else if (myObjects4[1].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle2);
                    rough3.rjButton2.BackColor = Color.Orange;
                    CallValSet("50", dataModel.textBoxRegist50, myObjects4[1].Value.ToString(), dateTime);
                }
                else if (myObjects4[1].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle2);
                    rough3.rjButton2.BackColor = Color.DarkGreen;
                }
                else if (myObjects4[1].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle2);
                    rough3.rjButton2.BackColor = Color.Blue;
                    CallValSet("50", dataModel.textBoxRegist50, myObjects4[1].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects4[2].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[2].DidChange += () => {
                Console.WriteLine("changed!");
              
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime3.Text = time;

                string regId = "51";
                if (!string.IsNullOrEmpty(myElapseTime[50]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[50], regId);
                }

                myElapseTime[50] = "00:00";

                /*if (myObjects[50].Value == 261)
                {

                    myStopWatchObjects[50].Start();
                    rough3.myRjButton3.Text = "00:00";
                    myStopWatchObjects[50].Reset();
                }
                else
                {*/
                myStopWatchObjects[50].Stop();
                    rough3.myRjButton3.Text = "00:00";
                    myStopWatchObjects[50].Reset();
                    myStopWatchObjects[50].Start();
                //}

                if (myObjects4[2].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle3);
                    rough3.rjButton3.BackColor = Color.Red;
                    CallValSet("51", dataModel.textBoxRegist51, myObjects4[2].Value.ToString(), dateTime);
                }
                else if (myObjects4[2].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle3);
                    rough3.rjButton3.BackColor = Color.Orange;
                    CallValSet("51", dataModel.textBoxRegist51, myObjects4[2].Value.ToString(), dateTime);
                }
                else if (myObjects4[2].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle3);
                    rough3.rjButton3.BackColor = Color.DarkGreen;
                }
                else if (myObjects4[2].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle3);
                    rough3.rjButton3.BackColor = Color.Blue;
                    CallValSet("51", dataModel.textBoxRegist51, myObjects4[2].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects4[3].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[3].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime4.Text = time;

                string regId = "52";
                if (!string.IsNullOrEmpty(myElapseTime[51]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[51], regId);
                }

                myElapseTime[51] = "00:00";

                /*if (myObjects[51].Value == 261)
                {

                    myStopWatchObjects[51].Start();
                    rough3.myRjButton4.Text = "00:00";
                    myStopWatchObjects[51].Reset();
                }
                else
                {*/
                myStopWatchObjects[51].Stop();
                    rough3.myRjButton4.Text = "00:00";
                    myStopWatchObjects[51].Reset();
                    myStopWatchObjects[51].Start();
                //}

                if (myObjects4[3].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle4);
                    rough3.rjButton4.BackColor = Color.Red;
                    CallValSet("52", dataModel.textBoxRegist52, myObjects4[3].Value.ToString(), dateTime);
                }
                else if (myObjects4[3].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle4);
                    rough3.rjButton4.BackColor = Color.Orange;
                    CallValSet("52", dataModel.textBoxRegist52, myObjects4[3].Value.ToString(), dateTime);
                }
                else if (myObjects4[3].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle4);
                    rough3.rjButton4.BackColor = Color.DarkGreen;
                }
                else if (myObjects4[3].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    Console.WriteLine("MY_DATA: "+dataModel.textBoxRegist52);
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle4);
                    rough3.rjButton4.BackColor = Color.Blue;
                    CallValSet("52", dataModel.textBoxRegist52, myObjects4[3].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects4[4].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[4].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime5.Text = time;

                string regId = "53";
                if (!string.IsNullOrEmpty(myElapseTime[52]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[52], regId);
                }

                myElapseTime[52] = "00:00";

                /*if (myObjects[52].Value == 261)
                {

                    myStopWatchObjects[52].Start();
                    rough3.myRjButton5.Text = "00:00";
                    myStopWatchObjects[52].Reset();
                }
                else
                {*/
                myStopWatchObjects[52].Stop();
                    rough3.myRjButton5.Text = "00:00";
                    myStopWatchObjects[52].Reset();
                    myStopWatchObjects[52].Start();
                //}

                if (myObjects4[4].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle5);
                    rough3.rjButton5.BackColor = Color.Red;
                    CallValSet("53", dataModel.textBoxRegist53, myObjects4[4].Value.ToString(), dateTime);
                }
                else if (myObjects4[4].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle5);
                    rough3.rjButton5.BackColor = Color.Orange;
                    CallValSet("53", dataModel.textBoxRegist53, myObjects4[4].Value.ToString(), dateTime);
                }
                else if (myObjects4[4].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle5);
                    rough3.rjButton5.BackColor = Color.DarkGreen;
                }
                else if (myObjects4[4].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle5);
                    rough3.rjButton5.BackColor = Color.Blue;
                    CallValSet("53", dataModel.textBoxRegist53, myObjects4[4].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects4[5].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[5].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime6.Text = time;

                string regId = "54";
                if (!string.IsNullOrEmpty(myElapseTime[53]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[53], regId);
                }

                myElapseTime[53] = "00:00";

                /*if (myObjects[53].Value == 261)
                {

                    myStopWatchObjects[53].Start();
                    rough3.myRjButton6.Text = "00:00";
                    myStopWatchObjects[53].Reset();
                }
                else
                {*/
                myStopWatchObjects[53].Stop();
                    rough3.myRjButton6.Text = "00:00";
                    myStopWatchObjects[53].Reset();
                    myStopWatchObjects[53].Start();
                //}

                if (myObjects4[5].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle6);
                    rough3.rjButton6.BackColor = Color.Red;
                    CallValSet("54", dataModel.textBoxRegist54, myObjects4[5].Value.ToString(), dateTime);
                }
                else if (myObjects4[5].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle6);
                    rough3.rjButton6.BackColor = Color.Orange;
                    CallValSet("54", dataModel.textBoxRegist54, myObjects4[5].Value.ToString(), dateTime);
                }
                else if (myObjects4[5].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle6);
                    rough3.rjButton6.BackColor = Color.DarkGreen;
                }
                else if (myObjects4[5].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle6);
                    rough3.rjButton6.BackColor = Color.Blue;
                    CallValSet("54", dataModel.textBoxRegist54, myObjects4[5].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects4[6].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[6].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime7.Text = time;

                string regId = "55";
                if (!string.IsNullOrEmpty(myElapseTime[54]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[54], regId);
                }

                myElapseTime[54] = "00:00";

                /*if (myObjects[54].Value == 261)
                {

                    myStopWatchObjects[54].Start();
                    rough3.myRjButton7.Text = "00:00";
                    myStopWatchObjects[54].Reset();
                }
                else
                {*/
                myStopWatchObjects[54].Stop();
                    rough3.myRjButton7.Text = "00:00";
                    myStopWatchObjects[54].Reset();
                    myStopWatchObjects[54].Start();
                //}

                if (myObjects4[6].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle7);
                    rough3.rjButton7.BackColor = Color.Red;
                    CallValSet("55", dataModel.textBoxRegist55, myObjects4[6].Value.ToString(), dateTime);
                }
                else if (myObjects4[6].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle7);
                    rough3.rjButton7.BackColor = Color.Orange;
                    CallValSet("55", dataModel.textBoxRegist55, myObjects4[6].Value.ToString(), dateTime);
                }
                else if (myObjects4[6].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle7);
                    rough3.rjButton7.BackColor = Color.DarkGreen;
                }
                else if (myObjects4[6].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle7);
                    rough3.rjButton7.BackColor = Color.Blue;
                    CallValSet("55", dataModel.textBoxRegist55, myObjects4[6].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects4[7].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[7].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime8.Text = time;

                string regId = "56";
                if (!string.IsNullOrEmpty(myElapseTime[55]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[55], regId);
                }

                myElapseTime[55] = "00:00";

                /*if (myObjects[54].Value == 261)
                {

                    myStopWatchObjects[54].Start();
                    rough3.myRjButton7.Text = "00:00";
                    myStopWatchObjects[54].Reset();
                }
                else
                {*/
                myStopWatchObjects[55].Stop();
                rough3.myRjButton8.Text = "00:00";
                myStopWatchObjects[55].Reset();
                myStopWatchObjects[55].Start();
                //}

                if (myObjects4[7].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle8);
                    rough3.rjButton8.BackColor = Color.Red;
                    CallValSet("56", dataModel.textBoxRegist56, myObjects4[7].Value.ToString(), dateTime);
                }
                else if (myObjects4[7].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle8);
                    rough3.rjButton8.BackColor = Color.Orange;
                    CallValSet("56", dataModel.textBoxRegist56, myObjects4[7].Value.ToString(), dateTime);
                }
                else if (myObjects4[7].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle8);
                    rough3.rjButton8.BackColor = Color.DarkGreen;
                }
                else if (myObjects4[7].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle8);
                    rough3.rjButton8.BackColor = Color.Blue;
                    CallValSet("56", dataModel.textBoxRegist56, myObjects4[7].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };




            myObjects4[8].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[8].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime9.Text = time;

                string regId = "57";
                if (!string.IsNullOrEmpty(myElapseTime[56]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[56], regId);
                }

                myElapseTime[56] = "00:00";

                /*if (myObjects[57].Value == 261)
                {

                    myStopWatchObjects[57].Start();
                    rough3.myRjButton9.Text = "00:00";
                    myStopWatchObjects[57].Reset();
                }
                else
                {*/
                myStopWatchObjects[57].Stop();
                    rough3.myRjButton9.Text = "00:00";
                    myStopWatchObjects[57].Reset();
                    myStopWatchObjects[57].Start();
                //}

                if (myObjects4[8].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle9);
                    rough3.rjButton9.BackColor = Color.Red;
                    CallValSet("57", dataModel.textBoxRegist57, myObjects4[8].Value.ToString(), dateTime);
                }
                else if (myObjects4[8].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle9);
                    rough3.rjButton9.BackColor = Color.Orange;
                    CallValSet("57", dataModel.textBoxRegist57, myObjects4[8].Value.ToString(), dateTime);
                }
                else if (myObjects4[8].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle9);
                    rough3.rjButton9.BackColor = Color.DarkGreen;
                    CallValSet("57", dataModel.textBoxRegist57, myObjects4[8].Value.ToString(), dateTime);
                }
                else if (myObjects4[8].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle9);
                    rough3.rjButton9.BackColor = Color.Blue;
                    CallValSet("57", dataModel.textBoxRegist57, myObjects4[8].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects4[9].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[9].DidChange += () => {
                Console.WriteLine("changed!");
          
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime10.Text = time;

                string regId = "58";
                if (!string.IsNullOrEmpty(myElapseTime[57]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[57], regId);
                }

                myElapseTime[57] = "00:00";

                /*if (myObjects[58].Value == 261)
                {

                    myStopWatchObjects[58].Start();
                    rough3.myRjButton10.Text = "00:00";
                    myStopWatchObjects[58].Reset();
                }
                else
                {*/
                myStopWatchObjects[57].Stop();
                    rough3.myRjButton10.Text = "00:00";
                    myStopWatchObjects[57].Reset();
                    myStopWatchObjects[57].Start();
                //}

                if (myObjects4[9].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle10);
                    rough3.rjButton10.BackColor = Color.Red;
                    CallValSet("58", dataModel.textBoxRegist58, myObjects4[9].Value.ToString(), dateTime);
                }
                else if (myObjects4[9].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle10);
                    rough3.rjButton10.BackColor = Color.Orange;
                    CallValSet("58", dataModel.textBoxRegist58, myObjects4[9].Value.ToString(), dateTime);
                }
                else if (myObjects4[9].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle10);
                    rough3.rjButton10.BackColor = Color.DarkGreen;
                }
                else if (myObjects4[9].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle10);
                    rough3.rjButton10.BackColor = Color.Blue;
                    CallValSet("58", dataModel.textBoxRegist58, myObjects4[9].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects4[10].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[10].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime11.Text = time;

                string regId = "59";
                if (!string.IsNullOrEmpty(myElapseTime[58]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[58], regId);
                }

                myElapseTime[58] = "00:00";

                /*if (myObjects[59].Value == 261)
                {

                    myStopWatchObjects[59].Start();
                    rough3.myRjButton11.Text = "00:00";
                    myStopWatchObjects[59].Reset();
                }
                else
                {*/
                myStopWatchObjects[58].Stop();
                    rough3.myRjButton11.Text = "00:00";
                    myStopWatchObjects[58].Reset();
                    myStopWatchObjects[58].Start();
                //}

                if (myObjects4[10].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle11);
                    rough3.rjButton11.BackColor = Color.Red;
                    CallValSet("59", dataModel.textBoxRegist59, myObjects4[10].Value.ToString(), dateTime);
                }
                else if (myObjects4[10].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle11);
                    rough3.rjButton11.BackColor = Color.Orange;
                    CallValSet("59", dataModel.textBoxRegist59, myObjects4[10].Value.ToString(), dateTime);
                }
                else if (myObjects4[10].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle11);
                    rough3.rjButton11.BackColor = Color.DarkGreen;
                }
                else if (myObjects4[10].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle11);
                    rough3.rjButton11.BackColor = Color.Blue;
                    CallValSet("59", dataModel.textBoxRegist59, myObjects4[10].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects4[11].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[11].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime12.Text = time;

                string regId = "60";
                if (!string.IsNullOrEmpty(myElapseTime[59]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[59], regId);
                }

                myElapseTime[59] = "00:00";

                /*if (myObjects[59].Value == 261)
                {

                    myStopWatchObjects[59].Start();
                    rough3.myRjButton12.Text = "00:00";
                    myStopWatchObjects[59].Reset();
                }
                else
                {*/
                myStopWatchObjects[59].Stop();
                    rough3.myRjButton12.Text = "00:00";
                    myStopWatchObjects[59].Reset();
                    myStopWatchObjects[59].Start();
                //}

                if (myObjects4[11].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle12);
                    rough3.rjButton12.BackColor = Color.Red;
                    CallValSet("60", dataModel.textBoxRegist60, myObjects4[11].Value.ToString(), dateTime);
                }
                else if (myObjects4[11].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle12);
                    rough3.rjButton12.BackColor = Color.Orange;
                    CallValSet("60", dataModel.textBoxRegist60, myObjects4[11].Value.ToString(), dateTime);
                }
                else if (myObjects4[11].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle12);
                    rough3.rjButton12.BackColor = Color.DarkGreen;
                }
                else if (myObjects4[11].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle12);
                    rough3.rjButton12.BackColor = Color.Blue;
                    CallValSet("60", dataModel.textBoxRegist60, myObjects4[11].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects4[12].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[12].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime13.Text = time;

                string regId = "61";
                if (!string.IsNullOrEmpty(myElapseTime[60]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[60], regId);
                }

                myElapseTime[60] = "00:00";

                /*if (myObjects[60].Value == 261)
                {

                    myStopWatchObjects[60].Start();
                    rough3.myRjButton13.Text = "00:00";
                    myStopWatchObjects[60].Reset();
                }
                else
                {*/
                myStopWatchObjects[60].Stop();
                    rough3.myRjButton13.Text = "00:00";
                    myStopWatchObjects[60].Reset();
                    myStopWatchObjects[60].Start();
                //}

                if (myObjects4[12].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle13);
                    rough3.rjButton13.BackColor = Color.Red;
                    CallValSet("61", dataModel.textBoxRegist61, myObjects4[12].Value.ToString(), dateTime);
                }
                else if (myObjects4[12].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle13);
                    rough3.rjButton13.BackColor = Color.Orange;
                    CallValSet("61", dataModel.textBoxRegist61, myObjects4[12].Value.ToString(), dateTime);
                }
                else if (myObjects4[12].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle13);
                    rough3.rjButton13.BackColor = Color.DarkGreen;
                }
                else if (myObjects4[12].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle13);
                    rough3.rjButton13.BackColor = Color.Blue;
                    CallValSet("61", dataModel.textBoxRegist61, myObjects4[10].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects4[13].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[13].DidChange += () => {
                Console.WriteLine("changed!");
                
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime14.Text = time;

                string regId = "62";
                if (!string.IsNullOrEmpty(myElapseTime[61]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[61], regId);
                }

                myElapseTime[61] = "00:00";

                /*if (myObjects[61].Value == 261)
                {

                    myStopWatchObjects[61].Start();
                    rough3.myRjButton14.Text = "00:00";
                    myStopWatchObjects[61].Reset();
                }
                else
                {*/
                myStopWatchObjects[61].Stop();
                    rough3.myRjButton14.Text = "00:00";
                    myStopWatchObjects[61].Reset();
                    myStopWatchObjects[61].Start();
                //}

                if (myObjects4[13].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle14);
                    rough3.rjButton14.BackColor = Color.Red;
                    CallValSet("62", dataModel.textBoxRegist62, myObjects4[13].Value.ToString(), dateTime);
                }
                else if (myObjects4[13].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle14);
                    rough3.rjButton14.BackColor = Color.Orange;
                    CallValSet("62", dataModel.textBoxRegist62, myObjects4[13].Value.ToString(), dateTime);
                }
                else if (myObjects4[13].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle14);
                    rough3.rjButton14.BackColor = Color.DarkGreen;
                }
                else if (myObjects4[13].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle14);
                    rough3.rjButton14.BackColor = Color.Blue;
                    CallValSet("62", dataModel.textBoxRegist62, myObjects4[13].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };


            myObjects4[14].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[14].DidChange += () => {
                Console.WriteLine("changed!");
              
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime15.Text = time;

                string regId = "63";
                if (!string.IsNullOrEmpty(myElapseTime[62]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[62], regId);
                }

                myElapseTime[62] = "00:00";

                /*if (myObjects[62].Value == 261)
                {
                    myStopWatchObjects[62].Start();
                    rough3.myRjButton15.Text = "00:00";
                    myStopWatchObjects[62].Reset();
                }
                else
                {*/
                myStopWatchObjects[62].Stop();
                    rough3.myRjButton15.Text = "00:00";
                    myStopWatchObjects[62].Reset();
                    myStopWatchObjects[62].Start();
                //}

                if (myObjects4[14].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle15);
                    rough3.rjButton15.BackColor = Color.Red;
                    CallValSet("63", dataModel.textBoxRegist63, myObjects4[14].Value.ToString(), dateTime);
                }
                else if (myObjects4[14].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle15);
                    rough3.rjButton15.BackColor = Color.Orange;
                    CallValSet("63", dataModel.textBoxRegist63, myObjects4[14].Value.ToString(), dateTime);
                }
                else if (myObjects4[14].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle15);
                    rough3.rjButton15.BackColor = Color.DarkGreen;
                }
                else if (myObjects4[14].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle15);
                    rough3.rjButton15.BackColor = Color.Blue;
                    CallValSet("63", dataModel.textBoxRegist63, myObjects4[14].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            myObjects4[15].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects4[15].DidChange += () => {
                Console.WriteLine("changed!");
               
                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros
                string dateTime = DateTime.Now.ToString();
                rough3.rjButtonTime16.Text = time;

                string regId = "64";
                if (!string.IsNullOrEmpty(myElapseTime[63]))
                {
                    dbHandlr.update_call_data(m_dbConnection, myElapseTime[63], regId);
                }

                myElapseTime[63] = "00:00";

                /*if (myObjects[63].Value == 261)
                {
                    myStopWatchObjects[63].Start();
                    rough3.myRjButton16.Text = "00:00";
                    myStopWatchObjects[63].Reset();
                }
                else
                {*/
                myStopWatchObjects[63].Stop();
                    rough3.myRjButton16.Text = "00:00";
                    myStopWatchObjects[63].Reset();
                    myStopWatchObjects[63].Start();
                //}

                if (myObjects4[15].Value == 258)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle16);
                    rough3.rjButton16.BackColor = Color.Red;
                    CallValSet("64", dataModel.textBoxRegist64, myObjects4[15].Value.ToString(), dateTime);
                }
                else if (myObjects4[15].Value == 262)
                {
                    PlayingSound();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle16);
                    rough3.rjButton16.BackColor = Color.Orange;
                    CallValSet("64", dataModel.textBoxRegist64, myObjects4[15].Value.ToString(), dateTime);
                }
                else if (myObjects4[15].Value == 261)
                {
                    flowLayoutPanel1.Controls.Remove(rough3.roundPanelWithoutTitle16);
                    rough3.rjButton16.BackColor = Color.DarkGreen;
                }
                else if (myObjects4[15].Value == 264)
                {
                    PlayingSoundBlueCodeFemale();
                    flowLayoutPanel1.Controls.Add(rough3.roundPanelWithoutTitle16);
                    rough3.rjButton16.BackColor = Color.Blue;
                    CallValSet("64", dataModel.textBoxRegist64, myObjects4[15].Value.ToString(), dateTime);
                }
                AutoScrollMax();
            };

            
        }

        public void CallValSet(string registerId, string lastCallValue, string lastCallStatus, string dateTime) 
        {
           // CallValueSet("19", "Care From 19", myObjects[18].Value.ToString(), dateTime);
            dataModel.lastCallValue = lastCallValue;   // myObjects[18].Value.ToString();
            dataModel.lastCallStatus = lastCallStatus; // "Care From 19";//258
            dataModel.registerId = registerId;
            dataModel.dateTime = dateTime;
            dbHandlr.insert_call_data(m_dbConnection, dataModel);
        }

        private void rjButton3_Click(object sender, EventArgs e)
        {

        }

        private void rjButton17_Click(object sender, EventArgs e)
        {
            /*
             /02//16/2024
             An unhandled exception of type 'System.ArgumentException' occurred in mscorlib.dll
             An item with the same key has already been added.
             */
            Report MyReport = new Report(m_dbConnection);
            MyReport.Show();
        }

        private void rjSettings_Click(object sender, EventArgs e)
        {
            Settings mySetting = new Settings(dataModel, dbHandlr,m_dbConnection);
            mySetting.Show();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void roundPanelWithoutTitle2_Enter(object sender, EventArgs e)
        {

        }

        private void roundPanelWithoutTitle7_Enter(object sender, EventArgs e)
        {

        }

        private void roundPanelWithoutTitle3_Enter(object sender, EventArgs e)
        {

        }

        private void roundPanelWithoutTitle4_Enter(object sender, EventArgs e)
        {

        }

        private void roundPanelWithoutTitle5_Enter(object sender, EventArgs e)
        {

        }

        private void roundPanelWithoutTitle6_Enter(object sender, EventArgs e)
        {

        }

        private void roundPanelWithoutTitle8_Enter(object sender, EventArgs e)
        {

        }

        private void roundPanelWithoutTitle9_Enter(object sender, EventArgs e)
        {

        }

        private void roundPanelWithoutTitle10_Enter(object sender, EventArgs e)
        {

        }

        private void roundPanelWithoutTitle11_Enter(object sender, EventArgs e)
        {

        }

        private void roundPanelWithoutTitle12_Enter(object sender, EventArgs e)
        {

        }

        private void roundPanelWithoutTitle13_Enter(object sender, EventArgs e)
        {

        }

        private void roundPanelWithoutTitle14_Enter(object sender, EventArgs e)
        {

        }

        private void roundPanelWithoutTitle15_Enter(object sender, EventArgs e)
        {

        }

        private void roundPanelWithoutTitle16_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton1_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator2_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator1_Enter(object sender, EventArgs e)
        {

        }

        private void bButton1_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton2_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator3_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator4_Enter(object sender, EventArgs e)
        {

        }

        private void bButton2_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton7_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator13_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator14_Enter(object sender, EventArgs e)
        {

        }

        private void rjButton7_Click(object sender, EventArgs e)
        {

        }

        private void rjButtonTime7_Click(object sender, EventArgs e)
        {

        }

        private void bButton7_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton3_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator5_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator6_Enter(object sender, EventArgs e)
        {

        }

        private void rjButtonTime3_Click(object sender, EventArgs e)
        {

        }

        private void bButton3_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton4_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator7_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator8_Enter(object sender, EventArgs e)
        {

        }

        private void rjButton4_Click(object sender, EventArgs e)
        {

        }

        private void rjButtonTime4_Click(object sender, EventArgs e)
        {

        }

        private void bButton4_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton5_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator9_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator10_Enter(object sender, EventArgs e)
        {

        }

        private void rjButton5_Click_1(object sender, EventArgs e)
        {

        }

        private void rjButtonTime5_Click(object sender, EventArgs e)
        {

        }

        private void bButton5_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton6_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator11_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator12_Enter(object sender, EventArgs e)
        {

        }

        private void rjButton6_Click(object sender, EventArgs e)
        {

        }

        private void rjButtonTime6_Click(object sender, EventArgs e)
        {

        }

        private void bButton6_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton8_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator15_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator16_Enter(object sender, EventArgs e)
        {

        }

        private void rjButton8_Click(object sender, EventArgs e)
        {

        }

        private void rjButtonTime8_Click(object sender, EventArgs e)
        {

        }

        private void bButton8_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton9_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator23_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator24_Enter(object sender, EventArgs e)
        {

        }

        private void rjButton9_Click(object sender, EventArgs e)
        {

        }

        private void rjButtonTime9_Click(object sender, EventArgs e)
        {

        }

        private void bButton9_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton10_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator25_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator26_Enter(object sender, EventArgs e)
        {

        }

        private void rjButton10_Click(object sender, EventArgs e)
        {

        }

        private void rjButtonTime10_Click(object sender, EventArgs e)
        {

        }

        private void bButton10_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton11_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator27_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator28_Enter(object sender, EventArgs e)
        {

        }

        private void rjButton11_Click(object sender, EventArgs e)
        {

        }

        private void rjButtonTime11_Click(object sender, EventArgs e)
        {

        }

        private void bButton11_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton12_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator17_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator18_Enter(object sender, EventArgs e)
        {

        }

        private void rjButton12_Click(object sender, EventArgs e)
        {

        }

        private void rjButtonTime12_Click(object sender, EventArgs e)
        {

        }

        private void bButton12_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton13_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator21_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator22_Enter(object sender, EventArgs e)
        {

        }

        private void rjButton13_Click(object sender, EventArgs e)
        {

        }

        private void rjButtonTime13_Click(object sender, EventArgs e)
        {

        }

        private void bButton13_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton14_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator31_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator32_Enter(object sender, EventArgs e)
        {

        }

        private void rjButton14_Click(object sender, EventArgs e)
        {

        }

        private void rjButtonTime14_Click(object sender, EventArgs e)
        {

        }

        private void bButton14_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton15_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator29_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator30_Enter(object sender, EventArgs e)
        {

        }

        private void rjButton15_Click(object sender, EventArgs e)
        {

        }

        private void rjButtonTime15_Click(object sender, EventArgs e)
        {

        }

        private void bButton15_Click(object sender, EventArgs e)
        {

        }

        private void myRjButton16_Click(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator19_Enter(object sender, EventArgs e)
        {

        }

        private void horizontalLineSeparator20_Enter(object sender, EventArgs e)
        {

        }

        private void rjButton16_Click(object sender, EventArgs e)
        {

        }

        private void rjButtonTime16_Click(object sender, EventArgs e)
        {

        }

        private void bButton16_Click(object sender, EventArgs e)
        {

        }

        private void rjBtnCross_Click(object sender, EventArgs e)
        {
           
           

            if (MessageBox.Show("Are You Sure to Exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                AutoSaveOnClose();
                // Writedata();
              
                Application.Exit();
            }
        }

        private void rjBtnSpeaker_Click(object sender, EventArgs e)
        {

            if (isSpeakerOn)
            {
                
                player.PlayLooping();
                isSpeakerOn = false;
                rjBtnSpeaker.BackgroundImage = Properties.Resources.speaker_off_white_912;
            }
            else {
                player.Stop();
                isSpeakerOn = true;
                rjBtnSpeaker.BackgroundImage = Properties.Resources.speaker_on_white_912;
            }
            
            // player.Play();
        }

        public void PlayingSound() {
            playerFemale.Stop();
            playerMale.Stop();
            player.SoundLocation = SoundPath("sound.wav");
            player.PlayLooping();
            isSpeakerOn = false;
            rjBtnSpeaker.BackgroundImage = Properties.Resources.speaker_off_white_912;

        }

        public void PlayingSoundBlueCodeMale()
        {
            player.Stop();
            playerFemale.Stop();
            playerMale.SoundLocation = SoundPath("blue_code_male.wav");
            playerMale.PlayLooping();
            isSpeakerOn = false;
            rjBtnSpeaker.BackgroundImage = Properties.Resources.speaker_off_white_912;

        }

        public void PlayingSoundBlueCodeFemale()
        {
            player.Stop();
            playerMale.Stop();
            playerFemale.SoundLocation = SoundPath("blue_code_female.wav");
            playerFemale.PlayLooping();
            isSpeakerOn = false;
            rjBtnSpeaker.BackgroundImage = Properties.Resources.speaker_off_white_912;

        }

        public void StopSound()
        {

            player.Stop();
            isSpeakerOn = true;
            rjBtnSpeaker.BackgroundImage = Properties.Resources.speaker_on_white_912;

        }

        public string SoundPath(String path)
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string truePath = (System.IO.Path.GetDirectoryName(executable));
            //  AppDomain.CurrentDomain.SetData("DataDirectory", path);
            // string[] filePaths = Directory.GetFiles(@path, "*.mdb",
            // SearchOption.TopDirectoryOnly);
            // MessageBox.Show(filePaths[0]);
            // E:\ShreekrishnaProject\PayrollManagement\PayrollManagement
            //  return filePaths[0];
            // Console.WriteLine("file path"+filePaths[0]);
            return truePath + "\\sound" + "\\" + path;

        }

        public void StopWatchTimer()
        {
            if (myStopWatchObjects[0].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[0].ElapsedMilliseconds);
                rough.myRjButton1.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[0] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton1.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);
                // Console.WriteLine("Running/Stop: " + stopWatchObj().IsRunning);
                //  Console.WriteLine("Running/Stop2: " + stopWatchObj2().IsRunning);

            }
            if (myStopWatchObjects[1].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[1].ElapsedMilliseconds);
                rough.myRjButton2.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[1] = objTimeSpan.ToString("mm':'ss");
                //  Console.WriteLine("Running/Stop: " + stopWatchObj2().IsRunning);
                // form1.myRjButton2.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[2].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[2].ElapsedMilliseconds);
                rough.myRjButton3.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[2] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton3.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[3].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[3].ElapsedMilliseconds);
                rough.myRjButton4.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[3] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton4.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[4].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[4].ElapsedMilliseconds);
                rough.myRjButton5.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[4] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton5.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[5].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[5].ElapsedMilliseconds);
                rough.myRjButton6.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[5] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton6.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[6].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[6].ElapsedMilliseconds);
                rough.myRjButton7.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[6] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton7.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[7].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[7].ElapsedMilliseconds);
                rough.myRjButton8.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[7] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton8.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[8].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[8].ElapsedMilliseconds);
                rough.myRjButton9.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[8] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton9.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[9].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[9].ElapsedMilliseconds);
                rough.myRjButton10.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[9] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton10.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[10].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[10].ElapsedMilliseconds);
                rough.myRjButton11.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[10] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton11.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[11].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[11].ElapsedMilliseconds);
                rough.myRjButton12.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[11] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton12.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[12].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[12].ElapsedMilliseconds);
                rough.myRjButton13.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[12] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton13.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[13].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[13].ElapsedMilliseconds);
                rough.myRjButton14.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[13] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton14.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[14].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[14].ElapsedMilliseconds);
                rough.myRjButton15.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[14] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton15.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[15].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[15].ElapsedMilliseconds);
                rough.myRjButton16.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[15] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton16.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (myStopWatchObjects[16].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[16].ElapsedMilliseconds);
                rough1.myRjButton1.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[16] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton1.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);
                // Console.WriteLine("Running/Stop: " + stopWatchObj().IsRunning);
                //  Console.WriteLine("Running/Stop2: " + stopWatchObj2().IsRunning);

            }
            if (myStopWatchObjects[17].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[17].ElapsedMilliseconds);
                rough1.myRjButton2.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[17] = objTimeSpan.ToString("mm':'ss");
                //  Console.WriteLine("Running/Stop: " + stopWatchObj2().IsRunning);
                //  form1.myRjButton2.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[18].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[18].ElapsedMilliseconds);
                rough1.myRjButton3.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[18] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton3.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[19].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[19].ElapsedMilliseconds);
                rough1.myRjButton4.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[19] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton4.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[20].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[20].ElapsedMilliseconds);
                rough1.myRjButton5.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[20] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton5.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[21].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[21].ElapsedMilliseconds);
                rough1.myRjButton6.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[21] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton6.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[22].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[22].ElapsedMilliseconds);
                rough1.myRjButton7.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[22] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton7.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[23].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[23].ElapsedMilliseconds);
                rough1.myRjButton8.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[23] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton8.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[24].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[24].ElapsedMilliseconds);
                rough1.myRjButton9.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[24] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton9.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[25].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[25].ElapsedMilliseconds);
                rough1.myRjButton10.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[25] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton10.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[26].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[26].ElapsedMilliseconds);
                rough1.myRjButton11.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[26] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton11.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[27].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[27].ElapsedMilliseconds);
                rough1.myRjButton12.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[27] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton12.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[28].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[28].ElapsedMilliseconds);
                rough1.myRjButton13.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[28] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton13.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[29].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[29].ElapsedMilliseconds);
                rough1.myRjButton14.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[29] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton14.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[30].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[30].ElapsedMilliseconds);
                rough1.myRjButton15.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[30] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton15.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[31].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[31].ElapsedMilliseconds);
                rough1.myRjButton16.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[31] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton16.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }

            // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (myStopWatchObjects[32].IsRunning)
            {
                
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[32].ElapsedMilliseconds);
                // Console.WriteLine("IsRunning "+ objTimeSpan.ToString("mm':'ss"));
                rough2.myRjButton1.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[32] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton1.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);
                // Console.WriteLine("Running/Stop: " + stopWatchObj().IsRunning);
                // Console.WriteLine("Running/Stop2: " + stopWatchObj2().IsRunning);

            }
            if (myStopWatchObjects[33].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[33].ElapsedMilliseconds);
                rough2.myRjButton2.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[33] = objTimeSpan.ToString("mm':'ss");
                // Console.WriteLine("Running/Stop: " + stopWatchObj2().IsRunning);
                // form1.myRjButton2.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[34].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[34].ElapsedMilliseconds);
                rough2.myRjButton3.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[34] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton3.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[35].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[35].ElapsedMilliseconds);
                rough2.myRjButton4.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[35] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton4.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[36].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[36].ElapsedMilliseconds);
                rough2.myRjButton5.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[36] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton5.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[37].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[37].ElapsedMilliseconds);
                rough2.myRjButton6.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[37] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton6.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[38].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[38].ElapsedMilliseconds);
                rough2.myRjButton7.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[38] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton7.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[39].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[39].ElapsedMilliseconds);
                rough2.myRjButton8.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[39] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton8.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[40].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[40].ElapsedMilliseconds);
                rough2.myRjButton9.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[40] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton9.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[41].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[41].ElapsedMilliseconds);
                rough2.myRjButton10.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[41] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton10.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[42].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[42].ElapsedMilliseconds);
                rough2.myRjButton11.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[42] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton11.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[43].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[43].ElapsedMilliseconds);
                rough2.myRjButton12.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[43] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton12.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[44].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[44].ElapsedMilliseconds);
                rough2.myRjButton13.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[44] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton13.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[45].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[45].ElapsedMilliseconds);
                rough2.myRjButton14.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[45] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton14.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[46].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[46].ElapsedMilliseconds);
                rough2.myRjButton15.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[46] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton15.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[47].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[47].ElapsedMilliseconds);
                rough2.myRjButton16.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[47] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton16.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }

            //++++++++++++++++++++++++++++++++++++++++++++++++

            if (myStopWatchObjects[48].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[48].ElapsedMilliseconds);
                rough3.myRjButton1.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[48] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton1.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);
                // Console.WriteLine("Running/Stop: " + stopWatchObj().IsRunning);
                //  Console.WriteLine("Running/Stop2: " + stopWatchObj2().IsRunning);

            }
            if (myStopWatchObjects[49].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[49].ElapsedMilliseconds);
                rough3.myRjButton2.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[49] = objTimeSpan.ToString("mm':'ss");
                //  Console.WriteLine("Running/Stop: " + stopWatchObj2().IsRunning);
                // form1.myRjButton2.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[50].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[50].ElapsedMilliseconds);
                rough3.myRjButton3.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[50] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton3.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[51].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[51].ElapsedMilliseconds);
                rough3.myRjButton4.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[51] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton4.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[52].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[52].ElapsedMilliseconds);
                rough3.myRjButton5.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[52] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton5.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[53].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[53].ElapsedMilliseconds);
                rough3.myRjButton6.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[53] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton6.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[54].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[54].ElapsedMilliseconds);
                rough3.myRjButton7.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[54] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton7.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[55].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[55].ElapsedMilliseconds);
                rough3.myRjButton8.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[55] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton8.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[56].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[56].ElapsedMilliseconds);
                rough3.myRjButton9.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[56] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton9.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[57].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[57].ElapsedMilliseconds);
                rough3.myRjButton10.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[57] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton10.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[58].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[58].ElapsedMilliseconds);
                rough3.myRjButton11.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[58] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton11.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[59].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[59].ElapsedMilliseconds);
                rough3.myRjButton12.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[59] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton12.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[60].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[60].ElapsedMilliseconds);
                rough3.myRjButton13.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[60] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton13.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[61].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[61].ElapsedMilliseconds);
                rough3.myRjButton14.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[61] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton14.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[62].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[62].ElapsedMilliseconds);
                rough3.myRjButton15.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[62] = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton15.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[63].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[63].ElapsedMilliseconds);
                rough3.myRjButton16.Text = objTimeSpan.ToString("mm':'ss");
                myElapseTime[63] = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton16.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }

        }


       

       

    }
}
