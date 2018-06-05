using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;

namespace 光伏制冷
{ 
    public partial class FormCollectorConfiguration : Form
    {
        DataAccess dataaccess = new DataAccess();

        #region 表格中获取的信息 定义为全局是为了让Form1调用生成树状图

        string ProjectName;//在树状图中选中的项目名称 既该采集器所属的项目名称
        string AreaName;//在树状图中选中的项目名称的父节点（地区名） 既该采集器所属的地区名称
        public string CollectorName;//控制器器名称
        public string CollectorCode;//控制器编码
        public string CollectorPointNum;//采集点数量
        public string CollectorMode;//控制器模式
        public string CollectPointNum;//控制器采集点数量

        #endregion

        public FormCollectorConfiguration(string projectName, string areaName)
        {
            this.ProjectName = projectName;
            this.AreaName = areaName;
            InitializeComponent();
            this.CenterToParent();
        }
        private void FormCollectorConfiguration_Load(object sender, EventArgs e)
        {
            //位于父类中心
            this.CenterToParent();

            //采集器添加显示在树状图选中的项目名称及地区名称
            this.txtProjectName.Text = ProjectName;
            this.txtAreaName.Text = AreaName;

            //采集器删除 加载在该项目中已经存在的采集器名称及编号(数据库操作)
            

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if ((txtCollectorName.Text != "") && (txtCollectorCode.Text != "") &&(cbCollectorMode.Text!="")&&(txtCollectorPointNum.Text!=""))
            {
                //首字母不能是数字 因为表格的首字母不能是数字
                if (!(Regex.IsMatch(txtCollectorName.Text, @"^[0-9]")))
                {
                    if (DialogResult.OK == MessageBox.Show("确认要添加该控制器？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                    {

                        //if (txtPassword1.Text == txtPassword2.Text)
                       // {
                            //0 先检查是否已经存在该控制器
                            //1 向CollectorInfo数据库表格中插入信息
                            //2 向新建的数据库中新建表格（表格的列数由采集点数量决定）还和技术类型有关（计算参数不同）！ 待定！！！！
                            //3 向树状图中添加该采集器
                            CollectorName = txtCollectorName.Text;
                            CollectorCode = txtCollectorCode.Text.Trim();
                            CollectorPointNum = txtCollectorPointNum.Text.Trim();
                            
                            //string username = this.txtWebUserName.Text;
                            //string password = this.txtPassword1.Text;


                            switch (cbCollectorMode.Text)
                            {
                                case "单水箱供热系统":
                                    CollectorMode = "1";
                                    break;

                                case "纯热泵干燥系统":
                                    CollectorMode = "2";
                                    break;

                                case "气象信息":
                                    CollectorMode = "3";
                                    break;

                                case "复杂热水系统":
                                    CollectorMode = "4";
                                    break;

                                case "简单热水系统":
                                    CollectorMode = "5";
                                    break;
                                //光伏制冷系统->增加
                                case "光伏制冷系统":
                                    CollectorMode ="8";
                                    break;
                                default:
                                    break;
                            }
                            try
                            {
                                //0 先检查是否已经存在该采集器(判断在该地区该项目名称下 2 采集器名称 3 采集器编号)
                                string SQL_IsExist1 = @"select count(*) from CollectorInfo where AreaName='" + AreaName + "' and  ProjectName='" + ProjectName + "' and CollectorName='" + CollectorName + "'";//判断采集器名称
                                string SQL_IsExist2 = @"select count(*) from CollectorInfo where AreaName='" + AreaName + "' and  ProjectName='" + ProjectName + "' and CollectorCode='" + CollectorCode + "'";//判断采集器编码
                                //string SQL_IsExist2 = @"select count(*) from CollectorInfo where CollectorName='" + CollectorName + "'";
                                if ((dataaccess.IsExistColletorOrPoint(SQL_IsExist1) == false))//判断采集器名称是否存在
                                {
                                    if ((dataaccess.IsExistColletorOrPoint(SQL_IsExist2) == false))//判断采集器编码是否存在
                                    {
                                        //1 向默认数据库CollectorInfo表格中插入信息
                                        string SQL_InsertCollectorInfo = @"insert into CollectorInfo(AreaName,ProjectName,CollectorName,CollectorCode,CollectorMode,CollectPointNum)values('" + AreaName + "','" + ProjectName + "','" + CollectorName + "','" + CollectorCode + "','" + CollectorMode + "','" + CollectorPointNum + "')";
                                        if (0 == dataaccess.ExeSQL(SQL_InsertCollectorInfo, GlobalInfo.DefaultDatabase))
                                        {
                                            string year = DateTime.Now.Year.ToString();
                                            string month = DateTime.Now.Month.ToString("00");
                                            string database = AreaName + ProjectName;//数据库名称

                                            string tablename = "TimeStamp varchar(50),";

                                            //生成sql语句
                                            for (int i = 1; i <= Convert.ToInt32(CollectorPointNum); i++)
                                            {
                                                tablename += "Num" + i + " varchar(50)" + ",";
                                            }
                                            tablename = tablename.Substring(0, tablename.Length - 1);//去掉最后一个逗号


                                            ////插入实时数据表(根据采集点数量建表)
                                            //string sql_jcRealTimeData10 = " create table  " + CollectorName + "  (" + tablename + ") ";
                                            //dataaccess.ExeSQL(sql_jcRealTimeData10, database);
                                            ////插入历史数据表(根据采集点数量建表)
                                            //string sql_jcHistoryData10 = "create table " + CollectorName + year + month + " (" + tablename + ")  ";
                                            //dataaccess.ExeSQL(sql_jcHistoryData10, database);


                                            //声明变量
                                            string strategyNum = "";
                                            string canshuNum = "";
                                            string sql_celue = "";
                                            string sql_canshu = "";
                                            string sql_Point="";
                                            string sql_metering = "";
                                            string sql_data = "";
                                            string sql_pump = "";

                                            int num = 0;
                                            int count = 0;
                                            string pumpNum = "";
                                            string shujuNum = "";
                                            string history_data = "";
                                            string meterNum = "";
                                            string ss = "";

                                            switch (CollectorMode)//插入策略参数表，因为不同模式的参数策略不同(写死的)
                                            {
                                                case "8"://光伏制冷系统
                                                    #region 光伏制冷系统

                                                    //建立设置参数表[时间戳 -> 设定温度 -> 制冷压缩机p,i,d -> 水泵频率p,i,d -> 开关量]
                                                    sql_canshu = "create table " + CollectorName + "参数  (TimeStamp varchar(50),设定温度 varchar(50),水泵频率P varchar(50),水泵频率I varchar(50),水泵频率D varchar(50),进水电磁阀开关 varchar(50),出水电磁阀开关 varchar(50),市电光伏开关 varchar(50),控制柜手动开关 varchar(50))";
                                                    dataaccess.ExeSQL(sql_canshu, database);

                                                    //插入到采集点表
                                                    string[] collectorPointName = { "环境温度", "太阳能辐照度", "风速", "湿度", "光伏输出直流电压", "光伏输出直流电流","光伏输出直流功率", "光伏输出电能","压缩机ab电压", "压缩机bc电压","压缩机ca电压","压缩机a相电流","压缩机b相电流","压缩机c相电流","压缩机视在功率","压缩机有功功率","压缩机无功功率","压缩机功率因素",
                                                                                    "压缩机电能","a相畸变电压", "a相畸变电流","a相电压不平衡度","a相电流不平衡度",  "压缩机排气压力", "蒸发器出口压力", "压缩机排气温度", "冷凝器出口温度", "蒸发器出口温度", 
                                                                                    "蒸发器入口温度", "水泵变频运行电压", "水泵变频运行电流", "水泵变频运行功率","水泵变频运行频率","水泵电能",  "供冷冷水质量流量","冰块温度","水箱上层水温","供冷循环中回水温度",
                                                                                    "水箱下层水温","供冷循环中供水温度","风机盘管出风口温度","房间温度","溢水量","水箱液位","逆控率","能效比","PID占空比","太阳o光伏","总电能","压缩机频率"};

                                                    for (int i = 0; i < collectorPointName.Length; i++)
                                                    {
                                                        int j = i + 1;
                                                        sql_Point = "insert into CollectPointInfo (AreaName,ProjectName,CollectorName,CollectPointName,CollectPointCode)values('" + AreaName + "','" + ProjectName + "','" + CollectorName + "','" + collectorPointName[i] + "','" + j.ToString("00") +"')";
                                                        dataaccess.ExeSQL(sql_Point, GlobalInfo.DefaultDatabase);
                                                    }


                                                    //创建数据表
                                                    num = collectorPointName.Length;//表示数据表的数据个数长度
                                                    shujuNum = "TimeStamp varchar(50),";
                                                    for (int i = 0; i < num; i++)
                                                    {
                                                        shujuNum += collectorPointName[i] + " varchar(50),";
                                                    }
                                                    shujuNum = shujuNum.Substring(0, shujuNum.Length - 1);
                                                    sql_data = "create table " + CollectorName +"实时 (" + shujuNum + " )";
                                                    dataaccess.ExeSQL(sql_data, database);

                                                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                    string sqlInit = "insert into " + CollectorName + "实时 values ( '" + time + "','1','2','3','4','5','6','7','8','9','10','11','12','13','14','15','16','17','18','19','20','21','22','23','24','25','26','27','28','29','30','31','32','33','34','35','36','37','38','39','40','41','42','43','44','45','46','47','48','49','50')";
                                                    dataaccess.ExeSQL(sqlInit, database);

                                                    //创建当月历史数据表格
                                                    history_data = "create table " + CollectorName +"实时"+ DateTime.Now.Year + DateTime.Now.Month.ToString("00") + " (" + shujuNum + " )";
                                                    string sqlInithis = "insert into " + CollectorName + "实时" + DateTime.Now.Year + DateTime.Now.Month.ToString("00") + "  values ( '" + time + "','1','2','3','4','5','6','7','8','9','10','11','12','13','14','15','16','17','18','19','20','21','22','23','24','25','26','27','28','29','30','31','32','33','34','35','36','37','38','39','40','41','42','43','44','45','46','47','48','49','50')";
                                                    dataaccess.ExeSQL(history_data, database);
                                                    dataaccess.ExeSQL(sqlInithis,database);

                                                    #endregion
                                                    break;
                                                   
                                                default:
                                                    break;
                                            }


                                            GlobalInfo.IsNewCollector = true;
                                            MessageBox.Show("添加控制器成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            this.Dispose();

                                        }
                                        //向默认数据库CollectorInfo表格中插入信息操作错误
                                        else
                                        { }
                                    }

                                    else
                                    { MessageBox.Show("该采集器编码已经存在！请确认后重新添加！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                                }
                                else
                                { MessageBox.Show("该采集器名称已经存在！请确认后重新添加！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                            }
                            //数据库操作错误
                            catch
                            { }

                       // }
                       // else
                       // { MessageBox.Show("两次输入的密码不同！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

                    }
                }
                else
                { MessageBox.Show("采集器名称不能以数字作为首字符！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else
            { MessageBox.Show("信息填写不完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        /// <summary>
        /// 模式改变时，自己匹配采集点数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCollectorMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mode = this.cbCollectorMode.SelectedItem.ToString();
            switch(mode)
            {
                case "单水箱供热系统":
                    this.txtCollectorPointNum.Text="18";
                    break;
                case "纯热泵干燥系统":
                    this.txtCollectorPointNum.Text = "10";
                    break;
                case "气象信息":
                    this.txtCollectorPointNum.Text = "24";
                    break;
                case "光伏制冷系统":

                    this.txtCollectorPointNum.Text="50";
                    break;
                default:
                    break; 

            }
        }
    }
}
