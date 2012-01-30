using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace IAClass
{
    //该类用来重写VerifyRenderingInServerForm方法，该方法的控制对呈现服务端控件有很大的影响  
    public class MyPage : Page
    {
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);  
        }
    }
}
