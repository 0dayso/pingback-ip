using System;
using System.Collections.Generic;
using System.Text;
using EagleString;
using System.Windows.Forms;

namespace EagleForms.Printer
{
    public class PrintIA10
    {
        /// <summary>
        /// 过度保险类
        /// </summary>
        public class MiddleClassCallIA
        {
            EagleProtocal.MyTcpIpClient socket;
            EagleString.CommandPool pool;
            EagleString.LoginInfo li = new LoginInfo ();
            EagleExtension.DataHandler dataHandler = new EagleExtension.DataHandler();
            EagleString.LoginResultBtoc.LOGIN_INFO_BTOC lib2c = new EagleString.LoginResultBtoc.LOGIN_INFO_BTOC ();


            Insurance ia;



            public void SetLoginInfo(string username, string password, LineProvider lp)
            {
                //EagleExtension.Api.login2server(false, "", "", username, password, lp, lib2c, ref li);//得到li
                li.b2b.username = Options.GlobalVar.B2bLoginName;
                li.b2b.password = Options.GlobalVar.B2bLoginPassword;
                li.b2b.webservice = Options.GlobalVar.B2bWebServiceURL;
                li.b2b.webside = Options.GlobalVar.B2bLoginURL;
                if(!string.IsNullOrEmpty(Options.GlobalVar.B2bLoginXml))
                    li.b2b.lr = new LoginResult(Options.GlobalVar.B2bLoginXml);
            }
            public void SetSocket(System.Net.Sockets.Socket sk)
            {
                socket = new EagleProtocal.MyTcpIpClient(sk,li);
                socket.Error += new EagleProtocal.ErrorEvent(socket_Error);
                socket.Incept += new EagleProtocal.InceptEvent(socket_Incept);
            }
            public void SetCommandPool()
            {
                pool = new CommandPool(li);
            }
            public void RecvHz(string s)
            {
                try
                {
                    switch (pool.TYPE)
                    {
                        case ETERM_COMMAND_TYPE.RT:
                            ia.SetControlsByRtResult(new RtResult(s, pool.PNRing));
                            break;
                        case ETERM_COMMAND_TYPE.DETR:
                            ia.SetControlsByDetrResult(new DetrResult(s));
                            break;
                        case ETERM_COMMAND_TYPE.DETRF:
                            ia.SetCardByDetrfResult(new DetrFResult(s));
                            break;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //public void RecvHz(byte[] buffer)
            public void RecvHz(object bufferByte)
            {
                try
                {
                    byte[] buffer = (byte[])bufferByte;
                    dataHandler.InitInputArgs(buffer, li, EagleProtocal.EagleProtocal.MsgNo++);
                    dataHandler.recvHandle();
                    switch (pool.TYPE)
                    {
                        case ETERM_COMMAND_TYPE.RT:
                            ia.SetControlsByRtResult(new RtResult(dataHandler.COMMANDRESULT, pool.PNRing));
                            break;
                        case ETERM_COMMAND_TYPE.DETR:
                            ia.SetControlsByDetrResult(new DetrResult(dataHandler.COMMANDRESULT));
                            break;
                        case ETERM_COMMAND_TYPE.DETRF:
                            ia.SetCardByDetrfResult(new DetrFResult(dataHandler.COMMANDRESULT));
                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (pool == null)
                        return;
                    else if (pool.TYPE == ETERM_COMMAND_TYPE.RT || pool.TYPE == ETERM_COMMAND_TYPE.DETR || pool.TYPE == ETERM_COMMAND_TYPE.DETRF)
                    {
                        ia.LoadingEnd();
                        MessageBox.Show(ex.Message);
                    }
                    //EagleString.EagleFileIO.LogWrite(ex.ToString());
                }
            }
            /// <summary>
            /// 接收事件
            /// </summary>
            void socket_Incept(object sender, EagleProtocal.InceptEventArgs e)
            {
                lock (o)
                {
                    try
                    {
                        byte[] buffer = new byte[e.Astream.Length];
                        e.Astream.Position = 0;
                        e.Astream.Read(buffer, 0, (int)e.Astream.Length);
                        dataHandler.InitInputArgs(buffer, li, EagleProtocal.EagleProtocal.MsgNo++);
                        dataHandler.recvHandle();
                        switch (pool.TYPE)
                        {
                            case ETERM_COMMAND_TYPE.RT:
                                ia.SetControlsByRtResult(new RtResult(dataHandler.COMMANDRESULT, pool.PNRing));
                                break;
                            case ETERM_COMMAND_TYPE.DETR:
                                ia.SetControlsByDetrResult(new DetrResult(dataHandler.COMMANDRESULT));
                                break;
                            case ETERM_COMMAND_TYPE.DETRF:
                                ia.SetCardByDetrfResult(new DetrFResult(dataHandler.COMMANDRESULT));
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

            }
            object o = new object();
            /// <summary>
            /// 错误事件
            /// </summary>
            void socket_Error(object sender, EagleProtocal.ErrorEventArgs e)
            {
                throw e.Error;
            }

            public void ShowIA(string iaCode)
            {
                ia = new Insurance(PRINT_TYPE.INSURANCE, iaCode, socket, pool);
                ia.ShowDialog();
            }
            //added by king
            public Insurance GetIA(string iaCode)
            {
                ia = new Insurance(PRINT_TYPE.INSURANCE, iaCode, socket, pool);
                return ia;
            }
        }
    }
}
