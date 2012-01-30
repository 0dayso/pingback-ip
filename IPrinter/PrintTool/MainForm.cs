using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PrintTool
{
    public partial class MainForm : Form
    {
        public static string ConfigFileName = Application.StartupPath + "\\PrintRules.xml";
        public MainForm()
        {
            InitializeComponent();

            if (System.IO.File.Exists(ConfigFileName))
            {
                xml.Load(ConfigFileName); 
            }

            GetAllAirStations();
        }

        ItemPainter painter = null;

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {        
            //e.Graphics.DrawRectangle(new Pen(Color.Black),20,20,100,100);  
            if (painter == null)
                painter = new ItemPainter(ConfigFileName, currentNode.Attributes[0].Value);

            painter.Paint(e); 
        }

        private void btnPrintSetup_Click(object sender, EventArgs e)
        {
            if (pageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.DefaultPageSettings = pageSetupDialog.PageSettings;
                printDocument.PrinterSettings = pageSetupDialog.PrinterSettings;   
            }
        }

        private PrintPreviewDialog m_PrintPreviewDialog = null;
        private XmlDocument xml = new XmlDocument();
        private XmlNode currentNode = null;
        //private bool showEditor = false;

        private PrintPreviewDialog printPreviewDialog
        {
            get
            {
                return m_PrintPreviewDialog;
            }
            set
            {
                if (m_PrintPreviewDialog != null)
                {
                    m_PrintPreviewDialog.Dispose();
                    m_PrintPreviewDialog = null;
                }

                value.Document = this.printDocument;
                m_PrintPreviewDialog = value;

                m_PrintPreviewDialog.Left = this.Right;
                m_PrintPreviewDialog.Top = this.Top;
                m_PrintPreviewDialog.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                m_PrintPreviewDialog.Width = Screen.PrimaryScreen.WorkingArea.Width - m_PrintPreviewDialog.Right;
                m_PrintPreviewDialog.Height = Screen.PrimaryScreen.WorkingArea.Height - m_PrintPreviewDialog.Bottom;

                m_PrintPreviewDialog.ShowDialog();

            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            printPreviewDialog = new PrintPreviewDialog();
        }

        private void GetAllAirStations()
        {
            XmlNode root = xml.FirstChild;
            comboBox.Items.Clear();
            foreach (XmlNode node in root["Reports"].ChildNodes)
            {
                PrintTitle title = new PrintTitle(node.Attributes[0].InnerText, node.InnerText, node.Attributes[1].InnerText);
                title.Tag = node;
                comboBox.Items.Add(title); 
            }

            if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;  
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox.SelectedItem != null)
            {
                bool findOut = false;
                PrintTitle title = (PrintTitle)comboBox.SelectedItem;
                propertyGrid.SelectedObject = title;
 
                XmlNode root = xml.FirstChild["Defines"];

                foreach (XmlNode node in root.ChildNodes)
                {
                    findOut = node.Attributes[0].InnerText.ToUpper() == title.Name.ToUpper();
                    if (findOut)
                    {
                        root = node;
                        break;
                    }
                }

                if(findOut)
                {
                    listBox.Items.Clear();
                    currentNode = root;
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

                        item.Tag = node;

                        listBox.Items.Add(item);   
                    }
                }
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                propertyGrid.SelectedObject = listBox.SelectedItem;
            }
        }

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            if (listBox.GetItemRectangle(listBox.SelectedIndex).Contains(listBox.PointToClient(MousePosition)))
            {
                //showEditor = true;
                textBox.Parent = listBox;
                textBox.Text = listBox.SelectedItem.ToString();
                textBox.Location = listBox.GetItemRectangle(listBox.SelectedIndex).Location;
                textBox.Size = listBox.GetItemRectangle(listBox.SelectedIndex).Size; 
                textBox.Show();
                textBox.Focus();
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            PrintItem printItem = (PrintItem)listBox.SelectedItem;
            printItem.Name = textBox.Text;
            
            textBox.Hide();
            listBox.Refresh();  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                if (currentNode != null)
                    foreach (XmlNode node in currentNode.ChildNodes)
                        if (node["Name"].InnerText == ((PrintItem)listBox.SelectedItem).Name)
                        {
                            currentNode.RemoveChild(node);
                            listBox.Items.Remove(listBox.SelectedItem);   
                        }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (xml != null)
                xml.Save(ConfigFileName);


            DataTable dt = new DataTable();
            dt.TableName = "EA";
            dt.Columns.Add("Name");
            dt.Columns.Add("From");

            dt.Rows.Add("张三", "武汉飞往北京");
            dt.Rows.Add("李四", "武汉飞往武汉");
            dt.WriteXml("NA.xml", XmlWriteMode.WriteSchema); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmlNode node = xml.CreateNode("element", "item", "");
            XmlNode subItem = currentNode.AppendChild(node);

            subItem.AppendChild(xml.CreateNode("element", "Name", ""));
            subItem.AppendChild(xml.CreateNode("element", "Description", ""));
            subItem.AppendChild(xml.CreateNode("element", "Location", ""));
            subItem.AppendChild(xml.CreateNode("element", "PrintFont", ""));

            PrintItem item = new PrintItem();

            item.Tag = subItem;
            item.Name = "新建名";
            item.Description = "新建名描述";
            item.Location = Point.Empty;
            item.PrintFont = new Font("宋体",9);

            listBox.Items.Add(item);
            listBox.Refresh(); 
        }

        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.State == DrawItemState.Selected)
                e.DrawFocusRectangle();
            else
                e.DrawBackground();

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;

            e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor),e.Bounds);      
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                listBox.Focus();  
        }
    }
}