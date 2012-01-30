using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.IO;
using gs.DataBase;

namespace logic
{
    public class MyExcel
    {
        
        public System.IO.FileStream fs;
        System.IO.StreamWriter sw;
        public void CreateExcel(DataSet ds, string FileName)
        {
            
            string colHeaders = "", ls_item = "";

            //定义表对象与行对象，同时用DataSet对其值进行初始化 
            DataTable dt = ds.Tables[0];
            DataRow[] myRow = dt.Select();//可以类似dt.Select("id>10")之形式达到数据筛选目的
            int i = 0;
            int cl = dt.Columns.Count;

            //取得数据表各列标题，各标题之间以t分割，最后一个列标题后加回车符 
            //for (i = 0; i < cl; i++)
            //{
            //    if (i == (cl - 1))//最后一列，加n
            //    {
            //        colHeaders += dt.Columns[i].Caption.ToString() + "\n";
            //    }
            //    else
            //    {
            //        colHeaders += dt.Columns[i].Caption.ToString() + "\t";
            //    }

            //}
            colHeaders="用户,名称,操作时间,PNR,行程单号,脱机打印,电子票号,航班1,舱位1,航班2,舱位2,城市对1,城市对2,乘机日1,乘机日2,总价,客票状态,燃油,基建,票价\n";
            colHeaders=colHeaders.Replace(",","\t");
            SaveExcel(colHeaders, FileName);
            //逐行处理数据   
            foreach (DataRow dr in myRow)
            {
                //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据     
                //for (i = 0; i < cl; i++)
                //{
                //    if (i == (cl - 1))//最后一列，加n
                //    {
                //        ls_item += row[i].ToString() + "\n";
                //    }
                //    else
                //    {
                //        ls_item += row[i].ToString() + "\t";
                //    }

                //}
                //ls_item += ls_item;
                string state = (dr["State"].ToString()=="0"?"正常":"退票");
				string isOffline = dr["isOffline"].ToString() == "1"? "是" : "";
                ls_item = dr["userid"] + ","
                            + dr["vcUserTitle"] + ","
                            + dr["OperateTime"] + ","
                            + dr["Pnr"] + ","
                            + dr["receiptNumber"] + ","
                            + isOffline + ","
                            + dr["etNumber"] + ","
                            + dr["FlightNumber1"] + ","
                            + dr["Bunk1"] + ","
                            + dr["FlightNumber2"] + ","
                            + dr["Bunk2"] + ","
                            + dr["CityPair1"] + ","
                            + dr["CityPair2"] + ","
                            + dr["Date1"] + ","
                            + dr["Date2"] + ","
                            + dr["TotalFC"] + ","
                            + state + ","
                            + dr["numOilPrc"] + ","
                            + dr["numBasePrc"] + ","
                            + (getTheVal(dr["TotalFC"]) - getTheVal(dr["numOilPrc"]) - getTheVal(dr["numBasePrc"])).ToString()+"\n";
                SaveExcel(ls_item,FileName);
                ls_item = string.Empty;
            }
            
            
        }

