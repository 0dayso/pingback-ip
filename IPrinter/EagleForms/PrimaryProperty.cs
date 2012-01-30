using System;
using System.Collections.Generic;
using System.Text;

namespace EagleForms
{
    public partial class Primary
    {
        /// <summary>
        /// 是否显示中文界面标签
        /// </summary>
        public bool PropertyShowTabPageEasyVersion
        {
            get
            {
                return tcMain.TabPages.Contains(tpEasy);
            }
            set
            {
                if (value)
                {
                    if (!tcMain.TabPages.Contains(tpEasy))
                    {
                        tcMain.TabPages.Add(tpEasy);
                    }
                }
                else
                {
                    if (tcMain.TabPages.Contains(tpEasy)) tcMain.TabPages.Remove(tpEasy);
                }
            }
        }
        /// <summary>
        /// 是否显示后台管理标签
        /// </summary>
        public bool PropertyShowTabpageManager
        {
            get
            {
                return tcMain.TabPages.Contains(tpManager);
            }
            set
            {
                if (value)
                {
                    if (!tcMain.TabPages.Contains(tpManager))
                    {
                        tcMain.TabPages.Add(tpManager);
                    }
                }
                else
                {
                    if (tcMain.TabPages.Contains(tpManager)) tcMain.TabPages.Remove(tpManager);
                }
            }
        }

        /// <summary>
        /// 是否显示对帐平台标签
        /// </summary>
        public bool PropertyShowTabpageFinance
        {
            get
            {
                return tcMain.TabPages.Contains(tpFinance);
            }
            set
            {
                if (value)
                {
                    if (!tcMain.TabPages.Contains(tpFinance))
                    {
                        tcMain.TabPages.Add(tpFinance);
                    }
                }
                else
                {
                    if (tcMain.TabPages.Contains(tpFinance)) tcMain.TabPages.Remove(tpFinance);
                }
            }
        }
        /// <summary>
        /// 仅显示黑屏
        /// </summary>
        public void PropertyShowOnlyBlackWindow()
        {
            blackWindow.Dock = System.Windows.Forms.DockStyle.Fill;
        }
    }
}
