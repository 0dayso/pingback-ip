#define receipt_
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using gs.para;
using System.Net.Sockets;
using System.Net;
using System.Collections;

namespace ePlus
{
    class GlobalVar2
    {
        /// <summary>
        /// 0:ԭ��ͨ�û�,1:BtoC�û�
        /// </summary>
        static public int gbUserModel = 0;

        static public YzpBtoC.YZPnewOrder gbYzpOrder;// = new YzpBtoC.YZPnewOrder();

        static public bool registered = false;
        static public bool b_���ð� = false;
        static public string xmlPolicies = "";

        static public string bxUserAccount = "";
        static public string bxPassWord = "";
        static public string bxTelephone = "";
        static public System.Globalization.DateTimeFormatInfo gbDtfi = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
        static public bool bTempus = false;//tempus�ı��մ�ӡ���
        static public int gbConnectType = 1;//0:Ĭ�ϣ�1:���ţ�2:��ͨ
        static public string gbFromto = "";//avָ���е�fromto
        static public bool gbUsingBlackWindows = true;
        static public bool gbDisplayPolicy = false;

        static public int gbFeeOfDec = 0;
        static public string gbPnrOfEtdzing = "";

        
        /// <summary>
        /// �Ƿ�ʹ����eplus��񣬽���������ģʽ
        /// </summary>
        static public bool gbEplusStyle = false;

        /// <summary>
        /// �г̵���ӡ��ʱ�������Է�û�������
        /// </summary>
        static public string tmpPrintName = "";
        /// <summary>
        /// �г̵���ӡ��ʱPNR���Է�û���PNR
        /// </summary>
        static public string tmpPrintPnr = "";
        /// <summary>
        /// ���ڴ���Ѿ��ύ��PNR����Ӧȡ��ʱ�䡣
        /// </summary>
        static public Hashtable gbHashTableOfTheSubmittingPnr = new Hashtable();
        static public string strFare_Tax_Gain_1 = "0~0~0~0";//����1�� Ʊ��+˰+����
        static public string strFare_Tax_Gain_2 = "0~0~0~0";//����2�� Ʊ��+˰+����


        static public string gbPassegersInEasyVersion = "";//���װ�˿����������֤���绰��;
        
#if !receipt
        //static public BookTicket bookTicket = new BookTicket(false);
#else
        static public BookTicket bookTicket;
#endif
        //���ܴ��ڲ�ͬ�۸�ĺ���
        static public void differenceFC(string cityBeg, string cityEnd)
        {
            string city1 = cityBeg.ToLower().Trim();
            string city2 = cityEnd.ToLower().Trim();
            bool bDiff = false;
            if ((city1 == "ngb" && city2 == "sha") || (city1 == "sha" && city2 == "ngb"))
                bDiff = true;
            if ((city1 == "ngb" && city2 == "pvg") || (city1 == "pvg" && city2 == "ngb"))
                bDiff = true;
            if (bDiff) MessageBox.Show("ע�⣺�˺��ζ��ڲ�ͬ���չ�˾���ڲ�ͬ�۸����ں�������FD��ѯʵ�ʼ۸�");
        }
    }
}
