using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Xml;
using System.Data;

namespace PrintTool
{
    class PrintItem
    {
        public PrintItem()
        {
        }

        [Description("获取或设置打印项（打印内容）名称。"), Category("打印项数据")]
        public string Name
        {
            get
            {
                return m_Name; 
            }
            set
            {
                m_Name = value;
                if (Tag != null)
                {
                    Tag["Name"].InnerText = value.ToString();
                }
            }
        }

        [Description("获取或设置打印项（打印内容）的描述信息。"), Category("打印项数据")]
        public string Description
        {
            get
            {
                return m_Description; 
            }
            set
            {
                m_Description = value;
                if (Tag != null)
                {
                    Tag["Description"].InnerText = value;
                }
            }
        }

        [Description("获取或设置打印项（打印内容）字体。"), Category("打印项外观"),AmbientValue(typeof(FontFamily),"宋体")]
        public Font PrintFont
        {
            get
            {
                return m_Font; 
            }
            set
            {
                m_Font = value;
                if (Tag != null)
                {
                    Tag["PrintFont"].InnerText = GetFontName(value);
                }
            }
        }

        [Description("获取或设置打印项（打印内容）位置，单位：毫米(mm)"), Category("打印项布局")]
        public Point Location
        {
            get
            {
                return m_Location; 
            }
            set
            {
                m_Location = value;
                if (Tag != null)
                {
                    Tag["Location"].InnerText = value.ToString();
                }
            }
        }

        public override string ToString()
        {
            return m_Name;
        }

        public string GetFontName(Font font)
        {
            string[] strings = font.ToString().Substring(6).Split(',');

            return strings[0].Split('=')[1] + "," + strings[1].Split('=')[1];
        }

        public Font SetFontName(string font)
        {
            return new Font(font.Split(',')[0], float.Parse(font.Split(',')[1]));
        }


        public System.Xml.XmlNode Tag = null;

        private Point m_Location = new Point(0, 0);
        private Font m_Font = new Font("宋体", 9);
        private string m_Name = "";
        private string m_Description = "";
    }

    public class PrintTitle
    {
        public PrintTitle(string name, string value,string dataFile)
        {
            m_Name = name;
            m_Value = value;
            m_DataFile = dataFile;  
        }

        [Description("指示代码中用来标识该对象的名称。")]
        public String Name
        {
            get
            {
                return m_Name;  
            }
            set
            {
                m_Name = value;
                if (Tag != null)
                {
                    Tag.Attributes[0].Value = value.ToString();
                }
            }
        }

        
        public String Text
        {
            get
            {
                return m_Value; 
            }
            set
            {
                m_Value = value;
                if (Tag != null)
                {
                    Tag.InnerText = value.ToString();
                }
            }
        }

        [Description("获取或设置打印项对应的打印内容文件。"), Category("打印项数据")]
        public string FileName
        {
            get
            {
                return m_DataFile;
            }
            set
            {
                m_DataFile = value;
                if (Tag != null)
                {
                    Tag.Attributes[1].Value = value.ToString();
                }
            }
        }

        public override string ToString()
        {
            return m_Value;           
        }

        public System.Xml.XmlNode Tag = null;
        private string m_Name, m_Value,m_DataFile;
        
    }

    public class ItemPainter
    {
        public static uint PageHeight = (new Properties.Settings()).PageHeight;
        public ItemPainter(string configFileName,string printTitleName)
        {
            try
            {
                xml = new XmlDocument();
                xml.Load(configFileName);
            }
            catch
            {
                if (xml != null)
                    xml = null;
            }

            //e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            //this.e = e;
            this.printTitleName = printTitleName;

            string fileName = GetDataFileName();
            if (fileName == "")
                return;

            list = GetPrintItems();
            if (list == null)
                return;

            dt.ReadXml(fileName);

            rowCount = dt.Rows.Count;
            currentRow = 0;
        }

        public void Paint(System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;

            if (rowCount < 1 || list == null)
                return;


            byte pos = 0;
            foreach (PrintItem item in list)
            {
                e.Graphics.DrawString(dt.Rows[currentRow][pos++].ToString(), item.PrintFont, new SolidBrush(Color.Black), item.Location);
            }

            e.HasMorePages = ++currentRow < rowCount;
        }

        private System.Data.DataTable dt = new System.Data.DataTable();
        private int rowCount = 0,currentRow=0;
        private List<PrintItem> list = null;

        private string GetDataFileName()
        {
            if (printTitleName == "" || xml == null)
                return "";

            XmlNode root = xml.FirstChild;
            
            foreach (XmlNode node in root["Reports"].ChildNodes)
                if (node.Attributes[0].Value.ToUpper() == printTitleName.ToUpper())
                    return node.Attributes[1].Value;

            return "";
        }

        private List<PrintItem> GetPrintItems()
        {
            bool findOut = false;
            List<PrintItem> ItemParameterList = null; 

            XmlNode root = xml.FirstChild["Defines"];

            foreach (XmlNode node in root.ChildNodes)
            {
                findOut = node.Attributes[0].InnerText.ToUpper() == printTitleName.ToUpper();
                if (findOut)
                {
                    root = node;
                    break;
                }
            }

            if (findOut)
            {
                ItemParameterList = new List<PrintItem>(); 
                //currentNode = root;
                foreach (XmlNode node in root.ChildNodes)
                {
                    PrintItem item = new PrintItem();
                    item.Name = node["Name"].InnerText;
                    item.Description = node["Description"].InnerText;
                    try
                    {
                        item.Location = node["Location"].InnerText == "" ? Point.Empty : new Point(int.Parse(node["Location"].InnerText.Substring(1, node["Location"].InnerText.Length - 2).Split(',')[0].Split('=')[1]), int.Parse(node["Location"].InnerText.Substring(1, node["Location"].InnerText.Length - 2).Split(',')[1].Split('=')[1]));
                        item.PrintFont = node["PrintFont"].InnerText == "" ? null : item.SetFontName(node["PrintFont"].InnerText);
                    }
                    catch
                    {
                        item.Location = Point.Empty;
                        item.PrintFont = null;
                    }

                    ItemParameterList.Add(item);
                }
            }
            
            return ItemParameterList;
        }

        //private System.Drawing.Printing.PrintPageEventArgs e;
        private XmlDocument xml = null;
        private string printTitleName;
    }
}
