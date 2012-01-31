using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace AutoUpdate
{
    /// <summary>
    /// Form1 的摘要说明。
    /// </summary>
    public class fmMain : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtDownFile;

        private string strMainProgramfile = "IPrinter.exe";
        private string strUpdateXmlFile = "update.xml";
        private string strWebPath = "/UpdateFiles";

        private XmlDocument docOld = new XmlDocument();
        private ArrayList arrayFiles = new ArrayList();
        public static Thread thread;
        Thread threadStart;
        private bool isCanceled = false;
        /// <summary>
        /// 点击取消的次数
        /// </summary>
        private int downTimeout = System.Threading.Timeout.Infinite;

        private RadioButton rbDianx;
        private RadioButton rbWangt;
        private PictureBox picLoading;
        public System.Windows.Forms.ProgressBar pBar;
        public System.Windows.Forms.Label labMes;
        private System.Windows.Forms.Button btnExit;
        private ToolTip toolTipForRoute;
        private Button btnCancel;
        private ToolTip toolTip2;
        private IContainer components = null;

        public fmMain()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmMain));
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtDownFile = new System.Windows.Forms.TextBox();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.labMes = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.rbDianx = new System.Windows.Forms.RadioButton();
            this.rbWangt = new System.Windows.Forms.RadioButton();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.toolTipForRoute = new System.Windows.Forms.ToolTip(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdate.Location = new System.Drawing.Point(186, 64);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(72, 24);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.Click += new System.EventHandler(this.butUpdate_Click);
            // 
            // txtDownFile
            // 
            this.txtDownFile.Location = new System.Drawing.Point(114, 9);
            this.txtDownFile.Name = "txtDownFile";
            this.txtDownFile.ReadOnly = true;
            this.txtDownFile.Size = new System.Drawing.Size(213, 21);
            this.txtDownFile.TabIndex = 1;
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(8, 40);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(299, 16);
            this.pBar.TabIndex = 3;
            this.pBar.Value = 5;
            // 
            // labMes
            // 
            this.labMes.Location = new System.Drawing.Point(8, 64);
            this.labMes.Name = "labMes";
            this.labMes.Size = new System.Drawing.Size(172, 24);
            this.labMes.TabIndex = 4;
            // 
            // btnExit
            // 
            this.btnExit.Enabled = false;
            this.btnExit.Location = new System.Drawing.Point(264, 64);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(64, 24);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // rbDianx
            // 
            this.rbDianx.AutoSize = true;
            this.rbDianx.Location = new System.Drawing.Point(8, 10);
            this.rbDianx.Name = "rbDianx";
            this.rbDianx.Size = new System.Drawing.Size(47, 16);
            this.rbDianx.TabIndex = 7;
            this.rbDianx.Text = "电信";
            this.rbDianx.UseVisualStyleBackColor = true;
            this.rbDianx.CheckedChanged += new System.EventHandler(this.rbDianx_CheckedChanged);
            // 
            // rbWangt
            // 
            this.rbWangt.AutoSize = true;
            this.rbWangt.Location = new System.Drawing.Point(61, 10);
            this.rbWangt.Name = "rbWangt";
            this.rbWangt.Size = new System.Drawing.Size(47, 16);
            this.rbWangt.TabIndex = 8;
            this.rbWangt.Text = "网通";
            this.rbWangt.UseVisualStyleBackColor = true;
            this.rbWangt.CheckedChanged += new System.EventHandler(this.rbWangt_CheckedChanged);
            // 
            // picLoading
            // 
            this.picLoading.Image = ((System.Drawing.Image)(resources.GetObject("picLoading.Image")));
            this.picLoading.Location = new System.Drawing.Point(190, 68);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(16, 16);
            this.picLoading.TabIndex = 9;
            this.picLoading.TabStop = false;
            this.picLoading.Visible = false;
            // 
            // toolTipForRoute
            // 
            this.toolTipForRoute.IsBalloon = true;
            this.toolTipForRoute.ShowAlways = true;
            this.toolTipForRoute.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipForRoute.ToolTipTitle = "卡了吗？";
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(307, 38);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(20, 20);
            this.btnCancel.TabIndex = 11;
            this.toolTip2.SetToolTip(this.btnCancel, "取消当前下载");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // fmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(333, 93);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.rbWangt);
            this.Controls.Add(this.rbDianx);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtDownFile);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.labMes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动更新";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmMain_FormClosing);
            this.Shown += new System.EventHandler(this.fmMain_Shown);
            this.Move += new System.EventHandler(this.fmMain_Move);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void butUpdate_Click(object sender, System.EventArgs e)
        {
            UpdateControls(true);
            isCanceled = false;

            threadStart = new System.Threading.Thread(new System.Threading.ThreadStart(CheckConfigXML));
            threadStart.Start();
        }

        /// <summary>
        /// 更新控件状态
        /// </summary>
        /// <param name="isStart">true-开始 false-结束</param>
        private void UpdateControls(bool isStart)
        {
            btnUpdate.Enabled = !isStart;
            picLoading.Visible = isStart;
            btnExit.Enabled = !isStart;
            btnCancel.Enabled = isStart;
        }

        private void CheckConfigXML()
        {
            try
            {
                string strTemp = DateTime.Now.Ticks.ToString();
                string file = Application.StartupPath + "\\updateServerSide.xml";
                string fileTemp = file + "." + strTemp + ".tmp";
                string fileBak = file + "." + strTemp + ".bak";
                CheckForIllegalCrossThreadCalls = false;

                HttpDownloader httpDown = new HttpDownloader(downTimeout);
                httpDown.Download(GetURL() + "/" + strUpdateXmlFile, fileTemp);

                if(File.Exists(file))
                    Directory.Move(file, fileBak);
                Directory.Move(fileTemp, file); 
            }
            catch (ThreadAbortException)
            {
                //在“取消”按钮里面有后续处理
            }
            catch (Exception ee)
            {
                Common.LogWrite(ee.ToString());
                this.BeginInvoke(new MethodInvoker(delegate
                    {
                        if (!isCanceled)
                        {
                            MessageBox.Show("更新失败，请重试或取消。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        UpdateControls(false);
                        TipRoute();
                    }));
                return;
            }

            threadStart = new System.Threading.Thread(new System.Threading.ThreadStart(CheckNewFiles));
            threadStart.Start();
        }

        private void CheckNewFiles()
        {
            CheckForIllegalCrossThreadCalls = false;
            GetNewFileList(Path.Combine(Application.StartupPath, "updateServerSide.xml"));

            if (arrayFiles.Count > 0)
            {
                if (DialogResult.Yes == MessageBox.Show(this, "有" + arrayFiles.Count + "个文件可以更新！是否立即更新？", "提示", MessageBoxButtons.YesNo))
                {
                    for (int i = 0; i < arrayFiles.Count; i++)
                    {
                        string[] strAry = (string[])arrayFiles[i];
                        string strFileName = strAry[0].Trim();
                        Download(strFileName);
                    }

                    System.IO.File.Copy(Application.StartupPath + "\\updateServerSide.xml", Path.Combine(Application.StartupPath, strUpdateXmlFile), true);
                    System.IO.File.Delete(Application.StartupPath + "\\updateServerSide.xml");

                    //this.BeginInvoke(new MethodInvoker(delegate
                    //{
                    //    MessageBox.Show(this, "更新成功！", "自动更新", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}));
                    Application.Exit();
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void Download(object fileName)
        {
            string strFileName = fileName.ToString();
            string strTemp = DateTime.Now.Ticks.ToString();//临时文件名
            string tempName = "";
            string bakName = "";
            try
            {
                string locationPath = GetURL();
                string location = locationPath + "/" + strFileName;
                tempName = strFileName + strTemp + ".tmp";
                bakName = strFileName + strTemp + ".bak";

                HttpDownloader ht = new HttpDownloader(downTimeout);
                ht.Download(location, tempName, this.pBar, labMes);

                try
                {
                    File.Move(strFileName, bakName);//改名原文件
                }
                catch { }

                File.Move(tempName, strFileName);//启用新文件

                try
                {
                    File.Delete(bakName);//删除原文件(测试发现：运行中的文件，可以被修改名字！但是不能被删除！)
                }
                catch { }
            }
            catch (ThreadAbortException)
            {
                //在“取消”按钮里面有后续处理
            }
            catch (Exception e)
            {
                Common.LogWrite(strFileName + ":" + e.ToString());
                if (!isCanceled)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        MessageBox.Show(this, "更新失败,请重试或者取消更新。"
                            + System.Environment.NewLine + System.Environment.NewLine + "文件名：" + strFileName, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }));
                }

                this.BeginInvoke(new MethodInvoker(delegate
                {
                    UpdateControls(false);
                    TipRoute();
                }));

                return;
            }
            finally
            {
                try
                {
                    File.Delete(tempName);
                }
                catch { }
            }
        }

        /// <summary>
        /// 需要更新的文件名列表
        /// </summary>
        /// <param name="p_strFileName"></param>
        private void GetNewFileList(string p_strFileName)
        {
            try
            {
                docOld.Load(Path.Combine(Application.StartupPath, strUpdateXmlFile));
            }
            catch (Exception ee)
            {
                Common.LogWrite(strUpdateXmlFile);
                Common.LogWrite(ee.ToString());
            }

            try
            {
                arrayFiles.Clear();
                XmlDocument docTmp = new XmlDocument();
                docTmp.Load(p_strFileName);
                XmlNode root = docTmp.DocumentElement;
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    XmlNode rec = root.ChildNodes[i];
                    string strFileName = rec.ChildNodes[0].ChildNodes[0].Value;
                    string strFileDate = rec.ChildNodes[1].ChildNodes[0].Value;
                    if (needUpdate(strFileName, strFileDate))
                    {
                        string[] strAry = new string[2];
                        strAry[0] = strFileName;
                        strAry[1] = strFileDate;
                        Console.WriteLine("filename: {0}", strFileName);
                        arrayFiles.Add(strAry);
                    }
                }
            }
            catch (Exception ee)
            {
                Common.LogWrite(p_strFileName);
                Common.LogWrite(ee.ToString());
                MessageBox.Show("解析 XML 信息失败，退出更新！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }

        /// <summary>
        /// 是否更新
        /// </summary>
        /// <param name="p_strFileName">文件名</param>
        /// <param name="p_strDate">日期</param>
        /// <returns>true要更新 false不更新</returns>
        private bool needUpdate(string p_strFileName, string p_strDate)
        {
            bool bRet = true;
            XmlNode root = docOld.DocumentElement;
            if (root == null)//配置文件为空，则直接更新
                return true;

            for (int i = 0; i < root.ChildNodes.Count; i++)
            {
                XmlNode rec = root.ChildNodes[i];
                string strFileName = rec.ChildNodes[0].ChildNodes[0].Value;
                if (p_strFileName.Trim() == strFileName.Trim())
                {
                    string strFileDate = rec.ChildNodes[1].ChildNodes[0].Value;
                    if (DateTime.Parse(p_strDate) > DateTime.Parse(strFileDate))
                    {
                        bRet = true;
                    }
                    else
                    {
                        bRet = false;
                    }
                    break;
                }
            }
            return bRet;//如果没有找到,既新增文件,则要更新
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            HttpDownloader.Stop();
            isCanceled = true;
            if (threadStart != null)
                threadStart.Abort();

            UpdateControls(false);
            TipRoute();
        }

        /// <summary>
        /// 启动Eagle.exe并退出此升级程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void fmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (thread != null)
            //{
            //    if (thread.IsAlive)
            //    {
            //        thread.Abort();
            //    }
            //    thread.Join();//如果没有上面的Abort(),这Join（）会一直阻塞直至thread执行完毕。
            //}

            //删除所有临时文件
            string[] tmpFiles = Directory.GetFiles(Application.StartupPath, "*.bak", SearchOption.TopDirectoryOnly);
            foreach (string file in tmpFiles)
            {
                try
                { File.Delete(file); }
                catch { }
            }

            tmpFiles = Directory.GetFiles(Application.StartupPath, "*.tmp", SearchOption.TopDirectoryOnly);
            foreach (string file in tmpFiles)
            {
                try
                { File.Delete(file); }
                catch { }
            }

            if (File.Exists(strMainProgramfile))
            {
                try
                {
                    RunProgram(Path.Combine(Application.StartupPath, strMainProgramfile), null);
                }
                catch { }
            }
            else
            {
                if (MessageBox.Show(this, "主程序丢失，是否重新下载？", "自动更新",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                {
                    e.Cancel = true;
                    UpdateControls(true);
                    isCanceled = false;

                    threadStart = new System.Threading.Thread(Download);
                    threadStart.Start("IPrinter.exe");
                }
            }
        }

        //API
        public static void RunProgram(string fileName, string arg)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.Arguments = arg;
            proc.Start();
        }

        private void fmMain_Shown(object sender, EventArgs e)
        {
            XMLConfigUser user = new XMLConfigUser().Read() as XMLConfigUser;
            if (user.SelectedISP == ISP.ChinaTelecom)
                rbDianx.Checked = true;
            else if(user.SelectedISP == ISP.ChinaUnicom)
                rbWangt.Checked = true;

            if (user.SelectedISP != ISP.Default)
                butUpdate_Click(sender, e);
            else
                GetRoute();
        }

        private string GetURL()
        {
            string url = txtDownFile.Text.Trim() + strWebPath;
            if(url.StartsWith("http://"))
                return url;
            else
                return "http://" + url;
        }

        private void rbDianx_CheckedChanged(object sender, EventArgs e)
        {
            GetRoute();
        }

        private void rbWangt_CheckedChanged(object sender, EventArgs e)
        {
            GetRoute();
        }

        private void GetRoute()
        {
            XMLSettingsGlobal global = new XMLSettingsGlobal().Read("XMLConfigGlobal.xml") as XMLSettingsGlobal;
            XMLConfigUser user = new XMLConfigUser().Read() as XMLConfigUser;

            if (rbDianx.Checked)
            {
                this.txtDownFile.Text = global.Website1;
                user.SelectedISP = ISP.ChinaTelecom;
            }
            else if (rbWangt.Checked)
            {
                this.txtDownFile.Text = global.Website2;
                user.SelectedISP = ISP.ChinaUnicom;
            }
            else
                this.txtDownFile.Text = global.Website;

            user.Save();
        }

        void TipRoute()
        {
            //bug？：要执行2次，否则toolTip的箭头位置不对！
            toolTipForRoute.Show("换一条线路，也许可以更快登录。", rbDianx);
            toolTipForRoute.Show("换一条线路，也许可以更快登录。", rbDianx);
        }

        private void fmMain_Move(object sender, EventArgs e)
        {
            toolTipForRoute.RemoveAll();
        }
    }
}

