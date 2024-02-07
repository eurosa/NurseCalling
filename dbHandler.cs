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


                    string sql1 = "create table call_table (ID INTEGER PRIMARY KEY AUTOINCREMENT, lastCallValue varchar(200), lastCallStatus varchar(200), registerId varchar(200), dateTime varchar(200), elapseTime varchar(200), date_ varchar(200))";
                    SQLiteCommand command1 = new SQLiteCommand(sql1, dbConnection);
                    command1.ExecuteNonQuery();

                    string sql2 = "create table setting_table (ID INTEGER PRIMARY KEY AUTOINCREMENT, textBoxRegist1 varchar(60), textBoxRegist2 varchar(60), textBoxRegist3 varchar(60)" +
                        ", textBoxRegist4 varchar(60), textBoxRegist5 varchar(60), textBoxRegist6 varchar(60), textBoxRegist7 varchar(60), textBoxRegist8 varchar(60), textBoxRegist9 varchar(60)" +
                        ", textBoxRegist10 varchar(60), textBoxRegist11 varchar(60), textBoxRegist12 varchar(60), textBoxRegist13 varchar(60), textBoxRegist14 varchar(60)" +
                        ", textBoxRegist15 varchar(60), textBoxRegist16 varchar(60), textBoxRegist17 varchar(60), textBoxRegist18 varchar(60), textBoxRegist19 varchar(60), textBoxRegist20 varchar(60)" +
                        ", textBoxRegist21 varchar(60), textBoxRegist22 varchar(60), textBoxRegist23 varchar(60), textBoxRegist24 varchar(60), textBoxRegist25 varchar(60)" +
                        ", textBoxRegist26 varchar(60), textBoxRegist27 varchar(60), textBoxRegist28 varchar(60), textBoxRegist29 varchar(60), textBoxRegist30 varchar(60)" +
                        ", textBoxRegist31 varchar(60), textBoxRegist32 varchar(60), textBoxRegist33 varchar(60), textBoxRegist34 varchar(60), textBoxRegist35 varchar(60)" +
                        ", textBoxRegist36 varchar(60), textBoxRegist37 varchar(60), textBoxRegist38 varchar(60), textBoxRegist39 varchar(60), textBoxRegist40 varchar(60)" +
                        ", textBoxRegist41 varchar(60), textBoxRegist42 varchar(60), textBoxRegist43 varchar(60), textBoxRegist44 varchar(60), textBoxRegist45 varchar(60)" +
                        ", textBoxRegist46 varchar(60), textBoxRegist47 varchar(60), textBoxRegist48 varchar(60), textBoxRegist49 varchar(60), textBoxRegist50 varchar(60)" +
                        ", textBoxRegist51 varchar(60), textBoxRegist52 varchar(60), textBoxRegist53 varchar(60), textBoxRegist54 varchar(60), textBoxRegist55 varchar(60)" +
                        ", textBoxRegist56 varchar(60), textBoxRegist57 varchar(60), textBoxRegist58 varchar(60), textBoxRegist59 varchar(60), textBoxRegist60 varchar(60)" +
                        ", textBoxRegist61 varchar(60), textBoxRegist62 varchar(60), textBoxRegist63 varchar(60), textBoxRegist64 varchar(60),checkBoxRegister1 varchar(10)" +
                        ",checkBoxRegister2 varchar(10),checkBoxRegister3 varchar(10),checkBoxRegister4 varchar(10),checkBoxRegister5 varchar(10),checkBoxRegister6 varchar(10)" +
                        ",checkBoxRegister7 varchar(10),checkBoxRegister8 varchar(10),checkBoxRegister9 varchar(10),checkBoxRegister10 varchar(10),checkBoxRegister11 varchar(10)" +
                        ",checkBoxRegister12 varchar(10),checkBoxRegister13 varchar(10),checkBoxRegister14 varchar(10),checkBoxRegister15 varchar(10),checkBoxRegister16 varchar(10)" +
                        ",checkBoxRegister17 varchar(10),checkBoxRegister18 varchar(10),checkBoxRegister19 varchar(10),checkBoxRegister20 varchar(10),checkBoxRegister21 varchar(10)" +
                        ",checkBoxRegister22 varchar(10),checkBoxRegister23 varchar(10),checkBoxRegister24 varchar(10),checkBoxRegister25 varchar(10),checkBoxRegister26 varchar(10)" +
                        ",checkBoxRegister27 varchar(10),checkBoxRegister28 varchar(10),checkBoxRegister29 varchar(10),checkBoxRegister30 varchar(10),checkBoxRegister31 varchar(10)" +
                        ",checkBoxRegister32 varchar(10),checkBoxRegister33 varchar(10),checkBoxRegister34 varchar(10),checkBoxRegister35 varchar(10),checkBoxRegister36 varchar(10)" +
                        ",checkBoxRegister37 varchar(10),checkBoxRegister38 varchar(10),checkBoxRegister39 varchar(10),checkBoxRegister40 varchar(10),checkBoxRegister41 varchar(10)" +
                        ",checkBoxRegister42 varchar(10),checkBoxRegister43 varchar(10),checkBoxRegister44 varchar(10),checkBoxRegister45 varchar(10),checkBoxRegister46 varchar(10)" +
                        ",checkBoxRegister47 varchar(10),checkBoxRegister48 varchar(10),checkBoxRegister49 varchar(10),checkBoxRegister50 varchar(10),checkBoxRegister51 varchar(10)" +
                        ",checkBoxRegister52 varchar(10),checkBoxRegister53 varchar(10),checkBoxRegister54 varchar(10),checkBoxRegister55 varchar(10),checkBoxRegister56 varchar(10)" +
                        ",checkBoxRegister57 varchar(10),checkBoxRegister58 varchar(10),checkBoxRegister59 varchar(10),checkBoxRegister60 varchar(10),checkBoxRegister61 varchar(10)" +
                        ",checkBoxRegister62 varchar(10),checkBoxRegister33 varchar(10),checkBoxRegister64 varchar(10))";
                    SQLiteCommand command2 = new SQLiteCommand(sql2, dbConnection);
                    command2.ExecuteNonQuery();

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


        public void updatComport(SQLiteConnection m_dbConnection, DataModel modelData)
        {
            
            string sql_update = "UPDATE general_table SET comport_name = @comport_name Where ID = @ID";

            SQLiteCommand command = new SQLiteCommand(sql_update, m_dbConnection);

            command.Parameters.AddWithValue("@comport_name", modelData.comport_name);
         
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

        public void updateFirstCall(SQLiteConnection m_dbConnection, DataModel modelData)
        {

            string sql_update = "UPDATE general_table SET  firstcall_status = @firstcall_status Where ID = @ID";

            SQLiteCommand command = new SQLiteCommand(sql_update, m_dbConnection);
 
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


        public void insert_call_data(SQLiteConnection m_dbConnection, DataModel dataModel)
        {


            SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO call_table (lastCallValue, lastCallStatus, registerId, dateTime, date_,elapseTime) VALUES (@lastCallValue, @lastCallStatus, @registerId, @dateTime, @date_,@elapseTime)", m_dbConnection);

            insertSQL.Parameters.AddWithValue("@lastCallValue", dataModel.lastCallValue);
            insertSQL.Parameters.AddWithValue("@lastCallStatus", dataModel.lastCallStatus);
            insertSQL.Parameters.AddWithValue("@registerId", dataModel.registerId);
            insertSQL.Parameters.AddWithValue("@dateTime", dataModel.dateTime);
            insertSQL.Parameters.AddWithValue("@elapseTime", "00:00");
            insertSQL.Parameters.AddWithValue("@date_", DateTime.Parse(dataModel.dateTime.ToString()).ToString("yyyy-MM-dd"));

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

        public void update_call_data(SQLiteConnection m_dbConnection, string elapseTime, string regId)
        {
            if (elapseTime!="") { 
            string sql_update = "UPDATE call_table SET elapseTime = @elapseTime Where ID=(SELECT max(ID) FROM call_table where registerId=@registerId) and elapseTime='00:00'"; 
            SQLiteCommand command = new SQLiteCommand(sql_update, m_dbConnection);
            command.Parameters.AddWithValue("@elapseTime", elapseTime);
            command.Parameters.AddWithValue("@registerId", regId);

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
}
