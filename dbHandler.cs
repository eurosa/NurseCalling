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
                        ", checkBoxRegister2 varchar(10),checkBoxRegister3 varchar(10),checkBoxRegister4 varchar(10),checkBoxRegister5 varchar(10),checkBoxRegister6 varchar(10)" +
                        ", checkBoxRegister7 varchar(10),checkBoxRegister8 varchar(10),checkBoxRegister9 varchar(10),checkBoxRegister10 varchar(10),checkBoxRegister11 varchar(10)" +
                        ", checkBoxRegister12 varchar(10),checkBoxRegister13 varchar(10),checkBoxRegister14 varchar(10),checkBoxRegister15 varchar(10),checkBoxRegister16 varchar(10)" +
                        ", checkBoxRegister17 varchar(10),checkBoxRegister18 varchar(10),checkBoxRegister19 varchar(10),checkBoxRegister20 varchar(10),checkBoxRegister21 varchar(10)" +
                        ", checkBoxRegister22 varchar(10),checkBoxRegister23 varchar(10),checkBoxRegister24 varchar(10),checkBoxRegister25 varchar(10),checkBoxRegister26 varchar(10)" +
                        ", checkBoxRegister27 varchar(10),checkBoxRegister28 varchar(10),checkBoxRegister29 varchar(10),checkBoxRegister30 varchar(10),checkBoxRegister31 varchar(10)" +
                        ", checkBoxRegister32 varchar(10),checkBoxRegister33 varchar(10),checkBoxRegister34 varchar(10),checkBoxRegister35 varchar(10),checkBoxRegister36 varchar(10)" +
                        ", checkBoxRegister37 varchar(10),checkBoxRegister38 varchar(10),checkBoxRegister39 varchar(10),checkBoxRegister40 varchar(10),checkBoxRegister41 varchar(10)" +
                        ", checkBoxRegister42 varchar(10),checkBoxRegister43 varchar(10),checkBoxRegister44 varchar(10),checkBoxRegister45 varchar(10),checkBoxRegister46 varchar(10)" +
                        ", checkBoxRegister47 varchar(10),checkBoxRegister48 varchar(10),checkBoxRegister49 varchar(10),checkBoxRegister50 varchar(10),checkBoxRegister51 varchar(10)" +
                        ", checkBoxRegister52 varchar(10),checkBoxRegister53 varchar(10),checkBoxRegister54 varchar(10),checkBoxRegister55 varchar(10),checkBoxRegister56 varchar(10)" +
                        ", checkBoxRegister57 varchar(10),checkBoxRegister58 varchar(10),checkBoxRegister59 varchar(10),checkBoxRegister60 varchar(10),checkBoxRegister61 varchar(10)" +
                        ", checkBoxRegister62 varchar(10),checkBoxRegister63 varchar(10),checkBoxRegister64 varchar(10))";
                    SQLiteCommand command2 = new SQLiteCommand(sql2, dbConnection);
                    command2.ExecuteNonQuery();

                    try
                    {
                        insert_general_data(dbConnection);

                    }
                    catch (Exception Ex)
                    {

                    }

                    try
                    {
                        insert_setting_data(dbConnection);

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


        public void insert_setting_data(SQLiteConnection m_dbConnection)
        {


            SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO setting_table (textBoxRegist1, textBoxRegist2,textBoxRegist3,textBoxRegist4" +
                ",textBoxRegist5,textBoxRegist6,textBoxRegist7,textBoxRegist8,textBoxRegist9,textBoxRegist10,textBoxRegist11,textBoxRegist12" +
                ",textBoxRegist13,textBoxRegist14,textBoxRegist15,textBoxRegist16,textBoxRegist17,textBoxRegist18,textBoxRegist19,textBoxRegist20" +
                ",textBoxRegist21,textBoxRegist22,textBoxRegist23,textBoxRegist24,textBoxRegist25,textBoxRegist26,textBoxRegist27,textBoxRegist28" +
                ",textBoxRegist29,textBoxRegist30,textBoxRegist31,textBoxRegist32,textBoxRegist33,textBoxRegist34,textBoxRegist35,textBoxRegist36" +
                ",textBoxRegist37,textBoxRegist38,textBoxRegist39,textBoxRegist40,textBoxRegist41,textBoxRegist42,textBoxRegist43,textBoxRegist44" +
                ",textBoxRegist45,textBoxRegist46,textBoxRegist47,textBoxRegist48,textBoxRegist49,textBoxRegist50,textBoxRegist51,textBoxRegist52" +
                ",textBoxRegist53,textBoxRegist54,textBoxRegist55,textBoxRegist56,textBoxRegist57,textBoxRegist58,textBoxRegist59,textBoxRegist60" +
                ",textBoxRegist61,textBoxRegist62,textBoxRegist63,textBoxRegist64,checkBoxRegister1,checkBoxRegister2,checkBoxRegister3" +
                ",checkBoxRegister4,checkBoxRegister5,checkBoxRegister6,checkBoxRegister7,checkBoxRegister8,checkBoxRegister9,checkBoxRegister10" +
                ",checkBoxRegister11,checkBoxRegister12,checkBoxRegister13,checkBoxRegister14,checkBoxRegister15,checkBoxRegister16,checkBoxRegister17" +
                ",checkBoxRegister18,checkBoxRegister19,checkBoxRegister20,checkBoxRegister21,checkBoxRegister22,checkBoxRegister23,checkBoxRegister24" +
                ",checkBoxRegister25,checkBoxRegister26,checkBoxRegister27,checkBoxRegister28,checkBoxRegister29,checkBoxRegister30,checkBoxRegister31" +
                ",checkBoxRegister32,checkBoxRegister33,checkBoxRegister34,checkBoxRegister35,checkBoxRegister36,checkBoxRegister37,checkBoxRegister38,checkBoxRegister39" +
                ",checkBoxRegister40,checkBoxRegister41,checkBoxRegister42,checkBoxRegister43,checkBoxRegister44,checkBoxRegister45,checkBoxRegister46,checkBoxRegister47" +
                ",checkBoxRegister48,checkBoxRegister49,checkBoxRegister50,checkBoxRegister51,checkBoxRegister52,checkBoxRegister53,checkBoxRegister54,checkBoxRegister55" +
                ",checkBoxRegister56,checkBoxRegister57,checkBoxRegister58,checkBoxRegister59,checkBoxRegister60,checkBoxRegister61,checkBoxRegister62,checkBoxRegister63" +
                ",checkBoxRegister64) VALUES (@textBoxRegist1, @textBoxRegist2, @textBoxRegist3, @textBoxRegist4" +
                ", @textBoxRegist5, @textBoxRegist6, @textBoxRegist7, @textBoxRegist8, @textBoxRegist9, @textBoxRegist10, @textBoxRegist11" +
                ", @textBoxRegist12, @textBoxRegist13, @textBoxRegist14, @textBoxRegist15, @textBoxRegist16, @textBoxRegist17, @textBoxRegist18" +
                ", @textBoxRegist19, @textBoxRegist20, @textBoxRegist21, @textBoxRegist22, @textBoxRegist23, @textBoxRegist24 ,@textBoxRegist25" +
                ",@textBoxRegist26,@textBoxRegist27,@textBoxRegist28,@textBoxRegist29,@textBoxRegist30,@textBoxRegist31,@textBoxRegist32,@textBoxRegist33" +
                ",@textBoxRegist34,@textBoxRegist35,@textBoxRegist36,@textBoxRegist37,@textBoxRegist38,@textBoxRegist39,@textBoxRegist40,@textBoxRegist41" +
                ",@textBoxRegist42,@textBoxRegist43,@textBoxRegist44,@textBoxRegist45,@textBoxRegist46,@textBoxRegist47,@textBoxRegist48,@textBoxRegist49" +
                ",@textBoxRegist50,@textBoxRegist51,@textBoxRegist52,@textBoxRegist53,@textBoxRegist54,@textBoxRegist55,@textBoxRegist56,@textBoxRegist57" +
                ",@textBoxRegist58,@textBoxRegist59,@textBoxRegist60,@textBoxRegist61,@textBoxRegist62,@textBoxRegist63,@textBoxRegist64,@checkBoxRegister1" +
                ",@checkBoxRegister2,@checkBoxRegister3,@checkBoxRegister4,@checkBoxRegister5,@checkBoxRegister6,@checkBoxRegister7,@checkBoxRegister8" +
                ",@checkBoxRegister9,@checkBoxRegister10,@checkBoxRegister11,@checkBoxRegister12,@checkBoxRegister13,@checkBoxRegister14,@checkBoxRegister15,@checkBoxRegister16" +
                ",@checkBoxRegister17,@checkBoxRegister18,@checkBoxRegister19,@checkBoxRegister20,@checkBoxRegister21,@checkBoxRegister22,@checkBoxRegister23,@checkBoxRegister24" +
                ",@checkBoxRegister25,@checkBoxRegister26,@checkBoxRegister27,@checkBoxRegister28,@checkBoxRegister29,@checkBoxRegister30,@checkBoxRegister31" +
                ",@checkBoxRegister32,@checkBoxRegister33,@checkBoxRegister34,@checkBoxRegister35,@checkBoxRegister36,@checkBoxRegister37,@checkBoxRegister38" +
                ",@checkBoxRegister39,@checkBoxRegister40,@checkBoxRegister41,@checkBoxRegister42,@checkBoxRegister43,@checkBoxRegister44,@checkBoxRegister45" +
                ",@checkBoxRegister46,@checkBoxRegister47,@checkBoxRegister48,@checkBoxRegister49,@checkBoxRegister50,@checkBoxRegister51,@checkBoxRegister52" +
                ",@checkBoxRegister53,@checkBoxRegister54,@checkBoxRegister55,@checkBoxRegister56,@checkBoxRegister57,@checkBoxRegister58,@checkBoxRegister59" +
                ",@checkBoxRegister60,@checkBoxRegister61,@checkBoxRegister62,@checkBoxRegister63,@checkBoxRegister64)", m_dbConnection);

            insertSQL.Parameters.AddWithValue("@textBoxRegist1", "Hub 1");
            insertSQL.Parameters.AddWithValue("@textBoxRegist2", "Hub 2");
            insertSQL.Parameters.AddWithValue("@textBoxRegist3", "Hub 3");
            insertSQL.Parameters.AddWithValue("@textBoxRegist4", "Hub 4");
            insertSQL.Parameters.AddWithValue("@textBoxRegist5", "Hub 5");
            insertSQL.Parameters.AddWithValue("@textBoxRegist6", "Hub 6");
            insertSQL.Parameters.AddWithValue("@textBoxRegist7", "Hub 7");
            insertSQL.Parameters.AddWithValue("@textBoxRegist8", "Hub 8");
            insertSQL.Parameters.AddWithValue("@textBoxRegist9", "Hub 9");
            insertSQL.Parameters.AddWithValue("@textBoxRegist10", "Hub 10");
            insertSQL.Parameters.AddWithValue("@textBoxRegist11", "Hub 11");
            insertSQL.Parameters.AddWithValue("@textBoxRegist12", "Hub 12");
            insertSQL.Parameters.AddWithValue("@textBoxRegist13", "Hub 13");
            insertSQL.Parameters.AddWithValue("@textBoxRegist14", "Hub 14");
            insertSQL.Parameters.AddWithValue("@textBoxRegist15", "Hub 15");
            insertSQL.Parameters.AddWithValue("@textBoxRegist16", "Hub 16");
            insertSQL.Parameters.AddWithValue("@textBoxRegist17", "Hub 17");
            insertSQL.Parameters.AddWithValue("@textBoxRegist18", "Hub 18");
            insertSQL.Parameters.AddWithValue("@textBoxRegist19", "Hub 19");
            insertSQL.Parameters.AddWithValue("@textBoxRegist20", "Hub 20");
            insertSQL.Parameters.AddWithValue("@textBoxRegist21", "Hub 21");
            insertSQL.Parameters.AddWithValue("@textBoxRegist22", "Hub 22");
            insertSQL.Parameters.AddWithValue("@textBoxRegist23", "Hub 23");
            insertSQL.Parameters.AddWithValue("@textBoxRegist24", "Hub 24");
            insertSQL.Parameters.AddWithValue("@textBoxRegist25", "Hub 25");
            insertSQL.Parameters.AddWithValue("@textBoxRegist26", "Hub 26");
            insertSQL.Parameters.AddWithValue("@textBoxRegist27", "Hub 27");
            insertSQL.Parameters.AddWithValue("@textBoxRegist28", "Hub 28");
            insertSQL.Parameters.AddWithValue("@textBoxRegist29", "Hub 29");
            insertSQL.Parameters.AddWithValue("@textBoxRegist30", "Hub 30");
            insertSQL.Parameters.AddWithValue("@textBoxRegist31", "Hub 31");
            insertSQL.Parameters.AddWithValue("@textBoxRegist32", "Hub 32");
            insertSQL.Parameters.AddWithValue("@textBoxRegist33", "Hub 33");
            insertSQL.Parameters.AddWithValue("@textBoxRegist34", "Hub 34");
            insertSQL.Parameters.AddWithValue("@textBoxRegist35", "Hub 35");
            insertSQL.Parameters.AddWithValue("@textBoxRegist36", "Hub 36");
            insertSQL.Parameters.AddWithValue("@textBoxRegist37", "Hub 37");
            insertSQL.Parameters.AddWithValue("@textBoxRegist38", "Hub 38");
            insertSQL.Parameters.AddWithValue("@textBoxRegist39", "Hub 39");
            insertSQL.Parameters.AddWithValue("@textBoxRegist40", "Hub 40");
            insertSQL.Parameters.AddWithValue("@textBoxRegist41", "Hub 41");
            insertSQL.Parameters.AddWithValue("@textBoxRegist42", "Hub 42");
            insertSQL.Parameters.AddWithValue("@textBoxRegist43", "Hub 43");
            insertSQL.Parameters.AddWithValue("@textBoxRegist44", "Hub 44");
            insertSQL.Parameters.AddWithValue("@textBoxRegist45", "Hub 45");
            insertSQL.Parameters.AddWithValue("@textBoxRegist46", "Hub 46");
            insertSQL.Parameters.AddWithValue("@textBoxRegist47", "Hub 47");
            insertSQL.Parameters.AddWithValue("@textBoxRegist48", "Hub 48");
            insertSQL.Parameters.AddWithValue("@textBoxRegist49", "Hub 49");
            insertSQL.Parameters.AddWithValue("@textBoxRegist50", "Hub 50");
            insertSQL.Parameters.AddWithValue("@textBoxRegist51", "Hub 51");
            insertSQL.Parameters.AddWithValue("@textBoxRegist52", "Hub 52");
            insertSQL.Parameters.AddWithValue("@textBoxRegist53", "Hub 53");
            insertSQL.Parameters.AddWithValue("@textBoxRegist54", "Hub 54");
            insertSQL.Parameters.AddWithValue("@textBoxRegist55", "Hub 55");
            insertSQL.Parameters.AddWithValue("@textBoxRegist56", "Hub 56");
            insertSQL.Parameters.AddWithValue("@textBoxRegist57", "Hub 57");
            insertSQL.Parameters.AddWithValue("@textBoxRegist58", "Hub 58");
            insertSQL.Parameters.AddWithValue("@textBoxRegist59", "Hub 59");
            insertSQL.Parameters.AddWithValue("@textBoxRegist60", "Hub 60");
            insertSQL.Parameters.AddWithValue("@textBoxRegist61", "Hub 61");
            insertSQL.Parameters.AddWithValue("@textBoxRegist62", "Hub 62");
            insertSQL.Parameters.AddWithValue("@textBoxRegist63", "Hub 63");
            insertSQL.Parameters.AddWithValue("@textBoxRegist64", "Hub 64");
            insertSQL.Parameters.AddWithValue("@checkBoxRegister1", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister2", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister3", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister4", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister5", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister6", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister7", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister8", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister9", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister10", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister11", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister12", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister13", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister14", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister15", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister16", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister17", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister18", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister19", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister20", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister21", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister22", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister23", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister24", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister25", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister26", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister27", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister28", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister29", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister30", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister31", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister32", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister33", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister34", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister35", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister36", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister37", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister38", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister39", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister40", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister41", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister42", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister43", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister44", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister45", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister46", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister47", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister48", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister49", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister50", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister51", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister52", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister53", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister54", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister55", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister56", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister57", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister58", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister59", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister60", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister61", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister62", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister63", 0);
            insertSQL.Parameters.AddWithValue("@checkBoxRegister64", 0);

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


        public void update_setting_table_data(SQLiteConnection m_dbConnection, DataModel modelData)
        {

            string sql_update = "UPDATE setting_table SET   textBoxRegist1 = @textBoxRegist1, textBoxRegist2 = @textBoxRegist2 ,textBoxRegist3 = @textBoxRegist3 "+
                                ",textBoxRegist4 = @textBoxRegist4 " +
                                ",textBoxRegist5 = @textBoxRegist5 " +
                                ",textBoxRegist6 = @textBoxRegist6 " +
                                ",textBoxRegist7 = @textBoxRegist7 " +
                                ",textBoxRegist8 = @textBoxRegist8 " +
                                ",textBoxRegist9 = @textBoxRegist9 " +
                                ",textBoxRegist10 = @textBoxRegist10 " +
                                ",textBoxRegist11 = @textBoxRegist11 " +
                                ",textBoxRegist12 = @textBoxRegist12 " +
                                ",textBoxRegist13 = @textBoxRegist13 " +
                                ",textBoxRegist14 = @textBoxRegist14 " +
                                ",textBoxRegist15 = @textBoxRegist15 " +
                                ",textBoxRegist16 = @textBoxRegist16 " +
                                ",textBoxRegist17 = @textBoxRegist17 " +
                                ",textBoxRegist18 = @textBoxRegist18 " +
                                ",textBoxRegist19 = @textBoxRegist19 " +
                                ",textBoxRegist20 = @textBoxRegist20 " +
                                ",textBoxRegist21 = @textBoxRegist21 " +
                                ",textBoxRegist22 = @textBoxRegist22 " +
                                ",textBoxRegist23 = @textBoxRegist23 " +
                                ",textBoxRegist24 = @textBoxRegist24 " +
                                ",textBoxRegist25 = @textBoxRegist25 " +
                                ",textBoxRegist26 = @textBoxRegist26 " +
                                ",textBoxRegist27 = @textBoxRegist27 " +
                                ",textBoxRegist28 = @textBoxRegist28 " +
                                ",textBoxRegist29 = @textBoxRegist29 " +
                                ",textBoxRegist30 = @textBoxRegist30 " +
                                ",textBoxRegist31 = @textBoxRegist31 " +
                                ",textBoxRegist32 = @textBoxRegist32 " +
                                ",textBoxRegist33 = @textBoxRegist33 " +
                                ",textBoxRegist34 = @textBoxRegist34 " +
                                ",textBoxRegist35 = @textBoxRegist35 " +
                                ",textBoxRegist36 = @textBoxRegist36 " +
                                ",textBoxRegist37 = @textBoxRegist37 " +
                                ",textBoxRegist38 = @textBoxRegist38 " +
                                ",textBoxRegist39 = @textBoxRegist39 " +
                                ",textBoxRegist40 = @textBoxRegist40 " +
                                ",textBoxRegist41 = @textBoxRegist41 " +
                                ",textBoxRegist42 = @textBoxRegist42 " +
                                ",textBoxRegist43 = @textBoxRegist43 " +
                                ",textBoxRegist44 = @textBoxRegist44 " +
                                ",textBoxRegist45 = @textBoxRegist45 " +
                                ",textBoxRegist46 = @textBoxRegist46 " +
                                ",textBoxRegist47 = @textBoxRegist47 " +
                                ",textBoxRegist48 = @textBoxRegist48 " +
                                ",textBoxRegist49 = @textBoxRegist49 " +
                                ",textBoxRegist50 = @textBoxRegist50 " +
                                ",textBoxRegist51 = @textBoxRegist51 " +
                                ",textBoxRegist52 = @textBoxRegist52 " +
                                ",textBoxRegist53 = @textBoxRegist53 " +
                                ",textBoxRegist54 = @textBoxRegist54 " +
                                ",textBoxRegist55 = @textBoxRegist55 " +
                                ",textBoxRegist56 = @textBoxRegist56 " +
                                ",textBoxRegist57 = @textBoxRegist57 " +
                                ",textBoxRegist58 = @textBoxRegist58 " +
                                ",textBoxRegist59 = @textBoxRegist59 " +
                                ",textBoxRegist60 = @textBoxRegist60 " +
                                ",textBoxRegist61 = @textBoxRegist61 " +
                                ",textBoxRegist62 = @textBoxRegist62 " +
                                ",textBoxRegist63 = @textBoxRegist63 " +
                                ",textBoxRegist64 = @textBoxRegist64 " +
                                ",checkBoxRegister1 = @checkBoxRegister1 " +
                                ",checkBoxRegister2 = @checkBoxRegister2 " +
                                ",checkBoxRegister3 = @checkBoxRegister3 " +
                                ",checkBoxRegister4 = @checkBoxRegister4 " +
                                ",checkBoxRegister5 = @checkBoxRegister5 " +
                                ",checkBoxRegister6 = @checkBoxRegister6 " +
                                ",checkBoxRegister7 = @checkBoxRegister7 " +
                                ",checkBoxRegister8 = @checkBoxRegister8 " +
                                ",checkBoxRegister9 = @checkBoxRegister9 " +
                                ",checkBoxRegister10 = @checkBoxRegister10 " +
                                ",checkBoxRegister11 = @checkBoxRegister11 " +
                                ",checkBoxRegister12 = @checkBoxRegister12 " +
                                ",checkBoxRegister13 = @checkBoxRegister13 " +
                                ",checkBoxRegister14 = @checkBoxRegister14 " +
                                ",checkBoxRegister15 = @checkBoxRegister15 " +
                                ",checkBoxRegister16 = @checkBoxRegister16 " +
                                ",checkBoxRegister17 = @checkBoxRegister17 " +
                                ",checkBoxRegister18 = @checkBoxRegister18 " +
                                ",checkBoxRegister19 = @checkBoxRegister19 " +
                                ",checkBoxRegister20 = @checkBoxRegister20 " +
                                ",checkBoxRegister21 = @checkBoxRegister21 " +
                                ",checkBoxRegister22 = @checkBoxRegister22 " +
                                ",checkBoxRegister23 = @checkBoxRegister23 " +
                                ",checkBoxRegister24 = @checkBoxRegister24 " +
                                ",checkBoxRegister25 = @checkBoxRegister25 " +
                                ",checkBoxRegister26 = @checkBoxRegister26 " +
                                ",checkBoxRegister27 = @checkBoxRegister27 " +
                                ",checkBoxRegister28 = @checkBoxRegister28 " +
                                ",checkBoxRegister29 = @checkBoxRegister29 " +
                                ",checkBoxRegister30 = @checkBoxRegister30 " +
                                ",checkBoxRegister31 = @checkBoxRegister31 " +
                                ",checkBoxRegister32 = @checkBoxRegister32 " +
                                ",checkBoxRegister33 = @checkBoxRegister33 " +
                                ",checkBoxRegister34 = @checkBoxRegister34 " +
                                ",checkBoxRegister35 = @checkBoxRegister35 " +
                                ",checkBoxRegister36 = @checkBoxRegister36 " +
                                ",checkBoxRegister37 = @checkBoxRegister37 " +
                                ",checkBoxRegister38 = @checkBoxRegister38 " +
                                ",checkBoxRegister39 = @checkBoxRegister39 " +
                                ",checkBoxRegister40 = @checkBoxRegister40 " +
                                ",checkBoxRegister41 = @checkBoxRegister41 " +
                                ",checkBoxRegister42 = @checkBoxRegister42 " +
                                ",checkBoxRegister43 = @checkBoxRegister43 " +
                                ",checkBoxRegister44 = @checkBoxRegister44 " +
                                ",checkBoxRegister45 = @checkBoxRegister45 " +
                                ",checkBoxRegister46 = @checkBoxRegister46 " +
                                ",checkBoxRegister47 = @checkBoxRegister47 " +
                                ",checkBoxRegister48 = @checkBoxRegister48 " +
                                ",checkBoxRegister49 = @checkBoxRegister49 " +
                                ",checkBoxRegister50 = @checkBoxRegister50 " +
                                ",checkBoxRegister51 = @checkBoxRegister51 " +
                                ",checkBoxRegister52 = @checkBoxRegister52 " +
                                ",checkBoxRegister53 = @checkBoxRegister53 " +
                                ",checkBoxRegister54 = @checkBoxRegister54 " +
                                ",checkBoxRegister55 = @checkBoxRegister55 " +
                                ",checkBoxRegister56 = @checkBoxRegister56 " +
                                ",checkBoxRegister57 = @checkBoxRegister57 " +
                                ",checkBoxRegister58 = @checkBoxRegister58 " +
                                ",checkBoxRegister59 = @checkBoxRegister59 " +
                                ",checkBoxRegister60 = @checkBoxRegister60 " +
                                ",checkBoxRegister61 = @checkBoxRegister61 " +
                                ",checkBoxRegister62 = @checkBoxRegister62 " +
                                ",checkBoxRegister63 = @checkBoxRegister63 " +
                                ",checkBoxRegister64 = @checkBoxRegister64  Where ID = @ID";

            SQLiteCommand command = new SQLiteCommand(sql_update, m_dbConnection);

            command.Parameters.AddWithValue("@textBoxRegist1", modelData.textBoxRegist1);
            command.Parameters.AddWithValue("@textBoxRegist2", modelData.textBoxRegist2);
            command.Parameters.AddWithValue("@textBoxRegist3", modelData.textBoxRegist3);
            command.Parameters.AddWithValue("@textBoxRegist4", modelData.textBoxRegist4);
            command.Parameters.AddWithValue("@textBoxRegist5", modelData.textBoxRegist5);
            command.Parameters.AddWithValue("@textBoxRegist6", modelData.textBoxRegist6);
            command.Parameters.AddWithValue("@textBoxRegist7", modelData.textBoxRegist7);
            command.Parameters.AddWithValue("@textBoxRegist8", modelData.textBoxRegist8);
            command.Parameters.AddWithValue("@textBoxRegist9", modelData.textBoxRegist9);
            command.Parameters.AddWithValue("@textBoxRegist10", modelData.textBoxRegist10);
            command.Parameters.AddWithValue("@textBoxRegist11", modelData.textBoxRegist11);
            command.Parameters.AddWithValue("@textBoxRegist12", modelData.textBoxRegist12);
            command.Parameters.AddWithValue("@textBoxRegist13", modelData.textBoxRegist13);
            command.Parameters.AddWithValue("@textBoxRegist14", modelData.textBoxRegist14);
            command.Parameters.AddWithValue("@textBoxRegist15", modelData.textBoxRegist15);
            command.Parameters.AddWithValue("@textBoxRegist16", modelData.textBoxRegist16);
            command.Parameters.AddWithValue("@textBoxRegist17", modelData.textBoxRegist17);
            command.Parameters.AddWithValue("@textBoxRegist18", modelData.textBoxRegist18);
            command.Parameters.AddWithValue("@textBoxRegist19", modelData.textBoxRegist19);
            command.Parameters.AddWithValue("@textBoxRegist20", modelData.textBoxRegist20);
            command.Parameters.AddWithValue("@textBoxRegist21", modelData.textBoxRegist21);
            command.Parameters.AddWithValue("@textBoxRegist22", modelData.textBoxRegist22);
            command.Parameters.AddWithValue("@textBoxRegist23", modelData.textBoxRegist23);
            command.Parameters.AddWithValue("@textBoxRegist24", modelData.textBoxRegist24);
            command.Parameters.AddWithValue("@textBoxRegist25", modelData.textBoxRegist25);
            command.Parameters.AddWithValue("@textBoxRegist26", modelData.textBoxRegist26);
            command.Parameters.AddWithValue("@textBoxRegist27", modelData.textBoxRegist27);
            command.Parameters.AddWithValue("@textBoxRegist28", modelData.textBoxRegist28);
            command.Parameters.AddWithValue("@textBoxRegist29", modelData.textBoxRegist29);
            command.Parameters.AddWithValue("@textBoxRegist30", modelData.textBoxRegist30);
            command.Parameters.AddWithValue("@textBoxRegist31", modelData.textBoxRegist31);
            command.Parameters.AddWithValue("@textBoxRegist32", modelData.textBoxRegist32);
            command.Parameters.AddWithValue("@textBoxRegist33", modelData.textBoxRegist33);
            command.Parameters.AddWithValue("@textBoxRegist34", modelData.textBoxRegist34);
            command.Parameters.AddWithValue("@textBoxRegist35", modelData.textBoxRegist35);
            command.Parameters.AddWithValue("@textBoxRegist36", modelData.textBoxRegist36);
            command.Parameters.AddWithValue("@textBoxRegist37", modelData.textBoxRegist37);
            command.Parameters.AddWithValue("@textBoxRegist38", modelData.textBoxRegist38);
            command.Parameters.AddWithValue("@textBoxRegist39", modelData.textBoxRegist39);
            command.Parameters.AddWithValue("@textBoxRegist40", modelData.textBoxRegist40);
            command.Parameters.AddWithValue("@textBoxRegist41", modelData.textBoxRegist41);
            command.Parameters.AddWithValue("@textBoxRegist42", modelData.textBoxRegist42);
            command.Parameters.AddWithValue("@textBoxRegist43", modelData.textBoxRegist43);
            command.Parameters.AddWithValue("@textBoxRegist44", modelData.textBoxRegist44);
            command.Parameters.AddWithValue("@textBoxRegist45", modelData.textBoxRegist45);
            command.Parameters.AddWithValue("@textBoxRegist46", modelData.textBoxRegist46);
            command.Parameters.AddWithValue("@textBoxRegist47", modelData.textBoxRegist47);
            command.Parameters.AddWithValue("@textBoxRegist48", modelData.textBoxRegist48);
            command.Parameters.AddWithValue("@textBoxRegist49", modelData.textBoxRegist49);
            command.Parameters.AddWithValue("@textBoxRegist50", modelData.textBoxRegist50);
            command.Parameters.AddWithValue("@textBoxRegist51", modelData.textBoxRegist51);
            command.Parameters.AddWithValue("@textBoxRegist52", modelData.textBoxRegist52);
            command.Parameters.AddWithValue("@textBoxRegist53", modelData.textBoxRegist53);
            command.Parameters.AddWithValue("@textBoxRegist54", modelData.textBoxRegist54);
            command.Parameters.AddWithValue("@textBoxRegist55", modelData.textBoxRegist55);
            command.Parameters.AddWithValue("@textBoxRegist56", modelData.textBoxRegist56);
            command.Parameters.AddWithValue("@textBoxRegist57", modelData.textBoxRegist57);
            command.Parameters.AddWithValue("@textBoxRegist58", modelData.textBoxRegist58);
            command.Parameters.AddWithValue("@textBoxRegist59", modelData.textBoxRegist59);
            command.Parameters.AddWithValue("@textBoxRegist60", modelData.textBoxRegist60);
            command.Parameters.AddWithValue("@textBoxRegist61", modelData.textBoxRegist61);
            command.Parameters.AddWithValue("@textBoxRegist62", modelData.textBoxRegist62);
            command.Parameters.AddWithValue("@textBoxRegist63", modelData.textBoxRegist63);
            command.Parameters.AddWithValue("@textBoxRegist64", modelData.textBoxRegist64);

            command.Parameters.AddWithValue("@checkBoxRegister1", modelData.checkBoxRegister1);
            command.Parameters.AddWithValue("@checkBoxRegister2", modelData.checkBoxRegister2);
            command.Parameters.AddWithValue("@checkBoxRegister3", modelData.checkBoxRegister3);
            command.Parameters.AddWithValue("@checkBoxRegister4", modelData.checkBoxRegister4);
            command.Parameters.AddWithValue("@checkBoxRegister5", modelData.checkBoxRegister5);
            command.Parameters.AddWithValue("@checkBoxRegister6", modelData.checkBoxRegister6);
            command.Parameters.AddWithValue("@checkBoxRegister7", modelData.checkBoxRegister7);
            command.Parameters.AddWithValue("@checkBoxRegister8", modelData.checkBoxRegister8);
            command.Parameters.AddWithValue("@checkBoxRegister9", modelData.checkBoxRegister9);
            command.Parameters.AddWithValue("@checkBoxRegister10", modelData.checkBoxRegister10);
            command.Parameters.AddWithValue("@checkBoxRegister11", modelData.checkBoxRegister11);
            command.Parameters.AddWithValue("@checkBoxRegister12", modelData.checkBoxRegister12);
            command.Parameters.AddWithValue("@checkBoxRegister13", modelData.checkBoxRegister13);
            command.Parameters.AddWithValue("@checkBoxRegister14", modelData.checkBoxRegister14);
            command.Parameters.AddWithValue("@checkBoxRegister15", modelData.checkBoxRegister15);
            command.Parameters.AddWithValue("@checkBoxRegister16", modelData.checkBoxRegister16);
            command.Parameters.AddWithValue("@checkBoxRegister17", modelData.checkBoxRegister17);
            command.Parameters.AddWithValue("@checkBoxRegister18", modelData.checkBoxRegister18);
            command.Parameters.AddWithValue("@checkBoxRegister19", modelData.checkBoxRegister19);
            command.Parameters.AddWithValue("@checkBoxRegister20", modelData.checkBoxRegister20);
            command.Parameters.AddWithValue("@checkBoxRegister21", modelData.checkBoxRegister21);
            command.Parameters.AddWithValue("@checkBoxRegister22", modelData.checkBoxRegister22);
            command.Parameters.AddWithValue("@checkBoxRegister23", modelData.checkBoxRegister23);
            command.Parameters.AddWithValue("@checkBoxRegister24", modelData.checkBoxRegister24);
            command.Parameters.AddWithValue("@checkBoxRegister25", modelData.checkBoxRegister25);
            command.Parameters.AddWithValue("@checkBoxRegister26", modelData.checkBoxRegister26);
            command.Parameters.AddWithValue("@checkBoxRegister27", modelData.checkBoxRegister27);
            command.Parameters.AddWithValue("@checkBoxRegister28", modelData.checkBoxRegister28);
            command.Parameters.AddWithValue("@checkBoxRegister29", modelData.checkBoxRegister29);
            command.Parameters.AddWithValue("@checkBoxRegister30", modelData.checkBoxRegister30);
            command.Parameters.AddWithValue("@checkBoxRegister31", modelData.checkBoxRegister31);
            command.Parameters.AddWithValue("@checkBoxRegister32", modelData.checkBoxRegister32);
            command.Parameters.AddWithValue("@checkBoxRegister33", modelData.checkBoxRegister33);
            command.Parameters.AddWithValue("@checkBoxRegister34", modelData.checkBoxRegister34);
            command.Parameters.AddWithValue("@checkBoxRegister35", modelData.checkBoxRegister35);
            command.Parameters.AddWithValue("@checkBoxRegister36", modelData.checkBoxRegister36);
            command.Parameters.AddWithValue("@checkBoxRegister37", modelData.checkBoxRegister37);
            command.Parameters.AddWithValue("@checkBoxRegister38", modelData.checkBoxRegister38);
            command.Parameters.AddWithValue("@checkBoxRegister39", modelData.checkBoxRegister39);
            command.Parameters.AddWithValue("@checkBoxRegister40", modelData.checkBoxRegister40);
            command.Parameters.AddWithValue("@checkBoxRegister41", modelData.checkBoxRegister41);
            command.Parameters.AddWithValue("@checkBoxRegister42", modelData.checkBoxRegister42);
            command.Parameters.AddWithValue("@checkBoxRegister43", modelData.checkBoxRegister43);
            command.Parameters.AddWithValue("@checkBoxRegister44", modelData.checkBoxRegister44);
            command.Parameters.AddWithValue("@checkBoxRegister45", modelData.checkBoxRegister45);
            command.Parameters.AddWithValue("@checkBoxRegister46", modelData.checkBoxRegister46);
            command.Parameters.AddWithValue("@checkBoxRegister47", modelData.checkBoxRegister47);
            command.Parameters.AddWithValue("@checkBoxRegister48", modelData.checkBoxRegister48);
            command.Parameters.AddWithValue("@checkBoxRegister49", modelData.checkBoxRegister49);
            command.Parameters.AddWithValue("@checkBoxRegister50", modelData.checkBoxRegister50);
            command.Parameters.AddWithValue("@checkBoxRegister51", modelData.checkBoxRegister51);
            command.Parameters.AddWithValue("@checkBoxRegister52", modelData.checkBoxRegister52);
            command.Parameters.AddWithValue("@checkBoxRegister53", modelData.checkBoxRegister53);
            command.Parameters.AddWithValue("@checkBoxRegister54", modelData.checkBoxRegister54);
            command.Parameters.AddWithValue("@checkBoxRegister55", modelData.checkBoxRegister55);
            command.Parameters.AddWithValue("@checkBoxRegister56", modelData.checkBoxRegister56);
            command.Parameters.AddWithValue("@checkBoxRegister57", modelData.checkBoxRegister57);
            command.Parameters.AddWithValue("@checkBoxRegister58", modelData.checkBoxRegister58);
            command.Parameters.AddWithValue("@checkBoxRegister59", modelData.checkBoxRegister59);
            command.Parameters.AddWithValue("@checkBoxRegister60", modelData.checkBoxRegister60);
            command.Parameters.AddWithValue("@checkBoxRegister61", modelData.checkBoxRegister61);
            command.Parameters.AddWithValue("@checkBoxRegister62", modelData.checkBoxRegister62);
            command.Parameters.AddWithValue("@checkBoxRegister63", modelData.checkBoxRegister63);
            command.Parameters.AddWithValue("@checkBoxRegister64", modelData.checkBoxRegister64);

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

        public void getSettingData(SQLiteConnection m_dbConnection, DataModel dataModel)
        {
            SQLiteCommand cmd = new SQLiteCommand("select * From setting_table  where 1 ", m_dbConnection);
            SQLiteDataReader Sdr = cmd.ExecuteReader();
            while (Sdr.Read())
            {
                dataModel.textBoxRegist1 = Sdr["textBoxRegist1"].ToString();
                dataModel.textBoxRegist2 = Sdr["textBoxRegist2"].ToString();
                dataModel.textBoxRegist3 = Sdr["textBoxRegist3"].ToString();
                dataModel.textBoxRegist4 = Sdr["textBoxRegist4"].ToString();
                dataModel.textBoxRegist5 = Sdr["textBoxRegist5"].ToString();
                dataModel.textBoxRegist6 = Sdr["textBoxRegist6"].ToString();
                dataModel.textBoxRegist7 = Sdr["textBoxRegist7"].ToString();
                dataModel.textBoxRegist8 = Sdr["textBoxRegist8"].ToString();
                dataModel.textBoxRegist9 = Sdr["textBoxRegist9"].ToString();
                dataModel.textBoxRegist10 = Sdr["textBoxRegist10"].ToString();
                dataModel.textBoxRegist11 = Sdr["textBoxRegist11"].ToString();
                dataModel.textBoxRegist12 = Sdr["textBoxRegist12"].ToString();
                dataModel.textBoxRegist13 = Sdr["textBoxRegist13"].ToString();
                dataModel.textBoxRegist14 = Sdr["textBoxRegist14"].ToString();
                dataModel.textBoxRegist15 = Sdr["textBoxRegist15"].ToString();
                dataModel.textBoxRegist16 = Sdr["textBoxRegist16"].ToString();
                dataModel.textBoxRegist17 = Sdr["textBoxRegist17"].ToString();
                dataModel.textBoxRegist18 = Sdr["textBoxRegist18"].ToString();
                dataModel.textBoxRegist19 = Sdr["textBoxRegist19"].ToString();
                dataModel.textBoxRegist20 = Sdr["textBoxRegist20"].ToString();
                dataModel.textBoxRegist21 = Sdr["textBoxRegist21"].ToString();
                dataModel.textBoxRegist22 = Sdr["textBoxRegist22"].ToString();
                dataModel.textBoxRegist23 = Sdr["textBoxRegist23"].ToString();
                dataModel.textBoxRegist24 = Sdr["textBoxRegist24"].ToString();
                dataModel.textBoxRegist25 = Sdr["textBoxRegist25"].ToString();
                dataModel.textBoxRegist26 = Sdr["textBoxRegist26"].ToString();
                dataModel.textBoxRegist27 = Sdr["textBoxRegist27"].ToString(); 
                dataModel.textBoxRegist28 = Sdr["textBoxRegist28"].ToString();
                dataModel.textBoxRegist29 = Sdr["textBoxRegist29"].ToString();
                dataModel.textBoxRegist30 = Sdr["textBoxRegist30"].ToString();
                dataModel.textBoxRegist31 = Sdr["textBoxRegist31"].ToString();
                dataModel.textBoxRegist32 = Sdr["textBoxRegist32"].ToString();
                dataModel.textBoxRegist33 = Sdr["textBoxRegist33"].ToString();
                dataModel.textBoxRegist34 = Sdr["textBoxRegist34"].ToString();
                dataModel.textBoxRegist35 = Sdr["textBoxRegist35"].ToString();
                dataModel.textBoxRegist36 = Sdr["textBoxRegist36"].ToString();
                dataModel.textBoxRegist37 = Sdr["textBoxRegist37"].ToString();
                dataModel.textBoxRegist38 = Sdr["textBoxRegist38"].ToString();
                dataModel.textBoxRegist39 = Sdr["textBoxRegist39"].ToString();
                dataModel.textBoxRegist40 = Sdr["textBoxRegist40"].ToString();
                dataModel.textBoxRegist41 = Sdr["textBoxRegist41"].ToString();
                dataModel.textBoxRegist42 = Sdr["textBoxRegist42"].ToString();
                dataModel.textBoxRegist43 = Sdr["textBoxRegist43"].ToString();
                dataModel.textBoxRegist44 = Sdr["textBoxRegist44"].ToString();
                dataModel.textBoxRegist45 = Sdr["textBoxRegist45"].ToString();
                dataModel.textBoxRegist46 = Sdr["textBoxRegist46"].ToString();
                dataModel.textBoxRegist47 = Sdr["textBoxRegist47"].ToString();
                dataModel.textBoxRegist48 = Sdr["textBoxRegist48"].ToString();
                dataModel.textBoxRegist49 = Sdr["textBoxRegist49"].ToString();
                dataModel.textBoxRegist50 = Sdr["textBoxRegist50"].ToString();
                dataModel.textBoxRegist51 = Sdr["textBoxRegist51"].ToString();
                dataModel.textBoxRegist52 = Sdr["textBoxRegist52"].ToString();
                dataModel.textBoxRegist53 = Sdr["textBoxRegist53"].ToString();
                dataModel.textBoxRegist54 = Sdr["textBoxRegist54"].ToString();
                dataModel.textBoxRegist55 = Sdr["textBoxRegist55"].ToString();
                dataModel.textBoxRegist56 = Sdr["textBoxRegist56"].ToString();
                dataModel.textBoxRegist57 = Sdr["textBoxRegist57"].ToString();
                dataModel.textBoxRegist58 = Sdr["textBoxRegist58"].ToString();
                dataModel.textBoxRegist59 = Sdr["textBoxRegist59"].ToString();
                dataModel.textBoxRegist60 = Sdr["textBoxRegist60"].ToString();
                dataModel.textBoxRegist61 = Sdr["textBoxRegist61"].ToString();
                dataModel.textBoxRegist62 = Sdr["textBoxRegist62"].ToString();
                dataModel.textBoxRegist63 = Sdr["textBoxRegist63"].ToString();
                dataModel.textBoxRegist64 = Sdr["textBoxRegist64"].ToString();

                dataModel.checkBoxRegister1 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister1"].ToString())));
                dataModel.checkBoxRegister2 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister2"].ToString())));
                dataModel.checkBoxRegister3 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister3"].ToString())));
                dataModel.checkBoxRegister4 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister4"].ToString())));
                dataModel.checkBoxRegister5 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister5"].ToString())));
                dataModel.checkBoxRegister6 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister6"].ToString())));
                dataModel.checkBoxRegister7 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister7"].ToString())));
                dataModel.checkBoxRegister8 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister8"].ToString())));
                dataModel.checkBoxRegister9 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister9"].ToString())));
                dataModel.checkBoxRegister10 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister10"].ToString())));
                dataModel.checkBoxRegister11 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister11"].ToString())));
                dataModel.checkBoxRegister12 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister12"].ToString())));
                dataModel.checkBoxRegister13 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister13"].ToString())));
                dataModel.checkBoxRegister14 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister14"].ToString())));
                dataModel.checkBoxRegister15 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister15"].ToString())));
                dataModel.checkBoxRegister16 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister16"].ToString())));
                dataModel.checkBoxRegister17 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister17"].ToString())));
                dataModel.checkBoxRegister18 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister18"].ToString())));
                dataModel.checkBoxRegister19 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister19"].ToString())));
                dataModel.checkBoxRegister20 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister20"].ToString())));
                dataModel.checkBoxRegister21 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister21"].ToString())));
                dataModel.checkBoxRegister22 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister22"].ToString())));
                dataModel.checkBoxRegister23 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister23"].ToString())));
                dataModel.checkBoxRegister24 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister24"].ToString())));
                dataModel.checkBoxRegister25 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister25"].ToString())));
                dataModel.checkBoxRegister26 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister26"].ToString())));
                dataModel.checkBoxRegister27 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister27"].ToString())));
                dataModel.checkBoxRegister28 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister28"].ToString())));
                dataModel.checkBoxRegister29 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister29"].ToString())));
                dataModel.checkBoxRegister30 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister30"].ToString())));
                dataModel.checkBoxRegister31 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister31"].ToString())));
                dataModel.checkBoxRegister32 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister32"].ToString())));
                dataModel.checkBoxRegister33 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister33"].ToString())));
                dataModel.checkBoxRegister34 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister34"].ToString())));
                dataModel.checkBoxRegister35 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister35"].ToString())));
                dataModel.checkBoxRegister36 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister36"].ToString())));
                dataModel.checkBoxRegister37 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister37"].ToString())));
                dataModel.checkBoxRegister38 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister38"].ToString())));
                dataModel.checkBoxRegister39 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister39"].ToString())));
                dataModel.checkBoxRegister40 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister40"].ToString())));
                dataModel.checkBoxRegister41 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister41"].ToString())));
                dataModel.checkBoxRegister42 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister42"].ToString())));
                dataModel.checkBoxRegister43 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister43"].ToString())));
                dataModel.checkBoxRegister44 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister44"].ToString())));
                dataModel.checkBoxRegister45 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister45"].ToString())));
                dataModel.checkBoxRegister45 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister45"].ToString())));
                dataModel.checkBoxRegister46 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister46"].ToString())));
                dataModel.checkBoxRegister47 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister47"].ToString())));
                dataModel.checkBoxRegister48 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister48"].ToString())));
                dataModel.checkBoxRegister49 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister49"].ToString())));
                dataModel.checkBoxRegister50 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister50"].ToString())));
                dataModel.checkBoxRegister51 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister51"].ToString())));
                dataModel.checkBoxRegister52 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister52"].ToString())));
                dataModel.checkBoxRegister53 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister53"].ToString())));
                dataModel.checkBoxRegister54 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister54"].ToString())));
                dataModel.checkBoxRegister55 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister55"].ToString())));
                dataModel.checkBoxRegister56 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister56"].ToString())));
                dataModel.checkBoxRegister57 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister57"].ToString())));
                dataModel.checkBoxRegister58 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister58"].ToString())));
                dataModel.checkBoxRegister59 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister59"].ToString())));
                dataModel.checkBoxRegister60 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister60"].ToString())));
                dataModel.checkBoxRegister61 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister61"].ToString())));
                dataModel.checkBoxRegister62 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister62"].ToString())));
                dataModel.checkBoxRegister63 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister63"].ToString())));
                dataModel.checkBoxRegister64 = Convert.ToBoolean(Convert.ToInt16((Sdr["checkBoxRegister64"].ToString())));



            }
            Sdr.Close();
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
