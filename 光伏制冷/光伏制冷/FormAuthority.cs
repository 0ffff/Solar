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
    public partial class FormAuthority : Form
    {
        public FormAuthority()
        {
            InitializeComponent();
            this.CenterToParent();
        }

        private void FormAuthority_Load(object sender, EventArgs e)
        {
            LoadUser();
        }

        #region 按键

        /// <summary>
        /// 左移按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.lbAdmin.SelectedIndex != -1)
            {
                string normalid = this.lbAdmin.SelectedItem.ToString();
                string normalpsw = "";

                if (DialogResult.OK == MessageBox.Show(string.Format("确认要将普通用户{0}移动至超级用户吗？", normalid), "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    try
                    {
                        string path = "LoginPSW.xml";
                        XmlDocument xd = new XmlDocument();
                        xd.Load(path);
                        XmlNode root = xd.DocumentElement;
                        XmlNodeList rootlist = root.ChildNodes;
                        //得到普通的密码并且在普通用户列表中删除该用户
                        foreach (XmlNode xnroot in rootlist)//遍历根的子节点
                        {
                            if (xnroot.Name == "normal")//normal节点
                            {
                                XmlNodeList xnlNormal = xnroot.ChildNodes;//normal子节点
                                foreach (XmlNode xnUser in xnlNormal)//遍历normal子节点
                                {
                                    if (xnUser.Attributes["id"].Value == normalid)
                                    {
                                        normalpsw = xnUser.Attributes["psw"].Value;
                                        xnroot.RemoveChild(xnUser);
                                    }
                                }
                            }
                        }
                        //如果得到密码 向超级用户中添加该用户
                        if (normalpsw != "")
                        {
                            foreach (XmlNode xnroot in rootlist)//遍历根的子节点
                            {
                                if (xnroot.Name == "super")//Normal节点
                                {
                                    XmlElement newxe = xd.CreateElement("user");//新节点
                                    XmlAttribute xaId = xd.CreateAttribute("id");
                                    xaId.Value = normalid;
                                    XmlAttribute xaPSW = xd.CreateAttribute("psw");
                                    xaPSW.Value = normalpsw;

                                    newxe.Attributes.Append(xaId);
                                    newxe.Attributes.Append(xaPSW);//向新节点添加属性

                                    xnroot.AppendChild(newxe);//向normal节点添加新节点
                                    xd.Save(path);

                                    LoadUser();//刷新列表
                                    MessageBox.Show("更改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                }
                            }
                        }
                        //错误
                        else
                        { MessageBox.Show("修改错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

                    }
                    catch
                    { }
                }
            }
            else
            { MessageBox.Show("请选择对象！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        /// <summary>
        /// 右移按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.lbSuper.SelectedIndex != -1)
            {
                string superid = this.lbSuper.SelectedItem.ToString();
                string superpsw = "";

                if (DialogResult.OK == MessageBox.Show(string.Format("确认要将超级用户{0}移动至普通用户吗？", superid), "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    try
                    {
                        string path = "LoginPSW.xml";
                        XmlDocument xd = new XmlDocument();
                        xd.Load(path);
                        XmlNode root = xd.DocumentElement;
                        XmlNodeList rootlist = root.ChildNodes;
                        //得到超级用户的密码并且在超级用户列表中删除该用户
                        foreach (XmlNode xnroot in rootlist)//遍历根的子节点
                        {
                            if (xnroot.Name == "super")//super节点
                            {
                                XmlNodeList xnlSuper = xnroot.ChildNodes;//super子节点
                                foreach (XmlNode xnUser in xnlSuper)//遍历super子节点
                                {
                                    if (xnUser.Attributes["id"].Value == superid)
                                    {
                                        superpsw = xnUser.Attributes["psw"].Value;
                                        xnroot.RemoveChild(xnUser);
                                    }
                                }
                            }
                        }
                        //如果得到密码 向normal中添加该用户
                        if (superpsw != "")
                        {
                            foreach (XmlNode xnroot in rootlist)//遍历根的子节点
                            {
                                if (xnroot.Name == "normal")//Normal节点
                                {
                                    XmlElement newxe = xd.CreateElement("user");//新节点
                                    XmlAttribute xaId = xd.CreateAttribute("id");
                                    xaId.Value = superid;
                                    XmlAttribute xaPSW = xd.CreateAttribute("psw");
                                    xaPSW.Value = superpsw;

                                    newxe.Attributes.Append(xaId);
                                    newxe.Attributes.Append(xaPSW);//向新节点添加属性

                                    xnroot.AppendChild(newxe);//向normal节点添加新节点
                                    xd.Save(path);

                                    LoadUser();//刷新列表
                                    MessageBox.Show("更改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                }
                            }
                        }
                        //错误
                        else
                        { MessageBox.Show("修改错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

                    }
                    catch
                    { }
                }
            }
            else
            { MessageBox.Show("请选择对象！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }

        /// <summary>
        /// 添加超级用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            FormPassword fp = new FormPassword("超级用户");
            fp.ShowDialog();
            if (GlobalInfo.IsSetPSWFin == true)
            {
                string id = fp.id;
                string psw = fp.psw1;
                if (false == CheckUserName(id))
                {
                    //添加至xml
                    string path = "LoginPSW.xml";
                    XmlDocument xd = new XmlDocument();
                    xd.Load(path);
                    XmlNode root = xd.DocumentElement;

                    XmlNode super = root.SelectSingleNode("super");//选中super节点

                    XmlElement newxn = xd.CreateElement("user");//新建节点
                    XmlAttribute xa = xd.CreateAttribute("id");//用户名属性
                    xa.Value = id;
                    XmlAttribute xa1 = xd.CreateAttribute("psw");//密码属性
                    xa1.Value = psw;
                    newxn.Attributes.Append(xa);//添加属性
                    newxn.Attributes.Append(xa1);

                    super.AppendChild(newxn);//向super中添加新的节点
                    xd.Save(path);

                    LoadUser();//刷新列表
                    MessageBox.Show("更改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GlobalInfo.IsSetPSWFin = false;
                }
                else
                { MessageBox.Show("用户已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }


        /// <summary>
        /// 添加普通用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            FormPassword fp = new FormPassword("普通用户");
            fp.ShowDialog();
            if (GlobalInfo.IsSetPSWFin == true)
            {
                string id = fp.id;
                string psw = fp.psw1;
                if (false == CheckUserName(id))
                {
                    //添加至xml
                    string path = "LoginPSW.xml";
                    XmlDocument xd = new XmlDocument();
                    xd.Load(path);
                    XmlNode root = xd.DocumentElement;

                    XmlNode normal = root.SelectSingleNode("normal");//选中normal节点

                    XmlElement newxn = xd.CreateElement("user");//新建节点
                    XmlAttribute xa = xd.CreateAttribute("id");//用户名属性
                    xa.Value = id;
                    XmlAttribute xa1 = xd.CreateAttribute("psw");//密码属性
                    xa1.Value = psw;
                    newxn.Attributes.Append(xa);//添加属性
                    newxn.Attributes.Append(xa1);

                    normal.AppendChild(newxn);//向super中添加新的节点
                    xd.Save(path);

                    LoadUser();//刷新列表
                    MessageBox.Show("更改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GlobalInfo.IsSetPSWFin = false;
                }
                else
                { MessageBox.Show("用户已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        /// <summary>
        /// 超级用户修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            if (this.lbSuper.SelectedIndex != -1)
            {
                string id = this.lbSuper.SelectedItem.ToString();
                FormPassword fp = new FormPassword("超级用户", id);
                fp.ShowDialog();
                if (GlobalInfo.IsSetPSWFin == true)
                {
                    string psw = fp.psw1;
                    string path = "LoginPSW.xml";
                    XmlDocument xd = new XmlDocument();
                    xd.Load(path);
                    XmlNode root = xd.DocumentElement;

                    XmlNode super = root.SelectSingleNode("super");//选中super节点
                    XmlNodeList superxnl = super.ChildNodes;
                    foreach (XmlNode userxn in superxnl)
                    {
                        if (userxn.Attributes["id"].Value == id)
                        {
                            userxn.Attributes["psw"].Value = psw;
                            break;
                        }
                    }
                    GlobalInfo.IsSetPSWFin = false;
                    xd.Save(path);
                    MessageBox.Show("更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            { MessageBox.Show("请选择对象！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        /// <summary>
        /// 普通用户修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            if (this.lbAdmin.SelectedIndex != -1)
            {
                string id = this.lbAdmin.SelectedItem.ToString();
                FormPassword fp = new FormPassword("普通用户", id);
                fp.ShowDialog();
                if (GlobalInfo.IsSetPSWFin == true)
                {
                    string psw = fp.psw1;
                    string path = "LoginPSW.xml";
                    XmlDocument xd = new XmlDocument();
                    xd.Load(path);
                    XmlNode root = xd.DocumentElement;

                    XmlNode normal = root.SelectSingleNode("normal");//选中super节点
                    XmlNodeList normalxnl = normal.ChildNodes;
                    foreach (XmlNode userxn in normalxnl)
                    {
                        if (userxn.Attributes["id"].Value == id)
                        {
                            userxn.Attributes["psw"].Value = psw;
                            break;
                        }
                    }
                    GlobalInfo.IsSetPSWFin = false;
                    xd.Save(path);
                    MessageBox.Show("更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            { MessageBox.Show("请选择对象！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        /// <summary>
        /// 超级用户删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.lbSuper.SelectedIndex != -1)
            {
                string id = this.lbSuper.SelectedItem.ToString();
                if (DialogResult.OK == MessageBox.Show(string.Format("确认要删除超级用户{0}", id), "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    string path = "LoginPSW.xml";
                    XmlDocument xd = new XmlDocument();
                    xd.Load(path);
                    XmlNode root = xd.DocumentElement;

                    XmlNode super = root.SelectSingleNode("super");//选中super节点
                    XmlNodeList superxnl = super.ChildNodes;
                    foreach (XmlNode userxn in superxnl)
                    {
                        if (userxn.Attributes["id"].Value == id)
                        {
                            super.RemoveChild(userxn);
                            break;
                        }
                    }
                    xd.Save(path);
                    LoadUser();
                    MessageBox.Show("更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            { MessageBox.Show("请选择对象！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        /// <summary>
        /// 普通用户删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (this.lbAdmin.SelectedIndex != -1)
            {
                string id = this.lbAdmin.SelectedItem.ToString();
                if (DialogResult.OK == MessageBox.Show(string.Format("确认要删除普通用户{0}", id), "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    string path = "LoginPSW.xml";
                    XmlDocument xd = new XmlDocument();
                    xd.Load(path);
                    XmlNode root = xd.DocumentElement;

                    XmlNode normal = root.SelectSingleNode("normal");//选中super节点
                    XmlNodeList normalxnl = normal.ChildNodes;
                    foreach (XmlNode userxn in normalxnl)
                    {
                        if (userxn.Attributes["id"].Value == id)
                        {
                            normal.RemoveChild(userxn);
                            break;
                        }
                    }
                    xd.Save(path);
                    LoadUser();
                    MessageBox.Show("更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            { MessageBox.Show("请选择对象！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        #endregion

        /// <summary>
        /// 加载函数
        /// </summary>
        private void LoadUser()
        {
            this.lbAdmin.Items.Clear();
            this.lbSuper.Items.Clear();
            try
            {
                string path = "LoginPSW.xml";
                XmlDocument xd = new XmlDocument();
                xd.Load(path);
                XmlNode root = xd.DocumentElement;
                XmlNodeList rootlist = root.ChildNodes;
                foreach (XmlNode xnroot in rootlist)//遍历根的子节点
                {
                    if (xnroot.Name == "super")//super子节点
                    {
                        XmlNodeList xnlSuper = xnroot.ChildNodes;
                        foreach (XmlNode xnUser in xnlSuper)
                        {
                            this.lbSuper.Items.Add(xnUser.Attributes["id"].Value);//加载超级用户的id
                        }
                    }
                    if (xnroot.Name == "normal")//normal子节点
                    {
                        XmlNodeList xnlNormal = xnroot.ChildNodes;
                        foreach (XmlNode xnNormal in xnlNormal)
                        {
                            this.lbAdmin.Items.Add(xnNormal.Attributes["id"].Value);//加载超级用户的id
                        }
                    }
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true 存在 false 不存在</returns>
        private bool CheckUserName(string id)
        {
            try
            {
                string path = "LoginPSW.xml";
                XmlDocument xd = new XmlDocument();
                xd.Load(path);
                XmlNode root = xd.DocumentElement;
                XmlNodeList rootlist = root.ChildNodes;
                foreach (XmlNode xnroot in rootlist)//遍历根的子节点
                {
                    if (xnroot.Name == "super")//super子节点
                    {
                        XmlNodeList xnlSuper = xnroot.ChildNodes;
                        foreach (XmlNode xnUser in xnlSuper)//遍历super子节点
                        {
                            if (xnUser.Attributes["id"].Value == id)//若有名字存在
                            {
                                return true;
                            }
                        }
                    }
                    if (xnroot.Name == "normal")//normal子节点
                    {
                        XmlNodeList xnlNormal = xnroot.ChildNodes;
                        foreach (XmlNode xnNormal in xnlNormal)//遍历normal子节点
                        {
                            if (xnNormal.Attributes["id"].Value == id)
                            {
                                return true;
                            }

                        }
                    }
                }
                return false;
            }
            catch
            { return true; }
        }

    }
}
