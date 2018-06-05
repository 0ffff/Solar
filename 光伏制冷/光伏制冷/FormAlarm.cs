using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace 光伏制冷
{
    public partial class FormAlarm : Form
    {
        DataAccess dataaccess = new DataAccess();
        public FormAlarm()
        {
            InitializeComponent();
            this.CenterToParent();
        }

        private void FormAlarm_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = "select Top 100 TimeStamp,AreaName,ProjectName,CollectorName,Info,State from tb_Alarm order by ID desc";
                DataSet ds = dataaccess.GetDataSet(sql, GlobalInfo.DefaultDatabase);
                DataTable dt = ds.Tables[0];
                dataGridView1.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string content = "";
                    if (dt.Rows[i][5].ToString() == "0")
                    {
                        content = "已处理";
                    }
                    else
                    {
                        content = "未处理";
                    }
                    this.dataGridView1.Rows.Insert(i,i + 1, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), content);
                }
            }
            catch
            {

            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime begin = this.dateTimePicker1.Value;//开始时间
            DateTime end = this.dateTimePicker2.Value;//结束时间

            //返回表格
            try
            {
                if (begin < end)
                {
                    string sql = "select * from tb_Alarm where TimeStamp between '" + begin.ToString("yyyy-MM-dd") + "' and '" + end.ToString("yyyy-MM-dd") + "' order by ID desc";
                    DataSet ds = dataaccess.GetDataSet(sql, GlobalInfo.DefaultDatabase);
                    DataTable dt = ds.Tables[0];
                    dataGridView1.Rows.Clear();
                    dataGridView1.DataSource = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string content = "";
                        if (dt.Rows[i][6].ToString() == "0")
                        {
                            content = "已处理";
                        }
                        else
                        {
                            content = "未处理";
                        }

                        this.dataGridView1.Rows.Insert(i, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), content);

                    }
                }
                else
                { MessageBox.Show("开始时间大于结束时间！请重新选择时间！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                string sql = "select * from tb_Alarm where State=0 order by ID desc";
                DataSet ds = dataaccess.GetDataSet(sql, GlobalInfo.DefaultDatabase);
                DataTable dt = ds.Tables[0];
                dataGridView1.Rows.Clear();
                dataGridView1.DataSource = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string content = "";
                    if (dt.Rows[i][6].ToString() == "0")
                    {
                        content = "已处理";
                    }
                    else
                    {
                        content = "未处理";
                    }

                    this.dataGridView1.Rows.Insert(i, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), content);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } 
        
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "select * from tb_Alarm where State=1 order by ID desc";
                DataSet ds = dataaccess.GetDataSet(sql, GlobalInfo.DefaultDatabase);
                DataTable dt = ds.Tables[0];
                dataGridView1.Rows.Clear();
                dataGridView1.DataSource = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string content = "";
                    if (dt.Rows[i][6].ToString() == "0")
                    {
                        content = "已处理";
                    }
                    else
                    {
                        content = "未处理";
                    }

                    this.dataGridView1.Rows.Insert(i, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), content);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql = "select * from tb_Alarm order by ID desc";
            DataSet ds = dataaccess.GetDataSet(sql, GlobalInfo.DefaultDatabase);
            DataTable dt = ds.Tables[0];
            dataGridView1.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string content = "";
                if (dt.Rows[i][5].ToString() == "0")
                {
                    content = "已处理";
                }
                else
                {
                    content = "未处理";
                }

                this.dataGridView1.Rows.Insert(i, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), content);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //string time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            ////string AreaName = "江苏省无锡市";
            ////string ProjectName = "颐养院太阳能热泵集热工程";
            //string CollectorName = "系统采集器";
            //string Info = "故障处理完毕";

            //string sql = "insert into tb_Alarm(TimeStamp,AreaName,ProjectName,CollectorName,Info,State)values('" + time + "','" + GlobalInfo.alarmAreaName + "','" + GlobalInfo.alarmProjectName + "','" + CollectorName + "','" + Info + "','0')";

            //dataaccess.ExeSQL(sql, GlobalInfo.DefaultDatabase);
        }
    }
}
