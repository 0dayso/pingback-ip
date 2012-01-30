using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus
{
    public partial class ModelMangementForm : Form
    {
        public ModelMangementForm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ModelMangementForm_Load(object sender, EventArgs e)
        {
            RefreshDataView();
        }

        private void RefreshDataView()
        {
            DataTable dt = ShortCutKeySettingsForm.dataBaseProcess.ExcuteQuery("select * from ModelList");

            this.dataGridView.DataSource = dt; 
        }

        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            btnDel.Enabled = e.RowIndex > -1;
 
            textBox1.Text = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("��ȷ����ɾ����ģ����", "ȷ��ɾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                //dataGridView.SelectedRows[0].Cells[0].ToString()    
                if (ShortCutKeySettingsForm.dataBaseProcess.ExcuteNonQuery("Delete from ModelList where ID = " + dataGridView.SelectedRows[0].Cells[0].Value.ToString()) > 0)
                {
                    dataGridView.Rows.RemoveAt(dataGridView.SelectedRows[0].Index);   
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "")
            {
                MessageBox.Show("�뽫����������е�������д������");
                return;
            }

            string sqlString = "insert into ModelList(Ʊ������,��Ʊģ��,ģ������) values('{0}','{1}','{2}')";
            sqlString = String.Format(sqlString, textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim());
            ShortCutKeySettingsForm.dataBaseProcess.ExcuteNonQuery(sqlString);
            RefreshDataView();   
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "")
            {
                MessageBox.Show("�뽫����������е�������д������");
                return;
            }

            if (dataGridView.SelectedRows.Count > 0)
            {
                string sqlString = "update ModelList set Ʊ������='{0}',��Ʊģ�� ='{1}',ģ������='{2}' where ID={3}";
                sqlString = String.Format(sqlString, textBox1.Text.Trim(),textBox2.Text.Trim(),textBox3.Text.Trim(),dataGridView.SelectedRows[0].Cells[0].Value.ToString());
                if (ShortCutKeySettingsForm.dataBaseProcess.ExcuteNonQuery(sqlString) > 0)
                {
                    dataGridView.SelectedRows[0].Cells[1].Value = textBox1.Text;   
                    dataGridView.SelectedRows[0].Cells[2].Value = textBox2.Text;
                    dataGridView.SelectedRows[0].Cells[3].Value = textBox3.Text;   

                }
            }
        }
    }
}