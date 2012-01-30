using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace EagleForms.General
{
    public partial class PasswordModify : Form
    {
        private string m_wsaddr;
        private string m_username;
        private string m_oldpassword;
        public PasswordModify(string wsaddr,string username,string oldpassword)
        {
            InitializeComponent();
            m_wsaddr = wsaddr;
            m_username = username;
            m_oldpassword = oldpassword;
        }
        public PasswordModify(EagleString.LoginInfo li)
        {
            InitializeComponent();
            m_wsaddr = li.b2b.webservice;
            m_username = li.b2b.username;
            m_oldpassword = li.b2b.password;
        }
        private string m_newpassword;
        public string PASSWORD_NEW
        {
            get
            {
                return m_newpassword;
            }
        }
        public string NEWPASSWORD { get { return m_newpassword; } }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text != textBox3.Text) throw new Exception("新密码不一致！");
                if (textBox1.Text != m_oldpassword) throw new Exception("旧密码不正确！");
                EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(m_wsaddr);
                bool bSucceed = false;
                kf.ChangePassword(m_username, textBox2.Text, ref bSucceed);
                if (!bSucceed) throw new Exception("密码修改失败！请重新输入！");
                else
                {
                    m_newpassword = textBox2.Text;
                    MessageBox.Show("修改成功！");
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                m_newpassword = m_oldpassword;
                MessageBox.Show(ex.Message);
                EagleString.EagleFileIO.LogWrite("密码修改:" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}