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
        private void ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Incoming ic = new Incoming();
            ic.ShowDialog();
        }

        private void ����TSLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ʹ�÷�������tpr�������txt�ı��ĵ��󣬵��뼴�ɣ�");
            string filename = "";
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "�����ļ�(*.*)|*.*|�ı��ļ�(*.txt)|*.txt|TPR�����ļ�(*.tpr)|*.tpr";
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
                        GlobalApi.getTotalTicket(dlg.FileName) + "�ŵ��ӿ�Ʊ" + "\n" +
                        GlobalApi.getNORMALTICKETS(dlg.FileName)
                    );
                    GlobalApi.importAllLineFromTpr(dlg.FileName);
                    MessageBox.Show("�������" + filename);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + filename);
                GlobalApi.appenderrormessage(ex.Message + "����ʧ��" + filename);
            }
        }
        
        private void ����Eagle���ӿ�Ʊ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ʹ�÷�������Eagle�����̨���ӿ�Ʊ������EXCEL��CSV�ĵ�ת���XLS�ĵ��󣬵��뼴�ɣ�");
            string filename = "";
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "EXCEL�ļ�XLS��ʽ(*.xls)|*.xls|EXCEL�ļ�CSV��ʽ(*.csv)|*.csv|�����ļ�(*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filename = dlg.FileName;
                    GlobalApi.importEagleReport(dlg.FileName);
                    MessageBox.Show("�������");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "����ʧ��" + filename);
                GlobalApi.appenderrormessage(ex.Message + "����ʧ��" + filename);
            }
        }
        BindingSource bs = new BindingSource();
        search sr = new search();
        private void ��ѯSToolStripMenuItem_Click(object sender, EventArgs e)
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
                GlobalApi.appenderrormessage("��ѯ" + ex.Message);
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
            label1.Text = string.Format("���ҵ�{0}����¼", dt.Rows.Count);
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
                this.Text = "֣�ݻ���Ʊ֤�������ϵͳ";
            
        }

        private void ɾ��DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete dl = new Delete();
            dl.ShowDialog();
        }

        private void ����XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export ep = new Export();
            if (ep.ShowDialog() == DialogResult.OK)
            {
                export(ep.rowExport);
            }
        }
        int LP = 3;//�׼۵�
        void export(List<int> rows)
        {
            Hashtable htColumn = new Hashtable();
            htColumn.Add(22, "�׼۽��");
            htColumn.Add(23, "������");
            htColumn.Add(24, "����");
            htColumn.Add(25, "ʵ��");
            htColumn.Add(26, "��ע");

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "����Excel�ļ���";

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            Stream myStream;
            myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
            string str = "";
            //���嵼��˳��
            /*
            int[] co = new int[dg.ColumnCount];
            {
                for (int i = 0; i < co.Length; i++)
                {
                    co[i] = i;
                }
            }
             * *///rows���Ѿ�������˳��
            //����˳���������
            try
            {
                //д����  
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
                //д���� 
                for (int j = 0; j < dg.Rows.Count-1; j++)
                {
                    FormMain.LABEL.Text = string.Format("���ڵ���������{0}������{1}��", j, dg.Rows.Count);
                    Application.DoEvents();
                    string tempStr = "";
                    /*for (int k = 0; k < dg.Columns.Count; k++)*/
                    for (int k = 0; k < rows.Count; k++)
                    {
                        int intColumn = rows[k];
                        /*if (!rows.Contains(k)) continue;*///���Ƿ�ѡ���˸��е�����δѡ���������һ��
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
                            else if (rows[k] == 8)//��8��Ϊ�ܷ��㣬������excelΪ3
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
                            else if (intColumn == 22)//��ֵ�Ľ��
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
                            else if (rows[k] == 20)//��20��Ϊ�����㣬�õ�8��-3
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
                            else if (intColumn == 23)//Yֵ�Ľ��
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
                            //MessageBox.Show("���ڲ�");
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

        private void ����BToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Backup bk = new Backup();
            bk.ShowDialog();
        }

        private void ��ԭRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "�����ļ�(*.bak)|*.bak|�����ļ�(*.*)|*.*";
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

        private void ����ABMS����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "EXCEL�ļ�XLS��ʽ(*.xls)|*.xls|EXCEL�ļ�CSV��ʽ(*.csv)|*.csv|�����ļ�(*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    GlobalApi.importAbmsReport(dlg.FileName);
                    MessageBox.Show("�������");
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
                if (selIndex != 8 && selIndex != 19) //8Ϊ�׼۷���
                {
                    return;
                    //dg.SelectedCells[0].Value = clickvalue;
                }
                if (value == clickvalue) return;
                if (MessageBox.Show("ȷ���޸�Ϊ��" + value, "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //���±�
                    {
                        FormMain.LABEL.Text = "֮ǰ�޸�ֵΪ"+value;
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
        private void ������Ϣ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            errorForm.ShowDialog();
        }
        zOther.DecFeeFrom decfeeForm = new EagleFinance.zOther.DecFeeFrom();
        private void ͬ��Eagle����ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void �Զ���������ToolStripMenuItem_Click(object sender, EventArgs e)
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
                    MessageBox.Show("����ִ�в�ѯ","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                bool b�߷� = !GlobalApi.canStartGetPolicy();
                //if (b�߷�) return;
                for (int i = 0; i < rows; i++)
                {
                    if (b�߷�) System.Threading.Thread.Sleep(3000);
                    LABEL.Text = "���ڴ����" + i.ToString() + "/" + rows.ToString() + "��";

                    Application.DoEvents();
                    if (!ds.bReget)
                    {
                        if (dg.Rows[i].Cells[19].Value.ToString().Trim() != "") continue;
                    }
                    string id = (dg.Rows[i].Cells[0].Value.ToString().Trim());
                    hashkey key = new hashkey();
                    key.username = dg.Rows[i].Cells[12].Value.ToString().Trim().ToUpper();//�û�
                    key.flightno = dg.Rows[i].Cells[16].Value.ToString().Trim().Replace("/"," ").Trim().ToUpper();//����
                    try
                    {
                        key.date = dg.Rows[i].Cells[15].Value.ToString().Trim().ToUpper();//�˻���
                        key.date = DateTime.Now.Year.ToString() + "-" + key.date;
                        if (Math.Abs(DateTime.Parse(key.date).Month - DateTime.Now.Month) > 6)

                            key.date = DateTime.Parse(key.date).AddYears(1).ToShortDateString();
                    }
                    catch
                    {
                        GlobalApi.appenderrormessage("δȡ���������ڣ�PNR = " + dg.Rows[i].Cells[9].Value.ToString().Trim().ToUpper());
                        continue;
                    }
                    key.bunk = dg.Rows[i].Cells[17].Value.ToString().Trim().ToUpper();//��λ
                    if (key.bunk.Length > 1)
                    {
                        GlobalApi.appenderrormessage("˫���Σ����ܼ������ߣ�PNR = " + dg.Rows[i].Cells[9].Value.ToString().Trim().ToUpper());
                        continue;
                    }
                    hashvalue value = zOther.policyht.getvaluefromhashtable(key);
                    string fromto = dg.Rows[i].Cells[18].Value.ToString().Trim().ToUpper();
                    if (value == null)
                        value = zOther.policyht.getvaluefromwebserverandsave(key, fromto.Substring(0, 3), fromto.Substring(3).Trim(), netroute);
                    if (value == null)
                        GlobalApi.appenderrormessage("��������ʧ�ܣ�����û��Ϊ���û������û����PNR = " 
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
                            GlobalApi.appenderrormessage("����ʱ����PNR = " + dg.Rows[i].Cells[9].Value.ToString().Trim().ToUpper()
                                +"�׼۷��� = " + value.maxGain 
                                + "������ = "+ value.userGain
                                );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalApi.appenderrormessage("�������߷���ʱ����" + ex.Message);
            }
            LABEL.Text = "���߷��㴦����ϣ�";
            MessageBox.Show("���߷��������ϣ������²�ѯ��");
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
                MessageBox.Show("���ڵ������ݺ�û�н��б���");
            this.Dispose();
        }

        private void ����Eagle���İ涩������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = "";
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "EXCEL�ļ�XLS��ʽ(*.xls)|*.xls|EXCEL�ļ�CSV��ʽ(*.csv)|*.csv|�����ļ�(*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filename = dlg.FileName;
                    GlobalApi.importEagleEasyReport(dlg.FileName);
                    MessageBox.Show("����Eagle����״̬���");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "����ʧ��" + filename);
                GlobalApi.appenderrormessage(ex.Message + "����Eagle����״̬ʧ��" + filename);
            }
        }
        public Forms.AutoImport autoImport;
        private void aȫ�Զ�����ToolStripMenuItem_Click(object sender, EventArgs e)
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
                    taxbuild = (int)float.Parse(col[8]);//����
                    int c1 = 10;
                    int c2 = 12;
                    if (line.IndexOf("CN") > 0)
                    {
                        c1++;
                        c2++;
                    }
                    pointReturn = (int)float.Parse(col[c1]) + (int)float.Parse(col[c2]);
                    LABEL.Text = string.Format("���ڵ��뺽Э�ʵ�:{0}",tktno.ToString());
                    Application.DoEvents();
                    string sqlstring = string.Format
                            ("update etickets set COMM={0}"
                            + " where [E-TKT-NUMBER]='{1}'",
                            pointReturn, tktno.ToString().Insert(3, "-")
                            );
                    OleDbCommand cmd = new OleDbCommand(sqlstring, GlobalVar.cn);
                    int l= cmd.ExecuteNonQuery();
                    count += l;
                    if (l == 0) EagleString.EagleFileIO.LogWrite(tktno.ToString() + "δ���룬���ݿ��в����ڸ�Ʊ��");
                }
                catch
                {
                    try
                    {
                        if (line.IndexOf("YQ") > 0)
                        {
                            taxfuel = (int)float.Parse(col[0]);//����
                            //����Ҫ���������޸�ȼ��ֵupdate...
                        }
                    }
                    catch
                    {
                    }
                }

            }
            MessageBox.Show(string.Format("���,��������{0}����¼!", count));
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
                menu.Items.Add("��ȡ�г̵���", null, new EventHandler(GetReceiptNumber));
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
            MessageBox.Show("��ϣ�");
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