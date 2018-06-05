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
    public partial class FormCollectPointConfiguration : Form
    {
        DataAccess dataaccess = new DataAccess();
        string AreaName;//地区名
        string ProjectName;//项目名称
        string CollectorName;//采集器名称
        public string CollectPointName;//采集点名称
        public string CollectPointCode;//采集点代码
        public string CollectData;//采集指标名称
        public string CollectDataCode;//采集指标代码
        public string CollectPointMaxValue;//最大值
        public string CollectPointMinValue;//最小值
        /// <summary>
        /// 重写构造函数
        /// </summary>
        /// <param name="areaName"></param>
        /// <param name="projectName"></param>
        /// <param name="collectorName"></param>
        /// <param name="Code">辨别是添加采集器 0   删除采集器 1</param>
        public FormCollectPointConfiguration(string areaName, string projectName, string collectorName, int Code)
        {
            InitializeComponent();
            this.AreaName = areaName;
            this.ProjectName = projectName;
            this.CollectorName = collectorName;
            if (Code == 0)
            {
                this.tabControl1.SelectedIndex = 0;
                this.button1.Enabled = true;
                this.button3.Enabled = false;
            }
            else
            {
                this.tabControl1.SelectedIndex = 1;
                this.button1.Enabled = false;
                this.button3.Enabled = true;
            }
        }

        private void FormCollectPointConfiguration_Load(object sender, EventArgs e)
        {
            //添加页面加载
            this.txtAreaName.Text = AreaName;
            this.txtProjectName.Text = ProjectName;
            this.txtCollectorName.Text = CollectorName;
            //删除页面加载
            this.txtDelAreaName.Text = AreaName;
            this.txtDelProjectName.Text = ProjectName;
            this.txtDelCollectorName.Text = CollectorName;
            //采集点删除时加载所有采集点
            try
            {
                string SQL_SelectCollectPoint = @"select CollectPointName from CollectPointInfo where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "' and CollectorName='" + CollectorName + "' order by cast(CollectPointCode as int) asc";
                //DataSet ds_CollectPoint = dataaccess.GetDataSet(SQL_SelectCollectPoint, GlobalInfo.DefaultDatabase);
                SqlDataReader dr = dataaccess.GetDataReader(SQL_SelectCollectPoint, GlobalInfo.DefaultDatabase);
                cmbCollectPointName.Items.Clear();
                while (dr.Read())
                {
                    cmbCollectPointName.Items.Add(dr["CollectPointName"]);
                }
                dataaccess.conn.Close();
                //this.cmbCollectPointName.DataSource = ds_CollectPoint.Tables[0];
                //this.cmbCollectPointName.DisplayMember = "CollectPointName";
                //this.cmbCollectPointName.SelectedIndex = -1;
                //cmbCollectPointName.Items.Add(
            }
            catch
            { }

            //采集点添加加载采集指标名称
            try
            {
                string path = "CollectDataInfo.xml";
                XmlDocument xd = new XmlDocument();
                xd.Load(path);
                XmlNode root = xd.DocumentElement;
                XmlNodeList xnl = root.ChildNodes;
                foreach (XmlNode xn in xnl)
                {
                    this.cmbCollectData.Items.Add(xn.Attributes["name"].Value);
                }
            }
            catch
            { }
        }

        //添加取消
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //添加确定
        private void button1_Click(object sender, EventArgs e)
        {

            if ((this.txtCollectPointName.Text != "") && (mtxtCollectPointCode.Text != "") && (cmbCollectData.Text != "") && (mtxtCollectPointCode.Text != "") && (this.txtMax.Text.Trim() != "") && (txtMin.Text.Trim() != ""))
            {
                if (!(Regex.IsMatch(this.txtMax.Text, @"[^+\-0-9]") || Regex.IsMatch(this.txtMin.Text, @"[^+\-0-9]")))
                {
                    if (DialogResult.OK == MessageBox.Show("确认要添加该采集点？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                    {
                        //0 先检查是否已经存在该采集点
                        //1 向CollectPointInfo数据库表格中插入信息
                        //2 向表格中添加该采集点
                        CollectPointName = txtCollectPointName.Text;
                        CollectPointCode = mtxtCollectPointCode.Text.Trim();
                        CollectData = cmbCollectData.Text;
                        CollectDataCode = txtCollectDataCode.Text;
                        string MaxValue = this.txtMax.Text.Trim();//最大阈值
                        string MinValue = txtMin.Text.Trim();//最小阈值
                        try
                        {
                            //0 先检查是否已经存在该采集点(该地区名称下该项目名称下该采集器名称下的 1 采集点名称 2 采集点编码)
                            string SQL_IsExist = @"select count(*) from CollectPointInfo where CollectorName='" + CollectorName + "' and ProjectName='" + ProjectName + "'and CollectPointName='" + CollectPointName + "' and AreaName='" + AreaName + "'";//检查采集点名称
                            string SQL_IsExist1 = @"select count(*) from CollectPointInfo where CollectorName='" + CollectorName + "' and ProjectName='" + ProjectName + "'and CollectPointCode='" + CollectPointCode + "' and AreaName='" + AreaName + "'";//检查采集点编码
                            //string SQL_IsExist2 = @"select count(*) from CollectorInfo where CollectorName='" + CollectorName + "'";
                            if ((dataaccess.IsExistColletorOrPoint(SQL_IsExist) == false))
                            {
                                if ((dataaccess.IsExistColletorOrPoint(SQL_IsExist1) == false))
                                {

                                    //1 向默认数据库CollectorInfo表格中插入信息
                                    string SQL_InsertCollectorInfo = @"insert into CollectPointInfo(AreaName,ProjectName,CollectorName,CollectPointName,CollectPointCode,CollectDataName,CollectDataCode,MaxValue,MinValue)values('" + AreaName + "','" + ProjectName + "','" + CollectorName + "','" + CollectPointName + "','" + CollectPointCode + "','" + CollectData + "','" + CollectDataCode + "','" + MaxValue + "','" + MinValue + "')";
                                    if (0 == dataaccess.ExeSQL(SQL_InsertCollectorInfo, "EnergyTesting1"))
                                    {

                                        GlobalInfo.IsNewCollectPoint = true;
                                        MessageBox.Show("添加采集点成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Dispose();
                                    }
                                    //向默认数据库CollectorInfo表格中插入信息操作错误
                                    else
                                    { }
                                }
                                else
                                { MessageBox.Show("该采集点编码已经存在！请确认后重新添加！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                            }
                            else
                            { MessageBox.Show("该采集点名称已经存在！请确认后重新添加！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        }
                        //数据库操作错误
                        catch
                        { }
                    }

                    else
                    { MessageBox.Show("信息填写不完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                    MessageBox.Show("请输入正确的阈值！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        //编辑取消
        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        //编辑确定
        private void button3_Click(object sender, EventArgs e)
        {
            //0 更新采集点相关信息CollectPointInfo
            //1 更新主界面表格中的采集点信息
            //2 
        }

        //采集点相关信息加载
        private void cmbCollectPointName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CollectPointNameDel = this.cmbCollectPointName.Text;
            string SQL_SelectCollectPointInfo = @"select CollectPointCode,CollectDataName,CollectDataCode,MaxValue,MinValue from CollectPointInfo where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "' and CollectorName='" + CollectorName + "' and CollectPointName='" + CollectPointNameDel + "'";
            try
            {
                DataSet ds = dataaccess.GetDataSet(SQL_SelectCollectPointInfo, GlobalInfo.DefaultDatabase);
                DataTable dt = ds.Tables[0];
                txtDelCollectPointCode.Text = dt.Rows[0][0].ToString();
                txtDelCollectDataName.Text = dt.Rows[0][1].ToString();
                txtDelCollectDataCode.Text = dt.Rows[0][2].ToString();
                txtDelMax.Text = dt.Rows[0][3].ToString();
                txtDelMin.Text = dt.Rows[0][4].ToString();
                CollectPointName = cmbCollectPointName.Text;
                CollectPointCode = txtDelCollectPointCode.Text;
                CollectData = txtDelCollectDataName.Text;
                CollectDataCode = txtDelCollectDataCode.Text;
                CollectPointMaxValue = txtDelMax.Text;
                CollectPointMinValue = txtDelMin.Text;
            }
            catch
            { }

        }

        /// <summary>
        /// 采集数据代码加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCollectData_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = "CollectDataInfo.xml";
            XmlDocument xd = new XmlDocument();
            xd.Load(path);
            XmlNode root = xd.DocumentElement;
            XmlNodeList xnl = root.ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                if (xn.Attributes["name"].Value == this.cmbCollectData.Text)
                {
                    this.txtCollectDataCode.Text = xn.Attributes["id"].Value;
                }
            }
            if (cmbCollectData.SelectedIndex >= 40)//选中开关量
            {
                txtMax.Text = "1";
                txtMin.Text = "0";
            }
            else
            {
                txtMax.Text = "";
                txtMin.Text = "";
            }
        }
        #region 编辑按钮

        //采集点内容修改
        private void btnModifyCollectPointName_Click(object sender, EventArgs e)
        {
            string content;//原始内容
            if (this.cmbCollectPointName.SelectedIndex != -1)
            {
                content = this.cmbCollectPointName.SelectedItem.ToString();
                FormEditCollectPoint fecp = new FormEditCollectPoint(content, 0, AreaName, ProjectName, CollectorName, CollectPointName, CollectPointCode);
                fecp.ShowDialog();
                //如果没有修改成功则返回之前的界面
                if (GlobalInfo.IsEditCollectPoint == true)
                {
                    this.Dispose();
                }
            }
            else
            { MessageBox.Show("请选择需要编辑的采集点名称！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }
        //采集点编码修改
        private void btnModifyCollectPointCode_Click(object sender, EventArgs e)
        {
            string content;//原始内容
            if (this.cmbCollectPointName.SelectedIndex != -1)
            {
                content = this.txtDelCollectPointCode.Text;
                FormEditCollectPoint fecp = new FormEditCollectPoint(content, 1, AreaName, ProjectName, CollectorName, CollectPointName, CollectPointCode);
                fecp.ShowDialog();
                //如果没有修改成功则返回之前的界面
                if (GlobalInfo.IsEditCollectPoint == true)
                {
                    this.Dispose();
                }
            }
            else
            { MessageBox.Show("请选择需要编辑的采集点名称！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        //采集数据名称修改
        private void btnModifyCollectDataName_Click(object sender, EventArgs e)
        {
            string content;//原始内容
            if (this.cmbCollectPointName.SelectedIndex != -1)
            {
                content = this.txtDelCollectDataName.Text;
                //FormEditCollectPoint fecp = new FormEditCollectPoint(content, 2, AreaName, ProjectName, CollectorName, CollectPointName, CollectPointCode);
                //fecp.ShowDialog();
                FormEditCollectPointsCollectData fecpc = new FormEditCollectPointsCollectData(content, AreaName, ProjectName, CollectorName, CollectPointName, CollectPointCode);
                fecpc.ShowDialog();
                //如果没有修改成功则返回之前的界面
                if (GlobalInfo.IsEditCollectPoint == true)
                {
                    this.Dispose();
                }
            }
            else
            { MessageBox.Show("请选择需要编辑的采集点名称！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        //采集数据代码修改
        private void btnModifyCollectDataCode_Click(object sender, EventArgs e)
        {
            string content;//原始内容
            if (this.cmbCollectPointName.SelectedIndex != -1)
            {
                content = this.txtDelCollectDataCode.Text;
                FormEditCollectPoint fecp = new FormEditCollectPoint(content, 3, AreaName, ProjectName, CollectorName, CollectPointName, CollectPointCode);
                fecp.ShowDialog();
                //如果没有修改成功则返回之前的界面
                if (GlobalInfo.IsEditCollectPoint == true)
                {
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show("请选择需要编辑的采集点名称！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //阈值上限修改
        private void button5_Click(object sender, EventArgs e)
        {
            string content;//原始内容
            if (this.cmbCollectPointName.SelectedIndex != -1)
            {
                content = this.txtDelMax.Text;
                FormEditCollectPoint fecp = new FormEditCollectPoint(content, 4, AreaName, ProjectName, CollectorName, CollectPointName, CollectPointCode);
                fecp.ShowDialog();
                //如果没有修改成功则返回之前的界面
                if (GlobalInfo.IsEditCollectPoint == true)
                {
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show("请选择需要编辑的采集点名称！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //阈值下限修改
        private void button6_Click(object sender, EventArgs e)
        {
            string content;//原始内容
            if (this.cmbCollectPointName.SelectedIndex != -1)
            {
                content = this.txtDelMin.Text;
                FormEditCollectPoint fecp = new FormEditCollectPoint(content, 5, AreaName, ProjectName, CollectorName, CollectPointName, CollectPointCode);
                fecp.ShowDialog();
                //如果没有修改成功则返回之前的界面
                if (GlobalInfo.IsEditCollectPoint == true)
                {
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show("请选择需要编辑的采集点名称！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion



    }
}
