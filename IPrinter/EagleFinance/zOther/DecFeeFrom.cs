using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace EagleFinance.zOther
{
    public partial class DecFeeFrom : Form
    {
        public DecFeeFrom()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!cert())
            {
                MessageBox.Show("用户名或密码不正确");
                return;
            }
            net();
            string user = textBox1.Text;
            string pnrs = ws.services.getUndecfeePnr(user).Trim();
            if (pnrs.Length > 5)
                if (pnrs.Substring(pnrs.Length - 1) == ";")
                    pnrs = pnrs.Substring(0, pnrs.Length - 1);
            string [] pnrArray = pnrs.Split(';');
            int count = pnrArray.Length;
            FormMain.LABEL.Text = string.Format("共{0}个PNR需要同步:{1}", count,pnrs);
            Application.DoEvents();
            MessageBox.Show(FormMain.LABEL.Text);
            if (count < 1 || pnrs.Length < 5) return;
            for (int i = 0; i < count; i++)
            {
                string pnr = pnrArray[i];
                string money = calmoneyWithPnr(pnr);
                decfeeMoney(pnr, money);
                FormMain.LABEL.Text = string.Format("共{0}/{1}个PNR需要同步", i + 1, count);
                GlobalApi.appenderrormessage(string.Format("上传--------------->PNR={0},MONEY={1}", pnr, money));
                Application.DoEvents();
            }
            FormMain.LABEL.Text = DateTime.Now.ToString() + "同步完毕,错误信息请查看菜单->窗口->错误信息窗口";
            this.Close();
        }
        void net()
        {
            if (this.radioButton1.Checked) 
                ws.services.url = "http://yinge.eg66.com/WS3/egws.asmx";
            else
                ws.services.url = "http://wangtong.eg66.com/WS3/egws.asmx";
        }
        bool cert()
        {
            WS.egws w = new WS.egws(ws.services.url);
            gs.para.NewPara np = new gs.para.NewPara();
            np.AddPara("cm", "Login");
            np.AddPara("UserName", textBox1.Text);//jm
            np.AddPara("PassWord", textBox2.Text);//jm
            string strReq = np.GetXML();
            string strRet = w.getEgSoap(strReq);
            if (strRet.IndexOf("LoginSucc") >= 0) return true;
            return false;
        }
        string calmoneyWithPnr(string pnr)
        {
            //1.选出所有当天PNR的记录,并总计金额后返回之
            try
            {
                string sel = string.Format
                    ("select [COLLECTION],[TAXS] from etickets where PNR='{0}' and [TKT-FLAG]='1' and DATEOFSALE>=cdate('{1}')",
                    pnr, DateTime.Now.AddDays(-4).ToShortDateString());
                OleDbCommand cmd = new OleDbCommand(sel, GlobalVar.cn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    int sum = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int fare = 0;
                        try { fare = int.Parse(dt.Rows[i][0].ToString()); }
                        catch { }
                        int taxs = 0;
                        try { taxs = int.Parse(dt.Rows[i][1].ToString()); }
                        catch { }
                        sum += (fare + taxs);
                    }
                    return sum.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("calmoneyWithPnr:" + ex.Message);
            }
            return "0";
        }
        bool decfeeMoney(string pnr,string money)
        {
            try
            {
                //if (pnr.Length < 5 || money == "" || float.Parse(money) < 1F) 
                    //throw new Exception(string.Format("同步:PNR={0}或金额={1}参数不正确", pnr, money));
                return ws.services.decfee(pnr, money);
            }
            catch(Exception ex)
            {
                GlobalApi.appenderrormessage(ex.Message);
            }
            return false;
        }
    }
}