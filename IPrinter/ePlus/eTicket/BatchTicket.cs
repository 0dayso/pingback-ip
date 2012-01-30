using System;
using System.Collections.Generic;
using System.Text;

namespace ePlus.eTicket
{
    public class PnrStatInBatchTicket
    {
        public string pnr;
        
        public string office = "";
        int stat = 0;//	1.δ�����أ�2.��������3.��Ϣ��ȫ��4.��Ʊ�ɹ���δ�ۿ5.��Ʊʧ�ܣ�6.��Ʊ���ѿۿ�
    }
    class BatchTicket
    {
        PnrStatInBatchTicket[] pnrStatInBatchTicket = null;
        public BatchTicket(string pnrstring)//pnr1-office1,pnr2-office2
        {
            string[] pnrArray = pnrstring.Split(',');
            pnrStatInBatchTicket = new PnrStatInBatchTicket[pnrArray.Length];
            for (int i = 0; i < pnrArray.Length; i++)
            {
                pnrStatInBatchTicket[i] = new PnrStatInBatchTicket();
                pnrStatInBatchTicket[i].pnr = pnrArray[i].Split('-')[0];
                try
                {
                    pnrStatInBatchTicket[i].office = pnrArray[i].Split('-')[1];
                }
                catch
                {
                }
            }
        }
    }

}
