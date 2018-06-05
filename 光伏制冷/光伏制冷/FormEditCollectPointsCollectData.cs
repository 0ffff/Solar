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
    public partial class FormEditCollectPointsCollectData : Form
    {
        string Content;//原始内容
        string AreaName;//地区名称
        string ProjectName;//项目名称
        string CollectorName;//采集器名称
        string CollectPointName;//采集点名称
        string CollectPointCode;//采集点代码
        DataAccess dataaccess = new DataAccess();
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="content">原始内容</param>
        /// <param name="areaName">地区名</param>
        ///<param name="projectNam">项目名</param>
        ///<param name="collectorName">采集器名称</param>
        public FormEditCollectPointsCollectData(string content, string areaName, string projectName, string collectorName, string collectPointName, string collectPointCode)
        {
            InitializeComponent();
            this.Content = content;
            this.AreaName = areaName;
            this.ProjectName = projectName;
            this.CollectorName = collectorName;
            this.CollectPointName = collectPointName;
            this.CollectPointCode = collectPointCode;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FormEditCollectPointsCollectData_Load(object sender, EventArgs e)
        {
            this.txtOriginal.Text = Content;
            //加载采集指标名称
            try
            {
                string path = "CollectDataInfo.xml";
                XmlDocument xd = new XmlDocument();
                xd.Load(path);
                XmlNode root = xd.DocumentElement;
                XmlNodeList xnl = root.ChildNodes;
                foreach (XmlNode xn in xnl)
                {
                    this.cmbNewContent.Items.Add(xn.Attributes["name"].Value);
                }
            }
            catch
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.cmbNewContent.SelectedIndex != -1)
            {
                if (DialogResult.OK == MessageBox.Show(string.Format("确认要将内容\"{0}\"替换为\"{1}\"", this.txtOriginal.Text, this.cmbNewContent.SelectedItem.ToString()), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                {
                    //0 判断是否已经存在该名称(不需要)(因为采集数据名称可以重复)
                    //1 修改数据库内容
                    //2 更新主界面内容（重新加载采集点）
                    string NewContent = this.cmbNewContent.SelectedItem.ToString();
                    //string SQL_IsExist = @"select CollectPointName from CollectPointInfo where CollectDataName='" + NewContent + "'";
                    //if (dataaccess.IsExistColletorOrPoint(SQL_IsExist) == false)
                    //{

                    string SQL_UpdateCollectPointInfo = @"update CollectPointInfo set CollectDataName='" + NewContent + "' where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "' and CollectorName='" + CollectorName + "' and CollectPointCode='" + CollectPointCode + "'";
                    if (dataaccess.ExeSQL(SQL_UpdateCollectPointInfo, GlobalInfo.DefaultDatabase) == 0)
                    {
                        MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GlobalInfo.IsEditCollectPoint = true;
                        this.Dispose();
                    }
                    else
                    { MessageBox.Show("修改失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                }
            }
        }
    }
}
