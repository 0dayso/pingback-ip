using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleControls
{
    public partial class MainStatusBar : Panel
    {

        Color colorY = Color.Lime;
        Color colorN = Color.Red;
        Button btnProfit = new StatusButton();
        Button btnGroup = new StatusButton();
        Button btnSpecTick = new StatusButton();
        Button btnUseIbe = new StatusButton();
        public MainStatusBar()
        {
            InitializeComponent();
            get_attributes_from_eagle_txt();
            this.Controls.Add(btnProfit);
            btnProfit.Text = "返点";
            btnProfit.Click += new EventHandler(btnProfit_Click);
            this.Controls.Add(btnGroup);
            btnGroup.Text = "散拼";
            btnGroup.Click += new EventHandler(btnGroup_Click);
            this.Controls.Add(btnSpecTick);
            btnSpecTick.Text = "特舱";
            btnSpecTick.Click += new EventHandler(btnSpecTick_Click);
            this.Controls.Add(btnUseIbe);
            btnUseIbe.Text = "IBE";
            btnUseIbe.Click += new EventHandler(btnUseIbe_Click);

            SetControlColor();
        }
        void SetControlColor()
        {
            btnProfit.BackColor = (m_show_profit ? colorY : colorN);
            btnGroup.BackColor = (m_show_group ? colorY : colorN);
            btnSpecTick.BackColor = (m_show_spectick ? colorY : colorN);
            btnUseIbe.BackColor = (m_use_ibe ? colorY : colorN);
        }
        void btnUseIbe_Click(object sender, EventArgs e)
        {
            USE_IBE = !USE_IBE;
        }

        void btnSpecTick_Click(object sender, EventArgs e)
        {
            SHOW_SPECTICK = !SHOW_SPECTICK;
        }

        void btnGroup_Click(object sender, EventArgs e)
        {
            SHOW_GROUP = !SHOW_GROUP;
        }

        void btnProfit_Click(object sender, EventArgs e)
        {
            SHOW_PROFIT = !SHOW_PROFIT;
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            btnProfit.Location = new Point(0, 0);
            btnProfit.Size = new Size(20, this.Height);
            btnGroup.Location = new Point(btnProfit.Location.X + btnProfit.Width, 0);
            btnGroup.Size = new Size(20, this.Height);
            btnSpecTick.Location = new Point(btnGroup.Location.X + btnGroup.Width, 0);
            btnSpecTick.Size = new Size(20, this.Height);
            btnUseIbe.Location = new Point(btnSpecTick.Location.X + btnSpecTick.Width, 0);
            btnUseIbe.Size = new Size(20, this.Height);

        }

        public bool SHOW_PROFIT
        {
            get { return m_show_profit; }

            set
            {
                m_show_profit = value;
                set_attributes_to_eagle_txt();
                SetControlColor();
            }
        }
        public bool SHOW_GROUP { get { return m_show_group; }
            set { m_show_group = value; set_attributes_to_eagle_txt(); SetControlColor(); }
        }
        public bool SHOW_SPECTICK { get { return m_show_spectick; }
            set { m_show_spectick = value; set_attributes_to_eagle_txt(); SetControlColor(); }
        }
        public bool USE_IBE { get { return m_use_ibe; }
            set { m_use_ibe = value; set_attributes_to_eagle_txt(); SetControlColor(); }
        }

        private bool m_show_profit = true;
        private bool m_show_group = true;
        private bool m_show_spectick =true;
        private bool m_use_ibe = false;
        readonly string str_profit = "STATUS_BAR_SHOW_PROFIT";
        readonly string str_group = "STATUS_BAR_SHOW_GROUP";
        readonly string str_spectick = "STATUS_BAR_SHOW_SPECTICK";
        readonly string str_use_ibe = "STATUS_BAR_USE_IBE";
        private void get_attributes_from_eagle_txt()
        {
            try
            {
                m_show_profit = (EagleString.EagleFileIO.ValueOf(str_profit) == "1");
                m_show_group = (EagleString.EagleFileIO.ValueOf(str_group) == "1");
                m_show_spectick = (EagleString.EagleFileIO.ValueOf(str_spectick) == "1");
                m_use_ibe = (EagleString.EagleFileIO.ValueOf(str_use_ibe) == "1");

            }
            catch
            {
                set_attributes_to_eagle_txt();
            }
            //
            //TODO:置控件
            //
        }
        private void set_attributes_to_eagle_txt()
        {
            //
            //TODO:从控件中取值
            //
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            ht.Add(str_group, m_show_group ? "1" : "0");
            ht.Add(str_profit, m_show_profit ? "1" : "0");
            ht.Add(str_spectick, m_show_spectick ? "1" : "0");
            ht.Add(str_use_ibe, m_use_ibe ? "1" : "0");
            EagleString.EagleFileIO.WiteHashTableToEagleDotTxt(ht);
        }
    }
    public class StatusButton : Button
    {
        public StatusButton()
        {
            this.Margin = new Padding (0) ;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.Cursor = Cursors.Cross;
            this.FlatStyle = FlatStyle.Popup;
            Font = new Font(Font.Name, 8F);
            this.Tag = "9999";
        }
    }
}
