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
            fillDg乘客信息(ps.dt乘客信息);
            fillDg航班信息(ps.dt航班信息);
            this.Text = "政策选择";
        }
        void fillDg航班信息(DataTable dt)
        {
            dg航班信息.DataSource = dt;
            
        }
        void fillDg乘客信息(DataTable dt)
        {
            dg乘客信息.DataSource = dt;
            
        }
        private void dlgPolicySelect_Load(object sender, EventArgs e)
        {
            dg航班信息.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dg乘客信息.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void dlgPolicySelect_SizeChanged(object sender, EventArgs e)
        {
            dg航班信息.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dg乘客信息.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }

        private void btCreateOrder_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}