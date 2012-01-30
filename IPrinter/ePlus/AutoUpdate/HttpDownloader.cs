using System;
using System.Net;
using System.IO;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Threading;


namespace ePlus
{
    ///   <summary> 
    ///   HttpDownloader   的摘要说明。 
    ///   </summary> 
    public class HttpDownloader
    {
        private string url;
        private string fileName;
        private long fileLength = 0;
        private long downLength = 0;//已经下载文件大小，外面想用就改成公共属性 
        private static bool stopDown = false;
        ProgressBar progressBar;
        Label label;

        /// <summary>
        /// 下载超时时限，单位是毫秒，默认10分钟
        /// </summary>
        private int TimeOutCounter = 300000;
        private string ErrorMsg;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeOut">下载超时时限，单位是秒，默认10分钟；指定 System.Threading.Timeout.Infinite 则不限时间</param>
        public HttpDownloader(int timeOut)
        {
            if (timeOut != System.Threading.Timeout.Infinite)
                this.TimeOutCounter = timeOut * 1000;
            else
                this.TimeOutCounter = timeOut;
        }

        ///   <summary> 
        ///   文件下载，原生的 WebClient 方式
        ///   </summary> 
        ///   <param   name="url">连接</param> 
        ///   <param   name="fileName">本地保存文件名</param> 
        public void Download(string url, string fileName)
        {
            this.url = url;
            this.fileName = fileName;
            Thread th = new Thread(new ThreadStart(Down));
            th.Start();
            //fmMain.thread = th;

            if (!th.Join(TimeOutCounter))
            {
                th.Abort();
                throw new TimeoutException("下载文件超时！");
            }

            if (!string.IsNullOrEmpty(ErrorMsg))
                throw new Exception(ErrorMsg);
        }
        private void Down()
        {
            WebClient DownFile = new WebClient();
            try
            {
                DownFile.DownloadFile(url, fileName);
            }
            catch (ThreadAbortException)
            { }
            catch (Exception ew)
            {
                //if (ew.Message.Contains("超时") || ew.Message.ToLower().Contains("timeout"))
                //    return;//该异常情况下线程会长时间堵塞，导致即使程序已经关闭了，但是该线程仍在后台运行，然后当超时时间到后，系统会弹出错误提示！下面的语句已经解决了该问题（保证安静退出而不干扰用户）
                //else
                {
                    ErrorMsg = ew.Message;
                }
            }
        }

        ///   <summary> 
        ///   文件下载，流读取方式
        ///   </summary> 
        ///   <param   name="url">连接</param> 
        ///   <param   name="fileName">本地保存文件名</param> 
        ///   <param   name="progressBar">进度条</param> 
        ///   <param   name="label">返回已经下载的百分比</param> 
        public void Download(string url, string fileName, ProgressBar progressBar, Label label)
        {
            this.url = url;
            this.fileName = fileName;
            this.progressBar = progressBar;
            this.label = label;

            Thread th = new Thread(new ThreadStart(DownByStream));
            th.Start();
            //fmMain.thread = th;

            if (!th.Join(TimeOutCounter))
            {
                th.Abort();
                throw new TimeoutException("下载文件超时！");
            }

            if (!string.IsNullOrEmpty(ErrorMsg))
                throw new Exception(ErrorMsg);
        }

        private delegate void ActionWithTwoLongParameters(long downLength, long fileLength);
        private void UpdateProgress(long downLength, long fileLength)
        {
            progressBar.Value = (int)(downLength * 100 / fileLength);
            label.Text = string.Format("{0} : {1}kB/{2}kB.", Path.GetFileName(url), downLength / 1024, fileLength / 1024);
        }

        private void DownByStream()
        {
            stopDown = false;
            Stream fs = null;

            try
            {
                //获取下载文件长度 
                fileLength = getDownLength(url);
                downLength = 0;
                if (fileLength > 0)
                {
                    //判断并建立文件 
                    if (createFile(fileName))
                    {
                        WebClient DownFile = new WebClient();
                        using (Stream str = DownFile.OpenRead(url))
                        {
                            byte[] mbyte = new byte[1024];
                            int readL = str.Read(mbyte, 0, 1024);
                            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                            //读取流 
                            while (readL != 0)
                            {
                                if (stopDown)
                                    break;
                                downLength += readL;//已经下载大小 
                                fs.Write(mbyte, 0, readL);//写文件 
                                readL = str.Read(mbyte, 0, 1024);//读流 
                                if(progressBar != null)
                                    progressBar.BeginInvoke(new ActionWithTwoLongParameters(UpdateProgress), downLength, fileLength);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //if (e.Message.Contains("超时") || e.Message.ToLower().Contains("timeout"))
                //    return;
                //else
                {
                    ErrorMsg = e.Message;
                }
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
        }

        ///   <summary> 
        ///   获取下载文件大小 
        ///   </summary> 
        ///   <param   name="url">连接</param> 
        ///   <returns>文件长度</returns> 
        private long getDownLength(string url)
        {
            WebRequest wrq = WebRequest.Create(url);
            WebResponse wrp = (WebResponse)wrq.GetResponse();
            wrp.Close();
            return wrp.ContentLength;
        }

        ///   <summary> 
        ///   建立文件(文件如已经存在，删除重建) 
        ///   </summary> 
        ///   <param   name="fileName">文件全名(包括保存目录)</param> 
        ///   <returns></returns> 
        private bool createFile(string fileName)
        {
            Stream s = null;
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            try
            {
                s = File.Create(fileName);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                    s = null;
                }
            }
            return true;
        }

        public void EndIt()
        {
            stopDown = true;
        }
    }
}