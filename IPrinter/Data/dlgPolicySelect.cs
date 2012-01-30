using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus.Data
{
    public partial class dlgPolicySelect : Form
    {
        public dlgPolicySelect()
        {
            InitializeComponent();
        }
        public dlgPolicySelect(ePlus.Data.PolicySelect ps)
        {
            InitializeComponent();
            fillDg�˿���Ϣ(ps.dt�˿���Ϣ);
            fillDg������Ϣ(ps.dt������Ϣ);
            this.Text = "����ѡ��";
        }
        void fillDg������Ϣ(DataTable dt)
        {
            dg������Ϣ.DataSource = dt;
            
        }
        void fillDg�˿���Ϣ(DataTable dt)
        {
            dg�˿���Ϣ.DataSource = dt;
            
        }
        private void dlgPolicySelect_Load(object sender, EventArgs e)
        {
            dg������Ϣ.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dg�˿���Ϣ.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void dlgPolicySelect_SizeChanged(object sender, EventArgs e)
        {
            dg������Ϣ.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dg�˿���Ϣ.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }

        private void btCreateOrder_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}