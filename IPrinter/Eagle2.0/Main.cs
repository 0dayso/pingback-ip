using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using EagleString;
using EagleForms;
using EagleForms.General;


namespace Eagle2
{
    public class Main:Form
    {
        Sunisoft.IrisSkin.SkinEngine skin = new Sunisoft.IrisSkin.SkinEngine();
        Login frmLogin;
        Primary frmMain;
        public Main()
        {
            this.Hide();
            frmLogin = new Login();
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                skin.SkinFile = Application.StartupPath + "\\MacOS.ssk";
                frmMain = new Primary();
                frmMain.loginInfo = frmLogin.LOGININFO;
                frmMain.ShowDialog();
            }
            //frmLogin.login2server_rwy("ceshi", "ceshi");
        }
        private void Main_Load(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Main_Shown(object sender, EventArgs e)
        {
            this.Close();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(104, 24);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
        }
    }
}
