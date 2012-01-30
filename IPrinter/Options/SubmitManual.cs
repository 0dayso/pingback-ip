using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Options
{
    public partial class SubmitManual : Form
    {
        public SubmitManual()
        {
            InitializeComponent();
        }
        public string Pnr = "";
        public string etNumber = "";
        public string FlightNumber1 = "";
        public string Bunk1 = "";
        public string FlightNumber2 = "";
        public string Bunk2 = "";
        public string CityPair1 = "";
        public string CityPair2 = "";
        public string Date1 = "";
        public string Date2 = "";
        public string TotalFC = "";
        public string State = "0";
        public string DecFeeState = "1";
        public string Passengers = "";

        public string TotalFare = "";//暂时不用
        public string TotalTaxBuild = "";
        public string TotalTaxFuel = "";
        public string TerminalNumber = "";//使用IP的ID

        private void button1_Click(object sender, EventArgs e)
        {
            long ibeg = long.Parse(textBox2.Text.Trim());
            long iend = long.Parse(textBox3.Text.Trim());
            for (long i = ibeg; i <= iend; i++)
            {
                etNumber += textBox1.Text.Trim() + " " + i.ToString("D10") + ";";
            }
            FlightNumber1 = textBox4.Text.Trim();
            Bunk1 = textBox5.Text.Trim();
            CityPair1 = textBox6.Text.Trim();
            Date1 = textBox7.Text.Trim();
            FlightNumber2 = textBox8.Text.Trim();
            Bunk2 = textBox9.Text.Trim();
            CityPair2 = textBox10.Text.Trim();
            Date2 = textBox11.Text.Trim();

            Pnr = textBox12.Text.Trim();
            TotalFC = textBox13.Text.Trim();
            Passengers = textBox14.Text.Trim();
            TotalTaxBuild = textBox15.Text.Trim();
            TotalTaxFuel = textBox16.Text.Trim();
            

        }
    }
}