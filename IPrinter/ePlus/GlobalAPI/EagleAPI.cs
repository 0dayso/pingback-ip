using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using gs.para;
using System.Xml;
using System.Data.OleDb;


namespace ePlus
{
    public partial class EagleAPI
    {
        public EagleAPI()
        {
        }

        //��ӡ����ȫ�ֱ���
        //static public PointF o_ticket = new PointF(0F, 0F);
        //static public PointF o_receipt = new PointF(0F, 0F);
        //static public PointF o_insurance = new PointF(0F, 0F);
        //static public float fontsizecn = 10;
        //static public float fontsizeen = 8;
        //��ʷ����ȫ�ֱ���

        //���ӿ�Ʊ�г̵����Ƿ� ��Ʊ����ȡ
        //static public bool b_rtByEticket = false;
        //static public bool b_��� = false;
        
        #region PART1 ��ȡ�г̵���Ϣ
        /*
         * ����޸�����2006.7.2
         */
        //1.ȡ��������NQ8PT
        static public List<string> GetNames(string rs)
        {
            List<string> ls = new List<string>();
            try
            {
                if (GlobalVar.b_rtByEticket)
                {
                    ls.Add(etGetName(rs));
                    return ls;
                }
                bool et = false;
                if (rs.LastIndexOf("**ELECTRONIC TICKET PNR**") >= 0) et = true;
                string[] items = rs.Split('\n');
                int thesecond = HasFirst(rs);
                int etflag = 0;//�Ƿ�ΪE-TICKET��־
                if (et) etflag = 1;//�����1��
                if (thesecond < 0) thesecond = 2 + etflag;

                if (1 + etflag >= thesecond) return ls;
                if (rs.IndexOf("NO PNR") >= 0) return ls;

                try
                {
                    bool b_group = false;
                    for (int j = 1 + etflag; j < thesecond; j++)
                    {
                        if (items[j].IndexOf(" 0.") > -1)
                        {
                            b_group = true;
                            continue;
                        }


                        if (j == thesecond - 1)
                        {
                            items[j] = substring(items[j], 0, items[j].Length - 1).Trim();//.Substring(0, items[j].Length - 1).Trim();
                            if(!b_group)items[j] = substring(items[j], 0, items[j].Length - 5);//.Substring(0, items[j].Length - 5);
                        }
                        string temp = items[j].Trim();
                        List<string> sp_temp = new List<string>();
                        for (int i = 1; i < 100; i++)
                        {
                            sp_temp.Add(i.ToString());
                        }
                        string[] sp = sp_temp.ToArray();
                        string[] names = temp.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < names.Length; i++)
                        {
                            //ȥ�����ɼ��ַ�
                            while (names[i].Length >0 && names[i][0] < 'A' ) names[i] = names[i].Substring(1);
                            while (names[i].Length > 0 && names[i][names[i].Length - 1] < 'A' && names[i].Length > 0) names[i] = names[i].Substring(0, names[i].Length - 1);
                            if (names[i].Trim() == "") continue;
                            ls.Add(names[i]);
                        }
                    }
                    mystring.sortStringListByPinYin(ls);
                    for (int i = 0; i < ls.Count; i++)
                    {
                        while (ls[i][0] < 'A') ls[i] = ls[i].Substring(1);                        
                    }
                }
                catch
                {
                    //MessageBox.Show("ȡ���������������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
            }
            catch
            {
                ;
            }
            for (int i = 1; i < 13; i++)
            {
                ls.Remove(GetMonthCode(i));
            }
            return ls;
        }
        //1.1����Ʊû��������������
        static public int GetPeopleNumber(string rs)
        {
            int beg = 0;
            int end = 0;
            beg = rs.IndexOf(" 0.");
            if (beg > -1)
            {
                beg = beg + 3;
                end = beg;
                while (rs[end] <= '9' && rs[end] >= '0')
                {
                    end++;
                }
            }
            else return 0;
            string no = rs.Substring(beg, end - beg);
            int ret = 0;
            try
            {
                ret = int.Parse(no);
            }
            catch
            {
            }
            return ret;
        }
        //2.ȡ�����
        static public string GetFlightNo(string rs)
        {
            int i = HasFirst(rs);
            if (i < 0) return "";
            try
            {
                string[] items = rs.Split('\n');
                string temp = items[i].Substring(5, 7);
                return temp.Trim();
            }
            catch
            {
                //MessageBox.Show("ȡ����1�������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        //2.01ȡ�ڶ������κ����
        static public string GetFlightNo2(string rs)
        {
            int i = HasSecond(rs);
            if (i < 0) return "";
            try
            {
                string[] items = rs.Split('\n');
                string temp = items[i].Substring(5, 7);
                return temp.Trim();
            }
            catch
            {
                //MessageBox.Show("ȡ����2�������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        //2.1������
        static public string GetCarrier(string rs)
        {
            string temp = GetFlightNo(rs);
            try
            {
                if (temp.Length < 2) return "";
                return temp.Substring(0, 2);
            }
            catch
            {
                //MessageBox.Show("ȡ������1�������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        //����2
        static public string GetCarrier2(string rs)
        {
            string temp = GetFlightNo2(rs);
            try
            {
                if (temp.Length < 2) return "";
                return temp.Substring(0, 2);
            }
            catch
            {
                //MessageBox.Show("ȡ������2�������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        //2.2�����
        static public string GetFlight(string rs)
        {
            string temp = GetFlightNo(rs);
            if (temp.Length < 5) return "";
            try
            {
                return GetFlightNo(rs).Substring(2);
            }
            catch
            {
                //MessageBox.Show("ȡ�����1�������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        //����2
        static public string GetFlight2(string rs)
        {
            string temp = GetFlightNo2(rs);
            if (temp.Length < 5) return "";
            try
            {
                return temp.Substring(2);
            }
            catch
            {
                //MessageBox.Show("ȡ�����2�������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        //3.ȡ��λ
        static public string GetClass(string rs)
        {
            int i = HasFirst(rs);
            if (i < 0) return "";
            string[] items = rs.Split('\n');
            try
            {
                string temp = items[i].Substring(12, 4);
                return temp.Trim();
            }
            catch
            {
                //MessageBox.Show("ȡ��λ1�������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        //����2
        static public string GetClass2(string rs)
        {
            int i = HasSecond(rs);
            if (i < 0) return "";
            string[] items = rs.Split('\n');
            try
            {
                string temp = items[i].Substring(12, 4);
                return temp.Trim();
            }
            catch
            {
                //MessageBox.Show("ȡ��λ2�������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

        }
        //4.ȡ�������
        static public string GetDateStart(string rs)
        {
            int i = HasFirst(rs);
            if (i < 0) return "";
            string[] items = rs.Split('\n');
            try
            {
                string temp = items[i].Substring(16, 9).Trim();
                while (!(temp[0] >= '0' && temp[0] <= '9'))
                {
                    temp = temp.Substring(1);
                }
                while (temp[temp.Length - 1] >= '0' && temp[temp.Length - 1] <= '9')
                {
                    temp = temp.Substring(0, temp.Length - 1);
                }
                return temp;
            }
            catch
            {
                //MessageBox.Show("ȡ�������1�������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

        }
        //����2
        static public string GetDateStart2(string rs)
        {
            int i = HasSecond(rs);
            if (i < 0) return "";
            string[] items = rs.Split('\n');
            try
            {
                string temp = items[i].Substring(16, 9).Trim();
                while (!(temp[0] >= '0' && temp[0] <= '9'))
                {
                    temp = temp.Substring(1);
                }
                while (temp[temp.Length - 1] >= '0' && temp[temp.Length - 1] <= '9')
                {
                    temp = temp.Substring(0, temp.Length - 1);
                }
                return temp;
            }
            catch
            {
                //MessageBox.Show("ȡ�������2�������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

        }

        //4.1ȡ���ʱ��
        static public string GetTimeStart(string rs)
        {
            int i = HasFirst(rs);
            if (i < 0) return "";
            string[] items = rs.Split('\n');
            try
            {
                string temp = items[i].Substring(38, 4);
                return temp.Trim();
            }
            catch
            {
                //MessageBox.Show("ȡ���ʱ��1�������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        //����2
        static public string GetTimeStart2(string rs)
        {
            int i = HasSecond(rs);
            if (i < 0) return "";
            string[] items = rs.Split('\n');
            try
            {
                string temp = items[i].Substring(38, 4);
                return temp.Trim();
            }
            catch
            {
                //MessageBox.Show("ȡ���ʱ��2�������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        //4.2ȡ����ʱ��

        static public string GetTimeEnd(string rs)
        {
            int i = HasFirst(rs);
            if (i < 0) return "";
            string[] items = rs.Split('\n');
            try
            {
                string temp = items[i].Substring(43, 4);
                return temp.Trim();
            }
            catch
            {
                //MessageBox.Show("ȡ����ʱ��1�������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        //����2
        static public string GetTimeEnd2(string rs)
        {
            int i = HasSecond(rs);
            if (i < 0) return "";
            string[] items = rs.Split('\n');
            try
            {
                string temp = items[i].Substring(43, 4);
                return temp.Trim();
            }
            catch
            {
                //MessageBox.Show("ȡ����ʱ��2�������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        //5.ȡ��ɵ�
        static public string GetStartCity(string rs)
        {
            int i = HasFirst(rs);
            if (i < 0) return "";
            string[] items = rs.Split('\n');
            //string temp = items[i].Substring(25, 3);
            string temp = substring(items[i], 25, 3);
            return temp.Trim(); 
        }

        static public string GetStartCity2(string rs)
        {
            int i = HasSecond(rs);
            if (i < 0) return "";
            string[] items = rs.Split('\n');
            string temp = items[i].Substring(25, 3);
            return temp.Trim(); 
        }
        //5.1ȡ��ɵ�����
        static public string GetStartCityCn(string rs)
        {
            string code = GetStartCity(rs);
            if (code.Length != 3) return null;
            //FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\CityCode.mp3", FileMode.Open, FileAccess.Read);//mp3
            FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\citycode.txt", FileMode.Open, FileAccess.Read);//txt
            StreamReader reader = new StreamReader(fs, System.Text.Encoding.GetEncoding("gb2312"));
            string temp = reader.ReadLine();
            while (temp != null)
            {
                //if (temp.Substring(0, 3) == code)//mp3
                try
                {
                    if (temp.Split(',')[2] == code.ToUpper())//txt
                    {
                        reader.Close();
                        fs.Close();
                        //return temp.Substring(3);//mp3
                        return temp.Split(',')[1];//txt
                    }
                }
                catch
                {
                }
                temp = reader.ReadLine();
            }
            reader.Close();
            fs.Close();
            return "";
        }
        //6.ȡ�����
        static public string GetEndCity(string rs)
        {
            int i = HasFirst(rs);
            if (i < 0) return "";
            string[] items = rs.Split('\n');
            //string temp = items[i].Substring(28, 3);
            string temp = substring(items[i], 28, 3);
            return temp.Trim(); 
        }
        static public string GetEndCity2(string rs)
        {
            int i = HasSecond(rs);
            if (i < 0) return "";
            string[] items = rs.Split('\n');
            string temp = items[i].Substring(28, 3);
            return temp.Trim(); 
        }
        //6.1ȡ���������
        static public string GetEndCityCn(string rs)
        {
            string code = GetEndCity(rs);
            if (code.Length != 3) return null;
            //FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath+"\\CityCode.mp3", FileMode.Open, FileAccess.Read);//mp3
            FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\citycode.txt", FileMode.Open, FileAccess.Read);//txt
            StreamReader reader = new StreamReader(fs, System.Text.Encoding.GetEncoding("gb2312"));
            string temp = reader.ReadLine();
            while (temp != null)
            {
                //if (temp.Substring(0, 3) == code)//mp3
                try
                {
                    if (temp.Split(',')[2] == code.ToUpper())//txt
                    {
                        reader.Close();
                        fs.Close();
                        //return temp.Substring(3);//mp3
                        return temp.Split(',')[1];//txt
                    }
                }
                catch
                { }
                temp = reader.ReadLine();
            }
            reader.Close();
            fs.Close();
            return "";
 
        }
        //6.9��citycodeȡ���ĳ�����
        static public string GetCityCn(string rs,string citycode)
        {
            string code = citycode;
            if (code.Length != 3) return null;
            //FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\CityCode.mp3", FileMode.Open, FileAccess.Read);//mp3
            FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\citycode.txt", FileMode.Open, FileAccess.Read);//txt
            StreamReader reader = new StreamReader(fs, System.Text.Encoding.GetEncoding("gb2312"));
            string temp = reader.ReadLine();
            while (temp != null)
            {
                //if (temp.Substring(0, 3) == code)
                try
                {
                    if (temp.Split(',')[2] == code.ToUpper())//txt
                    {
                        reader.Close();
                        fs.Close();
                        //return temp.Substring(3);
                        return temp.Split(',')[1];//txt

                    }
                    
                }
                catch
                {
                }
                temp = reader.ReadLine();
            }
            reader.Close();
            fs.Close();
            return "";

        }
        //7.ȡ�����(���չ�˾����)
        static public string GetCodeBig(string rs)
        {
            string[] items = rs.Split('\n');
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Length < 6) continue;
                if (items[i].Trim().Length>=9 && items[i].Substring(3, 3) == "RMK" && items[i].Substring(7, 3)=="CA/" )
                {
                    if (items[i].Length < 15) return "";
                    return items[i].Substring(10, 5);
                }
            }
            return "";
        }
        //8.ȡƱ��������
        static public string GetFN(string rs)
        {
            string[] items = rs.Split('\n');
            string str = "";
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Length < 5) continue;
                if (items[i].Substring(3, 2) == "FN")
                {
                    str = items[i].Substring(0,items[i].Length-2).Substring(6).Trim();
                    break;
                }
            }
            return str;
        }


        //FN/FCNY360.00/SCNY360.00/C3.00/XCNY80.00/TCNY50.00CN/TCNY30.00YQ/ACNY440.00
        //8.1ȡƱ��FareCNY360.00
        static public string GetFare(string rs)
        {
            string temp = GetFN(rs);
            if (temp == "") return "";
            string[] items = temp.Split('/');
            for (int i = 0; i < items.Length; i++)
            {
                try
                {
                    if (items[i][0] == 'F')
                        return items[i].Substring(1);
                }
                catch { }
            }
            return "";
        }
        //8.2ȡ˰��(���������)CNY50.00
        static public string GetTaxBuild(string rs)
        {
            string temp = GetFN(rs);
            if (temp == "") return "";
            string[] items = temp.Split('/');
            for (int i = 0; i < items.Length; i++)
            {
                try
                {
                    if (items[i][0] == 'T' && items[i].Substring(items[i].Length - 2) == "CN")
                        return items[i].Substring(1, items[i].Length - 3);
                }
                catch
                {
                    continue;
                }
            }
            return "";
        }
        //8.3ȡ˰��(ȼ��˰)CNY30.00
        //����CNY30.00;
        static public string GetTaxFuel(string rs)
        {
            string temp = GetFN(rs);
            if (temp == "") return "";
            string[] items = temp.Split('/');
            for (int i = 0; i < items.Length; i++)
            {
                try
                {
                    if (items[i][0] == 'T' && items[i].Substring(items[i].Length - 2) == "YQ")
                        return items[i].Substring(1, items[i].Length - 3);
                }
                catch
                {
                }
            }
            return "";
        }
        //8.4ȡʵ����ֵ����CNY360.00
        static public string GetEqualFare(string rs)
        {
            string temp = GetFN(rs);
            if (temp == "") return "";
            string[] items = temp.Split('/');
            try
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i][0] == 'S')
                        return items[i].Substring(1);
                }
            }
            catch { }
            return "";
        }
        //8.5ȡ����ACNY440.00
        static public string GetTatol(string rs)
        {
            try
            {
                //string temp = GetFN(rs);
                //if (temp == "") return "";
                //string[] items = temp.Split('/');
                //for (int i = 0; i < items.Length; i++)
                //{
                //    if (items[i].Trim()[0] == 'A')
                //        return items[i].Substring(1);
                //}
                int iACNY = rs.IndexOf("ACNY") + 1;
                int iend = rs.IndexOf(".", iACNY);
                if (iACNY < 1)
                {
                    //MessageBox.Show("���Ǳ�OFFICE��Ʊ�����ܻ�ȡ�۸�!","EAGLE",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return "CNY0";
                }
                return rs.Substring(iACNY, iend - iACNY + 3);
            }
            catch
            {
                MessageBox.Show("ȡ�ܼ�����!");
                return "";
            }

        }
        /*
         *�޸�����2006.7.2 
         */
        //9.ȡƱ�۵ȼ�FC/WUH MU SHA 360.00Y45 CNY360.00END
        static public string GetFC(string rs)
        {
            string[] items = rs.Split('\n');
            string str = "";
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Length < 5)  continue;
                //if (items[i].Substring(3, 2) == "FC")
                try
                {
                    if ((items[i].IndexOf('.') == 2 || items[i].IndexOf('.') == 3)

                        && items[i].Substring(items[i].Split('.')[0].Length + 1, 2) == "FC")
                    {
                        int i_point = items[i].IndexOf('.', 3);
                        int i_blanket = items[i].IndexOf(' ', i_point);
                        str = items[i].Substring(i_point, i_blanket - i_point).Substring(3);
                        //str = items[i].Substring(23,3);
                        break;
                    }
                }
                catch { }
            }
            return str;
        }
        
        //10.ȡƱ�ۼ���16JUN06WUH MU SHA 360.00CNY  360.00END
        static public string GetFareCal(string rs)
        {
            string[] items = rs.Split('\n');
            string str = "";
            
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Length < 5)continue;
                if (items[i].Substring(3, 2) == "FC")
                {
                    str = items[i].Substring(0, items[i].Length - 2).Substring(6).Trim();
                    break;
                }
            }
            string ret = "";
            //ret = "M " + GetDateStart(rs) + System.DateTime.Now.Year.ToString().Substring(2) + " " + str;
            ret = "M " + GetDateStart(rs)  + " " + str;
            return ret;

                
        }
        //11.ȡ���ʽFP/CASH,CNY
        static public string GetFP(string rs)
        {
            string[] items = rs.Split('\n');
            string str = "CASH,CNY";
            
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Length < 5) continue;
                if (items[i].Substring(3, 2) == "FP")
                {
                    str = items[i].Substring(0, items[i].Length - 2).Substring(6).Trim();
                    break;
                }
            }
            return str;
        }
        //12.ȡ��Ʊ���ڣ���ǰʱ�䣩
        static public string GetDatePrint()
        {
            string temp = System.DateTime.Now.Day.ToString();
            if (temp.Length == 1) temp = "0" + temp;
            string month = "";
            switch (System.DateTime.Now.Month)
            {
                case 1:
                    month = "JAN";
                    break;
                case 2:
                    month = "FEB";
                    break;
                case 3:
                    month = "MAR";
                    break;
                case 4:
                    month = "APR";
                    break;
                case 5:
                    month = "MAY";
                    break;
                case 6:
                    month = "JUN";
                    break;
                case 7:
                    month = "JUL";
                    break;
                case 8:
                    month = "AUG";
                    break;
                case 9:
                    month = "SEP";
                    break;
                case 10:
                    month = "OCT";
                    break;
                case 11:
                    month = "NOV";
                    break;
                case 12:
                    month = "DEC";
                    break;

            }
            string year = System.DateTime.Now.Year.ToString().Substring(2);
            return temp + month + year;

        }
        static public string GetMonthCode(int months)
        {
            string month = "";
            switch (months)
            {
                case 1:
                    month = "JAN";
                    break;
                case 2:
                    month = "FEB";
                    break;
                case 3:
                    month = "MAR";
                    break;
                case 4:
                    month = "APR";
                    break;
                case 5:
                    month = "MAY";
                    break;
                case 6:
                    month = "JUN";
                    break;
                case 7:
                    month = "JUL";
                    break;
                case 8:
                    month = "AUG";
                    break;
                case 9:
                    month = "SEP";
                    break;
                case 10:
                    month = "OCT";
                    break;
                case 11:
                    month = "NOV";
                    break;
                case 12:
                    month = "DEC";
                    break;
                default:
                    month = "";
                    break;

            }
            return month;
        }
        static public string GetMonthInt(string code)
        {
            if (code.Length != 3) return "";
            switch (code.ToUpper())
            {
                case "JAN":
                    return "01";
                case "FEB":

                    return "02";
                case "MAR":
                    return "03";
                case "APR":
                    return "04";
                case "MAY":
                    return "05";
                case "JUN":
                    return "06";
                case "JUL":
                    return "07";
                case "AUG":
                    return "08";
                case "SEP":
                    return "09";
                case "OCT":
                    return "10";
                case "NOV":
                    return "11";
                case "DEC":
                    return "12";
            }
            return null;

        }
        //��һ������,���򷵻�λ�ڵڼ��У�û�򷵻�-1
        static public int HasFirst(string rs)
        {
            string[] items = rs.Split('\n');
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Length < 5) continue;
                if (items[i].Substring(3, 2) == "  " || items[i].Substring(3, 2) == " *")
                {
                    return i;
                }
            }
            return -1;

        }
        //�ڶ������Σ����򷵻�λ�ڵڼ��У�û�򷵻�-1
        static public int HasSecond(string rs)
        {
            int ifirst = HasFirst(rs);
            if (ifirst < 0) return -1;
            string[] items = rs.Split('\n');
            string temp = items[ifirst +1];
            if (temp.Length >= 5)
                if (temp.Substring(3, 2) == "  " || temp.Substring(3, 2) == " *")
                    return ifirst + 1;
            return -1;
        }
        //
        static public int GetSegmentNumber(string rs)
        {
            try
            {
                int ret = 0;
                string[] items = rs.Split('\n');
                for (int i = 0; i < items.Length; i++)
                {
                    string temp = items[i];
                    if (temp.Trim().Length >= 5)
                        if (temp.Substring(3, 2) == "  " || temp.Substring(3, 2) == " *")
                            ret++;
                }
                return ret;
            }
            catch
            {
                
            }
            return 1;
        }
        //����յ�
        static public string GetFromTo(string rs)
        {
            string start = GetStartCity(rs);
            string end = GetEndCity(rs);
            if (HasSecond(rs) > 0)
            {
                end = GetEndCity2(rs);
            }
            return start+"/"+end;
        }
        //�������1
        static public string GetBook(string rs)
        {
            if (HasFirst(rs)>0) return "OK";
            return "";
        }
        static public string GetBook2(string rs)
        {
            if (HasSecond(rs)>0) return "OK";
            return "";
        }
        //�����
        static public string GetAllow(string rs)
        {
            if (HasFirst(rs) > 0) return "20K";
            return "";
        }
        static public string GetAllow2(string rs)
        {
            if (HasSecond(rs) > 0) return "20K";
            return "";
        }
        //���ܻ���
        static public string GetCash(string rs)
        {
            return GetTatol(rs);
        }
        //��������
        static public string GetCommRate(string rs)
        {
            string temp = GetFN(rs);
            if (temp == "") return "";
            string[] items = temp.Split('/');
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i][0] == 'C')
                    return items[i].Substring(1);
            }
            return "";
        }
        //˰��ϼ�
        static public string GetTaxTotal(string rs)
        {
            float t1 = 0F;
            float t2 = 0F;
            try
            {
                if (GetTaxBuild(rs).Length > 3) t1 = float.Parse(GetTaxBuild(rs).Substring(3));
            }
            catch
            {
                t1 = 0F;
            }
            try
            {
                if (GetTaxFuel(rs).Length > 3) t2 = float.Parse(GetTaxFuel(rs).Substring(3));
            }
            catch
            {
                t2 = 0F;
            }
            float t = t1+t2;
            return "CNY" + t.ToString("n");
        }
        /*0
         * 7.SSR FOID MF HK1 NI420102197607051428/P1
         */
        //���֤��
        static public string[] GetIDCardNo(string rs)
        {
            if (GlobalVar.b_rtByEticket)
            {
                string[] ret1 = new string[1];
                ret1[0] =  etGetIDNumber(rs);
                return ret1;
            }
            List<string> names = new List<string>();
            names = GetNames(rs);
            int peoples = names.Count;
            peoples = GetPeopleNumber(rs) > peoples ? GetPeopleNumber(rs) : peoples;

            string []ret = new string [peoples];
            for (int i = 0; i < names.Count; i++) ret[i] = "";
            string[] items = rs.Split('\n');
            string str = "";
            try
            {
                for (int ii = 0; ii < items.Length; ii++)
                {
                    if (items[ii].Length < 6) continue;
                    if (items[ii].Substring(3, 3) == "SSR")
                    {
                        int start = items[ii].LastIndexOf("NI") + 2;
                        int end = items[ii].LastIndexOf("/P");
                        if (start < 2 || end < 0) continue;
                        int which;
                        which = int.Parse(items[ii].Substring(end + 2).Trim());
                        ret[which - 1] = items[ii].Substring(start, end - start);
                    }
                }
            }
            catch { }
            return ret;
        }
        //���չ�˾����
        static public string GetAirlineCode(string rs)
        {
            string[] items = rs.Split('\n');
            string str = "";

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Length < 6) continue;
                if (items[i].Substring(3, 2) == "T/" || items[i].Substring(3, 3) == "TN/")
                {
                    str = (items[i].Split('/'))[1];
                    if (str.Length >= 3)
                        return str.Substring(0, 3);
                }

            }
            return str;
        }
        //ȡ���ӿ�Ʊ��
        static public string[] GetETNumber(string rs)
        {

                List<string> names = new List<string>();
                names = GetNames(rs);
                int peoples = names.Count;
                string[] ret = new string[peoples];
                for (int i = 0; i < names.Count; i++) ret[i] = "";
                string[] items = rs.Split('\n');
            try
            {
                for (int i = 0; i < items.Length; i++)
                {
                    try
                    {
                        if (items[i].Length < 6) continue;
                        if (items[i].Substring(3, 2) == "T/" || items[i].Substring(3, 3) == "TN/" || items[i].Substring(4, 3) == "TN/")
                        {
                            int start = items[i].IndexOf("/") + 1;
                            int end = items[i].LastIndexOf("/P");
                            if (start < 1 || end < 0) continue;
                            string str = items[i].Substring(end + 2).Trim();
                            if (str.Length > 2) str = str.Substring(0, 2);
                            str = str.Trim();
                            int which = int.Parse(str);
                            str = items[i].Substring(start, end - start);
                            if (str.Contains("-"))
                            {
                                str = str.Substring(0, 3) + str.Substring(4);
                            }
                            ret[which - 1] = str;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                return ret;
            }
            catch
            {
                //MessageBox.Show("ȡ���ӿ�Ʊ���������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��","�׸�Ƽ�",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return ret;
            }

        }
        static public string GetOfficeNumberByETicket(string rs)
        {
            try
            {
                string[] lines = mystring.trim(rs).Split('\n');
                for (int i = lines.Length - 1; i >= 0; i--)
                {
                    lines[i] = mystring.trim(lines[i]);
                    if (lines[i].Length > 6) return mystring.right(lines[i], 6);
                }
            }
            catch { return ""; }
            return "";
        }
        //ȡ��Ʊ�ɹ���� 7.SSR TKNE MU HK1 WUHPEK 2453 B24OCT 7815963667911/1/P1
        static public bool IsBookETOK(string rs)
        {
            List<string> names = new List<string>();
            names = GetNames(rs);
            int peoples = names.Count;
            string[] items = rs.Split('\n');
            try
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].IndexOf("SSR TKNE") > -1)
                    {
                        string tempst = EagleAPI.substring(items[i], 15, 2).ToUpper();
                        string tempst1 = EagleAPI.substring(items[i], 16, 2).ToUpper();//����Ŵ���100ʱ
                        if (tempst != "HK" && tempst1 !="HK")
                        {
                            return false;
                        }
                    }
                }

            }
            catch
            {
                //MessageBox.Show("ȡ���ӿ�Ʊ���������¼�˶����ŵ���ϸ��Ϣ���������Ӧ����ϵ��","�׸�Ƽ�",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return true;
            }
            return true;
        }
        #endregion

        //NO PNR OR *THIS PNR WAS ENTIRELY CANCELLED*
        //PNR�Ƿ���Ч �޸�����2006.7.2
        static public bool GetNoPnr(string rs)
        {
            if (rs.IndexOf("NO PNR") < 0 && rs.IndexOf("*THIS PNR WAS ENTIRELY CANCELLED*") < 0 && rs.IndexOf("PLEASE SIGN IN FIRST") < 0)
                return true;//PNR��Ч
            MessageBox.Show(rs, "EAGLE���ն�Ʊϵͳ��������");
            return false;
        }
        static public bool GetNoPnr(string rs,bool showMessage)
        {
            if (rs.IndexOf("NO PNR") < 0 && rs.IndexOf("*THIS PNR WAS ENTIRELY CANCELLED*") < 0)
                return true;//PNR��Ч
            if(showMessage)MessageBox.Show(rs, "EAGLE���ն�Ʊϵͳ��������");
            return false;
        }

        //ֻ�е��ӿ�Ʊ�г̵�������һ���õ��ĺ���#############################################################################################
        //1.����Command.AV_string�жϴ�������ƾ֤�Ƿ�ɹ������򷵻�true
        static public bool IsCreateReceiptSuccess()//Modify By Eric
        {
            string errorbegin = "<ErrorReason>";
            string errorend = "/ErrorReason>";
            string response = "</Response";
            string response2 = "onse>";
            if (connect_4_Command.AV_String.IndexOf(response) < 0 && connect_4_Command.AV_String.IndexOf(response2) < 0)

            {
                GlobalVar.intPrintErrorType = 1;
                return false;//���Ƿ�����Ӧ������false
            }
            if (connect_4_Command.AV_String.Contains("[Itinerary_INV: Not Support]"))//2009-4-20�·��ִ�����Ϣ clawc
            {
                GlobalVar.intPrintErrorType = 3;
                return false;
            }
            int ie_begin = connect_4_Command.AV_String.IndexOf(errorbegin);// +errorbegin.Length;
            int ie_end = connect_4_Command.AV_String.IndexOf(errorend);
            if (ie_begin < 0)
            {
                GlobalVar.intPrintErrorType = 0;
                return true;//û�д�����Ϣ����true
            }
            string errorstring = connect_4_Command.AV_String.Substring(ie_begin + errorbegin.Length, ie_end - (ie_begin + errorbegin.Length)-1);
            GlobalVar.intPrintErrorType = 2;
            MessageBox.Show(errorstring, "����ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        //2.����Command.AV_string�ж����ϱ���ƾ֤�Ƿ�ɹ������򷵻�true
        static public bool IsRemoveReceiptSuccess()
        {
            bool ret = IsCreateReceiptSuccess();
            if (ret)
            {
                MessageBox.Show("�ɹ����ϵ��ӿ�Ʊ����ƾ֤��", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return ret;
        }
        #region ���������API
        static WindowInfo wndInfo;
        static bool SETWNDINFO()
        {

            if (frmMain.st_tabControl == null)
            {
                if(!GlobalVar.printProgram)MessageBox.Show("���½�ȣ�");
                return false;
            }
            if (frmMain.st_tabControl.InvokeRequired)
            {
                EventHandler eh = new EventHandler(setwndinfo);
                TabControl tb = frmMain.st_tabControl;
                frmMain.st_tabControl.Invoke(eh, new object[] { tb, EventArgs.Empty });
            }
            else
            {
                //wndInfo = frmMain.windowSwitch[frmMain.st_tabControl.SelectedIndex];
                wndInfo = frmMain.windowSwitch[0];
            }
            if (wndInfo == null)
            {
                MessageBox.Show("���ڳ�ʼ��ʧ�ܣ�����������Ӧ�ó���");
                return false;
            }
            return true;
        }
        static void setwndinfo(object sender, EventArgs e)
        {
            //wndInfo = frmMain.windowSwitch[frmMain.st_tabControl.SelectedIndex];
            wndInfo = frmMain.windowSwitch[0];
        }
        /// <summary>
        /// ֱ�ӷ��Ͱ�packet��API�����������鷢�ͣ�string������ʱ��ʹ��˫����
        /// </summary>
        /// <param name="packet"></param>
        static public void EagleSend(string packet)
        {
            if (SETWNDINFO()) wndInfo.Send3IN1(packet);
        }
        static public void EagleSend(char[] packet,int length)
        {
            if (SETWNDINFO()) wndInfo.Send3IN1(packet, length);
        }
        /// <summary>
        /// ����ָ���API����������֮ǰ��������鷢��
        /// </summary>
        /// <param name="cmd"></param>

        static public void EagleSendCmd(string cmd)
        {
            if (SETWNDINFO()) wndInfo.SendData(cmd);
        }
        static public void EagleSendCmd(string cmd,int msgtype)
        {
            if (SETWNDINFO()) wndInfo.SendData(cmd,msgtype);
        }
        static public void EagleSendBytes(byte []b)
        {
            if (SETWNDINFO()) wndInfo.sendbyte(b);
        }
        public static bool b_onecmd = false;
        /// <summary>
        /// ���͵���ָ���API��������֮ǰ����ָ���޹أ���֮��ָ��Ҳ�޹�
        /// </summary>
        /// <param name="cmd"></param>
        static public void EagleSendOneCmd(string cmd)
        {
            if (SETWNDINFO())
            {
                b_onecmd = true;
                wndInfo.SendData(cmd);
                b_onecmd = false;
            }
        }
        static public void EagleSendOneCmd(string cmd,int msgtype)
        {
            if (SETWNDINFO())
            {
                b_onecmd = true;
                wndInfo.SendData(cmd, msgtype);
                b_onecmd = false;
            }
        }
        static public void EagleSend_useconfiglonely_Static(int ipid)
        {

            int it = 0;
            EagleProtocol ep = new EagleProtocol();
            ep.MsgBody = ipid.ToString();
            ep.MsgType = 8;
            ep.SetMsgLength();
            char[] temp = ep.ConvertToString(out it);

            EagleAPI.EagleSend(temp, it);
            //byte[] bTemp = new byte[it];
            //for (int i = 0; i < it; i++)
            //{
            //    bTemp[i] = (byte)temp[i];
            //}
            //m_Socket.Send(bTemp);
        }
        /// <summary>
        /// �����г̵�����ָ��
        /// </summary>
        /// <param name="str">����ReceiptRemove�õ��Ĵ�</param>
        static public void EagleSendCancelReceipt(string str)
        {
            GlobalVar.CurrentSendCommands = "";//������һָ�����ǰ����ָ���б���գ���������һû�յ������ط�ָ���б������
            int outlength = 0;
            EagleProtocol ep = new EagleProtocol();
            ep.MsgType = 3;
            ep.cmdType = 2;
            ep.MsgBody = str;
            ep.SetMsgLength();
            char[] sendstring = ep.ConvertToString(out outlength);

            EagleAPI.EagleSend(sendstring, outlength);

        }
        static public void EagleSendPrintReceipt(string str)
        {

            GlobalVar.CurrentSendCommands = "";//������һָ�����ǰ����ָ���б���գ���������һû�յ������ط�ָ���б������
            int outlength = 0;
            EagleProtocol ep = new EagleProtocol();
            ep.MsgType = 3;
            ep.cmdType = 2;
            ep.MsgBody = str;
            ep.SetMsgLength();
            char[] sendstring = ep.ConvertToString(out outlength);

            EagleAPI.EagleSend(sendstring, outlength);
        }
        #endregion

        #region//������־#############################################################################################################################
        //1.д���ļ����ļ���Ϊ��������
        static public void LogWrite(string text)
        {
            EagleString.EagleFileIO.LogWrite(text);
        }
        //2.���ļ�����ȡ��־�ļ�
        static public void LogRead(string filename)
        {
            PrintTicket.RunProgram(Environment.SystemDirectory + "\\notepad.exe", filename);
        }
        //3.д���������־
        public string Content = "";
        public string S_R = "";
        public void LogWriteServer()
        {

        }
        #endregion

        #region//�����ļ���д(����)
        [DllImport("kernel32")]//����0��ʾʧ�ܣ���0Ϊ�ɹ�
        private static extern long WritePrivateProfileString(string section, string key,string val, string filePath);

        [DllImport("kernel32")]//����ȡ���ַ����������ĳ���
        private static extern long GetPrivateProfileString(string section, string key,string def, StringBuilder retVal, int size, string filePath);
        //#region ��Ini�ļ�
        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, NoText, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else
            {
                return String.Empty;
            }
        }


        //#region дIni�ļ�
        public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
                if (OpStation == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
#endregion



#region//�����Ƿ��Ѿ����Զ���������(����)
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName,string lpWindowName);
        static public bool SearchUpdateForm()
        {

            if (FindWindow(null, "������������").Equals(IntPtr.Zero))
                return false;
            return true;
        }
        #endregion

        //���ַ������غ���
        static public string substring(string oldstring, int start, int length)
        {
            if (length < 0) return "";
            if (start < 0  || start > oldstring.Length) return "";
            if (start + length > oldstring.Length) return oldstring.Substring(start);
            return oldstring.Substring(start, length);
        }
        
        /// <summary>
        /// �ж�ָ����룬�����ļ��в�����ָ���򷵻�null
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>

        static public string GetCmdName(string cmd)
        {
            List<string> ls = new List<string> ();
            FileStream fs = new FileStream(Application.StartupPath + "\\CmdList.mp3", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                string temp = sr.ReadLine();
                if (temp != null) ls.Add(temp.Trim());
            }
            ls.Sort();
            sr.Close();
            fs.Close();

            for (int i = ls.Count - 1; i >= 0; i--)
            {
                if (substring(cmd, 0, ls[i].Length).ToLower() == ls[i].ToLower())
                    return ls[i];
            }
            return null;


        }
        /// <summary>
        /// ָ��cmd�Ƿ����û�����ָ���б���ޣ��򷵻�""
        /// </summary>
        /// <param name="cmd">һ��ָ��</param>
        /// <param name="visuableCommands">��~���ָ�</param>
        /// <returns></returns>
        static public string GetCmdName(string cmd,string visuableCommands)
        {
            visuableCommands += "~rt~av";
            string [] sArray= visuableCommands.Split('~');
            List<string> ls = new List<string>();
            for (int i = 0; i < sArray.Length; i++) ls.Add(sArray[i]);
            ls.Sort();
            for (int i = ls.Count - 1; i >= 0; i--)
            {
                if (substring(cmd, 0, ls[i].Length).ToLower() == ls[i].ToLower())
                    return ls[i];
            }
            return "";
        }
        static public string GetCmdNameEntire(string cmd, string visuableCommands)
        {
            string[] sArray = visuableCommands.Split('~');
            List<string> ls = new List<string>();
            for (int i = 0; i < sArray.Length; i++) ls.Add(sArray[i]);
            ls.Sort();
            for (int i = ls.Count - 1; i >= 0; i--)
            {
                if (cmd.ToLower() == ls[i].ToLower())
                    return ls[i];
            }
            return "";
        }

        /// <summary>
        /// ��ӡ������ΪA3
        /// </summary>
        /// <param name="ptDoc"></param>
        /// <returns></returns>

        static public bool PrinterSetup(System.Drawing.Printing.PrintDocument ptDoc)
        {
            List<string> ls = new List<string>();
            bool b_A3 = false;

            for (int i = 0; i < ptDoc.PrinterSettings.PaperSizes.Count; i++)
            {
                if (ptDoc.PrinterSettings.PaperSizes.Count <= 0)
                {


                    MessageBox.Show("��ӡ��û�а�װ��û��������");
                    return false;
                }
                else
                {
                    ls.Add(ptDoc.PrinterSettings.PaperSizes[i].PaperName);
                    if (ls[ls.Count - 1] == "A3")
                    {
                        b_A3 = true;
                        break;
                    }
                    //this.comboBox1.Items.Add(this.printDocument1.PrinterSettings.PaperSizes[i].PaperName);
                }
            }
            int iPage = ls.Count - 1;

            if (iPage >= 0 && b_A3)
            {
                ptDoc.DefaultPageSettings.PaperSize = ptDoc.PrinterSettings.PaperSizes[iPage];
            }
            else
            {
                //if (DialogResult.No == MessageBox.Show("��ӡ�����ô����Ƿ�ʹ��Ĭ�����ô�ӡ��", "����", MessageBoxButtons.YesNo,MessageBoxIcon.Warning)) return false;                
            }
            return true;
        }

        static public bool PrinterSetupCostom(System.Drawing.Printing.PrintDocument ptDoc, int w, int h)
        {
            try
            {
                System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
                ps.PaperSize = new System.Drawing.Printing.PaperSize("Custom", w, h);
                ps.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                ptDoc.DefaultPageSettings = ps;
            }
            catch
            { return false; }
            return true;
        }


        //��½�ʺ�����
        //static public string loginName = "";
        //static public string loginPassword = "";
        //static public bool loginSuccess = false;
        //static public Login_Classes loginLC = new Login_Classes();
        

        #region//ͨ����Ʊ����ȡ�û���Ϣ(����,ID,ǩת,�г�(from,carrier class date time fare_basis etc.),FARE,AIRPORT TAX,FUEL SURCHARGE,OTHER TAXES TOTAL
        //E-TN,CK,COONJUNCTION TKT,INSURANCE
        /*
         * >DETR:TN/999-5904253662 
            ISSUED BY:                           ORG/DST: WUH/BJS                 BSP-D 
            E/R: ����ǩת
            TOUR CODE: 
            PASSENGER: ��˷ 
            EXCH:                               CONJ TKT:
            O FM:1WUH CA    1366  K 28JUN 1815 OK K                        20K VOID 
                      RL:
              TO: PEK
            FC:
            FARE:                      |FOP:
            TAX:                       |OI: 
            TOTAL:                     |TKTN: 999-5904253662
         */
        //tn1.ȡ����
        static public string etGetName(string rs)
        {
            string label = "PASSENGER:";
            int i_name = rs.IndexOf(label);
            if (i_name < 0) return "";
            i_name += label.Length;
            int i_end = rs.IndexOf("\n", i_name);
            return substring(rs, i_name, i_end - i_name).Trim();
        }
        //tn2.ȡ���֤��ID
        static public string etGetIDNumber(string rs)
        {
            return null;

        }
        //tn3ǩת
        static public string etGetRestrictions(string rs)
        {
            string label = "E/R:";
            int i_restrictions = rs.IndexOf(label);
            if (i_restrictions < 0) return "";
            i_restrictions += label.Length;
            int i_end = rs.IndexOf("\n", i_restrictions);
            return substring(rs, i_restrictions, i_end - i_restrictions).Trim();
        }
        //tn4Ʊ��FARE
        static public string etGetFare(string rs)
        {
            string label = "FARE:";
            int i_name = rs.IndexOf(label);
            if (i_name < 0) return "";
            i_name += label.Length;
            int i_end = rs.IndexOf("|", i_name);
            return substring(rs, i_name, i_end - i_name).Trim();
        }
        //tn5���������AIRPORT TAX
        //
        #endregion

        //����Ƕ�������
        //public static string Notice = "EG��ʾ��";
        //public static string WaitString = "���Եȡ���";
        //public static string EagleWebString = "�׸���";
        //
        //public static bool etProcessing = false;
        //public static string WebUrl = "http://yinge.eg66.com/EagleWeb/login.aspx";
        //public static string WebServer = "http://yinge.eg66.com/WS/egws.asmx";

        //��ͨ
        //public static string weburl_WT = "http://wangtong.eg66.com/EagleWeb/login.aspx";
        //public static string webserver_WT = "http://wangtong.eg66.com/WS/egws.asmx";

        //����
        //public static string weburl_DX = "http://yinge.eg66.com/EagleWeb/login.aspx";
        //public static string webserver_DX = "http://yinge.eg66.com/WS/egws.asmx";
        //public static string LocalCityCode = "WUH";
        //������־��ָ���
        //public static string LogCmdString = "rT~ss~sd~sp~nm~etdz~dz~@~\\~";

        //���������л�
        //public static bool b_cfg_First = true;
        //public static string str_cfg_name = "";
        //���ڿۿ��ȫ�ֱ���
        
        //public static bool b_etdz = false;
        //public static bool b_etdz_A = false;
        //public static bool b_enoughMoney = false;
        //public static bool b_endbook = false;//���
        //public static string f_CurMoney = "0.00";
        //public static float f_fc = 0F;//Ҫ�ۿ�Ľ��������Ʊ��
        //������Ʊָ��
        //public static bool b_cmd_trfd_AM = false;
        //public static bool b_cmd_trfd_M = false;
        //public static bool b_cmd_trfd_L = false;
        public static string FullSpace(string input, int totallen ,string fillchar)
        {
            if (input.Trim().Length > totallen) return "";
            string ret = input.Trim();
            while (ret.Length < totallen)
            {
                ret += fillchar;
            }
            return ret;
        }
        //���ڵ��ӿ�Ʊͳ�ƣ��ύ��Ʊ��Ϣ
        public static eTicket.etStatic etstatic = new ePlus.eTicket.etStatic();

        //�������Ʊ���
        //public static bool b_pat = false;//�ж��Ƿ�ո�ʹ����pat:ָ��
        //public static string s_configfile = Application.StartupPath + "\\config.xml";//�����ļ�����·��
        //public static string s_ConnectCFG = Application.StartupPath + "\\ConnectCFG.xml";//�����ļ�2

        //10����ת����62�����ַ���
        public static string ConvertTo62(long dec)
        {
            string ret = "";
            string basestring = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (dec < 0) return "-";
            long tt = dec;
            while (tt >= 62)
            {
                long mod = tt % 62;//ȡ��
                tt = tt / 62;//ȡ��
                ret = basestring[(int)mod] + ret;
            }
            ret = basestring[(int)tt] + ret;
            return ret;
            
        }
        /// <summary>
        /// ȡ7λʱ�䴮
        /// </summary>
        /// <returns></returns>
        public static string GetRandom62()
        {
            return GetRandom62(100L);
        }
        public static string GetRandom62(string notRandom)
        {
            if (notRandom == "") return GetRandom62();
            else return notRandom;
        }
        public static string GetRandom62(long multicks)
        {
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            DateTime dd = new DateTime(4, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);

            //ʱ��̶�:1���� = 10000 �̶�
            long ii = dd.Ticks / multicks;
            string ss = EagleAPI.ConvertTo62(ii);
            return ss;
        }
        public static string GetRandom01(int second)
        {
            string ret = "";
            string basestring = "12345678abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int s = second % 60;
            return basestring[s].ToString();
            
        }
        //public static bool b_SubmitETicket = false;
        //public static bool b_SubmitETicketStart = false;
        //public static bool b_SubmitETicketEnd = false;

        //public static bool b_nextPage = false;
        //public static bool b_pnCommand = false;
        //public static bool b_rtCommand = false;
        //public static bool b_querryCommand = false;
        //public static bool b_otherCommand = false;
        /// <summary>
        /// �õ��ӿ�Ʊͳ�Ʊ����������ض�ʱ���ϴ����ӿ�Ʊ��Ϣ
        /// </summary>
        public void SetetStatic()
        {
            if (connect_4_Command.ReceiveString.IndexOf("+") > connect_4_Command.ReceiveString.IndexOf("-") && connect_4_Command.ReceiveString.IndexOf("+") > -1)
            {
                //b_nextPage = true;
            }
            else
            {
                //b_nextPage = false;
            }
            string temp;
            try
            {
                temp = connect_4_Command.Qall[connect_4_Command.Qall.Count - 1];
            }
            catch
            {
                temp = "";
            }
            if (temp.Length >= 7)
            {
                //if (Command.SendString.Substring(0, 2).ToUpper() == "rT")
                if (substring(temp, 0, 2).ToUpper() == "RT" || (GlobalVar.b_rtCommand && GlobalVar.b_pnCommand && !GlobalVar.b_querryCommand && !GlobalVar.b_otherCommand))
                {
                    try
                    {
                        etstatic.Bunk1 = GetClass(connect_4_Command.ReceiveString);
                        etstatic.Bunk2 = GetClass2(connect_4_Command.ReceiveString);
                        etstatic.CityPair1 = GetStartCity(connect_4_Command.ReceiveString) + GetEndCity(connect_4_Command.ReceiveString);
                        etstatic.CityPair2 = GetStartCity2(connect_4_Command.ReceiveString) + GetEndCity2(connect_4_Command.ReceiveString);
                        etstatic.Date1 = GetDateStart(connect_4_Command.ReceiveString);
                        etstatic.Date2 = GetDateStart2(connect_4_Command.ReceiveString);
                        string[] etn = GetETNumber(connect_4_Command.ReceiveString);
                        for (int i = 0; i < etn.Length; i++)
                        {
                            etstatic.etNumber = ";" + etn[i];
                        }
                        if (etn.Length > 0)
                        {
                            etstatic.etNumber = etstatic.etNumber.Substring(1);
                        }
                        etstatic.FlightNumber1 = GetCarrier(connect_4_Command.ReceiveString) + GetFlight(connect_4_Command.ReceiveString);
                        etstatic.FlightNumber2 = GetCarrier2(connect_4_Command.ReceiveString) + GetFlight2(connect_4_Command.ReceiveString);
                        etstatic.GetPnrString(temp);
                        etstatic.State = "0";
                        etstatic.TotalFC = GetTatol(connect_4_Command.ReceiveString).Substring(3);//��CNY
                        GlobalVar.f_fc = float.Parse(etstatic.TotalFC);
                        etstatic.UserID = GlobalVar.loginName;
                    }
                    catch
                    {
                    }

                }
            }
#region//��������ϴ����ӿ�Ʊ��Ϣ����
            //if (EagleAPI.b_SubmitETicket && b_endbook)
            //{
            //    //��send rt+pnr
            //    if (frmMain.st_tabControl.InvokeRequired)
            //    {
            //        EventHandler eh = new EventHandler(send1);
            //        TabControl tb = frmMain.st_tabControl;
            //        frmMain.st_tabControl.Invoke(eh, new object[] { tb, EventArgs.Empty });
            //        //ListView lv = context.lv_��ѯ���;
            //        //context.lv_��ѯ���.Invoke(eh, new object[] { lv, EventArgs.Empty });
            //    }
            //    else
            //    {
            //        EagleAPI.EagleSendCmd("rT" + etstatic.Pnr);
            //        b_SubmitETicketStart = true;
            //        b_SubmitETicket = false;
            //    }

            //}
            ////��ʼ�ϴ�
            //else if (EagleAPI.b_SubmitETicketStart && EagleAPI.b_nextPage)
            //{

            //    //����PN
            //    if (frmMain.st_tabControl.InvokeRequired)
            //    {
            //        EventHandler eh = new EventHandler(send2);
            //        TabControl tb = frmMain.st_tabControl;
            //        frmMain.st_tabControl.Invoke(eh, new object[] { tb, EventArgs.Empty });
            //    }
            //    else
            //    {
            //        EagleAPI.EagleSendCmd("pn");
            //    }

            //}
            //else
            //{
            //    if (EagleAPI.b_SubmitETicketStart)
            //    {
            //        if (etstatic.SubmitInfo())//�ϴ��ɹ�
            //        {
            //            b_SubmitETicketEnd = true;//������ʾ����
                       
            //        }
            //        b_SubmitETicketStart = false;
            //    }
            //}
#endregion
        }
        void send1(object sender, EventArgs e)
        {
            EagleAPI.EagleSendCmd("rT" + etstatic.Pnr);
            //b_SubmitETicketStart = true;
            //b_SubmitETicket = false;
        }
        void send2(object sender, EventArgs e)
        {
            EagleAPI.EagleSendCmd("pn");
        }
        public static string GetAirLineName(string AirLineCode)
        {
            if (AirLineCode.Length != 2)
            {
                return "";
            }
            string temp = AirLineCode.ToUpper();
            switch (temp)
            {
                case "CA":
                    return "�й����ʺ��չ�˾";
                case "MU":
                    return "�й��������չ�˾";
                case "CZ":
                    return "�й��Ϸ����չ�˾";
                case "SZ":
                    return "�й����Ϻ��չ�˾";
                case "WH":
                    return "�й��������չ�˾";
                case "CJ":
                    return "�й��������չ�˾";
                case "SH":
                    return "�Ϻ����չ�˾";
                case "3Q":
                    return "���Ϻ��չ�˾";
                case "F6":
                    return "�й����չɷ����޹�˾";
                case "GP":
                    return "�й�ͨ�ú��չ�˾";
                case "Z2":
                    return "��ԭ���չ�˾";
                case "MF":
                    return "�й����ź��չ�˾";
                case "3U":
                    return "�Ĵ����չ�˾";
                case "XO":
                    return "�½����չ�˾";
                case "H4":
                    return "����ʡ���չ�˾";
                case "X2":
                    return "�й��»����չ�˾";
                case "ZH":
                    return "���ں��չ�˾";
                case "ZJ":
                    return "�㽭���չ�˾";
                case "WU":
                    return "�人���չ�˾";
                case "GH":
                    return "����ʡ���չ�˾";
                case "GW":
                    return "���Ǻ��չ�˾";
                case "FJ":
                    return "�������չ�˾";
                case "SC":
                    return "ɽ�����չ�˾";
                case "3W":
                    return "�Ͼ����չ�˾";
                case "2Z":
                    return "�������չ�˾";
                case "FM":
                    return "�й��Ϻ����չ�˾";
                case "HU":
                    return "���Ϻ��չ�˾";
                case "8C":
                    return "���Ǻ��չ�˾";
                case "EU":
                    return "ӥ�����չ�˾";
                case "KA":
                    return "�������չ�˾";
                case "HO":
                    return "���麽�չ�˾";
                case "8L":
                    return "���麽�չ�˾";
                case "KN":
                    return "�������չ�˾";
                case "G5":
                    return "���ĺ��չ�˾";
                case "GS":
                    return "���ĺ��չ�˾";
                case "BK":
                    return "�¿����չ�˾";
                case "CN":
                    return "�й��»����ع����޹�˾";
                default:
                    return "";
            }
        }
        public static bool isNormalBunk(string al,string bunk)
        {
            bool bret = false;
            FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\bunks.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                string linestring = sr.ReadLine();
                if (linestring.Substring(0, 2).ToUpper() == al.ToUpper())
                {
                    if (linestring.Substring(2).ToUpper().IndexOf(bunk) >= 0)
                    {
                        bret = true;
                        break;
                    }
                }
            }
            sr.Close();
            fs.Close();
            return bret;
        }
        public static int IndexOfBunk(string al, string bunk)
        {
            int iret = 100;
            FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\bunks.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                string linestring = sr.ReadLine();
                if (linestring.Substring(0, 2).ToUpper() == al.ToUpper())
                {
                    if (linestring.Substring(3).ToUpper().IndexOf(bunk)>=0)
                    {
                        iret = linestring.Substring(3).ToUpper().IndexOf(bunk);
                        break;
                    }
                }
            }
            sr.Close();
            fs.Close();
            return iret;
        }
        public static string GetAirLineBunkSort(string al,DateTime dt)
        {
            string tt = "";
            FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\bunks.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            try
            {
                while (!sr.EndOfStream)
                {
                    string linestring = sr.ReadLine();
                    if (linestring.Substring(0, 2).ToUpper() == al.ToUpper())
                    {
                        tt = linestring.Split(',')[1].Replace('-', ' ');
                        break;
                    }
                }
            }
            catch
            {
                tt = "";
            }
            if (tt != "")
            {
                switch (al)
                {
                    //case "HO":
                    //    if (dt < DateTime.Parse("2007-5-28"))
                    //        tt = "FCY HKLMTEVUQGBR ";
                    //    else
                    //        tt = " CY B LMTEH QVXWR";
                    //    break;

                    default:
                        //    tt = "FCY ";
                        break;
                }
            }
            else
            {
                tt = "FCY ";
            }
            return tt;
        }
        public static string GetAirLineBunkFareClass(string al, string bunk,DateTime dt)
        {
            string tt = GetAirLineBunkSort(al,dt);
            int offset = tt.IndexOf(bunk);
            switch (offset)
            {
                case -1:
                    return bunk;
                case 0:
                case 1:
                case 2:
                    return tt[offset].ToString();
                default:
                    int temp = 95 - (offset - 3) * 5;
                    return "Y" + temp.ToString();

            }

        }
        public static string GetAirLineEnglishName(string al)
        {
            string ret = "";
            switch (al)
            {
                case "3U":
                    ret = " CHINA SICHUAN AIRLINE";
                    break;
                case "BK":
                    ret = " OKAY AIRWAYS COMPANY LIMITEDE";
                    break;
                case "CA":
                    ret = " AIR CHINA";
                    break;
                case "CZ":
                    ret = " CHINA SOUTHERN AIRLINES";
                    break;
                case "EU":
                    ret = " UNITED EAGLE AIRLINES";
                    break;
                case "FM":
                    ret = " SHANGHAI AIRLINE";
                    break;
                case "HU":
                    ret = " HAINAN AIRLINES";
                    break;
                case "MF":
                    ret = " XIAMEN AIRLINES";
                    break;
                case "MU":
                    ret = " CHINA EASTERN AIRLINES";
                    break;
                case "SC":
                    ret = " SHANDONG AIRLINE";
                    break;
                case "ZH":
                    ret = " CHINA SHENZHEN AIRLINE";
                    break;
                case "8C":
                    ret = " EAST STAR AIRLINE";
                    break;
                case "8L":
                    ret = " GRAND INTERNATIONAL AIRWAYS";
                    break;
                default:
                    ret = "δ֪���չ�˾";
                    break;
            }
            return ret;
        }
        public static void CLEARCMDLIST()
        {
            
            connect_4_Command.Qall.Clear();
            connect_4_Command.Qorder.Clear();
            connect_4_Command.Qquery.Clear();
            connect_4_Command.Qsend.Clear();
        }
        public static void CLEARCMDLIST(int msgtype)
        {
            
            switch (msgtype)
            {
                case 0:
                    connect_4_Command.Qall.Clear();
                    connect_4_Command.Qorder.Clear();
                    connect_4_Command.Qquery.Clear();
                    connect_4_Command.Qsend.Clear();
                    break;
                case 3:
                    connect_4_Command.Qall0003.Clear();
                    connect_4_Command.Qorder0003.Clear();
                    connect_4_Command.Qquery0003.Clear();
                    connect_4_Command.Qsend0003.Clear();
                    break;
                case 7:
                    connect_4_Command.Qall0007.Clear();
                    connect_4_Command.Qorder0007.Clear();
                    connect_4_Command.Qquery0007.Clear();
                    connect_4_Command.Qsend0007.Clear();
                    break;
            }
        }
        private delegate void myDelegate();//���� 
        public static void SpecifyCFG(string ipstrings)
        {//ipstringsΪ��~����Ķ��IP��
            try
            {
                GlobalVar.b_switchingconfig = true;
                GlobalVar.current_using_config = ipstrings;
                GlobalVar.loginLC.IPsString = ipstrings;//�Է�ֹ��������������
                string[] ipls = ipstrings.Split('~');

                //���������Ѿ�����ָ������ѯ(������Ĭ��ʹ��ipid����ſ�ǰ�����ã���������ñ�ռ�ã�����ѯ��������)
                int seconds;
                int random = 0;
                if (ipls.Length > 1)//���Ĭ��һ������ modified by king 2010.12.30
                {
                    seconds = DateTime.Now.Second;
                    random = seconds % ipls.Length;
                    //ipls = new string[] { ipls[random] };//�����������ѯ���ܣ����Ѿ������ڿͻ��˽��иÿ���
                    
                    //myDelegate md = delegate()
                    //{
                    //    frmMain.instance.Text = GlobalVar.exeTitle + " - Office:" + GlobalVar.str_cfg_name;//�޸ı����� commentted by king
                    //};
                    //frmMain.instance.BeginInvoke(md);
                }

                GlobalVar.str_cfg_name = GetConfigNumberByIpIdFromXml(ipls[random]);//��Ϊ�ͻ������Զ�si����
                GlobalVar.CurIPUsing = ipls[random];
                int outlength = 0;
                EagleProtocol ep = new EagleProtocol();
                ep.MsgType = 2;
                ep.IPAddress = ipls;
                ep.IPCount = (UInt16)ep.IPAddress.Length;
                ep.SetMsgLength();
                char[] sendstring = ep.ConvertToString(out outlength);
                EagleAPI.EagleSend(sendstring, outlength);
            }
            catch (Exception e)
            {
                MessageBox.Show("SpecifyCFG" + e.Message);
                GlobalVar.b_switchingconfig = false;
            }

        }
        public static void SpecifyPassport()
        {
            EagleAPI.LogWrite("SpecifyPassport...");
            int outlength = 0;
            EagleProtocol ep = new EagleProtocol();
            ep.MsgType = 1;
            ep.MsgBody = GlobalVar.loginLC.PassPort;
            ep.SetMsgLength();
            char[] sendstring = ep.ConvertToString(out outlength);
            EagleAPI.EagleSend(sendstring, outlength);
            EagleAPI.LogWrite("SpecifyPassport...End!");
        }
        public static string GetConfigNumberByIP(string sip)
        {
            return GetConfigNumberByIPFromFile(sip);

        }
        public static string GetIPByConfigNumber(string cfg)
        {
            if (cfg == "ȫ������")
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(GlobalVar.loginXml);
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("IPS");
                string ret = "";
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    if (xn.ChildNodes[i].SelectSingleNode("SrvIp").InnerText.Trim() == GlobalVar.loginLC.SrvIP && xn.ChildNodes[i].SelectSingleNode("SrvPort").InnerText.Trim() == GlobalVar.loginLC.SrvPort.ToString())
                    {
                        //ret += xn.ChildNodes[i].SelectSingleNode("ip").InnerText + "~";
                        ret += xn.ChildNodes[i].SelectSingleNode("ipid").InnerText + "~";//ipid
                    }
                    else if (LogonForm.isZzInternet)//�����֣�ݹ����Ļ���SrvIpӦΪ����ַ�Ŀ������ã���Ȼ�޷��ҵ�����
                    {
                        if (xn.ChildNodes[i].SelectSingleNode("SrvIp").InnerText.Trim() == GlobalVar.gbZzVpnIP && xn.ChildNodes[i].SelectSingleNode("SrvPort").InnerText.Trim() == GlobalVar.loginLC.SrvPort.ToString())
                        {
                            //ret += xn.ChildNodes[i].SelectSingleNode("ip").InnerText + "~";
                            ret += xn.ChildNodes[i].SelectSingleNode("ipid").InnerText + "~";//ipid
                        }
                    }
                }
                if (ret.Length > 1) ret = ret.Substring(0, ret.Length - 1);
                return ret;
            }
            return GetIPByConfigNumberFromFile(cfg);

        }
        public static string GetIPByConfigNumberFromFile(string cfg)
        {
            //if (ret == "")//ipidʱȥ������
                return GetIPByConfigNumberFromXml(cfg);//ֻ�д˾���Ч
            //else return ret;//ipidʱȥ������
        }
        public static string GetIPByConfigNumberFromXml(string cfg)
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(GlobalVar.loginXml);
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("IPS");
                string ret =  "";
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    XmlNode xncfg = xn.ChildNodes[i].SelectSingleNode("PeiZhi");
                    if (cfg == xncfg.InnerText)
                    {
                        //ret += xn.ChildNodes[i].SelectSingleNode("ip").InnerText + "~";
                        ret += xn.ChildNodes[i].SelectSingleNode("ipid").InnerText + "~";//ipid��ɾ��connectcfg.xml���������Ϣ
                    }
                }
                if (ret.Length > 0)
                {
                    ret = ret.Substring(0, ret.Length - 1);
                    return ret;
                }
            }
            catch
            {
            }
            return "";
        }
        /// <summary>
        /// ����IP��ַ
        /// </summary>
        /// <param name="sip"></param>
        /// <returns>OFFICE�ţ���WUH128</returns>
        public static string GetConfigNumberByIPFromFile(string sip)
        {
            {
                return GetConfigNumberByIPFromXml(sip);
            }
            //return sip;
        }
        public static string GetConfigNumberByIPFromXml(string sip)
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(GlobalVar.loginXml);
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("IPS");
                string ret = "";
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    XmlNode xnip = xn.ChildNodes[i].SelectSingleNode("ip");
                    try
                    {
                        if (sip == xnip.InnerText)//�����sip�����ݿ�������ñ�ţ�������ip��ַ���ʸ÷�������ʵ������ commentted by king
                        {
                            ret += xn.ChildNodes[i].SelectSingleNode("PeiZhi").InnerText + "~";
                        }
                        else if (Login_Classes.dns2ip_static(xnip.InnerText.Trim()) == sip)
                        {
                            ret += xn.ChildNodes[i].SelectSingleNode("PeiZhi").InnerText + "~";
                        }
                    }
                    catch
                    {
                        MessageBox.Show("���ò����ڣ�" + sip);
                        continue;
                    }
                }
                if (ret.Length > 0)
                {
                    ret = ret.Substring(0, ret.Length - 1);
                    return ret;
                }
                
            }
            catch(Exception ex)
            {
            }
            return sip;
        }
        public static string GetConfigNumberByIpIdFromXml(string sip)
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(GlobalVar.loginXml);
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("IPS");
                string ret = "";
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    XmlNode xnip = xn.ChildNodes[i].SelectSingleNode("ipid");
                    try
                    {
                        if (sip == xnip.InnerText)//�����sip�����ݿ�������ñ�ţ�������ip��ַ���ʸ÷�������ʵ������ commentted by king
                        {
                            ret += xn.ChildNodes[i].SelectSingleNode("PeiZhi").InnerText + "~";
                        }
                        else if (Login_Classes.dns2ip_static(xnip.InnerText.Trim()) == sip)
                        {
                            ret += xn.ChildNodes[i].SelectSingleNode("PeiZhi").InnerText + "~";
                        }
                    }
                    catch
                    {
                        MessageBox.Show("���ò����ڣ�" + sip);
                        continue;
                    }
                }
                if (ret.Length > 0)
                {
                    ret = ret.Substring(0, ret.Length - 1);
                    return ret.TrimEnd('~');
                }

            }
            catch (Exception ex)
            {
            }
            return sip;
        }
        static public string strClearListCommand = "ab~an~" 
                                                 + "cd~cntd~cp~cv~co~"
                                                 + "ddi~date~da~dq~detr~di~dsm~"
                                                 + "etrf~ec~"
                                                 + "ff~"
                                                 + "help~"
                                                 + "i~ig~"
                                                 + "nt~nfq~nfd~"
                                                 + "pss~"
                                                 + "qs~qr~qn~qe~qd~qc~"
                                                 + "stn~so~siif~si~"
                                                 + "tc~tsl~trfd~tpr~to~tn~time~tim~te~tc~trfu~trfx~tss~ti~"
                                                 + "vt~wf~xc~xi~xo~yi";
        //�ַ��ͺ���գ��뷢��ǰ�������
        static public bool CanClearList(string cmd)
        {
            //if (cmd.IndexOf("@") > -1) return true;
            //if (cmd.IndexOf("\\") > -1) return true;
            if (cmd.ToLower().StartsWith("av")) return false;
            if (cmd.ToLower().StartsWith("rt") && cmd.Length < 7) return false;
            if (cmd.ToLower() == "rtc" || cmd.ToLower().StartsWith("rtu") || cmd.ToLower().StartsWith("rtn") || cmd.ToLower()=="rt") return false;
            if (EagleAPI.GetCmdName(cmd, "rt") != "" && cmd.Length >= 7) return true;
            if (EagleAPI.GetCmdName(cmd, strClearListCommand) != "") return true;
            return false;
        }
        static public string GetCurrentStrings2Send(string cmd, List<string> cmdlist)
        {
            string ret = "i";
            if (CanClearList(cmd)) { cmdlist.Clear(); connect_4_Command.AV_String = ""; }//����ǰ���
            cmdlist.Add(cmd);
            for (int i = 0; i < cmdlist.Count; i++)
            {
                ret += "~" + cmdlist[i];
            }
            if (cmd == "i" || cmd == "ig")
            {
                ret = "i~i";
                cmdlist.Clear();
            }
            if ((cmd.IndexOf("@") > -1) || (cmd.IndexOf("\\") > -1) || (cmd.IndexOf("etdz") == 0) || (cmd.IndexOf("dz") == 0))
            { cmdlist.Clear(); connect_4_Command.AV_String = ""; }//���ͺ����
            return ret;
        }

        static public bool IsRtCode(string code)
        {
            code = code.ToUpper();
            if (code.Contains("O")) return false;
            if (code.Contains("I")) return false;
            if (code.Contains("U")) return false;
            if (code.Length != 5) return false;
            int i = 0;
            while (i < code.Length)
            {
                if (code[i] > 'Z' || code[i] < '0')
                    return false;
                i++;
            }
            string[] word = {"TOTAL","SEATS","CHECK","LEASE","NAMES","MULTI","NVALI","PLEAS" };
            for (i = 0; i < word.Length; i++)
            {
                if (code == word[i]) return false;
            }
            return true;
        }

        static public void GetOptions()
        {
            string filename = System.Windows.Forms.Application.StartupPath + "\\options.XML";
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);

            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("LocalCityCode");
            GlobalVar.LocalCityCode = xn.InnerText;

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("SelectCityType");
            GlobalVar.SelectCityType = xn.InnerText;
            try
            {
                xn = xd.SelectSingleNode("eg");
                xn = xn.SelectSingleNode("isListNoSeatBunk");
                GlobalVar.b_ListNoSeatBunk = (xn.InnerText == "1");
            }
            catch
            {
            }

        }
        static public void GetPrintConfig()
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\ptconfig.mp3", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.GetEncoding("gb2312"));
            List<string> readins = new List<string>();
            string temp;
            temp = sr.ReadLine();
            while (temp != null)
            {
                readins.Add(temp);
                temp = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
            GlobalVar.o_ticket.X = float.Parse(readins[0].Substring(15));
            GlobalVar.o_ticket.Y = float.Parse(readins[1].Substring(15));
            GlobalVar.o_receipt.X = float.Parse(readins[2].Substring(16));
            GlobalVar.o_receipt.Y = float.Parse(readins[3].Substring(16));
            GlobalVar.o_insurance.X = float.Parse(readins[4].Substring(18));
            GlobalVar.o_insurance.Y = float.Parse(readins[5].Substring(18));
            GlobalVar.fontsizecn = float.Parse(readins[6].Substring(11));
            GlobalVar.fontsizeen = float.Parse(readins[7].Substring(11));
        }
        static public void WritePriceTable(string c,string p, string d)
        {
            try
            {
                OleDbConnection cn = new OleDbConnection();
                string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\fc.mdb;";
                cn.ConnectionString = ConnStr;
                cn.Open();
                string s_temp = string.Format("insert into price (CityPair,[PriceY],Distance) values ('{0}','{1}','{2}')", c, p, d);
                OleDbCommand t_cmd = new OleDbCommand(s_temp, cn);
                try
                {
                    if (t_cmd.ExecuteNonQuery() < 1)
                    {
                        MessageBox.Show("δд�뱾�����ݿ�Ʊ�۱�");
                    }
                }
                catch { }
                cn.Close();
            }
            catch
            {
            }
        }
        static public string egReadFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            return temp;
        }
        static public void egSaveFileOverWrite(string filename,string content)
        {
            FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(content);
            sw.Close();
            fs.Close();

        }
        //�Ƿ��Զ�ת��sd,ss
        static public void egNeedTransferSS(string avCmd)
        {
            string avHeads = "avh/~av h ~av h/";
            if (EagleAPI.GetCmdName(avCmd, avHeads) != "") GlobalVar.b_sd2ss = true;
            else GlobalVar.b_sd2ss = false;
        }

        /*/
>PAT:M
WUH MU2501 PVG   1220.00  F            %   1220.00 738 31MAR07 0810 0935
 
>FN FCNY  1220.00/ SCNY 1220.00/ C3.00/ TCNY 50.00CN/ TCNY 50.00YQ
FC:WUH MU PVG 1220.00F
-  CNY  1220.00 END 
FP CASH, CNY*/
        /// <summary>
        /// ͨ��PAT:���ؽ��ȡƱ��
        /// </summary>
        /// <param name="rs">pat:�ķ��ؽ��</param>
        /// <param name="fare">Ʊ��� CNY50.00</param>
        /// <param name="tb">���� CNY40.00</param>
        /// <param name="tf">ȼ�� CNY30.00</param>
        /// <param name="total">�ܼ�</param>
        static public void GetFareFromPat(string rs, ref string fare, ref string tb, ref string tf, ref string total)
        {
            try
            {
                int pos1 = rs.LastIndexOf("FN ");
                int pos2 = rs.IndexOf("YQ", pos1) + 2;
                string fn = rs.Substring(pos1, pos2 - pos1);

                pos1 = fn.IndexOf("FCNY") + 4;
                pos2 = fn.IndexOf('/');
                fare = "CNY" + fn.Substring(pos1, pos2 - pos1).Trim();

                pos1 = fn.IndexOf("TCNY") + 4;
                pos2 = fn.IndexOf("CN", pos1);
                tb = "CNY" + fn.Substring(pos1, pos2 - pos1).Trim();

                pos1 = fn.IndexOf("TCNY", pos2) + 4;
                pos2 = fn.IndexOf("YQ", pos1);
                tf = "CNY" + fn.Substring(pos1, pos2 - pos1).Trim();

                total = string.Format("{0}",
                    float.Parse(fare.Substring(3).Trim()) + float.Parse(tb.Substring(3).Trim()) + float.Parse(tb.Substring(3).Trim()));
            }
            catch
            {
                fare = tb = tf = "";
                total = "CNY0";
            }
            if (fare == "CNY0.00")
            {
            }
        }
        //�ر�FN���ʾ,FN/FCNY360.00/SCNY360.00/C3.00/XCNY80.00/TCNY50.00CN/TCNY30.00YQ/ACNY440.00
        static public string CloseFnItem(string rs)
        {
            string fc="";
            string oldstring = rs;
            //try
            //{
            //    fc = EagleAPI.GetFare(rs);                
            //}
            //catch
            //{
            //}
            //if (fc != "")//ȡ��FN��
            {
                try
                {
                    int beg = (rs.IndexOf("FN/FCNY"));
                    int mid = (rs.IndexOf("ACNY", beg));
                    int end = (rs.IndexOf("\n", mid));
                    oldstring = rs.Remove(beg + 3, end - beg - 3);
                }
                catch
                {
                }
            }
            return oldstring;
        }
        /*ȡdetr:tn,F�����֤��
NAME: ��֪��   TKTN:7844940061811
RCPT:
  1         RP7111206827
  2         NI510227195807150055
         * */
        static public string GetCardIdByDetr_F(string rs)
        {
            int pos1 = rs.LastIndexOf("NI");
            if (pos1 < 0) return "";
            int pos2 = rs.IndexOf("\r\n",pos1);
            if (pos2 < pos1) return "";
            return rs.Substring(pos1 + 2, pos2 - pos1 - 2).Trim();
        }
        

        //�����ʺ�ʹ��
        static public bool test()
        {
            try
            {
                //if (GlobalVar.loginName.ToLower() == "bbb" || mystring.right(GlobalVar.loginName, 5).ToLower() == "eagle") return true;
                
            }
            catch
            {
            }
            return false;
        }
    }
}
