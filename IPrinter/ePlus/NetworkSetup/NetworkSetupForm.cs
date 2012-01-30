using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus
{
    public partial class NetworkSetupForm : Form
    {
        public NetworkSetupForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void NetworkSetupForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); 
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确认要删除此服务器注册信息吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                //删除操作
                string sqlStatement = "delete from serverinfo where [ID] = " + ((ServerInfo)listView.SelectedItems[0].Tag).ID.ToString();
                try
                {
                    if (ShortCutKeySettingsForm.dataBaseProcess.ExcuteNonQuery(sqlStatement) > 0)
                    {
                        MessageBox.Show(string.Format("删除服务器注册信息[{0}]成功！", listView.SelectedItems[0].Text));
                        listView.SelectedItems[0].Remove();
                        SetupButtonState();
                    }
                    else
                        throw new Exception(); 
                }
                catch
                {
                    MessageBox.Show(string.Format("删除服务器注册信息[{0}]失败！", listView.SelectedItems[0].Text));  
                }
            }
        }

        private void listView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            //GetListViewLastColumnWidth();
        }

        /// <summary>
        /// 计算并设置ListView最后一列的列宽。 
        /// </summary>
        private void GetListViewLastColumnWidth()
        {
            listView.Columns[4].Width = listView.ClientRectangle.Width - listView.Columns[0].Width
                                                                       - listView.Columns[1].Width
                                                                       - listView.Columns[2].Width
                                                                       - listView.Columns[3].Width;
        }

        /// <summary>
        /// 从数据库中获取最新的服务器信息，并放入List中。
        /// </summary>
        private void RefreshServerList()
        {
            DataTable dataTable = ShortCutKeySettingsForm.dataBaseProcess.ExcuteQuery("select * from serverinfo");
            
            listView.SuspendLayout();
            listView.Items.Clear();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dr = dataTable.Rows[i];

                ListViewItem item = new ListViewItem();

                ServerInfo serverInfo = new ServerInfo(
                                                        long.Parse(dr["ID"].ToString()),
                                                        dr["Name"].ToString(), 
                                                        dr["Address"].ToString(),
                                                        int.Parse(dr["Port"].ToString()),
                                                        dr["Descript"].ToString(),
                                                        bool.Parse(dr["AuthenticationType"].ToString()),
                                                        dr["UserName"].ToString(),
                                                        dr["Password"].ToString(),
                                                        bool.Parse(dr["Default"].ToString())
                                                      );


                item.Text = serverInfo.Name; 
                item.SubItems.Add(serverInfo.Address);
                item.SubItems.Add(serverInfo.Port.ToString());
                item.SubItems.Add(serverInfo.IsDefault ? "是" : "否" );
                item.SubItems.Add(serverInfo.Description);
                item.Tag = serverInfo;

                listView.Items.Add(item);
            }

            GetListViewLastColumnWidth();

            listView.ResumeLayout();

            if (listView.Items.Count > 0)
                listView.Items[0].Selected = true;
  
            SetupButtonState();
            GetListViewLastColumnWidth();
        }

        /// <summary>
        /// 窗口初始化
        /// </summary>
        private void InitializeForm()
        {
            RefreshServerList(); 
        }

        private void SetupButtonState()
        {
            this.SuspendLayout(); 
            btnAttribute.Enabled = btnDel.Enabled = listView.SelectedItems.Count > 0;
            this.ResumeLayout(); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            NetworkSetup.EditNetworkSetupForm setup = new ePlus.NetworkSetup.EditNetworkSetupForm(new ServerInfo());
            setup.ShowDialog();
            setup.Dispose();

            RefreshServerList(); 
        }

        private void btnAttribute_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                ServerInfo serverInfo = (ServerInfo)listView.SelectedItems[0].Tag;

                NetworkSetup.EditNetworkSetupForm setup = new ePlus.NetworkSetup.EditNetworkSetupForm(serverInfo);
                setup.ShowDialog();
                setup.Dispose();

                RefreshServerList(); 
            }
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            SetupButtonState();
        }

        private void listView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAttribute_Click(btnAttribute, EventArgs.Empty);  
        }
    }

    /// <summary>
    /// 记录和编辑服务器信息
    /// </summary>
    public class ServerInfo
    {
        public ServerInfo(long ID, string name, string address, int port, string descript, bool authenticationType, string userName, string password, bool isDefault)
        {
            m_ID = ID;
            m_Name = name;
            m_Address = address;
            m_Port = port;
            m_Descript = descript;
            m_AuthenticationType = authenticationType;
            m_UserName = userName;
            m_Password = password;
            m_Default = isDefault;

            copy = this;
        }

        public ServerInfo(string name, string address, int port, string descript, bool authenticationType, string userName, string password, bool isDefault)
            : this(0, name, address, port, descript, authenticationType, userName, password, isDefault)
        {
        }

        //这里必需这么写，否则构成循环。
        public ServerInfo()
            : this(0, "", "", 0, "", true, "", "", false)
        {
        }

        #region 属性
        /// <summary>
        /// 获取服务器信息ID(对应数据库ID字段，关键字)。
        /// </summary>
        public long ID
        {
            get
            {
                return m_ID;
            }
        }

        /// <summary>
        /// 服务名
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name; 
            }
            set
            {
                if (!m_BeginEdit)
                    throw new Exception("在更改此属性前必须调用Edit()方法！");

                copy.m_Name = value;
                m_Edited = true;
            }
        }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Address
        {
            get
            {
                return m_Address; 
            }
            set
            {
                if (!m_BeginEdit)
                    throw new Exception("在更改此属性前必须调用Edit()方法！");
                
                
                copy.m_Address = value;
                m_Edited = true;
            }
        }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port
        {
            get
            {
                return m_Port; 
            }
            set
            {
                if (!m_BeginEdit)
                    throw new Exception("在更改此属性前必须调用Edit()方法！");

                copy.m_Port = value;
                m_Edited = true;
            }
        }

        /// <summary>
        /// 服务描述
        /// </summary>
        public string Description
        {
            get
            {
                return m_Descript; 
            }
            set
            {
                if (!m_BeginEdit)
                    throw new Exception("在更改此属性前必须调用Edit()方法！");

                copy.m_Descript = value;
                m_Edited = true;
            }
        }

        /// <summary>
        /// 认证类型 ，True 为用户认证，False为地址认证
        /// </summary>
        public bool AuthenticationType
        {
            get
            {
                return m_AuthenticationType; 
            }
            set
            {
                if (!m_BeginEdit)
                    throw new Exception("在更改此属性前必须调用Edit()方法！");

                copy.m_AuthenticationType = value;
                m_Edited = true;
            }
        }

        /// <summary>
        /// 登录服务器时使用的用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return m_UserName; 
            }
            set
            {
                if (!m_BeginEdit)
                    throw new Exception("在更改此属性前必须调用Edit()方法！");
                
                copy.m_UserName = value;
                m_Edited = true;
            }
        }

        /// <summary>
        /// 登录服务器时使用的密码
        /// </summary>
        public string Password
        {
            get
            {
                return m_Password; 
            }
            set
            {
                if (!m_BeginEdit)
                    throw new Exception("在更改此属性前必须调用Edit()方法！");

                copy.m_Password = value;
                m_Edited = true;
            }
        }

        /// <summary>
        /// 是否为缺省项
        /// </summary>
        public bool IsDefault
        {
            get
            {
                return m_Default; 
            }
            set
            {
                if (!m_BeginEdit)
                    throw new Exception("在更改此属性前必须调用Edit()方法！");

                copy.m_Default = value;
                m_Edited = true;
            }
        }

        #endregion

        public override string ToString()
        {
            return m_Address;
        }

        public void Edit()
        {
            m_BeginEdit = true; 
        }

        public void Save()
        {
            if (!m_BeginEdit || !m_Edited)
                return;

            
            this.m_Name = copy.Name;
            this.m_Address = copy.Address;
            this.m_Port = copy.Port;
            this.m_Descript = copy.Description;
            this.m_AuthenticationType = copy.AuthenticationType;
            this.m_UserName = copy.UserName;
            this.m_Password = copy.Password;
            this.m_Default = copy.IsDefault;
 
            //存入数据库
            string sqlStatement ="";
            if (this.m_ID > 0)
            {
                sqlStatement = "update ServerInfo set Name='{0}',Address='{1}',Port={2},[Default]={3},Descript='{4}',AuthenticationType={5},UserName='{6}',[Password]='{7}' where ID={8}";
                sqlStatement = string.Format(sqlStatement, m_Name, m_Address, m_Port, m_Default.ToString(), m_Descript, m_AuthenticationType.ToString(), m_UserName, m_Password, m_ID);
            }
            else
            {
                sqlStatement = "insert into ServerInfo(Name,Address,Port,[Default],Descript,AuthenticationType,UserName,[Password]) values('{0}','{1}',{2},{3},'{4}',{5},'{6}','{7}')";
                sqlStatement = string.Format(sqlStatement, m_Name, m_Address, m_Port, m_Default.ToString(), m_Descript, m_AuthenticationType.ToString(), m_UserName, m_Password);
            }
            try
            {
                if (ShortCutKeySettingsForm.dataBaseProcess.ExcuteNonQuery(sqlStatement) < 1)
                    throw new Exception("保存数据时出现错误，数据保存失败！");
            }
            catch
            {
                throw; 
            }
        }

        #region 对应数据库内相应字段的内部成员变量
        long m_ID;
        int m_Port;
        string m_Name, m_Address, m_Descript, m_UserName, m_Password;
        bool m_Default, m_AuthenticationType;
        #endregion

        protected bool m_BeginEdit = false;
        protected bool m_Edited = false;
        protected static ServerInfo copy = new ServerInfo();

    }
}