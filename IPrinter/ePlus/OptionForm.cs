using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus
{
    public partial class OptionForm : Form
    {
        public OptionForm()
        {
            InitializeComponent();

            Properties.Settings settings = new ePlus.Properties.Settings();

            lblNoticeBackColor.BackColor = settings.NoticeBackColor;
            lblNoticeForeColor.BackColor = settings.NoticeForeColor;
            lblNoticeFontSize.Text = GetFontName(settings.NoticeFont);

            lblWorkAreaBackColor.BackColor = settings.EditorBackColor;
            lblWorkAreaForeColor.BackColor = settings.EditorForeColor;
            lblWorkAreaFontSize.Text = GetFontName(settings.EditorFont);

            XMLConfig.XMLSettings xml = XMLConfig.Operation.GetSettingsCTI();
            nudUdpPort.Value = xml.CtiUdpPort;
            txtB2CURL.Text = xml.B2CURL;

            switch (xml.CtiType)
            {
                case XMLConfig.CtiTypeEnum.EGPlug:
                    this.rbEGPlug.Checked = true;
                    break;
                case XMLConfig.CtiTypeEnum.EGSwitch:
                    this.rbEGSwitch.Checked = true;
                    break;
                case XMLConfig.CtiTypeEnum.EGUSB:
                    this.rbUSB.Checked = true;
                    break;
                case XMLConfig.CtiTypeEnum.EGMMPBX:
                    this.rbMMPBX.Checked = true;
                    break;
            }
        }

        private void lblNoticeForeColor_DoubleClick(object sender, EventArgs e)
        {
            colorDialog.Color = (sender as Label).BackColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                (sender as Label).BackColor = colorDialog.Color;    
            }
        }

        private void lblNoticeFontSize_DoubleClick(object sender, EventArgs e)
        {
            fontDialog.Font = SetFontName((sender as Label).Text);

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                (sender as Label).Text = GetFontName(fontDialog.Font); 
            }
        }

        public string GetFontName(Font font)
        {
            string [] strings = font.ToString().Substring(6).Split(',');

            return strings[0].Split('=')[1] + "," + strings[1].Split('=')[1]; 
        }

        public Font SetFontName(string font)
        {
            return new Font(font.Split(',')[0], float.Parse(font.Split(',')[1]));  
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings settings = new ePlus.Properties.Settings();

            settings.NoticeBackColor = lblNoticeBackColor.BackColor;
            settings.NoticeForeColor = lblNoticeForeColor.BackColor;
            settings.NoticeFont = SetFontName(lblNoticeFontSize.Text);

            settings.EditorBackColor = lblWorkAreaBackColor.BackColor;
            settings.EditorForeColor = lblWorkAreaForeColor.BackColor;
            settings.EditorFont = SetFontName(lblWorkAreaFontSize.Text);

            settings.Save();

            XMLConfig.XMLSettings xml = XMLConfig.Operation.GetSettingsCTI();

            if (rbEGSwitch.Checked)
                xml.CtiType = XMLConfig.CtiTypeEnum.EGSwitch;
            else if (rbEGPlug.Checked)
                xml.CtiType = XMLConfig.CtiTypeEnum.EGPlug;
            else if (rbUSB.Checked)
                xml.CtiType = XMLConfig.CtiTypeEnum.EGUSB;
            else if (rbMMPBX.Checked)
                xml.CtiType = XMLConfig.CtiTypeEnum.EGMMPBX;

            xml.CtiUdpPort = Convert.ToInt32(nudUdpPort.Value);
            xml.B2CURL = txtB2CURL.Text.Trim();
            XMLConfig.Operation.SaveSettingsCTI(xml);

            Close();
        }
    }
}