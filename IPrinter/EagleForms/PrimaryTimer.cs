using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using EagleString;
using EagleControls;
using EagleProtocal;
using EagleExtension;
using EagleWebService;
namespace EagleForms
{
    public partial class Primary
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private void InitTimer()
        {
            
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {

            timer_ScrollNotice();
            (new Thread(new ThreadStart(timer_upload_log))).Start();
        }
        void timer_ScrollNotice()
        {
            if (OuterCall) return;
            if (scrollNotice1.UPDATETEXT)
            {
                scrollNotice1.SetText(wserviceKernal.Get_Notice_Scroll(loginInfo.b2b.username, "0"));
            }
            if (!uploadEticketInfo.RUNNING)
            {
                (new Thread(new ThreadStart(uploadEticketInfo.Start))).Start();
            }
        }
        void timer_upload_log()
        {
            EagleExtension.EagleExtension.LogUpload(loginInfo, 256);
        }
        void timer_check_unsubmit_eticket()//后台提交电子客票信息未完成
        {
            string pnr = wserviceKernal.PnrUnchecked(loginInfo.b2b.username);
            if (!string.IsNullOrEmpty(pnr))
            {
                if (!EagleString.BaseFunc.PnrValidate(pnr))
                {
                }
            }
        }
    }
}
