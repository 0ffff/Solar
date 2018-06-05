using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace 光伏制冷
{
    public partial class FormPassword : Form
    {
        public string id = "";
        public string psw1 = "";
        public string psw2 = "";
        public FormPassword(string type)
        {
            InitializeComponent();
            this.CenterToParent();
            this.txtIdType.Text = type;
            this.txtID.ReadOnly = false;
        }

        //该构造器用于用户修改
        public FormPassword(string type, string id)
        {
            InitializeComponent();
            this.CenterToParent();
            this.txtIdType.Text = type;
            this.txtID.Text = id;
            this.txtID.ReadOnly = true;
        }

        //取消按钮
        private void button2_Click(object sender, EventArgs e)
        {
            if (this != null)
            {
                this.Dispose();
            }
        }

        //确定按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if ((this.txtID.Text != "") && (this.txtPsw.Text != "") && (this.txtPsw2.Text != ""))
            {
                id = this.txtID.Text;
                psw1 = this.txtPsw.Text;
                psw2 = this.txtPsw2.Text;
                if (psw1 == psw2)//两次密码一致
                {
                    if (!(Regex.IsMatch(psw1, @"[^0-9a-zA-Z]")))//密码的正则表达式检查
                    {
                        GlobalInfo.IsSetPSWFin = true;
                        this.Dispose();
                    }
                    else
                    { MessageBox.Show("密码输入格式有误！请输入字母和数字！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                    MessageBox.Show("两次密码不一致！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtPsw.Text = "";
                    this.txtPsw2.Text = "";
                }
            }
            else
            { MessageBox.Show("信息输入不完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void FormPassword_Load(object sender, EventArgs e)
        {

        }
    }
}
