using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NurseCalling
{
    public partial class S1 : Form
    {
        //S21 s2 = new S21();
        S2 s2 = new S2();
        S3 s3 = new S3();
        S4 s4 = new S4();

        public SerialPort ComPort1, ComPort2;
        public String SerialPortName;
        private BackgroundWorker worker;

        public S1()
        {
         
            InitializeComponent();

            this.Controls.Add(s2.panel1);
            this.Controls.Add(s3.panel1);
            this.Controls.Add(s4.panel1);

            blinkLabel();


            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += timer_Elapsed1;
            timer.Start();

            worker = new BackgroundWorker();
            //  worker.DoWork += worker_DoWork;
            //     worker.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;  //Tell the user how the process went
            // worker.ProgressChanged += backgroundWorker1_ProgressChanged;
            worker.RunWorkerAsync();
            worker.WorkerReportsProgress = true;
            System.Timers.Timer timer1 = new System.Timers.Timer(500);
           // timer1.Elapsed += timer_Elapsed;
            timer1.Start(); 
            connect1(); 
        }


        void timer_Elapsed1(object sender, System.Timers.ElapsedEventArgs e)
        {
            BytesToRead();
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

                        //  MessageBox.Show("098908");
                        //   if (ComPort1.BytesToRead > 18)
                        //   {

                       // ComPort1.Read(Rxbuf, 0, 18);
                        ComPort1.DiscardInBuffer();
                        // Debug.WriteLine(Rxbuf[0] + " " + Rxbuf[1] + " " + Rxbuf[2] + " " + Rxbuf[3] + " " + Rxbuf[4] + " " + Rxbuf[5] + " " + Rxbuf[6] + " " + Rxbuf[7] + " " + Rxbuf[8] + " " + Rxbuf[9] + " " + Rxbuf[10] + " " + Rxbuf[11] + " " + Rxbuf[12] + " " + Rxbuf[13] + " " + Rxbuf[14] + " " + Rxbuf[15] + " " + Rxbuf[16] + " " + Rxbuf[17] + " " + Rxbuf[18]);
                        int milliseconds = 2000;
                        //  Thread.Sleep(milliseconds);
                        //  processserialport1();




                        //  }
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
                    connect1();
                }
            }
            else { connect1(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
       
            this.Hide();
        }

        void blinkLabel()
        {
            int blink_times = 25;
        
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

            timer1.Interval = 3000;//every one second

            timer1.Tick += new System.EventHandler((s, e) =>
            {
                if (blink_times == 25)
                {
                    // label1.Visible = !label1.Visible;
                    Debug.WriteLine("my_visible 1");
                    this.panel1.Show(); 
                    s2.panel1.Hide() ;
                    s3.panel1.Hide();
                    s4.panel1.Hide();
                    blink_times--;
                }
                else if (blink_times == 24)
                {
                   
                    Debug.WriteLine("my_visible 2"); 
                    s2.panel1.Show(); 
                    this.panel1.Hide(); 
                    s3.panel1.Hide();
                    s4.panel1.Hide();
                    // timer1.Stop();
                    blink_times--;
                } 
                else if (blink_times == 23) 
                {
                    s3.panel1.Show();
                    s2.panel1.Hide(); 
                    this.panel1.Hide(); 
                    s4.panel1.Hide();
                    blink_times--;
                }
                else if (blink_times == 22) 
                {
                    s4.panel1.Show();
                    s3.panel1.Hide();
                    s2.panel1.Hide(); 
                    this.panel1.Hide(); 
                    blink_times = 25;
                } 
            }

            );


            timer1.Start();
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

        public void connect1()
        {


            ComPort1 = new SerialPort();

            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                // Debug.WriteLine("port list: " + port); 
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
            {
                try
                {
                    ComPort1.PortName = Properties.Settings.Default["portName"].ToString();// Properties.Settings.Default.portName.ToString();
                }
                catch (Exception ex) { }
            }


            Console.WriteLine("SERIAL PORT:" + Properties.Settings.Default["portName"].ToString());

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
                        ComPort1.Open();
                            /*this.Invoke((MethodInvoker)delegate
                        {
                            //  Globals.logWriter.LogWrite("Open Comport 1: " + ComPort1.IsOpen);
                            connectionStatus.Text = "Connected";
                            connectionStatus.ForeColor = Color.White;
                            //connectionStatus.BackColor = Color.White;
                            connectionPanel.BackColor = Color.Green;
                        });*/
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

    }
}
