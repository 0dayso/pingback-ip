using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ePlus.BookSimple
{
    public partial class ToGroup : Form
    {
        
        public ToGroup()
        {
            InitializeComponent();
        }


        public string listcontent = "1,产品名称,WUHSHA,MU2501,2006-10-31,0.6,20,XXXXX,10,备注;2,产品名称,WUHPEK,CA3177,2006-10-31,0.8,15,YYYYY,5,备注";
        public string fromto = "";
        public string date = "";

        //369,WUHSHA,XXXXX,2007-05-09,10,1000,YYYYY,测试,请不要入团,0
        // 0     2     3      4       5   6     7    1     9       8   
        private void ToGroup_Load(object sender, EventArgs e)
        {
            setlistview();
        }
        void setlistview()
        {
            if (listcontent == "") return;
            lvToGroup.Items.Clear();
            string[] splitLine = { "<eg666>" };
            string[] splitField = { "<eg66>" };
            //string[] lsrows = listcontent.Split('~');
            string[] lsrows = listcontent.Split(splitLine, StringSplitOptions.None);
            try
            {
                for (int irows = 0; irows < lsrows.Length; irows++)
                {

                    //string[] lscols_old = lsrows[irows].Split(',');
                    string[] lscols_old = lsrows[irows].Split(splitField,StringSplitOptions.None);
                    string[] lscols = { lscols_old[0], lscols_old[7], lscols_old[1], lscols_old[2], lscols_old[3], lscols_old[4], lscols_old[5], lscols_old[7], lscols_old[9], lscols_old[8] };

                    System.Windows.Forms.ListViewItem item = new ListViewItem();
                    item.Text = lscols[0];
                    for (int icols = 1; icols < lscols.Length; icols++)
                    {

                        item.SubItems.Add(lscols[icols]);
                    }
                    lvToGroup.Items.Add(item);
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show("setlistview" + ee.Message);
            }
            bool bEven = true;
            for (int i = 0; i < lvToGroup.Items.Count; i++)
            {
                bEven = !bEven;
                if (bEven) lvToGroup.Items[i].BackColor = System.Drawing.Color.Pink;
                else lvToGroup.Items[i].BackColor = System.Drawing.Color.White;

            }
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            selectclick();
            btUdate_Click(sender, e);
        }
        private void selectclick()
        {
            if (lvToGroup.SelectedItems.Count != 1) return;
            string id = "加入：";
            for (int i = 2; i < 6; i++)
            {
                id += " →" + lvToGroup.SelectedItems[0].SubItems[i].Text;
            }
            this.TopMost = false;
            AddPassenger ap = new AddPassenger();
            ap.promote = id;
            ap.total = lvToGroup.SelectedItems[0].SubItems[6].Text;
            ap.booked = lvToGroup.SelectedItems[0].SubItems[8].Text;
            ap.groupid = lvToGroup.SelectedItems[0].SubItems[0].Text;
            ap.ShowDialog();
            

        }
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btUdate_Click(object sender, EventArgs e)
        {
            CSListGroupTicket gt = new CSListGroupTicket();
            gt.fromto = fromto;
            gt.date = date;

            listcontent = gt.listgroupticket();
            setlistview();
        }

        private void lvToGroup_DoubleClick(object sender, EventArgs e)
        {
            if (DateTime.Compare(DateTime.Parse(date), DateTime.Now) < 0)
            {
                if (DialogResult.Yes !=
                    MessageBox.Show("入团日期" + date + "比本地系统日期小，是否继续?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    return;
            }
            btSelect_Click(sender, e);
        }
        MouseEventArgs eMouseMove = null;
        private void lvToGroup_MouseMove(object sender, MouseEventArgs e)
        {
            eMouseMove = e;
            lvonmousemove(e);
            
        }
        
        private void lvonmousemove(MouseEventArgs e)
        {
            try
            {
                int clickcolumn = -1;
                int clickrow = -1;
                int xpt = 0;
                for (int i = 0; i < lvToGroup.Columns.Count; i++)
                {
                    xpt += lvToGroup.Columns[i].Width;
                    if (e.X < xpt)
                    {
                        clickcolumn = i;
                        break;
                    }
                }
                clickrow = (e.Y - 16) / this.imageList1.ImageSize.Height;
                if (lvToGroup.Items.Count < clickrow) return;
                string tipstring = "";
                tipstring += "产品：" + lvToGroup.Items[clickrow].SubItems[1].Text + "\r\n";

                //if (icols == 9)
                {
                    int width = 100;
                    string remarktext = lvToGroup.Items[clickrow].SubItems[9].Text;
                    string[] array = remarktext.Split('\n');
                    for (int i = 0; i < array.Length; i++)
                    {
                        int len = System.Text.Encoding.Default.GetBytes(array[i]).Length / width;
                        for (int j = len; j > 0; j--)
                        {
                            array[i] = array[i].Insert(j * width / 2, "\n");
                        }
                    }
                    string strRemark = "";
                    for (int i = 0; i < array.Length; i++)
                    {
                        strRemark += array[i] + "\r\n";
                    }
                    tipstring += "备注：" + strRemark;//备注内容
                }

                
                ttTooltip.SetToolTip(lvToGroup, tipstring);
            }
            catch
            {
            }
        }

        private void ToGroup_Activated(object sender, EventArgs e)
        {
            this.TopMost = false;
        }

        private void ToGroup_MouseMove(object sender, MouseEventArgs e)
        {
            this.TopMost = false;
        }

        private void lvToGroup_MouseHover(object sender, EventArgs e)
        {
            //lvonmousemove(eMouseMove);
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            if (this.lvToGroup.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选择一个产品","PLEASE",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            //MessageBox.Show("将导出到 我的文档 目录中");
            try
            {
                string f = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\" + this.lvToGroup.SelectedItems[0].SubItems[1].Text + ".doc";
                MessageBox.Show("将导出到 我的文档 目录中" + f, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FileStream fs = new FileStream(f, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(this.lvToGroup.SelectedItems[0].SubItems[9].Text);//备注
                sw.Close();
                fs.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("导出错误" + ee.Message,"FATAL ERROR",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            //SaveFileDialog dlg = new SaveFileDialog();
            //dlg.Title = "导出备注";
            //dlg.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //dlg.FileName = this.lvToGroup.SelectedItems[0].SubItems[1].Text;// "产品名称";//产品名称
            //dlg.DefaultExt = "DOC";
            //dlg.Filter = "WORD文档 (*.doc)|*.doc|所有文件 (*.*)|*.*";
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    FileStream fs = new FileStream(dlg.FileName, FileMode.OpenOrCreate, FileAccess.Write);
            //    StreamWriter sw = new StreamWriter(fs);
            //    sw.Write(this.lvToGroup.SelectedItems[0].SubItems[9].Text);//备注
            //    sw.Close();
            //    fs.Close();
            //}
        }

    }
}