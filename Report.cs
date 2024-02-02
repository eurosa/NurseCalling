using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            LoadData();
        }

        private void LoadData()
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
    }
}
