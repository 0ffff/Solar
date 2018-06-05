using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 光伏制冷
{
    class GlobalInfo
    {
        /// <summary>
        /// 控制器模式
        /// </summary>
        public static string CollectorMode { set; get; }
        public static string PortName1 { set; get; }
        public static string PortName2 { set; get; }

        #region 状态参数
        /// <summary>
        /// 表示是否已经接到过数据
        /// </summary>
        public static bool IsGetData { set; get; }
        /// <summary>
        /// 表示是否配置项目成功
        /// </summary>
        public static bool IsNewProject { set; get; }

        /// <summary>
        /// 表示是否配置采集器成功
        /// </summary>
        public static bool IsNewCollector { set; get; }

        /// <summary>
        /// 表示是否配置采集点成功
        /// </summary>
        public static bool IsNewCollectPoint { set; get; }

        /// <summary>
        /// 表示是否编辑采集点成功
        /// </summary>
        public static bool IsEditCollectPoint { set; get; }

        /// <summary>
        /// 表示是否配置曲线成功
        /// </summary>
        public static bool IsCurveOk { set; get; }

        /// <summary>
        /// 表示是否设置周期成功
        /// </summary>
        public static bool IsSetPeriodFin { set; get; }

        /// <summary>
        /// 表示是否设置上传时间成功
        /// </summary>
        public static bool IsSetUploadFin { set; get; }


        /// <summary>
        /// 下位机是否发回上传成功标志
        /// </summary>
        public static bool IsUploadFin { set; get; }

        /// <summary>
        /// 表示下位机是否设置周期成功
        /// </summary>
        public static bool IsPeriodFin { set; get; }

        /// <summary>
        /// 表示下位机是否重启成功
        /// </summary>
        public static bool IsRestartFin { set; get; }


        /// <summary>
        /// 表示密码设置窗口是否成功
        /// </summary>
        public static bool IsSetPSWFin { set; get; }

        /// <summary>
        /// 表示MD5密钥设置窗口是否成功
        /// </summary>
        public static bool IsSetMD5KEYFin { set; get; }

        /// <summary>
        /// 表示AES密钥设置窗口是否成功
        /// </summary>
        public static bool IsSetAESKEYFin { set; get; }

        /// <summary>
        /// 表示AES初始量设置窗口是否成功
        /// </summary>
        public static bool IsSetAESIVFin { set; get; }

        /// <summary>
        /// 表示禁用控制器是否成功
        /// </summary>
        public static bool IsCloseCollector { set; get; }

        /// <summary>
        /// 表示启动控制器是否成功
        /// </summary>
        public static bool IsOpenCollector { set; get; }

        /// <summary>
        /// 表示启动控制器是否成功
        /// </summary>
        public static bool IsSetData { set; get; }

        /// <summary>
        /// 表示下位机回复md5密钥设置 因为要统计回复的个数与连接完成的个数是否一致
        /// </summary>
        private static int isMD5KEYFin = 0;
        public static int IsMD5KEYFin
        {
            set
            {
                isMD5KEYFin = value;
            }
            get
            {
                return isMD5KEYFin;
            }
        }

        /// <summary>
        /// 表示下位机回复AES密钥设置 因为要统计回复的个数与连接完成的个数是否一致
        /// </summary>
        private static int isAESKEYFin = 0;
        public static int IsAESKEYFin
        {
            set
            {
                isAESKEYFin = value;
            }
            get
            {
                return isAESKEYFin;
            }
        }

        /// <summary>
        /// 表示下位机回复AES初始量设置 因为要统计回复的个数与连接完成的个数是否一致
        /// </summary>
        private static int isAESIVFin = 0;
        public static int IsAESIVFin
        {
            set
            {
                isAESIVFin = value;
            }
            get
            {
                return isAESIVFin;
            }
        }

        /// <summary>
        /// 默认数据库名称
        /// </summary>
        private static string defaultDatabase = "EnergyTesting1";
        public static string DefaultDatabase
        {
            get
            {
                return defaultDatabase;
            }
            set
            {
                defaultDatabase = value;
            }

        }
        /// <summary>
        /// 是否正常退出监听
        /// </summary>
        private static bool isExit = false;
        public static bool IsExit
        {
            get
            {
                return isExit;
            }
            set
            {
                isExit = value;
            }
        }

        /// <summary>
        /// 是否开始监听
        /// </summary>
        private static bool isStart = false;
        public static bool IsStart
        {
            get
            {
                return isStart;
            }
            set
            {
                isStart = value;
            }
        }


        #endregion

        #region 系统参数

        public static List<User> userThread = new List<User>();//用于Tcp线程

        public static List<User> userList = new List<User>();//用于存储连接用户


        public static int port { set; get; }//本地监听端口号
        public static string MD5 { set; get; }//MD5密钥
        public static string Key { set; get; }//AES密钥
        public static string IV { set; get; }//AES初始量

        //从xml文件中读取上来的xml格式发送指令模板（程序中不能进行修改）
        public static string id_validate { get; set; }
        public static string stand { get; set; }
        public static string heart_beat { get; set; }
        public static string data { get; set; }
        public static string setkey { get; set; }


        //采集器上传周期(单位是小时  默认是30分钟)用于周期设置
        private static double period;
        public static double Period
        {
            set { period = value; }
            get { return period; }
        }

        //采集器上传开始时间 格式为yyyyMMddHH
        private static string begintime;
        public static string BeginTime
        {
            set { begintime = value; }
            get { return begintime; }
        }

        //采集器上传结束时间 格式为yyyyMMddHH
        private static string endtime;
        public static string EndTime
        {
            set { endtime = value; }
            get { return endtime; }
        }

        public static string UserType { set; get; }//登入的用户级别
        #endregion

        //太阳能指标字典
        public static Dictionary<string, string> querySolarIndex = new Dictionary<string, string>();
        //地源热泵指标字典
        public static Dictionary<string, string> queryHeatPumpIndex = new Dictionary<string, string>();
        //太阳能指标转化精度 数据库数据与界面显示单位之间的转化单位
        public static Dictionary<string, double> querySolarIndexUnit = new Dictionary<string, double>();

        #region 报警 对比用数据
        public static string[] alarmPre = new string[14];
        #endregion

        public static double Shuju1 { set; get; }
        public static double Shuju2 { set; get; }
        public static double Shuju3 { set; get; }
        public static double Shuju4 { set; get; }
        public static double Shuju5 { set; get; }

        public static string[,] Alarm = new string[10, 8]{
                                            {"环境温度状态","进水温度状态","回水温度状态","水箱1温度状态","盘管1温度状态","盘管2温度状态","吸气1温度状态","吸气2温度状态"},
                                            {"排气1温度状态","排气2温度状态","水箱2温度状态","预留温度状态","","","",""},
                                            {"水箱和进水状态" ,"存在环境和盘管损坏","环境和盘管1状态","环境和盘管2状态","","","",""},
                                            {"低压开关1","低压开关2","未使用作为预留","未使用作为预留","高压开关1","高压开关2","",""},
                                            {"过流开关1","过流开关2","未使用作为预留","未使用作为预留","排气保护1","排气保护2","",""},
                                            {"进水温度过高状态","缺相或逆相","水流开关","水压开关","低水位开关","高水位开关","",""},
                                            {"屏蔽不需要处理","屏蔽不需要处理","屏蔽不需要处理","屏蔽不需要处理","屏蔽不需要处理","","",""},
                                            {"屏蔽不需要处理","屏蔽不需要处理","屏蔽不需要处理","屏蔽不需要处理","","","",""},
                                            {"联动开关","逆相状态","缺相B状态","缺相C状态","屏蔽不需要处理","","",""},
                                            {"EE24C02的好坏","化霜","防冻","通信状态","","","",""}};

    }
}
