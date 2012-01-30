using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace EagleFinance
{
    public partial class Backup : Form
    {
        public Backup()
        {
            InitializeComponent();
        }

        private void Backup_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(Application.StartupPath + "\\etmconfig.txt");
            string dir = sr.ReadLine();
            sr.Close();
            tbDir.Text = dir;
            btSelectAutoBackupDirectory.Enabled = false;
        }

        private void btSelectAutoBackupDirectory_Click(object sender, EventArgs e)
        {
        }

        private void Backup_FormClosed(object sender, FormClosedEventArgs e)
        {
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\etmconfig.txt", false);
            sw.WriteLine(tbDir.Text);
            sw.Close();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btBackupManual_Click(object sender, EventArgs e)
        {
            try
            {
                string destFile = DateTime.Now.Ticks.ToString() + ".bak";
                string dir = tbDir.Text.Trim();
                if (!Directory.Exists(dir)) throw new Exception("路径不存在！");
                if (dir[dir.Length - 1] != '\\') dir += "\\";
                File.Copy(Application.StartupPath + "\\database.mdb", dir + destFile);
                MessageBox.Show("备份完毕！文件名为：" + dir + destFile);
                GlobalVar.bModified = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}