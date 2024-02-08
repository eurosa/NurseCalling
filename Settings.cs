using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NurseCalling
{
    public partial class Settings : Form
    {
        DataModel modelData;
        public Settings(DataModel dataModel)
        {
            InitializeComponent();
            modelData = dataModel; 
            SetPlaceHolder(textBoxRegist1, "Hub 1");
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
            SetPlaceHolder(textBoxRegist64, "Hub 64");

        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void applyButton_Click(object sender, EventArgs e)
        {
          
        }

        private void textBoxRegist1_TextChanged(object sender, EventArgs e)
        {
            modelData.textBoxRegist1 = (string)textBoxRegist1.Text;
        }

        private void textBoxRegist2_TextChanged(object sender, EventArgs e)
        {
            modelData.textBoxRegist2 = (string)textBoxRegist2.Text;
        }

        private void textBoxRegist3_TextChanged(object sender, EventArgs e)
        {
            modelData.textBoxRegist3 = (string)textBoxRegist3.Text;
        }

        private void textBoxRegist4_TextChanged(object sender, EventArgs e)
        {
            modelData.textBoxRegist4 = (string)textBoxRegist4.Text;
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
    }
}
