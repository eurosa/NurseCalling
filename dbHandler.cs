using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseCalling
{
    public class dbHandler
    {
          
        public void createDB(DataModel dataModel)
        {

            if (!File.Exists("dscp.sqlite"))
            {
                try
                {
                    SQLiteConnection.CreateFile("dscp.sqlite");

                    SQLiteConnection dbConnection = new SQLiteConnection("Data Source=dscp.sqlite;Version=3;");

                    dbConnection.Open();
                    string sql = "create table general_table (ID INTEGER PRIMARY KEY AUTOINCREMENT, comport_name varchar(60), firstcall_status varchar(60))"; 
                    SQLiteCommand command = new SQLiteCommand(sql, dbConnection); 
                    command.ExecuteNonQuery();

                    try
                    {
                        insert_general_data(dbConnection);
                    }
                    catch (Exception Ex)
                    {

                    }
                    
                    dbConnection.Close();

                }
                catch (Exception Ex)
                {

                }
            }

        }

         
        public void getGeneralData(SQLiteConnection m_dbConnection, DataModel dataModel)
        {
            SQLiteCommand cmd = new SQLiteCommand("select * From general_table  where 1 ", m_dbConnection);
            SQLiteDataReader Sdr = cmd.ExecuteReader();
            while (Sdr.Read())
            {
                dataModel.comport_name = Sdr["comport_name"].ToString();
                dataModel.firstcall_status = Sdr["firstcall_status"].ToString();
            }
            Sdr.Close();
        }


         
        public void insert_general_data(SQLiteConnection m_dbConnection)
        {
 

            SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO general_table (comport_name, firstcall_status)", m_dbConnection);

            insertSQL.Parameters.AddWithValue("@comport_name", "COM1");
            insertSQL.Parameters.AddWithValue("@firstcall_status", 1);

            try
            {
                insertSQL.ExecuteNonQuery();
                // AutoClosingMessageBox.Show("Data Inserted Successfully", "Insert", 1000);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 

        }

         
       

        public void updateGeneralTable(SQLiteConnection m_dbConnection, DataModel modelData)
        {
            
            string sql_update = "UPDATE general_table SET comport_name = @comport_name, firstcall_status = @firstcall_status Where ID = @ID";

            SQLiteCommand command = new SQLiteCommand(sql_update, m_dbConnection);

            command.Parameters.AddWithValue("@comport_name", modelData.comport_name);
            command.Parameters.AddWithValue("@firstcall_status", modelData.firstcall_status); 
            command.Parameters.AddWithValue("@ID", 1);

            try
            {
                command.ExecuteNonQuery(); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
