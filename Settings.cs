﻿using System;
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
        public Settings()
        {
            InitializeComponent();

          //  SetPlaceHolder(textBoxRegist1, " Логин ");
           // SetPlaceHolder(textBoxRegist2, " Пароль ");

        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void applyButton_Click(object sender, EventArgs e)
        {
          
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
