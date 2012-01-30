using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleControls
{
    public partial class LV_GroupList : ListView
    {
        public LV_GroupList()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            // 调用基类 OnPaint
            base.OnPaint(pe);
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            
            base.OnMouseDoubleClick(e);
        }

        private void LV_GroupList_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.SelectedIndices.Count == 0) return;
            try
            {
                string tip = "";
                ListViewItem lvi= this.SelectedItems[0];
                int count = lvi.SubItems.Count;
                for (int i = 0; i < count; ++i)
                {
                    string head = this.Columns[i].Text;
                    tip += head.PadRight(8, ' ');
                    tip += " : ";
                    tip += lvi.SubItems[i].Text;
                    tip += "\r\n";
                }
                toolTip1.SetToolTip(this, tip);
            }
            catch
            {
            }
        }
    }
}
