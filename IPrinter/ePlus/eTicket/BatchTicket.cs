using System;
using System.Collections.Generic;
using System.Text;

namespace ePlus.eTicket
{
    public class PnrStatInBatchTicket
    {
        public string pnr;
        
        public string office = "";
        int stat = 0;//	1.未被下载，2.正被处理，3.信息不全，4.出票成功但未扣款，5.出票失败，6.出票并已扣款
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
