using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleControls
{
    public partial class BlackWindowAttri : Form
    {
        public Font FONT
        {
            get
            {
                return m_font;
            }
        }
        public Color FORECOLOR
        {
            get
            {
                return m_fore;
            }
        }
        public Color BACKCOLOR
        {
            get
            {
                return m_back;
            }
        }
        public Color IMPORTANT { get { return m_important; } }
        Font m_font;
        Color m_fore;
        Color m_back;
        Color m_important;
        public BlackWindowAttri(Font font,Color fore,Color back,Color important)
        {
            InitializeComponent();
            m_font = font;
            m_fore = fore;
            m_back = back;
            m_important = important;
            setbuttontext();
        }

        void setbuttontext()
        {
            btnFont.Text = m_font.Name + "," +m_font.Size.ToString() +","+ m_font.Bold.ToString();
            btnForeColor.BackColor = m_fore;
            btnBackColor.BackColor = m_back;
            btnImportantColor.BackColor = m_important;
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            dlg.Font = m_font;
            
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_font = dlg.Font;
                setbuttontext();
            }
        }

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_fore = dlg.Color;
                setbuttontext();
            }
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_back = dlg.Color;
                setbuttontext();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnImportantColor_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_important = dlg.Color;
                setbuttontext();
            }
        }
    }
}