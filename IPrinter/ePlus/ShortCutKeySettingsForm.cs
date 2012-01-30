using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus
{
    public partial class ShortCutKeySettingsForm : Form
    {
        //public static Data.DataBaseProcess dataBaseProcess = new ePlus.Data.DataBaseProcess(Data.DataBaseProcess.GetConnectionString(AppDomain.CurrentDomain.BaseDirectory + "data\\data.mdb"));
        public static Data.DataBaseProcess dataBaseProcess = new ePlus.Data.DataBaseProcess(Data.DataBaseProcess.GetConnectionString(AppDomain.CurrentDomain.BaseDirectory + "data.mdb"));
        public ShortCutKeySettingsForm()
        {
            InitializeComponent();
        }

        class ShortCutKeyItem
        {
            public int ID
            {
                get
                {
                    return m_ID;
                }
            }
            public string Func
            {
                get
                {
                    return m_Func; 
                }
            }

            public int ShortcutKey
            {
                get
                {
                    return m_ShortcutKey; 
                }
            }

            public bool Shift
            {
                get
                {
                    return m_Shift; 
                }
            }

            public bool Ctrl
            {
                get
                {
                    return m_Ctrl; 
                }
            }

            public bool Alt
            {
                get
                {
                    return m_Alt; 
                }
            }

            public string Memo
            {
                get
                {
                    return m_Memo; 
                }
            }

            public ShortCutKeyItem(int ID, string Func, int ShortcutKey, bool Shift, bool Ctrl, bool Alt)
            {
                this.m_ID           = ID;
                this.m_Func         = Func;
                this.m_ShortcutKey  = ShortcutKey;
                this.m_Shift        = Shift;
                this.m_Ctrl         = Ctrl;
                this.m_Alt          = Alt;

                StringBuilder str = new StringBuilder();
                str.Append(Shift ? "Shift+" : "");
                str.Append(Ctrl ? "Ctrl+" : "");
                str.Append(Alt ? "Alt+" : "");
                str.Append(((Keys)ShortcutKey).ToString());

                this.m_Memo = str.ToString(); 
            }

            public ShortCutKeyItem(string Func, int ShortcutKey, bool Shift, bool Ctrl, bool Alt)
            {
                this.m_ID = 0;
                this.m_Func = Func;
                this.m_ShortcutKey = ShortcutKey;
                this.m_Shift = Shift;
                this.m_Ctrl = Ctrl;
                this.m_Alt = Alt;

                StringBuilder str = new StringBuilder();
                str.Append(Shift ? "Shift+" : "");
                str.Append(Ctrl ? "Ctrl+" : "");
                str.Append(Alt ? "Alt+" : "");
                str.Append(((Keys)ShortcutKey).ToString());

                this.m_Memo = str.ToString();
            }

            private int m_ID;
            private string m_Func;
            private int m_ShortcutKey;
            private bool m_Shift;
            private bool m_Ctrl;
            private bool m_Alt;
            private string m_Memo;
        }

        void RebindKeyList()
        {
            //string [] names = Enum.GetNames(typeof(Keys));
            //cbKeyList.DataSource = names; 
        }

        void RebindShortCutKeyList()
        {
            string sqlStatement = "select * from ShortCutKey where Stat<>0";
            System.Data.DataTable dt = dataBaseProcess.ExcuteQuery(sqlStatement);

            listView.SuspendLayout();
            listView.Items.Clear();
            
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                DataRow dr = dt.Rows[i];
 
                ListViewItem item = new ListViewItem();
                ShortCutKeyItem key = new ShortCutKeyItem(
                                                            int.Parse(dr["ID"].ToString()),
                                                            dr["Func"].ToString(),
                                                            int.Parse(dr["ShortCutKey"].ToString()),
                                                            bool.Parse(dr["Shift"].ToString()),
                                                            bool.Parse(dr["Ctrl"].ToString()),
                                                            bool.Parse(dr["Alt"].ToString())
                                                          );
                item.Text = key.Func;
                item.SubItems.Add(key.Memo);
                item.Tag = (object)key;
                listView.Items.Add(item);
            }

            int width = listView.ClientRectangle.Width;
            listView.Columns[0].Width = (int)(width * 0.53);
            listView.Columns[1].Width = width - listView.Columns[0].Width;

            listView.ResumeLayout();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtInstruction.Text = listView.SelectedItems[0].SubItems[0].Text;
            }
            catch
            {
            }
        }

        private void ShortCutKeySettingsForm_Load(object sender, EventArgs e)
        {
            RebindShortCutKeyList();
            RebindKeyList();
            rbStandardCode.Checked = true;
            rbSpecialCode.Checked = false;
            rbSpecialCode.Visible = false;

            

        }

        private void ShortCutKeySettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); 
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (txtInstruction.Text.Trim() == "") return;
            if (keyvalue == -1) return;
            string sqlString = "select * from ShortCutKey where Alt = {0} and Shift = {1} and Ctrl ={2} and ShortCutKey={3}";
            sqlString = string.Format(sqlString, cbAlt.Checked.ToString(), cbShift.Checked.ToString(), cbCtrl.Checked.ToString(), keyvalue);
            System.Data.DataTable dt = dataBaseProcess.ExcuteQuery(sqlString);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("已经存在");
                return;
            }
            sqlString = "insert into ShortCutKey (Func,ShortCutKey,Shift,Ctrl,Alt,Stat) values ('{0}',{1},{2},{3},{4},{5})";
            sqlString = string.Format(sqlString, 
                txtInstruction.Text, 
                keyvalue.ToString(), 
                cbShift.Checked.ToString(), 
                cbCtrl.Checked.ToString(), 
                cbAlt.Checked.ToString(),
                1);
            if (dataBaseProcess.ExcuteNonQuery(sqlString) > 0)
            {
                RebindShortCutKeyList();
                txtInstruction.Text = "";
                cbKeyList.Text = "";
                cbAlt.Checked = cbCtrl.Checked = cbShift.Checked = false;
            }
            else
            {
                MessageBox.Show("添加失败");
            }
        }

        private void cbKeyList_Enter(object sender, EventArgs e)
        {
            cbKeyList.Text = "请按下一个键";
        }
        int keyvalue = -1;
        private void cbKeyList_KeyDown(object sender, KeyEventArgs e)
        {
            keyvalue = e.KeyValue;
            cbKeyList.Text = e.KeyValue.ToString();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count <= 0) return;
            string sqlStatement = "select * from ShortCutKey where Stat<>0";
            System.Data.DataTable dt = dataBaseProcess.ExcuteQuery(sqlStatement);
            for (int i = listView.SelectedItems.Count - 1; i >= 0; i--)
            {
                int temp = listView.SelectedItems[i].Index;
                string sqlDelete = "delete * from ShortCutKey where Alt = {0} and Shift = {1} and Ctrl ={2} and ShortCutKey={3}";
                sqlDelete = string.Format(sqlDelete,
                    dt.Rows[temp]["Alt"].ToString(),
                    dt.Rows[temp]["Shift"].ToString(),
                    dt.Rows[temp]["Ctrl"].ToString(),
                    (int)dt.Rows[temp]["ShortCutKey"]);
                if (dataBaseProcess.ExcuteNonQuery(sqlDelete) > 0)
                {

                }
                else
                {
                    MessageBox.Show("删除失败");
                }
            }
            RebindShortCutKeyList();
        }

        private void cbKeyList_KeyUp(object sender, KeyEventArgs e)
        {
            cbKeyList.Text = keyvalue.ToString();
        }
    }
}