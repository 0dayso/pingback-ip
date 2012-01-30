using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EagleControls
{
    /// <summary>
    /// 控件的一些操作类，将控件作为参数，在本类中进行一些特殊操作
    /// </summary>
    public class Operators
    {
        public static void ListView_PositionOfMouse(ListView lv, MouseEventArgs e, ref int row, ref int col)
        {
            ListView_PositionOfMouse(lv, e, null,ref row, ref col);
        }
        /// <summary>
        /// 计算简易版中，鼠标在ListView中的行列位置,超出时返回-1，要考虑滚动条的位置(已过时)
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
                //    row = (e.Y - 16) / (il.ImageSize.Height + 1);//1为线宽 16为Header的高
                //else
                //    row = (e.Y - 16) / 16;//分母为无ImageList的默认行高
                try
                {
                    row = lv.GetItemAt(e.X, e.Y).Index;//解决垂直滚动条问题
                    
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
        /// 计算简易版中，在ListView单击后选中的航段
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
                int nr2 = -1;//联程航班行在lv中的索引

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
                //得到了1/2航班的索引(nr为被点击的索引,nr2为对应联程的索引,若没有为-1)

                string s_flight, s_cp, s_bunk, s_date;
                string s_flight2, s_cp2, s_bunk2, s_date2;
                s_flight = s_cp = s_bunk = s_date = s_flight2 = s_cp2 = s_bunk2 = s_date2 = "";
                s_flight = lv.Items[nr].SubItems[1].Text;
                if (!EagleString.BaseFunc.FlightValidate(s_flight))
                {
                    EagleString.EagleFileIO.LogWrite("航班错误！");
                    return false;
                }
                if (s_flight[0] == '*')
                {
                    s_flight = s_flight.Substring(1);
                    MessageBox.Show("注意:" + s_flight + "是共享航班!可能无效!");
                }
                s_cp = lv.Items[nr].SubItems[3].Text;
                s_date = dt.ToString("ddMMM", EagleString.egString.dtFormat);
                if (nr2 > -1)
                {
                    s_flight2 = lv.Items[nr2].SubItems[1].Text;
                    if (s_flight2[0] == '*')
                    {
                        s_flight2 = s_flight2.Substring(1);
                        MessageBox.Show("注意:" + s_flight2 + "是共享航班!可能无效!");
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
                if (nr2 > -1 && nr2 < nr)//交换
                {
                    EagleString.egString.Switch(ref s_flight, ref s_flight2);
                    EagleString.egString.Switch(ref s_cp, ref s_cp2);
                    EagleString.egString.Switch(ref s_bunk, ref s_bunk2);

                    s_switched = true;
                }
                if (nr2 > -1 && s_cp2.Trim().Length == 3) s_cp2 = s_cp.Substring(3) + s_cp2;
                //以上取得点击后的对应航班信息

                //检查航班是否在数组中已经存在(航班号+日期)
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

                if (!s_exist && !s_full)//不存在，并且未满，则加入空白久
                {
                    for (int i = 0; i < flightno.Length; ++i)
                    {
                        if (flightno[i] == "")
                        {
                            flightno[i] = s_flight;
                            citypair[i] = s_cp;
                            if (!s_switched)
                                bunk[i] = s_bunk;//被点击项才能有舱位赋值
                            date[i] = s_date;
                            break;
                        }
                    }
                }
                else if (s_full && !s_exist) MessageBox.Show("超出4个航段不能增加，请先清除不需要的航段！");

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
                    if (!s_exist && !s_full)//不存在，并且未满，则加入空白久
                    {
                        for (int i = 0; i < flightno.Length; ++i)
                        {
                            if (flightno[i] == "")
                            {
                                flightno[i] = s_flight2;
                                citypair[i] = s_cp2;
                                if (s_switched)
                                    bunk[i] = s_bunk2;//被点击项，如果被交换了，第二航段为被点击项
                                date[i] = s_date2;
                                break;
                            }
                        }
                    }
                    else if (s_full && !s_exist) MessageBox.Show("超出4个航段不能增加，请先清除不需要的航段！");
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 根据几个航段的日期，取出票时限日期及时间
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
                //MessageBox.Show("注意:由于当天航班，请在一小时内出票！");
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
                //MessageBox.Show("注意:由于当天航班，请在一小时内出票！");
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
        /// 一些黑屏的操作
        /// </summary>
        public class richedit
        {
            public static void BlackWindowPolicy(RichTextBox rtb, EagleString.AvResult ar, EagleString.ProfitResult pr)
            {
                   
            }
        }
        /// <summary>
        /// 一些主窗口操作
        /// </summary>
        public class mainforms
        {
            
        }
    }
}
