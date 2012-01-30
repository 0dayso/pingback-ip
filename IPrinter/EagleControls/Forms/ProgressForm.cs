using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleControls.Forms
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
            loadingCircle1.Active = true;
        }

        private void loadingCircle1_Click(object sender, EventArgs e)
        {

        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            loadingCircle1.Active = true;
        }
    }
}