        public void CreateExcel2(DataSet ds, string FileName)
        {

            string colHeaders = "", ls_item = "";

            //定义表对象与行对象，同时用DataSet对其值进行初始化 
            DataTable dt = ds.Tables[0];
            DataRow[] myRow = dt.Select();//可以类似dt.Select("id>10")之形式达到数据筛选目的
            int i = 0;
            int cl = dt.Columns.Count;

            //取得数据表各列标题，各标题之间以t分割，最后一个列标题后加回车符 
            //for (i = 0; i < cl; i++)
            //{
            //    if (i == (cl - 1))//最后一列，加n
            //    {
            //        colHeaders += dt.Columns[i].Caption.ToString() + "\n";
            //    }
            //    else
            //    {
            //        colHeaders += dt.Columns[i].Caption.ToString() + "\t";
            //    }

            //}
            colHeaders = "用户,名称,操作时间,PNR,行程单号,脱机打印,电子票号,航班1,舱位1,航班2,舱位2,城市对1,城市对2,乘机日1,乘机日2,总价,客票状态,燃油,基建,票价\n";
            colHeaders = colHeaders.Replace(",", "\t");
            SaveExcel(colHeaders, FileName);
            //逐行处理数据   
            foreach (DataRow dr in myRow)
            {
                //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据     
                //for (i = 0; i < cl; i++)
                //{
                //    if (i == (cl - 1))//最后一列，加n
                //    {
                //        ls_item += row[i].ToString() + "\n";
                //    }
                //    else
                //    {
                //        ls_item += row[i].ToString() + "\t";
                //    }

                //}
                //ls_item += ls_item;
                string state = (dr["State"].ToString() == "0" ? "正常" : "退票");
                string isOffline = dr["isOffline"].ToString() == "1" ? "是" : "";
                ls_item = dr["userid"] + ","
                            + dr["vcUserTitle"] + ","
                            + dr["OperateTime"] + ","
                            + dr["Pnr"] + ","
                            + dr["receiptNumber"] + ","
                            + isOffline + ","
                            + dr["etNumber"] + ","
                            + dr["FlightNumber1"] + ","
                            + dr["Bunk1"] + ","
                            + dr["FlightNumber2"] + ","
                            + dr["Bunk2"] + ","
                            + dr["CityPair1"] + ","
                            + dr["CityPair2"] + ","
                            + dr["Date1"] + ","
                            + dr["Date2"] + ","
                            + dr["TotalFC"] + ","
                            + state + ","
                            + dr["numOilPrc"] + ","
                            + dr["numBasePrc"] + ","
                            + (getTheVal(dr["TotalFC"]) - getTheVal(dr["numOilPrc"]) - getTheVal(dr["numBasePrc"])).ToString() + "\n";
                SaveExcel(ls_item, FileName);
                ls_item = string.Empty;
            }


        }
        private double getTheVal(object p_obj)
        {
            double dbRet = 0;
            if (p_obj != null && p_obj.ToString().Trim() != "")
            {
                dbRet = Double.Parse(p_obj.ToString());
            }
            return dbRet;
        }
        public void SaveExcel(string excel,string filename)
        {
            try
            {
                fs = new System.IO.FileStream(filename, System.IO.FileMode.Append);
                sw = new System.IO.StreamWriter(fs,System.Text.Encoding.Default);
                sw.Write(excel);
                sw.Close();
                fs.Close();
            }
            catch
            {
                gs.util.func.Write("save Error!");
            }
        }

