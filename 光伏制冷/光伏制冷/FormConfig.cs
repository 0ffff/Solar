using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Data.SqlClient;

namespace 光伏制冷
{
    public partial class FormConfig : Form
    {

        DataAccess dataaccess = new DataAccess();
        public FormConfig()
        {
            InitializeComponent();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            LoadArea();
            this.cmbArea.SelectedIndex = 0;
        }

        #region 加载项目信息
        /// <summary>
        /// 加载地区
        /// </summary>
        private void LoadArea()
        {
            try
            {
                this.cmbArea.Items.Clear();
                this.cmbProject.Items.Clear();
                this.cmbCollector.Items.Clear();
                this.cmbData.Items.Clear();

                string sql = "select Province,City from AreaInfo";

                SqlDataReader read = dataaccess.GetDataReader(sql, GlobalInfo.DefaultDatabase);
                while (read.Read())
                {
                    this.cmbArea.Items.Add(read["Province"].ToString() + read["City"].ToString());
                }
                dataaccess.conn.Close();
                read.Dispose();
            }
            catch
            {

            }
        }
        /// <summary>
        /// 加载项目
        /// </summary>
        private void LoadProject()
        {
            try
            {
                if (this.cmbArea.SelectedIndex != -1)
                {

                    this.cmbProject.Items.Clear();
                    this.cmbCollector.Items.Clear();
                    this.cmbData.Items.Clear();

                    string sql = "select ProjectName from CollectorInfo where AreaName='" + this.cmbArea.Text + "'";
                    SqlDataReader read = dataaccess.GetDataReader(sql, GlobalInfo.DefaultDatabase);
                    while (read.Read())
                    {
                        this.cmbProject.Items.Add(read["ProjectName"]);
                    }
                    dataaccess.conn.Close();
                    read.Dispose();
                }
                else
                {
                    this.cmbProject.Items.Clear();
                }
            }
            catch { }

        }
        /// <summary>
        /// 加载采集器
        /// </summary>
        private void LoadCollector()
        {
            try
            {
                if (this.cmbProject.SelectedIndex != -1 && this.cmbArea.SelectedIndex != -1)
                {
                    this.cmbCollector.Items.Clear();
                    this.cmbData.Items.Clear();

                    string sql = "select CollectorName from CollectorInfo where AreaName='" + this.cmbArea.Text + "' and ProjectName='" + this.cmbProject.Text + "'";

                    SqlDataReader read = dataaccess.GetDataReader(sql, GlobalInfo.DefaultDatabase);
                    while (read.Read())
                    {
                        this.cmbCollector.Items.Add(read["CollectorName"]);
                    }
                    dataaccess.conn.Close();
                    read.Dispose();
                }
                else
                { this.cmbCollector.Items.Clear(); }
            }
            catch { }
        }
        /// <summary>
        /// 加载水箱模式
        /// </summary>
        private void LoadData()
        {
            try
            {
                if (this.cmbArea.SelectedIndex != -1 && this.cmbProject.SelectedIndex != -1 && this.cmbCollector.SelectedIndex != -1)
                {
                    this.cmbData.Items.Clear();

                    string sql = "select CollectorMode from CollectorInfo where AreaName='" + this.cmbArea.Text + "' and ProjectName='" + this.cmbProject.Text + "' and CollectorName='" + this.cmbCollector.Text + "'";
                    string mode = dataaccess.ReturnSingleData(sql).ToString();//模式代码
                    if (mode == "2")
                    {

                        this.cmbData.Items.Add("单水箱模式");
                    }
                    else if (mode == "5")
                    {
                        this.cmbData.Items.Add("双水箱模式");
                    }


                }
                else
                {
                    this.cmbData.Items.Clear();
                }
            }
            catch { }
        }

        private void cmbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProject();
        }

        private void cmbProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCollector();
        }

        private void cmbCollector_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
            this.cmbData.SelectedIndex = 0;

        }
        #endregion

        #region 耗电量初始值配置
        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定重置电量初始值吗？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (cmbArea.Text != "" && cmbProject.Text != "" && cmbCollector.Text != "")
                {
                    if (textBox1.Text != "")
                    {

                    }
                    else
                    { MessageBox.Show("发送数据不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else { MessageBox.Show("请先将查询对象填写完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }
        #endregion

        #region 耗水量初始值配置
        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定重置水量初始值吗？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (cmbArea.Text != "" && cmbProject.Text != "" && cmbCollector.Text != "")
                {
                    if (textBox2.Text != "")
                    {
                       
                    }
                    else
                    { MessageBox.Show("发送数据不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else { MessageBox.Show("请先将查询对象填写完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }
        #endregion

    }
}
