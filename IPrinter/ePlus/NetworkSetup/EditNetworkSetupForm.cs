using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus.NetworkSetup
{
    public partial class EditNetworkSetupForm : Form
    {
        private EditNetworkSetupForm()
        {
            InitializeComponent();
        }

        public EditNetworkSetupForm(ServerInfo serverInfo)
            : this()
        {
            if (serverInfo != null)
            {
                if (serverInfo.ID > 0)
                    this.Text = "修改服务器“" + serverInfo.Name + "”登录信息";
                else
                    this.Text = "添加服务器登录信息";

                txtName.Text = serverInfo.Name;
                txtAddress.Text = serverInfo.Address == "" ? "" : serverInfo.Address + ":" + serverInfo.Port;
                txtDescript.Text = serverInfo.Description;
                rbByPassword.Checked = serverInfo.AuthenticationType;
                rbByAddress.Checked = !rbByPassword.Checked;
                txtUserName.Text = serverInfo.UserName;
                txtPassword.Text = serverInfo.Password;
                cbDefault.Checked = serverInfo.IsDefault;
                cbSaveUserPassword.Checked = serverInfo.Password != "";
            }

            m_ServerInfo = serverInfo; 
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (m_ServerInfo != null)
            {
                string[] ipAndPort = txtAddress.Text.Split(':');   
                m_ServerInfo.Edit();

                m_ServerInfo.Name = txtName.Text;  
                m_ServerInfo.Address = ipAndPort[0];
                Properties.Settings settings = new ePlus.Properties.Settings();
                m_ServerInfo.Port = ipAndPort.Length > 1 && ipAndPort[1] != "" ? Microsoft.VisualBasic.Information.IsNumeric(ipAndPort[1]) ? int.Parse(ipAndPort[1]) : settings.DefaultPort : settings.DefaultPort;
                m_ServerInfo.Description = txtDescript.Text;
                m_ServerInfo.AuthenticationType = rbByPassword.Checked;
                m_ServerInfo.IsDefault = cbDefault.Checked;
                m_ServerInfo.UserName = m_ServerInfo.AuthenticationType ? txtUserName.Text : "";
                m_ServerInfo.Password = cbSaveUserPassword.Checked ? txtPassword.Text : "";

                m_ServerInfo.Save();
                Close();
            }
        }


        private ServerInfo m_ServerInfo = null;

        private void rbByPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtUserName.Enabled = txtPassword.Enabled = rbByPassword.Checked;   
        }
    }
}