        protected void SaveExcel2(object sender, System.EventArgs e)
        {
            
            
            

                //string strQry = "";
        

                //Conn cn = null;
                //DataSet ds = null;

                //try
                //{
                //    cn = ConnFact.getConn();
                //    ds = cn.GetDataSet(strQry);
                //}
                //catch (Exception exCn)
                //{
                //    gs.util.func.Write("EXCEL导出简易版统计数据得到数据库数据产生错误" + exCn.Message);
                //}
                //finally
                //{
                //    cn.close();
                //}
                ////System.IO.File.Delete("c:\\test.xls");

                //string strGuid = System.Guid.NewGuid().ToString();
                //string strFileName = System.Configuration.ConfigurationSettings.AppSettings["DownTmpPath"].ToString().Trim() + strGuid + ".xls";

                //object obj = System.Reflection.Missing.Value;
                //Excel.Application ex = new Excel.ApplicationClass();
                //Excel.Workbook wk = null;
                //Excel._Worksheet sheet = null;
                //try
                //{


                //    wk = ex.Application.Workbooks.Add(true);
                //    sheet = (Excel._Worksheet)wk.Worksheets[1];
                //    sheet.Name = "changeSheet";
                //    ex.Visible = false;
                //    //ex.Cells[1,4] = "a excel report";
                //    //string strTitle = "State,ticketID,Price,reallyPrice,buildAdd,fuelAdd,issueDate,SaleDate,agencyFare,ticketType,terminalID,IsNational,site1,site2,site3,site4,flightStart,flightTransfer,seatStart,seatTransfer,flightTime_Start,flightTime_Transfer,agencyID,enterDate,addition3,workOut,songPiao,passengerName,remark,addition2,thisName,thisOther";
                //    string strTitle = "PNR,证件号,票价,实收,基建,燃油,起飞时间,销售时间,利润,客票状态,终端编号,是否国际,出发城市一,到达城市一,出发城市二,到达城市二,航班号,经停,舱位,seatTransfer,起飞时间,飞行时长,代理商编号,到达日期,已发出,送票人,passengerName,备注,addition2,审核状态,电子客票号,销售商,预订帐号,预订人,出票帐号";
                //    string[] aryTitle = strTitle.Split(',');
                //    for (int j = 0; j < aryTitle.Length; j++)
                //    {
                //        ex.Cells[1, j + 1] = aryTitle[j];
                //    }

                //    int iCurRow = 0;
                //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //    {

                //        try
                //        {
                //            string numPnrId = ds.Tables[0].Rows[i]["numPnrId"].ToString().Trim();
                //            string strLoginName = ds.Tables[0].Rows[i]["vcLoginName"].ToString().Trim();
                //            string vcNames = ds.Tables[0].Rows[i]["vcNames"].ToString().Trim();
                //            string strPnrNo = ds.Tables[0].Rows[i]["vcPnrNo"].ToString().Trim();
                //            string strEtkNo = ds.Tables[0].Rows[i]["vcEtkNo"].ToString().Trim();
                //            string strOderStat = ds.Tables[0].Rows[i]["numStat"].ToString().Trim();
                //            string strBz = ds.Tables[0].Rows[i]["vcBz"].ToString().Trim();
                //            string strTl = ds.Tables[0].Rows[i]["vcTL"].ToString().Trim();

                //            int iPerCounts = Int32.Parse(ds.Tables[0].Rows[i]["numPersonCount"].ToString());

                //            if (numPnrId != "-1")
                //            {
                //                string[] aryTkCust = new string[1];
                //                if (vcNames == "" || strTl == "黑屏提交！")
                //                {
                //                    aryTkCust = new string[iPerCounts];
                //                    for (int iP = 0; iP < iPerCounts; iP++)
                //                    {
                //                        aryTkCust[iP] = "未知-00000";
                //                    }
                //                }
                //                else
                //                {
                //                    aryTkCust = vcNames.Split(';');
                //                }

                //                for (int iTmp = 0; iTmp < aryTkCust.Length; iTmp++)
                //                {
                //                    string strTkCust = aryTkCust[iTmp];
                //                    int iPos = strTkCust.IndexOf("-") + 1;
                //                    string strTk = strTkCust.Substring(iPos);	//得到电子客票信息

                //                    DataSet dsTks = cn.GetDataSet("select * from eg_pnrtks where numPnrId=" + numPnrId + " order by numPnrTkId");
                //                    DataSet dsAgent = cn.GetDataSet("select dbo.getAgentName(numAgentId) as vcAgentName from eg_user where vcLoginName='" + strLoginName + "'");
                //                    string strCurAgentName = dsAgent.Tables[0].Rows[0]["vcAgentName"].ToString().Trim();
                //                    dsAgent.Clear();

                //                    ex.Cells[2 + iCurRow, 1] = strPnrNo;
                //                    ex.Cells[2 + iCurRow, 2] = strTkCust;
                //                    ex.Cells[2 + iCurRow, 3] = Double.Parse(ds.Tables[0].Rows[i]["numTkPrc"].ToString()) / iPerCounts;
                //                    ex.Cells[2 + iCurRow, 4] = Double.Parse(ds.Tables[0].Rows[i]["numRealPrc"].ToString()) / iPerCounts;
                //                    ex.Cells[2 + iCurRow, 5] = Double.Parse(ds.Tables[0].Rows[i]["numBasePrc"].ToString()) / iPerCounts;
                //                    ex.Cells[2 + iCurRow, 6] = Double.Parse(ds.Tables[0].Rows[i]["numOilPrc"].ToString()) / iPerCounts;
                //                    ex.Cells[2 + iCurRow, 7] = "";
                //                    ex.Cells[2 + iCurRow, 8] = DateTime.Parse(ds.Tables[0].Rows[i]["dtApply"].ToString()).ToShortDateString();

                //                    if (ViewState["roleId"].ToString().Trim() == "0")	//如果是底层黑带,则把它的利润算出来.
                //                    {
                //                        double dbPrc = 0, dbRealPrc = 0, dbBase = 0, dbOil = 0;
                //                        if (ex.Cells[2 + iCurRow, 3] != null && ex.Cells[2 + iCurRow, 3].ToString().Trim() != "")
                //                        {
                //                            dbPrc = Double.Parse(ds.Tables[0].Rows[i]["numTkPrc"].ToString()) / iPerCounts;
                //                        }

                //                        if (ex.Cells[2 + iCurRow, 4] != null && ex.Cells[2 + iCurRow, 4].ToString().Trim() != "")
                //                        {
                //                            dbRealPrc = Double.Parse(ds.Tables[0].Rows[i]["numRealPrc"].ToString()) / iPerCounts;
                //                        }

                //                        if (ex.Cells[2 + iCurRow, 5] != null && ex.Cells[2 + iCurRow, 5].ToString().Trim() != "")
                //                        {
                //                            dbBase = Double.Parse(ds.Tables[0].Rows[i]["numBasePrc"].ToString()) / iPerCounts;
                //                        }

                //                        if (ex.Cells[2 + iCurRow, 6] != null && ex.Cells[2 + iCurRow, 6].ToString().Trim() != "")
                //                        {
                //                            dbOil = Double.Parse(ds.Tables[0].Rows[i]["numOilPrc"].ToString()) / iPerCounts;
                //                        }

                //                        double dbUserGain = dbPrc - dbRealPrc;// - dbBase - dbOil;

                //                        ex.Cells[2 + iCurRow, 9] = dbUserGain.ToString();
                //                    }
                //                    else
                //                    {
                //                        ex.Cells[2 + iCurRow, 9] = Double.Parse(ds.Tables[0].Rows[i]["numGain"].ToString()) / iPerCounts;
                //                    }


                //                    ex.Cells[2 + iCurRow, 10] = "c";
                //                    ex.Cells[2 + iCurRow, 11] = "";//txtTerm.Text.ToString().Trim() ;	//配置编号,暂时空\
                //                    ex.Cells[2 + iCurRow, 12] = "0";	//IsNational

                //                    if (dsTks.Tables[0].Rows.Count > 0)
                //                    {
                //                        string strCity = dsTks.Tables[0].Rows[0]["vcCityPair"].ToString().Trim();
                //                        if (strCity == "黑屏")
                //                        {
                //                            ex.Cells[2 + iCurRow, 13] = "黑屏";	//site1
                //                            ex.Cells[2 + iCurRow, 14] = "黑屏";	//site2
                //                        }
                //                        else
                //                        {
                //                            if (strCity != null && strCity != "")
                //                            {
                //                                string strBegin = strCity.Substring(0, 3);
                //                                string strEnd = strCity.Substring(3);
                //                                ex.Cells[2 + iCurRow, 13] = strBegin;	//site1
                //                                ex.Cells[2 + iCurRow, 14] = strEnd;	//site2
                //                            }
                //                        }
                //                    }

                //                    if (dsTks.Tables[0].Rows.Count > 1)
                //                    {
                //                        string strCity = dsTks.Tables[0].Rows[1]["vcCityPair"].ToString().Trim();
                //                        if (strCity == "黑屏")
                //                        {
                //                            ex.Cells[2 + iCurRow, 15] = "黑屏";	//site1
                //                            ex.Cells[2 + iCurRow, 16] = "黑屏";	//site2
                //                        }
                //                        else
                //                        {
                //                            if (strCity != null && strCity != "")
                //                            {
                //                                string strBegin = strCity.Substring(0, 3);
                //                                string strEnd = strCity.Substring(3);
                //                                ex.Cells[2 + iCurRow, 15] = strBegin;	//site3
                //                                ex.Cells[2 + iCurRow, 16] = strEnd;	//site4
                //                            }
                //                        }
                //                    }

                //                    if (dsTks.Tables[0].Rows.Count > 0)
                //                    {
                //                        ex.Cells[2 + iCurRow, 17] = dsTks.Tables[0].Rows[0]["vcFlightNo"].ToString().Trim();	//flightStart
                //                        ex.Cells[2 + iCurRow, 18] = "";	//flightTransfer
                //                        ex.Cells[2 + iCurRow, 19] = dsTks.Tables[0].Rows[0]["vcBunk"].ToString().Trim();	//seatStart
                //                        ex.Cells[2 + iCurRow, 20] = "";//seatTransfer
                //                        if (dsTks.Tables[0].Rows[0]["vcDate"].ToString().Trim() == "黑屏")
                //                            ex.Cells[2 + iCurRow, 21] = "黑屏";
                //                        else
                //                            ex.Cells[2 + iCurRow, 21] = getDateFormFlDate(dsTks.Tables[0].Rows[0]["vcDate"].ToString());
                //                    }
                //                    else
                //                    {
                //                        ex.Cells[2 + iCurRow, 17] = "";	//flightStart
                //                        ex.Cells[2 + iCurRow, 18] = "";	//flightTransfer
                //                        ex.Cells[2 + iCurRow, 19] = "";	//seatStart
                //                        ex.Cells[2 + iCurRow, 20] = "";		//seatTransfer
                //                        ex.Cells[2 + iCurRow, 21] = "";
                //                    }

                //                    ex.Cells[2 + iCurRow, 22] = "";	//flightTime_Transfer
                //                    ex.Cells[2 + iCurRow, 23] = "";//txtAgentId.Text.ToString().Trim();	//agencyID
                //                    ex.Cells[2 + iCurRow, 24] = DateTime.Parse(ds.Tables[0].Rows[i]["dtApply"].ToString()).ToShortDateString();	//enterDate
                //                    ex.Cells[2 + iCurRow, 25] = "";	//workOut
                //                    ex.Cells[2 + iCurRow, 26] = "";	//songPiao
                //                    ex.Cells[2 + iCurRow, 27] = "";//txtAgentName.Text.ToString().Trim();     //strCurAgentName;	//passengerName
                //                    ex.Cells[2 + iCurRow, 28] = "";	//remark
                //                    ex.Cells[2 + iCurRow, 29] = "";	//addition2
                //                    ex.Cells[2 + iCurRow, 30] = strOderStat;	//thisName
                //                    ex.Cells[2 + iCurRow, 31] = strEtkNo;	//thisOther
                //                    ex.Cells[2 + iCurRow, 32] = ds.Tables[0].Rows[i]["vcSalsName"].ToString();	//vcSalsName
                //                    ex.Cells[2 + iCurRow, 33] = ds.Tables[0].Rows[i]["vcLoginName"].ToString();
                //                    ex.Cells[2 + iCurRow, 34] = ds.Tables[0].Rows[i]["vcUserTitle"].ToString();
                //                    ex.Cells[2 + iCurRow, 35] = ds.Tables[0].Rows[i]["vcManage"].ToString();

                //                    iCurRow++;

                //                }

                //            }
                //            else
                //            {
                //                ex.Cells[2 + iCurRow, 1] = ds.Tables[0].Rows[i]["vcLoginName"].ToString().Trim();
                //                iCurRow++;
                //            }

                //        }
                //        catch (Exception eXport)
                //        {
                //            gs.util.func.Write("EXCEL导出一条简易版数据产生异常" + eXport.Message);
                //        }


                //    }
                //    //for(int i=0;i<11;i++)
                //    //{
                //    //	for(int j=0;j<7;j++)
                //    //	{
                //    //		//以单引号开头，表示该单元格为纯文本
                //    //		ex.Cells[2+i,1+j] = i.ToString() + ":" + j.ToString() + "我日";
                //    //	}
                //    //}

                //    sheet.SaveAs(strFileName, obj, obj, obj, obj, obj, obj, obj, obj, obj);

                //}
                //catch (Exception e2)
                //{
                //    gs.util.func.Write("EXCEL导出异常" + e2.Message);
                //    throw e2;
                //}
                //finally
                //{
                //    ds.Clear();

                //    try
                //    {
                //        if (wk != null)
                //        {
                //            wk.Close(false, strFileName, obj);
                //        }

                //        ex.Quit();



                //        if (sheet != null)
                //        {
                //            System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
                //            sheet = null;
                //        }

                //        if (wk != null)
                //        {
                //            System.Runtime.InteropServices.Marshal.ReleaseComObject(wk);
                //            wk = null;
                //        }

                //        if (ex != null)
                //        {
                //            System.Runtime.InteropServices.Marshal.ReleaseComObject(ex);
                //            ex = null;
                //        }
                //        GC.Collect();
                //        GC.WaitForPendingFinalizers();
                //    }
                //    catch (Exception ex3)
                //    {
                //        gs.util.func.Write("EXCEL关闭异常" + ex3.Message);
                //    }

                //}

                ////杀掉没有关掉的EXCEL进程
                //System.Diagnostics.Process[] processOnComputer = System.Diagnostics.Process.GetProcesses();
                //foreach (System.Diagnostics.Process p in processOnComputer)
                //{
                //    string strProc = p.ProcessName.Trim();
                //    if (strProc.IndexOf("EXCEL") > -1)
                //    {
                //        //gs.util.func.Write("--------------" + strProc);
                //        p.Kill();
                //        break;
                //        //gs.util.func.Write("--over--------" + strProc);
                //    }
                //}

                ////把数据文件导出到IE
                //System.IO.FileInfo file = new System.IO.FileInfo(strFileName);

                //Response.Clear();
                //Response.Charset = "GB2312";
                //Response.ContentEncoding = System.Text.Encoding.UTF8;
                //// 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
                //Response.AddHeader("Content-Disposition", "attachment; filename=thedata.xls");
                //// 添加头信息，指定文件大小，让浏览器能够显示下载进度 
                //Response.AddHeader("Content-Length", file.Length.ToString());

                //// 指定返回的是一个不能被客户端读取的流，必须被下载 
                //Response.ContentType = "application/ms-excel";

                //// 把文件流发送到客户端 
                //Response.WriteFile(file.FullName);
                //// 停止页面的执行 

                ////txtTerm.Text = "";
                ////txtAgentId.Text = "";
                ////txtAgentName.Text = "";

                //Response.End();

                //file.Delete();
            

        }
    }

}
