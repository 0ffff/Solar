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
    public partial class Form1 : Form
    {
        DataAccess dataaccess = new DataAccess();
        public Form1()
        {
            InitializeComponent();
            GlobalInfo.PortName1 = "COM5";
         
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadArea();
            this.cmbArea.SelectedIndex = 0;
           // LoadZb();
           //this.cmbYear.SelectedIndex = 0;
          
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
                    this.cmbArea.Items.Add(read["Province"].ToString()+read["City"].ToString());
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
        /// 加载运行模式
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
                    if (mode == "1")
                    {
                        this.cmbData.Items.Add("单水箱供热模式");
                        LoadZb();
                    }
                   else if (mode == "2")
                   {
                        this.cmbData.Items.Add("纯热泵干燥模式");
                        LoadZb();
                   }
                   else if (mode == "4")
                   {
                       this.cmbData.Items.Add("复杂热水模式");
                       LoadZb();
                   }
                   else if (mode == "5")
                   {
                       this.cmbData.Items.Add("简单热水模式");
                       LoadZb();
                   }
                  else if (mode =="8")
                  {
                      this.cmbData.Items.Add("光伏制冷系统");
                      LoadZb();
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

        private void LoadZb()
        {
            this.cmbColumnMonthIndex.Items.Clear();
            this.cmbColumn.Items.Clear();
            this.cmbColumnYearIndex.Items.Clear();
            this.cmbColumnHour.Items.Clear();
            string sql = "select CollectorMode from CollectorInfo where AreaName='" + this.cmbArea.Text + "' and ProjectName='" + this.cmbProject.Text + "' and CollectorName='" + this.cmbCollector.Text + "'";
            string mode = dataaccess.ReturnSingleData(sql).ToString();//模式代码
            #region 模式1能耗指标加载
            if (mode == "1")
            {
                string[] tJ = new string[] { "系统累计耗电量", "用户累计供热量", "系统累计节煤量", "二氧化碳减排量", "二氧化硫减排量", "氮氧化物减排量", "粉尘减排量" };
                for (int i = 0; i < tJ.Length; i++)
                {
                    this.cmbColumnHour.Items.Add(tJ[i]);
                    this.cmbColumnMonthIndex.Items.Add(tJ[i]);
                    this.cmbColumn.Items.Add(tJ[i]);
                    this.cmbColumnYearIndex.Items.Add(tJ[i]);
                }
            }
            #endregion

            #region 模式2能耗指标加载
            if (mode == "2")
            {
                string[] tJ = new string[] { "系统实时功率", "系统实时电流", "系统实时电压1", "系统实时电压2", "系统实时电压3"};
                for (int i = 0; i < tJ.Length; i++)
                {
                    this.cmbColumnHour.Items.Add(tJ[i]);
                    this.cmbColumnMonthIndex.Items.Add(tJ[i]);
                    this.cmbColumn.Items.Add(tJ[i]);
                    this.cmbColumnYearIndex.Items.Add(tJ[i]);
                }
            }
            #endregion

            #region 模式4能耗指标加载
            if (mode == "4")
            {
                string[] tJ = new string[] { "系统总耗电量", "用户耗水量", "系统累计耗电量", "累计太阳能有效集热量", "累计热泵供热量", "累计热泵供热量", "累计回水热损", "累计系统供热量", "累计常规能源代替量", "累计名义用户供热量", "系统累计节煤量", "二氧化碳减排量", "二氧化硫减排量", "氮氧化物减排量", "粉尘减排量" };
                for (int i = 0; i < tJ.Length; i++)
                {
                    this.cmbColumnHour.Items.Add(tJ[i]);
                    this.cmbColumnMonthIndex.Items.Add(tJ[i]);
                    this.cmbColumn.Items.Add(tJ[i]);
                    this.cmbColumnYearIndex.Items.Add(tJ[i]);
                }
            }
            #endregion

            #region 模式5能耗指标加载
            if (mode == "5")
            {
                string[] tJ = new string[] { "系统累计总水量", "累计耗电量", "累计名义用户供热量", "系统累计节煤量", "二氧化碳减排量", "二氧化硫减排量", "氮氧化物减排量", "粉尘减排量" };
                for (int i = 0; i < tJ.Length; i++)
                {
                    this.cmbColumnHour.Items.Add(tJ[i]);
                    this.cmbColumnMonthIndex.Items.Add(tJ[i]);
                    this.cmbColumn.Items.Add(tJ[i]);
                    this.cmbColumnYearIndex.Items.Add(tJ[i]);
                }
            }
            #endregion


        }

        

      

        /// <summary>
        /// 判断是否为闰年
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private static bool IsLeap(int year)
        {
            if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获取某年某月的天数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        private int reDay(int year, int month)
        {
            int a = DateTime.DaysInMonth(year, month);
            return a;
        }
        /// <summary>
        /// 日计量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {

            if (cmbArea.Text != "" && cmbProject.Text != "" && cmbCollector.Text != "")
            {
                string sql1 = "select CollectorMode from CollectorInfo where AreaName='" + this.cmbArea.Text + "' and ProjectName='" + this.cmbProject.Text + "' and CollectorName='" + this.cmbCollector.Text + "'";
                string mode = dataaccess.ReturnSingleData(sql1).ToString();//模式代码
                #region 模式1能耗加载
                if (mode =="1")
                {
                    if (this.cmbColumnMonthIndex.SelectedIndex != -1)
                    {
                        if (cmbYear.Text != "" && cmbMonth.Text != "")
                        {
                            //柱状图形态和颜色设置
                            //this.chart1.Series[0]["DrawingStyle"] = "Cylinder";
                            //this.chart1.Series[0].Color = Color.FromArgb(210, 100, 255, 100);

                            //chart1.ChartAreas[0].AxisX.Title = "天数";
                            chart1.Series[0].LabelFormat = "0.00";//设置标签显示的小数点位数
                            chart1.ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
                            chart1.ChartAreas[0].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
                            chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false; ;//讲x坐标不交错设置
                            this.chart1.Series[0].Name = "日计量";//样图
                            foreach (var serice in chart1.Series)//将图表清空
                            {
                                serice.Points.Clear();
                            }
                            chart1.Titles[0].Text = this.cmbColumnMonthIndex.Text;
                            string[] nuM = new string[] { "Esys", "Quse", "Qss", "mCO2", "mSO2", "mNOx", "mfc" };
                            int day = reDay(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.Text));//某年某月有几天
                            double[] data = new double[day];

                            string[] x = new string[day];
                            for (int i = 0; i < day; i++)
                            {
                                x[i] = i + 1 + "日";
                            }



                            string database = this.cmbArea.Text + this.cmbProject.Text;
                            try
                            {
                                if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + cmbYear.Text + cmbMonth.Text, database))
                                {

                                    for (int j = 1; j <= day; j++)
                                    {
                                        string sql = "select " + nuM[cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text)] + " from " + cmbCollector.Text + "Index" + cmbYear.Text + cmbMonth.Text + " where TimeStamp between '" + cmbYear.Text + cmbMonth.Text + j.ToString("00") + " 00:00:00' and '" + cmbYear.Text + cmbMonth.Text + j.ToString("00") + " 24:00:00' order by TimeStamp desc";


                                        DataTable dt = dataaccess.GetDataTable(sql, database);

                                        int num = dt.Rows.Count;
                                        if (num > 0)
                                        {
                                            if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 0 || cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 8)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/m3";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 1)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/kWh";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 2)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/kg";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) / 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) / 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;


                                            }
                                        }
                                        else
                                        { data[j - 1] = 0.00; }




                                    }


                                    //将XY轴绑定
                                    for (int i = 0; i < day; i++)
                                    {
                                        chart1.Series[0].Points.AddXY(x[i], data[i]);
                                    }

                                }
                                else { MessageBox.Show("数据不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                            }
                            catch { }

                        }
                        else { MessageBox.Show("请选择正确的年份和月份！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }


                    }

                    else
                    { MessageBox.Show("请选择统计指标", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
#endregion
                #region 模式2能耗加载
                if (mode == "2")
                {
                    if (this.cmbColumnMonthIndex.SelectedIndex != -1)
                    {
                        if (cmbYear.Text != "" && cmbMonth.Text != "")
                        {
                            //柱状图形态和颜色设置
                            //this.chart1.Series[0]["DrawingStyle"] = "Cylinder";
                            //this.chart1.Series[0].Color = Color.FromArgb(210, 100, 255, 100);

                            //chart1.ChartAreas[0].AxisX.Title = "天数";
                            chart1.Series[0].LabelFormat = "0.00";//设置标签显示的小数点位数
                            chart1.ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
                            chart1.ChartAreas[0].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
                            chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false; ;//讲x坐标不交错设置
                            this.chart1.Series[0].Name = "日计量";//样图
                            foreach (var serice in chart1.Series)//将图表清空
                            {
                                serice.Points.Clear();
                            }
                            chart1.Titles[0].Text = this.cmbColumnMonthIndex.Text;
                            string[] nuM = new string[] { "P", "I", "V1", "V2", "V3"};
                            int day = reDay(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.Text));//某年某月有几天
                            double[] data = new double[day];

                            string[] x = new string[day];
                            for (int i = 0; i < day; i++)
                            {
                                x[i] = i + 1 + "日";
                            }



                            string database = this.cmbArea.Text + this.cmbProject.Text;
                            try
                            {
                                if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + cmbYear.Text + cmbMonth.Text, database))
                                {

                                    for (int j = 1; j <= day; j++)
                                    {
                                        string sql = "select " + nuM[cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text)] + " from " + cmbCollector.Text + "Index" + cmbYear.Text + cmbMonth.Text + " where TimeStamp between '" + cmbYear.Text + cmbMonth.Text + j.ToString("00") + " 00:00:00' and '" + cmbYear.Text + cmbMonth.Text + j.ToString("00") + " 24:00:00' order by TimeStamp desc";


                                        DataTable dt = dataaccess.GetDataTable(sql, database);

                                        int num = dt.Rows.Count;
                                        if (num > 0)
                                        {
                                            if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 0 || cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 8)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/W";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 1)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/A";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 2)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/V";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;
                                                data[j - 1] = result;
                                            }
                                            else
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/V";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;
                                                data[j - 1] = result;


                                            }
                                        }
                                        else
                                        { data[j - 1] = 0.00; }




                                    }


                                    //将XY轴绑定
                                    for (int i = 0; i < day; i++)
                                    {
                                        chart1.Series[0].Points.AddXY(x[i], data[i]);
                                    }

                                }
                                else { MessageBox.Show("数据不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                            }
                            catch { }

                        }
                        else { MessageBox.Show("请选择正确的年份和月份！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }


                    }

                    else
                    { MessageBox.Show("请选择统计指标", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                #endregion
                #region 模式4能耗加载
                if (mode == "4")
                {
                    if (this.cmbColumnMonthIndex.SelectedIndex != -1)
                    {
                        if (cmbYear.Text != "" && cmbMonth.Text != "")
                        {
                            //柱状图形态和颜色设置
                            //this.chart1.Series[0]["DrawingStyle"] = "Cylinder";
                            //this.chart1.Series[0].Color = Color.FromArgb(210, 100, 255, 100);

                            //chart1.ChartAreas[0].AxisX.Title = "天数";
                            chart1.Series[0].LabelFormat = "0.00";//设置标签显示的小数点位数
                            chart1.ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
                            chart1.ChartAreas[0].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
                            chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false; ;//讲x坐标不交错设置
                            this.chart1.Series[0].Name = "日计量";//样图
                            foreach (var serice in chart1.Series)//将图表清空
                            {
                                serice.Points.Clear();
                            }
                            chart1.Titles[0].Text = this.cmbColumnMonthIndex.Text;
                            string[] nuM = new string[] { "Allc", "AllMv", "AllEsys", "AllQc", "AllQhp", "AllQtc", "AllQsh", "AllQuse", "AllQbm", "AllQu", "AllQss", "Allmco2", "Allmso2", "Allmnox", "Allmfc" };
                            int day = reDay(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.Text));//某年某月有几天
                            double[] data = new double[day];

                            string[] x = new string[day];
                            for (int i = 0; i < day; i++)
                            {
                                x[i] = i + 1 + "日";
                            }



                            string database = this.cmbArea.Text + this.cmbProject.Text;
                            try
                            {
                                if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + cmbYear.Text + cmbMonth.Text, database))
                                {

                                    for (int j = 1; j <= day; j++)
                                    {
                                        string sql = "select " + nuM[cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text)] + " from " + cmbCollector.Text + "Index" + cmbYear.Text + cmbMonth.Text + " where TimeStamp between '" + cmbYear.Text + cmbMonth.Text + j.ToString("00") + " 00:00:00' and '" + cmbYear.Text + cmbMonth.Text + j.ToString("00") + " 24:00:00' order by TimeStamp desc";


                                        DataTable dt = dataaccess.GetDataTable(sql, database);

                                        int num = dt.Rows.Count;
                                        if (num > 0)
                                        {
                                            if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 0 || cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 1)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/m3";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 2)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/kWh";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 3)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 4)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 5)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 6)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 7)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 8)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 9)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/kg";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) / 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) / 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;


                                            }
                                        }
                                        else
                                        { data[j - 1] = 0.00; }




                                    }


                                    //将XY轴绑定
                                    for (int i = 0; i < day; i++)
                                    {
                                        chart1.Series[0].Points.AddXY(x[i], data[i]);
                                    }

                                }
                                else { MessageBox.Show("数据不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                            }
                            catch { }

                        }
                        else { MessageBox.Show("请选择正确的年份和月份！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }


                    }

                    else
                    { MessageBox.Show("请选择统计指标", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                #endregion
                #region 模式5能耗加载
                if (mode == "5")
                {
                    if (this.cmbColumnMonthIndex.SelectedIndex != -1)
                    {
                        if (cmbYear.Text != "" && cmbMonth.Text != "")
                        {
                            //柱状图形态和颜色设置
                            //this.chart1.Series[0]["DrawingStyle"] = "Cylinder";
                            //this.chart1.Series[0].Color = Color.FromArgb(210, 100, 255, 100);

                            //chart1.ChartAreas[0].AxisX.Title = "天数";
                            chart1.Series[0].LabelFormat = "0.00";//设置标签显示的小数点位数
                            chart1.ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
                            chart1.ChartAreas[0].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
                            chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false; ;//讲x坐标不交错设置
                            this.chart1.Series[0].Name = "日计量";//样图
                            foreach (var serice in chart1.Series)//将图表清空
                            {
                                serice.Points.Clear();
                            }
                            chart1.Titles[0].Text = this.cmbColumnMonthIndex.Text;
                            string[] nuM = new string[] { "Allc", "AllEsys", "AllQuse", "Qss", "mco2", "mso2", "mnox", "mfc" };
                            int day = reDay(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.Text));//某年某月有几天
                            double[] data = new double[day];

                            string[] x = new string[day];
                            for (int i = 0; i < day; i++)
                            {
                                x[i] = i + 1 + "日";
                            }



                            string database = this.cmbArea.Text + this.cmbProject.Text;
                            try
                            {
                                if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + cmbYear.Text + cmbMonth.Text, database))
                                {

                                    for (int j = 1; j <= day; j++)
                                    {
                                        string sql = "select " + nuM[cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text)] + " from " + cmbCollector.Text + "Index" + cmbYear.Text + cmbMonth.Text + " where TimeStamp between '" + cmbYear.Text + cmbMonth.Text + j.ToString("00") + " 00:00:00' and '" + cmbYear.Text + cmbMonth.Text + j.ToString("00") + " 24:00:00' order by TimeStamp desc";


                                        DataTable dt = dataaccess.GetDataTable(sql, database);

                                        int num = dt.Rows.Count;
                                        if (num > 0)
                                        {
                                            if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 0)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/m3";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 1)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/kWh";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumnMonthIndex.Items.IndexOf(cmbColumnMonthIndex.Text) == 2)
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else
                                            {
                                                chart1.ChartAreas[0].AxisY.Title = "单位/kg";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) / 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) / 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;


                                            }
                                        }
                                        else
                                        { data[j - 1] = 0.00; }




                                    }


                                    //将XY轴绑定
                                    for (int i = 0; i < day; i++)
                                    {
                                        chart1.Series[0].Points.AddXY(x[i], data[i]);
                                    }

                                }
                                else { MessageBox.Show("数据不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                            }
                            catch { }

                        }
                        else { MessageBox.Show("请选择正确的年份和月份！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }


                    }

                    else
                    { MessageBox.Show("请选择统计指标", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                #endregion
            }
            else { MessageBox.Show("请先将查询对象填写完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        /// <summary>
        /// 月统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbArea.Text != "" && cmbProject.Text != "" && cmbCollector.Text != "")
            {
                string sql1 = "select CollectorMode from CollectorInfo where AreaName='" + this.cmbArea.Text + "' and ProjectName='" + this.cmbProject.Text + "' and CollectorName='" + this.cmbCollector.Text + "'";
                string mode = dataaccess.ReturnSingleData(sql1).ToString();//模式代码
                #region 模式1能耗加载
                if (mode == "1")
                {
                    if (this.cmbColumn.SelectedIndex != -1)
                    {
                        if (cmbMyear.Text != "")
                        {
                            //柱状图形态和颜色设置
                            //this.chart1.Series[0]["DrawingStyle"] = "Cylinder";
                            //this.chart2.Series[0].Color = Color.FromArgb(210, 100, 255, 100);

                            //chart1.ChartAreas[0].AxisX.Title = "天数";
                            chart2.Series[0].LabelFormat = "0.00";//设置标签显示的小数点位数
                            chart2.ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
                            chart2.ChartAreas[0].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
                            chart2.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false; ;//讲x坐标不交错设置

                            this.chart2.Series[0].Name = "月计量";//样图
                            foreach (var serice in chart2.Series)//将图表清空
                            {
                                serice.Points.Clear();
                            }
                            chart2.Titles[0].Text = this.cmbColumn.Text;
                            string[] nuM = new string[] { "Esys", "Quse", "Qss", "mCO2", "mSO2", "mNOx", "mfc" };
                            int month = 12;
                            double[] data = new double[month];

                            string[] x = new string[month];
                            for (int i = 0; i < month; i++)
                            {
                                x[i] = i + 1 + "月份";
                            }



                            string database = this.cmbArea.Text + this.cmbProject.Text;
                            for (int j = 1; j <= month; j++)
                            {
                                try
                                {
                                    if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + cmbMyear.Text + j.ToString("00"), database))
                                    {


                                        string sql = "select " + nuM[cmbColumn.Items.IndexOf(cmbColumn.Text)] + " from " + cmbCollector.Text + "Index" + cmbMyear.Text + j.ToString("00") + " order by TimeStamp desc";


                                        DataTable dt = dataaccess.GetDataTable(sql, database);

                                        int num = dt.Rows.Count;
                                        if (num > 0)
                                        {
                                            if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 0 || cmbColumn.Items.IndexOf(cmbColumn.Text) == 8)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/m3";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 1)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/kWh";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 2)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/kg";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) / 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) / 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;


                                            }
                                        }
                                        else { data[j - 1] = 0.00; }

                                    }
                                    else { data[j - 1] = 0.00; }


                                }
                                catch { }
                            }
                            //将XY轴绑定
                            for (int i = 0; i < month; i++)
                            {
                                chart2.Series[0].Points.AddXY(x[i], data[i]);
                            }

                            //this.chart1.Series[0].Points.Add(day);





                        }
                        else { MessageBox.Show("请先将查询年份填写完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }




                    }

                    else
                    { MessageBox.Show("请选择统计指标", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                #endregion
                #region 模式2能耗加载
                if (mode == "2")
                {
                    if (this.cmbColumn.SelectedIndex != -1)
                    {
                        if (cmbMyear.Text != "")
                        {
                            //柱状图形态和颜色设置
                            //this.chart1.Series[0]["DrawingStyle"] = "Cylinder";
                            //this.chart2.Series[0].Color = Color.FromArgb(210, 100, 255, 100);

                            //chart1.ChartAreas[0].AxisX.Title = "天数";
                            chart2.Series[0].LabelFormat = "0.00";//设置标签显示的小数点位数
                            chart2.ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
                            chart2.ChartAreas[0].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
                            chart2.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false; ;//讲x坐标不交错设置

                            this.chart2.Series[0].Name = "月计量";//样图
                            foreach (var serice in chart2.Series)//将图表清空
                            {
                                serice.Points.Clear();
                            }
                            chart2.Titles[0].Text = this.cmbColumn.Text;
                            string[] nuM = new string[] { "P", "I", "V1", "V2", "V3" };
                            int month = 12;
                            double[] data = new double[month];

                            string[] x = new string[month];
                            for (int i = 0; i < month; i++)
                            {
                                x[i] = i + 1 + "月份";
                            }



                            string database = this.cmbArea.Text + this.cmbProject.Text;
                            for (int j = 1; j <= month; j++)
                            {
                                try
                                {
                                    if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + cmbMyear.Text + j.ToString("00"), database))
                                    {


                                        string sql = "select " + nuM[cmbColumn.Items.IndexOf(cmbColumn.Text)] + " from " + cmbCollector.Text + "Index" + cmbMyear.Text + j.ToString("00") + " order by TimeStamp desc";


                                        DataTable dt = dataaccess.GetDataTable(sql, database);

                                        int num = dt.Rows.Count;
                                        if (num > 0)
                                        {
                                            if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 0 || cmbColumn.Items.IndexOf(cmbColumn.Text) == 8)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/W";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 1)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/A";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 2)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/V";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/V";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) / 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) / 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;


                                            }
                                        }
                                        else { data[j - 1] = 0.00; }

                                    }
                                    else { data[j - 1] = 0.00; }


                                }
                                catch { }
                            }
                            //将XY轴绑定
                            for (int i = 0; i < month; i++)
                            {
                                chart2.Series[0].Points.AddXY(x[i], data[i]);
                            }

                            //this.chart1.Series[0].Points.Add(day);





                        }
                        else { MessageBox.Show("请先将查询年份填写完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }




                    }

                    else
                    { MessageBox.Show("请选择统计指标", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                #endregion
                #region 模式4能耗加载
                if (mode == "4")
                {
                    if (this.cmbColumn.SelectedIndex != -1)
                    {
                        if (cmbMyear.Text != "")
                        {
                            //柱状图形态和颜色设置
                            //this.chart1.Series[0]["DrawingStyle"] = "Cylinder";
                            //this.chart2.Series[0].Color = Color.FromArgb(210, 100, 255, 100);

                            //chart1.ChartAreas[0].AxisX.Title = "天数";
                            chart2.Series[0].LabelFormat = "0.00";//设置标签显示的小数点位数
                            chart2.ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
                            chart2.ChartAreas[0].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
                            chart2.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false; ;//讲x坐标不交错设置

                            this.chart2.Series[0].Name = "月计量";//样图
                            foreach (var serice in chart2.Series)//将图表清空
                            {
                                serice.Points.Clear();
                            }
                            chart2.Titles[0].Text = this.cmbColumn.Text;
                            string[] nuM = new string[] { "Allc", "AllMv", "AllEsys", "AllQc", "AllQhp", "AllQtc", "AllQsh", "AllQuse", "AllQbm", "AllQu", "AllQss", "Allmco2", "Allmso2", "Allmnox", "Allmfc" };
                            int month = 12;
                            double[] data = new double[month];

                            string[] x = new string[month];
                            for (int i = 0; i < month; i++)
                            {
                                x[i] = i + 1 + "月份";
                            }



                            string database = this.cmbArea.Text + this.cmbProject.Text;
                            for (int j = 1; j <= month; j++)
                            {
                                try
                                {
                                    if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + cmbMyear.Text + j.ToString("00"), database))
                                    {


                                        string sql = "select " + nuM[cmbColumn.Items.IndexOf(cmbColumn.Text)] + " from " + cmbCollector.Text + "Index" + cmbMyear.Text + j.ToString("00") + " order by TimeStamp desc";


                                        DataTable dt = dataaccess.GetDataTable(sql, database);

                                        int num = dt.Rows.Count;
                                        if (num > 0)
                                        {
                                            if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 0 || cmbColumn.Items.IndexOf(cmbColumn.Text) == 1)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/m3";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 2)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/kWh";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 3)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 4)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 5)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 6)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 7)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 8)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 9)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/kg";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) / 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) / 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;


                                            }
                                        }
                                        else { data[j - 1] = 0.00; }

                                    }
                                    else { data[j - 1] = 0.00; }


                                }
                                catch { }
                            }
                            //将XY轴绑定
                            for (int i = 0; i < month; i++)
                            {
                                chart2.Series[0].Points.AddXY(x[i], data[i]);
                            }

                            //this.chart1.Series[0].Points.Add(day);





                        }
                        else { MessageBox.Show("请先将查询年份填写完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }




                    }

                    else
                    { MessageBox.Show("请选择统计指标", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                #endregion
                #region 模式5能耗加载
                if (mode == "5")
                {
                    if (this.cmbColumn.SelectedIndex != -1)
                    {
                        if (cmbMyear.Text != "")
                        {
                            //柱状图形态和颜色设置
                            //this.chart1.Series[0]["DrawingStyle"] = "Cylinder";
                            //this.chart2.Series[0].Color = Color.FromArgb(210, 100, 255, 100);

                            //chart1.ChartAreas[0].AxisX.Title = "天数";
                            chart2.Series[0].LabelFormat = "0.00";//设置标签显示的小数点位数
                            chart2.ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
                            chart2.ChartAreas[0].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
                            chart2.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false; ;//讲x坐标不交错设置

                            this.chart2.Series[0].Name = "月计量";//样图
                            foreach (var serice in chart2.Series)//将图表清空
                            {
                                serice.Points.Clear();
                            }
                            chart2.Titles[0].Text = this.cmbColumn.Text;
                            string[] nuM = new string[] { "Allc", "AllEsys", "AllQuse", "Qss", "mco2", "mso2", "mnox", "mfc" };
                            int month = 12;
                            double[] data = new double[month];

                            string[] x = new string[month];
                            for (int i = 0; i < month; i++)
                            {
                                x[i] = i + 1 + "月份";
                            }



                            string database = this.cmbArea.Text + this.cmbProject.Text;
                            for (int j = 1; j <= month; j++)
                            {
                                try
                                {
                                    if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + cmbMyear.Text + j.ToString("00"), database))
                                    {


                                        string sql = "select " + nuM[cmbColumn.Items.IndexOf(cmbColumn.Text)] + " from " + cmbCollector.Text + "Index" + cmbMyear.Text + j.ToString("00") + " order by TimeStamp desc";


                                        DataTable dt = dataaccess.GetDataTable(sql, database);

                                        int num = dt.Rows.Count;
                                        if (num > 0)
                                        {
                                            if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 0)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/m3";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 1)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/kWh";
                                                double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else if (cmbColumn.Items.IndexOf(cmbColumn.Text) == 2)
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/kJ";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;
                                            }
                                            else
                                            {
                                                chart2.ChartAreas[0].AxisY.Title = "单位/kg";
                                                double first = Convert.ToDouble(dt.Rows[0][0]) / 1000;//将单位转换成kg
                                                double last = Convert.ToDouble(dt.Rows[num - 1][0]) / 1000;
                                                double result = first - last;

                                                //double dou = double.Parse(result.ToString("0.00"));
                                                //double dou = Math.Round(result, 2);
                                                //string to = string.Format("{0:F2}", result);//取小数点后2位
                                                data[j - 1] = result;


                                            }
                                        }
                                        else { data[j - 1] = 0.00; }

                                    }
                                    else { data[j - 1] = 0.00; }


                                }
                                catch { }
                            }
                            //将XY轴绑定
                            for (int i = 0; i < month; i++)
                            {
                                chart2.Series[0].Points.AddXY(x[i], data[i]);
                            }

                            //this.chart1.Series[0].Points.Add(day);





                        }
                        else { MessageBox.Show("请先将查询年份填写完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }




                    }

                    else
                    { MessageBox.Show("请选择统计指标", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                #endregion

            }
            else { MessageBox.Show("请先将查询对象填写完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        /// <summary>
        /// 年统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            if (cmbArea.Text != "" && cmbProject.Text != "" && cmbCollector.Text != "")
            {
                if (this.cmbColumnYearIndex.SelectedIndex != -1)
                {
                    chart4.Series[0].LabelFormat = "0.00";//设置标签显示的小数点位数
                    chart4.ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
                    chart4.ChartAreas[0].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
                    chart4.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false; ;//讲x坐标不交错设置
                    foreach (var serice in chart4.Series)//将图表清空
                    {
                        serice.Points.Clear();
                    }
                    chart4.Titles[0].Text = this.cmbColumnYearIndex.Text;
                    List<string> yearList = GetExistYear();

                    double[] data = GetYearData(yearList);
                    if (data != null)
                    {
                        this.chart4.Series[0].Name = "年统计量";//样图
                        this.chart4.Titles[0].Text = this.cmbColumnYearIndex.Text;//标题
                        for (int i = 0; i < data.Length; i++)
                        {
                            this.chart4.Series[0].Points.AddXY(yearList[i],data[i]);
                        }
                    }



                }
                else{ MessageBox.Show("请选择统计指标", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else { MessageBox.Show("请先将查询对象填写完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }


        private List<string> GetExistYear()
        {
            try
            {
                List<string> yearList = new List<string>();
                string area = this.cmbArea.Text;//地区名
                string project = this.cmbProject.Text;//项目名称
                string CollectorName = this.cmbCollector.Text;//采集器名称
                string database = area + project ;
                string sql = "select name from sysobjects where name like'" + this.cmbCollector.Text + "Index20%' order by name";//按照年度递增依次加载
                DataSet ds = dataaccess.GetDataSet(sql, database);
                DataTable dt = ds.Tables[0];
                //List<string> yearList = new List<string>();//存放提取的年份
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string year = dt.Rows[i][0].ToString().Substring(this.cmbCollector.Text.Length+5, 4);//加载年份数
                    if (!yearList.Contains(year))
                    {
                        yearList.Add(year);
                    }
                }
                return yearList;
            }
            catch
            { return null; }
        }



        /// <summary>
        /// 获取年的数据
        /// </summary>
        /// <returns>返回每年的统计值</returns>
        private double[] GetYearData(List<string> yearList)
        {
            //1 得到数据库中存在的年份
            //2 根据年份依次加载每年的统计量
            string area = this.cmbArea.Text;//地区名
            string project = this.cmbProject.Text;//项目名称
            string CollectorName = this.cmbCollector.Text;//采集器名称
            string database = area + project;
            //string CollectorPointName = this.cmbColumnYearIndex.Text;
            string sql1 = "select CollectorMode from CollectorInfo where AreaName='" + this.cmbArea.Text + "' and ProjectName='" + this.cmbProject.Text + "' and CollectorName='" + this.cmbCollector.Text + "'";
            string mode = dataaccess.ReturnSingleData(sql1).ToString();//模式代码
            if (mode == "1")
            {
                #region 获取数据
                try
                {
                    int yearCount = yearList.Count;//年份数量
                    string[] nuM = new string[] { "Esys", "Quse", "Qss", "mCO2", "mSO2", "mNOx", "mfc" };
                    double[] back = new double[yearCount];//返回的数值

                    for (int i = 0; i < yearCount; i++)//找到年份
                    {
                        //2 根据年份依次加载每年的统计量
                        //2.1 依次判断该年一月至十二月的表格是否存在
                        //2.2 累加每个月的统计值
                        //2.3 得出最终值
                        double monthColumn = 0.00;//该年下的每个月的累计值

                        for (int j = 1; j <= 12; j++)//找到该年份的数据 i代表月份
                        {
                            if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + yearList[i] + j.ToString("00"), database))//2.1 依次判断该年一月至十二月的表格是否存在
                            {
                                double result = 0;//接受每个月的计量值
                                string sql = "select " + nuM[cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text)] + " from " + cmbCollector.Text + "Index" + yearList[i] + j.ToString("00") + " order by TimeStamp desc";
                                DataTable dt = dataaccess.GetDataTable(sql, database);
                                //DataSet ds_data = dataaccess.GetDataSet(sql, database );
                                //DataTable dt_data = ds_data.Tables[0];
                                //没有该时间段的数据
                                int num = dt.Rows.Count;
                                if (num > 0)
                                {
                                    if (cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 0 || cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 8)
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/m3";
                                        double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                        result = first - last;

                                    }
                                    else if (cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 1)
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/kWh";
                                        double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                        result = first - last;
                                    }
                                    else if (cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 2)
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/kJ";
                                        double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                        result = first - last;

                                    }
                                    else
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/kg";
                                        double first = Convert.ToDouble(dt.Rows[0][0]) / 1000;//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]) / 1000;
                                        result = first - last;


                                    }
                                }
                                else
                                {
                                    result = 0.00;
                                }
                                monthColumn += result;

                            }
                        }
                        back[i] = monthColumn;//2.3 得出最终值
                    }
                    return back;//返回


                }
                catch
                { return null; }
                #endregion
            }
            else if (mode =="2")
            {
                #region 获取数据
                try
                {
                    int yearCount = yearList.Count;//年份数量
                    string[] nuM = new string[] { "P", "I", "V1", "V2", "V3" };
                    double[] back = new double[yearCount];//返回的数值

                    for (int i = 0; i < yearCount; i++)//找到年份
                    {
                        //2 根据年份依次加载每年的统计量
                        //2.1 依次判断该年一月至十二月的表格是否存在
                        //2.2 累加每个月的统计值
                        //2.3 得出最终值
                        double monthColumn = 0.00;//该年下的每个月的累计值

                        for (int j = 1; j <= 12; j++)//找到该年份的数据 i代表月份
                        {
                            if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + yearList[i] + j.ToString("00"), database))//2.1 依次判断该年一月至十二月的表格是否存在
                            {
                                double result = 0;//接受每个月的计量值
                                string sql = "select " + nuM[cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text)] + " from " + cmbCollector.Text + "Index" + yearList[i] + j.ToString("00") + " order by TimeStamp desc";
                                DataTable dt = dataaccess.GetDataTable(sql, database);
                                //DataSet ds_data = dataaccess.GetDataSet(sql, database );
                                //DataTable dt_data = ds_data.Tables[0];
                                //没有该时间段的数据
                                int num = dt.Rows.Count;
                                if (num > 0)
                                {
                                    if (cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 0 || cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 8)
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/W";
                                        double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                        result = first - last;

                                    }
                                    else if (cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 1)
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/A";
                                        double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                        result = first - last;
                                    }
                                    else if (cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 2)
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/V";
                                        double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                        result = first - last;

                                    }
                                    else
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/V";
                                        double first = Convert.ToDouble(dt.Rows[0][0]) / 1000;//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]) / 1000;
                                        result = first - last;


                                    }
                                }
                                else
                                {
                                    result = 0.00;
                                }
                                monthColumn += result;

                            }
                        }
                        back[i] = monthColumn;//2.3 得出最终值
                    }
                    return back;//返回


                }
                catch
                { return null; }
                #endregion
            }
            else if (mode =="4")
            {
                #region 获取数据
                try
                {
                    int yearCount = yearList.Count;//年份数量
                    string[] nuM = new string[] { "Allc", "AllMv", "AllEsys", "AllQc", "AllQhp", "AllQtc", "AllQsh", "AllQuse", "AllQbm", "AllQu", "AllQss", "Allmco2", "Allmso2", "Allmnox", "Allmfc" };
                    double[] back = new double[yearCount];//返回的数值

                    for (int i = 0; i < yearCount; i++)//找到年份
                    {
                        //2 根据年份依次加载每年的统计量
                        //2.1 依次判断该年一月至十二月的表格是否存在
                        //2.2 累加每个月的统计值
                        //2.3 得出最终值
                        double monthColumn = 0.00;//该年下的每个月的累计值

                        for (int j = 1; j <= 12; j++)//找到该年份的数据 i代表月份
                        {
                            if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + yearList[i] + j.ToString("00"), database))//2.1 依次判断该年一月至十二月的表格是否存在
                            {
                                double result = 0;//接受每个月的计量值
                                string sql = "select " + nuM[cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text)] + " from " + cmbCollector.Text + "Index" + yearList[i] + j.ToString("00") + " order by TimeStamp desc";
                                DataTable dt = dataaccess.GetDataTable(sql, database);
                                //DataSet ds_data = dataaccess.GetDataSet(sql, database );
                                //DataTable dt_data = ds_data.Tables[0];
                                //没有该时间段的数据
                                int num = dt.Rows.Count;
                                if (num > 0)
                                {
                                    if (cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 0 || cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 1)
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/m3";
                                        double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                        result = first - last;

                                    }
                                    else if (cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 2)
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/kWh";
                                        double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                        result = first - last;
                                    }
                                    else if (cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 3 || cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 4)
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/kJ";
                                        double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                        result = first - last;

                                    }
                                    else if (cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 5 || cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 6)
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/kJ";
                                        double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                        result = first - last;

                                    }
                                    else if (cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 7 || cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) ==8)
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/kJ";
                                        double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                        result = first - last;

                                    }
                                    else if (cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 9)
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/kJ";
                                        double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                        result = first - last;

                                    }
                                    else
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/kg";
                                        double first = Convert.ToDouble(dt.Rows[0][0]) / 1000;//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]) / 1000;
                                        result = first - last;


                                    }
                                }
                                else
                                {
                                    result = 0.00;
                                }
                                monthColumn += result;

                            }
                        }
                        back[i] = monthColumn;//2.3 得出最终值
                    }
                    return back;//返回


                }
                catch
                { return null; }
                #endregion
            }
            else 
            {
                #region 获取数据
                try
                {
                    int yearCount = yearList.Count;//年份数量
                    string[] nuM = new string[] { "Allc", "AllEsys", "AllQuse", "Qss", "mco2", "mso2", "mnox", "mfc" };
                    double[] back = new double[yearCount];//返回的数值

                    for (int i = 0; i < yearCount; i++)//找到年份
                    {
                        //2 根据年份依次加载每年的统计量
                        //2.1 依次判断该年一月至十二月的表格是否存在
                        //2.2 累加每个月的统计值
                        //2.3 得出最终值
                        double monthColumn = 0.00;//该年下的每个月的累计值

                        for (int j = 1; j <= 12; j++)//找到该年份的数据 i代表月份
                        {
                            if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + yearList[i] + j.ToString("00"), database))//2.1 依次判断该年一月至十二月的表格是否存在
                            {
                                double result = 0;//接受每个月的计量值
                                string sql = "select " + nuM[cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text)] + " from " + cmbCollector.Text + "Index" + yearList[i] + j.ToString("00") + " order by TimeStamp desc";
                                DataTable dt = dataaccess.GetDataTable(sql, database);
                                //DataSet ds_data = dataaccess.GetDataSet(sql, database );
                                //DataTable dt_data = ds_data.Tables[0];
                                //没有该时间段的数据
                                int num = dt.Rows.Count;
                                if (num > 0)
                                {
                                    if (cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 0 )
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/m3";
                                        double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                        result = first - last;

                                    }
                                    else if (cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 1)
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/kWh";
                                        double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                        result = first - last;
                                    }
                                    else if (cmbColumnYearIndex.Items.IndexOf(cmbColumnYearIndex.Text) == 2)
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/kJ";
                                        double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                        result = first - last;

                                    }
                                    else
                                    {
                                        chart4.ChartAreas[0].AxisY.Title = "单位/kg";
                                        double first = Convert.ToDouble(dt.Rows[0][0]) / 1000;//将单位转换成kg
                                        double last = Convert.ToDouble(dt.Rows[num - 1][0]) / 1000;
                                        result = first - last;


                                    }
                                }
                                else
                                {
                                    result = 0.00;
                                }
                                monthColumn += result;

                            }
                        }
                        back[i] = monthColumn;//2.3 得出最终值
                    }
                    return back;//返回


                }
                catch
                { return null; }
                #endregion
            }
        }


        /// <summary>
        /// 时计量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            if (cmbArea.Text != "" && cmbProject.Text != "" && cmbCollector.Text != "")
            {
                if (this.cmbColumnHour.Text != "")
                {
                    chart3.Series[0].LabelFormat = "0.00";//设置标签显示的小数点位数
                    chart3.ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
                    chart3.ChartAreas[0].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
                    chart3.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false; ;//讲x坐标不交错设置
                    this.chart3.Series[0].Name = "时计量";//样图
                    foreach (var serice in chart3.Series)//将图表清空
                    {
                        serice.Points.Clear();
                    }
                    chart3.Titles[0].Text = this.cmbColumnHour.Text;

                    double[] data = GetHourData(this.monthCalendar1.SelectionStart);

                    if (data != null)
                    {
                        for (int i = 0; i < 24; i++)
                        {
                            chart3.Series[0].Points.AddXY(i.ToString()+"时",data[i]);
                        }
                    }
                    else
                    { MessageBox.Show("无该时间数据", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }



                }
                else{ MessageBox.Show("请选择统计指标", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            
            }
            else { MessageBox.Show("请先将查询对象填写完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        /// <summary>
        /// 获取时数据
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>

        private double[] GetHourData(DateTime time)
        {
            
            int year = time.Year;//年
            int month = time.Month;//月
            int day = time.Day;//日
            string database = this.cmbArea.Text + this.cmbProject.Text;
            string sql1 = "select CollectorMode from CollectorInfo where AreaName='" + this.cmbArea.Text + "' and ProjectName='" + this.cmbProject.Text + "' and CollectorName='" + this.cmbCollector.Text + "'";
            string mode = dataaccess.ReturnSingleData(sql1).ToString();//模式代码
            if (mode == "1")
            {
                #region 获取数据
                try
                {
                    double[] back = new double[24];
                    string[] nuM = new string[] { "Esys", "Quse", "Qss", "mCO2", "mSO2", "mNOx", "mfc" };
                    if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + year.ToString() + month.ToString("00"), database))
                    {
                        for (int i = 0; i < 24; i++)
                        {
                            string sql = "select " + nuM[this.cmbColumnHour.Items.IndexOf(this.cmbColumnHour.Text)] + " from " + this.cmbCollector.Text + "Index" + year.ToString() + month.ToString("00") + " where TimeStamp between '" + year.ToString() + month.ToString("00") + day.ToString("00") + " " + i.ToString("00") + ":00:00' and '" + year.ToString() + month.ToString("00") + day.ToString("00") + " " + (i + 1).ToString("00") + ":00:00 ' order by TimeStamp desc";

                            DataTable dt = dataaccess.GetDataTable(sql, database);
                            int num = dt.Rows.Count;
                            if (num > 0)
                            {
                                if (cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 0 || cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 8)
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/m3";
                                    double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                    back[i] = first - last;

                                }
                                else if (cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 1)
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/kWh";
                                    double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                    back[i] = first - last;



                                }
                                else if (cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 2)
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/kJ";
                                    double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                    back[i] = first - last;



                                }
                                else
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/kg";
                                    double first = Convert.ToDouble(dt.Rows[0][0]) / 1000;//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]) / 1000;
                                    back[i] = first - last;

                                }
                            }
                            else
                            {
                                back[i] = 0.00;
                            }
                        }
                        return back;
                    }
                    else { return null; }
                }
                catch { return null; }
                #endregion
            }
            else if (mode =="2")
            {
                #region 获取数据
                try
                {
                    double[] back = new double[24];
                    string[] nuM = new string[] { "P", "I", "V1", "V2", "V3" };
                    if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + year.ToString() + month.ToString("00"), database))
                    {
                        for (int i = 0; i < 24; i++)
                        {
                            string sql = "select " + nuM[this.cmbColumnHour.Items.IndexOf(this.cmbColumnHour.Text)] + " from " + this.cmbCollector.Text + "Index" + year.ToString() + month.ToString("00") + " where TimeStamp between '" + year.ToString() + month.ToString("00") + day.ToString("00") + " " + i.ToString("00") + ":00:00' and '" + year.ToString() + month.ToString("00") + day.ToString("00") + " " + (i + 1).ToString("00") + ":00:00 ' order by TimeStamp desc";

                            DataTable dt = dataaccess.GetDataTable(sql, database);
                            int num = dt.Rows.Count;
                            if (num > 0)
                            {
                                if (cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 0 || cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 8)
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/W";
                                    double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                    back[i] = first - last;

                                }
                                else if (cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 1)
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/A";
                                    double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                    back[i] = first - last;



                                }
                                else if (cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 2)
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/V";
                                    double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                    back[i] = first - last;



                                }
                                else
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/V";
                                    double first = Convert.ToDouble(dt.Rows[0][0]) / 1000;//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]) / 1000;
                                    back[i] = first - last;

                                }
                            }
                            else
                            {
                                back[i] = 0.00;
                            }
                        }
                        return back;
                    }
                    else { return null; }
                }
                catch { return null; }
                #endregion
            }
            else if (mode =="4")
            {
                #region 获取数据
                try
                {
                    double[] back = new double[24];
                    string[] nuM = new string[] { "Allc", "AllMv", "AllEsys", "AllQc", "AllQhp", "AllQtc", "AllQsh", "AllQuse", "AllQbm", "AllQu", "AllQss", "Allmco2", "Allmso2", "Allmnox", "Allmfc" };
                    if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + year.ToString() + month.ToString("00"), database))
                    {
                        for (int i = 0; i < 24; i++)
                        {
                            string sql = "select " + nuM[this.cmbColumnHour.Items.IndexOf(this.cmbColumnHour.Text)] + " from " + this.cmbCollector.Text + "Index" + year.ToString() + month.ToString("00") + " where TimeStamp between '" + year.ToString() + month.ToString("00") + day.ToString("00") + " " + i.ToString("00") + ":00:00' and '" + year.ToString() + month.ToString("00") + day.ToString("00") + " " + (i + 1).ToString("00") + ":00:00 ' order by TimeStamp desc";

                            DataTable dt = dataaccess.GetDataTable(sql, database);
                            int num = dt.Rows.Count;
                            if (num > 0)
                            {
                                if (cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 0 || cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 1)
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/m3";
                                    double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                    back[i] = first - last;

                                }
                                else if (cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 2)
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/kWh";
                                    double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                    back[i] = first - last;



                                }
                                else if (cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 3 || cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 4)
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/kJ";
                                    double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                    back[i] = first - last;



                                }
                                else if (cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 5 || cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 6)
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/kJ";
                                    double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                    back[i] = first - last;



                                }
                                else if (cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 7 || cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 8)
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/kJ";
                                    double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                    back[i] = first - last;



                                }
                                else if (cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 9)
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/kJ";
                                    double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                    back[i] = first - last;



                                }
                                else
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/kg";
                                    double first = Convert.ToDouble(dt.Rows[0][0]) / 1000;//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]) / 1000;
                                    back[i] = first - last;

                                }
                            }
                            else
                            {
                                back[i] = 0.00;
                            }
                        }
                        return back;
                    }
                    else { return null; }
                }
                catch { return null; }
                #endregion
            }
            else
            {
                #region 获取数据
                try
                {
                    double[] back = new double[24];
                    string[] nuM = new string[] { "Allc", "AllEsys", "AllQuse", "Qss", "mco2", "mso2", "mnox", "mfc" };
                    if (dataaccess.IsExistTable(this.cmbCollector.Text + "Index" + year.ToString() + month.ToString("00"), database))
                    {
                        for (int i = 0; i < 24; i++)
                        {
                            string sql = "select " + nuM[this.cmbColumnHour.Items.IndexOf(this.cmbColumnHour.Text)] + " from " + this.cmbCollector.Text + "Index" + year.ToString() + month.ToString("00") + " where TimeStamp between '" + year.ToString() + month.ToString("00") + day.ToString("00") + " " + i.ToString("00") + ":00:00' and '" + year.ToString() + month.ToString("00") + day.ToString("00") + " " + (i + 1).ToString("00") + ":00:00 ' order by TimeStamp desc";

                            DataTable dt = dataaccess.GetDataTable(sql, database);
                            int num = dt.Rows.Count;
                            if (num > 0)
                            {
                                if (cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 0 )
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/m3";
                                    double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                    back[i] = first - last;

                                }
                                else if (cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 1)
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/kWh";
                                    double first = Convert.ToDouble(dt.Rows[0][0]);//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]);
                                    back[i] = first - last;



                                }
                                else if (cmbColumnHour.Items.IndexOf(cmbColumnHour.Text) == 2)
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/kJ";
                                    double first = Convert.ToDouble(dt.Rows[0][0]) * 1000;//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]) * 1000;
                                    back[i] = first - last;



                                }
                                else
                                {
                                    chart3.ChartAreas[0].AxisY.Title = "单位/kg";
                                    double first = Convert.ToDouble(dt.Rows[0][0]) / 1000;//将单位转换成kg
                                    double last = Convert.ToDouble(dt.Rows[num - 1][0]) / 1000;
                                    back[i] = first - last;

                                }
                            }
                            else
                            {
                                back[i] = 0.00;
                            }
                        }
                        return back;
                    }
                    else { return null; }
                }
                catch { return null; }
                #endregion
            }
        }
     
    }

}
