using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus
{
    public partial class LogoutForm : Form
    {
        public LogoutForm()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Value += 1;
            if (progressBar1.Value == 100) progressBar1.Value = 0;
            if (LocalOperation.LogOperation.b_OK)
            {
                timer1.Stop();
                this.Dispose();
            }
        }

        private void LogoutForm_Load(object sender, EventArgs e)
        {
            if (GlobalVar.loginName == "") { this.Close(); this.Dispose(); }
            timer1.Start();
            ExitSystem es = new ExitSystem();

            System.Threading.Thread th1= new System.Threading.Thread(new System.Threading.ThreadStart(es.logout));
            th1.Start();

            //保存打印配置到服务器
            EagleWebService.wsInsurrance ws = new EagleWebService.wsInsurrance();
            EagleWebService.TraceEntity ret;
            EagleWebService.LogoutRequestEntity req = new EagleWebService.LogoutRequestEntity();
            req.Username = Options.GlobalVar.IAUsername;
            req.Password = Options.GlobalVar.IAPassword;
            req.OffsetX = Options.GlobalVar.IAOffsetX;
            req.OffsetY = Options.GlobalVar.IAOffsetY;
            try
            {
                ret = ws.Logout(req);
                if (!string.IsNullOrEmpty(ret.ErrorMsg))
                    EagleString.EagleFileIO.LogWrite(ret.ErrorMsg + System.Environment.NewLine + ret.Detail);
            }
            catch(Exception ee)
            {
                EagleString.EagleFileIO.LogWrite(ee.Message);
            }

            try
            {
                string[] files = System.IO.Directory.GetFiles(".", "Pt*.xml");
                foreach (string file in files)
                {
                    System.IO.File.Delete(file);
                }
            }
            catch (Exception ee)
            {
                EagleString.EagleFileIO.LogWrite(ee.Message);
            }

            XMLConfig.XMLConfigUser user = new XMLConfig.XMLConfigUser().Read() as XMLConfig.XMLConfigUser;
            user.IACode = Options.GlobalVar.IACode;
            user.Save();

            //LocalOperation.LogOperation lo = new ePlus.LocalOperation.LogOperation();
            //System.Threading.Thread th2 = new System.Threading.Thread(new System.Threading.ThreadStart(lo.writeserverlog));
            //th2.Start();
            LocalOperation.LogOperation.b_OK = true;//不上传日志了
        }
    }
}