using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace ePlus.NetworkSetup
{
    public partial class Quenes : Form
    {
        public Quenes()
        {
            InitializeComponent();
        }
        static public bool b_opened = false;
        static public string retstring = "";
        static public NetworkSetup.Quenes context = null;
        string CurrentTerminal = "";

        OleDbConnection cn = new OleDbConnection();
        string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Quenes.mdb;";
        private void Quenes_Load(object sender, EventArgs e)
        {
            retstring = "";
            b_opened = true;
            connect_4_Command.PrintWindowOpen = true;
            context = this;


            cn.ConnectionString = ConnStr;
            cn.Open();
            ReadTerminal();
            cbQueneType.SelectedIndex = 0;
        }
        static public string returnstring
        {
            set
            {
                if (context != null)
                {
                    string temp = "";
                    PrintHyx.PrintHyxPublic.GetRetString(ref retstring, ref temp, true);
                    if (temp != "") rs = temp;
                }

            }
        }
        static public string rs
        {
            set
            {
                if (context.InvokeRequired)
                {
                    EventHandler eh = new EventHandler(setcontrol);
                    NetworkSetup.Quenes pt = NetworkSetup.Quenes.context;
                    NetworkSetup.Quenes.context.Invoke(eh, new object[] { pt, EventArgs.Empty });
                }
            }

        }
        static private void setcontrol(object sender, EventArgs e)
        {
            retstring = retstring.Replace('+', ' ');
            retstring = retstring.Replace('-', ' ');
            context.tbQuene.Text = retstring;
            retstring = "";
            if (context.b_qtCmd)//若是QT命令
            {
                string temp = context.GetTerminalByQT(context.tbQuene.Text);
                context.WriteTerminal(temp);
                context.CurrentTerminal = temp;
                context.ReadTerminal();
            }
            else//否则清Q指令
            {
                string temp = context.tbQuene.Text;                
                if (temp.IndexOf("Q EMPTY") > -1) return;
                else
                {
                    //存入数据库
                    context.WriteQuene(context.CurrentTerminal, context.cbQueneType.Text, temp);
                    //然后发送QD/QN
                    EagleAPI.EagleSendOneCmd("QN");
                }
            }
            context.b_qtCmd = false;

        }
        private void Quenes_FormClosed(object sender, FormClosedEventArgs e)
        {
            cn.Close();
            this.Dispose();
        }
        void ReadTerminal()
        {
            tvQuene.Nodes[0].Nodes.Clear();
            try
            {
                string sqlString = string.Format("select * from tbTerminal order by Terminal");
                OleDbCommand cmd = new OleDbCommand(sqlString, cn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dtTmp = new DataTable();
                adapter.Fill(dtTmp);
                for (int i = 0; i < dtTmp.Rows.Count; i++)
                {
                    string tNumber = dtTmp.Rows[i][1].ToString();
                    tvQuene.Nodes[0].Nodes.Add(tNumber, tNumber);
                    for (int j = 0; j < cbQueneType.Items.Count; j++)
                    {
                        string qName = cbQueneType.Items[j].ToString();

                        tvQuene.Nodes[0].Nodes[tNumber].Nodes.Add(qName, qName);
                    }
                    //tvQuene.Nodes[0].Nodes[tNumber].Nodes.Add("GQ", "GQ");
                    //tvQuene.Nodes[0].Nodes[tNumber].Nodes.Add("RP", "RP");
                    //tvQuene.Nodes[0].Nodes[tNumber].Nodes.Add("KK", "KK");
                    //tvQuene.Nodes[0].Nodes[tNumber].Nodes.Add("RE", "RE");
                    //tvQuene.Nodes[0].Nodes[tNumber].Nodes.Add("SR", "SR");
                    //tvQuene.Nodes[0].Nodes[tNumber].Nodes.Add("TC", "TC");
                    //tvQuene.Nodes[0].Nodes[tNumber].Nodes.Add("TL", "TL");
                    //tvQuene.Nodes[0].Nodes[tNumber].Nodes.Add("SC", "SC");
                }
            }
            catch 
            {
                tbQuene.Text = "读取数据失败！";
            }
        }
        void WriteTerminal(string tNumber)
        {
            try
            {
                string tm = tNumber.Trim();
                if (tm.Length != 6) return;
                string sqlString = string.Format("insert into tbTerminal (Terminal) Values('{0}')", tm);
                OleDbCommand cmd = new OleDbCommand(sqlString, cn);
                if (1 != cmd.ExecuteNonQuery()) ;
            }
            catch
            {
                //tbQuene.Text = "写入数据失败！" + tNumber;
            }
        }
        void DeleteTerminal(string tNumber)
        {
            try
            {
                string tm = tNumber.Trim();
                if (tm.Length != 6) return;
                string sqlString = string.Format("delete from tbTerminal where Terminal='{0}'", tm);
                OleDbCommand cmd = new OleDbCommand(sqlString, cn);
                if (1 != cmd.ExecuteNonQuery()) tbQuene.Text = "删除数据失败";
            }
            catch
            {
                tbQuene.Text = "删除数据失败！";
            }

        }
        string GetTerminalByQT(string s)
        {
            int pos = s.IndexOf("QT");
            if (pos > -1) pos += 2;
            string temp = s.Substring(pos).Trim();
            temp = temp.Substring(0, 6);
            return temp;
        }
        bool b_qtCmd = false;
        private void btQueneState_Click(object sender, EventArgs e)
        {
            tbQuene.Text = "";
            b_qtCmd = true;
            EagleAPI.EagleSendOneCmd("qt");
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            if (CurrentTerminal.Length != 6)
            {
                tbQuene.Text = "请先检查信箱状态";
                return;
            }
            EagleAPI.EagleSendOneCmd("i~@~qde~qs" + cbQueneType.Text);
        }

        string GetTerminalID(string tNumber)
        {
            string sqlString;
            sqlString = string.Format("select ID from tbTerminal where Terminal='{0}'", tNumber);
            OleDbCommand cmd1 = new OleDbCommand(sqlString, cn);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd1);
            DataTable dtTmp = new DataTable();
            adapter.Fill(dtTmp);
            return dtTmp.Rows[0][0].ToString();

        }
        bool WriteQuene(string tNumber, string qType, string qContent)
        {
            try
            {
                string sqlString;
                sqlString = string.Format("insert into QueneContent (TerminalID,QueneType,Content,RecordTime) Values ('{0}','{1}','{2}','{3}')",
                    GetTerminalID(tNumber), qType, qContent, System.DateTime.Now.ToString());
                OleDbCommand cmd = new OleDbCommand(sqlString, cn);

                if (1 != cmd.ExecuteNonQuery()) ;
            }
            catch
            {
                tbQuene.Text = "数据保存失败" + tNumber;
                return false;
            }
            return true;
        }
        DataTable ReadQuene(string tNumber, string qType)
        {
            DataTable dtTmp = new DataTable();
            string sqlString = string.Format("select Content from QueneContent Where TerminalID='{0}' and QueneType='{1}' ORDER BY RecordTime DESC", GetTerminalID(tNumber), qType);
            OleDbCommand cmd = new OleDbCommand(sqlString, cn);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            adapter.Fill(dtTmp);
            return dtTmp;
        }
        DataTable contentTable = new DataTable();
        private void tvQuene_AfterSelect(object sender, TreeViewEventArgs e)
        {
            iLine = 0;
            if (e.Node.Text.Length != 2) return;
            string tn = e.Node.Parent.Text;
            string qt = e.Node.Text;
            contentTable = ReadQuene(tn, qt);
            tbQuene.Text = tn + "的" + qt + "信箱共有 " + contentTable.Rows.Count.ToString() + " 条记录";
            for (int i = 0; i < contentTable.Rows.Count; i++)
            {
                tbQuene.Text += contentTable.Rows[i][0];
            }
        }
        int iLine = 0;
        private void btFirst_Click(object sender, EventArgs e)
        {
            if (contentTable.Rows.Count <= 0) tbQuene.Text = "无记录";
            else
            {
                tbQuene.Text = "第 1 条";
                tbQuene.Text += contentTable.Rows[0][0].ToString();
                iLine = 0;
            }
        }

        private void btLast_Click(object sender, EventArgs e)
        {
            if (contentTable.Rows.Count <= 0) tbQuene.Text = "无记录";
            else
            {
                tbQuene.Text = "第 " + contentTable.Rows.Count.ToString() + " 条";
                tbQuene.Text += contentTable.Rows[contentTable.Rows.Count - 1][0];
                iLine = contentTable.Rows.Count - 1;
            }
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            if (contentTable.Rows.Count <= 0) tbQuene.Text = "无记录";
            else
            {
                int i = iLine - 1;
                if (i < 0) i = 0;
                tbQuene.Text = string.Format("第 {0} 条", (i + 1));
                tbQuene.Text += contentTable.Rows[i][0];
                iLine = i;
            }

        }

        private void btNext_Click(object sender, EventArgs e)
        {
            if (contentTable.Rows.Count <= 0) tbQuene.Text = "无记录";
            else
            {
                int i = iLine + 1;
                if (i >= contentTable.Rows.Count) i = contentTable.Rows.Count - 1;
                tbQuene.Text = string.Format("第 {0} 条", (i + 1));
                tbQuene.Text += contentTable.Rows[i][0];
                iLine = i;
            }
        }
        string clearDest = "";
        string clearDestParent = "";
        private void tvQuene_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            clearDest = e.Node.Text;
            if (clearDest.Length == 2) clearDestParent = e.Node.Parent.Text;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu rightMenu = new ContextMenu();
                EventHandler eh0 = new EventHandler(clear);
                rightMenu.MenuItems.Add("清空记录", eh0);
                rightMenu.Show(tvQuene, new Point(e.X, e.Y));
            }
        }
        private void clear(object sender, EventArgs e)
        {
            string sql;
            if (clearDest == "全部信箱")
            {
                try
                {
                    if (MessageBox.Show("确定清空所有信箱记录吗？", "警告", MessageBoxButtons.YesNo) == DialogResult.No) return;
                    sql = "delete from QueneContent";
                    OleDbCommand cmd = new OleDbCommand(sql, cn);
                    cmd.ExecuteNonQuery();
                    sql = "delete from tbTerminal";
                    OleDbCommand cmd1 = new OleDbCommand(sql, cn);
                    cmd1.ExecuteNonQuery();
                }
                catch
                {
                    tbQuene.Text = ("删除所有信箱失败");
                    return;
                }
            }
            else if (clearDest.Length == 6)
            {
                try
                {
                    if (MessageBox.Show("确定清空" + clearDest + "的所有记录吗？", "警告", MessageBoxButtons.YesNo) == DialogResult.No) return;
                    sql = string.Format("delete from QueneContent where TerminalID='{0}'", GetTerminalID(clearDest));
                    OleDbCommand cmd = new OleDbCommand(sql, cn);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    tbQuene.Text = ("删除" + clearDest + "信箱失败");
                    return;
                }
            }
            else if (clearDest.Length == 2)
            {
                try
                {
                    if (MessageBox.Show("确定清空" + clearDestParent + "的" + clearDest + "的所有记录吗？", "警告", MessageBoxButtons.YesNo) == DialogResult.No) return;
                    sql = string.Format("delete from QueneContent where terminalID='{0}' and QueneType='{1}'", GetTerminalID(clearDestParent), clearDest);
                    OleDbCommand cmd = new OleDbCommand(sql, cn);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    tbQuene.Text = "删除" + clearDestParent + "的" + clearDest + "信箱失败";
                    return;
                }
            }
            tbQuene.Text = "删除成功！";
            ReadTerminal();
        }

    }
}