using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NurseCalling
{
    public partial class Report : Form
    {
        SQLiteConnection MDbConnection;

        SQLiteConnection SqlDbConnection;
        public Report(SQLiteConnection m_dbConnection)
        {
            InitializeComponent();
            MDbConnection = m_dbConnection;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            dateTimePicker2.CustomFormat = "dd-MM-yyyy";
           

            FillSiteNameComboBox();
            FillCallComboBox();
            LoadData();
        }

        private void LoadData()
        {
            SQLiteCommand comm;

            string startDate = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            string endDate = dateTimePicker2.Value.ToString("dd-MM-yyyy");

          //  string keyCallValue1 = ((KeyValuePair<string, string>)comboBoxCall.SelectedItem).Key;
            string keyRegisterId1 = ((KeyValuePair<string, string>)comboBoxSiteName.SelectedItem).Key;
            //Console.WriteLine("keyCallValue1: "+ keyCallValue1+" Register Id: "+ keyRegisterId1);

            if (comboBoxSiteName.SelectedIndex>0) {

                string keyRegisterId = ((KeyValuePair<string, string>)comboBoxSiteName.SelectedItem).Key;
                comm = new SQLiteCommand("Select * From call_table where registerId='"+ keyRegisterId + "' and date_ >='"+ startDate + "' and date_ <= '"+ endDate + "'", MDbConnection);
                Console.WriteLine("Select * From call_table where registerId='" + keyRegisterId + "' and date_ >='" + startDate + "' and date_ <= '" + endDate + "'");
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

                comm = new SQLiteCommand("Select * From call_table where  date_ >='" + startDate + "' and date_ <= '" + endDate + "'", MDbConnection);

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
    }
}
