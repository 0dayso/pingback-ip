using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ePlus.Data
{
    public class PolicySelect
    {
        public DataTable dt航班信息;
        public DataTable dt乘客信息;
        public DataTable dt政策信息;
        public DataTable dt票价信息;
        public PolicySelect()
        {
            dt航班信息 = new DataTable();
            dt乘客信息 = new DataTable();
            dt政策信息 = new DataTable();
            string[] arr = new string[] 
                    {"航班日期","起飞时间","到达时间","记录编号","订座状态","航空公司","航班号","舱位","价格","机型","燃油/机建" };
            for (int i = 0; i < arr.Length; i++)
            {
                dt航班信息.Columns.Add(arr[i]);
            }
            string[] arr乘客 = new string[] 
                    { "乘机人1", "证件号1", "乘机人类型1", "乘机人2", "证件号2", "乘机人类型2", "乘机人3", "证件号3", "乘机人类型3" };
            for (int i = 0; i < arr乘客.Length; i++)
            {
                dt乘客信息.Columns.Add(arr乘客[i]);
            }
            string[] arr政策 = new string[] 
                    {"适用政策","票证类型","同行返点","单张结算价","单张代理费","出票或废票时间","出票速度","选中适用政策" };
            for (int i = 0; i < arr政策.Length; i++)
            {
                dt政策信息.Columns.Add(arr政策[i]);
            }
        }
        public void FillTables(string[] arr航班信息, string[] arr乘客信息, string[] arr政策信息)
        {
            for (int i = 0; i < arr航班信息.Length; i++)
            {
                string[] ar = arr航班信息[i].Split(',');
                DataRow dr = dt航班信息.NewRow();
                for (int j = 0; j < ar.Length; j++)
                {
                    dr[j] = ar[j];
                }
                dt航班信息.Rows.Add(dr);
            }
            for (int i = 0; i < arr乘客信息.Length; i = i + 3)
            {
                string temp = "";
                temp += arr乘客信息[i];
                if ((i + 1) < arr乘客信息.Length)
                {
                    temp += "-" + arr乘客信息[i + 1];
                    if ((i + 2) < arr乘客信息.Length)
                    {
                        temp += "-" + arr乘客信息[i + 2];
                    }
                }
                DataRow dr = dt乘客信息.NewRow();
                string[] ar = temp.Split('-');
                for (int j = 0; j < ar.Length; j++)
                {
                    dr[j] = ar[j];
                }
                dt乘客信息.Rows.Add(dr);
            }
        }
    }
}
