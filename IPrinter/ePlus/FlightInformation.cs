using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ePlus
{
    /// <summary>
    /// 单个航班信息，用于简化式订票界面
    /// </summary>
    class FlightInformation
    {
        public string fiString="";
        public string FI_ID = "";
        public string FI_FlightNo = "";
        public string FI_Policy = "";
        public string FI_Path = "";
        public string FI_TakeOff = "";//起飞时间
        public string FI_Landing = "";//到达时间
        public string FI_AirType = "";
        public List<string> FI_Bunk = new List<string> ();//所有舱位情况列表
        public void setVar()
        {
            //fiString = fiString.Replace('-', ' ');    //Eric
            //fiString = fiString.Replace('+', ' ');    //Eric
            string[] sq = new string[1];
            sq[0] = "\n>";
            string[] fiRow = fiString.Split(sq, StringSplitOptions.RemoveEmptyEntries);
            string temp_bunk = "";
            for (int i = 0; i < fiRow.Length; i++)
            {
                string temp = fiRow[i];
                
                if (i == 0)
                {
                    FI_FlightNo = EagleAPI.substring(temp, 2, 8).Trim();
                    FI_Policy = EagleAPI.substring(temp, 11, 3).Trim();
                    temp_bunk += EagleAPI.substring(temp, 15, 44 - 15).Trim();
                    FI_Path = EagleAPI.substring(temp, 46, 6).Trim();
                    FI_TakeOff = EagleAPI.substring(temp, 53, 6);//Eric 原FI_TakeOff = EagleAPI.substring(temp, 53, 4);
                    FI_Landing = EagleAPI.substring(temp, 60, 6);//Eric 原FI_Landing = EagleAPI.substring(temp, 60, 4);
                    FI_AirType = EagleAPI.substring(temp, 67, 3);
                }
                if (i == 1)
                {
                    temp_bunk += (" " + EagleAPI.substring(temp, 15, temp.Length - 15).Trim());
                    int subpos = temp_bunk.IndexOf("\r\n");
                    if (subpos > 0) temp_bunk = temp_bunk.Substring(0, subpos);
                }
            }
            while (temp_bunk.IndexOf("  ") > -1)
            {
                temp_bunk = temp_bunk.Replace("  ", " ");
            }
            string[] bk = temp_bunk.Split(' ');
            for (int i = 0; i < bk.Length; i++)
            {
                if (bk[i].Length == 2)
                {
                    FI_Bunk.Add(bk[i]);
                }
            }
            //FI_Bunk.Sort();
        }
        public void SetToListview(System.Windows.Forms.ListView lv,DateTime dt)
        {
            string tt = "";
            tt = EagleAPI.GetAirLineBunkSort(FI_FlightNo[0]=='*'?FI_FlightNo.Substring(1, 2):FI_FlightNo.Substring(0,2),dt);
            char[] ch = tt.ToCharArray();
            sortbunk(ch);


            System.Windows.Forms.ListViewItem item = new ListViewItem();
            item.Text = FI_ID;
            item.SubItems.Add(FI_FlightNo);
            item.SubItems.Add(FI_Policy);
            item.SubItems.Add(FI_Path);
            item.SubItems.Add(FI_TakeOff);
            item.SubItems.Add(FI_Landing);
            item.SubItems.Add(FI_AirType);
            ////for (char c = 'A'; c <= 'Z'; c++)
            ////{
            ////    item.SubItems.Add(GetOneBunk(c));
            ////}
            //item.SubItems.Add(GetOneBunk('F'));
            //item.SubItems.Add(GetOneBunk('C'));
            //item.SubItems.Add(GetOneBunk('Y'));
            //if (FI_FlightNo.Substring(0, 2) != "MU" && FI_FlightNo.Substring(0, 2) != "CA" && FI_FlightNo.Substring(0, 2) != "SC")
            //{
            //    item.SubItems.Add("");
            //}
            //int y_start = 0;
            //for (int i = 0; i < FI_Bunk.Count; i++)
            //{
            //    if (FI_Bunk[i][0] == 'Y')
            //    {
            //        y_start = i+1;
            //        break;
            //    }
            //}
            //for (int i = y_start; i < FI_Bunk.Count; i++)
            for (int i = 0; i < FI_Bunk.Count; i++)
            {
                if (GlobalVar.b_ListNoSeatBunk)
                    item.SubItems.Add(FI_Bunk[i]);
                else
                    item.SubItems.Add(FI_Bunk[i][1] > 'A' ? "" : FI_Bunk[i]);
            }
            lv.Items.Add(item);
        }
        //按舱位序列排序
        void sortbunk(char[] input)
        {
            List<string> ls = new List<string>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ') { ls.Add("  "); continue; }
                int j=0;
                for (j = 0; j < FI_Bunk.Count; j++)
                {
                    if (FI_Bunk[j][0] == input[i])
                    {
                        ls.Add(FI_Bunk[j]);
                        FI_Bunk.RemoveAt(j);
                        break;
                    }
                }
                //找不到，则以空格代替
                if (j == FI_Bunk.Count)
                {
                    ls.Add("  ");
                }
            }
            
            for (int i = 0; i < FI_Bunk.Count; i++)
            {
                ls.Add(FI_Bunk[i]);
            }
            FI_Bunk = ls;
        }
        private string GetOneBunk(char bk)
        {
            for (int i = 0; i < FI_Bunk.Count; i++)
            {
                if (FI_Bunk[i][0] == bk) return FI_Bunk[i];
            }
            return "";
        }



    }
    /// <summary>
    /// 一个航程信息，至少包含一个航班，可能有二个航班
    /// </summary>
    class SailInformation
    {
        public string siString = "";
        public FlightInformation[] fi;
        
    }
    /// <summary>
    /// AV返回结果分析
    /// </summary>
    class AVResult
    {
        public string avResult = "";
        public string avDate = "";
        public string avFromTo = "";
        public int i_sail = 0;

        public SailInformation[] si;


        private string[] sp = new string [8];//分割符为"\ni"


        public AVResult()
        {
            avResult += " 27JUL(THU) WUHHKG\n";
            avResult += "1-  CZ3075  DS# CL DL IL JL YA KA HA MA QA VQ  WUHHKG 0900   1035   738 0\n";
            avResult += ">               XQ ZA\n";
            avResult += "2   CZ3368  DS# FA AA PA RA YA TA KA HA MA GA  WUHCAN 0840   1000   738 0    E\n";
            avResult += ">               SA LA QA UA EA VS BA XS ZA\n";
            avResult += "    CZ303   AS# CA DS IS JS YA KA HS MA QS VS     HKG 1130   1220   319 0    E\n";
            avResult += ">               XS ZA\n";
            avResult += "3   8C8231  DS# FA AS YA TA KA HA MA GA SS LS  WUHCAN 1400   1535   319 0    E\n";
            avResult += ">               QQ EQ WQ ZQ VQ XQ BQ IQ US NQ\n";
            avResult += "   *KA1305  DS* JR CR YR BR HR                    HKG 1830   1920   320 0 M \n";
            avResult += ">   CZ305\n";
            avResult += "4   CZ3343  DS# FA AA PA RA YA TA KA HA MA GA  WUHCAN 1520   1635   738 0    E\n";
            avResult += ">               SS LS QS UA ES VS BA XS ZA\n";
            avResult += " +  CZ305   DS# CA DQ IQ JQ YA KA HS MA QQ VQ     HKG 1830   1920   320 0^   E\n";
            avResult += ">               XQ ZA\n";

            for (int i = 0; i < 8; i++)
            {
                int itemp = i + 1;
                sp[i] = "\n" + itemp.ToString();
            }
        }
        public void SetVar()
        {
            try
            {
                string[] avRow = avResult.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                i_sail = avRow.Length - 1;//航班数
                avDate = avRow[0].Substring(0, 11).Trim();//27JUL(THU)
                avFromTo = avRow[0].Substring(11,7).Trim();//WUHHKG
                si = new SailInformation[i_sail];//航程，可能有2个航段
                for (int i = 0; i < i_sail; i++)
                {
                    si[i] = new SailInformation();
                    si[i].siString = avRow[i + 1];
                    string[] sq = new string[1];
                    sq[0] = "\n ";
                    string[] siSplit = si[i].siString.Split(sq, StringSplitOptions.RemoveEmptyEntries);
                    si[i].fi = new FlightInformation[siSplit.Length];
                    for (int j = 0; j < siSplit.Length; j++)
                    {
                        si[i].fi[j] = new FlightInformation();
                        si[i].fi[j].fiString = siSplit[j];
                        si[i].fi[j].setVar();
                        int itemp = i + 1;
                        si[i].fi[j].FI_ID = itemp.ToString();
                    }
                }
            }
            catch
            {
                //MessageBox.Show(avResult);
            }
        }
        public void SetToListview(System.Windows.Forms.ListView lv,DateTime dt)
        {
            try
            {
                SetVar();
                for (int i = 0; i < si.Length; i++)
                {
                    for (int j = 0; j < si[i].fi.Length; j++)
                    {
                        si[i].fi[j].SetToListview(lv,dt);
                    }
                }
                bool bEven = true;
                for (int i = 0; i < lv.Items.Count; i++) 
                {
                    bEven = !bEven;
                    if (bEven) lv.Items[i].BackColor = System.Drawing.Color.LightGray;
                    else lv.Items[i].BackColor = System.Drawing.Color.White;

                }
            }
            catch
            {
            }
        }

    }
    class av_sd
    {
        public string avstring = "";
        /// <summary>
        /// sd转换为ss指令
        /// </summary>
        /// <param name="sd"></param>
        /// <returns></returns>
        public string sd2ss(string sd)
        {
            try
            {
                if (!GlobalVar.b_sd2ss) return sd;
                string ret = "";
                sd = sd.Trim();
                if (EagleAPI.substring(sd, 0, 2).ToLower() == "sd")
                {
                    string sd_sub = sd.Substring(2).Trim();
                    int sd_i = int.Parse(sd_sub[0].ToString()) - 1;
                    if (sd_i < 0 || sd_i > 7) return sd;
                    AVResult ar = new AVResult();
                    ar.avResult = avstring;
                    EagleString.AvResult avres = new EagleString.AvResult(avstring);
                    ar.SetVar();
                    string state = sd_sub.Substring(2).Trim();
                    if (state[0] != '/') state = "/LL"+state;
                    for (int i = 0; i < ar.si[sd_i].fi.Length; i++)
                    {
                        if (i == 0)
                        {
                            string stemp = ar.si[sd_i].fi[i].FI_Path;
                            if (stemp == "") stemp = avres.CityPair;
                            ret += "\xdss:" + ar.si[sd_i].fi[i].FI_FlightNo + "/" + sd_sub[1].ToString() + "/" + ar.avDate.Substring(0, 5) + "/" + stemp + state;
                        }
                        if (i == 1)
                        {
                            string citystart = ar.si[sd_i].fi[0].FI_Path.Substring(3);
                            string city2end = ar.si[sd_i].fi[1].FI_Path.Trim();
                            citystart += city2end.Substring(city2end.Length - 3);
                            string strNewAvDate = "";
                            string temp="";
                            if(ar.si[sd_i].fi[i].FI_TakeOff.Length>=6)
                                temp=ar.si[sd_i].fi[i].FI_TakeOff.Substring(4,2);
                            if (temp.IndexOf("+") != -1)
                            {
                                strNewAvDate=(Convert.ToInt32(ar.avDate.Substring(0, 2)) 
                                    + Convert.ToInt32(temp.Substring(1, 1))) + ar.avDate.Substring(2, 3);
                            }
                            if (strNewAvDate == "") strNewAvDate = ar.avDate.Substring(0, 5);//add by wfb : 2009-2-18
                            //Eric 原 ret += "\xdss:" + ar.si[sd_i].fi[i].FI_FlightNo + "/" + sd_sub[1].ToString() + "/" + ar.avDate.Substring(0, 5) + "/" + citystart + state;
                            ret += "\xdss:" + ar.si[sd_i].fi[i].FI_FlightNo + "/" + sd_sub[1].ToString() + "/" + strNewAvDate + "/" + citystart + state;
                        }

                    }
                    ret = ret.Substring(1);
                    while (ret.IndexOf("*") > -1)
                    {
                        //MessageBox.Show("共享航班不能订座！");
                        
                        ret = ret.Remove(ret.IndexOf("*"), 1);
                    }
                    return ret;
                }
            }
            catch(Exception ea)
            {
                if (connect_4_Command.SendString.Substring(15, 2).IndexOf("/") == -1)
                    EagleString.EagleFileIO.LogWrite("SD2SS FAILED!" + sd);
            }

            return sd;
        }

        public string fdi2fdxx(string fd)
        {
            try
            {
                string ret = "";
                fd = fd.Trim();
                if (EagleAPI.substring(fd, 0, 2).ToLower() == "fd")
                {
                    string fd_sub = fd.Substring(2).Trim();
                    if (fd_sub[0] < '1' || fd_sub[0] > '8') return fd;
                    int fd_i = int.Parse(fd_sub[0].ToString()) - 1;
                    AVResult ar = new AVResult();
                    ar.avResult = avstring;
                    
                    ar.SetVar();

                    ret = "fd" + ar.avFromTo + "/" + ar.avDate + "/" + ar.si[fd_i].fi[0].FI_FlightNo.Substring(0, 2);
                    return ret;
                }
            }
            catch
            {
            }
            return fd;
        }
    }
}
