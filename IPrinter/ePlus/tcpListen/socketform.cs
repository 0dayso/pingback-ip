namespace Leo.LeonForm
{

    using System;
    using System.Windows.Forms;
    using System.Net.Sockets;
    using System.Net;
    using Leon.RawThread;
    using System.Threading;

    internal class MainForm : Form
    {
        ComboBox IPList = new ComboBox();
        Button BeginButton = new Button(), StopButton = new Button(),ClearButton = new Button ();
        Thread Listener = null;
        RawWorker ClassWorker = new RawWorker();
        ListBox FromTo = new ListBox();

        internal MainForm()
            : base()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            StartPosition = FormStartPosition.CenterScreen;
            IPList.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(IPList);
            //get ip of our computer
            IPHostEntry HosyEntry = Dns.Resolve(Dns.GetHostName());
            if (HosyEntry.AddressList.Length > 0)
            {
                foreach (IPAddress ip in HosyEntry.AddressList)
                {
                    //add ip to combobox
                    IPList.Items.Add(ip.ToString());
                }
            }
        }

        private void InitializeComponent()
        {
            this.FormClosed += new FormClosedEventHandler(MainForm_FormClosed);

            BeginButton.Text = "Start";
            BeginButton.Top = 25;
            BeginButton.Left = 0;
            BeginButton.Click += new EventHandler(BeginButton_Click);
            Controls.Add(BeginButton);

            StopButton.Text = "Stop";
            StopButton.Top = 25;
            StopButton.Left = 80;
            StopButton.Click += new EventHandler(StopButton_Click);
            Controls.Add(StopButton);

            ClearButton.Text = "Clear";
            ClearButton.Top = 25;
            ClearButton.Left = 160;
            ClearButton.Click += new EventHandler(ClearButton_Click);
            Controls.Add(ClearButton);


            FromTo.Top = 60;
            FromTo.Left = 0;
            FromTo.Size = new System.Drawing.Size(800, 600);
            FromTo.HorizontalScrollbar = true;
            FromTo.HorizontalExtent = 600;
            Controls.Add(FromTo);
            ClassWorker.RawForm = this;
            Width += 50;
            this.Size = new System.Drawing.Size(900, 700);
        }

        void ClearButton_Click(object sender, EventArgs e)
        {
            FromTo.Items.Clear();
        }

        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AbortWork();
        }
        void BeginButton_Click(object sender, EventArgs e)
        {
            if (IPList.SelectedIndex < 0)
            {
                MessageBox.Show("You must select ip!");
                return;
            }
            AbortWork();
            ClassWorker.SelectedIP = IPList.SelectedItem.ToString();
            ClassWorker.ReceiveAll.Out = FromTo;
            Listener = new Thread(new ThreadStart(ClassWorker.Run));
            Listener.Start();
        }

        void StopButton_Click(object sender, EventArgs e)
        {
            AbortWork();
        }
        void AbortWork()
        {
            if (Listener != null)
            {
                Listener.Abort();
                Listener.Join();
                Listener = null;
            }
        }
        //override public  void Dispose()
        //{
        // AbortWork();
        // base.Dispose();
        //}

    }

}