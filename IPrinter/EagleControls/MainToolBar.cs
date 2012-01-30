using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleControls
{
    public partial class MainToolBar : ToolStrip
    {
        public MainToolBar()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            // 调用基类 OnPaint
            base.OnPaint(pe);
        }
        EagleString.LoginResult m_lr;
        public void SetDropDownMenu(EagleString.LoginResult lr, string ipid)
        {
            m_lr = lr;
            SetDropDownMenu(mnSwitchVisableConfig2,lr, ipid);
        }
        public void SetDropDownMenu(ToolStripDropDownButton menu,EagleString.LoginResult lr, string ipid)
        {
            try
            {
                if (!EagleString.BaseFunc.OfficeValidate(lr.m_ls_office[lr.indexof(ipid)].OFFICE_NO))
                    throw new Exception("请联系易格客服修改OFFICE号(工作号:为6位OFFICE号)");
                menu.Text = lr.m_ls_office[lr.indexof(ipid)].OFFICE_NO.ToUpper() + "-" + ipid;

                List<string> lsTemp = new List<string>();
                for (int i = 0; i < lr.m_ls_office.Count; i++)
                {
                    if (lr.IpidSameServer(new string[] { ipid, lr.m_ls_office[i].IP_ID.ToString() }))
                    {
                        string office = lr.m_ls_office[i].OFFICE_NO.ToUpper();
                        if (!EagleString.BaseFunc.OfficeValidate(office))
                            throw new Exception("请联系易格客服修改OFFICE号(工作号:为6位OFFICE号)");
                        menu.DropDownItems.Add
                            (office + "-" + lr.m_ls_office[i].IP_ID.ToString()
                            , null
                            , new EventHandler(sc));
                        if (!lsTemp.Contains(office)) lsTemp.Add(office);
                    }
                }

                //menu.DropDownItems.Add("-");
                lsTemp.Sort();
                for (int i = 0; i < lsTemp.Count; i++)
                {
                    //menu.DropDownItems.Add(lsTemp[i] + "-" + "集群");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
        public delegate void switchconfig(string[] ips);
        public switchconfig sw;
        void sc(object sender, EventArgs e)
        {
            string s = (sender as ToolStripDropDownItem).Text;
            string office = s.Split('-')[0];
            string ipid = s.Split('-')[1];
            switch (ipid)
            {
                case "集群":
                    List<int> ipids = m_lr.SameConfigs(office);
                    //sw.Invoke(ipids.ToArray());
                    break;
                default:
                    //EagleString.Imported.SendMessage                            (0xFFFF, (int)EagleString.Imported.egMsg.SWITCH_CONFIG, int.Parse(ipid), 0);
                    sw.Invoke(new string[] { ipid });
                    break;
            }
        }
    }
}
