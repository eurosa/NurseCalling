using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NurseCalling
{
    public partial class Report : Form
    {
        GraphPane myPane;
        SQLiteConnection MDbConnection;

        SQLiteConnection SqlDbConnection;
        public Report(SQLiteConnection m_dbConnection)
        {
            InitializeComponent();

            myPane = zedGraphControl1.GraphPane;

            MDbConnection = m_dbConnection;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            dateTimePicker2.CustomFormat = "dd-MM-yyyy";
           

            FillSiteNameComboBox();
            FillCallComboBox();
            LoadData();

            // CreateGraph(zedGraphControl1);
            CreateGraph(zedGraphControl1, m_dbConnection);
        }

        private void LoadData()
        {
            SQLiteCommand comm;

            string startDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string endDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");

          //  string keyCallValue1 = ((KeyValuePair<string, string>)comboBoxCall.SelectedItem).Key;
            string keyRegisterId1 = ((KeyValuePair<string, string>)comboBoxSiteName.SelectedItem).Key;
            //Console.WriteLine("keyCallValue1: "+ keyCallValue1+" Register Id: "+ keyRegisterId1);

            if (comboBoxSiteName.SelectedIndex>0) {

                string keyRegisterId = ((KeyValuePair<string, string>)comboBoxSiteName.SelectedItem).Key;
                comm = new SQLiteCommand("Select * From call_table where registerId='"+ keyRegisterId + "' and date_ between '" + startDate + "' and '" + endDate + "'", MDbConnection);
                Console.WriteLine("Select * From call_table where registerId='" + keyRegisterId + "' and date_ between '" + startDate + "' and '" + endDate + "'");
            }
           /* else if(comboBoxCall.SelectedIndex > 0 )
            {
                string keyCallValue = ((KeyValuePair<string, string>)comboBoxCall.SelectedItem).Key;
                comm = new SQLiteCommand("Select * From call_table where lastCallValue='" + keyCallValue + "'", MDbConnection);

            }
            else if (comboBoxSiteName.SelectedIndex > 0 && comboBoxCall.SelectedIndex > 0)
            {
                string keyCallValue = ((KeyValuePair<string, string>)comboBoxCall.SelectedItem).Key;
                string keyRegisterId = ((KeyValuePair<string, string>)comboBoxSiteName.SelectedItem).Key;
                comm = new SQLiteCommand("Select * From call_table where lastCallValue='" + keyCallValue + "' and registerId='" + keyRegisterId + "'", MDbConnection);

            }*/
            else {
                Console.WriteLine("Select * From call_table where  date_  between '" + startDate + "' and '" + endDate + "'");
                // comm = new SQLiteCommand("Select * From call_table where  date_ >='" + startDate + "' and date_ <= '" + endDate + "'", MDbConnection);
                comm = new SQLiteCommand("Select * From call_table where  date_  between '" + startDate + "' and '" + endDate + "'", MDbConnection);

            }
          
        
            DataTable table1 = new DataTable();
            table1.Columns.AddRange(new DataColumn[]
                  {
                    new DataColumn("Register Id", typeof(System.String)),
                    new DataColumn("Call Name", typeof(System.String)),
                    new DataColumn("Date&Time", typeof(System.String)),
                    new DataColumn("Elapse Time", typeof(System.String)),
 
                  }
              );

            using (SQLiteDataReader read = comm.ExecuteReader())
            {
                TimeSpan result=new TimeSpan();
                string elapsedTimeString;
                while (read.Read())
                { 
                    table1.Rows.Add(new Object[] {read["registerId"].ToString(), read["lastCallStatus"].ToString(), read["dateTime"].ToString(),
                    read["elapseTime"].ToString() });
                    result +=  TimeSpan.Parse("00:"+read["elapseTime"].ToString());
                     
                }
               elapsedTimeString = string.Format("{0}:{1}:{2}",
                                       result.Hours.ToString("00"),
                                       result.Minutes.ToString("00"),
                                       result.Seconds.ToString("00"));

                table1.Rows.Add(new Object[] {"","","Total Elapse Time",
                    elapsedTimeString });
                
                dataGridView1.DataSource = table1;
               // dataGridView1.Rows[2].Cells[1].Style.BackColor = Color.Yellow;
                //   dataGridView1.Rows[6].DefaultCellStyle.BackColor = SystemColors.ControlDarkDark; 
                //  DataGridViewColorChange(dataGridView1);
            }

        }

        private void LoadDataBySearch()
        {

            SQLiteCommand comm = new SQLiteCommand("Select * From call_table where 1", MDbConnection);

            string format = "mm:ss";

            DataTable table1 = new DataTable();
            table1.Columns.AddRange(new DataColumn[]
                  {
                    new DataColumn("Register Id", typeof(System.String)),
                    new DataColumn("Call Name", typeof(System.String)),
                    new DataColumn("Date&Time", typeof(System.String)),
                    new DataColumn("Elapse Time", typeof(System.String)),

                  }
              );

            using (SQLiteDataReader read = comm.ExecuteReader())
            {
                TimeSpan result = new TimeSpan();
                string elapsedTimeString;
                while (read.Read())
                {
                    table1.Rows.Add(new Object[] {read["registerId"].ToString(), read["lastCallStatus"].ToString(), read["dateTime"].ToString(),
                    read["elapseTime"].ToString() });
                    result += TimeSpan.Parse("00:" + read["elapseTime"].ToString());

                }
                elapsedTimeString = string.Format("{0}:{1}:{2}",
                                        result.Hours.ToString("00"),
                                        result.Minutes.ToString("00"),
                                        result.Seconds.ToString("00"));

                table1.Rows.Add(new Object[] {"","","Total Elapse Time",
                    elapsedTimeString });

                dataGridView1.DataSource = table1;
                // dataGridView1.Rows[2].Cells[1].Style.BackColor = Color.Yellow;
                //   dataGridView1.Rows[6].DefaultCellStyle.BackColor = SystemColors.ControlDarkDark; 
                //  DataGridViewColorChange(dataGridView1);
            }

        }


        public void DataGridViewColorChange(DataGridView dataGridView)
        {

            int counter = dataGridView.Rows.Count;
 

            for (int i = 0; i < counter; i++)
            {
                
                if (i == counter - 1)
                {
                    Console.WriteLine("myrow: " + i);
                    //this is where your LAST LINE code goes
                    //row.DefaultCellStyle.BackColor = Color.Yellow;
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                }
                else
                {
                    //this is your normal code NOT LAST LINE
                    //row.DefaultCellStyle.BackColor = Color.Red;
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                }
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
            CreateGraph(zedGraphControl1, MDbConnection);
        }

        void FillSiteNameComboBox()
        {
            Dictionary<string, string> test = new Dictionary<string, string>();
            test.Add("select", "Select a Id");

            // themeColorComboBox.SelectedIndex = themeColorComboBox.FindStringExact(dataModel.theme_color);
    
            SQLiteCommand cmd = new SQLiteCommand("select DISTINCT TRIM(registerId) as registerId  From call_table", MDbConnection);
            SQLiteDataReader Sdr = cmd.ExecuteReader();
      
            while (Sdr.Read())
            {
             
                test.Add(Sdr["registerId"].ToString(), Sdr["registerId"].ToString());
     
            }
            comboBoxSiteName.DataSource = new BindingSource(test, null);
            comboBoxSiteName.DisplayMember = "Value";
            comboBoxSiteName.ValueMember = "Key";
            Sdr.Close();
            

        }

        void FillCallComboBox()
        {
            Dictionary<string, string> test = new Dictionary<string, string>();
            test.Add("select", "Select a Call Name");

            //    themeColorComboBox.SelectedIndex = themeColorComboBox.FindStringExact(dataModel.theme_color);

            SQLiteCommand cmd = new SQLiteCommand("select DISTINCT lastCallValue, lastCallStatus  From call_table", MDbConnection);
            SQLiteDataReader Sdr = cmd.ExecuteReader();
     
            while (Sdr.Read())
            {
                if (!test.ContainsKey(Sdr["lastCallStatus"].ToString()))
                {
                    test.Add(Sdr["lastCallStatus"].ToString(), Sdr["lastCallStatus"].ToString());
                }
           
            }
       //     comboBoxCall.DataSource = new BindingSource(test, null);
        //    comboBoxCall.DisplayMember = "Value";
         ///   comboBoxCall.ValueMember = "Key";
            Sdr.Close();


        }

        private void comboBoxCall_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  string key = ((KeyValuePair<string, string>)comboBoxCall.SelectedItem).Key;
            
        }

        private void comboBoxSiteName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = ((KeyValuePair<string, string>)comboBoxSiteName.SelectedItem).Key;
   
        }

        private void exportAsExcel_Click(object sender, EventArgs e)
        {
            // Create a thread
            Thread backgroundThread = new Thread(new ThreadStart(ExportAsExcel));
            // Start thread
            backgroundThread.Start();
        }

        public void ExportAsExcel()
        {

            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Exported from gridview";
            // storing header part in Excel  
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
            // save the application  
            //  workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();

        }

        private void CreateGraph(ZedGraphControl zg1)
        {
            // get a reference to the GraphPane
            GraphPane myPane = zg1.GraphPane;

            // Set the Titles
            myPane.Title.Text = "My Test Bar Graph";
            myPane.XAxis.Title.Text = "Label";
            myPane.YAxis.Title.Text = "My Y Axis";

            // Make up some random data points
            string[] labels = { "Panther", "Lion", "Cheetah",
                      "Cougar", "Tiger", "Leopard" };
            double[] y = { 100, 115, 75, 22, 98, 40 };
            double[] y2 = { 90, 100, 95, 35, 80, 35 };
            double[] y3 = { 80, 110, 65, 15, 54, 67 };
            double[] y4 = { 120, 125, 100, 40, 105, 75 };

            // Generate a red bar with "Curve 1" in the legend
            BarItem myBar = myPane.AddBar("Curve 1", null, y,
                                                        Color.Red);
            myBar.Bar.Fill = new Fill(Color.Red, Color.White,
                                                        Color.Red);

            // Generate a blue bar with "Curve 2" in the legend
            myBar = myPane.AddBar("Curve 2", null, y2, Color.Blue);
            myBar.Bar.Fill = new Fill(Color.Blue, Color.White,
                                                        Color.Blue);

            // Generate a green bar with "Curve 3" in the legend
            myBar = myPane.AddBar("Curve 3", null, y3, Color.Green);
            myBar.Bar.Fill = new Fill(Color.Green, Color.White,
                                                        Color.Green);

            // Generate a black line with "Curve 4" in the legend
            LineItem myCurve = myPane.AddCurve("Curve 4",
                  null, y4, Color.Black, SymbolType.Circle);
            myCurve.Line.Fill = new Fill(Color.White,
                                  Color.LightSkyBlue, -45F);

            // Fix up the curve attributes a little
            myCurve.Symbol.Size = 8.0F;
            myCurve.Symbol.Fill = new Fill(Color.White);
            myCurve.Line.Width = 2.0F;

            // Draw the X tics between the labels instead of 
            // at the labels
            myPane.XAxis.MajorTic.IsBetweenLabels = true;

            // Set the XAxis labels
            myPane.XAxis.Scale.TextLabels = labels;
            // Set the XAxis to Text type
            myPane.XAxis.Type = AxisType.Text;

            // Fill the Axis and Pane backgrounds
            myPane.Chart.Fill = new Fill(Color.White,
                  Color.FromArgb(255, 255, 166), 90F);
            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zg1.AxisChange();
        }

        public void CreateGraph(ZedGraphControl zg1, SQLiteConnection mbConnection)
        {
            string keyRegisterId = ((KeyValuePair<string, string>)comboBoxSiteName.SelectedItem).Key;
            string startDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string endDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            zg1.GraphPane.CurveList.Clear();
            zg1.ZoomOutAll(myPane);
            zg1.Refresh();
            Console.WriteLine("Just Click on Humidity");
            GraphData sd = new GraphData();
            sd.getGraphData(mbConnection, keyRegisterId, comboBoxSiteName.SelectedIndex, startDate, endDate);
            // zg1.GraphPane.YAxis.Scale.Min = 0;
            // zg1.GraphPane.YAxis.Scale.Max = 60;
            // List<Double> valueList = new List<Double>(sd.dateHumidity.Values);
            // List<string> keyList = new List<string>(sd.dateHumidity.Keys);
            // get a reference to the GraphPane
            // GraphPane myPane = zg1.GraphPane;
            myPane.Title.FontSpec.Size = 8.0f;
            myPane.YAxis.Title.FontSpec.Size = 9.0f;
            myPane.XAxis.Title.FontSpec.Size = 9.0f;
            myPane.XAxis.Scale.FontSpec.Size = 6.0f;
            myPane.YAxis.Scale.FontSpec.Size = 6.0f;

            zg1.Refresh();
            

            // Set the title and axis labels
            myPane.Title.Text = "Total Time Versus Hub Chart";
            myPane.XAxis.Title.Text = "Hub";
            myPane.YAxis.Title.Text = "Total Minutes";
            myPane.XAxis.Scale.FontSpec.Angle = 65;
            // Make up some random data points
            BarItem myBar = myPane.AddBar("Bar", null, sd.hubNTime.Values.ToArray(),
                                                          Color.Red);
            myBar.Bar.Fill = new Fill(Color.Red, Color.White,
                                                        Color.Red);

            // Generate a black line with "Curve 4" in the legend
            LineItem myCurve = myPane.AddCurve("Total Minutes vs Hub",
                  null, sd.hubNTime.Values.ToArray(), Color.Black, SymbolType.Circle);

           /* LineItem myCurve = myPane.AddCurve("Temperature vs Date Time",
                null, sd.hubNTime.Values.ToArray(), Color.Black, SymbolType.Circle); */

            myCurve.Line.Fill = new Fill(Color.White,
                                  Color.LightSkyBlue, -45F);

            // Fix up the curve attributes a little
            myCurve.Symbol.Size = 8.0F;
            myCurve.Symbol.Fill = new Fill(Color.White);
            myCurve.Line.Width = 2.0F;
            myCurve.Line.IsSmooth = true;

            myPane.XAxis.Scale.MinorStep = 1;//X-axis small step size 1, which is a small interval
            myPane.XAxis.Scale.MajorStep = 1;//The large step of the X axis is 5, which is the large interval of the displayed text

            myPane.XAxis.Scale.MajorUnit = DateUnit.Minute;
            myPane.XAxis.Scale.MinorUnit = DateUnit.Second;

            // Draw the X tics between the labels instead of 
            // at the labels
            myPane.XAxis.MajorTic.IsBetweenLabels = false;
            zg1.IsShowHScrollBar = true;
            zg1.IsAutoScrollRange = true;
            zg1.IsShowPointValues = true;

            double xRange = myPane.XAxis.Scale.Max - myPane.XAxis.Scale.Min;
           /* myPane.XAxis.Scale.Max = sd.dateTemperature.Values.Count() + 1;
            myPane.XAxis.Scale.Min = (sd.dateTemperature.Values.Count() + 1) - 91;*/
            //  myPane.XAxis.Scale.Min = myPane.XAxis.Scale.Max - xRange;

            // Set the XAxis labels
            myPane.XAxis.Scale.TextLabels = sd.hubNTime.Keys.ToArray();
            // Set the XAxis to Text type
            myPane.XAxis.Type = AxisType.Text;

            // Fill the Axis and Pane backgrounds
            myPane.Chart.Fill = new Fill(Color.White,
                  Color.FromArgb(255, 255, 166), 90F);
            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));


            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zg1.AxisChange();
            zg1.Refresh();
        }
    }
}
