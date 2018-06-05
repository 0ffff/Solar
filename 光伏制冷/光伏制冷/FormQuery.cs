using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;


namespace 光伏制冷
{
    public partial class FormQuery : Form
    {
        //声明
        public int flag = 0;
        DataAccess dataaccess = new DataAccess();
        DataTable dtHisData = new DataTable();//存放加载的数据
        DataTable dtHisDataCurve = new DataTable();
        //历史曲线开线程画图传递的参数
        struct Parameter
        {
            public int index;//下拉框选择的参数
            public Chart chart;//画图控件
        }
        Parameter parameter = new Parameter();//定义一个结构体对象
        int count = 20;    //每页显示的行数

        public FormQuery()
        {
            InitializeComponent();
        }

        private void FormQuery_Load(object sender, EventArgs e)
        {
            try
            {
                //this.CenterToParent();
                LoadArea();
                this.dtpBegin.Value = DateTime.Now;
                this.dtpEnd.Value = DateTime.Now;
                this.cmbArea.SelectedIndex = 0;//初选项
                this.tLP1.Visible = true;
                this.tLP2.Visible = false;
                
            }
            catch
            { }
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
                string sql = "select Province,City from AreaInfo";
                SqlDataReader dr = dataaccess.GetDataReader(sql, GlobalInfo.DefaultDatabase);
                while (dr.Read())
                {
                    this.cmbArea.Items.Add(dr["Province"].ToString() + dr["City"].ToString());
                }
                dataaccess.conn.Close();
                dr.Dispose();
            }
            catch
            { }
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
                    this.txtMode.Text = "";
                    string area = this.cmbArea.Text;
                    
