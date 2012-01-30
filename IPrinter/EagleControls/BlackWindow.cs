
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace EagleControls
{
    public partial class BlackWindow : RichTextBox
    {
        
        public BlackWindowHotKey m_hotkey;
        public BlackWindowHistory m_history = new BlackWindowHistory(64);
        public BlackWindow()
        {
            InitializeComponent();
            
            

            m_hotkey = new BlackWindowHotKey();

            //this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            LoadAttibutes();
        }

        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);
            m_history.ResetSelecting();
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            // 调用基类 OnPaint
            base.OnPaint(pe);
            
        }
        /// <summary>
        /// 取指令
        /// </summary>
        public string EagleCommand
        {
            get
            {
                try
                {
                    int end = this.SelectionStart;
                    int start = this.Text.LastIndexOf(m_charAngel,end-1) + 1;
                    return EagleString.egString.trim(Text.Substring(start, end - start)).Replace("\r\n", "\r").Replace("\n","\r");
                }
                catch
                {
                    return "";
                }
            }
        }
        private char m_charAngel = '>';
        
        /// <summary>
        /// 指令起始标志，如:'>'
        /// </summary>
        public char CHAR_ANGEL
        {
            get
            {
                return m_charAngel;
            }
            set
            {
                m_charAngel = value;
            }
        }
        
        /// <summary>
        /// handle the shortcut keys
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            //InsertShortCutEtermCommand(e);
            //ShowShortCutFunction(e);
            //ShowCommandFunction(e);
        }
        /// <summary>
        /// 在当前位置插入，并把当前位置设为原位置+插入长度
        /// </summary>
        /// <param name="str"></param>
        public void InsertString(string str)
        {
            int pos = SelectionStart;
            Text = Text.Insert(pos, str);
            SelectionStart = pos + str.Length;
        }
        /// <summary>
        /// 若是Eterm指令快键，则
        /// </summary>
        /// <param name="e"></param>
        public void InsertShortCutEtermCommand(KeyEventArgs e)
        {
            string ctrl = (e.Control ? "Ctrl+" : "");
            string alt = (e.Alt ? "Alt+" : "");
            string shift = (e.Shift ? "Shift+" : "");
            string keycode = e.KeyCode.ToString();
            string key = ctrl + alt + shift + keycode;
            key = key.ToUpper();
            if (m_hotkey.HOTKEY1[key] != null)                       //          -----Finished
            {
                string str = m_hotkey.HOTKEY1[key].ToString();
                InsertString(str);
            }
        }
        /// <summary>
        /// 快捷功能键
        /// </summary>
        /// <param name="e"></param>
        public void ShowShortCutFunction(KeyEventArgs e)
        {
            string ctrl = (e.Control ? "Ctrl+" : "");
            string alt = (e.Alt ? "Alt+" : "");
            string shift = (e.Shift ? "Shift+" : "");
            string keycode = e.KeyCode.ToString();
            string key = ctrl + alt + shift + keycode;
            key = key.ToUpper();
            try
            {
                switch (key)
                {
                    case "CTRL+C":
                        Clipboard.SetDataObject(this.SelectedText, true, 5, 10);
                        return;
                    case "CTRL+V":
                        InsertString(Clipboard.GetText(TextDataFormat.Text));
                        return;
                    case "CTRL+Z":
                        this.Undo();
                        return;
                    case "CTRL+Y":
                        this.Redo();
                        return;
                }
            }
            catch
            {
            }
            if (m_hotkey.HOTKEY2[key] != null)
            {
                string str = m_hotkey.HOTKEY2[key].ToString();

                //根据功能列表，执行对应的动作
                m_hotkey.HandleShortCut(str);                                   //  -----与下同 ToFinish
            }
        }
        public void ShowCommandFunction(KeyEventArgs e)
        {
            if(!(e.Control || e.Alt || e.Shift))
                m_history.ResetSelecting();

            if (e.KeyValue == 13)//回车执行对应快键功能
            {
                string key = this.EagleCommand;
                if (m_hotkey.HOTKEY3[key] != null)
                {
                    string str = m_hotkey.HOTKEY3[key].ToString();
                    //根据对应的指令功能列表，执行对应的动作
                    m_hotkey.HandleShortCut(str);                               //  -----与上同 ToFinish
                }
            }

        }


        #region//属性载入保存设置：字体，前景色，背景色。使用时调用SetAttibutes()即可
        private Font m_font;
        private Color m_forecolor;
        private Color m_backcolor;
        private Color m_importantcolor;

        private void set_control_attr()
        {
            this.Font = m_font;
            this.SelectionFont = m_font;
            this.ForeColor = m_forecolor;
            this.BackColor = m_backcolor;
        }
        private void get_controls_attr()
        {
            m_font = this.Font;
            m_forecolor = this.ForeColor;
            m_backcolor = this.BackColor;

        }
        public void LoadAttibutes()//载入并改变当前属性
        {
            try
            {
                string pre = "BlackWindow";
                string key = pre + "Font";
                string value = EagleString.EagleFileIO.ValueOf(key);
                string []a = value.Split(',');
                m_font = new Font(a[0], float.Parse(a[1]), bool.Parse(a[2]) ? FontStyle.Bold : FontStyle.Regular);
                this.Font = m_font;
                value = EagleString.EagleFileIO.ValueOf(pre+"ForeColor");
                a = value.Split(',');
                m_forecolor = Color.FromArgb(int.Parse(a[0]), int.Parse(a[1]), int.Parse(a[2]));
                value = EagleString.EagleFileIO.ValueOf(pre + "BackColor");
                a = value.Split(',');
                m_backcolor = Color.FromArgb(int.Parse(a[0]), int.Parse(a[1]), int.Parse(a[2]));

                value = EagleString.EagleFileIO.ValueOf(pre + "ImportantColor");
                a = value.Split(',');
                m_importantcolor = Color.FromArgb(int.Parse(a[0]), int.Parse(a[1]), int.Parse(a[2]));

                set_control_attr();
            }
            catch(Exception ex)
            {
                this.AppendText("载入字体、前景色、背景色失败！使用默认设置\r\n>");
                get_controls_attr();
                m_importantcolor = Color.Red;
                SaveAttibutes();
            }
        }


        private void SaveAttibutes()//保存到配置文件
        {
            try
            {
                string pre = "BlackWindow";
                Hashtable ht = new Hashtable();
                string[] a = new string[] { m_font.Name, m_font.Size.ToString(), m_font.Bold.ToString() };
                ht.Add(pre + "Font", string.Join(",", a));
                a = new string[] { m_forecolor.R.ToString(), m_forecolor.G.ToString(), m_forecolor.B.ToString() };
                ht.Add(pre + "ForeColor", string.Join(",", a));
                a = new string[] { m_backcolor.R.ToString(), m_backcolor.G.ToString(), m_backcolor.B.ToString() };
                ht.Add(pre + "BackColor", string.Join(",", a));
                a = new string[] { m_importantcolor.R.ToString(), m_importantcolor.G.ToString(), m_importantcolor.B.ToString() };
                ht.Add(pre + "ImportantColor", string.Join(",", a));

                EagleString.EagleFileIO.WiteHashTableToEagleDotTxt(ht);
            }
            catch(Exception ex)
            {
                this.AppendText("保存字体、前景色、背景色失败！");
            }
        }
        public void SetAttibutes()//打开设置对话框
        {
            BlackWindowAttri dlg = new BlackWindowAttri(m_font, m_forecolor, m_backcolor,m_importantcolor);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_font = dlg.FONT;
                m_forecolor = dlg.FORECOLOR;
                m_backcolor = dlg.BACKCOLOR;
            }
            SaveAttibutes();
            LoadAttibutes();
        }
        #endregion

        private RichTextBox rtb = new RichTextBox ();

        public void AppendResult(string txt)
        {
            
            try
            {

                int s = this.Text.Length; ;
                //for (int i = 0; i < txt.Length; i++)
                //{
                //    if (this.Lines[Lines.Length - 1].Length >= 80)
                //    {
                //        AppendText("\r\n");
                //    }
                //    AppendText(txt[i].ToString());
                //}
                this.AppendText(txt);
                
                int start = Text.IndexOf("\x1C", s);
                
                int end = Text.IndexOf("\x1D", start);
                if (start < 0 || end < 0) return;
                while (start >= s)
                {
                    if (start >= 0 && end > start)
                    {
                        this.SelectionStart = start;
                        this.SelectionLength = end - start;
                        this.SelectionColor = m_importantcolor;
                    }
                    else
                    {
                        break;
                    }
                    s = end;
                    start = Text.IndexOf("\x1C", s);
                    end = Text.IndexOf("\x1D", start);
                }
            }
            catch
            {
            }
            //string t1 = Convert.ToString('\x1c');
            //string t2 = Convert.ToString('\x1d');
            //Text = Text.Replace(t1, " ").Replace(t2, " ");
            
            SelectionStart = Text.Length;
            SelectionLength = 0;
        }

        private void BlackWindow_FontChanged(object sender, EventArgs e)
        {
            LoadAttibutes();
        }

        private void BlackWindow_TextChanged(object sender, EventArgs e)
        {
        }

    }
}
