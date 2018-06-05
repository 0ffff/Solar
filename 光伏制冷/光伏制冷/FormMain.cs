using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Threading;
using System.Configuration;
using System.Collections;
using System.IO.Ports;


namespace 光伏制冷
{
    public partial class FormMain : Form
    {
        //数据库函数
        DataAccess dataaccess = new DataAccess();
        List<string> listAlarm = new List<string>();//存放MX100报警信息
        List<string> listAlarm2 = new List<string>();//存放其他仪表报警信息

        //网络参数
        private IPAddress localAddress;//本地IP
        int port = 4007;//监听端口号

        //选中树状图时的全局变量
        StringBuilder collectorAreaName = new StringBuilder("");//选中采集器时的全局变量 变成全局变量是因为这几个变量使用频繁 为了减少垃圾而设置成全局的stringBuilder
        StringBuilder collectorProjectName = new StringBuilder("");
        StringBuilder collectorCollectorName = new StringBuilder("");
        StringBuilder collectorCollectorMode = new StringBuilder("");//选中控制器的模式

        FormEntry enter;

        //MX100设备符号
        public int comm;

        //编码转换
        Encoding enc = Encoding.ASCII;

        //MX100通道信息
        double[] chInfo = new double[26];

        //MX100错误信息
        int errorInfo;

        //设置温度参数标志
        bool setValueFlag = false;

        //改变开关量标志
        bool openX1Flag = false;
        bool closeX1Flag = false;
        bool openX2Flag = false;
        bool closeX2Flag = false;
        bool sunEnergyFlag = false;
        bool redEnergyFlag = false;
        
        //参数改变标志
        bool canshuChangeFlag = false;

        DateTime openX1Time = DateTime.Now;
        DateTime closeX1Time = DateTime.Now;
        DateTime openX2Time = DateTime.Now;
        DateTime closeX2Time = DateTime.Now;

        //默认时间间隔
        int timeSpan = 1;//代表10分钟
        int timeSpano = 1;

        //控制柜手动开关状态
        int handSwitchStatus;

        //容器当前液位
        const double upperLimit = 2.907;
        
        //线程声明
        Thread thread_ListenClient;

        //关闭线程用
        bool closeThead = false;

        //获取初始电能
        double startSumPAC = 0;//总电表电能
        double startDCPAC = 0;//直流表电能
        double startSinglePAC = 0;//单相表电能
        double startPressPAC = 0;//压缩机电能   

        #region FormMain构造函数

        public FormMain(FormEntry ff)
        {
            InitializeComponent();

            this.CenterToScreen();
            //提取IPv4地址
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in ips)
            {
                byte[] bytes = ip.GetAddressBytes();
                if (bytes.Length == 4)
                {
                    localAddress = ip;
                }
            }
            GlobalInfo.port = port;//配置监听端口号
            this.enter = ff;
            
        }

        #endregion

        #region FormMain_Load加载函数

        /// <summary>
        /// 事件句柄添加到FormMain的Load函数（第一次加载FormMain窗体时执行）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            GlobalInfo.IsGetData = false;//表示刚开始未接到过数据
            //模式选择
            if (GlobalInfo.UserType == "normal")
            {
                this.toolSBAuthority.Enabled = false;

            }
            //自已绘制,TreeView失去焦点选中节点仍突显
            treeView1.HideSelection = false;
            this.treeView1.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.treeView1.DrawNode += new DrawTreeNodeEventHandler(treeView1_DrawNode);

            //界面显示初始化
            this.tabControl.SelectedTab = 项目信息tabPage;

            //加载树状图
            LoadTreeView();

            treeView1.ExpandAll();
            //加载数据中心信息(端口号 IP)
            AddListView();

            //加载配置文件
            LoadXML();

            //加载报警信息表
            LoadAlarm();
            LoadDanAlarm();

            //设置报警灯为正常运行颜色
            this.led1.BackColor = Color.Lime;
            this.led1.Text = "无报警";