                    string sql = "select ProjectName from ProjectInfo where AreaName='" + area + "'";
                    SqlDataReader dr = dataaccess.GetDataReader(sql, GlobalInfo.DefaultDatabase);
                    while (dr.Read())
                    {
                        this.cmbProject.Items.Add(dr["ProjectName"]);
                    }
                    dataaccess.conn.Close();
                    dr.Dispose();
                }
                else
                { this.cmbProject.Items.Clear(); }
            }
            catch
            { }
        }

        /// <summary>
        /// 加载控制器
        /// </summary>
        private void LoadCollector()
        {
            try
            {
                if ((this.cmbArea.SelectedIndex != -1) && (this.cmbProject.SelectedIndex != -1))
                {
                    this.cmbCollector.Items.Clear();
                    string area = this.cmbArea.Text;
                    string project = this.cmbProject.Text;
                    txtMode.Text = "";
                    string sql = "select CollectorName from CollectorInfo where AreaName='" + area + "' and ProjectName='" + project + "'";
                    SqlDataReader dr = dataaccess.GetDataReader(sql, GlobalInfo.DefaultDatabase);
                    while (dr.Read())
                    {
                        this.cmbCollector.Items.Add(dr["CollectorName"]);
                    }
                    dataaccess.conn.Close();
                    dr.Dispose();
                }
                else
                { this.cmbCollector.Items.Clear(); }
            }
            catch
            { }

        }
        /// <summary>
        /// 选择地区之后，加载项目名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param

        private void cmbArea_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadProject();
        }
        /// <summary>
        /// 选着项目之后加载控制器名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbProject_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadCollector();
        }

        /// <summary>
        /// 选择控制器之后 加载控制模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCollector_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadHisCollectPointName();
            string sql = "select CollectorMode from CollectorInfo where AreaName ='" + this.cmbArea.Text + "' and ProjectName ='" + cmbProject.Text + "' and CollectorName = '" + cmbCollector.Text + "'";
            string mode = dataaccess.ReturnSingleData(sql).ToString();//模式代码
            switch (mode)
            {
                case "1":
                    this.txtMode.Text = "1";
                    break;
                case "2":
                    this.txtMode.Text = "2";
                    break;
                case "4":
                    this.txtMode.Text = "4";
                    break;
                case "5":
                    this.txtMode.Text = "5";
                    break;
                case "8":
                    this.txtMode.Text = "8";
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 查询所有数据
        /// <summary>
        /// 监测数据查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            
            

            #region 源代码
            //this.tLP1.Visible = true;
            //this.tLP2.Visible = false;

            //this.pnlHistory.Visible = true;
            //this.pnlHistory2.Visible = false;
            //this.pnlHistory3.Visible = false;

            //this.panel1.Visible = true;
            //this.panel6.Visible = false;


            //if (this.cmbArea.Text != "" && this.cmbProject.Text != "" && this.cmbCollector.Text != "" && txtMode.Text != "")
            //{

            //    string databaseName = cmbArea.Text + cmbProject.Text;//数据库名称
            //    string collectorName = this.cmbCollector.Text;//采集器名称

            //    //找到对象的所有表格信息
            //    string SQL_TableNum = "select name from sysobjects where name like'" + collectorName + "20%" + "'";
            //    List<string> listName = new List<string>();
            //    DataSet ds_Name = dataaccess.GetDataSet(SQL_TableNum, databaseName);
            //    if (ds_Name != null)
            //    {
            //        DataTable dt_Name = ds_Name.Tables[0];

            //        //将所有表格的名称加载到list中
            //        for (int i = 0; i < dt_Name.Rows.Count; i++)
            //        {
            //            listName.Add(dt_Name.Rows[i][0].ToString());
            //        }
            //        ds_Name.Dispose();
            //        dt_Name.Dispose();
            //    }
            //    if (listName.Count != 0)
            //    {
            //        DataTable dtTem = GetCompleteData(listName, databaseName);//返回的完整数据
            //        if (dtTem != null)
            //        {
            //            dtHisData.Clear();//加载前清除数据
            //            dtHisData = dtTem;
            //            //总页数
            //            this.txtHisTotalPage.Text = CalTotalPageNum(dtHisData.Rows.Count, count).ToString();
            //            //当页数
            //            this.txtHisCurrentPage.Text = "1";
            //            //List<string> CollectPointNameList = new List<string>();//采集点名称列表
            //            //string sql_CollectPointName = "select CollectPointName from CollectPointInfo where AreaName='" + areaName + "' and ProjectName='" + projectName + "' and CollectorName='" + colletorName + "' order by CollectPointCode asc";
            //            //SqlDataReader dr= dataaccess.GetDataReader(sql_CollectPointName, GlobalInfo.DefaultDatabase);
            //            //while (dr.Read())
            //            //{
            //            //    CollectPointNameList.Add(dr[0].ToString());//添加入列表
            //            //}
            //            //dataaccess.conn.Close();
            //            //dr.Dispose();
            //            //插入数据 导入采集点名称列表以便于表格显示
            //            InsertintoDgv(this.dgvHistory, dtHisData, 1, count);
            //        }
            //    }
            //    else
            //    {
            //        this.dgvHistory.Rows.Clear();
            //        MessageBox.Show("数据表格不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }

            //}
            //else
            //{
            //    MessageBox.Show("请先将查询对象填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            #endregion
        }
        
        #endregion

        #region 按时间查询数据参数策略
      





     
       
        #endregion

        /// <summary>
        ///在systemobjects中获取所有表格名称的数据
        /// </summary>
        /// <param name="listTableName">所有表格名称</param>
        /// <param name="databaseName">数据库名称</param>
        /// <returns>返回完整数据的table</returns>
        private DataTable GetCompleteData(List<string> listTableName, string databaseName)
        {
            try
            {
                StringBuilder SQL = new StringBuilder();
                for (int i = 0; i < listTableName.Count; i++)
                {
                    if (i != listTableName.Count - 1)
                    {
                        SQL.Append("select * from " + listTableName[i] + " union all ");
                    }
                    else
                    {
                        SQL.Append("select * from " + listTableName[i] + " order by TimeStamp asc");
                    }
                }
                DataSet ds = dataaccess.GetDataSet(SQL.ToString(), databaseName);
                DataTable dt = ds.Tables[0];
                return dt;
            }
            catch
            { return null; }
        }

        /// <summary>
        /// 计算总页数
        /// </summary>
        /// <param name="totalNum">数据总量</param>
        /// <param name="perNum">每页显示的数据</param>
        /// <returns></returns>
        private int CalTotalPageNum(int totalNum, int perNum)
        {
            if (totalNum != 0)
            {
                int totalPage;
                if (totalNum % perNum != 0)//数据总行数除以每页显示的数据行数  有余数
                {
                    totalPage = (int)totalNum / perNum + 1;
                }
                else
                {
                    totalPage = totalNum / perNum;
                }
                return totalPage;
            }
            //如果数据总量为0 则返回1 避免与当前页发生矛盾  避免总页数=0当前页=1的情况
            else
            { return 1; }
        }


        /// <summary>
        ///  向表格中插入一定区间的数据
        /// 1 通过计算出需要加载的数据区间来对表格进行赋值
        /// 2 注意datagridview需要加载的行号与数据表的行号不一致需要通过一定的转化
        /// </summary>
        /// <param name="dgv">要操作的表格</param>
        /// <param name="dt">数据</param>
        /// <param name="loadPage">要加载的页码</param>
        /// <param name="perPageNum">每页的数据量</param>
        private void InsertintoDgv(DataGridView dgv, DataTable dt, int loadPage, int perPageNum)
        {
            try
            {
                int totalNum = dt.Rows.Count;//数据总数
                int totalPage = CalTotalPageNum(totalNum, perPageNum);//总页数

                //找到采集点名称
                List<string> CollectPointNameList = new List<string>();//采集点名称列表
                string sql_CollectPointName = "select CollectPointName from CollectPointInfo where AreaName='" + this.cmbArea.Text + "' and ProjectName='" + this.cmbProject.Text + "' and CollectorName='" + this.cmbCollector.Text + "' order by CollectPointCode asc";
                SqlDataReader dr = dataaccess.GetDataReader(sql_CollectPointName, GlobalInfo.DefaultDatabase);
                while (dr.Read())
                {
                    CollectPointNameList.Add(dr[0].ToString());//添加入列表
                }
                dataaccess.conn.Close();
                dr.Dispose();


                int start = perPageNum * (loadPage - 1);//开始插入数据的行号=每页数据量*（要加载的页码-1）
                int end1 = perPageNum * loadPage - 1;//若不是最后一页  结束插入数据的行号=[每页数据量*(要加载的页码-1)+每页数据量]-1
                int end2 = totalNum - 1;//若是最后一页 则计算出剩余的数据量

                //switch 判断
                string projectname= this.cmbArea.Text+this.cmbProject.Text+this.cmbCollector.Text;

                //如果是历史数据表则需要添加表头
                if ((dgv.Name == "dgvHistory"))
                {
                    dgv.Columns.Clear();
                    //添加表头
                    dgv.Columns.Add("column0", "上传时间");
                    dgv.Columns["column0"].Width = 200;//时间格子设置为160宽度
                    dgv.Columns["column0"].SortMode = DataGridViewColumnSortMode.NotSortable;

                    switch (projectname)
                    {
                        case "老挝行政区万象市妇幼保健院热水系统系统采集器":
                            {
                                for (int i = 1; i <= 8;i++ )
                                {
                                    dgv.Columns.Add("column" + i.ToString(), CollectPointNameList[i - 1] + "(℃)");
                                }
                                for (int j = 9; j <= 12;j++ )
                                {
                                    dgv.Columns.Add("column" + j.ToString(), CollectPointNameList[j - 1] + "(m³/s)");
                                }
                                for (int k = 13; k <= 16;k++ )
                                {
                                    dgv.Columns.Add("column" + k.ToString(), CollectPointNameList[k - 1] + "(m³)");
                                }
                                dgv.Columns.Add("column17" , CollectPointNameList[16] + "(kWh)");
                                dgv.Columns.Add("column18", CollectPointNameList[17] + "(kW)");
                                dgv.Columns.Add("column19", CollectPointNameList[18] + "(A)");
                                dgv.Columns.Add("column20", CollectPointNameList[19] + "(V)");
                                for (int l = 21; l <= 25; l++)
                                {
                                    dgv.Columns.Add("column" + l.ToString(), CollectPointNameList[l - 1]);
                                }

                            }
                            break;
                        case "老挝行政区万象市研究所热水系统系统采集器": 
                            {
                                for (int i = 1; i <= 5;i++ )
                                {
                                    dgv.Columns.Add("column" + i.ToString(), CollectPointNameList[i - 1] + "(℃)");
                                }
                                dgv.Columns.Add("column6", CollectPointNameList[5] + "(kWh)");
                                dgv.Columns.Add("column7", CollectPointNameList[6] + "(kW)");
                                dgv.Columns.Add("column8", CollectPointNameList[7] + "(A)");
                                dgv.Columns.Add("column9", CollectPointNameList[8] + "(V)");
                                dgv.Columns.Add("column10", CollectPointNameList[9] );


                            }
                            break;
                        case "云南省香格里拉市香格里拉单水箱供暖系统系统采集器":
                            {
                                for (int i = 1; i <= 6; i++)
                                {
                                    dgv.Columns.Add("column" + i.ToString(), CollectPointNameList[i - 1] + "(℃)");
                                }
                                dgv.Columns.Add("column7", CollectPointNameList[6] + "(%)");
                                
                                for (int k = 8; k <= 9; k++)
                                {
                                    dgv.Columns.Add("column" + k.ToString(), CollectPointNameList[k - 1] + "(m³/s)");
                                }
                                dgv.Columns.Add("column10", CollectPointNameList[9] + "(A)");
                                dgv.Columns.Add("column11", CollectPointNameList[10] + "(V)");
                                dgv.Columns.Add("column12", CollectPointNameList[11] + "(kW)");
                                dgv.Columns.Add("column13", CollectPointNameList[12] + "(m)");
                                dgv.Columns.Add("column12", CollectPointNameList[13] + "(m/s)");
                                dgv.Columns.Add("column12", CollectPointNameList[14] + "(W/㎡)");
                                for (int l = 16; l <= 18; l++)
                                {
                                    dgv.Columns.Add("column" + l.ToString(), CollectPointNameList[l - 1] );
                                }


                            } break;
                        case "云南省昆明市光伏制冷系统系统采集器":
                            for (int i = 1; i <= 50; i++)
                            {
                                dgv.Columns.Add("column" + i.ToString(), CollectPointNameList[i-1]);
                            }
                            break;
                    }

                }

                //插入数据
                if (totalNum != 0)//数据总数不为0 否则下面的算法会出错
                {
                    if (dgv.Rows.Count != 0)
                    {
                        dgv.Rows.Clear();
                    }

                    int dgvStart;
                    if (loadPage != totalPage)//不是最后一页
                    {
                        for (int j = start; j <= end1; j++)
                        {
                            dgvStart = j - (perPageNum * (loadPage - 1));//开始行号
                            dgv.Rows.Add(1);
                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                //dgv.Rows.Add(1);                                                   //start - (perPageNum * (loadPage - 1))对应于datagridview的开始行号 
                                dgv.Rows[dgvStart].Cells[k].Value = dt.Rows[j][k].ToString();//注意表格需要加载的行号与数据表的行号不一致需要通过一定的转化
                            }
                        }
                    }
                    else//最后一页
                    {
                        for (int j = start; j <= end2; j++)
                        {
                            dgvStart = j - (perPageNum * (loadPage - 1));//开始行号

                            dgv.Rows.Add(1);

                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                //dgv.Rows.Add(1);
                                dgv.Rows[dgvStart].Cells[k].Value = dt.Rows[j][k].ToString();
                            }
                        }
                    }
                }
                else
                {
                    dgv.Rows.Clear();//若数据总数为0则清除表格    
                    return;
                }
            }
            catch
            { }
        }

        

        /// <summary>
        ///在systemobjects中获取符合时间的所有表格名称
        ///再对每一张表格用时间去取数据
        ///此处的sql语言也是包括两个边界的 只是表格中还有具体的时间 
        ///例如查找的数据是2012-04-02至2012-05-02则时间范围是2012-04-02的00:00至2012-05-02的00:00 
        ///因此2012-05-02的01:00就不在查找范围内
        /// </summary>
        /// <param name="listTableName">所有表格名称</param>
        /// <param name="databaseName">数据库名称</param>
        /// <returns>返回完整数据的table</returns>
        private DataTable GetTimeSpanData(List<string> listTableName, string databaseName, DateTime begin, DateTime end)
        {
            try
            {
                StringBuilder SQL = new StringBuilder();
                for (int i = 0; i < listTableName.Count; i++)
                {
                    if (i != listTableName.Count - 1)
                    {
                        SQL.Append("select * from " + listTableName[i] + " where TimeStamp between '" + begin.ToString("yyyy-MM-dd") + "' and '" + end.ToString("yyyy-MM-dd") + "' union all ");
                    }
                    else
                    {
                        SQL.Append("select * from " + listTableName[i] + " where TimeStamp between '" + begin.ToString("yyyy-MM-dd") + "' and '" + end.ToString("yyyy-MM-dd") + "' order by TimeStamp desc");
                    }
                }
                DataSet ds = dataaccess.GetDataSet(SQL.ToString(), databaseName);
                DataTable dt = ds.Tables[0];
                return dt;
            }
            catch
            { return null; }
        }

       
        /// <summary>
        /// 加载图形统计的cmb
        /// </summary>
        private void LoadHisCollectPointName()
        {
            this.cmbCollectPointNameOne.Items.Clear();

            try
            {
                string sql = "select CollectPointName,CollectDataName from CollectPointInfo where AreaName='" + cmbArea.SelectedItem.ToString() + "' and ProjectName='" + cmbProject.SelectedItem.ToString() + "' and CollectorName='" + this.cmbCollector.SelectedItem.ToString() + "' order by cast(CollectPointCode as int)";
                SqlDataReader dr = dataaccess.GetDataReader(sql, GlobalInfo.DefaultDatabase);
                while (dr.Read())
                {

                    this.cmbCollectPointNameOne.Items.Add(dr["CollectPointName"].ToString());

                }
                dataaccess.conn.Close();
            }
            catch
            { }
        }
 

        /// <summary>
        /// 历史曲线按钮执行函数
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="chart">画图控件名称</param>
        /// <param name="cmb">采集点下拉框</param>
        private void SearchHisData(DateTime begin, DateTime end, Chart chart, ComboBox cmb)
        {

            // 1 按照时间取出相应的数据
            dtHisDataCurve.Clear();
            dtHisDataCurve = LoadTimeSpanDataHisCurve(begin, end);

            // 2 画图
            chart.Titles[0].Text = cmb.SelectedItem.ToString();//标题
            //chart.Series[0].Name = LoadCollectDataNameThroughCollectPointName(cmb.SelectedItem.ToString());//例图中的说明
            chart.Series[0].Points.Clear();//画图前清除数据
            int index = cmb.SelectedIndex;//下拉框选择的序号 既知道了要显示的是第几号采集点
            //x轴数据的列号是=index*3+2 y轴数据的列号index*3+1
            //用线程池画图 减少消耗 因为要频繁画图所以用线程池更加节省内存空间  
            parameter.chart = chart;//画图控件
            parameter.index = index;
            if (dtHisDataCurve.Rows.Count != 0)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(DrawHis));
            }
            else
            { MessageBox.Show("所选时间段内无数据！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        

        /// <summary>
        /// 历史数据曲线中加载 按时间查找的数据
        /// </summary>
        /// <param name="code">查找的是历史数据或者是统计数据(sql查询语句会不同) 0 历史数据 1 统计数据</param>
        /// <param name="begin">起始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns></returns>
        private DataTable LoadTimeSpanDataHisCurve(DateTime begin, DateTime end)
        {
            string database = this.cmbArea.Text + this.cmbProject.Text;//数据库名称
            string collectorname = cmbCollector.Text;//采集器名称


            string SQL = "select name from sysobjects where name between '" + collectorname+"实时" + begin.ToString("yyyyMM") + "' and '" + collectorname +"实时"+ end.ToString("yyyyMM") + "'";

            DataSet ds_Name = dataaccess.GetDataSet(SQL, database);
            //string test = ds_Name.Tables[0].ToString();
            DataTable dt_Name = ds_Name.Tables[0];
            List<string> listName = new List<string>();
            //将所有表格的名称加载到list中
            for (int i = 0; i < dt_Name.Rows.Count; i++)
            {
                listName.Add(dt_Name.Rows[i][0].ToString());
            }
            ds_Name.Dispose();//释放减少垃圾
            dt_Name.Dispose();//释放减少垃圾

            if (listName.Count != 0)//找不到对应的表格（找不到要求的时间的表格）
            {
                DataTable dtTem = GetTimeSpanData(listName, database, begin, end);
                return dtTem;//返回数据
            }
            else
            {
                MessageBox.Show("数据表格不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }


        }

        /// <summary>
        /// 历史曲线画图
        /// </summary>
        private void DrawHis(object state)
        {
            try
            {
                //委托画图
                Action action = delegate
                {
                    //数据量过大，每10个数据画一个点
                    for (int i = 0; i < dtHisDataCurve.Rows.Count; i ++)
                    {
                        if ((dtHisDataCurve.Rows[i][0].ToString() != "null") && (dtHisDataCurve.Rows[i][parameter.index + 1].ToString() != "null"))//防止数据掉包的帧
                        {
                            parameter.chart.Series[0].Points.AddXY(dtHisDataCurve.Rows[i][0].ToString(), Convert.ToDouble(dtHisDataCurve.Rows[i][parameter.index + 1].ToString()));
                        }
                    }
                };
                this.Invoke(action);

            }
            catch
            { }
        }


      

      

        #region 计量查询
        private void button11_Click(object sender, EventArgs e)
        {

            this.tLP1.Visible = true;
            this.tLP2.Visible = false;

            this.pnlHistory.Visible = false;
            this.pnlHistory2.Visible = false;
            this.pnlHistory3.Visible = true;


            this.panel6.Visible = true;
            this.panel1.Visible = false;

            if (this.cmbArea.Text != "" && this.cmbProject.Text != "" && this.cmbCollector.Text != "")
            {

                string databaseName = cmbArea.Text + cmbProject.Text;//数据库名称
                string collectorName = cmbCollector.Text;//采集器名称
                //找到对象的所有表格信息
                string SQL_TableNum = "select name from sysobjects where name like'" + collectorName + "Index20%" + "'";
                DataSet ds_Name = dataaccess.GetDataSet(SQL_TableNum, databaseName);
                List<string> listName = new List<string>();
                if (ds_Name != null)
                {
                    DataTable dt_Name = ds_Name.Tables[0];

                    //将所有表格的名称加载到list中
                    for (int i = 0; i < dt_Name.Rows.Count; i++)
                    {
                        listName.Add(dt_Name.Rows[i][0].ToString());
                    }
                    ds_Name.Dispose();
                    dt_Name.Dispose();
                }
                if (listName.Count != 0)
                {
                    DataTable dtTem = GetCompleteData(listName, databaseName);//返回的完整数据
                    if (dtTem != null)
                    {
                        dtHisData.Clear();//加载前清除数据
                        dtHisData = dtTem;
                        //总页数
                        this.textBox1.Text = CalTotalPageNum(dtHisData.Rows.Count, count).ToString();
                        //当页数
                        this.textBox2.Text = "1";
                        InsertintoDgv2(this.dgvHistory3, dtHisData, 1, count);
                    }
                }
                else
                {
                    this.dgvHistory3.Rows.Clear();
                    MessageBox.Show("数据表格不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("请先将查询对象填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void InsertintoDgv2(DataGridView dgv, DataTable dt, int loadPage, int perPageNum)
        {
            try
            {
                int totalNum = dt.Rows.Count;//数据总数
                int totalPage = CalTotalPageNum(totalNum, perPageNum);//总页数

                //找到采集点名称
                List<string> CollectDataNameList = new List<string>();//采集点名称列表
                string sql_CollectDataName = "select distinct CollectDataName,CollectDataCode from CollectPointInfo where AreaName='" + this.cmbArea.Text + "' and ProjectName='" + this.cmbProject.Text + "' and CollectorName='" + this.cmbCollector.Text + "' order by CollectDataCode asc";
                SqlDataReader dr = dataaccess.GetDataReader(sql_CollectDataName, GlobalInfo.DefaultDatabase);
                while (dr.Read())
                {
                    CollectDataNameList.Add(dr[0].ToString());//添加入列表
                }
                dataaccess.conn.Close();
                dr.Dispose();


                int start = perPageNum * (loadPage - 1);//开始插入数据的行号=每页数据量*（要加载的页码-1）
                int end1 = perPageNum * loadPage - 1;//若不是最后一页  结束插入数据的行号=[每页数据量*(要加载的页码-1)+每页数据量]-1
                int end2 = totalNum - 1;//若是最后一页 则计算出剩余的数据量

                string projectname = this.cmbArea.Text + this.cmbProject.Text + this.cmbCollector.Text;
                
                //如果是历史数据表则需要添加表头
                if ((dgv.Name == "dgvHistory3"))
                {
                    dgv.Columns.Clear();
                    //添加表头
                    dgv.Columns.Add("column0", "上传时间");
                    dgv.Columns["column0"].Width = 200;//时间格子设置为160宽度
                    dgv.Columns["column0"].SortMode = DataGridViewColumnSortMode.NotSortable;

                    //将所有表格的名称加载到list中
                    switch (projectname)
                    {
                        case "老挝行政区万象市妇幼保健院热水系统系统采集器":
                            {

                                dgv.Columns.Add("column1", CollectDataNameList[0] + "(m³)");
                                dgv.Columns.Add("column2", CollectDataNameList[1] + "(m³)");
                                dgv.Columns.Add("column3", CollectDataNameList[2] + "(kWh)");
                                
                                for (int i = 4; i <= 10; i++)
                                {
                                    dgv.Columns.Add("column" + i.ToString(), CollectDataNameList[i - 1]+"(mJ)");
                                }
                                dgv.Columns.Add("column11", CollectDataNameList[10] + "(吨标准煤)");
                                
                                for (int i = 12; i <= 15; i++)
                                {
                                    dgv.Columns.Add("column" + i.ToString(), CollectDataNameList[i - 1] + "(kg)");
                                }
                                for (int i = 16; i <= 18; i++)
                                {
                                    dgv.Columns.Add("column" + i.ToString(), CollectDataNameList[i - 1] + "(%)");
                                }

                            }
                            break;
                        case "老挝行政区万象市研究所热水系统系统采集器":
                            {
                                dgv.Columns.Add("column1", CollectDataNameList[0] + "(m³)");
                                dgv.Columns.Add("column2", CollectDataNameList[1] + "(kWh)");
                                dgv.Columns.Add("column3", CollectDataNameList[2] + "(mJ)");
                                dgv.Columns.Add("column4", CollectDataNameList[3] + "(吨标准煤)");
                               
                                for (int i = 5; i <= 8 ;i++)
                                {
                                    dgv.Columns.Add("column" + i.ToString(), CollectDataNameList[i - 1] + "(kg)");
                                }

                            } break;
                        case "云南省香格里拉市香格里拉单水箱供暖系统系统采集器":
                            {
                                dgv.Columns.Add("column1", CollectDataNameList[0] + "(kWh)");
                                dgv.Columns.Add("column2", CollectDataNameList[1] + "(mJ)");
                                dgv.Columns.Add("column3", CollectDataNameList[2] + "(mJ)");
                                dgv.Columns.Add("column4", CollectDataNameList[3] + "(吨标准煤)");

                                for (int i = 5; i <= 8; i++)
                                {
                                    dgv.Columns.Add("column" + i.ToString(), CollectDataNameList[i - 1] + "(kg)");
                                }

                            } break;
                    }
                    //for (int i = 1; i <= dt.Columns.Count - 1; i++)
                    //{
                    //    dgv.Columns.Add("column" + i.ToString(), CollectDataNameList[i - 1]);
                    //}



                }

                //插入数据
                if (totalNum != 0)//数据总数不为0 否则下面的算法会出错
                {
                    if (dgv.Rows.Count != 0)
                    {
                        dgv.Rows.Clear();
                    }

                    int dgvStart;
                    if (loadPage != totalPage)//不是最后一页
                    {
                        for (int j = start; j <= end1; j++)
                        {
                            dgvStart = j - (perPageNum * (loadPage - 1));//开始行号
                            dgv.Rows.Add(1);
                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                //dgv.Rows.Add(1);                                                   //start - (perPageNum * (loadPage - 1))对应于datagridview的开始行号 
                                dgv.Rows[dgvStart].Cells[k].Value = dt.Rows[j][k].ToString();//注意表格需要加载的行号与数据表的行号不一致需要通过一定的转化
                            }
                        }
                    }
                    else//最后一页
                    {
                        for (int j = start; j <= end2; j++)
                        {
                            dgvStart = j - (perPageNum * (loadPage - 1));//开始行号

                            dgv.Rows.Add(1);

                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                //dgv.Rows.Add(1);
                                dgv.Rows[dgvStart].Cells[k].Value = dt.Rows[j][k].ToString();
                            }
                        }
                    }
                }
                else
                {
                    dgv.Rows.Clear();//若数据总数为0则清除表格    
                    return;
                }
            }
            catch
            { }
        }
        #endregion

        /// <summary>
        /// 热泵数据插入函数
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="dt"></param>
        /// <param name="loadPage"></param>
        /// <param name="perPageNum"></param>
        private void InsertintoDgv3(DataGridView dgv, DataTable dt, int loadPage, int perPageNum)
        {
            try
            {
                int totalNum = dt.Rows.Count;//数据总数
                int totalPage = CalTotalPageNum(totalNum, perPageNum);//总页数

              
                int start = perPageNum * (loadPage - 1);//开始插入数据的行号=每页数据量*（要加载的页码-1）
                int end1 = perPageNum * loadPage - 1;//若不是最后一页  结束插入数据的行号=[每页数据量*(要加载的页码-1)+每页数据量]-1
                int end2 = totalNum - 1;//若是最后一页 则计算出剩余的数据量


                string[] name = new string[12] { "环境温度", "温度传感器状态", "盘管1温度状态", "排气1温度状态", "低压开关状态", "高压开关状态", "过流开关状态", "排气1保护状态", "缺相逆相状态", "热水温度", "开关机状态", "水箱回差温度" };
                List<string> CollectPointNameList = new List<string>();//采集点名称列表
                for (int i = 0; i < name.Length; i++)
                {
                    CollectPointNameList.Add(name[i]);
                }

                //如果是历史数据表则需要添加表头
                if ((dgv.Name == "dgvHistory4"))
                {
                    //dgv.DataSource = null;
                    dgv.Columns.Clear();
                    dgv.Columns.Add("column0", "上传时间");
                    dgv.Columns["column0"].Width = 180;//时间格子设置为160宽度
                    dgv.Columns["column0"].SortMode = DataGridViewColumnSortMode.NotSortable;
                    for (int i = 1; i <= dt.Columns.Count - 1; i++)
                    {
                        dgv.Columns.Add("column" + i.ToString(), CollectPointNameList[i - 1]);
                    }



                }

                //插入数据
                if (totalNum != 0)//数据总数不为0 否则下面的算法会出错
                {
                    if (dgv.Rows.Count != 0)
                    {
                        dgv.Rows.Clear();
                    }

                    int dgvStart;
                    if (loadPage != totalPage)//不是最后一页
                    {
                        for (int j = start; j <= end1; j++)
                        {
                            dgvStart = j - (perPageNum * (loadPage - 1));//开始行号
                            dgv.Rows.Add(1);
                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                //dgv.Rows.Add(1);                                                   //start - (perPageNum * (loadPage - 1))对应于datagridview的开始行号 
                                dgv.Rows[dgvStart].Cells[k].Value = dt.Rows[j][k].ToString();//注意表格需要加载的行号与数据表的行号不一致需要通过一定的转化
                            }
                        }
                    }
                    else//最后一页
                    {
                        for (int j = start; j <= end2; j++)
                        {
                            dgvStart = j - (perPageNum * (loadPage - 1));//开始行号

                            dgv.Rows.Add(1);

                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                //dgv.Rows.Add(1);
                                dgv.Rows[dgvStart].Cells[k].Value = dt.Rows[j][k].ToString();
                            }
                        }
                    }
                }
                else
                {
                    dgv.Rows.Clear();//若数据总数为0则清除表格    
                    return;
                }
            }
            catch
            { }
        }

       

        #region EXCEL导出
        //EXCEL导出
        private void SaveToExcel(List<string> listheadname, DataTable dt)
        {
            //tableLayoutPanel4.Visible = false;
            try
            {
                SaveFileDialog savefiledialog = new SaveFileDialog();
                savefiledialog.Filter = "Execl files (*.xls)|*.xls";
                savefiledialog.FilterIndex = 0;
                savefiledialog.RestoreDirectory = true;
                savefiledialog.CreatePrompt = true;
                savefiledialog.Title = "Export Excel File To";

                if (savefiledialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }


                Stream MyStream;
                MyStream = savefiledialog.OpenFile();
                System.Text.Encoding.GetEncoding("gb2312");
                StreamWriter sw = new StreamWriter(MyStream, System.Text.Encoding.GetEncoding(-0));
                string str = "";//写标题用的字符串
                try
                {
                    //写标题

                    for (int i = 0; i < listheadname.Count; i++)
                    {
                        if (i > 0)
                        {
                            str += "\t";
                        }
                        str += listheadname[i];
                    }
                    sw.WriteLine(str);

                    //写内容
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        string tempStr = "";
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                tempStr += "\t";
                            }
                            if (k == 2)
                            {
                                string Number = dt.Rows[j][k].ToString();
                            }
                            tempStr += dt.Rows[j][k].ToString();
                        }
                        sw.WriteLine(tempStr);
                    }
                    sw.Close();
                    MyStream.Close();
                }
                catch
                {
                    //return;
                    //MessageBox.Show(err.Message);
                }
                finally
                {
                    sw.Close();
                    MyStream.Close();
                    MessageBox.Show("数据导出成功!");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 图片导出
        private void ExportPicture(string fileName)
        {
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.FileName = fileName;
            //saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;

            saveDialog.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg|EMF (*.emf)|*.emf|PNG (*.png)|*.png|SVG (*.svg)|*.svg|GIF (*.gif)|*.gif|TIFF (*.tif)|*.tif";
            saveDialog.FilterIndex = 2;
            saveDialog.RestoreDirectory = true;

            // Set image file format
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                ChartImageFormat format = ChartImageFormat.Bmp;

                if (saveDialog.FileName.EndsWith("bmp"))
                {
                    format = ChartImageFormat.Bmp;
                }
                else if (saveDialog.FileName.EndsWith("jpg"))
                {
                    format = ChartImageFormat.Jpeg;
                }
                else if (saveDialog.FileName.EndsWith("emf"))
                {
                    format = ChartImageFormat.Emf;
                }
                else if (saveDialog.FileName.EndsWith("gif"))
                {
                    format = ChartImageFormat.Gif;
                }
                else if (saveDialog.FileName.EndsWith("png"))
                {
                    format = ChartImageFormat.Png;
                }
                else if (saveDialog.FileName.EndsWith("tif"))
                {
                    format = ChartImageFormat.Tiff;
                }

                // Save image
                chartHisOne.SaveImage(saveDialog.FileName, format);
            }
        }
        #endregion

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #region 控制策略
        private void button2_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 控制参数
        private void button3_Click_1(object sender, EventArgs e)
        {

        }
        #endregion

        #region 图形统计
        private void btnCurve_Click_1(object sender, EventArgs e)
        {
            ++flag;
            if (flag%2==1)
            {
                this.tLP2.Visible = true;
                this.tLP1.Visible = false;
                this.dtpBeginOne.Text = DateTime.Now.ToString();
                this.dtpEndOne.Text = DateTime.Now.ToString();

                if (this.cmbArea.Text != "" && this.cmbProject.Text != "" && this.cmbCollector.Text != "" && txtMode.Text != "")
                {
                    LoadHisCollectPointName();//加载下拉框
                }
                else
                {
                    MessageBox.Show("请先将查询对象填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                this.tLP1.Visible = true;
                this.tLP2.Visible = false;
                this.dtpBeginOne.Text = DateTime.Now.ToString();
                this.dtpEndOne.Text = DateTime.Now.ToString();
            }

        }
        #endregion


        #region 数据导出
        private void button4_Click_1(object sender, EventArgs e)
        {
            List<string> listheadname = new List<string>();
            //List<string> listmetering = new List<string>();
            //List<string> listSc= new List<string>();
            if (this.dgvHistory.Rows.Count != 0)
            {
                listheadname.Clear();
                for (int i = 0; i < this.dgvHistory.Columns.Count; i++)
                {
                    listheadname.Add(this.dgvHistory.Columns[i].HeaderText);
                }
                SaveToExcel(listheadname, dtHisData);
            }
            else if (this.dgvHistory3.Rows.Count != 0)
            {
                listheadname.Clear();
                for (int i = 0; i < this.dgvHistory3.Columns.Count; i++)
                {
                    listheadname.Add(this.dgvHistory3.Columns[i].HeaderText);
                }
                SaveToExcel(listheadname, dtHisData);
            }
            else if (this.dgvHistory2.Rows.Count != 0)
            {
                listheadname.Clear();
                for (int i = 0; i < this.dgvHistory2.Columns.Count; i++)
                {
                    listheadname.Add(this.dgvHistory2.Columns[i].HeaderText);
                }
                SaveToExcel(listheadname, dtHisData);
            }
            else if (this.dgvHistory4.Rows.Count != 0)
            {
                listheadname.Clear();
                for (int i = 0; i < this.dgvHistory4.Columns.Count; i++)
                {
                    listheadname.Add(this.dgvHistory4.Columns[i].HeaderText);
                }
                SaveToExcel(listheadname, dtHisData);
            }


            else
            { MessageBox.Show("缺少数据源！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        #endregion

        #region 数据时间查询
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            this.tLP1.Visible = true;
            this.tLP2.Visible = false;

            this.pnlHistory.Visible = true;
            this.pnlHistory2.Visible = false;
            this.pnlHistory3.Visible = false;
            this.pnlHistory4.Visible = false;

            this.panel1.Visible = true;
            this.panel6.Visible = false;
            this.panel7.Visible = false;

            this.dgvHistory.Rows.Clear();
            this.dgvHistory2.DataSource = null;
            this.dgvHistory3.Rows.Clear();
            this.dgvHistory4.Rows.Clear();

            if (this.cmbArea.Text != "" && this.cmbProject.Text != "" && this.cmbCollector.Text != "" && txtMode.Text != "")
            {


                DateTime begin = this.dtpBegin.Value;//开始时间
                DateTime end = this.dtpEnd.Value;//结束时间
                string database = this.cmbArea.Text + this.cmbProject.Text;//数据库名称
                string collectorname = cmbCollector.Text;//采集器名称
                if (begin < end)
                {
                    string SQL = "";
                    SQL = "select name from sysobjects where name between '" + collectorname + "实时" + begin.ToString("yyyyMM") + "' and '" + collectorname + "实时" + end.ToString("yyyyMM") + "'";

                    DataSet ds_Name = dataaccess.GetDataSet(SQL, database);
                    DataTable dt_Name = ds_Name.Tables[0];
                    List<string> listName = new List<string>();
                    //将所有表格的名称加载到list中
                    for (int i = 0; i < dt_Name.Rows.Count; i++)
                    {
                        listName.Add(dt_Name.Rows[i][0].ToString());
                    }
                    ds_Name.Dispose();
                    dt_Name.Dispose();

                    if (listName.Count != 0)//找不到对应的表格（找不到要求的时间的表格）
                    {
                        DataTable dtTem = GetTimeSpanData(listName, database, begin, end);
                        if (dtTem != null)
                        {
                            dtHisData.Clear();//加载前清除数据
                            dtHisData = dtTem;//重新加载datatable 此时的数据为经过时间节选过的数据
                            //总页数
                            this.txtHisTotalPage.Text = CalTotalPageNum(dtHisData.Rows.Count, count).ToString();
                            //当页数
                            this.txtHisCurrentPage.Text = "1";//当前页是第一页
                            //插入数据
                            InsertintoDgv(this.dgvHistory, dtHisData, 1, count);//加载第一页数据
                        }
                    }
                    else
                    {
                        this.dgvHistory.Rows.Clear();
                        MessageBox.Show("数据表格不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                { MessageBox.Show("开始时间大于结束时间！请重新选择时间！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else
            {
                MessageBox.Show("请先将查询对象填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 计量时间查询
        private void button5_Click_1(object sender, EventArgs e)
        {
            this.tLP1.Visible = true;
            this.tLP2.Visible = false;

            this.pnlHistory.Visible = false;
            this.pnlHistory2.Visible = false;
            this.pnlHistory3.Visible = true;
            this.pnlHistory4.Visible = false;

            this.panel7.Visible = false;
            this.panel6.Visible = true;
            this.panel1.Visible = false;

            this.dgvHistory.Rows.Clear();
            this.dgvHistory2.DataSource = null;
            this.dgvHistory3.Rows.Clear();
            this.dgvHistory4.Rows.Clear();

            if (this.cmbArea.Text != "" && this.cmbProject.Text != "" && this.cmbCollector.Text != "" && txtMode.Text != "")
            {
                string collectorName = this.cmbCollector.Text;

                DateTime begin = this.dtpBegin.Value;//开始时间
                DateTime end = this.dtpEnd.Value;//结束时间
                string database = this.cmbArea.Text + this.cmbProject.Text;//数据库名称
                string collectorname = cmbCollector.Text;//采集器名称
                if (begin < end)
                {
                    string SQL = "";
                    SQL = "select name from sysobjects where name between '" + collectorname + "index" + begin.ToString("yyyyMM") + "' and '" + collectorname + "index" + end.ToString("yyyyMM") + "'";
                    List<string> listName = new List<string>();
                    DataSet ds_Name = dataaccess.GetDataSet(SQL, database);

                   
                    if (ds_Name != null)
                    {
                        DataTable dt_Name = ds_Name.Tables[0];

                        //将所有表格的名称加载到list中
                        for (int i = 0; i < dt_Name.Rows.Count; i++)
                        {
                            listName.Add(dt_Name.Rows[i][0].ToString());
                        }
                        ds_Name.Dispose();
                        dt_Name.Dispose();
                    }
                    if (listName.Count != 0)//找不到对应的表格（找不到要求的时间的表格）
                    {
                        DataTable dtTem = GetTimeSpanData(listName, database, begin, end);
                        if (dtTem != null)
                        {
                            dtHisData.Clear();//加载前清除数据
                            dtHisData = dtTem;//重新加载datatable 此时的数据为经过时间节选过的数据
                            //总页数
                            this.textBox1.Text = CalTotalPageNum(dtHisData.Rows.Count, count).ToString();
                            //当页数
                            this.textBox2.Text = "1";//当前页是第一页
                            //插入数据
                            InsertintoDgv2(this.dgvHistory3, dtHisData, 1, count);//加载第一页数据
                        }
                    }
                    else
                    {
                        this.dgvHistory3.Rows.Clear();
                        MessageBox.Show("数据表格不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                { MessageBox.Show("开始时间大于结束时间！请重新选择时间！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else
            {
                MessageBox.Show("请先将查询对象填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 热泵数据查询 
        private void button1_Click_1(object sender, EventArgs e)
        {

        }
        #endregion

        #region 曲线查询
        private void btnHisSearchOne_Click_1(object sender, EventArgs e)
        {
            try
            {
                DateTime beginOne = this.dtpBeginOne.Value;
                DateTime endOne = this.dtpEndOne.Value;
                if (beginOne < endOne)
                {
                    if (this.cmbCollectPointNameOne.SelectedIndex != -1)
                    {
                        SearchHisData(beginOne, endOne, this.chartHisOne, this.cmbCollectPointNameOne);
                    }
                    else
                    { MessageBox.Show("请选择采集点名称！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                { MessageBox.Show("开始时间大于结束时间！请重新选择时间！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch
            { }
        }
        #endregion

        #region 具体数据显示
        private void ckbHisOne_CheckedChanged_1(object sender, EventArgs e)
        {
            if (ckbHisOne.Checked)
            {
                this.chartHisOne.Series[0].IsValueShownAsLabel = true;
            }
            else
            {
                this.chartHisOne.Series[0].IsValueShownAsLabel = false;
            }
        }
        #endregion


        #region 翻页按钮
        /// <summary>
        /// 热泵翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast2_Click(object sender, EventArgs e)
        {
            try
            {
                int totalPageNum = CalTotalPageNum(dtHisData.Rows.Count, count);
                if (this.textBox4.Text != totalPageNum.ToString())
                {
                    this.dgvHistory4.Rows.Clear();
                    InsertintoDgv3(this.dgvHistory4, dtHisData, totalPageNum, count);//加载当前页的下一页
                    this.textBox4.Text = totalPageNum.ToString();
                }
                else
                { MessageBox.Show("已经是最后一页！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }


            }
            catch
            {
            }
        }

        private void btnLeft2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox4.Text != "1")
                {
                    this.dgvHistory4.Rows.Clear();
                    InsertintoDgv3(this.dgvHistory4, dtHisData, int.Parse(this.textBox4.Text) - 1, count);//加载当前页的下一页
                    this.textBox4.Text = (int.Parse(this.textBox4.Text) - 1).ToString();
                }
                else
                { MessageBox.Show("已经是第一页！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            }
            catch
            { }
        }

        private void btnRight2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox4.Text != CalTotalPageNum(dtHisData.Rows.Count, count).ToString())
                {
                    this.dgvHistory4.Rows.Clear();
                    InsertintoDgv3(this.dgvHistory4, dtHisData, int.Parse(this.textBox4.Text) + 1, count);//加载当前页的下一页
                    this.textBox4.Text = (int.Parse(this.textBox4.Text) + 1).ToString();
                }
                else
                { MessageBox.Show("已经是最后一页！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            }
            catch
            {
            }
        }

        private void btnFirst2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox4.Text != "1")
                {
                    this.dgvHistory4.Rows.Clear();
                    InsertintoDgv3(this.dgvHistory4, dtHisData, 1, count);//加载当前页的下一页
                    this.textBox4.Text = "1";
                }
                else
                { MessageBox.Show("已经是第一页！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            }
            catch
            { }
        }
        /// <summary>
        /// 计量翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast1_Click_1(object sender, EventArgs e)
        {
            try
            {
                int totalPageNum = CalTotalPageNum(dtHisData.Rows.Count, count);
                if (this.textBox2.Text != totalPageNum.ToString())
                {
                    this.dgvHistory3.Rows.Clear();
                    InsertintoDgv(this.dgvHistory3, dtHisData, totalPageNum, count);//加载当前页的下一页
                    this.textBox2.Text = totalPageNum.ToString();
                }
                else
                { MessageBox.Show("已经是最后一页！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }


            }
            catch
            {
            }
        }

        private void btnLeft1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox2.Text != "1")
                {
                    this.dgvHistory3.Rows.Clear();
                    InsertintoDgv(this.dgvHistory3, dtHisData, int.Parse(this.textBox2.Text) - 1, count);//加载当前页的下一页
                    this.textBox2.Text = (int.Parse(this.textBox2.Text) - 1).ToString();
                }
                else
                { MessageBox.Show("已经是第一页！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            }
            catch
            { }
        }

        private void btnRight1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox2.Text != CalTotalPageNum(dtHisData.Rows.Count, count).ToString())
                {
                    this.dgvHistory3.Rows.Clear();
                    InsertintoDgv(this.dgvHistory3, dtHisData, int.Parse(this.textBox2.Text) + 1, count);//加载当前页的下一页
                    this.textBox2.Text = (int.Parse(this.textBox2.Text) + 1).ToString();
                }
                else
                { MessageBox.Show("已经是最后一页！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            }
            catch
            {
            }
        }

        private void btnFirst1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox2.Text != "1")
                {
                    this.dgvHistory3.Rows.Clear();
                    InsertintoDgv(this.dgvHistory3, dtHisData, 1, count);//加载当前页的下一页
                    this.textBox2.Text = "1";
                }
                else
                { MessageBox.Show("已经是第一页！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            }
            catch
            { }
        }
        /// <summary>
        /// 数据翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click_1(object sender, EventArgs e)
        {
            try
            {
                int totalPageNum = CalTotalPageNum(dtHisData.Rows.Count, count);
                if (this.txtHisCurrentPage.Text != totalPageNum.ToString())
                {
                    this.dgvHistory.Rows.Clear();
                    InsertintoDgv(this.dgvHistory, dtHisData, totalPageNum, count);//加载当前页的下一页
                    this.txtHisCurrentPage.Text = totalPageNum.ToString();
                }
                else
                { MessageBox.Show("已经是最后一页！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }


            }
            catch
            {
            }
        }

        private void btnLeft_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (this.txtHisCurrentPage.Text != "1")
                {
                    this.dgvHistory.Rows.Clear();
                    InsertintoDgv(this.dgvHistory, dtHisData, int.Parse(this.txtHisCurrentPage.Text) - 1, count);//加载当前页的下一页
                    this.txtHisCurrentPage.Text = (int.Parse(this.txtHisCurrentPage.Text) - 1).ToString();
                }
                else
                { MessageBox.Show("已经是第一页！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            }
            catch
            { }
        }

        private void btnRight_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (this.txtHisCurrentPage.Text != CalTotalPageNum(dtHisData.Rows.Count, count).ToString())
                {
                    this.dgvHistory.Rows.Clear();
                    InsertintoDgv(this.dgvHistory, dtHisData, int.Parse(this.txtHisCurrentPage.Text) + 1, count);//加载当前页的下一页
                    this.txtHisCurrentPage.Text = (int.Parse(this.txtHisCurrentPage.Text) + 1).ToString();
                }
                else
                { MessageBox.Show("已经是最后一页！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            }
            catch
            {
            }
        }
        
        private void btnFirst_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (this.txtHisCurrentPage.Text != "1")
                {
                    this.dgvHistory.Rows.Clear();
                    InsertintoDgv(this.dgvHistory, dtHisData, 1, count);//加载当前页的下一页
                    this.txtHisCurrentPage.Text = "1";
                }
                else
                { MessageBox.Show("已经是第一页！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            }
            catch
            { }
        }

        #endregion

        private void dgvHistory4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chartHisOne_DoubleClick(object sender, EventArgs e)
        {
            string name = this.cmbCollectPointNameOne.Text + this.dtpBeginOne.Text+"-"+ this.dtpEndOne.Text;
            ExportPicture(name);
        }








    }
}
