using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;


namespace ePlus.Data
{
    /// <summary>
    /// test
    /// </summary>
    public class avDataSet
    {
        public string FlightDate = "";
        public int Price_yBunk = 0;
        public int DistanceFromTo = 0;
        public string FromTo = "";
        public List<DataTable> TableFlight = new List<DataTable>();
        public List<DataTable> TableTicket = new List<DataTable>();
        public List<DataTable> TableLowestPerFlight = new List<DataTable>();
        public void init(ListView ls,string date)
        {
            try
            {
                FlightDate = date;
                if (ls.Columns.Count == 0 || ls.Items.Count == 0) return;
                if (!int.TryParse(ls.Items[0].SubItems[5].Text, out DistanceFromTo)) throw new Exception("未得到距离"); ;
                if (!int.TryParse(ls.Items[0].SubItems[9].Text, out Price_yBunk)) throw new Exception("未得到Y舱票价");
                FromTo = ls.Items[1].SubItems[3].Text;
                DataTable dt = new DataTable();
                for (int i = 0; i < ls.Columns.Count; i++)
                {
                    dt.Columns.Add(ls.Columns[i].Text);
                }
                for (int i = 1; i < ls.Items.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < ls.Columns.Count; j++)
                    {
                        try
                        {
                            dr[j] = ls.Items[i].SubItems[j].Text;
                        }
                        catch
                        {
                            dr[j] = "";
                        }
                    }
                    dt.Rows.Add(dr);
                }
                TableFlight.Add(dt);

                DataTable dtLowest = new DataTable();

                dt = new DataTable();
                dt.Columns.Add("FlightNo_Bunk");
                dt.Columns.Add("CityPair");
                dt.Columns.Add("TimeBeg");
                dt.Columns.Add("TimeEnd");
                dt.Columns.Add("PlaneType");
                dt.Columns.Add("Rebate");
                dt.Columns.Add("Price");
                dt.Columns.Add("Policy");
                for (int i = 0; i < dt.Columns.Count; i++) dtLowest.Columns.Add(dt.Columns[i].ColumnName.ToString());
               
                
                for (int i = 1; i < ls.Items.Count; i = i + 2)
                {
                    try
                    {
                        for (int j = 7; j <= 23; j++)
                        {
                            string txt = ls.Items[i].SubItems[j].Text;
                            if (txt.Trim().Length <2) continue;
                            if (txt.Trim()[1] > 'A') continue;
                            DataRow dr = dt.NewRow();

                            dr["FlightNo_Bunk"] = ls.Items[i].SubItems[1].Text + "-" + ls.Items[i].SubItems[j].Text[0].ToString();
                            dr["CityPair"] = ls.Items[i].SubItems[3].Text;
                            dr["TimeBeg"] = ls.Items[i].SubItems[4].Text;
                            dr["TimeEnd"] = ls.Items[i].SubItems[5].Text;
                            dr["PlaneType"] = ls.Items[i].SubItems[6].Text;

                            if (ls.Columns[j].Text == "F")
                            {
                                dr["Price"] = getRebatePrice(150);
                                dr["Rebate"] = "150";
                            }
                            else if (ls.Columns[j].Text == "C")
                            {
                                dr["Price"] = getRebatePrice(130);
                                dr["Rebate"] = "130";
                            }
                            else if (ls.Columns[j].Text == "Y")
                            {
                                dr["Price"] = getRebatePrice(100);
                                dr["Rebate"] = "100";
                            }
                            else
                            {
                                dr["Price"] = getRebatePrice(int.Parse(ls.Columns[j].Text));
                                dr["Rebate"] = ls.Columns[j].Text;
                            }
                            dr["Policy"] = (ls.Items[i + 1].SubItems[j].Text == "" ? "0%" : ls.Items[i + 1].SubItems[j].Text);
                            dt.Rows.Add(dr);
                        }
                        if (dt.Rows.Count > 0) dtLowest.ImportRow(dt.Rows[dt.Rows.Count - 1]);
                    }
                    catch
                    {
                    }
                }
                TableTicket.Add(dt);
                TableLowestPerFlight.Add(dtLowest);
            }
            catch(Exception ex)
            {
               // MessageBox.Show("Initial NKG Mode Failed: " +ex.Message);
            }
        }
        int getRebatePrice(int reb)//9.5折.则reb=95
        {
            return (reb * Price_yBunk + 500) / 1000 * 10;
        }
        public void ExportToExcel(System.Data.DataTable dt)
        {
            if (dt == null) return;
            Excel.Application xlApp = new Excel.Application();
            if (xlApp == null)
            {
                MessageBox .Show ( "无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }
            Excel.Workbooks workbooks = xlApp.Workbooks;
            Excel.Workbook workbook = workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
            Excel.Range range = null;
            long totalCount = dt.Rows.Count;
            long rowRead = 0;
            float percent = 0;

            //写入标题
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                range = (Excel.Range)worksheet.Cells[1, i + 1];
                //range.Interior.ColorIndex = 15;//背景颜色
                range.Font.Bold = true;//粗体
                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;//居中
                //加边框
                range.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, null);
                //range.ColumnWidth = 4.63;//设置列宽
                //range.EntireColumn.AutoFit();//自动调整列宽
                //r1.EntireRow.AutoFit();//自动调整行高

            }
            //写入内容
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i];
                    range = (Excel.Range)worksheet.Cells[r + 2, i + 1];
                    range.Font.Size = 9;//字体大小
                    //加边框
                    range.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, null);
                    range.EntireColumn.AutoFit();//自动调整列宽
                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
                System.Windows.Forms.Application.DoEvents();
            }

            range.Borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight = Excel.XlBorderWeight.xlThin;
            if (dt.Columns.Count > 1)
            {
                range.Borders[Excel.XlBordersIndex.xlInsideVertical].Weight = Excel.XlBorderWeight.xlThin;
            }

            try
            {
                workbook.Saved = true;
                workbook.SaveCopyAs("C:\\datatable" +"" + ".xls");
            }
            catch (Exception ex)
            {
                MessageBox .Show ("导出文件时出错,文件可能正被打开！\n" + ex.Message);
            }

            xlApp.Quit();
            GC.Collect();//强行销毁
            //这个是从服务器中下载文件,(请参考我另外一个文章)
            //参考网址http://www.cnblogs.com/ghostljj/archive/2007/01/24/629293.html
            //BIClass.BusinessLogic.Util.ResponseFile(Page.Request, Page.Response, "ReportToExcel.xls"
            //    , System.Web.HttpRuntime.AppDomainAppPath + "XMLFiles\\EduceWordFiles\\" + this.Context.User.Identity.Name + ".xls", 1024000);
        }


    }
}
