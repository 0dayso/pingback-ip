using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus.BookSimple
{
    public partial class AddPassenger : Form
    {


        public AddPassenger()
        {
            InitializeComponent();
        }
        public string fromto = "";
        public string promote = "";
        public string groupid = "";
        public string total = "";
        public string booked = "";
        private bool m_bSpeckTickFlag = false;
        public DateTime date = new DateTime();
        public string flight = "";
        public bool bSpecTickFlag
        {
            get
            {
                return m_bSpeckTickFlag;
                
            }
            set
            {
                m_bSpeckTickFlag = value;
                if (value)
                {
                    this.btAdd.Text = "申请";
                    this.label1.Text = "舱位";
                }
                else
                {
                    this.comboBox1.Visible = false;
                }
            }
        }
        public void CombboxSet(bool bFix, string al, char bunk, int dec)
        {
            if (m_bSpeckTickFlag)
            {
                if (bFix)
                {
                    comboBox1.Items.Clear();
                    if (!EagleString.egString.LargeThan420(date))
                    {
                        comboBox1.Items.Add(bunk.ToString() + "舱-" + EagleString.EagleFileIO.RebateOf(bunk, al) + "折");
                    }
                    else
                    {
                        comboBox1.Items.Add(bunk.ToString() + "舱");
                    }
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    comboBox1.Items.Clear();
                    string bunks = EagleString.EagleFileIO.BunksOf(bunk, al, dec);

                    
                    int[] rebates = EagleString.EagleFileIO.RebatesOf(bunk, al, dec);

                    if (EagleString.egString.LargeThan420(date))//对420作了修改
                    {
                        bunks = EagleString.EagleFileIO.BunksOfNew(bunk, al, dec);//420
                        rebates = EagleString.EagleFileIO.RebatesOfNew(bunk, al, dec);
                    }
                    for (int i = 0; i < bunks.Length; ++i)
                    {
                        if (!EagleString.egString.LargeThan420(date))
                        {
                            comboBox1.Items.Add(bunks[i].ToString() + "舱-" + rebates[i].ToString() + "折");
                        }
                        else
                        {
                            comboBox1.Items.Add(bunks[i].ToString() + "舱");
                        }
                    }
                    comboBox1.SelectedIndex = 0;
                }
            }
        }
        private void AddPassenger_Load(object sender, EventArgs e)
        {
            this.label1.Text = promote;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (!m_bSpeckTickFlag)
            {
                if (lb_姓名.Items.Count == 0)
                {
                    MessageBox.Show("抱歉：请正确输入", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                for (int i = 0; i < lb_姓名.Items.Count; i++)
                {
                    ePlus.CSAddToGroup ag = new ePlus.CSAddToGroup();
                    ag.groupid = groupid;
                    ag.name = lb_姓名.Items[i].ToString();
                    ag.cardid = lb_CardNo.Items[i].ToString();
                    if (ag.addtogroup())
                    {
                        //MessageBox.Show("入团成功！");
                    }
                    else
                    {
                        MessageBox.Show("警告：" + ag.name + "入团失败！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                MessageBox.Show("恭喜，入团完毕！", "CONGRATUATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                string wsaddr = GlobalVar.WebServer;
                string username = GlobalVar.loginName;
                int dataid = Convert.ToInt32(groupid);
                DateTime flightdate = date;
                char bunk = comboBox1.Text[0];
                int count = lb_姓名.Items.Count;
                string pnr = "";                     //here should be pnr
                string[] psgers = new string[count];
                lb_姓名.Items.CopyTo(psgers, 0);
                string[] cardnos = new string[count];
                lb_CardNo.Items.CopyTo(cardnos, 0);
                string[] phones = new string[count];
                for(int i=0;i<count;++i)phones[i]= "";
                bool bFlag = false;
                if (bunk < 'A' || bunk > 'Z')
                {
                    //不产生PNR，直接申请
                }
                else
                {
                    //生成PNR并置pnr
                    //fromto = fromto.Replace("SHA", "PVG");
                    EagleExtension.EagleExtension.CreatePnrFromIbe(
                       new string[] { flight },
                       new DateTime[] { flightdate },
                       new string[] { fromto.Substring(0, 3) },
                       new string[] { fromto.Substring(3) },
                       new char[] { bunk },
                       psgers,
                       cardnos,
                       new string[] { "TEST" },
                       ref pnr);
                    if (!EagleString.BaseFunc.PnrValidate(pnr))
                    {
                        MessageBox.Show("生成PNR失败！请重试" + pnr);
                        return;
                    }
                }
                string [] passport=null;
                EagleExtension.EagleExtension.SpecTickRequest
                    (wsaddr, username, dataid, flightdate, bunk, count, pnr, psgers, cardnos, phones, ref bFlag, ref passport);
                if (bFlag)
                {
                    string promopt = "";
                    if (pnr != "")
                    {
                        promopt = "并为您生成的PNR为:" + pnr + "(请牢记)";
                    }
                    if (passport != null)
                    {
                        EagleProtocal.PACKET_PROMOPT_NEW_APPLY ep =
                            new EagleProtocal.PACKET_PROMOPT_NEW_APPLY(EagleProtocal.EagleProtocal.MsgNo++, passport);
                        EagleAPI.EagleSendBytes(ep.ToBytes());
                        MessageBox.Show("已发出申请！" + promopt);
                    }
                    else
                    {
                        MessageBox.Show("已发出申请，但无K位组人员在线，请用其它方式联系！" + promopt);
                    }
                }
                else
                {
                    MessageBox.Show("申请失败，请重试");
                }
            }
        }

        private void bt_添加姓名_Click(object sender, EventArgs e)
        {
            if (lb_姓名.Items.Count + int.Parse(booked) >= int.Parse(total))
            {
                MessageBox.Show("抱歉：人数超出！不能再增加", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tbCardID.Text.Trim() == "" || tbName.Text.Trim() == "")
            {
                MessageBox.Show("抱歉：请正确输入", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                lb_姓名.Items.Add(tbName.Text.Trim());
                lb_CardNo.Items.Add(tbCardID.Text.Trim());
                tbCardID.Text = tbName.Text = "";
            }
            catch
            {
                MessageBox.Show("抱歉：添加乘客出错！", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bt_删除_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lb_姓名.SelectedItems.Count; i++)
            {
                int it = lb_姓名.SelectedIndices[i];
                lb_姓名.Items.RemoveAt(it);
                lb_CardNo.Items.RemoveAt(it);
            }
        }
    }
}