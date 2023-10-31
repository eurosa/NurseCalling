using EasyModbus;
using Modbus.Device;
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
        ModbusClient modbusClient;
        ushort[] registers;
        //  Wrapped<int> iVal;

        Wrapped<int>[] myObjects;
        // StopWatchCshartp[] myStopWatchObjects;

        Stopwatch[] myStopWatchObjects;

        public S1()
        {
         
            InitializeComponent();

            modbusClient = new ModbusClient("COM1");

            dataModel = new DataModel();

            int objectsToCreate = 64;

            // Create an array to hold all your objects
            myObjects = new Wrapped<int>[objectsToCreate];

           // myStopWatchObjects = new StopWatchCshartp[objectsToCreate];

            for (int i = 0; i < objectsToCreate; i++)
            {
                // Instantiate a new object, set it's number and
                // some other properties
                myObjects[i] = new Wrapped<int>();


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


            checkStatus();




            this.Controls.Add(s2.panel1);
            this.Controls.Add(s3.panel1);
            this.Controls.Add(s4.panel1);

            this.panel1.Show();



            //  systemClockTimer1 =  new SystemClockTimer(this);

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


            dbHandlr = new dbHandler();
            dbHandlr.createDB(dataModel);
            try
            {
             //   stopWatchCshartp1 = new StopWatchCshartp(this);
            
            }
            catch(Exception ex) { }

            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=dscp.sqlite;Version=3;");
                m_dbConnection.Open();
            }
            catch (Exception ex)
            {

            }

            

            dbHandlr.getGeneralData(m_dbConnection,dataModel);




            myStopWatchObjects = new Stopwatch[objectsToCreate];

            for (int i = 0; i < objectsToCreate; i++)
            {
                // Instantiate a new object, set it's number and
                // some other properties
                myStopWatchObjects[i] = new Stopwatch();


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
            catch (Exception Ex) { }
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

                    // Console.WriteLine("Welcome New User " + Properties.Settings.Default.FirstCallTime);

                   /* if ((startAddress + i) == 1)
                    {
                        //Age = (int)registers[i];

                        myObjects[0].Value = (int)registers[i];
                        rjButton1.Text = registers[i].ToString();
                         

                    }
                    if ((startAddress + i) == 2)
                    {
                        // Age = (int)registers[i]; 
                        myObjects[1].Value = (int)registers[i];
                        rjButton2.Text = registers[i].ToString(); 

                    }*/


                    switch ((startAddress + i))
                    {
                        case 1:
                            myObjects[i].Value = (int)registers[i];
                            rjButton1.Text = registers[i].ToString();
                            break;
                        case 2:
                            myObjects[i].Value = (int)registers[i];
                            rjButton2.Text = registers[i].ToString();
                            break;
                        case 3:
                            myObjects[i].Value = (int)registers[i];
                            rjButton3.Text = registers[i].ToString();
                            break;
                        case 4:
                            myObjects[i].Value = (int)registers[i];
                            rjButton4.Text = registers[i].ToString();
                            break;
                        case 5:
                            myObjects[i].Value = (int)registers[i];
                            rjButton5.Text = registers[i].ToString();
                            break;
                        case 6:
                            myObjects[i].Value = (int)registers[i];
                            rjButton6.Text = registers[i].ToString();
                            break;
                        case 7:
                            myObjects[i].Value = (int)registers[i];
                            rjButton7.Text = registers[i].ToString();
                            break;
                        case 8:
                            myObjects[i].Value = (int)registers[i];
                            rjButton8.Text = registers[i].ToString();
                            break;
                        case 9:
                            myObjects[i].Value = (int)registers[i];
                            rjButton9.Text = registers[i].ToString();
                            break;
                        case 10:
                            myObjects[i].Value = (int)registers[i];
                            rjButton10.Text = registers[i].ToString();
                            break;
                        case 11:
                            myObjects[i].Value = (int)registers[i];
                            rjButton11.Text = registers[i].ToString();
                            break;
                        case 12:
                            myObjects[i].Value = (int)registers[i];
                            rjButton12.Text = registers[i].ToString();
                            break;
                        case 13:
                            myObjects[i].Value = (int)registers[i];
                            rjButton13.Text = registers[i].ToString();
                            break;
                        case 14:
                            myObjects[i].Value = (int)registers[i];
                            rjButton14.Text = registers[i].ToString();
                            break;
                        case 15:
                            myObjects[i].Value = (int)registers[i];
                            rjButton15.Text = registers[i].ToString();
                            break;
                        case 16:
                            myObjects[i].Value = (int)registers[i];
                            rjButton16.Text = registers[i].ToString();
                            break;
                    }



                }

                sec++;
                if (sec >= 1)
                {
                    sec = 0;
                    if (toggle == true) toggle = false;          // FOR ALARM BLINKING AFTER EVERY 500 MSEC
                    else toggle = true;                          // FOR ALARM BLINKING AFTER EVERY 500 MSEC
                    checkdigitalinputs();                      // GAS ALARM FUNCTION AFTER EVERY 500 MSEC

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

        void checkdigitalinputs()
        {
            if (myObjects[0].Value != 261)
            {
                if (toggle == true) rjButton1.Visible = true;
                else rjButton1.Visible = false;
            } 
            
            if (myObjects[1].Value != 261) {

                if (toggle == true) rjButton2.Visible = true;
                else rjButton2.Visible = false;
            }

            if (myObjects[2].Value != 261)
            {

                if (toggle == true) rjButton3.Visible = true;
                else rjButton3.Visible = false;
            }
            if (myObjects[3].Value != 261)
            {

                if (toggle == true) rjButton4.Visible = true;
                else rjButton4.Visible = false;
            }
            if (myObjects[4].Value != 261)
            {

                if (toggle == true) rjButton5.Visible = true;
                else rjButton5.Visible = false;
            }
            if (myObjects[5].Value != 261)
            {

                if (toggle == true) rjButton6.Visible = true;
                else rjButton6.Visible = false;
            }
            if (myObjects[6].Value != 261)
            {

                if (toggle == true) rjButton7.Visible = true;
                else rjButton7.Visible = false;
            }
            if (myObjects[7].Value != 261)
            {

                if (toggle == true) rjButton8.Visible = true;
                else rjButton8.Visible = false;
            }
            if (myObjects[8].Value != 261)
            {

                if (toggle == true) rjButton9.Visible = true;
                else rjButton9.Visible = false;
            }
            if (myObjects[9].Value != 261)
            {

                if (toggle == true) rjButton10.Visible = true;
                else rjButton10.Visible = false;
            }
            if (myObjects[10].Value != 261)
            {

                if (toggle == true) rjButton11.Visible = true;
                else rjButton11.Visible = false;
            }
            if (myObjects[11].Value != 261)
            {

                if (toggle == true) rjButton12.Visible = true;
                else rjButton12.Visible = false;
            }
            if (myObjects[12].Value != 261)
            {

                if (toggle == true) rjButton13.Visible = true;
                else rjButton13.Visible = false;
            }
            if (myObjects[13].Value != 261)
            {

                if (toggle == true) rjButton14.Visible = true;
                else rjButton14.Visible = false;
            }
            if (myObjects[14].Value != 261)
            {

                if (toggle == true) rjButton15.Visible = true;
                else rjButton15.Visible = false;
            }
            if (myObjects[15].Value != 261)
            {

                if (toggle == true) rjButton16.Visible = true;
                else rjButton16.Visible = false;
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

        public void checkStatus() 
        {

            myObjects[0].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[0].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime1.Text = time;

                if (myObjects[0].Value == 261)
                {

                    myStopWatchObjects[0].Stop();
                    myStopWatchObjects[0].Reset();
                    myRjButton1.Text = "00:00";
                }
                else
                {
                    myStopWatchObjects[0].Stop();
                    myStopWatchObjects[0].Reset();
                    myRjButton1.Text = "00:00";
                    myStopWatchObjects[0].Start();
                }
                if (myObjects[0].Value == 258)
                {
                    rjButton1.BackColor = Color.Red;
                }
                else if (myObjects[0].Value == 262)
                {
                    rjButton1.BackColor = Color.Orange;
                }
                else if (myObjects[0].Value == 261)
                {
                    rjButton1.BackColor = Color.DarkGreen;
                }
                else if (myObjects[0].Value == 264)
                {
                    rjButton1.BackColor = Color.Blue;
                }

            };

            myObjects[1].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[1].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime2.Text = time;

                if (myObjects[1].Value == 261)
                {

                    myStopWatchObjects[1].Start();
                    myRjButton2.Text = "00:00";
                    myStopWatchObjects[1].Reset();
                }
                else
                {
                    myStopWatchObjects[1].Stop();
                    myRjButton2.Text = "00:00";
                    myStopWatchObjects[1].Reset();
                    myStopWatchObjects[1].Start();
                }

                if (myObjects[1].Value == 258)
                {
                    rjButton2.BackColor = Color.Red;
                }
                else if (myObjects[1].Value == 262)
                {
                    rjButton2.BackColor = Color.Orange;
                }
                else if (myObjects[1].Value == 261)
                {
                    rjButton2.BackColor = Color.DarkGreen;
                }
                else if (myObjects[1].Value == 264)
                {
                    rjButton2.BackColor = Color.Blue;
                }

            };


            myObjects[2].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[2].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime3.Text = time;

                if (myObjects[2].Value == 261)
                {

                    myStopWatchObjects[2].Start();
                    myRjButton3.Text = "00:00";
                    myStopWatchObjects[2].Reset();
                }
                else
                {
                    myStopWatchObjects[2].Stop();
                    myRjButton3.Text = "00:00";
                    myStopWatchObjects[2].Reset();
                    myStopWatchObjects[2].Start();
                }

                if (myObjects[2].Value == 258)
                {
                    rjButton3.BackColor = Color.Red;
                }
                else if (myObjects[2].Value == 262)
                {
                    rjButton3.BackColor = Color.Orange;
                }
                else if (myObjects[2].Value == 261)
                {
                    rjButton3.BackColor = Color.DarkGreen;
                }
                else if (myObjects[2].Value == 264)
                {
                    rjButton3.BackColor = Color.Blue;
                }

            };

            myObjects[3].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[3].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime4.Text = time;

                if (myObjects[3].Value == 261)
                {

                    myStopWatchObjects[3].Start();
                    myRjButton4.Text = "00:00";
                    myStopWatchObjects[3].Reset();
                }
                else
                {
                    myStopWatchObjects[3].Stop();
                    myRjButton4.Text = "00:00";
                    myStopWatchObjects[3].Reset();
                    myStopWatchObjects[3].Start();
                }

                if (myObjects[3].Value == 258)
                {
                    rjButton4.BackColor = Color.Red;
                }
                else if (myObjects[3].Value == 262)
                {
                    rjButton4.BackColor = Color.Orange;
                }
                else if (myObjects[3].Value == 261)
                {
                    rjButton4.BackColor = Color.DarkGreen;
                }
                else if (myObjects[3].Value == 264)
                {
                    rjButton4.BackColor = Color.Blue;
                }

            };


            myObjects[4].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[4].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime5.Text = time;

                if (myObjects[4].Value == 261)
                {

                    myStopWatchObjects[4].Start();
                    myRjButton5.Text = "00:00";
                    myStopWatchObjects[4].Reset();
                }
                else
                {
                    myStopWatchObjects[4].Stop();
                    myRjButton5.Text = "00:00";
                    myStopWatchObjects[4].Reset();
                    myStopWatchObjects[4].Start();
                }

                if (myObjects[4].Value == 258)
                {
                    rjButton5.BackColor = Color.Red;
                }
                else if (myObjects[4].Value == 262)
                {
                    rjButton5.BackColor = Color.Orange;
                }
                else if (myObjects[4].Value == 261)
                {
                    rjButton5.BackColor = Color.DarkGreen;
                }
                else if (myObjects[4].Value == 264)
                {
                    rjButton5.BackColor = Color.Blue;
                }

            };

            myObjects[5].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[5].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime6.Text = time;

                if (myObjects[5].Value == 261)
                {

                    myStopWatchObjects[5].Start();
                    myRjButton6.Text = "00:00";
                    myStopWatchObjects[5].Reset();
                }
                else
                {
                    myStopWatchObjects[5].Stop();
                    myRjButton6.Text = "00:00";
                    myStopWatchObjects[5].Start();
                }

                if (myObjects[5].Value == 258)
                {
                    rjButton6.BackColor = Color.Red;
                }
                else if (myObjects[5].Value == 262)
                {
                    rjButton6.BackColor = Color.Orange;
                }
                else if (myObjects[5].Value == 261)
                {
                    rjButton6.BackColor = Color.DarkGreen;
                }
                else if (myObjects[5].Value == 264)
                {
                    rjButton6.BackColor = Color.Blue;
                }

            };

            myObjects[6].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[6].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime7.Text = time;

                if (myObjects[6].Value == 261)
                {

                    myStopWatchObjects[6].Start();
                    myRjButton7.Text = "00:00";
                    myStopWatchObjects[6].Reset();
                }
                else
                {
                    myStopWatchObjects[6].Stop();
                    myRjButton7.Text = "00:00";
                    myStopWatchObjects[6].Start();
                }

                if (myObjects[6].Value == 258)
                {
                    rjButton7.BackColor = Color.Red;
                }
                else if (myObjects[6].Value == 262)
                {
                    rjButton7.BackColor = Color.Orange;
                }
                else if (myObjects[6].Value == 261)
                {
                    rjButton7.BackColor = Color.DarkGreen;
                }
                else if (myObjects[6].Value == 264)
                {
                    rjButton7.BackColor = Color.Blue;
                }

            };


            myObjects[7].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[7].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime8.Text = time;

                if (myObjects[7].Value == 261)
                {

                    myStopWatchObjects[7].Start();
                    myRjButton8.Text = "00:00";
                    myStopWatchObjects[7].Reset();
                }
                else
                {
                    myStopWatchObjects[7].Stop();
                    myRjButton8.Text = "00:00";
                    myStopWatchObjects[7].Start();
                }

                if (myObjects[7].Value == 258)
                {
                    rjButton8.BackColor = Color.Red;
                }
                else if (myObjects[7].Value == 262)
                {
                    rjButton8.BackColor = Color.Orange;
                }
                else if (myObjects[7].Value == 261)
                {
                    rjButton8.BackColor = Color.DarkGreen;
                }
                else if (myObjects[7].Value == 264)
                {
                    rjButton8.BackColor = Color.Blue;
                }

            };

            myObjects[8].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[8].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime9.Text = time;

                if (myObjects[8].Value == 261)
                {

                    myStopWatchObjects[8].Start();
                    myRjButton9.Text = "00:00";
                    myStopWatchObjects[8].Reset();
                }
                else
                {
                    myStopWatchObjects[8].Stop();
                    myRjButton9.Text = "00:00";
                    myStopWatchObjects[8].Start();
                }

                if (myObjects[8].Value == 258)
                {
                    rjButton9.BackColor = Color.Red;
                }
                else if (myObjects[8].Value == 262)
                {
                    rjButton9.BackColor = Color.Orange;
                }
                else if (myObjects[8].Value == 261)
                {
                    rjButton9.BackColor = Color.DarkGreen;
                }
                else if (myObjects[8].Value == 264)
                {
                    rjButton9.BackColor = Color.Blue;
                }

            };

            myObjects[9].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[9].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime10.Text = time;

                if (myObjects[9].Value == 261)
                {

                    myStopWatchObjects[9].Start();
                    myRjButton10.Text = "00:00";
                    myStopWatchObjects[9].Reset();
                }
                else
                {
                    myStopWatchObjects[9].Stop();
                    myRjButton10.Text = "00:00";
                    myStopWatchObjects[9].Start();
                }

                if (myObjects[9].Value == 258)
                {
                    rjButton10.BackColor = Color.Red;
                }
                else if (myObjects[9].Value == 262)
                {
                    rjButton10.BackColor = Color.Orange;
                }
                else if (myObjects[9].Value == 261)
                {
                    rjButton10.BackColor = Color.DarkGreen;
                }
                else if (myObjects[9].Value == 264)
                {
                    rjButton10.BackColor = Color.Blue;
                }

            };

            myObjects[10].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[10].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime11.Text = time;

                if (myObjects[10].Value == 261)
                {

                    myStopWatchObjects[10].Start();
                    myRjButton11.Text = "00:00";
                    myStopWatchObjects[10].Reset();
                }
                else
                {
                    myStopWatchObjects[10].Stop();
                    myRjButton11.Text = "00:00";
                    myStopWatchObjects[10].Start();
                }

                if (myObjects[10].Value == 258)
                {
                    rjButton11.BackColor = Color.Red;
                }
                else if (myObjects[10].Value == 262)
                {
                    rjButton11.BackColor = Color.Orange;
                }
                else if (myObjects[10].Value == 261)
                {
                    rjButton11.BackColor = Color.DarkGreen;
                }
                else if (myObjects[10].Value == 264)
                {
                    rjButton11.BackColor = Color.Blue;
                }

            };

            myObjects[11].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[11].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime12.Text = time;

                if (myObjects[11].Value == 261)
                {

                    myStopWatchObjects[11].Start();
                    myRjButton12.Text = "00:00";
                    myStopWatchObjects[11].Reset();
                }
                else
                {
                    myStopWatchObjects[11].Stop();
                    myRjButton12.Text = "00:00";
                    myStopWatchObjects[11].Start();
                }

                if (myObjects[11].Value == 258)
                {
                    rjButton12.BackColor = Color.Red;
                }
                else if (myObjects[11].Value == 262)
                {
                    rjButton12.BackColor = Color.Orange;
                }
                else if (myObjects[11].Value == 261)
                {
                    rjButton12.BackColor = Color.DarkGreen;
                }
                else if (myObjects[11].Value == 264)
                {
                    rjButton12.BackColor = Color.Blue;
                }

            };

            myObjects[12].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[12].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime13.Text = time;

                if (myObjects[12].Value == 261)
                {

                    myStopWatchObjects[12].Start();
                    myRjButton13.Text = "00:00";
                    myStopWatchObjects[12].Reset();
                }
                else
                {
                    myStopWatchObjects[12].Stop();
                    myRjButton13.Text = "00:00";
                    myStopWatchObjects[12].Start();
                }

                if (myObjects[12].Value == 258)
                {
                    rjButton13.BackColor = Color.Red;
                }
                else if (myObjects[12].Value == 262)
                {
                    rjButton13.BackColor = Color.Orange;
                }
                else if (myObjects[12].Value == 261)
                {
                    rjButton13.BackColor = Color.DarkGreen;
                }
                else if (myObjects[12].Value == 264)
                {
                    rjButton13.BackColor = Color.Blue;
                }

            };

            myObjects[13].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[13].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime14.Text = time;

                if (myObjects[13].Value == 261)
                {

                    myStopWatchObjects[13].Start();
                    myRjButton14.Text = "00:00";
                    myStopWatchObjects[13].Reset();
                }
                else
                {
                    myStopWatchObjects[13].Stop();
                    myRjButton14.Text = "00:00";
                    myStopWatchObjects[13].Start();
                }

                if (myObjects[13].Value == 258)
                {
                    rjButton14.BackColor = Color.Red;
                }
                else if (myObjects[13].Value == 262)
                {
                    rjButton14.BackColor = Color.Orange;
                }
                else if (myObjects[13].Value == 261)
                {
                    rjButton14.BackColor = Color.DarkGreen;
                }
                else if (myObjects[13].Value == 264)
                {
                    rjButton14.BackColor = Color.Blue;
                }

            };


            myObjects[14].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[14].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime15.Text = time;

                if (myObjects[14].Value == 261)
                {

                    myStopWatchObjects[14].Start();
                    myRjButton15.Text = "00:00";
                    myStopWatchObjects[14].Reset();
                }
                else
                {
                    myStopWatchObjects[14].Stop();
                    myRjButton15.Text = "00:00";
                    myStopWatchObjects[13].Start();
                }

                if (myObjects[14].Value == 258)
                {
                    rjButton15.BackColor = Color.Red;
                }
                else if (myObjects[14].Value == 262)
                {
                    rjButton15.BackColor = Color.Orange;
                }
                else if (myObjects[14].Value == 261)
                {
                    rjButton15.BackColor = Color.DarkGreen;
                }
                else if (myObjects[14].Value == 264)
                {
                    rjButton15.BackColor = Color.Blue;
                }

            };

            myObjects[15].WillChange += () => { Console.WriteLine("will be changed!"); };
            myObjects[15].DidChange += () => {
                Console.WriteLine("changed!");

                string time = DateTime.Now.ToString("hh:mm:ss"); // includes leading zeros
                // string date = DateTime.Now.ToString("dd/MM/yy"); // includes leading zeros 
                rjButtonTime16.Text = time;

                if (myObjects[15].Value == 261)
                { 
                    myStopWatchObjects[15].Start();
                    myRjButton16.Text = "00:00";
                    myStopWatchObjects[15].Reset();
                }
                else
                {
                    myStopWatchObjects[15].Stop();
                    myRjButton16.Text = "00:00";
                    myStopWatchObjects[15].Start();
                }

                if (myObjects[15].Value == 258)
                {
                    rjButton16.BackColor = Color.Red;
                }
                else if (myObjects[15].Value == 262)
                {
                    rjButton16.BackColor = Color.Orange;
                }
                else if (myObjects[15].Value == 261)
                {
                    rjButton16.BackColor = Color.DarkGreen;
                }
                else if (myObjects[15].Value == 264)
                {
                    rjButton16.BackColor = Color.Blue;
                }
                 
            };


        }

        private void rjButton3_Click(object sender, EventArgs e)
        {

        }

        public void StopWatchTimer()
        {
            if (myStopWatchObjects[0].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[0].ElapsedMilliseconds);
                myRjButton1.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton1.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);
                // Console.WriteLine("Running/Stop: " + stopWatchObj().IsRunning);
              //  Console.WriteLine("Running/Stop2: " + stopWatchObj2().IsRunning);

            }
            if (myStopWatchObjects[1].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[1].ElapsedMilliseconds);
                myRjButton2.Text = objTimeSpan.ToString("mm':'ss");

              //  Console.WriteLine("Running/Stop: " + stopWatchObj2().IsRunning);
                // form1.myRjButton2.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[2].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[2].ElapsedMilliseconds);
                myRjButton3.Text = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton3.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[3].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[3].ElapsedMilliseconds);
                myRjButton4.Text = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton4.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[4].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[4].ElapsedMilliseconds);
                myRjButton5.Text = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton5.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[5].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[5].ElapsedMilliseconds);
                myRjButton6.Text = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton6.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[6].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[6].ElapsedMilliseconds);
                myRjButton7.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton7.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[7].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[7].ElapsedMilliseconds);
                myRjButton8.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton8.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[8].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[8].ElapsedMilliseconds);
                myRjButton9.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton9.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[9].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[9].ElapsedMilliseconds);
                myRjButton10.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton10.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[10].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[10].ElapsedMilliseconds);
                myRjButton11.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton11.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[11].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[11].ElapsedMilliseconds);
                myRjButton12.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton12.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[12].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[12].ElapsedMilliseconds);
                myRjButton13.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton13.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[13].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[13].ElapsedMilliseconds);
                myRjButton14.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton14.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[14].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[14].ElapsedMilliseconds);
                myRjButton15.Text = objTimeSpan.ToString("mm':'ss");
                //form1.myRjButton15.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }
            if (myStopWatchObjects[15].IsRunning)
            {
                TimeSpan objTimeSpan = TimeSpan.FromMilliseconds(myStopWatchObjects[15].ElapsedMilliseconds);
                myRjButton16.Text = objTimeSpan.ToString("mm':'ss");
                // form1.myRjButton16.Text = String.Format(CultureInfo.CurrentCulture, "{0:00}:{1:00}:{2:00}", objTimeSpan.Hours, objTimeSpan.Minutes, objTimeSpan.Seconds);

            }

        }

    }
}
