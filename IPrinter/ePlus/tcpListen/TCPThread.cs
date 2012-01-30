namespace Leon.TcpThread
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
        unsafe public override void Receive(byte[] buf, int len)
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
                tmp_ip = new IPAddress(ip_srcaddr);
                from_ip = tmp_ip.ToString();
                tmp_ip = new IPAddress(ip_destaddr);
                to_ip = tmp_ip.ToString();
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
                    default: out_string = "UNKNOWN"; break;
                }
            }

            out_string += from_ip + "--->" + to_ip.ToString();
            list.Items.Add(out_string);

            out_string = "total length: " + len.ToString() + " bytes";
            list.Items.Add(out_string);

            out_string = "data length: " + (len - header_len).ToString() + " bytes";
            list.Items.Add(out_string);

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


    internal class TCPWorker
    {
        private MainForm form = null;
        public RawSocket ReceiveAll = new RawSocket();
        public MainForm TCPForm
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
                    MessageBox.Show("Invalid call SetSocketOption");
                    return;
                }
                ReceiveAll.Run();
            }
            catch (ThreadAbortException)
            {
                ReceiveAll.Shutdown();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}