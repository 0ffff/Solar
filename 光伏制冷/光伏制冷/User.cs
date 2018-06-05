using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace 光伏制冷
{
    class User
    {
        public TcpClient client { private set; get; }//客户
        public BinaryReader br { private set; get; }//读
        public BinaryWriter bw { private set; get; }//写

        #region 用户信息

        public string AreaCode { set; get; }//从工程码中解析出来的地区码（6位）
        public string AreaName { set; get; }//工程名称 数据库中读取
        public string ProjectID { set; get; }//工程编码(应该包括地区码) 下位机发送上来
        public string ProjectName { set; get; }//工程名称 数据库中读取
        public string CollectorID { set; get; }//采集器编码 下位机发送上来
        public string CollectorName { set; get; }//采集器名称 数据库中读取

        public string UserId { set; get; }//地区名称+项目名称+采集器名称（因为树状图中使用名称显示的所以用名称的ID比用数字的ID方便）

        public string MD5 { set; get; }//生成的MD5值

        public string TecType { set; get; }//技术类型

        //客户端的ip地址与端口号
        private string endpoint;
        public string Endpoint
        {
            set { endpoint = value; }
            get { return endpoint; }
        }

        //上一次的心跳包时间
        private DateTime onlineTime = DateTime.Now;
        public DateTime OnlineTime
        {
            set { onlineTime = value; }
            get { return onlineTime; }
        }

        /// <summary>
        /// 该用户是否在线 若false则跳出接收线程 若为true一直接收该客户端数据
        /// </summary>
        private bool isOnline = false;
        public bool IsOnline
        {
            get
            {
                return isOnline;
            }
            set
            {
                isOnline = value;
            }
        }


        #endregion

        #region 用户指令工具
        public byte[] command = new byte[1024];//发送指令预定义
        //最后要将有效数据提取出来放在另外一个字节数组中发送出去
        public ushort checkCRC { set; get; }//crc校验码 使用前注意清零

        //public string temReceiveStr = "";//接收数据满包临时存储
        #endregion

        #region 采集显示信息

        //采集器上传周期(单位是小时  默认是600秒)
        private double period = 600;
        public double Period
        {
            set { period = value; }
            get { return period; }
        }

        //最多显示4条曲线
        //public ArrayList arrDisCollectPointCode = new ArrayList(4) { "00","00","00","00"};//该对象需要显示的采集点id
        //public ArrayList arrDisCollectDataName = new ArrayList(4) { "", "", "", "" };//该对象需要显示的监测数据名称 y轴显示参数

        public List<string> arrDisCollectPointCode = new List<string>(4) { "00", "00", "00", "00" };//该对象需要显示的采集点id
        public List<string> arrDisCollectDataName = new List<string>(4) { "", "", "", "" };//该对象需要显示的监测数据名称 y轴显示参数

        //采集点数据(存放从下位机采集上来的各个采集点数据)
        public ArrayList arrCollectData;//数据
        public ArrayList arrCollectDataTimeStamp;//每个数据的时间戳
        public ArrayList arrCollectDataQualityCode;//每个数据的质量码
        //**********采集点数量***********//
        private int collectPointNum;
        public int CollectPointNum
        {
            get { return collectPointNum; }
            set
            {
                collectPointNum = value;
                arrCollectData = new ArrayList(collectPointNum);//根据采集点数量定义arraylist的容量
                arrCollectDataTimeStamp = new ArrayList(collectPointNum);//根据采集点数量定义arraylist的容量
                arrCollectDataQualityCode = new ArrayList(collectPointNum);//根据采集点数量定义arraylist的容量
                for (int i = 0; i < collectPointNum; i++)//对数组进行赋值 便于初次赋值
                {
                    arrCollectData.Insert(i, "null");
                    arrCollectDataQualityCode.Insert(i, "null");
                    arrCollectDataTimeStamp.Insert(i, "null");
                }
            }
        }

        //***************计算指标*****************//
        //public double Q { set; get; }//热量
        public string EER { set; get; }//能效比
        public string CER { set; get; }//费效比
        //public string Qbm { set; get; }//常规能源替代量
        public string CO2 { set; get; }//二氧化碳减排量
        public string SO2 { set; get; }//二氧化硫减排量
        public string CollectHeat { set; get; }//集热系统得热量
        public string Efficiency { set; get; }//集热系统效率
        public string Reliability { set; get; }//太阳能保证率

        //****************************************
        //富源指标 20130301
        //太阳能总得热量
        private string qs = "0";
        public string Qs
        {
            set { qs = value; }
            get { return qs; }
        }

        //太阳能集热量
        private string qc = "0";
        public string Qc
        {
            set { qc = value; }
            get { return qc; }
        }

        //热泵热源供热量
        private string qhp = "0";
        public string Qhp
        {
            set { qhp = value; }
            get { return qhp; }
        }

        //电加热供热量
        private string qeh = "0";
        public string Qeh
        {
            set { qeh = value; }
            get { return qeh; }
        }

        //其他辅助供热量
        private string qoh = "0";
        public string Qoh
        {
            set { qoh = value; }
            get { return qoh; }
        }

        //辅助能源供热量
        private string qaux = "0";
        public string Qaux
        {
            set { qaux = value; }
            get { return qaux; }
        }

        //用户供水热量
        private string quse = "0";
        public string Quse
        {
            set { quse = value; }
            get { return quse; }
        }

        //太阳能热水系统供热量
        private string qsh = "0";
        public string Qsh { set { qsh = value; } get { return qsh; } }

        //用户管路循环热损失量
        private string qtc = "0";
        public string Qtc { set { qtc = value; } get { return qtc; } }

        //水箱热损失
        private string qtanks = "0";
        public string Qtanks { set { qtanks = value; } get { return qtanks; } }

        //常规能源替代量
        private string qbm = "0";
        public string Qbm { set { qbm = value; } get { return qbm; } }

        //节煤量
        private string qss = "0";
        public string Qss { set { qss = value; } get { return qss; } }

        //累计二氧化碳减排量
        private string mco2 = "0";
        public string mCO2 { set { mco2 = value; } get { return mco2; } }

        //累计二氧化硫减排量
        private string mso2 = "0";
        public string mSO2 { set { mso2 = value; } get { return mso2; } }

        //累计氮氧化物减排量
        private string mnox = "0";
        public string mNOx { set { mnox = value; } get { return mnox; } }

        //累计粉尘减排量
        private string mfc1 = "0";
        public string mfc { set { mfc1 = value; } get { return mfc1; } }

        //系统总耗电量
        private string esys = "0";
        public string Esys { set { esys = value; } get { return esys; } }

        //热泵总耗电量
        private string ehp = "0";
        public string Ehp { set { ehp = value; } get { return ehp; } }

        //电加热总耗电量
        private string eeh = "0";
        public string Eeh { set { eeh = value; } get { return eeh; } }

        //系统总耗水
        private string mwater = "0";
        public string Mwater { set { mwater = value; } get { return mwater; } }

        //太阳能保证率
        private string f1 = "0";
        public string f { set { f1 = value; } get { return f1; } }

        //热泵能效比
        private string cophp = "0";
        public string COPhp { set { cophp = value; } get { return cophp; } }

        //系统总热性能系数
        private string copsys = "0";
        public string COPsys { set { copsys = value; } get { return copsys; } }

        //系统实际使用的性能系数
        private string copr = "0";
        public string COPr { set { copr = value; } get { return copr; } }

        //太阳能集热系统性能
        private string copc = "0";
        public string COPc { set { copc = value; } get { return copc; } }

        //太阳能集热器热效率
        private string nu1 = "0";
        public string nc { set { nu1 = value; } get { return nu1; } }

        //太阳能集热系统总的热量
        private string allqc = "0";
        public string AllQc { set { allqc = value; } get { return allqc; } }

        //热泵系统总得热量
        private string allhp = "0";
        public string AllQhp { set { allhp = value; } get { return allhp; } }

        //系统总耗电
        private string alles = "0";
        public string AllEsys { set { alles = value; } get { return alles; } }

        //热泵总耗电
        private string allepump = "0";
        public string AllEhp { set { allepump = value; } get { return allepump; } }
        //电加热总耗电
        private string alleele = "0";
        public string AllEeh { set { alleele = value; } get { return alleele; } }
        //系统总耗水量
        private string allmwater = "0";
        public string AllMwater { set { allmwater = value; } get { return allmwater; } }

        //*****************************************
        //每次采集器的上传数据的时间（作为统计量的时间戳）
        public string sampleTime { set; get; }

        #endregion

        #region 标志位信息

        public bool IsCurveFin { set; get; }//是否已经配置显示曲线

        public bool IsCollectFin { set; get; }//是否已经采集完成

        public int CountMD5 { set; get; }//md5验证次数统计 初始值是0

        public bool IsMD5OK { set; get; }//md5验证是否成功

        public int CountHeartbeat { set; get; }//心跳包验证次数

        //public bool IsGivenTimeOk { set; get; }//对下位机的授时是否成功
        public bool IsFullReceive { set; get; }//判断数据是否满包标志位
        public bool IsFullReceiveIndex { set; get; }//判断统计数据是否满包标志位
        #endregion

        public string StrTemp { set; get; }//临时存储字符串
        public int iCount { set; get; }//上一帧的数据长度

        public User(TcpClient client)
        {
            this.client = client;
            NetworkStream networkStream = client.GetStream();
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
            //数据包包头部分不变
            command[0] = 0x68;
            command[1] = 0x68;
            command[2] = 0x16;
            command[3] = 0x16;

        }
        public void Close()//执行该函数后下位机后自动断开连接并重新连接
        {
            br.Close();
            bw.Close();
            client.Close(); //释放此 System.Net.Sockets.TcpClient 实例，而不关闭基础连接。
        }

    }
}
