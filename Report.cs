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
            var DT = new DataTable();
            DT.Columns.Add("Register Id", typeof(string));//it should place in front of the btn column
            DT.Columns.Add("Call Name", typeof(string));
            DT.Columns.Add("Date&Time", typeof(string));

            using (SQLiteDataReader read = comm.ExecuteReader())
            {
          
                while (read.Read())
                {
                    DataRow dr = DT.NewRow();
                    dr["Register Id"] = read["registerId"].ToString();
                    dr["Call Name"] = read["lastCallStatus"].ToString();
                    dr["Date&Time"] = read["dateTime"].ToString();
                    DT.Rows.Add(dr); 
                    dataGridView1.DataSource = DT;
                }
            }
        }
    }
}
