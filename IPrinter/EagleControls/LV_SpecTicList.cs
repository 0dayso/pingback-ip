using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleControls
{
    public partial class LV_SpecTicList : SortListView
    {
        public LV_SpecTicList()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        public LV_SpecTicList(bool bFixBunk)
        {
            InitializeComponent();
            if (bFixBunk)
            {
                this.ch_rebate.Width = 60;
                this.ch_remark.Width = 100;
            }
            else
            {
                this.ch_rebate.Width = 0;
                this.ch_price.Width = 60;
                this.ch_bunk.Width = 0;
                this.ch_rebate.Text = "最低有座价";
                this.ch_remark.Width = 160;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            // 调用基类 OnPaint
            base.OnPaint(pe);
        }
    }
}
