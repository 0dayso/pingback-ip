using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleControls
{
    public partial class LV_Lowest : SortListView
    {
        public LV_Lowest()
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

        private void LV_Lowest_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            // TOTO:在此处添加自定义双击组件代码

            base.OnMouseDoubleClick(e);
        }


        private void LV_Lowest_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.SelectedIndices.Count == 0) return;
            int i = this.SelectedIndices[0];
            {
                string al = this.Items[i].SubItems[1].Text.Substring(0,2);
                string bunk = "ALL";
                string tgq = EagleString.EagleFileIO.TGQ_RULE(al,bunk);
                toolTip1.SetToolTip(this, tgq);
            }
            
        }

        private void LV_Lowest_MouseHover(object sender, EventArgs e)
        {
            
        }

    }
}
