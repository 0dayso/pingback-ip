using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ePlus.Data
{
    public class PolicySelect
    {
        public DataTable dt������Ϣ;
        public DataTable dt�˿���Ϣ;
        public DataTable dt������Ϣ;
        public DataTable dtƱ����Ϣ;
        public PolicySelect()
        {
            dt������Ϣ = new DataTable();
            dt�˿���Ϣ = new DataTable();
            dt������Ϣ = new DataTable();
            string[] arr = new string[] 
                    {"��������","���ʱ��","����ʱ��","��¼���","����״̬","���չ�˾","�����","��λ","�۸�","����","ȼ��/����" };
            for (int i = 0; i < arr.Length; i++)
            {
                dt������Ϣ.Columns.Add(arr[i]);
            }
            string[] arr�˿� = new string[] 
                    { "�˻���1", "֤����1", "�˻�������1", "�˻���2", "֤����2", "�˻�������2", "�˻���3", "֤����3", "�˻�������3" };
            for (int i = 0; i < arr�˿�.Length; i++)
            {
                dt�˿���Ϣ.Columns.Add(arr�˿�[i]);
            }
            string[] arr���� = new string[] 
                    {"��������","Ʊ֤����","ͬ�з���","���Ž����","���Ŵ����","��Ʊ���Ʊʱ��","��Ʊ�ٶ�","ѡ����������" };
            for (int i = 0; i < arr����.Length; i++)
            {
                dt������Ϣ.Columns.Add(arr����[i]);
            }
        }
        public void FillTables(string[] arr������Ϣ, string[] arr�˿���Ϣ, string[] arr������Ϣ)
        {
            for (int i = 0; i < arr������Ϣ.Length; i++)
            {
                string[] ar = arr������Ϣ[i].Split(',');
                DataRow dr = dt������Ϣ.NewRow();
                for (int j = 0; j < ar.Length; j++)
                {
                    dr[j] = ar[j];
                }
                dt������Ϣ.Rows.Add(dr);
            }
            for (int i = 0; i < arr�˿���Ϣ.Length; i = i + 3)
            {
                string temp = "";
                temp += arr�˿���Ϣ[i];
                if ((i + 1) < arr�˿���Ϣ.Length)
                {
                    temp += "-" + arr�˿���Ϣ[i + 1];
                    if ((i + 2) < arr�˿���Ϣ.Length)
                    {
                        temp += "-" + arr�˿���Ϣ[i + 2];
                    }
                }
                DataRow dr = dt�˿���Ϣ.NewRow();
                string[] ar = temp.Split('-');
                for (int j = 0; j < ar.Length; j++)
                {
                    dr[j] = ar[j];
                }
                dt�˿���Ϣ.Rows.Add(dr);
            }
        }
    }
}
