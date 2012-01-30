using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace EagleForms.General
{
    public partial class frmExpireTicketFind : Form
    {
        string filenm = Application.StartupPath + "\\ExpiredTicket.txt";
        string title = "检测设置";
        int total = 0;
        public frmExpireTicketFind()
        {
            InitializeComponent();
            this.Text = title;
            EagleBase.AccessOperation.ExpTickDatabaseCreate();
        }
        protected override void DefWndProc(ref Message m)
        {

            if (m.Msg == (int)EagleString.Imported.egMsg.EXPIRE_TICKET_IMPORT_FROM_EAGLEFINANCE_CURRENT)
            {
                int curr = (int)m.LParam;
                this.Text = title + "-" + string.Format("正在导入{0}/{1}", curr + 1, total);
                Application.DoEvents();
            }
            else if (m.Msg == (int)EagleString.Imported.egMsg.EXPIRE_TICKET_IMPORT_FROM_EAGLEFINANCE_TOTAL)
            {
                total = (int)m.LParam;
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }
        private void frmExpireTicketFind_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            dateTimePicker1.Value = DateTime.Parse(DateTime.Now.AddYears(-1).AddDays(1).ToString("yyyy-MM-dd"));
            dateTimePicker2.Value = DateTime.Parse(DateTime.Now.AddYears(-1).AddDays(1).ToString("yyyy-MM-dd"));
            dateTimePicker3.Value = DateTime.Parse(DateTime.Now.AddYears(-1).AddDays(1).ToString("yyyy-MM-dd"));
            dateTimePicker4.Value = DateTime.Parse(DateTime.Now.AddYears(-1).AddDays(1).ToString("yyyy-MM-dd"));
            titleStep1 = btnStep1.Text;
            titleStep2 = txtStep2.Text;
            try
            {
                numericUpDown1.Value = int.Parse(EagleString.EagleFileIO.ValueOf("ExpTickFinderhStart"));
                numericUpDown2.Value = int.Parse(EagleString.EagleFileIO.ValueOf("ExpTickFindermStart"));
                numericUpDown3.Value = int.Parse(EagleString.EagleFileIO.ValueOf("ExpTickFinderhEnd"));
                numericUpDown4.Value = int.Parse(EagleString.EagleFileIO.ValueOf("ExpTickFindermEnd"));
            }
            catch
            {
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ExpTickFinderhStart", numericUpDown1.Value.ToString());
            ht.Add("ExpTickFindermStart", numericUpDown2.Value.ToString());
            ht.Add("ExpTickFinderhEnd", numericUpDown3.Value.ToString());
            ht.Add("ExpTickFindermEnd", numericUpDown4.Value.ToString());
            EagleString.EagleFileIO.WiteHashTableToEagleDotTxt(ht);
        }
        
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            
            string prog = Environment.SystemDirectory + "\\notepad.exe";
            EagleString.EagleFileIO.RunProgram(prog, filenm);
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            //此为把票号范围转换为每行一个号
            List<string> ls = new List<string>();
            string[] a = File.ReadAllLines(filenm, Encoding.Default);
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].Length == 13 || a[i].Length == 14) ls.Add(a[i]);
                else if (a[i].Length > 14)
                {
                    string[] b = a[i].Split('-');
                    int l = b.Length;
                    long start = 0;
                    long end = 0;
                    if (l == 2)
                    {
                        start = long.Parse(b[0]);
                        end = long.Parse(b[0].Substring(0, b[0].Length - b[1].Length) + b[1]);
                    }
                    else if(l==3)
                    {
                        start = long.Parse(b[0] + b[1]);
                        end = long.Parse(b[0] + b[1].Substring(0, b[1].Length - b[2].Length) + b[2]);
                    }
                    for (long cc = start; cc <= end; cc++)
                    {
                        ls.Add(cc.ToString());
                    }
                }
            }
            File.WriteAllLines(filenm, ls.ToArray(), Encoding.Default);
        }

        private void btnViewResult_Click(object sender, EventArgs e)
        {
            string filenm2 = Application.StartupPath + "\\Expired\\" + "ExpTick" + DateTime.Now.ToString("yyyyMMdd") + ".egexp";
            string prog = Environment.SystemDirectory + "\\notepad.exe";
            EagleString.EagleFileIO.RunProgram(prog, filenm2);
        }

        private void btnFromDatabase_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value < dateTimePicker1.Value) throw new Exception("销售日范围");
            if (dateTimePicker4.Value < dateTimePicker3.Value) throw new Exception("乘机日范围");
            List<string> ls = EagleBase.AccessOperation.ExpTickSelect(
                dateTimePicker1.Value,
                dateTimePicker2.Value,
                rbAnd.Checked,
                dateTimePicker3.Value,
                dateTimePicker4.Value);
            
            List<string> ls2 = EagleBase.AccessOperation.ExpTickSelectFromFinance(
                dateTimePicker1.Value,
                dateTimePicker2.Value,
                rbAnd.Checked,
                dateTimePicker3.Value,
                dateTimePicker4.Value);
            MessageBox.Show(string.Format("共增加了{0}条记录需要检测！其中BSP{1}张，网支{2}张", ls.Count + ls2.Count, ls2.Count, ls.Count));
            ls.AddRange(ls2);
            
            ls.InsertRange(0, File.ReadAllLines(filenm, Encoding.Default));
            File.WriteAllLines(filenm, ls.ToArray(), Encoding.Default);
            
        }
        //不导入易格的报表了，直接从database.mdb中选择
        private void btnImportEg_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("视您的数据库大小，可能会花很长时间！请耐心等待！", "注意", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
            System.Threading.Thread thread = new System.Threading.Thread(
                new System.Threading.ParameterizedThreadStart(EagleBase.AccessOperation.ImportExpTickFromEagleFinance)
            );
            thread.Start(Application.StartupPath + "\\database.mdb");
            //EagleBase.AccessOperation.ImportExpTickFromEagleFinance(Application.StartupPath + "\\database.mdb");
        }
        private void importKnownAirReport(object sender, EventArgs e)
        {
            try
            {
                FileDialog dlg = new OpenFileDialog();
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                Application.DoEvents();
                Button button = (sender as Button);
                switch (button.Text)
                {
                    case "东航":
                        foreach (string s in dlg.FileNames)
                        {
                            this.Text = title + "-" + "正在导入" + button.Text + "报表" + s;
                            Application.DoEvents();
                            EagleBase.AccessOperation.ImportExpTickFromMu(s);
                        }
                        break;
                    case "深航":
                        foreach (string s in dlg.FileNames)
                        {
                            this.Text = title + "-" + "正在导入" + button.Text + "报表" + s;
                            Application.DoEvents();
                            EagleBase.AccessOperation.ImportExpTickFromZh(s);
                        }
                        break;
                    case "山东航":
                        foreach (string s in dlg.FileNames)
                        {
                            this.Text = title + "-" + "正在导入" + button.Text + "报表" + s;
                            Application.DoEvents();
                            EagleBase.AccessOperation.ImportExpTickFromSc(s);
                        }
                        break;
                    case "国航":
                        foreach (string s in dlg.FileNames)
                        {
                            this.Text = title + "-" + "正在导入" + button.Text + "报表" + s;
                            Application.DoEvents();
                            EagleBase.AccessOperation.ImportExpTickFromCa(s);
                        }
                        break;
                    case "海航":
                        foreach (string s in dlg.FileNames)
                        {
                            this.Text = title + "-" + "正在导入" + button.Text + "报表" + s;
                            Application.DoEvents();
                            EagleBase.AccessOperation.ImportExpTickFromHu(s);
                        }
                        break;
                    case "上航":
                        foreach (string s in dlg.FileNames)
                        {
                            this.Text = title + "-" + "正在导入" + button.Text + "报表" + s;
                            Application.DoEvents();
                            EagleBase.AccessOperation.ImportExpTickFromFm(s);
                        }
                        break;
                    case "鹰联":
                        foreach (string s in dlg.FileNames)
                        {
                            this.Text = title + "-" + "正在导入" + button.Text + "报表" + s;
                            Application.DoEvents();
                            EagleBase.AccessOperation.ImportExpTickFromEu(s);
                        }
                        break;
                }
                MessageBox.Show("导入" + button.Text + "报表完毕！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Text = title;
        }
        string fileCustom;
        
        string titleStep1 = "";
        string titleStep2 = "";
        private void btnStep1_Click(object sender, EventArgs e)
        {
            
            FileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                btnStep1.Text = dlg.FileName;
                fileCustom = dlg.FileName;
            }
        }

        private void btnCustomImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnStep1.Text == titleStep1) throw new Exception("未选择文件");
                bool bCsv = fileCustom.ToUpper().EndsWith("CSV");
                if (!bCsv)
                {
                    if (txtStep2.Text == "" || txtStep2.Text == titleStep2) throw new Exception("XLS文件请输入表名");
                }
                if (numericUpDown5.Value == 0) throw new Exception("必须选择起始票号列,若无终止票号列,可选择同一列。若终止票号列为0,则视起始票号列内容如9991234567890-990的格式");
                if (numericUpDown7.Value == 0) throw new Exception("销售日期列不能为0，若无该列，则可与航班日期选择同一列；若航班日期列为0，则视为与销售日期相同");
                int colBeg = (int)numericUpDown5.Value - 1;
                int colEnd = (int)numericUpDown6.Value - 1;
                int colSale = (int)numericUpDown7.Value - 1;
                int colFlight = (int)numericUpDown8.Value - 1;
                if (colFlight == -1) colFlight = colSale;
                if (colEnd == -1)
                {
                    EagleBase.AccessOperation.ImportExpTickFrom(fileCustom, txtStep2.Text, colBeg, colSale, colFlight);
                }
                else
                {
                    EagleBase.AccessOperation.ImportExpTickFrom(fileCustom, txtStep2.Text, colBeg, colEnd, colSale, colFlight, false);
                }
                throw new Exception("导入自定义报表完毕");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker4.Value = dateTimePicker2.Value;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker3.Value = dateTimePicker1.Value;
        }
    }
}