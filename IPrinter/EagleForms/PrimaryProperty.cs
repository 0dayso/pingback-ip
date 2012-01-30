using System;
using System.Collections.Generic;
using System.Text;

namespace EagleForms
{
    public partial class Primary
    {
        /// <summary>
        /// �Ƿ���ʾ���Ľ����ǩ
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
        /// �Ƿ���ʾ��̨�����ǩ
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
        /// �Ƿ���ʾ����ƽ̨��ǩ
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
        /// ����ʾ����
        /// </summary>
        public void PropertyShowOnlyBlackWindow()
        {
            blackWindow.Dock = System.Windows.Forms.DockStyle.Fill;
        }
    }
}
