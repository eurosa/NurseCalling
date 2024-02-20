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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NurseCalling
{
    public partial class Settings : Form
    {
        dbHandler dbHandlr;
        DataModel modelData;
        SQLiteConnection qLiteConnection;
        string backImagePath = null;
        string twoImagePath = null;
        public Settings(DataModel dataModel, dbHandler dbHandr, SQLiteConnection sQLite )
        {
            InitializeComponent();
            modelData = dataModel;
            dbHandlr = dbHandr;
            qLiteConnection = sQLite;

            dbHandlr.getSettingData(qLiteConnection,modelData);


            try
            {
                tsuPorts();

            }
            catch (Exception ex) { }


            dbHandlr.getGeneralData(qLiteConnection, modelData);

            dbHandr.getImage(qLiteConnection, modelData);


            checkBoxHub1.Checked = modelData.checkBoxHub1;
            checkBoxHub2.Checked = modelData.checkBoxHub2;
            checkBoxHub3.Checked = modelData.checkBoxHub3;
            checkBoxHub4.Checked = modelData.checkBoxHub4;

            portBox1.Text = modelData.comport;

            /* SetPlaceHolder(textBoxRegist1, "Hub 1");
             SetPlaceHolder(textBoxRegist2, "Hub 2");
             SetPlaceHolder(textBoxRegist3, "Hub 3");
             SetPlaceHolder(textBoxRegist4, "Hub 4");
             SetPlaceHolder(textBoxRegist5, "Hub 5");
             SetPlaceHolder(textBoxRegist6, "Hub 6");
             SetPlaceHolder(textBoxRegist7, "Hub 7");
             SetPlaceHolder(textBoxRegist8, "Hub 8");
             SetPlaceHolder(textBoxRegist9, "Hub 9");
             SetPlaceHolder(textBoxRegist10, "Hub 10");
             SetPlaceHolder(textBoxRegist11, "Hub 11");
             SetPlaceHolder(textBoxRegist12, "Hub 12");
             SetPlaceHolder(textBoxRegist13, "Hub 13");
             SetPlaceHolder(textBoxRegist14, "Hub 14");
             SetPlaceHolder(textBoxRegist15, "Hub 15");
             SetPlaceHolder(textBoxRegist16, "Hub 16");
             SetPlaceHolder(textBoxRegist17, "Hub 17");
             SetPlaceHolder(textBoxRegist18, "Hub 18");
             SetPlaceHolder(textBoxRegist19, "Hub 19");
             SetPlaceHolder(textBoxRegist20, "Hub 20");
             SetPlaceHolder(textBoxRegist21, "Hub 21");
             SetPlaceHolder(textBoxRegist22, "Hub 22");
             SetPlaceHolder(textBoxRegist23, "Hub 23");
             SetPlaceHolder(textBoxRegist24, "Hub 24");
             SetPlaceHolder(textBoxRegist25, "Hub 25");
             SetPlaceHolder(textBoxRegist26, "Hub 26");
             SetPlaceHolder(textBoxRegist27, "Hub 27");
             SetPlaceHolder(textBoxRegist28, "Hub 28");
             SetPlaceHolder(textBoxRegist29, "Hub 29");
             SetPlaceHolder(textBoxRegist30, "Hub 30");
             SetPlaceHolder(textBoxRegist31, "Hub 31");
             SetPlaceHolder(textBoxRegist32, "Hub 32");
             SetPlaceHolder(textBoxRegist33, "Hub 33");
             SetPlaceHolder(textBoxRegist34, "Hub 34");
             SetPlaceHolder(textBoxRegist35, "Hub 35");
             SetPlaceHolder(textBoxRegist36, "Hub 36");
             SetPlaceHolder(textBoxRegist37, "Hub 37");
             SetPlaceHolder(textBoxRegist38, "Hub 38");
             SetPlaceHolder(textBoxRegist39, "Hub 39");
             SetPlaceHolder(textBoxRegist40, "Hub 40");
             SetPlaceHolder(textBoxRegist41, "Hub 41");
             SetPlaceHolder(textBoxRegist42, "Hub 42");
             SetPlaceHolder(textBoxRegist43, "Hub 43");
             SetPlaceHolder(textBoxRegist44, "Hub 44");
             SetPlaceHolder(textBoxRegist45, "Hub 45");
             SetPlaceHolder(textBoxRegist46, "Hub 46");
             SetPlaceHolder(textBoxRegist47, "Hub 47");
             SetPlaceHolder(textBoxRegist48, "Hub 48");
             SetPlaceHolder(textBoxRegist49, "Hub 49");
             SetPlaceHolder(textBoxRegist50, "Hub 50");
             SetPlaceHolder(textBoxRegist51, "Hub 51");
             SetPlaceHolder(textBoxRegist52, "Hub 52");
             SetPlaceHolder(textBoxRegist53, "Hub 53");
             SetPlaceHolder(textBoxRegist54, "Hub 54");
             SetPlaceHolder(textBoxRegist55, "Hub 55");
             SetPlaceHolder(textBoxRegist56, "Hub 56");
             SetPlaceHolder(textBoxRegist57, "Hub 57");
             SetPlaceHolder(textBoxRegist58, "Hub 58");
             SetPlaceHolder(textBoxRegist59, "Hub 59");
             SetPlaceHolder(textBoxRegist60, "Hub 60");
             SetPlaceHolder(textBoxRegist61, "Hub 61");
             SetPlaceHolder(textBoxRegist62, "Hub 62");
             SetPlaceHolder(textBoxRegist63, "Hub 63");
             SetPlaceHolder(textBoxRegist64, "Hub 64");*/

            textBoxRegist1.Text = modelData.textBoxRegist1;
            textBoxRegist2.Text = modelData.textBoxRegist2;
            textBoxRegist3.Text = modelData.textBoxRegist3;
            textBoxRegist4.Text = modelData.textBoxRegist4;
            textBoxRegist5.Text = modelData.textBoxRegist5;
            textBoxRegist6.Text = modelData.textBoxRegist6;
            textBoxRegist7.Text = modelData.textBoxRegist7;
            textBoxRegist8.Text = modelData.textBoxRegist8;
            textBoxRegist9.Text = modelData.textBoxRegist9;
            textBoxRegist10.Text = modelData.textBoxRegist10;
            textBoxRegist11.Text = modelData.textBoxRegist11;
            textBoxRegist12.Text = modelData.textBoxRegist12;
            textBoxRegist13.Text = modelData.textBoxRegist13;
            textBoxRegist14.Text = modelData.textBoxRegist14;
            textBoxRegist15.Text = modelData.textBoxRegist15;
            textBoxRegist16.Text = modelData.textBoxRegist16;
            textBoxRegist17.Text = modelData.textBoxRegist17;
            textBoxRegist18.Text = modelData.textBoxRegist18;
            textBoxRegist19.Text = modelData.textBoxRegist19;
            textBoxRegist20.Text = modelData.textBoxRegist20;
            textBoxRegist21.Text = modelData.textBoxRegist21;
            textBoxRegist22.Text = modelData.textBoxRegist22;
            textBoxRegist23.Text = modelData.textBoxRegist23;
            textBoxRegist24.Text = modelData.textBoxRegist24;
            textBoxRegist25.Text = modelData.textBoxRegist25;
            textBoxRegist26.Text = modelData.textBoxRegist26;
            textBoxRegist27.Text = modelData.textBoxRegist27;
            textBoxRegist28.Text = modelData.textBoxRegist28;
            textBoxRegist29.Text = modelData.textBoxRegist29;
            textBoxRegist30.Text = modelData.textBoxRegist30;
            textBoxRegist31.Text = modelData.textBoxRegist31;
            textBoxRegist32.Text = modelData.textBoxRegist32;
            textBoxRegist33.Text = modelData.textBoxRegist33;
            textBoxRegist34.Text = modelData.textBoxRegist34;
            textBoxRegist35.Text = modelData.textBoxRegist35;
            textBoxRegist36.Text = modelData.textBoxRegist36;
            textBoxRegist37.Text = modelData.textBoxRegist37;
            textBoxRegist38.Text = modelData.textBoxRegist38;
            textBoxRegist39.Text = modelData.textBoxRegist39;
            textBoxRegist40.Text = modelData.textBoxRegist40;
            textBoxRegist41.Text = modelData.textBoxRegist41;
            textBoxRegist42.Text = modelData.textBoxRegist42;
            textBoxRegist43.Text = modelData.textBoxRegist43;
            textBoxRegist44.Text = modelData.textBoxRegist44;
            textBoxRegist45.Text = modelData.textBoxRegist45;
            textBoxRegist46.Text = modelData.textBoxRegist46;
            textBoxRegist47.Text = modelData.textBoxRegist47;
            textBoxRegist48.Text = modelData.textBoxRegist48;
            textBoxRegist49.Text = modelData.textBoxRegist49;
            textBoxRegist50.Text = modelData.textBoxRegist50;
            textBoxRegist51.Text = modelData.textBoxRegist51;
            textBoxRegist52.Text = modelData.textBoxRegist52;
            textBoxRegist53.Text = modelData.textBoxRegist53;
            textBoxRegist54.Text = modelData.textBoxRegist54;
            textBoxRegist55.Text = modelData.textBoxRegist55;
            textBoxRegist56.Text = modelData.textBoxRegist56;
            textBoxRegist57.Text = modelData.textBoxRegist57;
            textBoxRegist58.Text = modelData.textBoxRegist58;
            textBoxRegist59.Text = modelData.textBoxRegist59;
            textBoxRegist60.Text = modelData.textBoxRegist60;
            textBoxRegist61.Text = modelData.textBoxRegist61;
            textBoxRegist62.Text = modelData.textBoxRegist62;
            textBoxRegist63.Text = modelData.textBoxRegist63;
            textBoxRegist64.Text = modelData.textBoxRegist64;

            checkBoxRegister1.Checked = modelData.checkBoxRegister1;
            checkBoxRegister2.Checked = modelData.checkBoxRegister2;
            checkBoxRegister3.Checked = modelData.checkBoxRegister3;
            checkBoxRegister4.Checked = modelData.checkBoxRegister4;
            checkBoxRegister5.Checked = modelData.checkBoxRegister5;
            checkBoxRegister6.Checked = modelData.checkBoxRegister6;
            checkBoxRegister7.Checked = modelData.checkBoxRegister7;
            checkBoxRegister8.Checked = modelData.checkBoxRegister8;
            checkBoxRegister9.Checked = modelData.checkBoxRegister9;
            checkBoxRegister10.Checked = modelData.checkBoxRegister10;
            checkBoxRegister11.Checked = modelData.checkBoxRegister11;
            checkBoxRegister12.Checked = modelData.checkBoxRegister12;
            checkBoxRegister13.Checked = modelData.checkBoxRegister13;
            checkBoxRegister14.Checked = modelData.checkBoxRegister14;
            checkBoxRegister15.Checked = modelData.checkBoxRegister15;
            checkBoxRegister16.Checked = modelData.checkBoxRegister16;
            checkBoxRegister17.Checked = modelData.checkBoxRegister17;
            checkBoxRegister18.Checked = modelData.checkBoxRegister18;
            checkBoxRegister19.Checked = modelData.checkBoxRegister19;
            checkBoxRegister20.Checked = modelData.checkBoxRegister20;
            checkBoxRegister21.Checked = modelData.checkBoxRegister21;
            checkBoxRegister22.Checked = modelData.checkBoxRegister22;
            checkBoxRegister23.Checked = modelData.checkBoxRegister23;
            checkBoxRegister24.Checked = modelData.checkBoxRegister24;
            checkBoxRegister25.Checked = modelData.checkBoxRegister25;
            checkBoxRegister26.Checked = modelData.checkBoxRegister26;
            checkBoxRegister27.Checked = modelData.checkBoxRegister27;
            checkBoxRegister28.Checked = modelData.checkBoxRegister28;
            checkBoxRegister29.Checked = modelData.checkBoxRegister29;
            checkBoxRegister30.Checked = modelData.checkBoxRegister30;
            checkBoxRegister31.Checked = modelData.checkBoxRegister31;
            checkBoxRegister32.Checked = modelData.checkBoxRegister32;
            checkBoxRegister33.Checked = modelData.checkBoxRegister33;
            checkBoxRegister34.Checked = modelData.checkBoxRegister34;
            checkBoxRegister35.Checked = modelData.checkBoxRegister35;
            checkBoxRegister36.Checked = modelData.checkBoxRegister36;
            checkBoxRegister37.Checked = modelData.checkBoxRegister37;
            checkBoxRegister38.Checked = modelData.checkBoxRegister38;
            checkBoxRegister39.Checked = modelData.checkBoxRegister39;
            checkBoxRegister40.Checked = modelData.checkBoxRegister40;
            checkBoxRegister41.Checked = modelData.checkBoxRegister41;
            checkBoxRegister42.Checked = modelData.checkBoxRegister42;
            checkBoxRegister43.Checked = modelData.checkBoxRegister43;
            checkBoxRegister44.Checked = modelData.checkBoxRegister44;
            checkBoxRegister45.Checked = modelData.checkBoxRegister45;
            checkBoxRegister46.Checked = modelData.checkBoxRegister46;
            checkBoxRegister47.Checked = modelData.checkBoxRegister47;
            checkBoxRegister48.Checked = modelData.checkBoxRegister48;
            checkBoxRegister49.Checked = modelData.checkBoxRegister49;
            checkBoxRegister50.Checked = modelData.checkBoxRegister50;
            checkBoxRegister51.Checked = modelData.checkBoxRegister51;
            checkBoxRegister52.Checked = modelData.checkBoxRegister52;
            checkBoxRegister53.Checked = modelData.checkBoxRegister53;
            checkBoxRegister54.Checked = modelData.checkBoxRegister54;
            checkBoxRegister55.Checked = modelData.checkBoxRegister55;
            checkBoxRegister56.Checked = modelData.checkBoxRegister56;
            checkBoxRegister57.Checked = modelData.checkBoxRegister57;
            checkBoxRegister58.Checked = modelData.checkBoxRegister58;
            checkBoxRegister59.Checked = modelData.checkBoxRegister59;
            checkBoxRegister60.Checked = modelData.checkBoxRegister60;
            checkBoxRegister61.Checked = modelData.checkBoxRegister61;
            checkBoxRegister62.Checked = modelData.checkBoxRegister62;
            checkBoxRegister63.Checked = modelData.checkBoxRegister63;
            checkBoxRegister64.Checked = modelData.checkBoxRegister64;

            // MessageBox.Show(modelData.textBoxRegist64);

            if (!string.IsNullOrEmpty(dataModel.bed_image))
            {
                try
                {
                    buttonChoose.BackgroundImage = Image.FromFile(@dataModel.bed_image);
                }
                catch (Exception ex) { }
            }

            if (!string.IsNullOrEmpty(dataModel.toilet_image))
            {
                try
                {
                    buttonTwoImage.BackgroundImage = Image.FromFile(@dataModel.toilet_image);
                }
                catch (Exception ex) { }
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            modelData.textBoxRegist1 = textBoxRegist1.Text;
            modelData.textBoxRegist2 = textBoxRegist2.Text;
            modelData.textBoxRegist3 = textBoxRegist3.Text;
            modelData.textBoxRegist4 = textBoxRegist4.Text;
            modelData.textBoxRegist5 = textBoxRegist5.Text;
            modelData.textBoxRegist6 = textBoxRegist6.Text;
            modelData.textBoxRegist7 = textBoxRegist7.Text;
            modelData.textBoxRegist8 = textBoxRegist8.Text;
            modelData.textBoxRegist9 = textBoxRegist9.Text;
            modelData.textBoxRegist10 = textBoxRegist10.Text;
            modelData.textBoxRegist11 = textBoxRegist11.Text;
            modelData.textBoxRegist12 = textBoxRegist12.Text;
            modelData.textBoxRegist13 = textBoxRegist13.Text;
            modelData.textBoxRegist14 = textBoxRegist14.Text;
            modelData.textBoxRegist15 = textBoxRegist15.Text;
            modelData.textBoxRegist16 = textBoxRegist16.Text;
            modelData.textBoxRegist17 = textBoxRegist17.Text;
            modelData.textBoxRegist18 = textBoxRegist18.Text;
            modelData.textBoxRegist19 = textBoxRegist19.Text;
            modelData.textBoxRegist20 = textBoxRegist20.Text;
            modelData.textBoxRegist21 = textBoxRegist21.Text;
            modelData.textBoxRegist22 = textBoxRegist22.Text;
            modelData.textBoxRegist23 = textBoxRegist23.Text;
            modelData.textBoxRegist24 = textBoxRegist24.Text;
            modelData.textBoxRegist25 = textBoxRegist25.Text;
            modelData.textBoxRegist26 = textBoxRegist26.Text;
            modelData.textBoxRegist27 = textBoxRegist27.Text;
            modelData.textBoxRegist28 = textBoxRegist28.Text;
            modelData.textBoxRegist29 = textBoxRegist29.Text;
            modelData.textBoxRegist30= textBoxRegist30.Text;
            modelData.textBoxRegist31 = textBoxRegist31.Text;
            modelData.textBoxRegist32 = textBoxRegist32.Text; 
            modelData.textBoxRegist33 = textBoxRegist33.Text;
            modelData.textBoxRegist34 = textBoxRegist34.Text;
            modelData.textBoxRegist35 = textBoxRegist35.Text;
            modelData.textBoxRegist36 = textBoxRegist36.Text;
            modelData.textBoxRegist37 = textBoxRegist37.Text;
            modelData.textBoxRegist38 = textBoxRegist38.Text;
            modelData.textBoxRegist39 = textBoxRegist39.Text;
            modelData.textBoxRegist40 = textBoxRegist40.Text;
            modelData.textBoxRegist41 = textBoxRegist41.Text;
            modelData.textBoxRegist42 = textBoxRegist42.Text;
            modelData.textBoxRegist43 = textBoxRegist43.Text;
            modelData.textBoxRegist44 = textBoxRegist44.Text;
            modelData.textBoxRegist45 = textBoxRegist45.Text;
            modelData.textBoxRegist46 = textBoxRegist46.Text;
            modelData.textBoxRegist47 = textBoxRegist47.Text;
            modelData.textBoxRegist48 = textBoxRegist48.Text;
            modelData.textBoxRegist49 = textBoxRegist49.Text;
            modelData.textBoxRegist50 = textBoxRegist50.Text;
            modelData.textBoxRegist51 = textBoxRegist51.Text;
            modelData.textBoxRegist52 = textBoxRegist52.Text;
            modelData.textBoxRegist53 = textBoxRegist53.Text;
            modelData.textBoxRegist54 = textBoxRegist54.Text;
            modelData.textBoxRegist55 = textBoxRegist55.Text;
            modelData.textBoxRegist56 = textBoxRegist56.Text;
            modelData.textBoxRegist57 = textBoxRegist57.Text;
            modelData.textBoxRegist58 = textBoxRegist58.Text;
            modelData.textBoxRegist59 = textBoxRegist59.Text;
            modelData.textBoxRegist60 = textBoxRegist60.Text;
            modelData.textBoxRegist61 = textBoxRegist61.Text;
            modelData.textBoxRegist62 = textBoxRegist62.Text;
            modelData.textBoxRegist63 = textBoxRegist63.Text;
            modelData.textBoxRegist64 = textBoxRegist64.Text;

            modelData.checkBoxRegister1 = checkBoxRegister1.Checked;
            modelData.checkBoxRegister2 = checkBoxRegister2.Checked;
            modelData.checkBoxRegister3 = checkBoxRegister3.Checked;
            modelData.checkBoxRegister4 = checkBoxRegister4.Checked;
            modelData.checkBoxRegister5 = checkBoxRegister5.Checked;
            modelData.checkBoxRegister6 = checkBoxRegister6.Checked;
            modelData.checkBoxRegister7 = checkBoxRegister7.Checked;
            modelData.checkBoxRegister8 = checkBoxRegister8.Checked;
            modelData.checkBoxRegister9 = checkBoxRegister9.Checked;
            modelData.checkBoxRegister10 = checkBoxRegister10.Checked;
            modelData.checkBoxRegister11 = checkBoxRegister11.Checked;
            modelData.checkBoxRegister12 = checkBoxRegister12.Checked;
            modelData.checkBoxRegister13 = checkBoxRegister13.Checked;
            modelData.checkBoxRegister14 = checkBoxRegister14.Checked;
            modelData.checkBoxRegister15 = checkBoxRegister15.Checked;
            modelData.checkBoxRegister16 = checkBoxRegister16.Checked;
            modelData.checkBoxRegister17 = checkBoxRegister17.Checked;
            modelData.checkBoxRegister18 = checkBoxRegister18.Checked;
            modelData.checkBoxRegister19 = checkBoxRegister19.Checked;
            modelData.checkBoxRegister20 = checkBoxRegister20.Checked;
            modelData.checkBoxRegister21 = checkBoxRegister21.Checked;
            modelData.checkBoxRegister22 = checkBoxRegister22.Checked;
            modelData.checkBoxRegister23 = checkBoxRegister23.Checked;
            modelData.checkBoxRegister24 = checkBoxRegister24.Checked;
            modelData.checkBoxRegister25 = checkBoxRegister25.Checked;
            modelData.checkBoxRegister26 = checkBoxRegister26.Checked;
            modelData.checkBoxRegister27 = checkBoxRegister27.Checked;
            modelData.checkBoxRegister28 = checkBoxRegister28.Checked;
            modelData.checkBoxRegister29 = checkBoxRegister29.Checked;
            modelData.checkBoxRegister30 = checkBoxRegister30.Checked;
            modelData.checkBoxRegister31 = checkBoxRegister31.Checked;
            modelData.checkBoxRegister32 = checkBoxRegister32.Checked;
            modelData.checkBoxRegister33 = checkBoxRegister33.Checked;
            modelData.checkBoxRegister34 = checkBoxRegister34.Checked;
            modelData.checkBoxRegister35 = checkBoxRegister35.Checked;
            modelData.checkBoxRegister36 = checkBoxRegister36.Checked;
            modelData.checkBoxRegister37 = checkBoxRegister37.Checked;
            modelData.checkBoxRegister38 = checkBoxRegister38.Checked;
            modelData.checkBoxRegister39 = checkBoxRegister39.Checked;
            modelData.checkBoxRegister40 = checkBoxRegister40.Checked;
            modelData.checkBoxRegister41 = checkBoxRegister41.Checked;
            modelData.checkBoxRegister42 = checkBoxRegister42.Checked;
            modelData.checkBoxRegister43 = checkBoxRegister43.Checked;
            modelData.checkBoxRegister44 = checkBoxRegister44.Checked;
            modelData.checkBoxRegister45 = checkBoxRegister45.Checked;
            modelData.checkBoxRegister46 = checkBoxRegister46.Checked;
            modelData.checkBoxRegister47 = checkBoxRegister47.Checked;
            modelData.checkBoxRegister48 = checkBoxRegister48.Checked;
            modelData.checkBoxRegister49 = checkBoxRegister49.Checked;
            modelData.checkBoxRegister50 = checkBoxRegister50.Checked;
            modelData.checkBoxRegister51 = checkBoxRegister51.Checked;
            modelData.checkBoxRegister52 = checkBoxRegister52.Checked;
            modelData.checkBoxRegister53 = checkBoxRegister53.Checked;
            modelData.checkBoxRegister54 = checkBoxRegister54.Checked;
            modelData.checkBoxRegister55 = checkBoxRegister55.Checked;
            modelData.checkBoxRegister56 = checkBoxRegister56.Checked;
            modelData.checkBoxRegister57 = checkBoxRegister57.Checked;
            modelData.checkBoxRegister58 = checkBoxRegister58.Checked;
            modelData.checkBoxRegister59 = checkBoxRegister59.Checked;
            modelData.checkBoxRegister60 = checkBoxRegister60.Checked;
            modelData.checkBoxRegister61 = checkBoxRegister61.Checked;
            modelData.checkBoxRegister62 = checkBoxRegister62.Checked;
            modelData.checkBoxRegister63 = checkBoxRegister63.Checked;
            modelData.checkBoxRegister64 = checkBoxRegister64.Checked; 


            dbHandlr.update_setting_table_data(qLiteConnection, modelData);


            updateImage();
        }

        public void updateImage() {

            if (!string.IsNullOrEmpty(backImagePath))
            {
                modelData.bed_image = backImagePath;
            }


            if (!string.IsNullOrEmpty(modelData.bed_image))
            {

                dbHandlr.bedImage(qLiteConnection, modelData);
                try
                {
                    buttonChoose.BackgroundImage = Image.FromFile(modelData.bed_image);
                }
                catch (Exception ex) { }

            }

            if (!string.IsNullOrEmpty(twoImagePath))
            {
                modelData.toilet_image = twoImagePath;
            }


            if (!string.IsNullOrEmpty(modelData.toilet_image))
            {

                dbHandlr.toiletImage(qLiteConnection, modelData);
                try
                {
                    buttonTwoImage.BackgroundImage = Image.FromFile(modelData.toilet_image);
                }
                catch (Exception ex) { }

            }

        }

        private void textBoxRegist1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBoxRegist2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist14_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist17_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist18_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist19_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist20_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist21_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist22_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist23_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist24_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist25_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist26_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist27_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist28_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist29_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist30_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist31_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist32_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist33_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist34_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist35_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist36_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist37_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist38_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist39_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist40_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist41_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist42_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist43_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist44_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist45_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist46_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist47_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist48_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist49_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist50_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist51_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist52_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist53_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist54_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist55_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist56_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist57_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist58_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist59_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist60_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist61_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist62_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist63_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxRegist64_TextChanged(object sender, EventArgs e)
        {

        }

        public void SetPlaceHolder(Control control, string PlaceHolderText)
        {
            control.Text = PlaceHolderText;
            control.GotFocus += delegate (object sender, EventArgs args) {
                if (control.Text == PlaceHolderText)
                {
                    control.Text = "";
                }
            };
            control.LostFocus += delegate (object sender, EventArgs args) {
                if (control.Text.Length == 0)
                {
                    control.Text = PlaceHolderText;
                }
            };
        }

        private void applyButton_Click_1(object sender, EventArgs e)
        {

        }

        private void checkBox31_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister1_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("CheckBox1 "+checkBoxRegister1.Checked);
        }

        private void checkBoxRegister2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister10_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister11_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister12_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister13_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister14_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister15_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister16_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister17_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister64_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister18_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister19_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister20_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister21_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister22_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister23_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister24_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister25_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister26_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister27_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister28_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister29_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister30_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister32_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister33_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister34_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister35_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister36_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister37_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister38_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister39_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister40_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister41_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister42_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister43_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister44_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister45_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister46_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister47_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister48_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister49_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister50_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister51_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister52_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister53_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister54_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister55_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister56_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister57_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister58_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister59_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister60_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister61_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister62_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRegister63_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {
            try
            {
                //Create a new instance of openFileDialog
                OpenFileDialog res = new OpenFileDialog();

                //Filter
                res.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";

                //When the user select the file
                if (res.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //Get the file's path
                        backImagePath = res.FileName;
                        // dbHandlr.upDateBackGroundImage();
                        Debug.WriteLine("Background Image: " + backImagePath);
                        // Do something
                        buttonChoose.BackgroundImage = Image.FromFile(@backImagePath);
                    }
                    catch (Exception ex) { }

                }
             
            }
            catch (Exception ex) { }
        }

        private void buttonTwoImage_Click(object sender, EventArgs e)
        {
            try
            {
                //Create a new instance of openFileDialog
                OpenFileDialog res = new OpenFileDialog();

                //Filter
                res.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";

                //When the user select the file
                if (res.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //Get the file's path
                        twoImagePath = res.FileName;
                        // dbHandlr.upDateBackGroundImage();
                        Debug.WriteLine("Background Image: " + twoImagePath);
                        // Do something
                        buttonTwoImage.BackgroundImage = Image.FromFile(twoImagePath);
                    }
                    catch (Exception ex) { }

                }

            }
            catch (Exception ex) { }
        }
        public void updateLogoImage(SQLiteConnection m_dbConnection, DataModel modelData)
        {

            string sql_update = "UPDATE image_table SET bed_image = @bed_image,toilet_image = @toilet_image Where ID = @ID";

            SQLiteCommand command = new SQLiteCommand(sql_update, m_dbConnection);

            command.Parameters.AddWithValue("@bed_image", modelData.bed_image);
            command.Parameters.AddWithValue("@toilet_image", modelData.toilet_image);
            //  command.Parameters.AddWithValue("@logo_enable_disable", modelData.logo_enable_disable);
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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Console.WriteLine("my_value "+ checkBoxHub1.Checked+" "+ checkBoxHub2.Checked+" "+ checkBoxHub3.Checked+" "+checkBoxHub4.Checked);
            modelData.checkBoxHub1 = checkBoxHub1.Checked;
            modelData.checkBoxHub2 = checkBoxHub2.Checked;
            modelData.checkBoxHub3 = checkBoxHub3.Checked;
            modelData.checkBoxHub4 = checkBoxHub4.Checked; 
            modelData.comport = portBox1.Text;
            dbHandlr.UpdateComport(modelData,qLiteConnection);
        }

        



        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                tsuPorts();
              
            }
            catch (Exception ex) { }
        }

        private void tsuPorts()
        {
            try
            {
                // Retrieve the list of all COM ports on your Computer
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    if (!portBox1.Items.Contains(port))
                    {
                        portBox1.Items.Add(port);
                    }
                }
            }
            catch (Exception ex) { }
        }
    }
}
