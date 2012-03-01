using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using EagleString;
using XMLConfig;

namespace ePlus
{
    class Autoupdate
    {
        //string strAutoUpdatePath = "AutoUpdatePath.txt";
        //private string strMainProgramfile = "IPrinter.exe";
        private string strUpdateXmlFile = "update.xml";
        private string strWebPath = "/UpdateFiles";

        private XmlDocument docOld = new XmlDocument();
        private ArrayList arrayFiles = new ArrayList();
        //public static Thread thread;
        //private bool isCanceled = false;
        //private string[] WebSites;
        //private int tryCount = 2;
        private int downTimeout = System.Threading.Timeout.Infinite;

        public void Start()
        {
            Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(downloadXML));
            th.IsBackground = true;
            th.Start();
        }

        private void downloadXML()
        {
            try
            {
                string strTemp = DateTime.Now.Ticks.ToString();
                string file = Application.StartupPath + "\\updateServerSide.xml";
                string fileTemp = file + "." + strTemp + ".tmp";
                string fileBak = file + "." + strTemp + ".bak";

                HttpDownloader httpDown = new HttpDownloader(downTimeout);
                httpDown.Download(GetURL() + "/" + strUpdateXmlFile, fileTemp);

                if (File.Exists(file))
                    Directory.Move(file, fileBak);
                Directory.Move(fileTemp, file);
                try { File.Delete(fileBak); }
                catch { }
            }
            catch (Exception ee)
            {
                EagleFileIO.LogWrite(ee.ToString());
                EagleFileIO.LogWrite("下载升级列表失败！请重试或者取消更新。");
                //tryCount = 2;
                return;
            }

            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(downloadFiles));
            th.Start();
        }

        private void downloadFiles()
        {
            GetNewFileList(Path.Combine(Application.StartupPath, "updateServerSide.xml"));

            if (arrayFiles.Count > 0)
            {
                //if (DialogResult.Yes == MessageBox.Show(this, "有" + m_ary.Count + "个文件可以更新！是否立即更新？", "提示", MessageBoxButtons.YesNo))
                {
                    string locationPath = GetURL();
                    string strTemp = DateTime.Now.Ticks.ToString();//临时文件名
                    string tempName = "";
                    string bakName = "";
                    bool bUpdateSucc = true;
                    //pBar.Minimum = 0;
                    //pBar.Maximum = 100;// m_ary.Count + 1;

                    for (int i = 0; i < arrayFiles.Count; i++)
                    {
                        //pBar.Value = 0;
                        string[] strAry = (string[])arrayFiles[i];
                        string strFileName = strAry[0].Trim();
                        //labMes.Text = strFileName;//该语句会导致子线程更新主界面卡死!应使用Invoke操作

                        try
                        {
                            string location = locationPath + "/" + strFileName;
                            tempName = strFileName + strTemp + ".tmp";
                            bakName = strFileName + strTemp + ".bak";

                            HttpDownloader ht = new HttpDownloader(downTimeout);
                            ht.Download(location, tempName, null, null);

                            //btnCancel.Enabled = false;
                            try
                            {
                                File.Move(strFileName, bakName);//改名原文件
                            }
                            catch (Exception ee) { EagleFileIO.LogWrite(ee.ToString() + strFileName); }

                            File.Move(tempName, strFileName);//启用新文件

                            try
                            {
                                File.Delete(bakName);//删除原文件(测试发现：运行中的文件，可以被修改名字！但是不能被删除！)
                            }
                            catch { }

                            //btnCancel.Enabled = true;
                        }
                        catch (Exception e)
                        {
                            EagleFileIO.LogWrite("更新失败：" + strFileName);
                            EagleFileIO.LogWrite(e.ToString());
                            //if (!isCanceled)
                            //    MessageBox.Show(this, "更新失败！请重试或者取消更新。"
                            //        + System.Environment.NewLine + System.Environment.NewLine + "文件名：" + strFileName, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            bUpdateSucc = false;
                            //tryCount = 2;
                        }
                        finally
                        {
                            try
                            {
                                File.Delete(tempName);
                            }
                            catch { }
                        }

                        if (!bUpdateSucc)
                        {
                            //btnUpdate.Enabled = true;
                            //picLoading.Visible = false;
                            break;
                        }
                        //System.Threading.Thread.Sleep(5000);
                    }

                    if (bUpdateSucc)
                    {
                        System.IO.File.Copy(Application.StartupPath + "\\updateServerSide.xml", Path.Combine(Application.StartupPath, strUpdateXmlFile), true);
                        System.IO.File.Delete(Application.StartupPath + "\\updateServerSide.xml");

                        EagleFileIO.LogWrite("自动更新成功！文件数：" + arrayFiles.Count);
                        //tryCount = 2;
                        return;
                    }
                    else
                    {
                        //MessageBox.Show("更新失败");
                    }
                }
                //else
                //{
                //    tryCount = 2;
                //    Application.Exit();
                //}
            }
            else
            {
                //MessageBox.Show("没有代码需要更新");
                //tryCount = 2;
                return; ;
            }
        }

        private void GetNewFileList(string p_strFileName)
        {
            try
            {
                docOld.Load(Path.Combine(Application.StartupPath, strUpdateXmlFile));
            }
            catch (Exception ee)//若本地xml文件不存在将直接更新
            {
                EagleFileIO.LogWrite(strUpdateXmlFile);
                EagleFileIO.LogWrite(ee.ToString());
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
                    //rec.Attributes["eid"].Value;
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
                EagleFileIO.LogWrite(p_strFileName);
                EagleFileIO.LogWrite(ee.ToString());
                //MessageBox.Show("解析 XML 信息失败，退出更新！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //tryCount = 2;
                return;
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

        private string GetURL()
        {
            string url = "";
            XMLSettingsGlobal set = new XMLSettingsGlobal().Read("XMLConfigGlobal.xml") as XMLSettingsGlobal;
            XMLConfigUser user = new XMLConfigUser().Read() as XMLConfigUser;

            if (user.SelectedISP == ISP.ChinaTelecom)
                url = set.Website1;
            else
                url = set.Website2;

            url += strWebPath;

            if (url.StartsWith("http://"))
                return url;
            else
                return "http://" + url;
        }
    }
}
