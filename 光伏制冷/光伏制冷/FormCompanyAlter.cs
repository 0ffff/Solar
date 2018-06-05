using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 光伏制冷
{
    public partial class FormCompanyAlter : Form
    {
        DataAccess dataaccess = new DataAccess();

        string CompanyName;
        string CompanyLogin;
        string CompanyPsw;

        public FormCompanyAlter()
        {
            InitializeComponent();
            
        }

        public FormCompanyAlter(string a, string b, string c)
        {
            InitializeComponent();
            this.CenterToParent();
            CompanyName = a;
            CompanyLogin = b;
            CompanyPsw = c;
        }

        private void FormCompanyAlter_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = CompanyName;
            this.textBox2.Text = CompanyLogin;
            this.textBox3.Text = CompanyPsw;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox3.Text!="" && this.textBox2.Text!="" )
            {
                string sql = "update CompanyInfo set CompanyLogin='" + this.textBox2.Text + "',CompanyPsw ='" + this.textBox3.Text + "' where CompanyName='"+this.textBox1.Text+"'";
                dataaccess.ExeSQL(sql,GlobalInfo.DefaultDatabase);
                MessageBox.Show("信息修改完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("信息填写不完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




    }
}
