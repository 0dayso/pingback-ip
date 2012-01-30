using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Collections;


namespace EagleFinance
{
    public partial class FormMain : Form
    {
        public static Label LABEL = null;
        private EagleString.LoginInfo m_li;
        private EagleProtocal.MyTcpIpClient m_socket;
        private EagleString.CommandPool m_cmdpool;
        public FormMain(EagleString.LoginInfo li,EagleProtocal.MyTcpIpClient sk,EagleString.CommandPool pool)
        {
            InitializeComponent();
            LABEL = this.label1;
            CheckForIllegalCrossThreadCalls = false;
            //skinEngine1.SkinFile = Application.StartupPath + "\\EagleSkin.ssk";
            set_args(li, sk, pool);
        }
        public void set_args(EagleString.LoginInfo li, EagleProtocal.MyTcpIpClient sk, EagleString.CommandPool pool)
        {
            m_li = li;
            m_cmdpool = pool;
            m_socket = sk;
        }
        string selstring = "";
        private void 入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Incoming ic = new Incoming();
            ic.ShowDialog();
        }

        private void 导入TSLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("使用方法：将tpr报表保存成txt文本文档后，导入即可！");
            string filename = "";
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "所有文件(*.*)|*.*|文本文件(*.txt)|*.txt|TPR报表文件(*.tpr)|*.tpr";
                string path = Application.StartupPath;
                if (!Directory.Exists(path + "\\Tpr"))
                    Directory.CreateDirectory(path + "\\Tpr");
                dlg.InitialDirectory = Application.StartupPath + "\\Tpr";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filename = dlg.FileName;
                    MessageBox.Show
                        (GlobalApi.getOffice(dlg.FileName) + "\n" +
                        GlobalApi.getDateOfSale(dlg.FileName) + "\n" +
                        GlobalApi.getTotalTicket(dlg.FileName) + "张电子客票" + "\n" +
                        GlobalApi.getNORMALTICKETS(dlg.FileName)
                    );
                    GlobalApi.importAllLineFromTpr(dlg.FileName);
                    MessageBox.Show("导入完成" + filename);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + filename);
                GlobalApi.appenderrormessage(ex.Message + "导入失败" + filename);
            }
        }
        
        private void 导入Eagle电子客票报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("使用方法：从Eagle管理后台电子客票管理导出EXCEL的CSV文档转存成XLS文档后，导入即可！");
            string filename = "";
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "EXCEL文件XLS格式(*.xls)|*.xls|EXCEL文件CSV格式(*.csv)|*.csv|所有文件(*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filename = dlg.FileName;
                    GlobalApi.importEagleReport(dlg.FileName);
                    MessageBox.Show("导入完成");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "导入失败" + filename);
                GlobalApi.appenderrormessage(ex.Message + "导入失败" + filename);
            }
        }
        BindingSource bs = new BindingSource();
        search sr = new search();
        private void 查询SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (sr.ShowDialog() == DialogResult.OK)
                {
                    display(sr.selString);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                GlobalApi.appenderrormessage("查询" + ex.Message);
            }
        }
        
        void display(string sel)
        {
            if (sel == "") return;
            selstring = sel;
            DataTable dt = new DataTable();
            OleDbCommand cmd = new OleDbCommand(selstring, GlobalVar.cn);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            adapter.Fill(dt);
            bs.DataSource = dt;
            dg.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            label1.Text = string.Format("共找到{0}条记录", dt.Rows.Count);
        }
        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            //dg.Size = new Size(this.Size.Width - 8, this.Size.Height - 70);
            label1.Location = new Point(8, this.Size.Height-50);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (GlobalVar.cn.State != ConnectionState.Open) GlobalVar.cn.Open();
            dg.DataSource = bs;
            if (GlobalVar.agent == AGENTS.ZHENGZHOU)
                this.Text = "郑州机场票证管理对帐系统";
            
        }

        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete dl = new Delete();
            dl.ShowDialog();
        }

        private void 导出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export ep = new Export();
            if (ep.ShowDialog() == DialogResult.OK)
            {
                export(ep.rowExport);
            }
        }
        int LP = 3;//底价点
        void export(List<int> rows)
        {
            Hashtable htColumn = new Hashtable();
            htColumn.Add(22, "底价金额");
            htColumn.Add(23, "返点金额");
            htColumn.Add(24, "利润");
            htColumn.Add(25, "实收");
            htColumn.Add(26, "备注");

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Excel文件到";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            Stream myStream;
            myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
            string str = "";
            //定义导出顺序！
            /*
            int[] co = new int[dg.ColumnCount];
            {
                for (int i = 0; i < co.Length; i++)
                {
                    co[i] = i;
                }
            }
             * *///rows里已经包含了顺序
            //导出顺序定义结束！
            try
            {
                //写标题  
                /*for (int i = 0; i < dg.ColumnCount; i++)*/
                for (int i = 0; i < rows.Count; i++)
                {
                    /*if (!rows.Contains(i)) continue;*/
                    if (i > 0)
                    {
                        str += "\t";
                    }

                    int intColumn = rows[i];
                    if (intColumn > 20)
                    {
                        str += htColumn[intColumn].ToString();
                    }
                    else
                    {
                        /*str += dg.Columns[co[i]].HeaderText;*/
                        str += dg.Columns[intColumn].HeaderText;
                    }
                }
                if (str.Substring(0, 1) == "\t") str = str.Substring(1);
                sw.WriteLine(str);
                //写内容 
                for (int j = 0; j < dg.Rows.Count-1; j++)
                {
                    FormMain.LABEL.Text = string.Format("正在导出报表，第{0}条，共{1}条", j, dg.Rows.Count);
                    Application.DoEvents();
                    string tempStr = "";
                    /*for (int k = 0; k < dg.Columns.Count; k++)*/
                    for (int k = 0; k < rows.Count; k++)
                    {
                        int intColumn = rows[k];
                        /*if (!rows.Contains(k)) continue;*///看是否选择了该列导出，未选择则继续下一列
                        if (k > 0)
                        {
                            tempStr += "\t";
                        }
                        try
                        {
                            //string temp = dg.Rows[j].Cells[co[k]].Value.ToString();
                            string temp = "NULL";
                            try
                            {
                                

                                if (intColumn > 20)
                                    temp = "NULL";
                                else
                                    temp = dg.Rows[j].Cells[intColumn].Value.ToString();
                            }
                            catch
                            {
                                temp = "NULL";
                            }
                            if (rows[k] == 3 || rows[k] == 11 || rows[k] == 16)
                            {
                                try
                                {
                                    temp = DateTime.Parse(temp).ToShortDateString();
                                    if (rows[k] == 16)
                                        temp = temp.Substring(5);
                                }
                                catch
                                {
                                    temp = "NULL";
                                }
                            }
                            else if (rows[k] == 8)//第8列为总返点，导出到excel为3
                            {
                                try
                                {
                                    int value = int.Parse(temp);
                                    if (value >= LP) temp = LP.ToString();
                                }
                                catch
                                {
                                }
                            }
                            else if (intColumn == 22)//Ｘ值的金额
                            {
                                try
                                {
                                    float ftemp = float.Parse(dg.Rows[j].Cells[6].Value.ToString()) * (float)LP/100F;
                                    temp = ftemp.ToString("f2");
                                }
                                catch
                                {
                                }
                            }
                            else if (rows[k] == 20)//第20列为代理返点，用第8列-3
                            {
                                try
                                {
                                    int tRet = int.Parse(dg.Rows[j].Cells[8].Value.ToString());
                                    if (tRet >= LP)
                                        temp = string.Format("{0}", tRet - LP);
                                    else temp = "0";
                                }
                                catch
                                {
                                }
                            }
                            else if (intColumn == 23)//Y值的金额
                            {
                                try
                                {
                                    int tRet = int.Parse(dg.Rows[j].Cells[8].Value.ToString());
                                    if (tRet >= LP) tRet -= LP;
                                    float ftemp = float.Parse(dg.Rows[j].Cells[6].Value.ToString())
                                        * (float)tRet/100F;
                                    temp = ftemp.ToString("f2");
                                }
                                catch
                                {
                                }
                            }
                            tempStr += temp;
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show("次内部");
                            tempStr += "NULL";
                            if (j == dg.Rows.Count - 1) ;
                            else
                                ;// throw new Exception(ex.ToString());
                        }
                    }
                    if (tempStr.Substring(0, 1) == "\t") tempStr = tempStr.Substring(1);
                    sw.WriteLine(tempStr.Replace("\t\t", "\tNULL\t"));
                }
                sw.Close();
                myStream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                GlobalApi.appenderrormessage(e.ToString());
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }


        }

        private void 备份BToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Backup bk = new Backup();
            bk.ShowDialog();
        }

        private void 还原RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "备份文件(*.bak)|*.bak|所有文件(*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    GlobalVar.cn.Close();
                    File.Copy(dlg.FileName, Application.StartupPath + "\\database.mdb", true);
                    GlobalVar.cn.Open();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 导入ABMS报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "EXCEL文件XLS格式(*.xls)|*.xls|EXCEL文件CSV格式(*.csv)|*.csv|所有文件(*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    GlobalApi.importAbmsReport(dlg.FileName);
                    MessageBox.Show("导入完成");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                GlobalApi.appenderrormessage(ex.Message);
            }
        }

        private void dg_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string id = (dg.Rows[dg.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                string value = dg.SelectedCells[0].Value.ToString();
                int selIndex = dg.SelectedCells[0].ColumnIndex;
                string field = "";
                if (selIndex == 8) field = "[COMM]";
                if (selIndex == 19) field = "[AGENTCOMM]";
                if (selIndex != 8 && selIndex != 19) //8为底价返点
                {
                    return;
                    //dg.SelectedCells[0].Value = clickvalue;
                }
                if (value == clickvalue) return;
                if (MessageBox.Show("确定修改为：" + value, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //更新表
                    {
                        FormMain.LABEL.Text = "之前修改值为"+value;
                        Application.DoEvents();
                        string cmdString = string.Format
                            ("update etickets set "+field+"={0}"
                            + " where [ID]={1}",
                            value,id
                            );
                        OleDbCommand cmd = new OleDbCommand(cmdString, GlobalVar.cn);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    dg.SelectedCells[0].Value = clickvalue;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        string clickvalue = "";
        private void dg_Click(object sender, EventArgs e)
        {
            try
            {
                clickvalue = dg.SelectedCells[0].Value.ToString();
            }
            catch
            {
                clickvalue = "";
            }
        }
        public static ErrorForm errorForm = new ErrorForm();
        private void 错误信息窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            errorForm.ShowDialog();
        }
        zOther.DecFeeFrom decfeeForm = new EagleFinance.zOther.DecFeeFrom();
        private void 同步Eagle数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            decfeeForm.ShowDialog();
        }

        private void dg_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
              e.RowBounds.Location.Y,
              this.dg.RowHeadersWidth,
              e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                this.dg.RowHeadersDefaultCellStyle.Font,
                rectangle,
                this.dg.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);

        }

        private void 自动填入政策ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zOther.PolicyDefaultSetup ds = new EagleFinance.zOther.PolicyDefaultSetup();
            if (ds.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                
                int netroute = 1;
                int rows = dg.RowCount - 1;
                if (rows < 1)
                {
                    MessageBox.Show("请先执行查询","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                bool b高峰 = !GlobalApi.canStartGetPolicy();
                //if (b高峰) return;
                for (int i = 0; i < rows; i++)
                {
                    if (b高峰) System.Threading.Thread.Sleep(3000);
                    LABEL.Text = "正在处理第" + i.ToString() + "/" + rows.ToString() + "条";

                    Application.DoEvents();
                    if (!ds.bReget)
                    {
                        if (dg.Rows[i].Cells[19].Value.ToString().Trim() != "") continue;
                    }
                    string id = (dg.Rows[i].Cells[0].Value.ToString().Trim());
                    hashkey key = new hashkey();
                    key.username = dg.Rows[i].Cells[12].Value.ToString().Trim().ToUpper();//用户
                    key.flightno = dg.Rows[i].Cells[16].Value.ToString().Trim().Replace("/"," ").Trim().ToUpper();//航班
                    try
                    {
                        key.date = dg.Rows[i].Cells[15].Value.ToString().Trim().ToUpper();//乘机日
                        key.date = DateTime.Now.Year.ToString() + "-" + key.date;
                        if (Math.Abs(DateTime.Parse(key.date).Month - DateTime.Now.Month) > 6)

                            key.date = DateTime.Parse(key.date).AddYears(1).ToShortDateString();
                    }
                    catch
                    {
                        GlobalApi.appenderrormessage("未取到航班日期！PNR = " + dg.Rows[i].Cells[9].Value.ToString().Trim().ToUpper());
                        continue;
                    }
                    key.bunk = dg.Rows[i].Cells[17].Value.ToString().Trim().ToUpper();//舱位
                    if (key.bunk.Length > 1)
                    {
                        GlobalApi.appenderrormessage("双航段，不能计算政策！PNR = " + dg.Rows[i].Cells[9].Value.ToString().Trim().ToUpper());
                        continue;
                    }
                    hashvalue value = zOther.policyht.getvaluefromhashtable(key);
                    string fromto = dg.Rows[i].Cells[18].Value.ToString().Trim().ToUpper();
                    if (value == null)
                        value = zOther.policyht.getvaluefromwebserverandsave(key, fromto.Substring(0, 3), fromto.Substring(3).Trim(), netroute);
                    if (value == null)
                        GlobalApi.appenderrormessage("计算政策失败！可能没有为该用户分配用户类别，PNR = " 
                            + dg.Rows[i].Cells[9].Value.ToString().Trim().ToUpper());
                    else
                    {
                        try
                        {
                            string cmdString = string.Format
                                                    ("update etickets set [COMM]={0},[AGENTCOMM]={1}"
                                                    + " where [ID]={2}",
                                                    value.maxGain, value.userGain, id
                                                    );
                            OleDbCommand cmd = new OleDbCommand(cmdString, GlobalVar.cn);
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {
                            GlobalApi.appenderrormessage("更新时出错！PNR = " + dg.Rows[i].Cells[9].Value.ToString().Trim().ToUpper()
                                +"底价返点 = " + value.maxGain 
                                + "代理返点 = "+ value.userGain
                                );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalApi.appenderrormessage("计算政策返点时错误：" + ex.Message);
            }
            LABEL.Text = "政策返点处理完毕！";
            MessageBox.Show("政策返点计算完毕！请重新查询！");
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                GlobalVar.cn.Close();
            }
            catch
            {
            }
            if (GlobalVar.bModified)
                MessageBox.Show("您在导入数据后没有进行备份");
            this.Dispose();
        }

        private void 导入Eagle中文版订单报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = "";
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "EXCEL文件XLS格式(*.xls)|*.xls|EXCEL文件CSV格式(*.csv)|*.csv|所有文件(*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filename = dlg.FileName;
                    GlobalApi.importEagleEasyReport(dlg.FileName);
                    MessageBox.Show("导入Eagle订单状态完成");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "导入失败" + filename);
                GlobalApi.appenderrormessage(ex.Message + "导入Eagle订单状态失败" + filename);
            }
        }
        public Forms.AutoImport autoImport;
        private void a全自动导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoImport = new EagleFinance.Forms.AutoImport(m_socket, m_li, m_cmdpool);
            autoImport.TopMost = true;
            autoImport.Show();
        }
        void mnImportPolicyClick()
        {
            string txtfile = "";
            FileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtfile = dlg.FileName;
            }
            else return;
            string[] lines = File.ReadAllLines(txtfile);
            long tktno;
            int taxbuild;
            int taxfuel;
            int pointReturn;
            int count = 0;
            
            foreach (string line in lines)
            {
                string[] col = line.Split(' ');
                try
                {
                    tktno = long.Parse(col[0] + col[1] + col[2] + col[3]);
                    taxbuild = (int)float.Parse(col[8]);//保留
                    int c1 = 10;
                    int c2 = 12;
                    if (line.IndexOf("CN") > 0)
                    {
                        c1++;
                        c2++;
                    }
                    pointReturn = (int)float.Parse(col[c1]) + (int)float.Parse(col[c2]);
                    LABEL.Text = string.Format("正在导入航协帐单:{0}",tktno.ToString());
                    Application.DoEvents();
                    string sqlstring = string.Format
                            ("update etickets set COMM={0}"
                            + " where [E-TKT-NUMBER]='{1}'",
                            pointReturn, tktno.ToString().Insert(3, "-")
                            );
                    OleDbCommand cmd = new OleDbCommand(sqlstring, GlobalVar.cn);
                    int l= cmd.ExecuteNonQuery();
                    count += l;
                    if (l == 0) EagleString.EagleFileIO.LogWrite(tktno.ToString() + "未导入，数据库中不存在该票号");
                }
                catch
                {
                    try
                    {
                        if (line.IndexOf("YQ") > 0)
                        {
                            taxfuel = (int)float.Parse(col[0]);//保留
                            //若需要，在这里修改燃油值update...
                        }
                    }
                    catch
                    {
                    }
                }

            }
            MessageBox.Show(string.Format("完毕,共更新了{0}条记录!", count));
        }
        private void mnImportPolicy_Click(object sender, EventArgs e)
        {
            new System.Threading.Thread(
                new System.Threading.ThreadStart(mnImportPolicyClick)
            ).Start();
                
        }

        private void dg_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                menu.Items.Add("获取行程单号", null, new EventHandler(GetReceiptNumber));
                menu.Show(dg,e.Location);
            }
        }
        private void GetReceiptNumber(object sender, EventArgs e)
        {
            for (int i = 0; i < dg.SelectedRows.Count;i++ )
            {
                string tktno=(dg.SelectedRows[i].Cells[4].Value.ToString());
                string cmd = string.Format("detr:tn/{0},f", tktno);
                m_cmdpool.SetType(EagleString.ETERM_COMMAND_TYPE.DETR_GetReceiptNoFinance);
                m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
                System.Threading.Thread.Sleep(2000);
            }
            MessageBox.Show("完毕！");
            //m_socket.Send();
        }
        public void SetReceiptNumber(string tktno, string receiptno)
        {
            if (receiptno == "") receiptno = "0000000000";
            tktno = tktno.Replace("-","").Insert(3,"-");
            string sqlstring = string.Format(
                "update etickets set receiptNumber='{0}' where [E-TKT-NUMBER]='{1}'", receiptno, tktno
                );
            if (new OleDbCommand(sqlstring, GlobalVar.cn).ExecuteNonQuery() == 1)
            {
                FormMain.LABEL.Text = tktno + " / " + receiptno;
            }
        }
    }
}