            //用户权限
            if (GlobalInfo.UserType == "super")
            {
                this.toolStripStatusLabelUserType.Text = "超级用户";

                //***********写权限内容
            }
            else
            {
                this.toolStripStatusLabelUserType.Text = "普通用户";

                //***********写权限内容
            }
            //测试
            flashData("云南省昆明市", "光伏制冷系统", "系统采集器", "8");
            


        }

        #endregion

        #region TreeView失去焦点时选中节点仍然突显
        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true; //我这里用默认颜色即可，只需要在TreeView失去焦点时选中节点仍然突显

            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                //演示为绿底白字
                e.Graphics.FillRectangle(Brushes.Green, e.Node.Bounds);
                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = ((TreeView)sender).Font;
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.White, Rectangle.Inflate(e.Bounds, 2, 0));
            }
            else
            {
                e.DrawDefault = true;
            }


            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                using (Pen focusPen = new Pen(Color.Black))
                {
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    Rectangle focusBounds = e.Node.Bounds;
                    focusBounds.Size = new Size(focusBounds.Width - 1,
                    focusBounds.Height - 1);
                    e.Graphics.DrawRectangle(focusPen, focusBounds);
                }
            }
        }
        #endregion

        #region 加载xml文件
        /// <summary>
        /// 加载xml文件(密钥文件，发送指令的XML模板)
        /// </summary>
        private void LoadXML()
        {
            try
            {

                string path1 = "keyInfo.xml";
                XmlDocument xd = new XmlDocument();
                xd.Load(path1);
                XmlNode root = xd.DocumentElement;
                GlobalInfo.MD5 = root.SelectSingleNode("MD5").InnerText;//获得MD5密钥
                GlobalInfo.Key = root.SelectSingleNode("key").InnerText; //获得AES密钥
                GlobalInfo.IV = root.SelectSingleNode("IV").InnerText;//获得AES初始量

                string path2 = "id_validate.xml";
                xd.Load(path2);
                GlobalInfo.id_validate = xd.OuterXml;

                string path3 = "stand.xml";
                xd.Load(path3);
                GlobalInfo.stand = xd.OuterXml;

                string path4 = "heart_beat.xml";
                xd.Load(path4);
                GlobalInfo.heart_beat = xd.OuterXml;

                string path5 = "data.xml";
                xd.Load(path5);
                GlobalInfo.data = xd.OuterXml;

                string path6 = "setkey.xml";
                xd.Load(path6);
                GlobalInfo.setkey = xd.OuterXml;
            }
            catch
            { }
        }
        #endregion

        #region 权限管理
        /// <summary>
        /// toolstripButton 权限管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolSBAuthority_Click(object sender, EventArgs e)
        {
            FormAuthority fa = new FormAuthority();
            fa.ShowDialog();
        }
        #endregion

        #region 单双水箱报警信息
        /// <summary>
        /// 加载错误信息报警信息
        /// </summary>
        private void LoadAlarm()
        {
            listAlarm.Add("水箱水位溢出");
            listAlarm.Add("压缩机压力异常");
            listAlarm.Add("水流量异常");
            listAlarm.Add("压缩机出口温度过高");
            listAlarm.Add("光伏输出电压异常");
            listAlarm.Add("光伏输出频率异常");
            listAlarm.Add("供水温度过低");
            listAlarm.Add("压缩机电流过大");
            listAlarm.Add("水泵电流过大");

        }


        private void LoadDanAlarm()
        {
            listAlarm2.Add("集热器温度传感器T1故障");
            listAlarm2.Add("集热器温度T1过高");
            listAlarm2.Add("集热器温度T1过低");
            listAlarm2.Add("环境温度传感器故障");
            listAlarm2.Add("环境温度过T4过低");
            listAlarm2.Add("水箱温度传感器T2故障");
            listAlarm2.Add("水箱温度传感器T2过高");
            listAlarm2.Add("水箱液位传感器L1故障");
            listAlarm2.Add("水箱液位传感器L1过高");
            listAlarm2.Add("水箱液位传感器L1过低");
            listAlarm2.Add("回水管温度传感器T3故障");
            listAlarm2.Add("回水管温度传感器过低");

            listAlarm2.Add("集热水箱满液位溢出");
            listAlarm2.Add("北京时间错误");

            listAlarm2.Add("集热循环靶流开关故障");
            listAlarm2.Add("供水靶流开关故障");
            listAlarm2.Add("回水靶流开关故障");



        }
        #endregion

        #region 加载树状图
        /// <summary>
        /// 加载树状图
        /// </summary>
        private void LoadTreeView()
        {
            //0 AreaInfo中找到省市添加
            //1 ProjectInfo中找到项目的名称 并添加节点
            //2 CollectorInfo中找到各个项目中的采集器名称 并依次加载至项目节点下
            try
            {
                string SQL_AreaInfo = @"select distinct Province Code from AreaInfo order by Code asc";//找到不同的省份
                DataSet ds_AreaInfo = dataaccess.GetDataSet(SQL_AreaInfo, GlobalInfo.DefaultDatabase);
                DataTable dt_AreaInfo = ds_AreaInfo.Tables[0];
                if (dt_AreaInfo != null)
                {
                    int ProvinceNum = dt_AreaInfo.Rows.Count;//已经添加的省份数量
                    for (int k = 0; k < ProvinceNum; k++)
                    {
                        string Province = dt_AreaInfo.Rows[k][0].ToString();
                        TreeNode provinceNode = new TreeNode(Province);
                        provinceNode.ImageIndex = 4;
                        provinceNode.SelectedImageIndex = 4;
                        treeView1.Nodes[0].Nodes.Add(provinceNode);//向根节点添加省份

                        string SQL_AreaInfo1 = @"select City from AreaInfo where Province='" + Province + "' order by Code asc";//找到不同的市
                        DataSet ds_City = dataaccess.GetDataSet(SQL_AreaInfo1, GlobalInfo.DefaultDatabase);
                        DataTable dt_City = ds_City.Tables[0];
                        if (dt_City != null)
                        {
                            int cityNum = dt_City.Rows.Count;//已经添加的市数量
                            for (int c = 0; c < cityNum; c++)
                            {
                                string City = dt_City.Rows[c][0].ToString();
                                TreeNode cityNode = new TreeNode(City);
                                cityNode.ImageIndex = 2;
                                cityNode.SelectedImageIndex = 2;
                                provinceNode.Nodes.Add(cityNode);

                                string AreaName = Province + City;
                                string SQL_SelectProjectInfo = @"select ProjectName from ProjectInfo where AreaName='" + AreaName + "' order by ProjectCode asc";
                                DataSet ds_ProjectInfo = dataaccess.GetDataSet(SQL_SelectProjectInfo, GlobalInfo.DefaultDatabase);
                                DataTable dt_ProjectInfo = ds_ProjectInfo.Tables[0];
                                if (dt_ProjectInfo != null)
                                {
                                    int ProjectCount = dt_ProjectInfo.Rows.Count;//已经添加的项目数量
                                    for (int i = 0; i < ProjectCount; i++)
                                    {

                                        string ProjectName = dt_ProjectInfo.Rows[i][0].ToString();//dt_ProjectInfo.Rows[i][2].ToString()代表项目的名称列
                                        TreeNode ProjectNode = new TreeNode(ProjectName);

                                        string ProjectNodeName = AreaName + ProjectName;
                                        ProjectNode.Name = ProjectNodeName;
                                        ProjectNode.ImageIndex = 0;
                                        ProjectNode.SelectedImageIndex = 0;
                                        cityNode.Nodes.Add(ProjectNode);//向城市节点添加项目节点

                                        string SQL_SelectCollectorInfo = @"select CollectorName from CollectorInfo where ProjectName='" + ProjectName + "' and AreaName='" + AreaName + "' order by CollectorCode asc";
                                        DataSet ds_CollectorInfo = dataaccess.GetDataSet(SQL_SelectCollectorInfo, GlobalInfo.DefaultDatabase);

                                        DataTable dt_CollectorInfo = ds_CollectorInfo.Tables[0];
                                        if (dt_CollectorInfo != null)
                                        {
                                            int CollectorCount = dt_CollectorInfo.Rows.Count;//已经添加的该项目的采集器数量
                                            if (CollectorCount > 0)//有采集器则向项目节点中添加
                                            {
                                                for (int j = 0; j < CollectorCount; j++)
                                                {
                                                    string CollectorName = dt_CollectorInfo.Rows[j][0].ToString();
                                                    TreeNode CollectorNode = new TreeNode(CollectorName);
                                                    CollectorNode.ImageIndex = 3;
                                                    CollectorNode.SelectedImageIndex = 3;
                                                    ProjectNode.Nodes.Add(CollectorNode);
                                                }
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            { }
        }
        #endregion

        #region  改变选择树状图节点后触发
        /// <summary>
        /// 改变选择树状图节点后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                #region 地区
                if (e.Node.SelectedImageIndex == 5)//选中的是地区
                {

                    this.添加地区ToolStripMenuItem.Enabled = true;//添加地区
                    this.删除地区ToolStripMenuItem.Enabled = false;//删除地区
                    this.添加项目ToolStripMenuItem.Enabled = false;
                    this.删除项目ToolStripMenuItem.Enabled = false;
                    this.添加控制器ToolStripMenuItem.Enabled = false;
                    this.禁用控制器toolStripMenuItem1.Enabled = false;
                    this.启动控制器toolStripMenuItem2.Enabled = false;
                    this.删除控制器ToolStripMenuItem.Enabled = false;




                    //标签页显示项目信息
                    this.tabControl.SelectedTab = 项目信息tabPage;

                }
                #endregion

                #region 省份
                if (e.Node.SelectedImageIndex == 4)//选中的是省
                {

                    this.添加地区ToolStripMenuItem.Enabled = false;//添加地区
                    this.删除地区ToolStripMenuItem.Enabled = true;//删除地区
                    this.添加项目ToolStripMenuItem.Enabled = false;
                    this.删除项目ToolStripMenuItem.Enabled = false;
                    this.添加控制器ToolStripMenuItem.Enabled = false;
                    this.禁用控制器toolStripMenuItem1.Enabled = false;
                    this.启动控制器toolStripMenuItem2.Enabled = false;
                    this.删除控制器ToolStripMenuItem.Enabled = false;

                    //标签页显示项目信息
                    this.tabControl.SelectedTab = 项目信息tabPage;


                }
                #endregion

                #region 城市
                if (e.Node.SelectedImageIndex == 2)//选中的是城市
                {


                    this.添加地区ToolStripMenuItem.Enabled = false;//添加地区
                    this.删除地区ToolStripMenuItem.Enabled = false;//删除地区
                    this.添加项目ToolStripMenuItem.Enabled = true;
                    this.删除项目ToolStripMenuItem.Enabled = false;
                    this.添加控制器ToolStripMenuItem.Enabled = false;
                    this.禁用控制器toolStripMenuItem1.Enabled = false;
                    this.启动控制器toolStripMenuItem2.Enabled = false;
                    this.删除控制器ToolStripMenuItem.Enabled = false;




                    //标签页显示项目信息
                    this.tabControl.SelectedTab = 项目信息tabPage;

                }
                #endregion

                #region 项目
                if ((e.Node.SelectedImageIndex == 0) || (e.Node.SelectedImageIndex == 1))//选中的是项目
                {


                    this.添加地区ToolStripMenuItem.Enabled = false;//添加地区
                    this.删除地区ToolStripMenuItem.Enabled = false;//删除地区
                    this.添加项目ToolStripMenuItem.Enabled = false;
                    this.删除项目ToolStripMenuItem.Enabled = true;
                    this.添加控制器ToolStripMenuItem.Enabled = true;
                    this.禁用控制器toolStripMenuItem1.Enabled = false;
                    this.启动控制器toolStripMenuItem2.Enabled = false;
                    this.删除控制器ToolStripMenuItem.Enabled = false;


                    //标签页显示项目信息
                    //this.tabControl.SelectedTab = 项目信息tabPage;

                    //1 根据项目名称从数据库加载该项目详情
                    string AreaName = treeView1.SelectedNode.Parent.Parent.Text + treeView1.SelectedNode.Parent.Text;
                    string ProjectName = treeView1.SelectedNode.Text;
                    string database = AreaName + ProjectName;
                    try
                    {
                    }
                    catch
                    { }
                }



                #endregion

                #region 控制器
                if (e.Node.SelectedImageIndex == 3)//选中的是控制器
                {
                    //选中的控制器 地区  项目 控制器名称存入全局变量
                    collectorAreaName.Remove(0, collectorAreaName.Length);
                    collectorAreaName.Append(treeView1.SelectedNode.Parent.Parent.Parent.Text + treeView1.SelectedNode.Parent.Parent.Text);
                    collectorProjectName.Remove(0, collectorProjectName.Length);
                    collectorProjectName.Append(treeView1.SelectedNode.Parent.Text);
                    collectorCollectorName.Remove(0, collectorCollectorName.Length);
                    collectorCollectorName.Append(treeView1.SelectedNode.Text);

                    try
                    {
                        //加载项目信息
                        string SQL_SelectProjectInfo = "select * from ProjectInfo where AreaName='" + collectorAreaName.ToString() + "' and ProjectName='" + collectorProjectName.ToString() + "'";
                        DataTable dt_SelectProjectInfo = dataaccess.GetDataTable(SQL_SelectProjectInfo, GlobalInfo.DefaultDatabase);
                        this.txtAreaName.Text = dt_SelectProjectInfo.Rows[0][0].ToString();//项目所在地
                        this.txtAreaCode.Text = dt_SelectProjectInfo.Rows[0][1].ToString();//地区代码
                        this.txtProjectName.Text = dt_SelectProjectInfo.Rows[0][2].ToString();//项目名称
                        this.txtProjectCode.Text = dt_SelectProjectInfo.Rows[0][3].ToString();//项目编码
                        this.txtTectype.Text = dt_SelectProjectInfo.Rows[0][4].ToString();//技术名称
                        this.txtTecCode.Text = dt_SelectProjectInfo.Rows[0][5].ToString();//技术代码

                        //加载项目详情
                        string SQL_SelectProjectDetailInfo = @"select ProjectAddress,ApplicationCompany,XAxis,YAxis,HeatingArea1,ProjectDescription from ProjectDetailInfo where AreaName='" + collectorAreaName.ToString() + "' and ProjectName='" + collectorProjectName.ToString() + "'";
                        DataTable dt_ProjectDetailInfo = dataaccess.GetDataTable(SQL_SelectProjectDetailInfo, GlobalInfo.DefaultDatabase);
                        this.txtProjectAddress.Text = dt_ProjectDetailInfo.Rows[0][0].ToString();//项目地址
                        this.txtCompanyName.Text = dt_ProjectDetailInfo.Rows[0][1].ToString();//公司名称
                        this.txtLongtitude.Text = dt_ProjectDetailInfo.Rows[0][2].ToString();//经度
                        this.txtLatitude.Text = dt_ProjectDetailInfo.Rows[0][3].ToString();//维度
                        this.txtHeatArea.Text = dt_ProjectDetailInfo.Rows[0][4].ToString();//集热面积
                        this.txtDescribe.Text = dt_ProjectDetailInfo.Rows[0][5].ToString();//详情

                    }

                    catch { }


                    //把控制器模式存入全局变量
                    string sql = "select CollectorMode from CollectorInfo where CollectorName='" + collectorCollectorName + "' and ProjectName='" + collectorProjectName + "'";
                    DataTable dt = dataaccess.GetDataTable(sql, GlobalInfo.DefaultDatabase);
                    collectorCollectorMode.Remove(0, collectorCollectorMode.Length);
                    collectorCollectorMode.Append(dt.Rows[0][0].ToString());

                    this.添加地区ToolStripMenuItem.Enabled = false;//添加地区
                    this.删除地区ToolStripMenuItem.Enabled = false;//删除地区
                    this.添加项目ToolStripMenuItem.Enabled = false;
                    this.删除项目ToolStripMenuItem.Enabled = true;
                    this.添加控制器ToolStripMenuItem.Enabled = false;
                    this.禁用控制器toolStripMenuItem1.Enabled = true;
                    this.启动控制器toolStripMenuItem2.Enabled = true;
                    this.删除控制器ToolStripMenuItem.Enabled = true;

                    this.tabControl.SelectedTab = 项目信息tabPage;


                    //加载flash[暂时先关闭]
                    flashData(collectorAreaName.ToString(), collectorProjectName.ToString(), collectorCollectorName.ToString(), collectorCollectorMode.ToString());

                    //数据库名称
                    string database = collectorAreaName.ToString() + collectorProjectName.ToString();

                    #region  选择flash模式

                    #region 其他项目flash模式[可删除]
                    if (collectorCollectorMode.ToString() == "1")
                    {


                        this.axShockwaveFlash1.Movie = Application.StartupPath + "\\" + collectorCollectorMode.ToString() + ".swf";
                        this.axShockwaveFlash1.Play();
                        //加载热水供暖据监控界面
                        this.chart1.Visible = true;
                        this.chart4.Visible = false;
                        this.chart5.Visible = false;
                        this.chart6.Visible = false;
                        this.splitContainer5.Visible = true;
                        this.splitContainer27.Visible = false;

                        //加载热水供暖画图功能
                        LoadDGV(database, collectorCollectorName.ToString());
                        Draw(database, collectorCollectorName.ToString());
                        LoadCollectorInfo();//加载树状图选中的控制器信息
                        LoadMetering(database, collectorCollectorName.ToString(), collectorCollectorMode.ToString());//刷新各表信息

                    }
                    if (collectorCollectorMode.ToString() == "2")
                    {


                        this.axShockwaveFlash纯热泵.Movie = Application.StartupPath + "\\" + collectorCollectorMode.ToString() + ".swf";
                        this.axShockwaveFlash纯热泵.Play();
                        //加载纯热泵干燥数据监控界面
                        this.chart1.Visible = false;
                        this.chart4.Visible = true;
                        this.chart5.Visible = false;
                        this.chart6.Visible = false;
                        this.splitContainer5.Visible = true;
                        this.splitContainer27.Visible = false;

                        //加载纯热泵干燥画图功能
                        LoadDGV(database, collectorCollectorName.ToString());
                        Draw(database, collectorCollectorName.ToString());
                        LoadCollectorInfo();//加载树状图选中的控制器信息
                        LoadMetering(database, collectorCollectorName.ToString(), collectorCollectorMode.ToString());//刷新各表信息

                    }
                    if (collectorCollectorMode.ToString() == "4")
                    {
                        ////加载复杂热水信息界面
                        //this.panel6.Enabled = false;
                        //this.panel5.Enabled = false;
                        //this.panel7.Enabled = true;
                        //this.panel34.Enabled = false;
                        ////加载纯复杂热水FLASH界面
                        //this.panelFlash1.Visible = false;
                        //this.panelFlash2.Visible = false;
                        //this.panelFlash纯热泵.Visible = false;
                        //this.panelFlash4.Visible = true;
                        //this.panelFlash5.Visible = false;


                        this.axShockwaveFlash4.Movie = Application.StartupPath + "\\" + collectorCollectorMode.ToString() + ".swf";
                        this.axShockwaveFlash4.Play();
                        //加载复杂热水系统数据监控界面
                        this.chart1.Visible = false;
                        this.chart4.Visible = false;
                        this.chart5.Visible = true;
                        this.chart6.Visible = false;
                        this.splitContainer5.Visible = true;
                        this.splitContainer27.Visible = false;

                        //加载复杂热水系统画图功能
                        LoadDGV(database, collectorCollectorName.ToString());
                        Draw(database, collectorCollectorName.ToString());
                        LoadCollectorInfo();//加载树状图选中的控制器信息
                        LoadMetering(database, collectorCollectorName.ToString(), collectorCollectorMode.ToString());//刷新各表信息

                    }
                    if (collectorCollectorMode.ToString() == "5")
                    {
                        //加载简单热水系统界面
                        //this.panel6.Enabled = false;
                        //this.panel5.Enabled = false;
                        //this.panel7.Enabled = false;
                        //this.panel34.Enabled = true;
                        ////加载简单热水系统FLASH界面
                        //this.panelFlash1.Visible = false;
                        //this.panelFlash2.Visible = false;
                        //this.panelFlash纯热泵.Visible = false;
                        //this.panelFlash4.Visible = false;
                        //this.panelFlash5.Visible = true;


                        this.axShockwaveFlash5.Movie = Application.StartupPath + "\\" + collectorCollectorMode.ToString() + ".swf";
                        this.axShockwaveFlash5.Play();
                        //加载简单热水系统数据监控界面
                        this.chart1.Visible = false;
                        this.chart4.Visible = false;
                        this.chart5.Visible = false;
                        this.chart6.Visible = true;
                        this.splitContainer5.Visible = true;
                        this.splitContainer27.Visible = false;

                        //加载简单热水系统画图功能
                        LoadDGV(database, collectorCollectorName.ToString());
                        Draw(database, collectorCollectorName.ToString());
                        LoadCollectorInfo();//加载树状图选中的控制器信息
                        LoadMetering(database, collectorCollectorName.ToString(), collectorCollectorMode.ToString());//刷新各表信息

                    }
                    #endregion

                    //如果是光伏制冷项目
                    if (collectorCollectorMode.ToString() == "8")
                    {
                        //加载光伏制冷界面
                        this.panel36.Enabled = true;
                        this.panel35.Enabled = true;

                        //加载光伏制冷FLASH界面
                        this.panelFlashCold.Visible = true;

                        //加载Flash动画模板
                        this.axShockwaveFlashCold.Movie = Application.StartupPath + "\\" + collectorCollectorMode.ToString() + ".swf";
                        this.axShockwaveFlashCold.Play();

                        //加载光伏制冷系统数据监控界面
                        this.chart1.Visible = true;//在Chart1的基础上更改
                        this.chart4.Visible = false;
                        this.chart5.Visible = false;
                        this.chart6.Visible = false;

                        this.splitContainer5.Visible = true;
                        this.splitContainer27.Visible = false;

                        //加载光伏制冷系统画图功能
                        LoadDGV(database, collectorCollectorName.ToString());

                        //画曲线
                        Draw(database, collectorCollectorName.ToString());


                        LoadCollectorInfo();//加载树状图选中的控制器信息
                       // LoadMetering(database, collectorCollectorName.ToString(), collectorCollectorMode.ToString());//刷新各表信息


                        //添加
                        LoadStrategy(database, collectorCollectorName.ToString(), collectorCollectorMode.ToString());
                        LoadCanshu(database, collectorCollectorName.ToString(), collectorCollectorMode.ToString());

                    }
                    #endregion
                }
                #endregion
            }
            catch
            { }
        }
        #endregion

        #region 点击启动
        /// <summary>
        /// 点击启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolSBStart_Click(object sender, EventArgs e)
        {
            try
            {
                this.toolSBStart.Enabled = false;
                this.toolSBStop.Enabled = true;
                GlobalInfo.IsExit = false;
                this.tabControl.SelectedTab = 系统运行信息tabPage;
                AddItemToListBox(string.Format("************本地监控客户端IP地址为{0}:{1}************", localAddress, GlobalInfo.port));
                
                //判断SerialPort串口是否打开
                serialPort1.Open();
                serialPort2.Open();
                int num = 0;
                if (!serialPort1.IsOpen)
                {
                    serialPort1.Open();
                    sendSerialPort(serialPort1, RS485.closeX1);
                    sendSerialPort(serialPort1, RS485.closeX2);
                    while (serialPort1.BytesToWrite != 0 || serialPort1.BytesToRead == 0)
                    {
                        Thread.Sleep(1);
                        num++;
                        if (num > 50)
                        {
                            break;
                        }
                    }
                }
                if (!serialPort2.IsOpen)
                {
                    serialPort2.Open();
                }

                //打开MX100
                comm = DAQMX100.openMX100(enc.GetBytes("192.168.1.5"), out errorInfo);

                if (comm != 0)
                {
                    //MX100模块初始化
                    errorInfo = DAQMX100.initSetValueMX100(comm);
                    errorInfo = DAQMX100.setUnitTempMX100(comm, DAQMX100.DAQMX100_TEMPUNIT_C);//设置温度单位为℃
                    AddItemToListBox(string.Format("************初始化MX100成功!************"));

                    //设置每个Module
                    for (int i = 0; i <= 4; i++)
                    {
                        errorInfo = DAQMX100.setIntegralMX100(comm, i, DAQMX100.DAQMX100_INTEGRAL_AUTO);//设置频率为自动
                        errorInfo = DAQMX100.setIntervalMX100(comm, i, DAQMX100.DAQMX100_INTERVAL_1000);//设置采样周期为1S
                    }
                    AddItemToListBox(string.Format("************初始化Module0、1、2、3成功!************"));

                    //设置1-4通道:压力温度
                    for (int ch = 1; ch <= 6; ch++)
                    {
                        errorInfo = DAQMX100.initDataChMX100(comm, ch);
                        errorInfo = DAQMX100.setRangeMX100(comm, ch, DAQMX100.DAQMX100_RANGE_VOLT_6V);
                    }

                    //设置11-14通道：热电偶
                    for (int ch = 11; ch <= 14; ch++)
                    {
                        errorInfo = DAQMX100.initDataChMX100(comm, ch);
                        errorInfo = DAQMX100.setRangeMX100(comm, ch, DAQMX100.DAQMX100_RANGE_TC_K);
                    }

                    //设置21-27通道：4线制热电阻
                    for (int ch = 21; ch <= 26; ch++)
                    {
                        errorInfo = DAQMX100.initDataChMX100(comm, ch);
                        errorInfo = DAQMX100.setRangeMX100(comm, ch, DAQMX100.DAQMX100_RANGE_RTD_1MAPTH);//设置通道24范围
                    }
                    for (int ch = 31; ch <= 36; ch++)
                    {
                        errorInfo = DAQMX100.initDataChMX100(comm, ch);
                        errorInfo = DAQMX100.setRangeMX100(comm, ch, DAQMX100.DAQMX100_RANGE_RTD_1MAPTH);//设置通道24范围
                    }

                    AddItemToListBox(string.Format("************初始化温度通道成功!************"));

                    //发送配置信息
                    errorInfo = DAQMX100.sendConfigMX100(comm);

                    //开始测量
                    errorInfo = DAQMX100.measStartMX100(comm);

                    AddItemToListBox(string.Format("************成功连接数据采集仪MX100!************"));

                    thread_ListenClient = new Thread(ListenClientConnect);
                    thread_ListenClient.IsBackground = true;//将监听线程设为后台运行
                    thread_ListenClient.Start();
                    GlobalInfo.IsStart = true;

                }
                else
                {
                    AddItemToListBox(string.Format("************局域网不存在MX100设备************"));
                    this.toolSBStart.Enabled = true;
                    this.toolSBStop.Enabled = false;
                    GlobalInfo.IsStart = false;
                }

            }
            catch (System.Exception ex)
            { 
                MessageBox.Show(ex.Message);
                this.toolSBStart.Enabled = true;
                this.toolSBStop.Enabled = false;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
                GlobalInfo.IsStart = false;
            }

        }

        #endregion

        #region 点击停止
        /// <summary>
        /// 点击停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolSBStop_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确认停止服务？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                this.tabControl.SelectedTab = 系统运行信息tabPage;


                GlobalInfo.IsExit = true;

                //清空textBox中的数据
                
                //终止线程
                closeThead = true;
                
                //停止测量
                //DAQMX100.measStopMX100(comm);
                AddItemToListBox("************停止测量成功************");


                AddItemToListBox("************关闭连接成功************");

                //设置开始按钮可用，停止按钮不可用
                this.toolSBStart.Enabled = true;
                this.toolSBStop.Enabled = false;
                GlobalInfo.IsStart = false;

            }
            else
            { }

        }
        #endregion

        #region 报警查询
        /// <summary>
        /// 报警按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.led1.BackColor = Color.Lime;
            this.led1.Text = "无报警";
            FormAlarm fmAlarm = new FormAlarm();
            fmAlarm.Show();

        }
        #endregion


        #region 后台获取数据
        /// <summary>
        /// 后台接收MX100,RS485数据
        /// </summary>
        private void ListenClientConnect()
        {
            try
            {
                #region 定义变量
                
                double[] cH01To05 = new  double[5];//通道1-4电流类型4-20mA
                double[] cH11To14 = new double[4];//通道11-14热电偶类型
                double[] cH21To26 = new double[6];//通道21-26热电阻类型
                double[] cH31To36 = new double[6];//通道31-36热电阻类型
                
                string z1Temp = "暂无数据";
                string z2sun = "暂无数据";
                string z3Win = "暂无数据";
                string z4Wet = "暂无数据";              
                string z5DCVolt = "-1";//直流表电压
                string z6DCCurrent = "-1";//直流表电流
                string z7DCP = "-1";//直流表功率
                string z8DCW = "-1";//直流表电能

                string z9PAC6Uab = "-1";//压缩机ab电压
                string z10PAC6Ubc = "-1";//压缩机bc电压
                string z11PAC6Uca= "-1";//压缩机ca电压
                string z12PAC6Ia = "-1";//压缩机a相电流
                string z13PAC6Ib = "-1";//压缩机b相电流
                string z14PAC6Ic = "-1";//压缩机c相电流
                string z15PAC6Ps = "-1";//视在功率
                string z16PAC6Py = "-1";//有功功率
                string z17PAC6Pw = "-1";//无功功率
                string z18PAC6Pn = "-1";//功率因素
                string z19PAC6W = "-1";//压缩机电能
                string z20PAC6Uaj = "-1";//a相畸变电压
                string z21PAC6Iaj = "-1";//a相畸变电流
                string z22PAC6UBa = "-1";//a相电压不平衡度
                string z23PAC6IBa = "-1";//a相电流不平衡度
                string z24PressOut = "-1";//压缩机排气压力
                string z25EvaOut = "-1";//蒸发器出口压力

                string z30m420Volt = "-1";//水泵电压
                string z31m420Current = "-1";//水泵电流
                string z32m420P = "-1";//水泵功率
                string z33m420f = "-1";//水泵频率
                string z34m420W = "-1";//水泵电能
                string z35WaterFlow = "-1";//供水流量
                string z43overWater = "-1";//溢水量
                double z43overWaterNow = 0;
                double z43overWaterOut = 0;
                string z44waterLeverl = "0";//水箱液位
                string z45transRate = "-1";//逆控效率
                string z46EER = "-1";//能效比
                string pWM = "-1";//占空比
                string modeChoose = "-1";//太阳能/市电模式
                string sUMPACW = "-1";//总电能
                string yaSuoJif = "-1";//压缩机频率

                //参数变量
                string UT35ANowTemp = "-1";
                string UT35ASetTemp = "-1";
                string UT35AP = "-1";
                string UT35AI = "-1";
                string UT35AD = "-1";
                
                string PAC5P = "-1";//总电表功率
                string PAC5W = "-1";//总电表电能
                string PAC7U = "-1";//单相表电压
                string PAC7I = "-1";//单相表电流
                string PAC7P = "-1";//单相表功率
                string PAC7W = "-1";//单相表电能



                string database = "云南省昆明市光伏制冷系统";
                string collectorName = "系统采集器";

                Action acLaodCanshu = delegate
                { LoadCanshu(database, collectorName, "8"); };
                this.Invoke(acLaodCanshu);

                //获取初始电能:
                //总电能
                sendSerialPort(serialPort1, RS485.readPAC5W);
                Thread.Sleep(100);
                string PAC5Wstart = changeToDouble(recieveData(serialPort1, RS485.readPAC5W, 13, RS485.CheckType_CRC));
                while (PAC5Wstart=="-1")
                {
                    serialPort1.DiscardInBuffer();
                    sendSerialPort(serialPort1, RS485.readPAC5W);
                    PAC5Wstart = changeToDouble(recieveData(serialPort1, RS485.readPAC5W, 13, RS485.CheckType_CRC));
                }
                startSumPAC = Convert.ToDouble(PAC5Wstart);

                //压缩机电能
                sendSerialPort(serialPort1, RS485.readPAC6W);
                Thread.Sleep(100);
                string PAC6Wstart = changeToDouble(recieveData(serialPort1, RS485.readPAC6W, 13, RS485.CheckType_CRC));
                while (PAC6Wstart == "-1")
                {
                    serialPort1.DiscardInBuffer();
                    sendSerialPort(serialPort1, RS485.readPAC6W);
                    PAC6Wstart = changeToDouble(recieveData(serialPort1, RS485.readPAC6W, 13, RS485.CheckType_CRC));
                }
                startPressPAC = Convert.ToDouble(PAC6Wstart);
                
                //单相电表
                sendSerialPort(serialPort1, RS485.readPAC7W);
                Thread.Sleep(100);
                string PAC7Wstart = changeToDouble(recieveData(serialPort1, RS485.readPAC7W, 13, RS485.CheckType_CRC));
                while (PAC7Wstart == "-1")
                {
                    serialPort1.DiscardInBuffer();
                    sendSerialPort(serialPort1, RS485.readPAC7W);
                    PAC7Wstart = changeToDouble(recieveData(serialPort1, RS485.readPAC7W, 13, RS485.CheckType_CRC));
                }
                startSinglePAC = Convert.ToDouble(PAC7Wstart);

                //直流电表
                sendSerialPort(serialPort1, RS485.readDCW);
                Thread.Sleep(100);
                byte[] tempTypestart = recieveData(serialPort1, RS485.readDCW, 9, RS485.CheckType_CRC);
                if (tempTypestart[0] != 0xFF || tempTypestart[1] != 0xFF || tempTypestart[2] != 0xFF && tempTypestart[3] != 0xFF)
                {
                    startDCPAC = (Convert.ToDouble((tempTypestart[0] * 256 + tempTypestart[1]) * 65536 + (tempTypestart[2] * 256 + tempTypestart[3])) / 100);
                }
       
                #endregion

                while (true)
                {

                    #region 首先获得开关状态
                    //X1状态
                    sendSerialPort(serialPort1,RS485.readStatusX1);
                    byte [] statusX1 =recieveData(serialPort1, RS485.readStatusX1, 6, RS485.CheckType_CRC);
                    if (statusX1[0]==0x00)
                    {
                        CanshuValue.switch1 = "0";
                        this.pbX1.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";
                    }
                    else if(statusX1[00]==0x01)
                    {
                        CanshuValue.switch1 = "1";
                        this.pbX1.ImageLocation = Application.StartupPath + "\\强制开关\\Open.png";
                    }

                    //X2状态
                    sendSerialPort(serialPort1, RS485.readStatusX2);
                        byte[] statusX2 = recieveData(serialPort1, RS485.readStatusX2, 6, RS485.CheckType_CRC);
                    if (statusX2[0] == 0x00)
                    {
                        CanshuValue.switch2 = "0";
                        this.pbX2.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";
                    }
                    else if (statusX2[0]==0x01)
                    {
                        CanshuValue.switch2 = "1";
                        this.pbX2.ImageLocation = Application.StartupPath + "\\强制开关\\Open.png";
                    }



                    //X11,X12状态
                    sendSerialPort(serialPort1, RS485.readStatusX11);
                    byte[] statusX11 = recieveData(serialPort1, RS485.readStatusX11, 6, RS485.CheckType_CRC);
                    sendSerialPort(serialPort1, RS485.readStatusX12);
                    byte[] statusX12 = recieveData(serialPort1, RS485.readStatusX12, 6, RS485.CheckType_CRC);
                    
                    //假设此时为市电
                    if (statusX11[0] == 0x01&&statusX12[0]==0x00)
                    {
                        //CanshuValue.switch3 = "0";
                        handSwitchStatus = 3;
                        
                       // this.pbX3.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";
                        this.meter1.Value = 3;
                        CanshuValue.handSwitchStatus = "3";
                    }
                    else if (statusX11[0] == 0x00 && statusX12[0] == 0x01)
                    {
                        //CanshuValue.switch3 = "1";
                        //this.pbX3.ImageLocation = Application.StartupPath + "\\强制开关\\Open.png";
                        this.meter1.Value = 1;
                        CanshuValue.handSwitchStatus = "1";
                        handSwitchStatus = 1;
                    }
                    else if ((statusX11[0] == 0x00 && statusX12[0] == 0x00) || (statusX11[0] == 0x01 && statusX12[0] == 0x01))
                    {
                        this.meter1.Value = 2;
                        CanshuValue.handSwitchStatus = "2";
                        handSwitchStatus = 2;
                        
                    }

                    //X3状态
                    sendSerialPort(serialPort1, RS485.readStatusX3);
                    byte[] statusX3 = recieveData(serialPort1, RS485.readStatusX3, 6, RS485.CheckType_CRC);
                   if (statusX3.Length==1)
                   {
                       if (statusX3[0] == 0x00)
                       {
                           CanshuValue.switch3 = "0";
                           //this.pbX3.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";
                       }
                       else if (statusX3[0] == 0x01)
                       {
                           CanshuValue.switch3 = "1";
                           //this.pbX3.ImageLocation = Application.StartupPath + "\\强制开关\\Open.png";
                       }
                   }                 
                    #endregion
                    
                    #region 获取MX100采集到的数据
                    for (int ch = 1; ch <= 5; ch++)
                    {
                        errorInfo = DAQMX100.measInstChMX100(comm, ch);
                    }
                    for (int ch = 11; ch <= 14;ch++ )
                    {
                        errorInfo = DAQMX100.measInstChMX100(comm, ch);
                    }
                    for (int ch = 21; ch <= 26;ch++ )
                    {
                        errorInfo = DAQMX100.measInstChMX100(comm, ch);
                    }

                    for (int ch = 1; ch <= 5; ch++)
                    {
                        cH01To05[ch - 1] = DAQMX100.dataDoubleValueMX100(comm, ch);
                    }
                    for (int ch = 11; ch <= 14; ch++)
                    {
                        cH11To14[ch - 11] = DAQMX100.dataDoubleValueMX100(comm, ch);
                    }
                    for (int ch = 21; ch <= 26; ch++)
                    {
                        cH21To26[ch - 21] = DAQMX100.dataDoubleValueMX100(comm, ch);
                    }
                    errorInfo = DAQMX100.measInstChMX100(comm, 31);
                    cH31To36[0] = DAQMX100.dataDoubleValueMX100(comm, 31);
                    while(cH01To05[0]==0||cH01To05[1]==0||cH01To05[2]==0||cH01To05[3]==0||cH01To05[4]==0)
                    {
                        for (int ch = 1; ch <= 5; ch++)
                        {
                            cH01To05[ch - 1] = DAQMX100.dataDoubleValueMX100(comm, ch);
                        }
                    }
                    //数据处理
                    //流量
                     z35WaterFlow = (Math.Abs(cH01To05[2] - 1) * 3.75).ToString("F2");
                                  
                    //压力
                    z24PressOut = (Math.Abs(cH01To05[3] - 1) * 0.375).ToString("F2");//压缩机排气
                    z25EvaOut = (Math.Abs(cH01To05[4] - 1) * 0.375).ToString("F2");//蒸发器出口                  

                    //水箱液位
                    z44waterLeverl = (Math.Abs(cH01To05[1] - 1) * 0.75).ToString("F2");
                   
                    //溢水液位
                    //备注：1.209为出水阀压力值 1v压力为2944.64075ml水
                    if (CanshuValue.switch1 == "0" && CanshuValue.switch2 == "1")
                    {
                        z43overWater = z43overWaterOut.ToString();
                    }
                    else
                    {
                          z43overWaterNow = (Math.Abs(cH01To05[0] - 1.207) * 2944.64075);
                          z43overWater = (z43overWaterNow + z43overWaterOut).ToString("F2");
                    }
                    if (Convert.ToDouble(z43overWater) < 100)
                    {
                        z43overWater = "0";
                    }
                    
                    #endregion
                   
                    #region 直流电测表参数：设备号：03
                    
                    //直流电压
                    sendSerialPort(serialPort1, RS485.readDCvolt);
                    string z5DCVoltnew = changeDC(recieveData(serialPort1, RS485.readDCvolt, 9, RS485.CheckType_CRC));
                    z5DCVolt = updateValue(z5DCVoltnew, z5DCVolt);

                    //获取直流电测仪表电流
                    sendSerialPort(serialPort1, RS485.readDCCurrent);
                    byte[] recivebyte = recieveData(serialPort1, RS485.readDCCurrent, 9, RS485.CheckType_CRC);
                    string z6DCCurrentnew = changeDC(recivebyte);
                    z6DCCurrent = updateValue(z6DCCurrentnew, z6DCCurrent);

                    //直流功率
                    sendSerialPort(serialPort1, RS485.readDCP);
                    string z7DCPnew = changeDC(recieveData(serialPort1, RS485.readDCP, 9, RS485.CheckType_CRC));
                    z7DCP = updateValue(z7DCPnew, z7DCP);

                    //直流电能
                    sendSerialPort(serialPort1, RS485.readDCW);
                    byte[] tempType = recieveData(serialPort1, RS485.readDCW, 9, RS485.CheckType_CRC);
                    if (tempType[0] != 0xFF || tempType[1] != 0xFF || tempType[2] != 0xFF && tempType[3] != 0xFF)
                    {
                        z8DCW = (Convert.ToDouble((tempType[0] * 256 + tempType[1]) * 65536 + (tempType[2] * 256 + tempType[3])) / 100).ToString("F2");
                    }
                    z8DCW=(Convert.ToDouble(z8DCW)-startDCPAC).ToString("F4");

                    sendSerialPort(serialPort1, RS485.readUT35ANowTemp);
                    byte[] Ut35 = recieveData(serialPort1, RS485.readUT35ANowTemp, 7, RS485.CheckType_CRC);
                    if (Ut35[0] != 0xFF || Ut35[1] != 0xFF)
                    {
                        byte[] Ut35t = new byte[Ut35.Length];
                        for (int i = 0; i < Ut35.Length; i++)
                        {
                            Ut35t[Ut35t.Length - 1 - i] = Ut35[i];
                        }
                        UT35ANowTemp = (Convert.ToDouble(BitConverter.ToInt16(Ut35t, 0)) / 10).ToString("F2");

                    }

                    //获取占空比
                    sendSerialPort(serialPort1, RS485.readUT35APWM);
                    byte[] Ut35pwm = recieveData(serialPort1, RS485.readUT35APWM, 7, RS485.CheckType_CRC);
                    if (Ut35pwm[0] != 0xFF || Ut35pwm[1] != 0xFF)
                    {
                        byte[] Ut35ptwm = new byte[Ut35pwm.Length];
                        for (int i = 0; i < Ut35pwm.Length; i++)
                        {
                            Ut35ptwm[Ut35ptwm.Length - 1 - i] = Ut35pwm[i];
                        }
                        pWM= (Convert.ToDouble(BitConverter.ToInt16(Ut35ptwm, 0)) / 10).ToString("F2");
                    }

                    #endregion                                  

                    #region  压缩机电表参数：设备号：05

                    //Uab电压
                    sendSerialPort(serialPort1, RS485.readPAC6Uab);
                    string z9PAC6Uabnew = changeToFloat(recieveData(serialPort1, RS485.readPAC6Uab, 9, RS485.CheckType_CRC));
                    z9PAC6Uab = updateValue(z9PAC6Uabnew, z9PAC6Uab);
                    //Ubc电压
                    sendSerialPort(serialPort1, RS485.readPAC6Ubc);
                    string z10PAC6Ubcnew = changeToFloat(recieveData(serialPort1, RS485.readPAC6Ubc, 9, RS485.CheckType_CRC));
                    z10PAC6Ubc = updateValue(z10PAC6Ubcnew, z10PAC6Ubc);
                    //Uca电压
                    sendSerialPort(serialPort1, RS485.readPAC6Uca);
                    string z11PAC6Ucanew = changeToFloat(recieveData(serialPort1, RS485.readPAC6Uca, 9, RS485.CheckType_CRC));
                    z11PAC6Uca = updateValue(z11PAC6Ucanew, z11PAC6Uca);
                    //Ia电流
                    sendSerialPort(serialPort1, RS485.readPAC6Ia);
                    string z12PAC6Ianew = changeToFloat(recieveData(serialPort1, RS485.readPAC6Ia, 9, RS485.CheckType_CRC));
                    z12PAC6Ia = updateValue(z12PAC6Ianew, z12PAC6Ia);
                    //Ib电流
                    sendSerialPort(serialPort1, RS485.readPAC6Ib);
                    string z13PAC6Ianew = changeToFloat(recieveData(serialPort1, RS485.readPAC6Ib, 9, RS485.CheckType_CRC));
                    z13PAC6Ib = updateValue(z13PAC6Ianew, z13PAC6Ib);
                    //Ic电流
                    sendSerialPort(serialPort1, RS485.readPAC6Ic);
                    string z14PAC6Icnew = changeToFloat(recieveData(serialPort1, RS485.readPAC6Ic, 9, RS485.CheckType_CRC));
                    z14PAC6Ic = updateValue(z14PAC6Icnew, z14PAC6Ic);
                    //视在功率
                    sendSerialPort(serialPort1, RS485.readPAC6Ps);
                    string PAC6Psnew = changeToFloat(recieveData(serialPort1, RS485.readPAC6Ps, 9, RS485.CheckType_CRC));
                    z15PAC6Ps = updateValue(PAC6Psnew, z15PAC6Ps);
                    //有功功率
                    sendSerialPort(serialPort1, RS485.readPAC6Py);
                    string PAC6Pynew = changeToFloat(recieveData(serialPort1, RS485.readPAC6Py, 9, RS485.CheckType_CRC));
                    z16PAC6Py = updateValue(PAC6Pynew, z16PAC6Py);
                    //有功功率
                    sendSerialPort(serialPort1, RS485.readPAC6Pw);
                    string PAC6Pwnew = changeToFloat(recieveData(serialPort1, RS485.readPAC6Pw, 9, RS485.CheckType_CRC));
                    z17PAC6Pw = updateValue(PAC6Pwnew, z17PAC6Pw);
                    //功率因数
                    sendSerialPort(serialPort1, RS485.readPAC6Pn);
                    string PAC6Pnnew = changeToFloat(recieveData(serialPort1, RS485.readPAC6Pn, 9, RS485.CheckType_CRC));
                    z18PAC6Pn = updateValue(PAC6Pnnew, z18PAC6Pn);
                    //压缩机电能
                    sendSerialPort(serialPort1, RS485.readPAC6W);
                    string PAC6Wnew = changeToDouble(recieveData(serialPort1, RS485.readPAC6W, 13, RS485.CheckType_CRC));
                    z19PAC6W = updateValue(PAC6Wnew, z19PAC6W);
                    z19PAC6W = (Convert.ToDouble(z19PAC6W) - startPressPAC).ToString("F4");
                    //压缩机频率
                    sendSerialPort(serialPort1, RS485.readPAC6f);
                    string yaSuoJifnew = changeToFloat(recieveData(serialPort1, RS485.readPAC6f, 9, RS485.CheckType_CRC));
                    yaSuoJif = updateValue(yaSuoJifnew, yaSuoJif);
                    //a相畸变电压
                    sendSerialPort(serialPort1, RS485.readPAC6Uj);
                    string z20PAC6Uajnew = changeToFloat(recieveData(serialPort1, RS485.readPAC6Uj, 9, RS485.CheckType_CRC));
                    z20PAC6Uaj = updateValue(z20PAC6Uajnew, z20PAC6Uaj);
                    //a相畸变电流
                    sendSerialPort(serialPort1, RS485.readPAC6Ij);
                    string z21PAC6Iajnew = changeToFloat(recieveData(serialPort1, RS485.readPAC6Ij, 9, RS485.CheckType_CRC));
                    z21PAC6Iaj = updateValue(z21PAC6Iajnew, z21PAC6Iaj);
                    //a相电压不平衡
                    sendSerialPort(serialPort1, RS485.readPAC6UB);
                    string z22PAC6UBnew = changeToFloat(recieveData(serialPort1, RS485.readPAC6UB, 9, RS485.CheckType_CRC));
                    z22PAC6UBa = updateValue(z22PAC6UBnew, z22PAC6UBa);
                    //a相电流不平衡
                    sendSerialPort(serialPort1, RS485.readPAC6IB);
                    string z23PAC6IBanew = changeToFloat(recieveData(serialPort1, RS485.readPAC6IB, 9, RS485.CheckType_CRC));
                    z23PAC6IBa = updateValue(z23PAC6IBanew, z23PAC6IBa);

                    //sendSerialPort(serialPort1, RS485.readPAC5f);
                    //string PAC5fnew = changeToFloat(recieveData(serialPort1, RS485.readPAC5f, 9, RS485.CheckType_CRC));
                    //PAC5f = updateValue(PAC5fnew, PAC5f);


                    //sendSerialPort(serialPort1, RS485.readPAC5Ia);
                    //string PAC5Ianew = changeToFloat(recieveData(serialPort1, RS485.readPAC5Ia, 9, RS485.CheckType_CRC));
                    //PAC5Ia = updateValue(PAC5Ianew, PAC5Ia);

                    //sendSerialPort(serialPort1, RS485.readPAC5U1n);
                    //string PAC5U1nnew = changeToFloat(recieveData(serialPort1, RS485.readPAC5U1n, 9, RS485.CheckType_CRC));
                    //PAC5U1n = updateValue(PAC5U1nnew, PAC5U1n);
                    
                    
                    sendSerialPort(serialPort1, RS485.readPAC5P);
                    string PAC5Pnew = changeToFloat(recieveData(serialPort1, RS485.readPAC5P, 9, RS485.CheckType_CRC));
                    PAC5P = updateValue(PAC5Pnew, PAC5P);

                    //总电能值
                    sendSerialPort(serialPort1, RS485.readPAC5W);
                    string PAC5Wnew = changeToDouble(recieveData(serialPort1, RS485.readPAC5W, 13, RS485.CheckType_CRC));
                    PAC5W = updateValue(PAC5Wnew, PAC5W);
                    sUMPACW = (Convert.ToDouble(PAC5W) - startSumPAC).ToString("F4");
                    
                    #endregion

                    #region M420水泵参数：设备号：02

                    //获取M420水泵电压
                    sendSerialPort(serialPort2, RS485.readM420Volt);
                    byte[] m420Vbyte = recieveData(serialPort2, RS485.readM420Volt, 16, RS485.CheckType_BC);
                    if (m420Vbyte[0] != 0xFF || m420Vbyte[1] != 0xFF || m420Vbyte[2] != 0xFF || m420Vbyte[3] != 0xFF)
                    {
                        string m420Vnew = changeToFloat(m420Vbyte);
                        z30m420Volt = updateValue(m420Vnew, z30m420Volt);
                    }


                    //获取M420水泵电流、水泵功率
                    sendSerialPort(serialPort2, RS485.readM420Current);
                    byte[] m420Ibyte = recieveData(serialPort2, RS485.readM420Current, 16, RS485.CheckType_BC);
                    if (m420Ibyte[0] != 0xFF || m420Ibyte[1] != 0xFF || m420Ibyte[2] != 0xFF || m420Ibyte[3] != 0xFF)
                    {
                        string m420Inew = changeToFloat(m420Ibyte);
                        z31m420Current = updateValue(m420Inew, z31m420Current);       
                       
                    }

                    z32m420P = (Convert.ToDouble(z30m420Volt) * Convert.ToDouble(z31m420Current)).ToString("F2");

                    //获取M420水泵频率
                    sendSerialPort(serialPort2, RS485.readM420f);
                    byte[] m420fbyte = recieveData(serialPort2, RS485.readM420f, 16, RS485.CheckType_BC);
                    if (m420fbyte[0] != 0xFF || m420fbyte[1] != 0xFF || m420fbyte[2] != 0xFF || m420fbyte[3] != 0xFF)
                    {
                        string m420fnew = changeToFloat(m420fbyte);
                        z33m420f = updateValue(m420fnew, z33m420f);
                    }
                    #endregion

                    #region PAC3200单相表[除压缩机以为的制冷系统]参数：设备号：07

                    sendSerialPort(serialPort1, RS485.readPAC7I);
                    string PAC7Inew = changeToFloat(recieveData(serialPort1, RS485.readPAC7I, 9, RS485.CheckType_CRC));
                    PAC7I = updateValue(PAC7Inew, PAC7W);

                    sendSerialPort(serialPort1, RS485.readPAC7U1n);
                    string PAC7Unew = changeToFloat(recieveData(serialPort1, RS485.readPAC7U1n, 9, RS485.CheckType_CRC));
                    PAC7U = updateValue(PAC7Unew, PAC7U);

                    sendSerialPort(serialPort1, RS485.readPAC7P);
                    string PAC7Pnew = changeToFloat(recieveData(serialPort1, RS485.readPAC7P, 9, RS485.CheckType_CRC));
                    PAC7P = updateValue(PAC7Pnew, PAC7P);

                    sendSerialPort(serialPort1, RS485.readPAC7W);
                    string PAC7Wnew = changeToDouble(recieveData(serialPort1, RS485.readPAC7W, 13, RS485.CheckType_CRC));
                    PAC7W = updateValue(PAC7Wnew, PAC7W);
                    PAC7W = (Convert.ToDouble(PAC7W) - startSinglePAC).ToString("F4");
                    #endregion

                    #region SM1910B温湿度模块：设备号：08
                    //sendSerialPort(serialPort1, RS485.readTempAndWet);
                    //byte [] tempAndWetNow =recieveData(serialPort1, RS485.readTempAndWet, 9, RS485.CheckType_CRC);
                    
                    //if (tempAndWetNow[0] != 0xFF || tempAndWetNow[1] != 0xFF||tempAndWetNow[1]!=0xFF||tempAndWetNow[1]!=0xFF)
                    //{
                    //    if (tempAndWetNow.Length==4)
                    //    {
                    //        byte[] newtemp = new byte[2];
                    //        byte[] newWet = new byte[2];
                    //        for (int i = 0; i < 2;i++ )
                    //        {
                    //            newtemp[1-i] = tempAndWetNow[i];
                    //        }
                    //        for (int j = 0; j < 2;j++ )
                    //        {
                    //            newWet[1-j] = tempAndWetNow[j + 2];
                    //        }
                    //        UInt16 tempNew =BitConverter.ToUInt16(newtemp, 0);
                    //        Int16 wetNew = BitConverter.ToInt16(newWet, 0);
                    //        z1Temp = updateValue((Convert.ToDouble(tempNew) / 100).ToString(), z1Temp);
                    //        z4Wet = updateValue((Convert.ToDouble(wetNew) / 100).ToString(), z4Wet);
                    //    }
                    //}
                    #endregion

                    #region 计算量
                    
                    //如果是光伏供电
                    if (CanshuValue.switch3=="1")
                    {
                        //逆变率
                        z45transRate = (Convert.ToDouble(z7DCP) / Convert.ToDouble(z15PAC6Ps)).ToString("F2");

                        z34m420W = (Convert.ToDouble(sUMPACW) - Convert.ToDouble(PAC7W)).ToString("F4");
                        modeChoose = "光伏";
                    }
                    else
                    {
                        Action removeZ45 = delegate { this.Z45.Visible = false;this.label8.Visible=false ;};
                        this.Invoke(removeZ45);
                        z45transRate ="空";
                        z34m420W = (Convert.ToDouble(sUMPACW) - Convert.ToDouble(PAC7W)-Convert.ToDouble(z19PAC6W)).ToString("F4");
                        modeChoose = "市电";
                    }
                    #endregion 

                    //上位机控制模式为自动
                    Action handControlMode = delegate
                    {
                        //选中自动模式
                        if (this.radioButton24.Checked == true)
                        {
                            #region 原代码
                            ////当前开关状态为X1关、X2关
                            //if (CanshuValue.switch1 == "0" && CanshuValue.switch2 == "0")
                            //{
                            //    //如果液位低于某值(一个很小的值)，打开X1进水
                            //    if (cH01To05[5] <lowerLimit && DateTime.Now - closeX1Time > new TimeSpan(0, 1, 0))
                            //    {
                            //        openX1Time = DateTime.Now;
                            //        openX1Flag = true;
                            //    }
                            //    //如果液位大于某值(比较大的值)，打开X2放水
                            //    else if (cH01To05[5]>upperLimit && DateTime.Now - closeX2Time > new TimeSpan(0, 1, 0))
                            //    {
                            //        openX2Time = DateTime.Now;
                            //        openX2Flag = true;
                            //    }

                            //}
                            ////当前开关状态为X1开，X2关
                            //else if (CanshuValue.switch1 == "1" && CanshuValue.switch2 == "0" && DateTime.Now - openX1Time > new TimeSpan(0, timeSpano, 0))
                            //{
                            //    //关闭X1
                            //    closeX1Time = DateTime.Now;
                            //    closeX1Flag = true;
                            //}
                            //else if (CanshuValue.switch1 == "0" && CanshuValue.switch2 == "1" && DateTime.Now - openX2Time > new TimeSpan(0, timeSpano, 0))
                            //{
                            //    //关闭X2
                            //    closeX2Time = DateTime.Now;
                            //    closeX2Flag = true;
                            //}
                            #endregion 

                            //当前开关状态为X1关、X2关
                            if (CanshuValue.switch1 == "0" && CanshuValue.switch2 == "0")
                            {
                                //如果液位小于某值，打开X1进水
                                if (cH01To05[0] <= upperLimit && DateTime.Now - closeX1Time > new TimeSpan(0, timeSpan, 0))
                                {
                                    openX1Time = DateTime.Now;
                                    openX1Flag = true;
                                }
                                //如果液位大于某值(比较大的值)，打开X2放水
                                else if (cH01To05[0] >= upperLimit )
                                {
                                    openX2Time = DateTime.Now;
                                    openX2Flag = true;
                                }

                            }
                            //当前开关状态为X1开，X2关
                            else if (CanshuValue.switch1 == "1" && CanshuValue.switch2 == "0" && DateTime.Now - openX1Time > new TimeSpan(0, timeSpano, 0))
                            {
                                //关闭X1
                                closeX1Time = DateTime.Now;
                                closeX1Flag = true;
                            }
                            else if (CanshuValue.switch1 == "0" && CanshuValue.switch2 == "1" && DateTime.Now - openX2Time > new TimeSpan(0, timeSpano, 0))
                            {
                                //关闭X2
                                closeX2Time = DateTime.Now;
                                closeX2Flag = true;
                            }


                        }
                    };
                    this.Invoke(handControlMode);

                    #region  数据判断
                    ////如果液位小于某值：电磁阀全部关闭
                    //if (s[2] < 1 && (CanshuValue.switch1 == "1" || CanshuValue.switch2 == "1"))
                    //{
                    //    closeX1Flag = true;
                    //    closeX1Flag = true;
                    //}

                    ////如果液位大于某个值、而且X1状态为关
                    //if (s[2]>1&&CanshuValue.switch1=="0")
                    //{
                    //    openX1Time = DateTime.Now;
                    //    openX1Flag = true;
                    //    closeX2Flag = true;
                    //}

                    ////电磁阀X1打开、当前时间与上次打开电磁阀时间间隔超过10min
                    //if (DateTime.Now - openX1Time >= new TimeSpan(0, 10, 0)&&CanshuValue.switch1=="1") 
                    //{
                    //    //关闭电磁阀X1
                    //    closeX1Time =DateTime.Now;
                    //    closeX1Flag = true;
                    //}

                    ////如果当前状态：X1,X2关
                    //if (DateTime.Now - closeX1Time >= new TimeSpan(0, 0, 30) && CanshuValue.switch1 == "0"&&CanshuValue.switch2=="0")
                    //{
                    //    //打开电磁阀X2
                    //    openX2Time = DateTime.Now;
                    //    openX2Flag = true;
                    //}
                    //if (DateTime.Now - openX2Time >= new TimeSpan(0, 2, 0)&&CanshuValue.switch1=="0"&&CanshuValue.switch2=="1")
                    //{
                    //    closeX2Time = DateTime.Now;
                    //    openX1Time = DateTime.Now;
                    //    closeX2Flag = true;
                    //    openX1Flag = true;
                    //}

                    //测试代码
                    //numtest++;

                    //if (numtest > 10)
                    //{
                    //    Thread.Sleep(20);
                    //    if (CanshuValue.switch1 == "0")
                    //    {
                    //        sendSerialPort(serialPort1, RS485.openX1);
                    //        Thread.Sleep(20);
                    //        CanshuValue.switch1 = "1";
                    //    }
                    //    else
                    //    {
                    //        closeX1Flag = true;
                    //        sendSerialPort(serialPort1, RS485.closeX1);
                    //        Thread.Sleep(20);
                    //        CanshuValue.switch1 = "0";
                    //    }
                    //    numtest = 0;
                    //}
                    //serialPort1.DiscardInBuffer();
                    #endregion

                    #region 事件标志位

                    //如果触发了发送设置按钮

                    if (setValueFlag == true)
                    {
                        if (this.C1.Text.Trim() != "" )
                        {
                            //读写设置温度
                            byte[] beSendValue = RS485.setUT35ATempValue;
                            Int16 keyValue = Convert.ToInt16(Convert.ToDouble(this.C1.Text)*10);
                            sendSetNum(keyValue, beSendValue);
                            recieveCanshu(serialPort1, RS485.readUT35ASetTemp, 8);

                            //改变状态值
                            setValueFlag = false;
                        }
                    }

                    #region X1,X2阀门改变标志，太阳能/市电开关标志
                    if (openX1Flag == true)
                    {
                        this.pbX1.ImageLocation = Application.StartupPath + "\\强制开关\\Open.png";
                        sendSerialPort(serialPort1, RS485.openX1);
                        recieveCanshu(serialPort1, RS485.openX1, 8);
                        CanshuValue.switch1 = "1";
                        canshuChangeFlag=true;
                        openX1Flag = false;
                    }

                    if (closeX1Flag == true)
                    {
                        this.pbX1.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";
                        sendSerialPort(serialPort1, RS485.closeX1);
                        recieveCanshu(serialPort1, RS485.closeX1, 8);
                        CanshuValue.switch1 = "0";
                        canshuChangeFlag=true;
                        closeX1Flag = false;
                    }


                    if (openX2Flag == true)
                    {
                        //排出水量
                        z43overWaterOut = z43overWaterOut+Convert.ToDouble(z43overWater);
                        this.pbX2.ImageLocation = Application.StartupPath + "\\强制开关\\Open.png";
                        sendSerialPort(serialPort1, RS485.openX2);
                        recieveCanshu(serialPort1, RS485.openX2, 8);
                        CanshuValue.switch2 = "1";
                        canshuChangeFlag=true;
                        openX2Flag = false;
                    }

                    if (closeX2Flag == true)
                    {
                        this.pbX2.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";
                        sendSerialPort(serialPort1, RS485.closeX2);
                        recieveCanshu(serialPort1, RS485.closeX2, 8);
                        CanshuValue.switch2 = "0";
                        canshuChangeFlag=true;
                        closeX2Flag = false;
                    }


                    if (sunEnergyFlag == true)
                    {
                        this.pbX3.ImageLocation = Application.StartupPath + "\\强制开关\\Open.png";
                        sendSerialPort(serialPort1, RS485.openX3);
                        recieveCanshu(serialPort1, RS485.openX3, 8);
                        CanshuValue.switch3 = "1";
                        canshuChangeFlag=true;
                        sunEnergyFlag = false;
                    }


                    if (redEnergyFlag == true)
                    {
                        this.pbX3.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";
                        sendSerialPort(serialPort1, RS485.closeX3);
                        recieveCanshu(serialPort1, RS485.closeX3, 8);
                        CanshuValue.switch3 = "0";
                        canshuChangeFlag=true;
                        redEnergyFlag = false;
                    }

                    #endregion
                    #endregion

                    #region 存储数据

                    //实时表
                    string input = 
                        #region SQL语句
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + z1Temp + "','" + z2sun + "','" + z3Win + "','" + z4Wet + "','" + z5DCVolt + "','" + z6DCCurrent + "','" + z7DCP + "','" + z8DCW + "','" + z9PAC6Uab + "','" + z10PAC6Ubc + "','" + z11PAC6Uca + "','" + z12PAC6Ia + "','" + z13PAC6Ib + "','" + z14PAC6Ic + "','" + z15PAC6Ps + "','" 
                        + z16PAC6Py + "','" + z17PAC6Pw + "','" + z18PAC6Pn + "','"+ z19PAC6W + "','" + z20PAC6Uaj + "','" + z21PAC6Iaj + "','" + z22PAC6UBa + "','" + z23PAC6IBa+ "','" + z24PressOut+ "','" + z25EvaOut+  "','" + cH11To14[0] + "','" + cH11To14[1] + "','"  +cH11To14[2] + "','"+ cH11To14[3] +"','"+ z30m420Volt + "','" + z31m420Current +
                         "','" + z32m420P + "','" + z33m420f + "','" + z34m420W + "','" + z35WaterFlow + "','" + cH21To26[0] + "','" + cH21To26[1] + "','" + cH21To26[2] + "','" + cH21To26[3] + "','" + cH21To26[4] + "','" + cH21To26[5] + "','" + cH31To36[0] + "','" + z43overWater + "','" + z44waterLeverl + "','" + z45transRate + "','" + z46EER + "','" + pWM + "','" + modeChoose + "','" + sUMPACW + "','" + yaSuoJif;
                        #endregion
                    string sqlWriteToDatabase = "insert into 系统采集器实时 values('" + input + "')";
                    dataaccess.ExeSQL(sqlWriteToDatabase, database);

                    //历史表
                    string historyTableName = "系统采集器实时" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");
                    bool boolValue = dataaccess.IsExistTable(historyTableName, database);
                    if (boolValue)//历史数据表是否存在
                    {
                        string sqlhistoryshuju = "insert into " + historyTableName + " values('" + input + "')";
                        dataaccess.ExeSQL(sqlhistoryshuju, database);
                    }
                    else//不存在该表，先创建该表再存入数据
                    {
                        //插入到采集点表
                        string[] collectorPointName = { "环境温度", "太阳能辐照度", "风速", "湿度", "光伏输出直流电压", "光伏输出直流电流","光伏输出直流功率", "光伏输出电能","压缩机ab电压", "压缩机bc电压","压缩机ca电压","压缩机a相电流","压缩机b相电流","压缩机c相电流","压缩机视在功率","压缩机有功功率","压缩机无功功率","压缩机功率因素",
                                                                                    "压缩机电能","a相畸变电压", "a相畸变电流","a相电压不平衡度","a相电流不平衡度",  "压缩机排气压力", "蒸发器出口压力", "压缩机排气温度", "冷凝器出口温度", "蒸发器出口温度", 
                                                                                    "蒸发器入口温度", "水泵变频运行电压", "水泵变频运行电流", "水泵变频运行功率","水泵变频运行频率","水泵电能",  "供冷冷水质量流量","冰块温度","水箱上层水温","供冷循环中回水温度",
                                                                                    "水箱下层水温","供冷循环中供水温度","风机盘管出风口温度","房间温度","溢水量","水箱液位","逆控率","能效比","PID占空比","太阳o光伏","总电能","压缩机频率"};
                        
                        string shujuNum = "TimeStamp varchar(50),";
                        int num = collectorPointName.Length;//表示数据表的数据个数长度
                        for (int i = 0; i < num; i++)
                        {
                            shujuNum += collectorPointName[i] + " varchar(50),";
                        }
                        shujuNum = shujuNum.Substring(0, shujuNum.Length - 1);
                        string sql_data = "create table "+historyTableName + "(" + shujuNum + " )";
                        dataaccess.ExeSQL(sql_data, database);
                        string sqlhistoryshuju = "insert into "+historyTableName+ "  values('" + input + "')";
                        dataaccess.ExeSQL(sqlhistoryshuju, database);
                    }

                    //参数值
                    //思路：参数值从设备中获取，并赋值给CanshuValue，然后存储。
                   
                    #region 横河UT35A参数：设备号：04

                    //温度为负，调整
                    sendSerialPort(serialPort1, RS485.readUT35ASetTemp);
                    byte[] Ut351 = recieveData(serialPort1, RS485.readUT35ANowTemp, 7, RS485.CheckType_CRC);

                    if (Ut351[0] != 0xFF || Ut351[1] != 0xFF)
                    {
                        byte[] Ut351t = new byte[Ut351.Length];
                        for (int i = 0; i < Ut351.Length; i++)
                        {
                            Ut351t[Ut351t.Length - 1 - i] = Ut351[i];
                        }
                        UT35ASetTemp = (Convert.ToDouble(BitConverter.ToInt16(Ut351t, 0)) / 10).ToString("F2");
                        if (UT35ASetTemp != CanshuValue.setTemp)
                        {
                            CanshuValue.setTemp = UT35ASetTemp;
                            canshuChangeFlag = true;
                        }
                    }

                    //获取P值
                    sendSerialPort(serialPort1, RS485.readUT35APValue);
                    byte[] Ut35p = recieveData(serialPort1, RS485.readUT35APValue, 7, RS485.CheckType_CRC);
                    if (Ut35p[0] != 0xFF || Ut35p[1] != 0xFF)
                    {
                        byte[] Ut35pt = new byte[Ut35p.Length];
                        for (int i = 0; i < Ut35p.Length; i++)
                        {
                            Ut35pt[Ut35pt.Length - 1 - i] = Ut35p[i];
                        }
                        UT35AP = (Convert.ToDouble(BitConverter.ToInt16(Ut35pt, 0)) / 10).ToString("F2");
                        if (UT35AP != CanshuValue.pValue)
                        {
                            CanshuValue.pValue = UT35AP;
                            canshuChangeFlag = true;
                        }
                    }

                    //获取i值
                    sendSerialPort(serialPort1, RS485.readUT35AIValue);
                    byte[] Ut35i = recieveData(serialPort1, RS485.readUT35AIValue, 7, RS485.CheckType_CRC);
                    if (Ut35i[0] != 0xFF || Ut35i[1] != 0xFF)
                    {
                        byte[] Ut35it = new byte[Ut35i.Length];
                        for (int i = 0; i < Ut35i.Length; i++)
                        {
                            Ut35it[Ut35it.Length - 1 - i] = Ut35i[i];
                        }
                        UT35AI = (Convert.ToDouble(BitConverter.ToInt16(Ut35it, 0))).ToString("F2");
                        if (UT35AI != CanshuValue.iValue)
                        {
                            CanshuValue.iValue = UT35AI;
                            canshuChangeFlag = true;
                        }
                    }

                    //获取d值
                    sendSerialPort(serialPort1, RS485.readUT35ADValue);
                    byte[] Ut35d = recieveData(serialPort1, RS485.readUT35ADValue, 7, RS485.CheckType_CRC);
                    if (Ut35d[0] != 0xFF || Ut35d[1] != 0xFF)
                    {
                        byte[] Ut35dt = new byte[Ut35d.Length];
                        for (int i = 0; i < Ut35i.Length; i++)
                        {
                            Ut35dt[Ut35dt.Length - 1 - i] = Ut35d[i];
                        }
                        UT35AD = (Convert.ToDouble(BitConverter.ToInt16(Ut35dt, 0))).ToString("F2");
                        if (UT35AD != CanshuValue.dValue)
                        {
                            CanshuValue.dValue = UT35AD;
                            canshuChangeFlag = true;
                        }
                    }

                    #endregion

                    if (canshuChangeFlag ==true)
                    {
                        string sql_canshu = "insert into 系统采集器参数 values('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + CanshuValue.setTemp + "','" + CanshuValue.pValue + "','" + CanshuValue.iValue + "','" + CanshuValue.dValue + "','" + CanshuValue.switch1 + "','" + CanshuValue.switch2 + "','" + CanshuValue.switch3 + "','" + CanshuValue.handSwitchStatus + "')";
                        dataaccess.ExeSQL(sql_canshu, database);
                        this.Invoke(acLaodCanshu);
                        canshuChangeFlag = false;
                        
                    }


                    #endregion

                    #region 曲线、DGV、Pannel显示
                    Action ldDGV = delegate { LoadDGV(database, "系统采集器"); };
                    this.Invoke(ldDGV);

                    Action ldData = delegate
                    {
                        LoadStrategy(database, "系统采集器", "8");
                        //LoadCanshu(database, "系统采集器", "8");
                    };
                    this.Invoke(ldData);

                    Action acDraw = delegate { Draw(database, "系统采集器");
                    flashData("云南省昆明市", "光伏制冷系统", "系统采集器", "8");
                    };
                    this.Invoke(acDraw);
                    #endregion

                    //点击停止按钮，结束这个线程
                    if (closeThead==true)
                    {
                        //关闭连接
                        DAQMX100.closeMX100(comm);
                        serialPort1.Close();
                        serialPort2.Close();
                        
                        break;
                    }


                }
                thread_ListenClient.Abort();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\nListenClientConnect错误");
            }

        }
        #endregion

        #region listBox操作

        /// <summary>
        /// 向listbox添加系统运行状态
        /// </summary>
        /// <param name="str"></param>
        private delegate void AddItemToListBoxDelegate(string str);
        private void AddItemToListBox(string str)
        {
            if (listBoxStatus.InvokeRequired)
            {
                AddItemToListBoxDelegate d = AddItemToListBox;
                listBoxStatus.Invoke(d, str);
            }
            else
            {
                listBoxStatus.Items.Add(str);
                listBoxStatus.SelectedIndex = listBoxStatus.Items.Count - 1;
                listBoxStatus.ClearSelected();
                InsertSQLLog(str);
            }
        }


        /// <summary>
        /// 向数据库插入日志
        /// </summary>
        /// <param name="str"></param>
        private void InsertSQLLog(string str)
        {
            try
            {
                string sql = "insert into log values('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + str + "')";
                dataaccess.ExeSQL(sql, GlobalInfo.DefaultDatabase);
            }
            catch
            {
            }
        }


        #endregion


        #region 数据处理
        /// <summary>
        /// 根据不同模式的数据处理过程
        /// </summary>
        /// <param name="user"></param>
        /// <param name="xd"></param>
        private void DataProcessing(XmlDocument xd)
        {
            XmlNode root = xd.DocumentElement;
            //控制器id   地区编号+项目编号+控制器编号
            string id = root.SelectSingleNode("common").SelectSingleNode("id").InnerText;
            string type = root.SelectSingleNode("common").SelectSingleNode("type").InnerText;
            string mode = root.SelectSingleNode("common").SelectSingleNode("mode").InnerText;

            //获取地区名，项目名称，控制器名，控制模式
            //根据项目编号，获取数据库名称 ，collectorID
            string area_id = id.Substring(0, 6);//地区编号 6位
            string project_id = id.Substring(6, 3);//项目编号 3位
            string collector_id = id.Substring(9, 2);//控制器编号 2位
            string sqlgetdatabase = "select AreaName,ProjectName from ProjectInfo where AreaCode ='" + area_id + "' and ProjectCode='" + project_id + "'";
            DataTable dt = dataaccess.GetDataTable(sqlgetdatabase, GlobalInfo.DefaultDatabase);
            string AreaName = dt.Rows[0][0].ToString();//地区名
            string ProjectName = dt.Rows[0][1].ToString();//项目名
            string database = AreaName + ProjectName;

            string sqlgetCollectorName = "select CollectorName,CollectorMode from CollectorInfo where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "' and CollectorCode ='" + collector_id + "'";
            DataTable dt2 = dataaccess.GetDataTable(sqlgetCollectorName, GlobalInfo.DefaultDatabase);
            string CollectorName = dt2.Rows[0][0].ToString();//控制器名
            string CollectorMode = dt2.Rows[0][1].ToString();//控制模式（未考虑模式变更，对数据库的影响）

            //if (mode == CollectorMode)//数据中心配置的模式如果与下位机上传的不同，不处理数据。
            //{
            //time = root.SelectSingleNode("time").InnerText; //此帧数据时间

            string time1 = root.SelectSingleNode("common").SelectSingleNode("time").InnerText;
            string year = time1.Substring(0, 4);
            string month = time1.Substring(4, 2);
            string day = time1.Substring(6, 2);
            string hour = time1.Substring(8, 2);
            string minute = time1.Substring(10, 2);
            string second = time1.Substring(12, 2);
            string time = year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;//此帧数据时间

            #region 数据或参数从xml中的提取
            string data = "";
            string strategy = "";
            string onoff = "";
            string alarm = "";
            string force = "";
            string data2 = "";
            string strategy2 = "";
            string onoff2 = "";
            string alarm2 = "";
            string force2 = "";
            string canshu1 = "";
            string canshu2 = "";
            string canshu3 = "";
            string canshu4 = "";
            if (CollectorMode == "1" || CollectorMode == "2")
            {
                if (type == "shuju")
                {
                    data = root.SelectSingleNode("common").SelectSingleNode("data").InnerText;
                    onoff = root.SelectSingleNode("common").SelectSingleNode("onoff").InnerText;
                    force = root.SelectSingleNode("common").SelectSingleNode("force").InnerText;
                    alarm = root.SelectSingleNode("common").SelectSingleNode("alarm").InnerText;
                }
                else if (type == "canshu")
                {
                    strategy = root.SelectSingleNode("common").SelectSingleNode("strategy").InnerText;
                    canshu1 = root.SelectSingleNode("common").SelectSingleNode("canshu1").InnerText;
                    canshu2 = root.SelectSingleNode("common").SelectSingleNode("canshu2").InnerText;
                    canshu3 = root.SelectSingleNode("common").SelectSingleNode("canshu3").InnerText;

                }

            }
            if (CollectorMode == "5")
            {
                if (type == "shuju")
                {
                    data2 = root.SelectSingleNode("common").SelectSingleNode("data").InnerText;
                    onoff2 = root.SelectSingleNode("common").SelectSingleNode("onoff").InnerText;
                    force2 = root.SelectSingleNode("common").SelectSingleNode("force").InnerText;
                    alarm2 = root.SelectSingleNode("common").SelectSingleNode("alarm").InnerText;
                }


                else if (type == "canshu")
                {

                    strategy2 = root.SelectSingleNode("common").SelectSingleNode("strategy").InnerText;
                    canshu1 = root.SelectSingleNode("common").SelectSingleNode("canshu1").InnerText;
                    canshu2 = root.SelectSingleNode("common").SelectSingleNode("canshu2").InnerText;
                    canshu3 = root.SelectSingleNode("common").SelectSingleNode("canshu3").InnerText;
                    canshu4 = root.SelectSingleNode("common").SelectSingleNode("canshu4").InnerText;

                }
            }
            #endregion

            //获取选中的控制器名称
            string treeViewNodeText = "";
            Action getNodeName = delegate { treeViewNodeText = GetTreeViewSelectNodeText(); };
            this.Invoke(getNodeName);

            //#region 数据的刷新展示

            //if (type == "shuju")
            //{
            //    if (collectorAreaName.ToString() + collectorProjectName.ToString() == database)
            //    {
            //        if (CollectorMode == "1" || CollectorMode == "2")
            //        {
            //            if (treeViewNodeText == CollectorName)
            //            {

            //                //刷新表格
            //                Action actionDGV = delegate { LoadDGV(database, CollectorName); };
            //                this.Invoke(actionDGV);
            //                //画曲线
            //                Action actionAxCW = delegate { Draw(database, CollectorName); };
            //                this.Invoke(actionAxCW);


            //                Action actionflash = delegate
            //                {
            //                    flashData(AreaName, ProjectName, CollectorName, CollectorMode);
            //                    this.axShockwaveFlash1.Movie = Application.StartupPath + "\\" + CollectorMode + ".swf";
            //                    this.axShockwaveFlash1.Play();
            //                };
            //                this.Invoke(actionflash);
            //            }
            //        }

            //    }

            //}


            //if (type == "canshu")
            //{
            //    if (treeViewNodeText == CollectorName)
            //    {
            //        //*********水箱模式 刷新实时数据
            //        Action actioncelv = delegate { LoadStrategy(database, CollectorName, CollectorMode); };
            //        this.Invoke(actioncelv);


            //        //*********水箱模式 刷新节能指标
            //        Action actioncanshu = delegate { LoadCanshu(database, CollectorName, CollectorMode); };
            //        this.Invoke(actioncanshu);
            //    }
            //}


            //#endregion

        }
        #endregion

        #region 计量数据和热泵处理
        private void ProcessingData(XmlDocument xd)
        {


            XmlNode root = xd.DocumentElement;
            string id = root.SelectSingleNode("common").SelectSingleNode("id").InnerText;
            string type = root.SelectSingleNode("common").SelectSingleNode("type").InnerText;
            string timestamp = root.SelectSingleNode("common").SelectSingleNode("timestamp").InnerText;
            string data = root.SelectSingleNode("common").SelectSingleNode("data").InnerText;
            //data = data.Substring(1);
            //获取地区名，项目名称，控制器名，控制模式
            //根据项目编号，获取数据库名称 ，collectorID
            string area_id = id.Substring(0, 6);//地区编号 6位
            string project_id = id.Substring(6, 3);//项目编号 3位
            string collector_id = id.Substring(9, 2);//控制器编号 2位
            string time = StampToDateTime(timestamp).ToString("yyyy-MM-dd HH:mm:ss");
            string sqlgetdatabase = "select AreaName,ProjectName from ProjectInfo where AreaCode ='" + area_id + "' and ProjectCode='" + project_id + "'";
            DataTable dt = dataaccess.GetDataTable(sqlgetdatabase, GlobalInfo.DefaultDatabase);
            string AreaName = dt.Rows[0][0].ToString();//地区名
            string ProjectName = dt.Rows[0][1].ToString();//项目名
            string database = AreaName + ProjectName;//浙江省杭州市数据库
            string Input1 = time;
            string Input2 = time;

            string sqlgetCollectorName = "select CollectorName,CollectorMode from CollectorInfo where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "' and CollectorCode ='" + collector_id + "'";
            DataTable dt2 = dataaccess.GetDataTable(sqlgetCollectorName, GlobalInfo.DefaultDatabase);
            string CollectorName = dt2.Rows[0][0].ToString();//控制器名
            string CollectorMode = dt2.Rows[0][1].ToString();//控制模式（未考虑模式变更，对数据库的影响）

            if (CollectorName != "系统采集器")//&2017.1.3-添加-各个项目中错误添加了 省+市+Index+年月 的数据表;防止此种表格乱添加,在此作判断&
            {
                CollectorName = "系统采集器";
            }

            //计量表存储
            #region 模式1数据存储

            if (type == "metering")
            {
                string[] metering = data.Split('/');

                if (CollectorMode == "1")
                {
                    for (int i = 0; i < 18; i++)//实时数据存储
                    {
                        Input1 += "','" + metering[i];
                    }
                    string sqlmetering1 = "insert into " + CollectorName + "实时 values('" + Input1 + "')";

                    dataaccess.ExeSQL(sqlmetering1, database);

                    string historyTableName1 = CollectorName + "实时" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");
                    bool boolValue1 = dataaccess.IsExistTable(historyTableName1, database);
                    if (boolValue1)//历史数据表是否存在
                    {
                        string sqlhistoryshuju = "insert into " + historyTableName1 + " (TimeStamp,Num01,Num02,Num03,Num04,Num05,Num06,Num07,Num08,Num09,Num10,Num11,Num12,Num13,Num14,Num15,Num16,Num17,Num18) values('" + Input1 + "')";
                        dataaccess.ExeSQL(sqlhistoryshuju, database);
                    }
                    else//不存在该表，先创建该表再存入数据
                    {
                        string sqlcreatTable = "create table " + historyTableName1 + " (TimeStamp varchar(50),Num01 varchar(50),Num02 varchar(50),Num03 varchar(50),Num04 varchar(50),Num05 varchar(50),Num06 varchar(50),Num07 varchar(50),Num08 varchar(50),Num09 varchar(50),Num10 varchar(50),Num11 varchar(50),Num12 varchar(50),Num13 varchar(50),Num14 varchar(50),Num15 varchar(50),Num16 varchar(50),Num17 varchar(50),Num18 varchar(50))" + @"insert into " + historyTableName1 + " (TimeStamp,Num01,Num02,Num03,Num04,Num05,Num06,Num07,Num08,Num09,Num10,Num11,Num12,Num13,Num14,Num15,Num16,Num17,Num18) values('" + Input1 + "')";
                        dataaccess.ExeSQL(sqlcreatTable, database);
                    }

                    for (int i = 18; i < metering.Length; i++)//节能指标存储
                    {
                        Input2 += "','" + metering[i];
                    }

                    string sqlmetering2 = "insert into " + CollectorName + "Index values('" + Input2 + "')";

                    dataaccess.ExeSQL(sqlmetering2, database);

                    string historyTableName2 = CollectorName + "Index" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");
                    bool boolValue2 = dataaccess.IsExistTable(historyTableName2, database);
                    if (boolValue2)//历史数据表是否存在
                    {
                        string sqlhistoryshuju = "insert into " + historyTableName2 + " (TimeStamp,Esys,Quse,Qc,Qss,mCO2,mSO2,mNOx,mfc) values('" + Input2 + "')";
                        dataaccess.ExeSQL(sqlhistoryshuju, database);
                    }
                    else//不存在该表，先创建该表再存入数据
                    {
                        string sqlcreatTable = "create table " + historyTableName2 + " (TimeStamp varchar(50),Esys float,Quse float,Qc float,Qss float,mCO2 float,mSO2 float,mNOx float,mfc float)" + @"insert into " + historyTableName2 + " (TimeStamp,P,I,Mv,Tcw,Thw,AllMv,Esys,Quse,Qss,mCO2,mSO2,mNOx,mfc) values('" + Input2 + "')";
                        dataaccess.ExeSQL(sqlcreatTable, database);
                    }
                }
            #endregion

                #region 模式2数据存储

                if (CollectorMode == "2")
                {
                    for (int i = 0; i < 10; i++)//实时数据存储
                    {
                        Input1 += "','" + metering[i];
                    }
                    string sqlmetering3 = "insert into " + CollectorName + "实时 values('" + Input1 + "')";

                    dataaccess.ExeSQL(sqlmetering3, database);

                    string historyTableName3 = CollectorName + "实时" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");
                    bool boolValue3 = dataaccess.IsExistTable(historyTableName3, database);
                    if (boolValue3)//历史数据表是否存在
                    {
                        string sqlhistoryshuju = "insert into " + historyTableName3 + " (TimeStamp,Num01,Num02,Num03,Num04,Num05,Num06,Num07,Num08,Num09,Num10) values('" + Input1 + "')";
                        dataaccess.ExeSQL(sqlhistoryshuju, database);
                    }
                    else//不存在该表，先创建该表再存入数据
                    {
                        string sqlcreatTable = "create table " + historyTableName3 + " (TimeStamp varchar(50),Num01 varchar(50),Num02 varchar(50),Num03 varchar(50),Num04 varchar(50),Num05 varchar(50),Num06 varchar(50),Num07 varchar(50),Num08 varchar(50),Num09 varchar(50),Num10 varchar(50))" + @"insert into " + historyTableName3 + " (TimeStamp,Num01,Num02,Num03,Num04,Num05,Num06,Num07,Num08,Num09,Num10) values('" + Input1 + "')";
                        dataaccess.ExeSQL(sqlcreatTable, database);
                    }

                    for (int i = 10; i < metering.Length; i++)//节能指标存储
                    {
                        Input2 += "','" + metering[i];
                    }

                    string sqlmetering4 = "insert into " + CollectorName + "Index values('" + Input2 + "')";

                    dataaccess.ExeSQL(sqlmetering4, database);

                    string historyTableName4 = CollectorName + "Index" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");
                    bool boolValue4 = dataaccess.IsExistTable(historyTableName4, database);
                    if (boolValue4)//历史数据表是否存在
                    {
                        string sqlhistoryshuju = "insert into " + historyTableName4 + " (TimeStamp,P, I, V1, V2, V3, offon1, offon2) values('" + Input2 + "')";
                        dataaccess.ExeSQL(sqlhistoryshuju, database);
                    }
                    else//不存在该表，先创建该表再存入数据
                    {
                        string sqlcreatTable = "create table " + historyTableName4 + " (TimeStamp varchar(50),P float,I float,V1 float,V2 float,V3 float,offon1 float,offon2 float)" + @"insert into " + historyTableName4 + " (TimeStamp,P, I, V1, V2, V3, offon1, offon2) values('" + Input2 + "')";
                        dataaccess.ExeSQL(sqlcreatTable, database);
                    }
                }
                #endregion

                #region 模式4数据存储

                if (CollectorMode == "4")
                {
                    for (int i = 0; i < 25; i++)//实时数据存储
                    {
                        Input1 += "','" + metering[i];
                    }
                    string sqlmetering5 = "insert into " + CollectorName + "实时 values('" + Input1 + "')";

                    dataaccess.ExeSQL(sqlmetering5, database);

                    string historyTableName5 = CollectorName + "实时" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");
                    bool boolValue5 = dataaccess.IsExistTable(historyTableName5, database);
                    if (boolValue5)//历史数据表是否存在
                    {
                        string sqlhistoryshuju = "insert into " + historyTableName5 + " (TimeStamp,Num01,Num02,Num03,Num04,Num05,Num06,Num07,Num08,Num09,Num10,Num11,Num12,Num13,Num14,Num15,Num16,Num17,Num18,Num19,Num20,Num21,Num22,Num23,Num24,Num25) values('" + Input1 + "')";
                        dataaccess.ExeSQL(sqlhistoryshuju, database);
                    }
                    else//不存在该表，先创建该表再存入数据
                    {
                        string sqlcreatTable = "create table " + historyTableName5 + " (TimeStamp varchar(50),Num01 varchar(50),Num02 varchar(50),Num03 varchar(50),Num04 varchar(50),Num05 varchar(50),Num06 varchar(50),Num07 varchar(50),Num08 varchar(50),Num09 varchar(50),Num10 varchar(50),Num11 varchar(50),Num12 varchar(50),Num13 varchar(50),Num14 varchar(50),Num15 varchar(50),Num16 varchar(50),Num17 varchar(50),Num18 varchar(50),Num19 varchar(50),Num20 varchar(50),Num21 varchar(50),Num22 varchar(50),Num23 varchar(50),Num24 varchar(50),Num25 varchar(50))" + @"insert into " + historyTableName5 + " (TimeStamp,Num01,Num02,Num03,Num04,Num05,Num06,Num07,Num08,Num09,Num10,Num11,Num12,Num13,Num14,Num15,Num16,Num17,Num18,Num19,Num20,Num21,Num22,Num23,Num24,Num25) values('" + Input1 + "')";
                        dataaccess.ExeSQL(sqlcreatTable, database);
                    }

                    for (int i = 25; i < metering.Length; i++)//节能指标存储
                    {
                        Input2 += "','" + metering[i];
                    }

                    string sqlmetering6 = "insert into " + CollectorName + "Index values('" + Input2 + "')";

                    dataaccess.ExeSQL(sqlmetering6, database);

                    string historyTableName6 = CollectorName + "Index" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");
                    bool boolValue6 = dataaccess.IsExistTable(historyTableName6, database);
                    if (boolValue6)//历史数据表是否存在
                    {
                        string sqlhistoryshuju = "insert into " + historyTableName6 + " (TimeStamp,Allc,AllMv,AllEsys,AllQc,AllQhp,AllQtc,AllQsh,AllQuse,AllQbm,AllQu,AllQss,Allmco2,Allmso2,Allmnox,Allmfc,fc,COPsys,COPr) values('" + Input2 + "')";
                        dataaccess.ExeSQL(sqlhistoryshuju, database);
                    }
                    else//不存在该表，先创建该表再存入数据
                    {
                        string sqlcreatTable = "create table " + historyTableName6 + " (TimeStamp varchar(50),Allc float,AllMv float,AllEsys float,AllQc float,AllQhp float,AllQtc float,AllQsh float,AllQuse float,AllQbm float,AllQu float,AllQss float,Allmco2 float,Allmso2 float,Allmnox float,Allmfc float,fc float,COPsys float,COPr float)" + @"insert into " + historyTableName6 + " (TimeStamp,Allc,AllMv,AllEsys,AllQc,AllQhp,AllQtc,AllQsh,AllQuse,AllQbm,AllQu,AllQss,Allmco2,Allmso2,Allmnox,Allmfc,fc,COPsys,COPr) values('" + Input2 + "')";
                        dataaccess.ExeSQL(sqlcreatTable, database);
                    }
                }
                #endregion

                #region 模式5数据存储

                if (CollectorMode == "5")
                {
                    for (int i = 0; i < 10; i++)//实时数据存储
                    {
                        Input1 += "','" + metering[i];
                    }
                    string sqlmetering7 = "insert into " + CollectorName + "实时 values('" + Input1 + "')";

                    dataaccess.ExeSQL(sqlmetering7, database);

                    string historyTableName7 = CollectorName + "实时" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");
                    bool boolValue7 = dataaccess.IsExistTable(historyTableName7, database);
                    if (boolValue7)//历史数据表是否存在
                    {
                        string sqlhistoryshuju = "insert into " + historyTableName7 + " (TimeStamp,Num01,Num02,Num03,Num04,Num05,Num06,Num07,Num08,Num09,Num10) values('" + Input1 + "')";
                        dataaccess.ExeSQL(sqlhistoryshuju, database);
                    }
                    else//不存在该表，先创建该表再存入数据
                    {
                        string sqlcreatTable = "create table " + historyTableName7 + " (TimeStamp varchar(50),Num01 varchar(50),Num02 varchar(50),Num03 varchar(50),Num04 varchar(50),Num05 varchar(50),Num06 varchar(50),Num07 varchar(50),Num08 varchar(50),Num09 varchar(50),Num10 varchar(50))" + @"insert into " + historyTableName7 + " (TimeStamp,Num01,Num02,Num03,Num04,Num05,Num06,Num07,Num08,Num09,Num10) values('" + Input1 + "')";
                        dataaccess.ExeSQL(sqlcreatTable, database);
                    }

                    for (int i = 10; i < metering.Length; i++)//节能指标存储
                    {
                        Input2 += "','" + metering[i];
                    }

                    string sqlmetering8 = "insert into " + CollectorName + "Index values('" + Input2 + "')";

                    dataaccess.ExeSQL(sqlmetering8, database);

                    string historyTableName8 = CollectorName + "Index" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");
                    bool boolValue8 = dataaccess.IsExistTable(historyTableName8, database);
                    if (boolValue8)//历史数据表是否存在
                    {
                        string sqlhistoryshuju = "insert into " + historyTableName8 + " (TimeStamp,Allc, AllEsys, AllQuse, Qss, mco2, mso2, mnox, mfc) values('" + Input2 + "')";
                        dataaccess.ExeSQL(sqlhistoryshuju, database);
                    }
                    else//不存在该表，先创建该表再存入数据
                    {
                        string sqlcreatTable = "create table " + historyTableName8 + " (TimeStamp varchar(50),Allc float, AllEsys float, AllQuse float, Qss float, mco2 float, mso2 float, mnox float, mfc float)" + @"insert into " + historyTableName8 + " (TimeStamp,AllEsys, AllEsys, AllQuse, Qss, mco2, mso2, mnox, mfc) values('" + Input2 + "')";
                        dataaccess.ExeSQL(sqlcreatTable, database);
                    }
                }
                #endregion
            }
        }
        //计量表刷新
        //if (collectorAreaName.ToString() + collectorProjectName.ToString() == database)
        // {
        //Action actionMeter = delegate { LoadMetering(database, CollectorName, CollectorMode); };
        //this.Invoke(actionMeter);

        //if (CollectorMode == "1")
        //{
        //Action actionflash = delegate
        //{
        //flashData(AreaName, ProjectName, CollectorName, CollectorMode);
        //   this.axShockwaveFlash1.Movie = Application.StartupPath + "\\" + CollectorMode + ".swf";
        // this.axShockwaveFlash1.Play();
        //   };
        //this.Invoke(actionflash1);
        //   }
        //     else if (CollectorMode == "2")
        //       {

        //Action actionflash2 = delegate
        //             {
        //flashData(AreaName, ProjectName, CollectorName, CollectorMode);
        // this.axShockwaveFlash2.Movie = Application.StartupPath + "\\" + CollectorMode + ".swf";
        //this.axShockwaveFlash2.Play();
        //              };
        //this.Invoke(actionflash2);
        //            }
        //         }
        //         }
        //        }

        //时间戳转为C#格式时间
        private static DateTime StampToDateTime(string timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dateTimeStart.Add(toNow);
        }
        #endregion

        #region 刷新metering数据
        private void LoadMetering(string database, string collectorName, string CollectorMode)
        {
            if (collectorName != "")
            {
                try
                {
                    string sql1 = "select top 1 * from " + collectorName + "实时 order by TimeStamp desc";
                    DataTable dt1 = dataaccess.GetDataTable(sql1, database);
                    string sql2 = "select top 1 * from " + collectorName + "Index order by TimeStamp desc";
                    DataTable dt2 = dataaccess.GetDataTable(sql2, database);
                    if (dt1.Rows.Count != 0)//参数表不为空
                    {

                        if (CollectorMode == "1")
                        {

                            // X1.Text = (Convert.ToDouble(dt1.Rows[0][1].ToString()) / 1000).ToString("0.00");
                            // X2.Text = dt1.Rows[0][2].ToString();
                            //  X3.Text = dt1.Rows[0][3].ToString();
                            //  X4.Text = dt1.Rows[0][4].ToString();
                            //  X5.Text = dt1.Rows[0][5].ToString();
                            //  X6.Text = (Convert.ToDouble(dt1.Rows[0][6].ToString()) / 1000).ToString("0.00");
                            //  X7.Text = (Convert.ToDouble(dt1.Rows[0][7].ToString()) / 1000).ToString("0.00");
                            //  X8.Text = (Convert.ToDouble(dt1.Rows[0][8].ToString()) / 1000).ToString("0.00");
                            //  X9.Text = (Convert.ToDouble(dt1.Rows[0][9].ToString()) / 1000).ToString("0.00");
                            //  X10.Text = (Convert.ToDouble(dt1.Rows[0][10].ToString()) / 1000).ToString("0.00");
                            //  Y1.Text = (Convert.ToDouble(dt2.Rows[0][1].ToString()) / 1000).ToString("0.00");
                            //  Y2.Text = (Convert.ToDouble(dt2.Rows[0][2].ToString()) / 1000).ToString("0.00");
                            //  Y3.Text = (Convert.ToDouble(dt2.Rows[0][3].ToString()) / 1000).ToString("0.00");
                            //  Y4.Text = (Convert.ToDouble(dt2.Rows[0][4].ToString()) / 1000).ToString("0.00");
                            //  Y5.Text = (Convert.ToDouble(dt2.Rows[0][5].ToString()) / 1000).ToString("0.00");
                            //  Y6.Text = (Convert.ToDouble(dt2.Rows[0][6].ToString()) / 1000).ToString("0.00");
                            //  Y7.Text = (Convert.ToDouble(dt2.Rows[0][7].ToString()) / 1000).ToString("0.00");
                            //  Y8.Text = (Convert.ToDouble(dt2.Rows[0][8].ToString()) / 1000).ToString("0.00");




                        }

                        if (CollectorMode == "2")
                        {

                            // W1.Text = (Convert.ToDouble(dt1.Rows[0][1].ToString()) / 1000).ToString("0.00");
                            // W2.Text = dt1.Rows[0][2].ToString();
                            // W3.Text = dt1.Rows[0][3].ToString();
                            //  W4.Text = dt1.Rows[0][4].ToString();
                            //  W5.Text = dt1.Rows[0][5].ToString();
                            //W6.Text = (Convert.ToDouble(dt1.Rows[0][6].ToString()) / 1000).ToString("0.00");
                            //  W7.Text = (Convert.ToDouble(dt1.Rows[0][7].ToString()) / 1000).ToString("0.00");
                            //  W8.Text = (Convert.ToDouble(dt1.Rows[0][8].ToString()) / 1000).ToString("0.00");
                            //  W9.Text = (Convert.ToDouble(dt1.Rows[0][9].ToString()) / 1000).ToString("0.00");
                            //  V1.Text = (Convert.ToDouble(dt2.Rows[0][1].ToString()) / 1000).ToString("0.00");
                            //  V4.Text = (Convert.ToDouble(dt2.Rows[0][2].ToString()) / 1000).ToString("0.00");
                            //  V5.Text = (Convert.ToDouble(dt2.Rows[0][3].ToString()) / 1000).ToString("0.00");

                        }

                        if (CollectorMode == "4")
                        {




                        }

                        if (CollectorMode == "5")
                        {






                        }

                    }
                }
                catch { }

            }

        }
        #endregion

        #region 十六进制转二进制
        private static string toTwo(string input1)
        {
            string a = "";
            switch (input1)
            {
                case "0":
                    a = "0000";
                    break;

                case "1":
                    a = "0001";
                    break;
                case "2":
                    a = "0010";
                    break;
                case "3":
                    a = "0011";
                    break;
                case "4":
                    a = "0100";
                    break;
                case "5":
                    a = "0101";
                    break;
                case "6":
                    a = "0110";
                    break;
                case "7":
                    a = "0111";
                    break;
                case "8":
                    a = "1000";
                    break;
                case "9":
                    a = "1001";
                    break;
                case "a":
                    a = "1010";
                    break;
                case "b":
                    a = "1011";
                    break;
                case "c":
                    a = "1100";
                    break;
                case "d":
                    a = "1101";
                    break;
                case "e":
                    a = "1110";
                    break;
                case "f":
                    a = "1111";
                    break;

            }

            return a;
        }
        private static string toTwo1(string input2)
        {
            string a = "";
            switch (input2)
            {
                case "0":
                    a = "0000";
                    break;

                case "1":
                    a = "0001";
                    break;
                case "2":
                    a = "0010";
                    break;
                case "3":
                    a = "0011";
                    break;
                case "4":
                    a = "0100";
                    break;
                case "5":
                    a = "0101";
                    break;
                case "6":
                    a = "0110";
                    break;
                case "7":
                    a = "0111";
                    break;
                case "8":
                    a = "1000";
                    break;
                case "9":
                    a = "1001";
                    break;
                case "a":
                    a = "1010";
                    break;
                case "b":
                    a = "1011";
                    break;
                case "c":
                    a = "1100";
                    break;
                case "d":
                    a = "1101";
                    break;
                case "e":
                    a = "1110";
                    break;
                case "f":
                    a = "1111";
                    break;

            }

            return a;
        }
        #endregion

        #region 获取当前选中树节点名称

        /// <summary>
        /// 获取当前选中的树状图节点名称 （用于接收线程中的使用）
        /// </summary>
        /// <returns></returns>
        private string GetTreeViewSelectNodeText()
        {
            string a = this.treeView1.SelectedNode.Text;
            return a;
        }

        #endregion

        #region 加载采集数据表
        /// <summary>
        /// 刷新 采集数据表格  取最新二十条数据
        /// </summary>
        /// <param name="database">数据库名</param>
        /// <param name="collectorName">采集器名</param>
        private void LoadDGV(string database, string collectorName)
        {

            this.DGVMain.DataSource = null;


            try
            {
                if (collectorCollectorMode.ToString() == "1")
                {
                    string sql = "select top 20 TimeStamp as '时间戳',Num01 as '集热器出口温度(℃)',Num02 as '集热器进口温度(℃)',Num03 as '水箱温度(℃)',Num04 as '供水温度(℃)',Num05 as '回水温度(℃)',Num06 as '室内温度(℃)',Num07 as '集热效率(%)',Num08 as '系统实时供水流量(m3/h)',Num09 as '集热供热流量（m3/h)',Num10 as '系统实时电流(A)',Num11 as '系统实时电压(V)',Num12 as '系统实时功率(W)',Num13 as '水箱液位高度(%)',Num14 as '环境湿度(RH%)',Num15 as '环境光照度(Lux)',Num16 as '供暖水泵开关量',Num17 as '集热水泵开关量' ,Num18 as '热泵开关量' from " + collectorName + "实时 order by TimeStamp desc";
                    DataTable dt = dataaccess.GetDataTable(sql, database);
                    this.DGVMain.DataSource = dt;
                }
                if (collectorCollectorMode.ToString() == "2")
                {
                    string sql = "select top 20 TimeStamp as '时间戳',Num01 as '温度1(℃)',Num02 as '温度2(℃)',Num03 as '温度3(℃)',Num04 as '温度4(℃)',Num05 as '温度5(℃)',Num06 as '温度6(℃)',Num07 as '温度7(℃)',Num08 as '温度8(℃)',Num09'湿球湿度(RH%)' ,Num10'系统累计耗电量(kwh)'  from " + collectorName + "实时 order by TimeStamp desc";
                    DataTable dt = dataaccess.GetDataTable(sql, database);
                    this.DGVMain.DataSource = dt;
                }
                if (collectorCollectorMode.ToString() == "4")
                {
                    string sql = "select top 20 TimeStamp as '时间戳',Num01 as '冷水上水温度(℃)',Num02 as '环境温度(℃)',Num03 as '回水温度(℃)',Num04 as '1号水箱温度(℃)',Num05 as '热水供水温度(℃)',Num06 as '2号水箱温度(℃)',Num07 as '热泵供水温度(℃)',Num08 as '热泵进水温度(℃)',Num09'冷水上水流量(m3)' ,Num10'回水流量(m3)',Num11'热水供水流量(m3)',Num12'热泵循环流量(m3)',Num13'冷水的耗水量(m3)',Num14'回水的耗水量(m3)',Num15'供水的耗水量(m3)',Num16'热泵的耗水量(m3)',Num17'电度值(KH)',Num18'总的有功功率(W)',Num19'三相平均电流(A)',Num20'三相平均电压(V)',Num21'混水阀开关',Num22'回水阀开关',Num23'水泵1开关',Num24'水泵2开关',Num25'热泵开关' from " + collectorName + "实时 order by TimeStamp desc";
                    DataTable dt = dataaccess.GetDataTable(sql, database);
                    this.DGVMain.DataSource = dt;
                }
                if (collectorCollectorMode.ToString() == "5")
                {
                    string sql = "select top 20 TimeStamp as '时间戳',Num01 as '冷水进水温度(℃)',Num02 as '热水出水温度(℃)',Num03 as '集热器进口温度(℃)',Num04 as '集热器出口温度(℃)',Num05 as '环境温度(℃)',Num06 as '实时功率(KW)',Num07 as '实时电流(A)',Num08 as '实时电压(V)',Num09 as '平均流量(m3/h)',Num10 as '水泵开关量' from " + collectorName + "实时 order by TimeStamp desc";
                    DataTable dt = dataaccess.GetDataTable(sql, database);
                    this.DGVMain.DataSource = dt;
                }
                //光伏制冷
                if (collectorCollectorMode.ToString() == "8")
                {
                    string sql = "select top 20 * from " + collectorName + "实时 order by TimeStamp desc";
                    DataTable dt = dataaccess.GetDataTable(sql, database);
                    this.DGVMain.DataSource = dt;
                }




            }
            catch
            {

            }


        }

        #endregion

        #region 箱画曲线

        private void Draw(string database, string collectorName)
        {

            //光伏制冷项目处理方案
            if (collectorCollectorMode.ToString() == "8")
            {
                
                try
                {
                    //取出关键值【温度、流量等】
                    //从数据库提取最新的20条数据
                    string sql = "select top 20 *  from " + collectorName + "实时 order by TimeStamp desc";
                    DataTable dt = dataaccess.GetDataTable(sql, database);
                    
                    int rowCount = dt.Rows.Count;
                    if (rowCount != 0)
                    {
                        //曲线的时间轴
                        List<DateTime> xTime = new List<DateTime>(rowCount);
                        for (int j = 0; j < rowCount; j++)
                        {
                            if ((dt.Rows[j][0].ToString()) != null)
                            {
                                xTime.Add(Convert.ToDateTime(dt.Rows[j][0].ToString()));
                            }
                        }

                        //添加CH4、CH5、CH11、CH12、CH13、CH14
                        for (int k = 24; k <= 29; k++)
                        {
                            this.chart1.Series[k-24].Points.Clear();//清除历史点
                            for (int i = rowCount-1; i >=0; i--)
                            {
                                try
                                {
                                    double data = Convert.ToDouble(dt.Rows[i][k].ToString());
                                    this.chart1.Series[k - 24].Points.AddXY(xTime[i].ToString(), data);
                                }
                                catch
                                { continue; }
                            }

                        }

                        //添加CH3,CH21-CH27
                        for (int p = 35; p <= 42; p++)
                        {
                            this.chart1.Series[p - 29].Points.Clear();//清除历史点
                            for (int i = rowCount - 1; i >= 0; i--)
                            {
                                try
                                {
                                    double data = Convert.ToDouble(dt.Rows[i][p].ToString());
                                    this.chart1.Series[p - 29].Points.AddXY(xTime[i].ToString(), data);
                                }
                                catch
                                { continue; }
                            }

                        }



                    }

                }
                catch (Exception e)
                { MessageBox.Show(e.Message + "\n画曲线Draw()出错"); }
            }



        }
        #endregion

        #region 实时数据显示[在pannel页显示最新数据]
        ///// <summary>
        ///// 刷新项目信息，取最新一条数据
        ///// </summary>
        private void LoadStrategy(string database, string collectorName, string collectorMode)
        {
            if (collectorName != "")//供暖
            {
                try
                {
                    string sql = "select top 1 * from " + collectorName + "实时 order by TimeStamp desc";
                    DataTable dt = dataaccess.GetDataTable(sql, database);

                    if (dt.Rows.Count != 0)//数据表不为空
                    {
                        //光伏制冷模式
                        if (collectorMode == "8")
                        {
                            DataRow dr = dt.Rows[0];
                            this.lblYHProject.Text = database;//获取项目名称
                            this.lblTimeCanShu1.Text = dr[0].ToString();//参数获取时间


                            Z1.Text = dr[1].ToString();
                            Z2.Text = dr[2].ToString();
                            Z3.Text = dr[3].ToString();
                            Z4.Text = dr[4].ToString();
                            Z5.Text = dr[5].ToString();
                            Z6.Text = dr[6].ToString();
                            Z7.Text = dr[7].ToString();
                            Z8.Text = dr[8].ToString();
                            Z9.Text = dr[9].ToString();

                            Z10.Text = dr[10].ToString();
                            Z11.Text = dr[11].ToString();
                           
                            Z12.Text = dr[12].ToString();
                            Z13.Text = dr[13].ToString();
                            Z14.Text = dr[14].ToString();
                            Z15.Text = dr[15].ToString();
                            Z16.Text = dr[16].ToString();
                            Z17.Text = dr[17].ToString();
                            Z18.Text = dr[18].ToString();
                            Z19.Text = dr[19].ToString();
                            Z20.Text = dr[20].ToString();
                            Z21.Text = dr[21].ToString();
                            Z22.Text = dr[22].ToString();
                            Z23.Text = dr[23].ToString();
                            Z24.Text = dr[24].ToString();
                            Z25.Text = dr[25].ToString();
                            Z26.Text = dr[26].ToString();
                            Z27.Text = dr[27].ToString();
                            Z28.Text = dr[28].ToString();
                            Z29.Text = dr[29].ToString();
                            Z30.Text = dr[30].ToString();
                            Z31.Text = dr[31].ToString();
                            Z32.Text = dr[32].ToString();
                            Z33.Text = dr[33].ToString();
                            Z34.Text = dr[34].ToString();
                            Z35.Text = dr[35].ToString();
                            Z36.Text = dr[36].ToString();
                            Z37.Text = dr[37].ToString();
                            Z38.Text = dr[38].ToString();
                            Z39.Text = dr[39].ToString();
                            Z40.Text = dr[40].ToString();
                            Z41.Text = dr[41].ToString();
                            Z42.Text = dr[42].ToString();
                            Z43.Text = dr[43].ToString();
                            Z44.Text = dr[44].ToString();
                            Z45.Text = dr[45].ToString();
                            Z46.Text = dr[46].ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("抱歉，暂时没有实时数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch
                {
                }


            }
            else//参数表为空
            {
                MessageBox.Show("请现在左侧选择查看的控制器！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 参数显示
        /// <summary>
        ///刷新项目信息，取最新一条能耗数据
        /// </summary>
        private void LoadCanshu(string database, string collectorName, string collectorMode)
        {
            if (collectorName != "")
            {
                try
                {
                    string sql = "select top 1 * from " + collectorName + "参数 order by TimeStamp desc";
                    DataTable dt = dataaccess.GetDataTable(sql, database);
                    if (dt.Rows.Count != 0)//能耗表不为空
                    {
                        if (collectorMode == "8")
                        {
                            DataRow dr = dt.Rows[0];
                            if (dr[0] != null)
                            {
                                //时间
                            }
                            if (dr[1] != null)
                            {
                                CanshuValue.setTemp = dr[1].ToString();
                                this.C1.Text = CanshuValue.setTemp;
                            }
                            else { this.C1.Text = "c1"; }

                            if (dr[5] != null)
                            {
                                CanshuValue.switch1 = dr[5].ToString();

                                if (CanshuValue.switch1 == "0")
                                {
                                   
                                    this.pbX1.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";

                                }
                                else if (CanshuValue.switch1 == "1")
                                {
                                    this.pbX1.ImageLocation = Application.StartupPath + "\\强制开关\\Open.png";
                                }
                             
                            }
                            else
                            {
                                this.pbX1.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";

                            }

                            if (dr[6] != null)
                            {
                                

                                if (dr[6].ToString()== "0")
                                {

                                    this.pbX2.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";

                                }
                                else if (dr[6].ToString()== "1")
                                {
                                    this.pbX2.ImageLocation = Application.StartupPath + "\\强制开关\\Open.png";
                                }
                            
                            }
                            else
                            {
                                this.pbX2.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";

                            }

                            if (dr[7] != null)
                            {
                                

                                if (dr[7].ToString() == "0")
                                {

                                    this.pbX3.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";

                                }
                                else if (dr[7].ToString() == "1")
                                {
                                    this.pbX3.ImageLocation = Application.StartupPath + "\\强制开关\\Open.png";
                                }
                              
                            }
                            else
                            {
                                this.pbX3.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";

                            }

                            if (dr[8] != null)
                            {
                                if (dr[8].ToString()=="1")
                                {
                                    Action changSwitch = delegate
                                    {
                                        this.meter1.Value = 1;
                                    };
                                    this.Invoke(changSwitch);
                                }
                                if (dr[8].ToString() == "2")
                                {
                                    Action changSwitch = delegate
                                    {
                                        this.meter1.Value = 2;
                                    };
                                    this.Invoke(changSwitch);
                                }
                                if (dr[8].ToString() == "3")
                                {
                                    Action changSwitch = delegate
                                    {
                                        this.meter1.Value = 3;
                                    };
                                    this.Invoke(changSwitch);
                                }
                            }

                        }


                    }

                }
                catch (Exception x)
                {
                    MessageBox.Show(x.ToString() + "\nLoadCanshu函数发生错误");
                }

            }
            else
            {
                MessageBox.Show("请现在左侧树状图选择要监测的控制器！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        #endregion

        #region 异步发送函数jmk
        /// <summary>
        /// 异步发送函数
        /// </summary>
        /// <param name="user"></param>
        /// <param name="command"></param>
        private delegate void SendToClientDelegate(User user, byte[] command);
        private static void AsyncSendToClient(User user, byte[] command)
        {
            SendToClientDelegate d = new SendToClientDelegate(Communication.SendToClient);
            IAsyncResult result = d.BeginInvoke(user, command, null, null);

            while (result.IsCompleted == false)//轮询
            {
                if (GlobalInfo.IsExit == true)
                {
                    break;
                }
                Thread.Sleep(100);
            }
            d.EndInvoke(result);
        }
        #endregion

        #region 树状图右键
        private void 添加地区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAreaConfiguration facf = new FormAreaConfiguration();
            facf.ShowDialog();

            //添加新的省份  b向树状图添加省市节点
            if (facf.IsNewProvince == true)
            {
                TreeNode ProvinceNode = new TreeNode(facf.Province);
                ProvinceNode.SelectedImageIndex = 4;
                ProvinceNode.ImageIndex = 4;
                TreeNode CityNode = new TreeNode(facf.City);
                CityNode.ImageIndex = 2;
                CityNode.SelectedImageIndex = 2;
                ProvinceNode.Nodes.Add(CityNode);
                treeView1.SelectedNode.Nodes.Add(ProvinceNode);
                facf.IsNewProvince = false;//复位
            }
            //添加新的市  b向树状图添加市节点
            if (facf.IsNewCity == true)
            {
                TreeNode CityNode = new TreeNode(facf.City);
                CityNode.ImageIndex = 2;
                CityNode.SelectedImageIndex = 2;
                foreach (TreeNode tn in treeView1.SelectedNode.Nodes)//!!!!!!!!!!
                {
                    if (tn.Text == facf.Province)
                    {
                        tn.Nodes.Add(CityNode);
                    }
                }
                facf.IsNewCity = false;//复位
            }
        }

        private void 添加项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //根据省市获取地区编码
            string Province = treeView1.SelectedNode.Parent.Text;
            string City = treeView1.SelectedNode.Text;
            string AreaCode = "";
            try
            {
                string SQL_AreaInfo = @"select Code from AreaInfo where Province='" + Province + "' and City='" + City + "'";
                DataSet ds = dataaccess.GetDataSet(SQL_AreaInfo, GlobalInfo.DefaultDatabase);
                DataTable dt = ds.Tables[0];
                AreaCode = dt.Rows[0][0].ToString();
            }
            catch
            {
            }
            FormProjectConfiguration fc = new FormProjectConfiguration(Province, City, AreaCode);
            fc.ShowDialog();

            //新建成功  4 向树状图中添加节点(项目节点)
            if (GlobalInfo.IsNewProject == true)
            {
                TreeNode ProjectNode = new TreeNode(fc.ProjectName);
                ProjectNode.ImageIndex = 0;
                ProjectNode.SelectedImageIndex = 0;
                //添加项目只能从点击地区的时候进入 所以 treeView1.SelectedNode必是一个地区
                treeView1.SelectedNode.Nodes.Add(ProjectNode);
                //还原该变量
                GlobalInfo.IsNewProject = false;
            }
        }

        private void 添加控制器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AreaName = treeView1.SelectedNode.Parent.Parent.Text + treeView1.SelectedNode.Parent.Text;//地区名
            string ProjectName = treeView1.SelectedNode.Text;//项目名
            FormCollectorConfiguration fcc = new FormCollectorConfiguration(ProjectName, AreaName);//传入选中的项目名称 地区名称
            fcc.ShowDialog();
            //新建成功  3 向树状图中添加节点(采集器节点)
            if (GlobalInfo.IsNewCollector == true)
            {
                //如果涉及多个城市
                //此处要先判断对应的地区，在该地区下添加项目
                TreeNode CollectorNode = new TreeNode(fcc.CollectorName);
                CollectorNode.ImageIndex = 3;
                CollectorNode.SelectedImageIndex = 3;
                //添加项目只能从点击地区的时候进入 所以 treeView1.SelectedNode必是一个地区
                treeView1.SelectedNode.Nodes.Add(CollectorNode);
                //还原该变量
                GlobalInfo.IsNewCollector = false;
            }
        }
        private void 删除地区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region 删除省份
            if (treeView1.SelectedNode.ImageIndex == 4)
            {
                if (DialogResult.OK == MessageBox.Show("确认要删除该地区？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    if (DialogResult.OK == MessageBox.Show("删除地区后将丢失该地区的一切数据！确认要删除该地区？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    {
                        string Province = treeView1.SelectedNode.Text;

                        //0 树状图中删除省份节点 在form1中完成
                        //1 删除该省份下的项目数据库文件（删除数据库）
                        //2 删除该省份下的采集点
                        //3 删除该省份下的采集器
                        //4 删除该省份下的项目
                        //5 删除该省份下的项目信息表
                        //6 删除该省份下的地区表
                        //7 重新加载树状图
                        try
                        {
                            this.treeView1.SelectedNode.Remove();
                            string SQL_SelectProjectInfo = @"select AreaName,ProjectName from ProjectInfo where AreaName like'" + Province + "%'";
                            DataSet ds_ProjectInfo = dataaccess.GetDataSet(SQL_SelectProjectInfo, GlobalInfo.DefaultDatabase);
                            DataTable dt_ProjectInfo = ds_ProjectInfo.Tables[0];
                            int ProjectNum = dt_ProjectInfo.Rows.Count;

                            string DataBaseName = dt_ProjectInfo.Rows[0][0].ToString() + dt_ProjectInfo.Rows[0][1].ToString();//项目数据名称=地区名称+项目名称
                            string SQL_DeleteDataBase = @"drop database " + DataBaseName;
                            //1 删除该省份下的项目数据库文件（删除数据库）
                            dataaccess.ExeSQL(SQL_DeleteDataBase, GlobalInfo.DefaultDatabase);

                            //2 删除该省份下的采集点
                            //不需要判断是否存在该项内容 若不存在该内容则0行受到影响
                            string SQL_deleteProjectPoint = @"delete from CollectPointInfo where AreaName like'" + Province + "%'";
                            if (0 == dataaccess.ExeSQL(SQL_deleteProjectPoint, GlobalInfo.DefaultDatabase))
                            {
                                //3 删除该省份下的采集器
                                string SQL_deleteCollector = @"delete from CollectorInfo where AreaName like'" + Province + "%'";
                                if (dataaccess.ExeSQL(SQL_deleteCollector, GlobalInfo.DefaultDatabase) == 0)
                                {
                                    //4 删除该省份下的项目
                                    string SQL_deleteProject = @"delete from ProjectInfo where AreaName like'" + Province + "%'";
                                    if (dataaccess.ExeSQL(SQL_deleteProject, GlobalInfo.DefaultDatabase) == 0)
                                    {
                                        //5 删除该省份下的项目信息表
                                        string SQL_deleteProjectDetail = @"delete from ProjectDetailInfo where AreaName like'" + Province + "%'";
                                        if (dataaccess.ExeSQL(SQL_deleteProjectDetail, GlobalInfo.DefaultDatabase) == 0)
                                        {
                                            //6 删除该省份下的地区表
                                            string SQL_deleteAreaInfo = @"delete from AreaInfo where Province='" + Province + "'";
                                            if (dataaccess.ExeSQL(SQL_deleteAreaInfo, GlobalInfo.DefaultDatabase) == 0)
                                            {
                                                //7 重新加载树状图
                                                //LoadTreeView();
                                                string SQL = @"delete from CollectPointInfo where AreaName like'" + Province + "%'";
                                                if (dataaccess.ExeSQL(SQL, GlobalInfo.DefaultDatabase) == 0)
                                                {

                                                    MessageBox.Show("删除地区成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                            }
                                            //数据库操作错误
                                            else
                                            { MessageBox.Show("删除地区失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                                        }
                                        //数据库操作错误
                                        else
                                        { MessageBox.Show("删除地区失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                                    }
                                    //数据库操作错误
                                    else
                                    { MessageBox.Show("删除地区失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                }
                                //数据库操作错误
                                else
                                { MessageBox.Show("删除地区失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            }

                            //数据库操作错误
                            else
                            { MessageBox.Show("删除地区失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        }
                        catch (Exception ex)
                        { MessageBox.Show("删除地区失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }

                }
            }
            #endregion
            #region 删除城市

            if (treeView1.SelectedNode.ImageIndex == 2)
            {
                if (DialogResult.OK == MessageBox.Show("确认要删除该地区？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    if (DialogResult.OK == MessageBox.Show("删除地区后将丢失该地区的一切数据！确认要删除该地区？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    {
                        string City = treeView1.SelectedNode.Text;

                        //0 树状图中删除省份节点 在form1中完成
                        //1 删除该市下的项目数据库文件（删除数据库）
                        //2 删除该市下的采集点
                        //3 删除该市下的采集器
                        //4 删除该市下的项目
                        //5 删除该市下的项目信息表
                        //6 删除该市下的地区表
                        try
                        {

                            //如果该省下没有城市了则删除该省
                            //if (this.treeView1.SelectedNode.Parent.PrevNode == null)
                            //{
                            //    this.treeView1.SelectedNode.Parent.Remove();
                            //}
                            //else
                            //{ this.treeView1.SelectedNode.Remove(); }
                            this.treeView1.SelectedNode.Remove();

                            string SQL_SelectProjectInfo = @"select AreaName,ProjectName from ProjectInfo where AreaName like'%" + City + "'";
                            DataSet ds_ProjectInfo = dataaccess.GetDataSet(SQL_SelectProjectInfo, GlobalInfo.DefaultDatabase);
                            DataTable dt_ProjectInfo = ds_ProjectInfo.Tables[0];
                            int ProjectNum = dt_ProjectInfo.Rows.Count;
                            for (int i = 0; i < ProjectNum; i++)
                            {
                                string DataBaseName = dt_ProjectInfo.Rows[i][0].ToString() + dt_ProjectInfo.Rows[i][1].ToString();//项目数据名称=地区名称+项目名称
                                string SQL_DeleteDataBase = @"drop database " + DataBaseName;
                                //1 删除该市下的项目数据库文件（删除数据库）
                                dataaccess.ExeSQL(SQL_DeleteDataBase, GlobalInfo.DefaultDatabase);
                            }
                            ////2 删除该市下的采集点
                            ////不需要判断是否存在该项内容 若不存在该内容则0行受到影响
                            //string SQL_deleteProjectPoint = @"delete from CollectPointInfo where AreaName like'%" + City + "'";
                            //if (0 == dataaccess.ExeSQL(SQL_deleteProjectPoint, GlobalInfo.DefaultDatabase))
                            //{
                            //3 删除该市下的采集器
                            string SQL_deleteCollector = @"delete from CollectorInfo where AreaName like'%" + City + "'";
                            if (dataaccess.ExeSQL(SQL_deleteCollector, GlobalInfo.DefaultDatabase) == 0)
                            {
                                //4 删除该市下的项目
                                string SQL_deleteProject = @"delete from ProjectInfo where AreaName like'%" + City + "'";
                                if (dataaccess.ExeSQL(SQL_deleteProject, GlobalInfo.DefaultDatabase) == 0)
                                {
                                    //5 删除该市下的项目信息表
                                    string SQL_deleteProjectDetail = @"delete from ProjectDetailInfo where AreaName like'%" + City + "'";
                                    if (dataaccess.ExeSQL(SQL_deleteProjectDetail, GlobalInfo.DefaultDatabase) == 0)
                                    {
                                        //6 删除该省份下的地区表
                                        string SQL_deleteAreaInfo = @"delete from AreaInfo where City='" + City + "'";
                                        if (dataaccess.ExeSQL(SQL_deleteAreaInfo, GlobalInfo.DefaultDatabase) == 0)
                                        {
                                            //7 重新加载树状图
                                            //LoadTreeView();
                                            MessageBox.Show("删除地区成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                    }
                                    //数据库操作错误
                                    else
                                    { MessageBox.Show("删除地区失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                                }
                                //数据库操作错误
                                else
                                { MessageBox.Show("删除地区失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            }
                            //数据库操作错误
                            else
                            { MessageBox.Show("删除地区失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            //}
                            ////数据库操作错误
                            //else
                            //{ MessageBox.Show("删除地区失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        }
                        catch (Exception ex)
                        { MessageBox.Show("删除地区失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }

                }
            }
            #endregion
        }
        private void 删除项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确认要删除该项目？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                if (DialogResult.OK == MessageBox.Show("删除项目后将丢失该项目的一切数据！确认要删除该项目？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    string AreaName = treeView1.SelectedNode.Parent.Parent.Text + treeView1.SelectedNode.Parent.Text;//地区名
                    string ProjectName = treeView1.SelectedNode.Text;//项目名
                    // 0 树状图中删除项目节点 在form1中完成
                    // 1 为该项目删除数据库文件（删除数据库）
                    // 2 ProjectDetaiInfo表格中删除信息 （未完成）
                    // 3 CollectPointInfo表格中删除相关内容(删除采集点)
                    // 4 CollectorInfo表格中删除相关内容(删除采集器)
                    // 5 ProjectInfo表格中删除相应数据 (删除项目)
                    // 6 判断该地区是否还有项目（若没有删除树状图地区 若有则不删除）
                    try
                    {
                        // 0 树状图中删除项目节点
                        treeView1.SelectedNode.Remove();
                        // 1 为该项目删除数据库文件（删除数据库）
                        string SQL_DeleteDatabase = @"drop database " + AreaName + ProjectName;
                        if (0 == dataaccess.ExeSQL(SQL_DeleteDatabase, GlobalInfo.DefaultDatabase))
                        {
                            // 2 ProjectDetaiInfo表格中删除信息 
                            string SQL_DeleteProjectDetailInfo = @"delete from ProjectDetailInfo where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "'";
                            if (0 == dataaccess.ExeSQL(SQL_DeleteProjectDetailInfo, GlobalInfo.DefaultDatabase))
                            {


                                //3 CollectPointInfo表格中删除相关内容(删除采集点) 并在表格中删除相关采集点
                                string SQL_IsExistPoint = @"select * from CollectPointInfo where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "'";
                                //若存在采集点则进行删除
                                if (dataaccess.IsExistColletorOrPoint(SQL_IsExistPoint) == true)
                                {
                                    string SQL_DeleteCollectPointInfo = @"delete from CollectPointInfo where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "'";
                                    dataaccess.ExeSQL(SQL_DeleteCollectPointInfo, GlobalInfo.DefaultDatabase);
                                }
                                //若不存在采集点则进行 4 采集器删除                       
                                string SQL_IsExistCollector = @"select * from CollectorInfo where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "'";
                                //若存在采集器则进行删除
                                if (true == dataaccess.IsExistColletorOrPoint(SQL_IsExistCollector))
                                {
                                    string SQL_DeleteColletor = @"delete from CollectorInfo where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "'";
                                    dataaccess.ExeSQL(SQL_DeleteColletor, GlobalInfo.DefaultDatabase);
                                }
                                //若不存在采集器则进行 5 项目删除
                                string SQL_DeleteProjectInfo = @"delete from ProjectInfo where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "'";
                                if (0 == dataaccess.ExeSQL(SQL_DeleteProjectInfo, GlobalInfo.DefaultDatabase))
                                {
                                    // 6 判断该地区是否还有项目（若没有删除地区 若有则不删除）
                                    string SQL_IsExistArea = @"select * from ProjectInfo where AreaName='" + AreaName + "'";
                                    if (dataaccess.IsExistColletorOrPoint(SQL_IsExistArea) == false)
                                    {

                                        treeView1.SelectedNode.Parent.Remove();
                                        MessageBox.Show("删除项目成功！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                    }
                                    else
                                    {

                                        MessageBox.Show("删除项目成功！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                { MessageBox.Show("删除项目失败！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information); }
                            }
                            //数据库删除ProjectDetailInfo相关信息错误
                            else
                            { }
                        }
                        //数据库删除错误
                        else
                        { MessageBox.Show("删除项目失败！数据库删除错误！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information); }

                    }
                    catch
                    { MessageBox.Show("删除项目失败！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information); }

                }
                else
                { return; }
            }
            else
            { return; }

        }
        private void 删除控制器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确认要删除该控制器？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                if (DialogResult.OK == MessageBox.Show("删除控制器后将丢失该采集器的一切数据！确认要删除该采集器？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {

                    // 0 树状图中删除采集器节点 


                    //// 2 CollectPointInfo表格中删除相关内容(删除采集点)
                    //string SQL_IsExistPoint = @"select count(*) from CollectPointInfo where AreaName='" + collectorAreaName.ToString() + "' and ProjectName='" + collectorProjectName.ToString() + "' and CollectorName='" + collectorCollectorName.ToString() + "'";
                    ////若存在采集点则进行删除
                    //if (dataaccess.IsExistColletorOrPoint(SQL_IsExistPoint) == true)
                    //{
                    //    string SQL_DeleteCollectPointInfo = @"delete from CollectPointInfo where AreaName='" + collectorAreaName.ToString() + "' and ProjectName='" + collectorProjectName.ToString() + "' and CollectorName='" + collectorCollectorName.ToString() + "'";
                    //    dataaccess.ExeSQL(SQL_DeleteCollectPointInfo, GlobalInfo.DefaultDatabase);
                    //}
                    //若不存在采集点则进行 3(删除采集器)
                    string SQL_DeleteColletor = @"delete from CollectorInfo where AreaName='" + collectorAreaName.ToString() + "' and ProjectName='" + collectorProjectName.ToString() + "' and CollectorName='" + collectorCollectorName.ToString() + "'";
                    if (dataaccess.ExeSQL(SQL_DeleteColletor, GlobalInfo.DefaultDatabase) == 0)
                    {
                        treeView1.SelectedNode.Remove();
                        MessageBox.Show("删除控制器器成功！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                    else
                    { MessageBox.Show("删除控制器器失败！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information); }
                }
                else
                { return; }
            }
            else
            { return; }
        }


        #endregion

        #region 关闭窗体
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确认要退出？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {

                if (GlobalInfo.IsStart == true)
                {
                    MessageBox.Show("请先停止服务！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
                else
                {
                    //Application.Exit();//使隐藏的formentry窗体关闭 
                    //关闭串口
                    serialPort1.Close();
                    this.Dispose();
                    enter.Close();
                }
            }
            else
            { e.Cancel = true; }
        }
        #endregion

        #region 历史查询
        /// <summary>
        /// 历史查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            FormQuery fmQuery = new FormQuery();
            fmQuery.ShowDialog();

        }
        #endregion

        #region  中继器配置
        private void button61_Click(object sender, EventArgs e)
        {
            FormConfig config = new FormConfig();
            config.ShowDialog();
        }
        #endregion

        #region 加载项目信息[填充系统运行信息中表]
        /// <summary>
        /// 载入运行状态中的采集器信息
        /// </summary>
        private void LoadCollectorInfo()
        {
            try
            {
                string SQL_Collector = @"select * from CollectorInfo where AreaName='" + collectorAreaName.ToString() + "' and ProjectName='" + collectorProjectName.ToString() + "' and CollectorName='" + collectorCollectorName.ToString() + "'";
                DataSet ds = dataaccess.GetDataSet(SQL_Collector, GlobalInfo.DefaultDatabase);
                DataTable dt = ds.Tables[0];
                int count = dt.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    this.txtInfoArea.Text = dt.Rows[i][0].ToString();
                    this.txtInfoProject.Text = dt.Rows[i][1].ToString();
                    this.txtInfoCollector.Text = dt.Rows[i][2].ToString();
                    this.txtInfoCollectorCode.Text = dt.Rows[i][3].ToString();
                    this.txtInfoCollectPointNum.Text = dt.Rows[i][5].ToString();
                }
                ds.Dispose();
                dt.Dispose();
            }
            catch
            { }
        }
        /// <summary>
        ///向运行信息中  加载列表信息(端口号 IP)
        /// </summary>
        private void AddListView()
        {
            string path = "portInfo.xml";
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(path);
                XmlNode root = xd.DocumentElement;
                GlobalInfo.port = Convert.ToInt32(root.SelectSingleNode("port").InnerText);
            }
            catch
            { }
            this.lsvServer.Items[0].SubItems.Add(localAddress.ToString());
            this.lsvServer.Items[0].SubItems.Add(GlobalInfo.port.ToString());
        }
        #endregion

        #region 树状图配置
        /// <summary>
        /// 树状图配置采集点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 配置采集点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //判断采集点是否已经到数量
                string SQL_CollectPoint = @"select CollectPointNum from CollectorInfo where AreaName='" + collectorAreaName.ToString() + "' and ProjectName='" + collectorProjectName.ToString() + "' and CollectorName='" + collectorCollectorName.ToString() + "'";
                DataSet ds_Point = dataaccess.GetDataSet(SQL_CollectPoint, GlobalInfo.DefaultDatabase);
                DataTable dt_Point = ds_Point.Tables[0];
                string num = dt_Point.Rows[0][0].ToString();//采集器中的采集点数量

                string SQL_CollecPointNum = @"select count(*) from CollectPointInfo where AreaName='" + collectorAreaName.ToString() + "' and ProjectName='" + collectorProjectName.ToString() + "' and CollectorName='" + collectorCollectorName.ToString() + "' ";
                int pointNum = Convert.ToInt32(dataaccess.ReturnSingleData(SQL_CollecPointNum));
                if (pointNum < int.Parse(num))
                {

                    FormCollectPointConfiguration fcpc = new FormCollectPointConfiguration(collectorAreaName.ToString(), collectorProjectName.ToString(), collectorCollectorName.ToString(), 1);
                    fcpc.ShowDialog();

                    ////2 向表格中添加该采集点
                    //if (GlobalInfo.IsNewCollectPoint == true)
                    //{
                    //    int Count = this.dgvDisplay.Rows.Count;
                    //    this.dgvDisplay.Rows.Add(Count + 1, fcpc.CollectPointName, fcpc.CollectPointCode, fcpc.CollectData, fcpc.CollectDataCode);
                    //    //还原该变量
                    //    GlobalInfo.IsNewCollectPoint = false;
                    //}

                }
                else
                {
                    MessageBox.Show("采集点数量已到达配置值！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 树状图添加采集点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 添加采集点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string AreaName = treeView1.SelectedNode.Parent.Parent.Parent.Text + treeView1.SelectedNode.Parent.Parent.Text;//地区名
            //string ProjectName = treeView1.SelectedNode.Parent.Text;//项目名
            //string CollectorName = treeView1.SelectedNode.Text;//采集器名称
            try
            {
                //判断采集点是否已经到数量
                string SQL_CollectPoint = @"select CollectPointNum from CollectorInfo where AreaName='" + collectorAreaName.ToString() + "' and ProjectName='" + collectorProjectName.ToString() + "' and CollectorName='" + collectorCollectorName.ToString() + "'";
                DataSet ds_Point = dataaccess.GetDataSet(SQL_CollectPoint, GlobalInfo.DefaultDatabase);
                DataTable dt_Point = ds_Point.Tables[0];
                string num = dt_Point.Rows[0][0].ToString();//采集器中的采集点数量

                string SQL_CollecPointNum = @"select count(*) from CollectPointInfo where AreaName='" + collectorAreaName.ToString() + "' and ProjectName='" + collectorProjectName.ToString() + "' and CollectorName='" + collectorCollectorName.ToString() + "' ";
                int pointNum = Convert.ToInt32(dataaccess.ReturnSingleData(SQL_CollecPointNum));
                if (pointNum < int.Parse(num))
                {

                    FormCollectPointConfiguration fcpc = new FormCollectPointConfiguration(collectorAreaName.ToString(), collectorProjectName.ToString(), collectorCollectorName.ToString(), 0);
                    fcpc.ShowDialog();
                    ////2 向表格中添加该采集点
                    //if (GlobalInfo.IsNewCollectPoint == true)
                    //{
                    //    int Count = this.dgvDisplay.Rows.Count;
                    //    this.dgvDisplay.Rows.Add(Count + 1, fcpc.CollectPointName, fcpc.CollectPointCode, fcpc.CollectData, fcpc.CollectDataCode);
                    //    //还原该变量
                    //    GlobalInfo.IsNewCollectPoint = false;
                    //}

                }
                else
                {
                    MessageBox.Show("采集点数量已到达配置值！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            { }
        }
        #endregion

        #region flash加载
        /// <summary>
        /// 加载flash
        /// </summary>
        /// <param name="areaName">地区名</param>
        /// <param name="projectName">项目名</param>
        /// <param name="collectorName">采集器名</param>
        /// <param name="flashMode">工作模式</param>
        private void flashData(string areaName, string projectName, string collectorName, string flashMode)
        {
            try
            {
                string database = areaName + projectName;
                string sql_data = "select top 1 * from " + collectorName + "实时 order by TimeStamp desc";
                DataTable dt = dataaccess.GetDataTable(sql_data, database);

                string sql = "select top 1 * from " + collectorName + "参数 order by TimeStamp desc";
                DataTable dt2 = dataaccess.GetDataTable(sql, database);


                if (dt.Rows.Count != 0 && dt2.Rows.Count != 0 && flashMode == "8")//热水供暖系统flash加载
                {
                    #region 模式8光伏制冷
                    //写入XML
                    string path = Application.StartupPath + "\\" + flashMode + ".xml";
                    XmlDocument xdflash = new XmlDocument();
                    xdflash.Load(path);
                    XmlNode rootflash = xdflash.DocumentElement;
                    XmlElement xeTest = (XmlElement)rootflash.SelectSingleNode("test");

                    string sun = "";//光照度
                    string temp0 = " ";//压缩机出口温度
                    string temp1 = "";//冷凝器出口温度
                    string temp2 = "";//蒸发器出口温度
                    string temp3 = "";//蒸发器入口温度
                    string temp4 = "";//冰层温度
                    string temp5 = "";//水箱上层温度
                    string temp6 = "";//供冷循环回水温度
                    string temp7 = "";//水箱下层温度
                    string temp8 = "";//供冷循环供水温度
                    string temp9 = "";//风机出口温度
                    string temp10 = "";//房间温度
                    //string temp11 = "";//设定温度
                    string temp12 = "";//环境温度

                    string yw1 = "";//溢水桶液位
                    string yw2 = "";//水箱液位
                    string yl1 = "";//压缩机出口压力
                    string yl2 = "";//蒸发器出口压力
                    string ll1 = "";//供冷冷水流量
                    string jinshuifa = "";//进水阀
                    string chushuifa = "";//出水阀
                    string zhileng = "";//制冷
                    string yongleng = "";//用冷  


                    //sun = dt.Rows[0][2].ToString();
                    temp0= dt.Rows[0][26].ToString() + "℃";//压缩机出口温度
                    temp1 = dt.Rows[0][27].ToString() + "℃";//冷凝器出口温度
                    temp2 = dt.Rows[0][28].ToString() + "℃";//蒸发器出口温度
                    temp3 = dt.Rows[0][29].ToString() + "℃";//蒸发器入口温度
                    temp4 = dt.Rows[0][36].ToString() + "℃";//冰层温度
                    temp5 = dt.Rows[0][37].ToString() + "℃";//水箱上层温度
                    temp6 = dt.Rows[0][38].ToString() + "℃";//供冷循环回水温度
                    temp7 = dt.Rows[0][39].ToString() + "℃";//水箱下层温度
                    temp8 = dt.Rows[0][40].ToString() + "℃";//供冷循环供水温度
                    temp9 = dt.Rows[0][41].ToString() + "℃";//风机出口温度
                    temp10 = dt.Rows[0][42].ToString() + "℃";//房间温度。
                    //temp12 = dt.Rows[0][1].ToString() + "℃";//环境温度

                    yl1 = dt.Rows[0][24].ToString();//压缩机出口压力
                    yl2 = dt.Rows[0][25].ToString();//蒸发器出口压力
                    yw1 = dt.Rows[0][30].ToString() + "";//溢水桶液位
                    yw2 = dt.Rows[0][31].ToString() + "";//水箱液位
                    ll1=dt.Rows[0][35].ToString();//流量
                   // ll2=dt.Rows[0][].ToString();

                    jinshuifa = dt2.Rows[0][5].ToString();//进水阀
                    chushuifa = dt2.Rows[0][6].ToString();//出水阀

                    //压缩机频率大于0
                    if (Convert.ToDouble(dt.Rows[0][15].ToString())>0)
                    {
                        zhileng = "1";
                    }
                    else
                    {
                        zhileng = "0";
                    }
                    //用冷
                    if (Convert.ToDouble(dt.Rows[0][27].ToString()) > 0)
                    {
                        yongleng = "1";
                    }
                    else
                    {
                        yongleng = "0";
                    }

                    //写入
                    xeTest.SetAttribute("sun", sun);
                    xeTest.SetAttribute("temp0", temp0);
                    xeTest.SetAttribute("temp1", temp1);
                    xeTest.SetAttribute("temp2", temp2);
                    xeTest.SetAttribute("temp3", temp3);
                    xeTest.SetAttribute("temp4", temp4);
                    xeTest.SetAttribute("temp5", temp5);
                    xeTest.SetAttribute("temp6", temp6);
                    xeTest.SetAttribute("temp7", temp7);
                    xeTest.SetAttribute("temp8", temp8);
                    xeTest.SetAttribute("temp9", temp9);
                    xeTest.SetAttribute("temp10", temp10);
                    xeTest.SetAttribute("temp12", temp12);
                    xeTest.SetAttribute("yl1", yl1);
                    xeTest.SetAttribute("yl2", yl2);
                    xeTest.SetAttribute("ll1", ll1);
                    xeTest.SetAttribute("yw1", yw1);
                    xeTest.SetAttribute("yw2", yw2);
                    xeTest.SetAttribute("jinshuifa", jinshuifa);
                    xeTest.SetAttribute("chushuifa", chushuifa);
                    xeTest.SetAttribute("zhileng", zhileng);
                    xeTest.SetAttribute("yongleng", yongleng);
                   

                   xdflash.Save(path);//保存xml
                    #endregion

                }

            }
            catch (Exception)
            {

                //throw;
            }

        }
        #endregion

        #region 公司权限
        /// <summary>
        /// 公司权限管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 公司管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompanyConfiguration fmcompany = new CompanyConfiguration();
            fmcompany.ShowDialog();
        }
        #endregion

        #region 能耗统计
        /// <summary>
        /// 能耗统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripCalculate_Click(object sender, EventArgs e)
        {
            Form1 ff = new Form1();
            ff.ShowDialog();
        }
        #endregion

        #region 串口连接

        /// <summary>
        /// 向串口发送数据
        /// </summary>
        /// <param name="writeBuffer">需要发送的命令：字节数组</param>

        private void sendSerialPort(SerialPort serialPort, byte[] writeBuffer)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    MessageBox.Show("请先打开串口");
                }
                else
                {
                    serialPort.Write(writeBuffer, 0, writeBuffer.Length);
                    Thread.Sleep(120);
                }

            }

            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n发送串口数据失败");
            }
        }

        /// <summary>
        /// 从串口接收数据
        /// </summary>
        /// <param name="serialPort">串口号</param>
        /// <returns>接受到的数据</returns>
        private byte[] recieveSerialPort(SerialPort serialPort, int numlength)
        {
            byte[] buffer = new byte[numlength];
            try
            {
                serialPort.Read(buffer, 0, numlength);
                return buffer;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n接收数据失败");
                return buffer;
            }
            finally
            {
                serialPort.DiscardInBuffer();
            }
        }

        /// <summary>
        /// 获得bcc校验值
        ///
        /// </summary>
        /// <param name="msg">需要检验字节的数组</param>
        public static byte getBccValue(byte[] msg)
        {
            byte bccValue = 0;

            for (int i = 1; i < msg.Length - 1; i++)
            {
                bccValue ^= msg[0];
            }
            return bccValue;
        }

        /// <summary>
        /// 获取加完crc的字节
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public byte[] getAddCrcValue(byte[] msg)
        {
            ushort crcValue = CRC.CRCcheck(msg, msg.Length - 2);
            msg[msg.Length - 2] = (byte)crcValue;
            msg[msg.Length - 1] = (byte)(crcValue >> 8);
            return msg;
        }


        /// <summary>
        /// bcc校验：校验数据传输过程中是否发生改变或者丢失
        ///
        /// </summary>
        /// <param name="msg">需要检验字节的数组</param>
        /// <returns>返回true，表示通过验证;返回false，表示数据改变</returns>
        public static bool bccCheck(byte[] msg)
        {
            byte bccValue = msg[0];
            for (int i = 1; i < 15; i++)
            {
                bccValue ^= msg[i];
            }
            if (msg[msg.Length - 1] == bccValue)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// crc校验
        /// </summary>
        /// <param name="msg">传输数据数组</param>
        /// <returns>返回true，表示通过验证;返回false，表示数据改变</returns>
        public static bool crcCheck(byte[] usefulMsg)
        {
            //获取校验码
            ushort crcvalue = CRC.CRCcheck(usefulMsg, usefulMsg.Length - 2);

            //对比校验码
            if ((usefulMsg[usefulMsg.Length - 2] == (byte)crcvalue) && ((byte)(crcvalue >> 8) == usefulMsg[usefulMsg.Length - 1]))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        /// <summary>
        /// 接收数据函数
        /// </summary>
        /// <param name="serialPort">串口名</param>
        /// <param name="sendData">发送的数据</param>
        /// <param name="dataNum">接收数据的字节数</param>
        /// <param name="checkType">校验类型</param>
        /// <returns>有效的字节数组</returns>
        private byte[] recieveData(SerialPort serialPort, byte[] sendData, int dataNum, int checkType)
        {
            int countNum = 0;

            //没有获取到数据的标志
            bool notGetData = false;

            //用于接收数据
            byte[] dataByte = new byte[dataNum];

            //等待数据传入
            while (serialPort1.BytesToWrite != 0 || serialPort.BytesToRead == 0)
            {
                Thread.Sleep(1);
                countNum++;
                if (countNum > 50)
                {
                    notGetData = true;
                    break;
                }
            }

            //正常获取数据
            if (notGetData == false)
            {
                dataByte = recieveSerialPort(serialPort, dataNum);


                //如果是CRC校验
                if (checkType == RS485.CheckType_CRC)
                {

                    //校验OK
                    if (crcCheck(dataByte))
                    {
                        int getNum = Convert.ToInt32(dataByte[2]);//需要读取的数据长度
                        byte[] keyValueByte = new byte[getNum];//接收字节数组

                        //接收字节
                        for (int i = 0; i < getNum; i++)
                        {
                            keyValueByte[i] = dataByte[i + 3];
                        }
                        return keyValueByte;
                    }
                    //校验Fail
                    else
                    {
                        serialPort.DiscardInBuffer();
                        byte[] fail = new byte[8];
                        for (int i = 0; i < 8; i++)
                        {
                            fail[i] = 0xff;
                        }
                        return fail;
                    }
                }

                //如果是BCC校验
                else
                {
                    if (bccCheck(dataByte))
                    {

                        byte[] keyValueByte = new byte[4];//接收字节数组

                        //接收字节
                        for (int i = 0; i < 4; i++)
                        {
                            keyValueByte[i] = dataByte[i + 7];
                        }
                        return keyValueByte;

                    }
                    else
                    {
                        byte[] fail = new byte[4];
                        for (int i = 0; i < 4; i++)
                        {
                            fail[i] = 0xff;
                        }
                        return fail;
                    }
                }

            }
            //如果发送失败
            else
            {
                if (checkType == RS485.CheckType_CRC)
                {

                    serialPort.DiscardInBuffer();
                    byte[] fail = new byte[8];
                    for (int i = 0; i < 4; i++)
                    {
                        fail[i] = 0xff;
                    }
                    return fail;

                }
                else
                {
                    serialPort.DiscardInBuffer();
                    byte[] fail = new byte[8];
                    for (int i = 0; i < 4; i++)
                    {
                        fail[i] = 0xff;
                    }
                    return fail;

                }
            }




        }

        private void recieveCanshu(SerialPort serialPort, byte[] sendData, int dataNum)
        {
            if (!serialPort.IsOpen)
            {
                MessageBox.Show("请先打开串口");
            }
            else
            {
                int countNum = 0;

                //没有获取到数据的标志
                bool notGetData = false;
                byte[] dataByte = new byte[dataNum];

                while (serialPort.BytesToWrite != 0 || serialPort.BytesToRead == 0)
                {
                    Thread.Sleep(1);
                    countNum++;
                    if (countNum > 100)
                    {
                        notGetData = true;
                        break;
                    }
                }
                //正常获取数据
                if (notGetData == false)
                {
                    recieveSerialPort(serialPort, dataNum);
                }
                else
                {
                    sendSerialPort(serialPort, sendData);
                    recieveCanshu(serialPort, sendData, dataNum);
                }
            }


        }

        //字节转换为需要的值
        private string changeToFloat(byte[] keyValue)
        {
            if (keyValue.Length != 4)
            {
                return " ";
            }
            else
            {
                if (keyValue[0] == 0xFF && keyValue[1] == 0xFF && keyValue[2] == 0xFF && keyValue[3] == 0xFF)
                {
                    return "";
                }
                else
                {
                    byte[] tempTurnOver = new byte[keyValue.Length];
                    for (int i = 0; i <= keyValue.Length - 1; i++)
                    {
                        tempTurnOver[keyValue.Length - 1 - i] = keyValue[i];
                    }
                    if (keyValue.Length == 4)
                    {
                        string theValue = BitConverter.ToSingle(tempTurnOver, 0).ToString("F2");
                        return theValue;
                    }
                    else
                    {
                        return " ";
                    }


                }

            }


        }

        //字节转换为双浮点型
        private string changeToDouble(byte[] keyValue)
        {
            if (keyValue.Length != 8)
            {
                return "-1";
            }
            else
            {
                if (keyValue[0] == 0xFF && keyValue[1] == 0xFF && keyValue[2] == 0xFF && keyValue[3] == 0xFF)
                {
                    return "-1";
                }
                else
                {
                    byte[] tempTurnOver = new byte[keyValue.Length];
                    for (int i = 0; i <= keyValue.Length - 1; i++)
                    {
                        tempTurnOver[keyValue.Length - 1 - i] = keyValue[i];

                    }
                    string theValue = (BitConverter.ToDouble(tempTurnOver, 0) / 1000).ToString("F4");
                    return theValue;
                }
            }



        }



        //直流电流转换
        private string changeDC(byte[] keyValue)
        {
            if (keyValue.Length != 4)
            {
                return " ";
            }
            else
            {
                if (keyValue[0] == 0xFF && keyValue[1] == 0xFF && keyValue[2] == 0xFF && keyValue[3] == 0xFF)
                {
                    return " ";
                }
                else
                {
                    string DCValue = "";
                    if (keyValue[2] == 0x00)
                    {
                        DCValue = Convert.ToString((keyValue[0] * 256 + keyValue[1]) * Math.Pow(10, (keyValue[3] - 2)));

                    }
                    if (keyValue[2] == 0x01)
                    {
                        DCValue = Convert.ToString((keyValue[0] * 256 + keyValue[1]) * Math.Pow(10, (keyValue[3] - 2) * (-1)));
                    }
                    return DCValue;

                }

            }


        }

        private string updateValue(string newValue, string oldValue)
        {
            if (newValue.Trim() == "")
            {
                return oldValue;
            }
            else return newValue;
        }


        /// <summary>
        /// 发送设置数据
        /// </summary>
        /// <param name="keyValue">有效数据</param>
        /// <param name="beSendValue">要发送的字节数组的模板</param>
        private void sendSetNum(Int16 keyValue, byte[] beSendValue)
        {
            //用于计数
            int countNum = 0;
            byte[] keyValueByte = BitConverter.GetBytes(keyValue);
            if (keyValueByte.Length==2)
            {
                beSendValue[4] = keyValueByte[1];
                beSendValue[5] = keyValueByte[0];
            }
            
            //CRC校验
            ushort crcValue = CRC.CRCcheck(beSendValue, beSendValue.Length - 2);

            //给校验位赋值
            beSendValue[beSendValue.Length - 2] = (byte)crcValue;
            beSendValue[beSendValue.Length - 1] = (byte)(crcValue >> 8);

            //发送带关键字和CRC的数据
            sendSerialPort(serialPort1,beSendValue);

            //等待接收
            while (serialPort1.BytesToRead == 0)
            {
                Thread.Sleep(1);
                countNum++;

                //超过10ms未接到数据，跳出循环，避免堵塞
                if (countNum > 10)
                {
                    countNum = 0;
                    break;
                }

            }

            //清空缓存
            serialPort1.DiscardInBuffer();
        }

        #endregion

        #region 启禁控制器
        private void 禁用控制器toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (User u in GlobalInfo.userList)
                {

                    //选中的项目在线
                    if (GlobalInfo.IsStart == true)
                    {
                        string AreaName = treeView1.SelectedNode.Parent.Parent.Parent.Text + treeView1.SelectedNode.Parent.Parent.Text; //地区名
                        string ProjectName = treeView1.SelectedNode.Parent.Text;//项目名
                        string CollectorName = treeView1.SelectedNode.Text;

                        if (u.CollectorName == CollectorName && u.ProjectName == ProjectName && u.AreaName == AreaName)
                        {

                            if (DialogResult.OK == MessageBox.Show("确定禁用该控制器？禁用后该系统将无法使用，请谨慎操作！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                            {
                                string config = "<?xml version='1.0' encoding='utf-8'?><root><common> <project_id>330400002</project_id><gateway_id>003</gateway_id><type>server_ctrl</type></common><config operation='server_ctrl'><server_ctrl>00030</server_ctrl></config></root>";
                                byte[] sendMessage = ReceiveDataProcessing.PacketMessage(u, config);
                                AsyncSendToClient(u, sendMessage);
                                GlobalInfo.IsCloseCollector = false;

                                //收到回复数据后再显示下面的话。
                                Thread.Sleep(3000);
                                if (GlobalInfo.IsCloseCollector == true)
                                {
                                    MessageBox.Show("控制器已禁用，已接收返回参数！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("控制器已禁用，返回参数未接收！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                }
                                GlobalInfo.IsCloseCollector = false;
                                treeView1.SelectedNode.ImageIndex = 6;
                                treeView1.SelectedNode.SelectedImageIndex = 6;
                            }
                            else
                            {
                                MessageBox.Show("控制器未禁用！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            //MessageBox.Show("请选择相应控制器！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                    }

                    else
                    {
                        //MessageBox.Show("项目未连接！请检查后重试！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }

                }
            }
            catch { }
        }
        private void 启动控制器toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (User u in GlobalInfo.userList)
                {
                    //选中的项目在线
                    if (GlobalInfo.IsStart == true)
                    {
                        string AreaName = treeView1.SelectedNode.Parent.Parent.Parent.Text + treeView1.SelectedNode.Parent.Parent.Text; //地区名
                        string ProjectName = treeView1.SelectedNode.Parent.Text;//项目名
                        string CollectorName = treeView1.SelectedNode.Text;

                        //选中相应的项目
                        if (u.CollectorName == CollectorName && u.ProjectName == ProjectName && u.AreaName == AreaName)
                        {
                            if (DialogResult.OK == MessageBox.Show("确定启用该控制器？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                            {
                                string config = "<?xml version='1.0' encoding='utf-8'?><root><common> <project_id>330400002</project_id><gateway_id>003</gateway_id><type>server_ctrl</type></common><config operation='server_ctrl'><server_ctrl>00031</server_ctrl></config></root>";
                                byte[] sendMessage = ReceiveDataProcessing.PacketMessage(u, config);
                                AsyncSendToClient(u, sendMessage);
                                GlobalInfo.IsOpenCollector = false;

                                //等待数据回复
                                Thread.Sleep(3000);
                                if (GlobalInfo.IsOpenCollector == true)
                                {
                                    MessageBox.Show("控制器成功启动，已接收返回参数！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("控制器成功启动，未接收返回参数！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                }
                                GlobalInfo.IsOpenCollector = false;
                                treeView1.SelectedNode.ImageIndex = 3;
                                treeView1.SelectedNode.SelectedImageIndex = 3;
                            }
                            else
                            {
                                MessageBox.Show("控制器未启动！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            //MessageBox.Show("请选择相应控制器！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        //MessageBox.Show("项目未连接！请检查后重试！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                }
            }
            catch { }
        }


        //参数配置
        private void toolSBParameter_Click(object sender, EventArgs e)
        {
            Form1 en = new Form1();
            en.Show();
        }


        //重置中继器
        private void 参数回发button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定重置中继器数据吗？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    foreach (User u in GlobalInfo.userList)
                    {
                        //选中的项目在线
                        if (GlobalInfo.IsStart == true)
                        {
                            string AreaName = treeView1.SelectedNode.Parent.Parent.Parent.Text + treeView1.SelectedNode.Parent.Parent.Text; //地区名
                            string ProjectName = treeView1.SelectedNode.Parent.Text;//项目名
                            string CollectorName = treeView1.SelectedNode.Text;
                            //选中相应的项目控制器
                            if (u.CollectorName == CollectorName && u.ProjectName == ProjectName && u.AreaName == AreaName)
                            {
                                string SetParameter = "3000";
                                string id = this.txtAreaCode.Text + this.txtProjectCode.Text + this.txtInfoCollectorCode.Text;
                                string config = "<?xml version='1.0' encoding='utf-8'?><root><common> <id>" + id + "</id><type>HeatPump</type><data>" + SetParameter + "</data></common></root>";
                                byte[] sendMessage = ReceiveDataProcessing.PacketMessage(u, config);
                                AsyncSendToClient(u, sendMessage);
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                        }
                    }
                }
                catch
                {
                }
            }
        }

        #endregion

        #region 清空日志列表
        private void button61_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定清空信息列表吗？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                listBoxStatus.Items.Clear();
            }
        }
        #endregion

        #region 维护登记**

        private void 录入电表数据_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定对此项目的电表登记？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (电表值录入textBox245.Text != "")
                {
                    //获取目的项目信息，明确插入哪个项目的数据库
                    string database = comboBox1.Text + comboBox2.Text;//对话框中的-地区-项目-下拉框选择地区项目&2017.01.13&

                    if (dataaccess.IsExistProject(comboBox1.Text, comboBox2.Text))
                    {
                        //获取当前时间
                        System.DateTime currentTime = new System.DateTime();
                        currentTime = System.DateTime.Now;
                        String strChinaTime = currentTime.ToString("f"); //不显示秒

                        //生成插入数据库的字符串，将时间写上
                        StringBuilder InPut = new StringBuilder();//插入-维修登记-数据表的值
                        InPut.Append("'");
                        InPut.Append(strChinaTime);
                        InPut.Append("'");

                        //获取电表文本框值
                        String LastRecord = 电表值录入textBox245.Text;//此电量表工作期间记录的系统耗电量&添加-2017.01.13&
                        InPut.Append(",");
                        InPut.Append(LastRecord);

                        //将热量表值置为0，为了是配合插入电表累计值时热量表列不为空
                        LastRecord = "0";
                        InPut.Append(",");
                        InPut.Append(LastRecord);

                        //将获取的电表值插入维护登记表中新的一行，记录此电表工作期间电量
                        string sqlmetering = "insert into 维护登记 (TimeStamp,DianBiao,ReLiangBiao) values(" + InPut.ToString() + ")";
                        dataaccess.ExeSQL(sqlmetering, database);
                    }
                    else
                    {
                        MessageBox.Show("不存在该项目！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    MessageBox.Show("发送数据不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void 录入热量表数据_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定对此项目的热量表登记？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (热量表值录入textBox246.Text != "")
                {
                    //获取目的项目信息，明确插入哪个项目的数据库
                    string database = comboBox1.Text + comboBox2.Text;//对话框中的-地区-项目-下拉框选择地区项目&2017.01.13&

                    if (dataaccess.IsExistProject(comboBox1.Text, comboBox2.Text))
                    {
                        //获取当前时间
                        System.DateTime currentTime = new System.DateTime();
                        currentTime = System.DateTime.Now;
                        String strChinaTime = currentTime.ToString("f"); //不显示秒

                        //生成插入数据库的字符串，将时间写上
                        StringBuilder InPut = new StringBuilder();//插入-维修登记-数据表的值
                        InPut.Append("'");
                        InPut.Append(strChinaTime);
                        InPut.Append("'");

                        //获取电表文本框值
                        String LastRecord = "0";//此电量表工作期间记录的系统耗电量&添加-2017.01.13&
                        InPut.Append(",");
                        InPut.Append(LastRecord);

                        //将热量表值置为0，为了是配合插入电表累计值时热量表列不为空
                        LastRecord = 热量表值录入textBox246.Text;
                        InPut.Append(",");
                        InPut.Append(LastRecord);

                        //将获取的电表值插入维护登记表中新的一行，记录此电表工作期间电量
                        string sqlmetering = "insert into 维护登记 (TimeStamp,DianBiao,ReLiangBiao) values(" + InPut.ToString() + ")";
                        dataaccess.ExeSQL(sqlmetering, database);
                    }
                    else
                    {
                        MessageBox.Show("不存在该项目！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    MessageBox.Show("发送数据不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        #endregion



        #region 相关功能按钮

        //供暖信息获取参数
        private void button56_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定获取参数吗？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    foreach (User u in GlobalInfo.userList)
                    {
                        //选中的项目在线
                        if (GlobalInfo.IsStart == true)
                        {
                            string AreaName = treeView1.SelectedNode.Parent.Parent.Parent.Text + treeView1.SelectedNode.Parent.Parent.Text; //地区名
                            string ProjectName = treeView1.SelectedNode.Parent.Text;//项目名
                            string CollectorName = treeView1.SelectedNode.Text;
                            //选中相应的项目控制器
                            if (u.CollectorName == CollectorName && u.ProjectName == ProjectName && u.AreaName == AreaName)
                            {
                                string SetParameter = "00041";
                                string id = this.txtAreaCode.Text + this.txtProjectCode.Text + this.txtInfoCollectorCode.Text;
                                string config = "<?xml version='1.0' encoding='utf-8'?><root><common> <id>" + id + "</id><type>server_ctrl</type></common><config operation='server_ctrl'><server_ctrl>" + SetParameter + "</server_ctrl></config></root>";
                                byte[] sendMessage = ReceiveDataProcessing.PacketMessage(u, config);
                                AsyncSendToClient(u, sendMessage);
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                        }
                    }
                }
                catch
                {
                }
            }
        }
        //干燥信息获取参数
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定获取参数吗？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    foreach (User u in GlobalInfo.userList)
                    {
                        //选中的项目在线
                        if (GlobalInfo.IsStart == true)
                        {
                            string AreaName = treeView1.SelectedNode.Parent.Parent.Parent.Text + treeView1.SelectedNode.Parent.Parent.Text; //地区名
                            string ProjectName = treeView1.SelectedNode.Parent.Text;//项目名
                            string CollectorName = treeView1.SelectedNode.Text;
                            //选中相应的项目控制器
                            if (u.CollectorName == CollectorName && u.ProjectName == ProjectName && u.AreaName == AreaName)
                            {
                                string SetParameter = "00041";
                                string id = this.txtAreaCode.Text + this.txtProjectCode.Text + this.txtInfoCollectorCode.Text;
                                string config = "<?xml version='1.0' encoding='utf-8'?><root><common> <id>" + id + "</id><type>server_ctrl</type></common><config operation='server_ctrl'><server_ctrl>" + SetParameter + "</server_ctrl></config></root>";
                                byte[] sendMessage = ReceiveDataProcessing.PacketMessage(u, config);
                                AsyncSendToClient(u, sendMessage);
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                        }
                    }
                }
                catch
                {
                }
            }
        }
        //复杂热水系统获取参数
        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定获取参数吗？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    foreach (User u in GlobalInfo.userList)
                    {
                        //选中的项目在线
                        if (GlobalInfo.IsStart == true)
                        {
                            string AreaName = treeView1.SelectedNode.Parent.Parent.Parent.Text + treeView1.SelectedNode.Parent.Parent.Text; //地区名
                            string ProjectName = treeView1.SelectedNode.Parent.Text;//项目名
                            string CollectorName = treeView1.SelectedNode.Text;
                            //选中相应的项目控制器
                            if (u.CollectorName == CollectorName && u.ProjectName == ProjectName && u.AreaName == AreaName)
                            {
                                string SetParameter = "00041";
                                string id = this.txtAreaCode.Text + this.txtProjectCode.Text + this.txtInfoCollectorCode.Text;
                                string config = "<?xml version='1.0' encoding='utf-8'?><root><common> <id>" + id + "</id><type>server_ctrl</type></common><config operation='server_ctrl'><server_ctrl>" + SetParameter + "</server_ctrl></config></root>";
                                byte[] sendMessage = ReceiveDataProcessing.PacketMessage(u, config);
                                AsyncSendToClient(u, sendMessage);
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                        }
                    }
                }
                catch
                {
                }
            }
        }
        //简单热水系统获取参数
        private void button29_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定获取参数吗？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    foreach (User u in GlobalInfo.userList)
                    {
                        //选中的项目在线
                        if (GlobalInfo.IsStart == true)
                        {
                            string AreaName = treeView1.SelectedNode.Parent.Parent.Parent.Text + treeView1.SelectedNode.Parent.Parent.Text; //地区名
                            string ProjectName = treeView1.SelectedNode.Parent.Text;//项目名
                            string CollectorName = treeView1.SelectedNode.Text;
                            //选中相应的项目控制器
                            if (u.CollectorName == CollectorName && u.ProjectName == ProjectName && u.AreaName == AreaName)
                            {
                                string SetParameter = "00041";
                                string id = this.txtAreaCode.Text + this.txtProjectCode.Text + this.txtInfoCollectorCode.Text;
                                string config = "<?xml version='1.0' encoding='utf-8'?><root><common> <id>" + id + "</id><type>server_ctrl</type></common><config operation='server_ctrl'><server_ctrl>" + SetParameter + "</server_ctrl></config></root>";
                                byte[] sendMessage = ReceiveDataProcessing.PacketMessage(u, config);
                                AsyncSendToClient(u, sendMessage);
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                        }
                    }
                }
                catch
                {
                }
            }
        }


        //光伏制冷：下一页按钮
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.panel35.Visible = false;
            this.panel36.Visible = true;
        }

        //光伏制冷：上一页按钮
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.panel36.Visible = false;
            this.panel35.Visible = true;
        }



        //光伏制冷pannel页，开关选项
        private void pbX1_Click(object sender, EventArgs e)
        {
            Action actionSwitch1 = delegate
            {
                //如果为手动模式
                if (this.radioButton1.Checked == true)
                {
                    if (CanshuValue.switch1 == "1")
                    {
                        //this.pbX1.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";
                        //CanshuValue.switch1 = "0";
                        closeX1Flag = true;
                    }
                    else
                    {
                        //this.pbX1.ImageLocation = Application.StartupPath + "\\强制开关\\Open.png";
                        //CanshuValue.switch1 = "1";
                        openX1Flag = true;
                    }
                }
            };
            this.Invoke(actionSwitch1);

        }
        private void pbX2_Click(object sender, EventArgs e)
        {
            Action actionSwitch2 = delegate
            {
                //如果为手动模式
                if (this.radioButton1.Checked == true)
                {
                    if (CanshuValue.switch2 == "1")
                    {
                        //this.pbX2.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";
                        //CanshuValue.switch2 = "0";
                        closeX2Flag = true;
                    }
                    else
                    {
                        //this.pbX2.ImageLocation = Application.StartupPath + "\\强制开关\\Open.png";
                        //CanshuValue.switch2 = "1";
                        openX2Flag = true;
                    }
                }
            };
            this.Invoke(actionSwitch2);

        }



        private void pbX3_Click(object sender, EventArgs e)
        {
            //如果控制柜开关为上位机控制
            if (handSwitchStatus==2)
            {
                Action actionSwitch3 = delegate
                {
                    //如果为手动模式
                        if (this.radioButton1.Checked==true)
                        {
                            if (CanshuValue.switch3 == "1")
                            {
                                //this.pbX3.ImageLocation = Application.StartupPath + "\\强制开关\\Close.png";
                                //CanshuValue.switch3 = "0";
                                redEnergyFlag = true;
                            }
                            else
                             {
                                 //this.pbX3.ImageLocation = Application.StartupPath + "\\强制开关\\Open.png";
                                 //CanshuValue.switch3 = "1";
                                 sunEnergyFlag = true;
                            }
                        }
                };
                this.Invoke(actionSwitch3);
                
            }
        }

        //光伏制冷刷新按钮
        private void btReflesh_Click(object sender, EventArgs e)
        {
            if (collectorCollectorName.ToString() != "")
            {


                string database = collectorAreaName.ToString() + collectorProjectName.ToString();
                string collectorName = collectorCollectorName.ToString();

                Action action = delegate
                {
                    LoadStrategy(database, collectorName, collectorCollectorMode.ToString());
                    LoadCanshu(database, collectorName, collectorCollectorMode.ToString());
                };
                this.Invoke(action);
            }
            else
            {
                MessageBox.Show("请选择左侧的控制器");
            }


        }

        //设定温度发送参数按钮
        private void button3_Click(object sender, EventArgs e)
        {
            setValueFlag = true;
        }
        //设定压缩机PID发送参数按钮
        private void button4_Click(object sender, EventArgs e)
        {
            setValueFlag = true;
        }
        //设定水泵PID发送参数按钮
        private void button5_Click_1(object sender, EventArgs e)
        {
            setValueFlag = true;
        }

        private void timerMX100_Tick_1(object sender, EventArgs e)
        {

        }

        #region 限制输入字符
        private void C1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != '\b'&&e.KeyChar!='.'&&e.KeyChar!='-')//这是允许输入退格键和小数点
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字
                {
                    e.Handled = true;
                }

            }
            //只能输入一个小数点
            if (this.C1.Text.Contains('.') && e.KeyChar == '.')
            {
                e.Handled = true;
            }
        }
        #endregion

        private void label13_Click(object sender, EventArgs e)
        {

        }

        #endregion

        //手动模式
        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确认要手动模式吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                //handMode = true;
                this.comboBox3.Visible = false;
                this.label17.Visible = false;
                this.checkBox1.Visible = false;
            } 
            else
            {
                this.radioButton1.Checked = false;
                this.radioButton24.Checked = true;
            }

        }
        //自动模式
        private void radioButton24_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确认要自动模式吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                //handMode = true;
                this.comboBox3.Visible = true;
                this.label17.Visible = true;
                this.checkBox1.Visible = true;

            }
            else
            {
                this.radioButton1.Checked = true;
                this.radioButton24.Checked = false;
            }
        }

        //自动控制时间改变时
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            timeSpan = Convert.ToInt16(this.comboBox3.Text);
        }

        //实验按钮
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Action actionCkeck = delegate
            {
                if (checkBox1.Checked == false)
                {
                    this.comboBox3.Text = "10";
                    this.comboBox3.Enabled = false;
                }
                else 
                {                  
                    this.comboBox3.Enabled = true;
                }
            };
            this.Invoke(actionCkeck);


        }

        //串口1的值
        private void cbBSerialPort1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确认要改变串口名吗？\n请在设备管理器中查看端口号！", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                this.serialPort1.PortName = this.cbBSerialPort1.Text;
            }
            else
            {
                this.cbBSerialPort1.Text = " ";
            }

        }

        private void cbBSerialPort2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确认要改变串口名吗？\n请在设备管理器中查看端口号！", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                this.serialPort2.PortName = this.cbBSerialPort2.Text;
            }
            else
            {
                this.cbBSerialPort1.Text = " ";
            }


        }

  






    }
}
