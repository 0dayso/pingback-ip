using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EagleControls
{
    /// <summary>
    /// �ؼ���һЩ�����࣬���ؼ���Ϊ�������ڱ����н���һЩ�������
    /// </summary>
    public class Operators
    {
        public static void ListView_PositionOfMouse(ListView lv, MouseEventArgs e, ref int row, ref int col)
        {
            ListView_PositionOfMouse(lv, e, null,ref row, ref col);
        }
        /// <summary>
        /// ������װ��У������ListView�е�����λ��,����ʱ����-1��Ҫ���ǹ�������λ��(�ѹ�ʱ)
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="e"></param>
        /// <param name="il"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public static void ListView_PositionOfMouse(ListView lv,MouseEventArgs e, ImageList il, ref int row, ref int col)
        {
            try
            {
                //int xpt = 0;
                //for (int i = 0; i < lv.Columns.Count; i++)
                //{
                //    xpt += lv.Columns[i].Width;
                //    if (e.X < xpt)
                //    {
                //        col = i;
                //        break;
                //    }
                //}
                //if (il != null)
                //    row = (e.Y - 16) / (il.ImageSize.Height + 1);//1Ϊ�߿� 16ΪHeader�ĸ�
                //else
                //    row = (e.Y - 16) / 16;//��ĸΪ��ImageList��Ĭ���и�
                try
                {
                    row = lv.GetItemAt(e.X, e.Y).Index;//�����ֱ����������
                    
                    ListViewItem lvi = lv.Items[row];
                    ListViewItem.ListViewSubItem lvsi = lvi.GetSubItemAt(e.X, e.Y);
                    for (int i = 0; i < lvi.SubItems.Count; ++i)
                    {
                        if (lvi.SubItems[i] == lvsi)
                        {
                            col = i;
                            break;
                        }
                    }
                }
                catch
                {
                    row = -1;
                    col = -1;
                }
            }
            catch (Exception ex)
            {
                EagleString.EagleFileIO.LogWrite("EagleControl.Operations.ListView_PositionOfMouse : " + ex.Message);
            }
        }
        /// <summary>
        /// ������װ��У���ListView������ѡ�еĺ���
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="e"></param>
        /// <param name="il"></param>
        /// <param name="dt"></param>
        /// <param name="flightno"></param>
        /// <param name="citypair"></param>
        /// <param name="bunk"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool ListView_FlightInfoOfClickItem(ListView lv, MouseEventArgs e, ImageList il,DateTime dt,
            ref string[] flightno, ref string[] citypair, ref string[] bunk,ref string [] date)
        {
            try
            {
                int nr = -1;
                int nc = -1;
                ListView_PositionOfMouse(lv, e, il, ref nr, ref nc);
                nr = lv.SelectedItems[0].Index;
                int nr2 = -1;//���̺�������lv�е�����

                string s_id = lv.SelectedItems[0].SubItems[0].Text;
                int nflights = 0;
                foreach (ListViewItem lvi in lv.Items)
                {
                    if (lvi.SubItems[0].Text == s_id)
                    {

                        ++nflights;
                        if (lvi.Index != nr) nr2 = lvi.Index;
                    }
                }
                //�õ���1/2���������(nrΪ�����������,nr2Ϊ��Ӧ���̵�����,��û��Ϊ-1)

                string s_flight, s_cp, s_bunk, s_date;
                string s_flight2, s_cp2, s_bunk2, s_date2;
                s_flight = s_cp = s_bunk = s_date = s_flight2 = s_cp2 = s_bunk2 = s_date2 = "";
                s_flight = lv.Items[nr].SubItems[1].Text;
                if (!EagleString.BaseFunc.FlightValidate(s_flight))
                {
                    EagleString.EagleFileIO.LogWrite("�������");
                    return false;
                }
                if (s_flight[0] == '*')
                {
                    s_flight = s_flight.Substring(1);
                    MessageBox.Show("ע��:" + s_flight + "�ǹ�����!������Ч!");
                }
                s_cp = lv.Items[nr].SubItems[3].Text;
                s_date = dt.ToString("ddMMM", EagleString.egString.dtFormat);
                if (nr2 > -1)
                {
                    s_flight2 = lv.Items[nr2].SubItems[1].Text;
                    if (s_flight2[0] == '*')
                    {
                        s_flight2 = s_flight2.Substring(1);
                        MessageBox.Show("ע��:" + s_flight2 + "�ǹ�����!������Ч!");
                    }
                    s_cp2 = lv.Items[nr2].SubItems[3].Text;
                    s_date2 = dt.ToString("ddMMM", EagleString.egString.dtFormat);
                }
                if (nc >= 7)
                {
                    s_bunk = EagleString.egString.substring(lv.Items[nr].SubItems[nc].Text, 0, 1);
                    if (nr2 > -1) s_bunk2 = EagleString.egString.substring(lv.Items[nr2].SubItems[nc].Text, 0, 1);
                }

                bool s_switched = false;
                if (nr2 > -1 && nr2 < nr)//����
                {
                    EagleString.egString.Switch(ref s_flight, ref s_flight2);
                    EagleString.egString.Switch(ref s_cp, ref s_cp2);
                    EagleString.egString.Switch(ref s_bunk, ref s_bunk2);

                    s_switched = true;
                }
                if (nr2 > -1 && s_cp2.Trim().Length == 3) s_cp2 = s_cp.Substring(3) + s_cp2;
                //����ȡ�õ����Ķ�Ӧ������Ϣ

                //��麽���Ƿ����������Ѿ�����(�����+����)
                bool s_exist = false;
                bool s_full = true;
                for (int i = 0; i < flightno.Length; ++i)
                {
                    if (flightno[i] == "") s_full = false;
                    if (s_flight == flightno[i] && s_date == date[i])
                    {
                        if (s_bunk != "" && !s_switched)
                            bunk[i] = s_bunk;
                        s_exist = true;
                    }
                }

                if (!s_exist && !s_full)//�����ڣ�����δ���������հ׾�
                {
                    for (int i = 0; i < flightno.Length; ++i)
                    {
                        if (flightno[i] == "")
                        {
                            flightno[i] = s_flight;
                            citypair[i] = s_cp;
                            if (!s_switched)
                                bunk[i] = s_bunk;//�����������в�λ��ֵ
                            date[i] = s_date;
                            break;
                        }
                    }
                }
                else if (s_full && !s_exist) MessageBox.Show("����4�����β������ӣ������������Ҫ�ĺ��Σ�");

                s_exist = false;
                s_full = true;
                if (nr2 > -1)
                {
                    for (int i = 0; i < flightno.Length; ++i)
                    {
                        if (flightno[i] == "") s_full = false;
                        if (s_flight2 == flightno[i] && s_date2 == date[i])
                        {
                            if (s_bunk2 != "" && (s_switched))
                                bunk[i] = s_bunk2;
                            s_exist = true;
                        }
                    }
                    if (!s_exist && !s_full)//�����ڣ�����δ���������հ׾�
                    {
                        for (int i = 0; i < flightno.Length; ++i)
                        {
                            if (flightno[i] == "")
                            {
                                flightno[i] = s_flight2;
                                citypair[i] = s_cp2;
                                if (s_switched)
                                    bunk[i] = s_bunk2;//����������������ˣ��ڶ�����Ϊ�������
                                date[i] = s_date2;
                                break;
                            }
                        }
                    }
                    else if (s_full && !s_exist) MessageBox.Show("����4�����β������ӣ������������Ҫ�ĺ��Σ�");
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// ���ݼ������ε����ڣ�ȡ��Ʊʱ�����ڼ�ʱ��
        /// </summary>
        /// <param name="date"></param>
        /// <param name="limitDay"></param>
        /// <param name="limitTime"></param>
        public static void LimitTime(string [] date,ref string limitDay,ref string limitTime)
        {
            DateTime dt = DateTime.Now;
            for (int i = 0; i < date.Length; ++i)
            {
                try
                {
                    dt = EagleString.BaseFunc.str2datetime(date[i], true);
                    break;
                }
                catch
                {
                }
            }
            for (int i = 0; i < date.Length; ++i)
            {
                try
                {
                    if (date[i] == "") continue;
                    DateTime d = EagleString.BaseFunc.str2datetime(date[i], true);
                    if (d < dt) dt = d;
                }
                catch(Exception ex)
                {
                    EagleString.EagleFileIO.LogWrite("Operators.LimitTime : " + ex.Message);
                }
            }
            limitDay = dt.ToString("ddMMM", EagleString.egString.dtFormat);
            if (DateTime.Now > dt)
            {
                limitTime = DateTime.Now.AddHours(1).ToString("HHmm");
                //MessageBox.Show("ע��:���ڵ��캽�࣬����һСʱ�ڳ�Ʊ��");
            }
            else limitTime = "0500";
            
        }
        public static void LimitTime(DateTime[] date, ref string limitDay, ref string limitTime)
        {
            DateTime dt = DateTime.Now;
            for (int i = 0; i < date.Length; ++i)
            {
                try
                {
                    if (date[i] > dt) dt = date[i];
                    break;
                }
                catch
                {
                }
            }
            for (int i = 0; i < date.Length; ++i)
            {
                try
                {
                    if (date[i] <DateTime.Now) continue;
                    DateTime d = date[i];
                    if (d < dt) dt = d;
                }
                catch (Exception ex)
                {
                    EagleString.EagleFileIO.LogWrite("Operators.LimitTime : " + ex.Message);
                }
            }
            limitDay = dt.ToString("ddMMM", EagleString.egString.dtFormat);
            if (DateTime.Now > dt)
            {
                limitTime = DateTime.Now.AddHours(1).ToString("HHmm");
                //MessageBox.Show("ע��:���ڵ��캽�࣬����һСʱ�ڳ�Ʊ��");
            }
            else limitTime = "0500";
        }
        public static void LimitTime(DateTime[] date, ref DateTime limitTime)
        {
            string day = "";
            string time = "";
            LimitTime(date, ref day, ref time);
            DateTime dt = EagleString.BaseFunc.str2datetime(day, true);
            int h = int.Parse(time.Substring(0, 2));
            int m = int.Parse(time.Substring(2));
            limitTime = dt.AddHours(h).AddMinutes(m);

        }
        /// <summary>
        /// һЩ�����Ĳ���
        /// </summary>
        public class richedit
        {
            public static void BlackWindowPolicy(RichTextBox rtb, EagleString.AvResult ar, EagleString.ProfitResult pr)
            {
                   
            }
        }
        /// <summary>
        /// һЩ�����ڲ���
        /// </summary>
        public class mainforms
        {
            
        }
    }
}
