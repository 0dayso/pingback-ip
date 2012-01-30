using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleControls
{
    public partial class MainMenu : MenuStrip
    {
        public MainMenu()
        {
            InitializeComponent();
            this.mn1Reconnect.Click += new System.EventHandler(this.SubItemClicked);
            this.mn1ModifyPassword.Click += new System.EventHandler(this.SubItemClicked);
            this.mn1ViewLog.Click += new System.EventHandler(this.SubItemClicked);
            this.mn1ViewLogToday.Click += new System.EventHandler(this.SubItemClicked);
            this.mn1Balance.Click += new System.EventHandler(this.SubItemClicked);
            this.mn1Command.Click += new System.EventHandler(this.SubItemClicked);
            this.mn1Exit.Click += new System.EventHandler(this.SubItemClicked);
            this.mn2Receipt.Click += new System.EventHandler(this.SubItemClicked);


            mn4BlackWindowMax.Click += new EventHandler(SubItemClicked);
            mn4BlackWindowNormal.Click += new EventHandler(SubItemClicked);
            mn4RightPanelHide.Click += new EventHandler(SubItemClicked);
            mn4RightPanelShow.Click += new EventHandler(SubItemClicked);
            mn4ScreenClear.Click += new EventHandler(SubItemClicked);
            mn4ShortCutKey.Click += new EventHandler(SubItemClicked);
            mn4BlackWindowColorFont.Click += new EventHandler(SubItemClicked);
            mn5About.Click += new EventHandler(SubItemClicked);
            mn5Calculator.Click += new EventHandler(SubItemClicked);
            mn5Help.Click += new EventHandler(SubItemClicked);
            mn5NotePad.Click += new EventHandler(SubItemClicked);
            mn5Paint.Click += new EventHandler(SubItemClicked);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            // 调用基类 OnPaint
            base.OnPaint(pe);
        }
        protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        {
            base.OnItemClicked(e);
        }
        void SubItemClicked(object sender, System.EventArgs e)
        {
            ToolStripItemClickedEventArgs ex = new ToolStripItemClickedEventArgs((ToolStripItem)sender);
            base.OnItemClicked(ex);
            
        }
    }
}
