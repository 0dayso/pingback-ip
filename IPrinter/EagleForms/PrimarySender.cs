using System;
using System.Collections.Generic;
using System.Text;
namespace EagleForms
{
    public partial class Primary
    {
        /// <summary>
        /// һ����Ʊ�����·��ͼ�ʱ��Ҫ����,�����Ϸ���ʱ���׳��쳣��Ϣ
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
            //0.�������ʹ�õ������Ƿ�����ͬһ��OFFICE
            if (loginInfo.b2b.lr.IpidUsingIsSameOffice() == "") throw new Exception("\r\n��ѡ�����ã�");
            //1.Ԥ�ύ
            bool bflag = false;
            wserviceKernal.PreSubmitEticketWhenEtdz(loginInfo.b2b.username,commandPool.PNRing,0,loginInfo.b2b.lr.UsingOffice(),ref bflag);
            if (!bflag) throw new Exception("\r\nԤ�ύʧ�ܣ�");
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
                throw new Exception("\r\nȡ��PNRʧ�ܣ�������");
            }
        }
        private void sender_ss()
        {
            if (commandPool.LAST.ToLower().IndexOf("/rr") >= 0)
            {
                throw new Exception("\r\n������ֱ��RR��Ʊ");
            }
        }
        private void sender_modifying()
        {
            if (EagleString.egString.right(commandPool.LAST.ToLower(), 2) == "rr")
            {
                if (!loginInfo.b2b.lr.AuthorityOfCommand("rr"))
                {
                    throw new Exception("\r\n��RRȨ��");
                }
            }
        }
    }
}
