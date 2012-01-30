using System;
using System.Collections.Generic;
using System.Text;

namespace EagleWebService
{
    public class IbeStuff
    {
        public static bool RefreshIbeUrl()
        {
            EagleWebService.wsKernal ws = new EagleWebService.wsKernal(Options.GlobalVar.B2bWebServiceURL);
            NewPara np = new NewPara();
            np.AddPara("cm", "GetIbeUrl");
            np.AddPara("UserName", Options.GlobalVar.B2bLoginName);
            np.AddPara("ibeID", Options.GlobalVar.IbeID.ToString());
            string strReq = np.GetXML();

            RemoteKernal.RemoteStatus statuTemp = RemoteKernal.status;//暂存
            RemoteKernal.status = RemoteKernal.RemoteStatus.DISCONNECT;
            string strRet;

            try
            {
                 strRet = ws.getEgSoap(strReq);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("获取 IBE 地址发生错误！");
                EagleString.EagleFileIO.LogWrite("无法得到IBE地址，错误是：" + ex.Message);
                return false;
            }

            RemoteKernal.status = statuTemp;//还原

            if (string.IsNullOrEmpty(strRet) || strRet.Contains("CmErr"))
            {
                //System.Windows.Forms.MessageBox.Show("未能从服务器获取到IBE地址！详细信息如下：\n\n" + strRet);
                EagleString.EagleFileIO.LogWrite("无法得到IBE地址，错误是：" + strRet);
                return false;
            }
            else
            {
                np = new NewPara(strRet);

                //if (np.FindTextByPath("//eg/cm") == "RetGetIbeUrl")
                {
                    Options.GlobalVar.IbeUrl = np.FindTextByPath("//eg/ibeURL");
                    Options.GlobalVar.IbeID = Int32.Parse(np.FindTextByPath("//eg/ibeID"));
                    EagleString.EagleFileIO.LogWrite("切换IBE地址至：" + Options.GlobalVar.IbeUrl);
                    return true;
                }
            }
        }
    }
}
