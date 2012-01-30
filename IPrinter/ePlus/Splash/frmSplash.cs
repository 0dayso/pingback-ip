using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SplashScreen
{
    public partial class frmSplash : Form,ISplashForm
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        #region ISplashForm

        void ISplashForm.SetStatusInfo(string NewStatusInfo)
        {
            lbStatusInfo.Text = NewStatusInfo;
        }

        void ISplashForm.SetBanner(System.Drawing.Image Banner)
        {
            picLogo.Image = Banner;
            this.Width = Banner.Width;
            this.Height = Banner.Height + lbStatusInfo.Height + 3;
        }

        #endregion
    }
}