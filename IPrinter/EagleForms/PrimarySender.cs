using System;
using System.Collections.Generic;
using System.Text;
namespace EagleForms
{
    public partial class Primary
    {
        /// <summary>
        /// 一键出票及按下发送键时需要调用,不符合发送时，抛出异常信息
        /// </summary>
        private void CheckBeforeSend()
        {
            switch (commandPool.TYPE)
            {
                case EagleString.ETERM_COMMAND_TYPE.XEPNR:
                    sender_xepnr();
                    break;
                case EagleString.ETERM_COMMAND_TYPE.SS:
                    sender_ss();
                    break;
                case EagleString.ETERM_COMMAND_TYPE.SS_ONLY:
                    sender_ss();
                    break;
                case EagleString.ETERM_COMMAND_TYPE.MODIFYING:
                    sender_modifying();
                    break;
                case EagleString.ETERM_COMMAND_TYPE.ETDZ:
                    sender_etdz();
                    break;
            }
        }
        private void sender_etdz()
        {
            if (loginInfo.b2b.username == "bb") return;
            //0.检查正在使用的配置是否属于同一个OFFICE
            if (loginInfo.b2b.lr.IpidUsingIsSameOffice() == "") throw new Exception("\r\n请选择配置！");
            //1.预提交
            bool bflag = false;
            wserviceKernal.PreSubmitEticketWhenEtdz(loginInfo.b2b.username,commandPool.PNRing,0,loginInfo.b2b.lr.UsingOffice(),ref bflag);
            if (!bflag) throw new Exception("\r\n预提交失败！");
        }
        private void sender_xepnr()
        {
            bool bflag = false;
            wserviceKernal.SubmitPnrState(loginInfo.b2b.username
                , commandPool.PNRing
                , 2
                , loginInfo.b2b.lr.UsingOffice()
                , ref bflag);
            if (!bflag)
            {
                throw new Exception("\r\n取消PNR失败，请重试");
            }
        }
        private void sender_ss()
        {
            if (commandPool.LAST.ToLower().IndexOf("/rr") >= 0)
            {
                throw new Exception("\r\n不允许直接RR订票");
            }
        }
        private void sender_modifying()
        {
            if (EagleString.egString.right(commandPool.LAST.ToLower(), 2) == "rr")
            {
                if (!loginInfo.b2b.lr.AuthorityOfCommand("rr"))
                {
                    throw new Exception("\r\n无RR权限");
                }
            }
        }
    }
}
