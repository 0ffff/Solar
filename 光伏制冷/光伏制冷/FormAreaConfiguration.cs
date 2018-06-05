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
    public partial class FormAreaConfiguration : Form
    {

        DataAccess dataaccess = new DataAccess();
        public string Province;
        public string City;
        public string Code;

        #region 是否创建新地区标志位
        private bool isNewProvince = false;
        public bool IsNewProvince
        {
            set { isNewProvince = value; }
            get { return isNewProvince; }
        }

        private bool isNewCity = false;
        public bool IsNewCity
        {
            set { isNewCity = value; }
            get { return isNewCity; }
        }
        #endregion

        public FormAreaConfiguration()
        {
            InitializeComponent();
            this.CenterToParent();
        }

        private void FormAreaConfiguration_Load(object sender, EventArgs e)
        {
            //加载省
            string path = @"ProvinceInfo.xml";
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(path);
                XmlNode root = xd.DocumentElement;//根节点
                XmlNodeList xnl = root.ChildNodes;//根节点的子节点list
                foreach (XmlNode xn in xnl)
                {
                    if (xn.Name.ToString() == "province")
                    {
                        cmbProvince.Items.Add(xn.Attributes["name"].Value);
                    }
                }
            }
            catch
            {

            }
        }
        //加载市
        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ProvinceName = this.cmbProvince.SelectedItem.ToString();
            string path = @"ProvinceInfo.xml";
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(path);
                XmlNode root = xd.DocumentElement;//根节点
                XmlNodeList xnl = root.ChildNodes;//根节点的子节点list
                foreach (XmlNode xn in xnl)
                {
                    if ((xn.Name.ToString() == "province") && (xn.Attributes["name"].Value == ProvinceName))
                    {
                        //遍历城市节点
                        XmlNodeList xnlCity = xn.ChildNodes;
                        this.cmbCity.Items.Clear();
                        foreach (XmlNode xnCity in xnlCity)
                        {

                            this.cmbCity.Items.Add(xnCity.Attributes["name"].Value);
                            //this.txtCityCode.Text = xnCity.Attributes["id"].Value;
                        }
                    }
                }
                this.cmbCity.SelectedIndex = 0;
            }
            catch
            { }
        }
        //显示编码
        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ProvinceName = this.cmbProvince.SelectedItem.ToString();
            string CityName = this.cmbCity.SelectedItem.ToString();
            string path = @"ProvinceInfo.xml";
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(path);
                XmlNode root = xd.DocumentElement;//根节点
                XmlNodeList xnl = root.ChildNodes;//根节点的子节点list
                foreach (XmlNode xn in xnl)
                {
                    if ((xn.Name.ToString() == "province") && (xn.Attributes["name"].Value == ProvinceName))
                    {
                        //遍历城市节点
                        XmlNodeList xnlCity = xn.ChildNodes;
                        foreach (XmlNode xnCity in xnlCity)
                        {
                            if (xnCity.Attributes["name"].Value == CityName)
                            {
                                this.txtCityCode.Text = xnCity.Attributes["id"].Value;
                            }
                        }
                    }
                }
            }
            catch
            { }
        }
        //点击确定
        private void button1_Click(object sender, EventArgs e)
        {
            //-1填写信息检查
            //0 检查是否存在该省（AreaInfo）
            //1 若存在该省则判断是否存在该市  若不存在该省 a向AreaInfo添加省和市 b向树状图添加省市节点
            //2 若存在市 显示该地区已经存在 若不存在该市 a向AreaInfo添加省和市  b向树状图添加市节点
            if ((cmbProvince.Text != "") && (cmbCity.Text != ""))
            {
                if (MessageBox.Show("确认添加该地区？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Province = this.cmbProvince.SelectedItem.ToString();
                    City = this.cmbCity.SelectedItem.ToString();
                    Code = this.txtCityCode.Text;
                    //0 检查是否存在该省（AreaInfo）
                    string SQL_IsExistProvince = @"select Province from AreaInfo where Province='" + Province + "'";
                    if (dataaccess.IsExistColletorOrPoint(SQL_IsExistProvince) == false)
                    {
                        //不存在该省 a向AreaInfo添加省和市 b向树状图添加省市节点
                        string SQL_AreaInfo1 = @"insert into AreaInfo(Province,City,Code)values('" + Province + "','" + City + "','" + Code + "')";
                        if (0 == dataaccess.ExeSQL(SQL_AreaInfo1, GlobalInfo.DefaultDatabase))
                        {
                            isNewProvince = true;
                            MessageBox.Show("地区添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Dispose();
                        }
                        //数据库操作错误
                        else
                        { 
                            MessageBox.Show("地区添加失败！数据库操作失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                        }
                    }
                    //存在该省 判断是否存在该市
                    else
                    {
                        string SQL_IsExistCity = @"select City from AreaInfo where City='" + City + "'";
                        if (dataaccess.IsExistColletorOrPoint(SQL_IsExistCity) == false)
                        {
                            //不存在该市  a向AreaInfo添加省和市  b向树状图添加市节点
                            string SQL_AreaInfo2 = @"insert into AreaInfo(Province,City,Code)values('" + Province + "','" + City + "','" + Code + "')";
                            if (0 == dataaccess.ExeSQL(SQL_AreaInfo2, GlobalInfo.DefaultDatabase))
                            {
                                isNewCity = true;
                                MessageBox.Show("地区添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Dispose();
                            }
                            else
                            { MessageBox.Show("地区添加失败！数据库操作失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                        }
                        //存在该市
                        else
                        { MessageBox.Show("地区已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }

                }
            }
            else
            { MessageBox.Show("信息填写不完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        //点击取消
        private void button2_Click(object sender, EventArgs e)
        {
            if (this != null)
            {
                this.Dispose();
            }
        }

    }
}
