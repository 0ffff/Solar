using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;
using System.Text.RegularExpressions;


namespace 光伏制冷
{
    public partial class FormProjectConfiguration : Form
    {
        DataAccess dataaccess = new DataAccess();
        #region 表格中获取的信息 定义为全局是为了让Form1调用生成树状图

        public string AreaName;//地区名称 省+市
        public string AreaCode;//地区代码
        public string ProjectName;// 项目名称
        public string ProjectCode;//项目代码
        public string TecType;//技术类型
        public string TecCode;//技术编码


        #endregion
        public FormProjectConfiguration(string province, string city, string areaCode)
        {
            InitializeComponent();
            this.CenterToParent();
            txtProvince.Text = province;
            txtCity.Text = city;
            this.AreaName = province + city;
            this.AreaCode = areaCode;
            this.mtxtCityCode.Text = AreaCode;
            LoadCBCompanyName();//加载下拉框
        }

        private void FormProjectConfiguration_Load(object sender, EventArgs e)
        {
            string path = @"TecInfo.xml";
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(path);
                XmlNode root = xd.DocumentElement;
                XmlNodeList xnl = root.ChildNodes;
                foreach (XmlNode xn in xnl)
                {
                    this.cmbTecType.Items.Add(xn.Attributes["name"].Value);
                }
                this.cmbTecType.SelectedIndex = 0;
            }
            catch
            { }
        }

        /// <summary>
        /// 加载公司的下拉框
        /// </summary>
        private void LoadCBCompanyName()
        {
            //try
            //{
            //    string sql = "select CompanyName from CompanyInfo";
            //    DataTable dt = dataaccess.GetDataTable(sql, GlobalInfo.DefaultDatabase);

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        this.cBCompanyName.Items.Add(dt.Rows[i][0].ToString());
            //    }

            //}
            //catch
            //{
               
            //}
        }

        /// <summary>
        /// 点击取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        /// <summary>
        /// 点击确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if ((mtxtCityCode.Text != "") && (this.txtProjectName.Text != "") && (this.mtxtProjectCode.Text != "") && (this.txtlongtitude.Text != "") && (this.txtLatitude.Text != "") && (this.txtUserName.Text != "") && (this.txtPwd1.Text != "") && (this.txtPwd2.Text != ""))
            {
                if (this.txtPwd1.Text == this.txtPwd2.Text)
                {
                    if (DialogResult.OK == MessageBox.Show("确认要添加该项目？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                    {
                        //添加项目的操作 
                        // 0 先检查是否已经存在该项目
                        // 1 向ProjectInfo表格中添加相应数据 
                        // 2 向ProjectDetaiInfo表格中添加信息 
                        // 3 为该项目添加数据库文件（但没有向该数据库中添加表格因为采集器和采集点还没有确定）
                        // 4 向树状图中添加节点 在form1中完成
                        //AreaName = cmbProvince.Text + "省" + cmbCity.Text;//地区名称
                        //AreaCode = mtxtCityCode.Text;//地区代码
                        ProjectName = txtProjectName.Text;// 项目名称
                        ProjectCode = mtxtProjectCode.Text;//项目代码
                        TecType = cmbTecType.Text;//技术类型
                        TecCode = mtxtTecCode.Text;//技术编码

                        //##################
                        //项目用户名和密码
                        string userName = this.txtUserName.Text;
                        string userPwd1 = this.txtPwd1.Text;
                        string userPwd2 = this.txtPwd2.Text;

                        try
                        {
                            string SQL_SelectProjectInfo = @"select count(*) from ProjectInfo where ProjectName='" + ProjectName + "' and AreaName='" + AreaName + "'";//检查项目名称是否存在
                            string SQL_SelectProjectInfo1 = @"select count(*) from ProjectInfo where ProjectCode='" + ProjectCode + "' and AreaName='" + AreaName + "'";//检查项目号是否存在
                            if (dataaccess.IsExistColletorOrPoint(SQL_SelectProjectInfo) == false)//检查项目名称是否存在
                            {
                                if (dataaccess.IsExistColletorOrPoint(SQL_SelectProjectInfo1) == false)//检查项目号是否存在
                                {
                                    //不存在该项目
                                    //1 ProjectInfo表格中添加相应数据 
                                    string SQL_InsertProjectInfo = @"insert into ProjectInfo(AreaName,AreaCode,ProjectName,ProjectCode,TecType,TecCode,UserID,UserPassword)values('" + AreaName + "','" + AreaCode + "','" + ProjectName + "','" + ProjectCode + "','" + TecType + "','" + TecCode + "','" + userName + "','" + userPwd1 + "')";
                                    if (0 == dataaccess.ExeSQL(SQL_InsertProjectInfo, "EnergyTesting1"))
                                    {

                                        string SQL_InsertProjectDetailInfo = @"insert into ProjectDetailInfo(ProjectCode,AreaName,ProjectName,ApplicationCompany,TecType,HeatingArea1,TecNum,XAxis,YAxis,ProjectAddress,ProjectDescription) values('" + mtxtProjectCode.Text.Trim() + "','" + AreaName + "','" + txtProjectName.Text + "','" + cBCompanyName.Text + "','" + this.cmbTecType.Text + "','" + mtxtHeatingArea1.Text.Trim() + "','" + mtxtTecCode.Text + "','" + this.txtlongtitude.Text + "','" + this.txtLatitude.Text + "','" + this.txtAddress.Text + "','" + this.richTextBox1.Text + "')";
                                        if (0 == dataaccess.ExeSQL(SQL_InsertProjectDetailInfo, GlobalInfo.DefaultDatabase))
                                        {
                                            // 3 为该项目添加数据库文件（但没有向该数据库中添加表格因为采集器和采集点还没有确定）
                                            if (0 == dataaccess.CreateDataBase(AreaName + ProjectName))
                                            {
                                                GlobalInfo.IsNewProject = true;
                                                MessageBox.Show("添加项目成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                this.Dispose();
                                            }
                                        //数据库操作错误
                                            else
                                            { MessageBox.Show("数据库已经存在以该项目名称命名的数据库！请先删除该数据库后重新配置！并且删除默认数据库表格ProjectInfo中的该条项目信息！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                                        }
                                        //数据库操作错误
                                        else
                                        { }
                                    }
                                    //数据库操作错误
                                    else
                                    { }
                                }
                                else
                                { MessageBox.Show("该项目编码已经存在！请确认后重新添加！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                            }
                            //若存在项目
                            else
                            { MessageBox.Show("该项目名称已经存在！请确认后重新添加！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        }
                        //数据库操作错误
                        catch
                        { }
                    }
                }
                else
                {
                    MessageBox.Show("确认密码与第一次输入的密码不一致，请重新输入！");
                    this.txtPwd2.Text = "";
                }

            }
            else
            { MessageBox.Show("信息填写不完整！", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); }
        }


        /// <summary>
        /// 技术类型选项改变触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTecType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tecType = this.cmbTecType.Text;
            string path = @"TecInfo.xml";
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(path);
                XmlNode root = xd.DocumentElement;
                XmlNodeList xnl = root.ChildNodes;
                foreach (XmlNode xn in xnl)
                {
                    if (xn.Attributes["name"].Value == tecType)
                    {
                        this.mtxtTecCode.Text = xn.Attributes["id"].Value;
                    }
                }
            }
            catch
            { }
        }

    }
}
