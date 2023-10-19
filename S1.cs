using EasyModbus;
using Modbus.Device;
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
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace NurseCalling
{
    
    public partial class S1 : Form
    {
        SQLiteConnection m_dbConnection;
        //S21 s2 = new S21();
        S2 s2 = new S2();
        S3 s3 = new S3();
        S4 s4 = new S4();

        dbHandler dbHandlr;
        DataModel dataModel;

        public SerialPort ComPort1, ComPort2;
        public String SerialPortName;
        private BackgroundWorker worker;
        StopWatchCshartp stopWatchCshartp1;
        StopWatchCshartp stopWatchCshartp2;
        StopWatchCshartp stopWatchCshartp3;
        StopWatchCshartp stopWatchCshartp4;
        StopWatchCshartp stopWatchCshartp5;
        StopWatchCshartp stopWatchCshartp6;
        StopWatchCshartp stopWatchCshartp7;
        StopWatchCshartp stopWatchCshartp8;
        StopWatchCshartp stopWatchCshartp9;
        StopWatchCshartp stopWatchCshartp10;
        StopWatchCshartp stopWatchCshartp11;
        StopWatchCshartp stopWatchCshartp12;
        StopWatchCshartp stopWatchCshartp13;
        StopWatchCshartp stopWatchCshartp14;
        StopWatchCshartp stopWatchCshartp15;
        StopWatchCshartp stopWatchCshartp16;

        SystemClockTimer systemClockTimer1;
        IModbusSerialMaster master;
        ModbusClient modbusClient;
        ushort[] registers;

        public S1()
        {
         
            InitializeComponent();

            modbusClient = new ModbusClient("COM1");

            dataModel = new DataModel();
          
            this.Controls.Add(s2.panel1);
            this.Controls.Add(s3.panel1);
            this.Controls.Add(s4.panel1);

            this.panel1.Show();
            systemClockTimer1 =  new SystemClockTimer(this);

            //  blinkLabel();

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
            System.Timers.Timer timer1 = new System.Timers.Timer(1000);
            timer1.Elapsed += timer_Elapsed;
            timer1.Start(); 
       


            try
            {
                stopWatchCshartp1 = new StopWatchCshartp(this);
            
            }
            catch(Exception ex) { }

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=dscp.sqlite;Version=3;");
            }
            catch (Exception ex)
            {

            }

            dbHandlr = new dbHandler();
            dbHandlr.createDB(dataModel);
        }


        void timer_Elapsed1(object sender, System.Timers.ElapsedEventArgs e)
        {
            BytesToRead();
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
            catch (Exception ex) { }
        }


        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

            this.Invoke((MethodInvoker)delegate { 
              
                string hex_add = "0x0001";// "0x00A4";
                ushort dec_add = Convert.ToUInt16(hex_add, 16);
                ushort startAddress = dec_add;
                ushort numRegisters = 16;

                for (int i = 0; i < numRegisters; i++)
                {

                    Console.WriteLine("Register {0}={1}", startAddress + i, registers[i]);
                    Console.WriteLine("Welcome New User " + Properties.Settings.Default.FirstCallTime);
                    if ((startAddress + i) == 1)
                    {
                        if (Properties.Settings.Default.FirstCallTime == 1)
                        {
                            rjButton3.Text = "Welcome New User";
                            
                            // Change the value since the program has run once now
                            Properties.Settings.Default.FirstCallTime = 0;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            rjButton3.Text = "Welcome Back User"; 
                        }
                        rjButton1.Text = registers[i].ToString();
                    }
                }

            });
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

                 

                        byte slaveId = 1;

                        string hex_add ="0x0001";// "0x00A4";
                        ushort dec_add = Convert.ToUInt16(hex_add, 16);
                        ushort startAddress = dec_add;
                        ushort numRegisters = 16;
                 
                        
                         // read five registers
                             registers = master.ReadHoldingRegisters(slaveId, 0, numRegisters);

                      
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
                    connect1();
                }
            }
            else { connect1(); }
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

            timer1.Interval = 3000;//every one second

            timer1.Tick += new System.EventHandler((s, e) =>
            {
                if (blink_times == 25)
                {
                    // label1.Visible = !label1.Visible;
                    Debug.WriteLine("my_visible 1");
                    this.panel1.Show(); 
                    s2.panel1.Hide();
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
                stopWatchCshartp1.StopWatchTimer();
            }
            catch (Exception ex)
            {
            }
        }

        private void rjButton5_Click(object sender, EventArgs e)
        {
            
            stopWatchCshartp1.btnStart_Click();
        }

        private void rjButton50_Click(object sender, EventArgs e)
        {
            stopWatchCshartp1.btnStart2_Click();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            try { 
            int[] data = { 1, 2 };
            modbusClient.Connect();
            modbusClient.WriteMultipleRegisters(1, data);
            }catch(Exception ex) {
                Console.WriteLine("Console1: "+ex.Message);
            }
            modbusClient.Disconnect();
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
                        Console.WriteLine("SERIAL PORT:" + Properties.Settings.Default["portName"].ToString() + " dsd " + SerialPortName);

                        ComPort1.Open();

                        master = ModbusSerialMaster.CreateRtu(ComPort1);

                        Console.WriteLine("SERIAL PORT:" + Properties.Settings.Default["portName"].ToString() + " dsd " + SerialPortName+" is "+ ComPort1.IsOpen);
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
