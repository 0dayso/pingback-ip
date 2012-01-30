using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Options
{
    public partial class InterTicketPrint : Form
    {
        public InterTicketPrint()
        {
            InitializeComponent();
        }

        private void ptDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Font ptFontEn = new Font("宋体", 9, System.Drawing.FontStyle.Regular);
            //Font ptFontCn = new Font("tec", EagleAPI.fontsizecn, System.Drawing.FontStyle.Regular);
            Brush ptBrush = Brushes.Black;
            Pen ptPen = new Pen(Brushes.Black,0.01F);
            e.PageSettings.Margins.Left = 0;
            e.PageSettings.Margins.Right = 0;
            e.PageSettings.Margins.Top = 0;
            e.PageSettings.Margins.Bottom = 0;
            PointF o = new PointF();
            o.X = 0F;// float.Parse(numericUpDown1.Value.ToString());
            o.Y = 0F;// float.Parse(numericUpDown2.Value.ToString());
            //划线变量
            float x1 = 55F;
            float x2 = 110F;
            float x3 = 65F;
            float x4 = 92F;
            float y = 6F;
            
            float z1 = 30F;
            float z2 = 50F;
            float z3 = 60F;
            float z4 = 65F;
            float z5 = 72F;
            float z7 = 77F;
            float z8 = 93F;
            float z9 = 108F;
            float z10 = 114F;
            float z11 = 125F;
            float z12 = 130F;
            float z13 = 145F;
            float z14 = 155F;
            float z15 = 105F;
            float z16 = 120F;
            float z17 = 110F;
            float z18 = 130F;
            float z19 = 140F;
            float z20 = 45F;
            float z21 = 55F;
            float z22 = 88F;
            float z23 = 105F;
            float z = (z13 - z4) / 11F;
            //横线
            e.Graphics.DrawLine(ptPen, 0F + o.X, 2F * y + o.Y, x2 + o.X, 2F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 3F * y + o.Y, x2 + o.X, 3F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 4F * y + o.Y, x2 + o.X, 4F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 5F * y + o.Y, x4 + o.X, 5F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 6F * y + o.Y, z14 + o.X, 6F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 7F * y + o.Y, z14 + o.X, 7F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 8F * y + o.Y, z14 + o.X, 8F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 9F * y + o.Y, z14 + o.X, 9F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 10F * y + o.Y, z14 + o.X, 10F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 11F * y + o.Y, z14 + o.X, 11F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 12F * y + o.Y, z14 + o.X, 12F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 13F * y + o.Y, z14 + o.X, 13F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 14F * y + o.Y, z14 + o.X, 14F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 15F * y + o.Y, z14 + o.X, 15F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 16F * y + o.Y, z14 + o.X, 16F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 17F * y + o.Y, z1 + o.X, 17F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z23 + o.X, 17F * y + o.Y, z14 + o.X, 17F * y + o.Y);
            e.Graphics.DrawLine(ptPen, 0F + o.X, 18F * y + o.Y, z1 + o.X, 18F * y + o.Y);
            //竖线
            e.Graphics.DrawLine(ptPen, x1 + o.X, 0F + o.Y, x1 + o.X, 2F * y + o.Y);
            e.Graphics.DrawLine(ptPen, x3 + o.X, 2F * y + o.Y, x3 + o.X, 6F * y + o.Y);
            e.Graphics.DrawLine(ptPen, x4 + o.X, 2F * y + o.Y, x4 + o.X, 5F * y + o.Y);
            e.Graphics.DrawLine(ptPen, x2 + o.X, 0F + o.Y, x2 + o.X, 6F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z1 + o.X, 11F * y + o.Y, z1 + o.X, 20F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z2 + o.X, 6F * y + o.Y, z2 + o.X, 11F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z3 + o.X, 6F * y + o.Y, z3 + o.X, 10F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z5 + o.X, 6F * y + o.Y, z5 + o.X, 10F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z7 + o.X, 6F * y + o.Y, z7 + o.X, 10F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z8 + o.X, 6F * y + o.Y, z8 + o.X, 10F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z9 + o.X, 6F * y + o.Y, z9 + o.X, 10F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z10 + o.X, 6F * y + o.Y, z10 + o.X, 10F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z11 + o.X, 6F * y + o.Y, z11 + o.X, 10F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z12 + o.X, 6F * y + o.Y, z12 + o.X, 10F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z13 + o.X, 6F * y + o.Y, z13 + o.X, 11F * y + o.Y);
            for (int i = 0; i < 11; i++)
            {
                e.Graphics.DrawLine(ptPen, z4 + (float)i * z + o.X, 10F * y + o.Y, z4 + (float)i * z + o.X, 11F * y + o.Y);
            }
            e.Graphics.DrawLine(ptPen, z15 + o.X, 15F * y + o.Y, z15 + o.X, 16F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z16 + o.X, 15F * y + o.Y, z16 + o.X, 16F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z17 + o.X, 17F * y + o.Y, z17 + o.X, 17.5F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z18 + o.X, 17F * y + o.Y, z18 + o.X, 17.5F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z19 + o.X, 17F * y + o.Y, z19 + o.X, 17.5F * y + o.Y);

            e.Graphics.DrawLine(ptPen, z20 + o.X, 16F * y + o.Y, z20 + o.X, 16.5F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z21 + o.X, 16F * y + o.Y, z21 + o.X, 16.5F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z22 + o.X, 16F * y + o.Y, z22 + o.X, 16.5F * y + o.Y);
            e.Graphics.DrawLine(ptPen, z23 + o.X, 16F * y + o.Y, z23 + o.X, 18F * y + o.Y);
            //打字
            string[] PrintStrings = tbContent.Text.Split('\n');
            e.Graphics.DrawString(PrintStrings[0].Substring(0, 56), ptFontEn, ptBrush, new PointF(0F + o.X, 1F * y + o.Y));
            e.Graphics.DrawString(PrintStrings[0].Substring(56), ptFontEn, ptBrush, new PointF(x2 + o.X, 1F * y + o.Y));

            e.Graphics.DrawString(PrintStrings[1].Substring(0, 43), ptFontEn, ptBrush, new PointF(0F + o.X, 2F * y + o.Y));
            e.Graphics.DrawString(PrintStrings[1].Substring(43, 13), ptFontEn, ptBrush, new PointF(x4 + o.X, 2F * y + o.Y));
            e.Graphics.DrawString(PrintStrings[1].Substring(56), ptFontEn, ptBrush, new PointF(x2 + o.X, 2F * y + o.Y));

            e.Graphics.DrawString(PrintStrings[2].Substring(0, 43), ptFontEn, ptBrush, new PointF(0F + o.X, 3F * y + o.Y));
            e.Graphics.DrawString(PrintStrings[2].Substring(43, 13), ptFontEn, ptBrush, new PointF(x4 + o.X, 3F * y + o.Y));
            e.Graphics.DrawString(PrintStrings[2].Substring(56), ptFontEn, ptBrush, new PointF(x2 + o.X, 3F * y + o.Y));

            e.Graphics.DrawString(PrintStrings[3].Substring(0, 56), ptFontEn, ptBrush, new PointF(0F + o.X, 4F * y + o.Y));
            e.Graphics.DrawString("BSP-CHINA", ptFontEn, ptBrush, new PointF(x3 + o.X, 4F * y + o.Y));
            e.Graphics.DrawString(PrintStrings[3].Substring(56), ptFontEn, ptBrush, new PointF(x2 + o.X, 4F * y + o.Y));

            e.Graphics.DrawString(PrintStrings[4].Substring(56), ptFontEn, ptBrush, new PointF(x2 +o.X, 5F * y + o.Y));
            try
            {
                e.Graphics.DrawString(PrintStrings[5].Substring(0, 19), ptFontEn, ptBrush, new PointF(0F + o.X, 6F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[5].Substring(19, 3), ptFontEn, ptBrush, new PointF(z2 - 10F + o.X, 6F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[5].Substring(22, 2), ptFontEn, ptBrush, new PointF(z2 + o.X, 6F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[5].Substring(26, 4), ptFontEn, ptBrush, new PointF(z3 + o.X, 6F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[5].Substring(31, 1), ptFontEn, ptBrush, new PointF(z5 + o.X, 6F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[5].Substring(33, 5), ptFontEn, ptBrush, new PointF(z7 + o.X, 6F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[5].Substring(39, 4), ptFontEn, ptBrush, new PointF(z8 + o.X, 6F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[5].Substring(45, 2), ptFontEn, ptBrush, new PointF(z9 + o.X, 6F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[5].Substring(47, 25), ptFontEn, ptBrush, new PointF(z10 + o.X, 6F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[5].Substring(72), ptFontEn, ptBrush, new PointF(z13 + o.X, 6F * y + o.Y));
            }
            catch { } 
            try
            {
                e.Graphics.DrawString(PrintStrings[6].Substring(0, 19), ptFontEn, ptBrush, new PointF(0F + o.X, 7F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[6].Substring(19, 3), ptFontEn, ptBrush, new PointF(z2 - 10F + o.X, 7F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[6].Substring(22, 2), ptFontEn, ptBrush, new PointF(z2 + o.X, 7F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[6].Substring(26, 4), ptFontEn, ptBrush, new PointF(z3 + o.X, 7F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[6].Substring(31, 1), ptFontEn, ptBrush, new PointF(z5 + o.X, 7F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[6].Substring(33, 5), ptFontEn, ptBrush, new PointF(z7 + o.X, 7F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[6].Substring(39, 4), ptFontEn, ptBrush, new PointF(z8 + o.X, 7F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[6].Substring(45, 2), ptFontEn, ptBrush, new PointF(z9 + o.X, 7F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[6].Substring(47, 25), ptFontEn, ptBrush, new PointF(z10 + o.X, 7F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[6].Substring(72), ptFontEn, ptBrush, new PointF(z13 + o.X, 7F * y + o.Y));
            }
            catch { } 
            try
            {
                e.Graphics.DrawString(PrintStrings[7].Substring(0, 19), ptFontEn, ptBrush, new PointF(0F + o.X, 8F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[7].Substring(19, 3), ptFontEn, ptBrush, new PointF(z2 - 10F + o.X, 8F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[7].Substring(22, 2), ptFontEn, ptBrush, new PointF(z2 + o.X, 8F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[7].Substring(26, 4), ptFontEn, ptBrush, new PointF(z3 + o.X, 8F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[7].Substring(31, 1), ptFontEn, ptBrush, new PointF(z5 + o.X, 8F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[7].Substring(33, 5), ptFontEn, ptBrush, new PointF(z7 + o.X, 8F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[7].Substring(39, 4), ptFontEn, ptBrush, new PointF(z8 + o.X, 8F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[7].Substring(45, 2), ptFontEn, ptBrush, new PointF(z9 + o.X, 8F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[7].Substring(47, 25), ptFontEn, ptBrush, new PointF(z10 + o.X, 8F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[7].Substring(72), ptFontEn, ptBrush, new PointF(z13 + o.X, 8F * y + o.Y));
            }
            catch { } 
            try
            {
                e.Graphics.DrawString(PrintStrings[8].Substring(0, 19), ptFontEn, ptBrush, new PointF(0F + o.X, 9F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[8].Substring(19, 3), ptFontEn, ptBrush, new PointF(z2 - 10F + o.X, 9F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[8].Substring(22, 2), ptFontEn, ptBrush, new PointF(z2 + o.X, 9F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[8].Substring(26, 4), ptFontEn, ptBrush, new PointF(z3 + o.X, 9F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[8].Substring(31, 1), ptFontEn, ptBrush, new PointF(z5 + o.X, 9F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[8].Substring(33, 5), ptFontEn, ptBrush, new PointF(z7 + o.X, 9F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[8].Substring(39, 4), ptFontEn, ptBrush, new PointF(z8 + o.X, 9F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[8].Substring(45, 2), ptFontEn, ptBrush, new PointF(z9 + o.X, 9F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[8].Substring(47, 25), ptFontEn, ptBrush, new PointF(z10 + o.X, 9F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[8].Substring(72), ptFontEn, ptBrush, new PointF(z13 + o.X, 9F * y + o.Y));
            }
            catch { } 
            try
            {
                e.Graphics.DrawString(PrintStrings[9].Substring(0, 19), ptFontEn, ptBrush, new PointF(0F + o.X, 10F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[9].Substring(19, 3), ptFontEn, ptBrush, new PointF(z2 - 10F + o.X, 10F * y + o.Y));
            }
            catch { }
            try
            {
                e.Graphics.DrawString(PrintStrings[10].Substring(0, PrintStrings[10].IndexOf(".00") + 3), ptFontEn, ptBrush, new PointF(0F + o.X, 11F * y + o.Y));
                e.Graphics.DrawString(PrintStrings[10].Substring(PrintStrings[10].IndexOf(".00") + 3), ptFontEn, ptBrush, new PointF(z1 + o.X, 11F * y + o.Y));
            }
            catch { }
            try
            {
                e.Graphics.DrawString(PrintStrings[11].Substring(PrintStrings[11].IndexOf(".00") + 3), ptFontEn, ptBrush, new PointF(z1 + o.X, 11F * y + o.Y));
            }
            catch { }
            
            e.Graphics.DrawString(PrintStrings[12].Substring(0), ptFontEn, ptBrush, new PointF(0F + o.X, 13F * y + o.Y));
            e.Graphics.DrawString(PrintStrings[13].Substring(0), ptFontEn, ptBrush, new PointF(0F + o.X, 14F * y + o.Y));
            e.Graphics.DrawString(PrintStrings[14].Substring(0, PrintStrings[14].IndexOf(".00") + 3), ptFontEn, ptBrush, new PointF(0F + o.X, 15F * y + o.Y));
            e.Graphics.DrawString(PrintStrings[14].Substring(PrintStrings[14].IndexOf(".00") + 3, 47), ptFontEn, ptBrush, new PointF(z1 + o.X, 15F * y + o.Y));
            e.Graphics.DrawString(PrintStrings[14].Substring(61), ptFontEn, ptBrush, new PointF(z16 + o.X, 15F * y + o.Y));
            e.Graphics.DrawString(PrintStrings[15].Substring(0), ptFontEn, ptBrush, new PointF(0F + o.X, 16F * y + o.Y));
            e.Graphics.DrawString(PrintStrings[16].Substring(21, 3), ptFontEn, ptBrush, new PointF(z20 + o.X, 17F * y + o.Y));
            e.Graphics.DrawString(PrintStrings[16].Substring(44, 21), ptFontEn, ptBrush, new PointF(z23 + o.X, 17F * y + o.Y));
            e.Graphics.DrawString(PrintStrings[16].Substring(65), ptFontEn, ptBrush, new PointF(z18 + o.X, 17F * y + o.Y));

        }
        
        private void tbPrint_Click(object sender, EventArgs e)
        {
            ptDoc.Print();
        }
    }
}
/*
    CHINA SOUTHERN AIRLINES                             08306760
   NONEND                                  WUHHKG       WUH CITS
                                           R1WWB/1E     WUH129  
   YANG/FENGLIAN                                        DEV-06  
                                                            2986
     WUHAN         WUHCZ  3075 H 07MAR 0900  OKY                        20K 
     HONG KONG     HKG    VOID  
     VOID                 VOID  
     VOID                 VOID  
     VOID   
   CNY 2130.00M07MAR07WUH CZ HKG272.16NUC272.16END ROE7.826260  
    
   CN    90.00  
   YQ    31.00  
   YR   106.00CASH(CNY)                                      6262A  
   CNY 2357.00  
                     784                    CNY1300.00           000 227.00 
*/