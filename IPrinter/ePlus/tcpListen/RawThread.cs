namespace Leon.RawThread
{
    using System;
    using System.Threading;
    using Leo.LeonForm;
    using System.Windows.Forms;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Net;
    using System.Text;
    using Leo.RawSockets;
    using System.Runtime.InteropServices;
    public class RawSocket : BaseSocket
    {
        public RawSocket() : base() { }
        ListBox list = null;

        public string ipSource = "";
        public int portSource = 0;
        public string ipDest = "";
        public int portDest = 0;



        public ListBox Out
        {
            get
            {
                return list;
            }
            set
            {
                list = value;
            }
        }
        //parse packet
        unsafe public override void Receive(byte[] buf, int len)
        {
            try
            {
                long ip_srcaddr = 10, ip_destaddr = 10, header_len = 0;
                string out_string = "";
                int protocol;
                short src_port = 0, dst_port = 0;
                IPAddress tmp_ip;
                string from_ip, to_ip;
                fixed (byte* fixed_buf = buf)
                {
                    IpHeader* header = (IpHeader*)fixed_buf;
                    header_len = (header->ip_verlen & 0x0F) << 2;
                    protocol = header->ip_protocol;
                    ip_srcaddr = header->ip_srcaddr;
                    ip_destaddr = header->ip_destaddr;
                    try
                    {
                        tmp_ip = new IPAddress(ip_srcaddr);
                        from_ip = tmp_ip.ToString();
                    }
                    catch
                    {
                        from_ip = "";
                    }
                    try
                    {
                        tmp_ip = new IPAddress(ip_destaddr);
                        to_ip = tmp_ip.ToString();
                    }
                    catch
                    {
                        to_ip = "";
                    }
                    tmp_ip = null;
                    switch (protocol)
                    {
                        case 1: out_string = "ICMP:"; break;
                        case 2: out_string = "IGMP:"; break;
                        case 6:
                            out_string = "TCP:";
                            src_port = *(short*)&fixed_buf[header_len];
                            dst_port = *(short*)&fixed_buf[header_len + 2];
                            from_ip += ":" + IPAddress.NetworkToHostOrder(src_port).ToString();
                            to_ip += ":" + IPAddress.NetworkToHostOrder(dst_port).ToString();
                            break;
                        case 17: out_string = "UDP:";
                            src_port = *(short*)&fixed_buf[header_len];
                            dst_port = *(short*)&fixed_buf[header_len + 2];
                            from_ip += ":" + IPAddress.NetworkToHostOrder(src_port).ToString();
                            to_ip += ":" + IPAddress.NetworkToHostOrder(dst_port).ToString();
                            break;
                        default: out_string = "UNKNOWN:"; break;
                    }
                }
                if (protocol ==6)
                {
                    out_string += from_ip + "--->" + to_ip.ToString();
                    if (IPAddress.NetworkToHostOrder(src_port) < 0 || IPAddress.NetworkToHostOrder(dst_port) < 0)
                        out_string += "<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<";
                    //list.Items.Add(out_string);

                    out_string = "total length: " + len.ToString() + " bytes!" + "data length: " + (len - header_len).ToString() + " bytes!";
                    //list.Items.Add(out_string);
                    int ridhead = 71;
                    if (from_ip == ipSource+":"+portSource.ToString() && len>ridhead
                        && IPAddress.NetworkToHostOrder(dst_port) == portDest)
                    {
                        byte[] tmpbyte = new byte[len - ridhead];
                        for (int i = 0; i < tmpbyte.Length-2; i++)
                        {
                            tmpbyte[i] = buf[ridhead + i];
                        }
                        string s = System.Text.Encoding.ASCII.GetString(tmpbyte);
                        Thread.Sleep(2000);
                        if(ePlus.GlobalVar.str_Listener == "")
                            ePlus.GlobalVar.str_Listener_Set = s;
                        //ePlus.GlobalVar.stdRichTB.AppendText("listenStart");
                        //MessageBox.Show(s.Substring(19));
                    }
                }
            }
            catch (Exception ex)
            {
                //list.Items.Add(ex.Source + ex.Message);
            }

        }

        public void Run()
        {
            int cout_receive_bytes;
            while (true)
            {
                IAsyncResult ar = socket.BeginReceive(receive_buf, 0, len_receive_buf, SocketFlags.None, new AsyncCallback(CallReceive), this);
                cout_receive_bytes = socket.EndReceive(ar);
                Receive(receive_buf, cout_receive_bytes);
            }

        }



        virtual public void CallReceive(IAsyncResult ar)
        {
        }

    }


    internal class RawWorker
    {
        public string ipSource = "";
        public int portSource = 0;
        public string ipDest = "";
        public int portDest = 0;


        private MainForm form = null;
        public RawSocket ReceiveAll = new RawSocket();
        public MainForm RawForm
        {
            set
            {
                form = value;
            }
        }
        private string ip = null;
        public string SelectedIP
        {
            set
            {
                ip = string.Copy(value);
            }
        }
        public void Run()
        {
            try
            {
                ReceiveAll.CreateAndBindSocket(ip);
                if (ReceiveAll.SetSockoption() != true)
                {
                    //MessageBox.Show("Invalid call SetSocketOption");
                    return;
                }
                ReceiveAll.ipDest = this.ipDest;
                ReceiveAll.ipSource = this.ipSource;
                ReceiveAll.portDest = this.portDest;
                ReceiveAll.portSource = this.portSource;
                ReceiveAll.Run();
            }
            catch (ThreadAbortException)
            {
                ReceiveAll.Shutdown();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
    }
}