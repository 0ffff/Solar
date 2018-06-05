using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace 光伏制冷
{
    public partial class FormEntry : Form
    {
        public FormEntry()
        {
            InitializeComponent();
            this.CenterToParent();
        }
        //点击登录
        private void button1_Click(object sender, EventArgs e)
        {
            if ((this.txtUser.Text != "") && (this.txtPSW.Text != ""))
            {
                string user = this.txtUser.Text;//用户名
                string psw = this.txtPSW.Text;//密码
                bool Isfind = false;//是否找到对象
                string path = "LoginPSW.xml";
                XmlDocument xd = new XmlDocument();
                xd.Load(path);
                XmlNode root = xd.DocumentElement;
                XmlNodeList xnl = root.ChildNodes;
                foreach (XmlNode xnroot in xnl)
                {
                    if (xnroot.Name == "super")//super节点
                    {
                        XmlNodeList xnlsuper = xnroot.ChildNodes;//super子节点
                        foreach (XmlNode xnsuper in xnlsuper)
                        {
                            if ((xnsuper.Attributes["id"].Value == user) && (xnsuper.Attributes["psw"].Value == psw))//找到对象
                            {
                                GlobalInfo.UserType = "super";
                                //管理员模式                                
                                Isfind = true;
                                break;
                            }
                        }
                        if (Isfind)
                        { break; }
                        else
                        { continue; }
                    }
                    if (xnroot.Name == "normal")//normal节点
                    {
                        XmlNodeList xnlnormal = xnroot.ChildNodes;//normal子节点
                        foreach (XmlNode xnnormal in xnlnormal)
                        {
                            if ((xnnormal.Attributes["id"].Value == user) && (xnnormal.Attributes["psw"].Value == psw))//找到对象
                            {
                                GlobalInfo.UserType = "normal";
                                Isfind = true;
                                break;
                            }
                        }
                        if (Isfind)
                        { break; }
                        else
                        { continue; }
                    }
                }
                //查找用户完毕
                if (Isfind)
                {
                   this.Hide();
                   FormMain f1 = new FormMain(this);
                   f1.ShowDialog();

                }
                else
                {
                    MessageBox.Show("用户名密码不正确！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUser.Focus();
                }
            }
            else
            {
                MessageBox.Show("信息输入不完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUser.Focus();
            }
        }
        //点击退出
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormEntry_Load(object sender, EventArgs e)
        {
            //将label控件背景置为透明
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;


        }

        private void FormEntry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
            {
                this.button1.Focus();
                this.button1.PerformClick();
            }
        }

       
       
    }
}
