using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace EagleFinance
{
    public partial class Delete : Form
    {
        public Delete()
        {
            InitializeComponent();
        }

        private void NumberBeg_TextChanged(object sender, EventArgs e)
        {
            NumberEnd.Text = NumberBeg.Text;
        }

        private void btIncoming_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除吗?", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (this.NumberBeg.Text.Trim().Length != 10) throw new Exception("请正确填写起始票号，10位！");
                    if (this.NumberEnd.Text.Trim().Length != 10) throw new Exception("请正确填写终止票号，10位！");
                    long numbeg = 0;
                    long numend = 0;
                    if (!long.TryParse(this.NumberBeg.Text, out numbeg)) throw new Exception("请正确填写起始票号，10位！只能有数字");
                    if (!long.TryParse(this.NumberEnd.Text, out numend)) throw new Exception("请正确填写终止票号，10位！只能有数字");
                    string cmString = string.Format("delete from etickets where TKTNUMBER>='{0}' and TKTNUMBER <='{1}'",
                        numbeg.ToString(), numend.ToString());
                    OleDbCommand cmd = new OleDbCommand(cmString, GlobalVar.cn);

                    int rows = cmd.ExecuteNonQuery();
                    throw new Exception("共删除" + rows.ToString() + "条");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("警告：将清空数据库，清空前请先备份数据，是否真的要清空", "",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    string cmString = string.Format("delete from etickets");
                    OleDbCommand cmd = new OleDbCommand(cmString, GlobalVar.cn);

                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show("清空成功！");
                }
                catch
                {
                    MessageBox.Show("清空失败！");
                }
            }
        }
    }
}