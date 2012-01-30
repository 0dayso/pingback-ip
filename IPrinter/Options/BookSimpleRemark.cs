using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Options
{
    public partial class BookSimpleRemark : Form
    {
        public BookSimpleRemark()
        {
            
        }
        public BookSimpleRemark(string passStr)
        {
            InitializeComponent();
            string[] arr = passStr.Split(';');
            if (arr.Length > 0)
            {
                tbName1.Text = arr[0].Split('-')[0];
                tbCard1.Text = arr[0].Split('-')[1];
            }
            if (arr.Length > 1)
            {
                tbName2.Text = arr[1].Split('-')[0];
                tbCard2.Text = arr[1].Split('-')[1];
            }
            if (arr.Length > 2)
            {
                tbName3.Text = arr[2].Split('-')[0];
                tbCard3.Text = arr[2].Split('-')[1];
            }
            if (arr.Length > 3)
            {
                tbName4.Text = arr[3].Split('-')[0];
                tbCard4.Text = arr[3].Split('-')[1];
            }
            if (arr.Length > 4)
            {
                tbName5.Text = arr[4].Split('-')[0];
                tbCard5.Text = arr[4].Split('-')[1];
            }
            if (arr.Length > 5)
            {
                tbName6.Text = arr[5].Split('-')[0];
                tbCard6.Text = arr[5].Split('-')[1];
            }
            if (arr.Length > 6)
            {
                tbName7.Text = arr[6].Split('-')[0];
                tbCard7.Text = arr[6].Split('-')[1];
            }
            if (arr.Length > 7)
            {
                tbName8.Text = arr[7].Split('-')[0];
                tbCard8.Text = arr[7].Split('-')[1];
            }
            if (arr.Length > 8)
            {
                tbName9.Text = arr[8].Split('-')[0];
                tbCard9.Text = arr[8].Split('-')[1];
            }
        }
        public string remark = "";
        public string passString = "";
        private void btOK_Click(object sender, EventArgs e)
        {

        }

        private void BookSimpleRemark_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            remark = this.textBox1.Text;
            //1
            if (tbName1.Text.Trim() != "")
            {
                try
                {
                    if(tbTele1.Text.Trim()!="")
                        long.Parse(tbTele1.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("第1名旅客的电话号码输入有误，只需要输入数字");
                    return;
                }
                passString +=
                    ";" + tbName1.Text.Trim() + "-" + tbCard1.Text.Trim() + "-" + tbTele1.Text.Trim();
            }
            //2
            if (tbName2.Text.Trim() != "")
            {
                try
                {
                    if (tbTele2.Text.Trim() != "")
                    long.Parse(tbTele2.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("第2名旅客的电话号码输入有误，只需要输入数字");
                    return;
                }
                passString +=
                    ";" + tbName2.Text.Trim() + "-" + tbCard2.Text.Trim() + "-" + tbTele2.Text.Trim();
            }
            //3
            if (tbName3.Text.Trim() != "")
            {
                try
                {
                    if (tbTele3.Text.Trim() != "")
                    long.Parse(tbTele3.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("第3名旅客的电话号码输入有误，只需要输入数字");
                    return;
                }
                passString +=
                    ";" + tbName3.Text.Trim() + "-" + tbCard3.Text.Trim() + "-" + tbTele3.Text.Trim();
            }
            //4
            if (tbName4.Text.Trim() != "")
            {
                try
                {
                    if (tbTele4.Text.Trim() != "")
                    long.Parse(tbTele4.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("第4名旅客的电话号码输入有误，只需要输入数字");
                    return;
                }
                passString +=
                    ";" + tbName4.Text.Trim() + "-" + tbCard4.Text.Trim() + "-" + tbTele4.Text.Trim();
            }
            //5
            if (tbName5.Text.Trim() != "")
            {
                try
                {
                    if (tbTele5.Text.Trim() != "")
                    long.Parse(tbTele5.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("第5名旅客的电话号码输入有误，只需要输入数字");
                    return;
                }
                passString +=
                    ";" + tbName5.Text.Trim() + "-" + tbCard5.Text.Trim() + "-" + tbTele5.Text.Trim();
            }
            //6
            if (tbName6.Text.Trim() != "")
            {
                try
                {
                    if (tbTele6.Text.Trim() != "")
                    long.Parse(tbTele6.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("第6名旅客的电话号码输入有误，只需要输入数字");
                    return;
                }
                passString +=
                    ";" + tbName6.Text.Trim() + "-" + tbCard6.Text.Trim() + "-" + tbTele6.Text.Trim();
            }
            //7
            if (tbName7.Text.Trim() != "")
            {
                try
                {
                    if (tbTele7.Text.Trim() != "")
                    long.Parse(tbTele5.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("第7名旅客的电话号码输入有误，只需要输入数字");
                    return;
                }
                passString +=
                    ";" + tbName7.Text.Trim() + "-" + tbCard7.Text.Trim() + "-" + tbTele7.Text.Trim();
            }
            //8
            if (tbName8.Text.Trim() != "")
            {
                try
                {
                    if (tbTele8.Text.Trim() != "")
                    long.Parse(tbTele8.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("第8名旅客的电话号码输入有误，只需要输入数字");
                    return;
                }
                passString +=
                    ";" + tbName8.Text.Trim() + "-" + tbCard8.Text.Trim() + "-" + tbTele8.Text.Trim();
            }
            //9
            if (tbName9.Text.Trim() != "")
            {
                try
                {
                    if (tbTele9.Text.Trim() != "")
                    long.Parse(tbTele9.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("第9名旅客的电话号码输入有误，只需要输入数字");
                    return;
                }
                passString +=
                    ";" + tbName9.Text.Trim() + "-" + tbCard9.Text.Trim() + "-" + tbTele9.Text.Trim();
            }
            if (passString.Length > 0) passString = passString.Substring(1);
            btOK_Click(null, null);
            this.Close();
        }
    }
}