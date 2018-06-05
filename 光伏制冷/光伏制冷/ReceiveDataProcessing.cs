using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Data;

namespace 光伏制冷
{
    class ReceiveDataProcessing
    {
        private static DataAccess dataaccess = new DataAccess();

        #region 数据处理函数

        #region 发送处理


        /// <summary>
        /// 中心发送随机序列
        /// </summary>
        /// <param name="user">客户端</param>
        /// <param name="ProjectID">工程编码</param>
        /// <param name="CollectorID">采集器编码</param>
        /// <returns></returns>
        public static byte[] GetSendMessageRandomSequence(User user, string projectID, string collectorID, string typeString)
        {
            try
            {
                string c = "";
                for (int i = 0; i < 5; i++)//产生5个随机数字
                {
                    Random r = new Random();
                    c = c + r.Next(0, 9).ToString();//生成随机数组
                }

                //生成MD5值
                string f = GlobalInfo.MD5;//获得MD5密钥
                //XmlNode xn = xd.SelectSingleNode("//MD5");
                //string f = xn.InnerText.ToString();
                user.MD5 = MD5Crypted.Md532(c + f);//MD5密钥与随机数一起生成MD5值 与收上来的MD5数据进行比较
                //如果一致则MD5校验成功

                //生成xml字符串
                string validData = GetSendXML_id_validateANDstand(0, projectID, collectorID, typeString, c);

                //数据封包
                byte[] sendMessage = PacketMessage(user, validData);
                return sendMessage;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 中心发送MD5验证结果
        /// </summary>
        /// <param name="user">客户端</param>
        /// <param name="projectID">项目编号</param>
        /// <param name="collectorID">采集器编号</param>
        /// <param name="typeString">类型码</param>
        /// <param name="result">MD5验证结果</param>
        /// <returns>打包后的完整数据包</returns>
        public static byte[] GetSendMessageMD5Result(User user, string projectID, string collectorID, string typeString, string result)
        {
            string validData = GetSendXML_id_validateANDstand(0, projectID, collectorID, typeString, result);
            byte[] sendMessage = PacketMessage(user, validData);
            return sendMessage;
        }

        /// <summary>
        /// 中心发送授时
        /// </summary>
        /// <param name="user">客户端</param>
        /// <param name="projectID">项目编号</param>
        /// <param name="collectorID">采集器编号</param>
        /// <param name="typeString">类型码</param>
        /// <returns>打包后的完整数据包</returns>
        public static byte[] GetSendMessageTime(User user, string projectID, string collectorID, string typeString)
        {
            string validData = GetSendXML_id_validateANDstand(0, projectID, collectorID, typeString, DateTime.Now.ToString("yyyyMMddHHmmss"));
            byte[] sendMessage = PacketMessage(user, validData);
            return sendMessage;
        }


        /// <summary>
        /// 中心发送上传数据命令
        /// </summary>
        /// <param name="user">客户端</param>
        /// <param name="projectID">项目编号</param>
        /// <param name="collectorID">采集器编号</param>
        /// <param name="typeString">类型码</param>
        /// <param name="begintime">开始时间 格式为yyyyMMddHH</param>
        /// <param name="endtime">结束时间 格式为yyyyMMddHH</param>
        /// <returns>打包后的完整数据包</returns>
        public static byte[] GetSendMessageUploadtime(User user, string projectID, string collectorID, string typeString, string begintime, string endtime)
        {
            string validData = GetSendXML_id_validateANDstand(0, projectID, collectorID, typeString, begintime + "-" + endtime);
            byte[] sendMessage = PacketMessage(user, validData);
            return sendMessage;
        }


        /// <summary>
        /// 中心发送密钥设置命令
        /// </summary>
        /// <param name="user">客户端</param>
        /// <param name="projectID">项目编号</param>
        /// <param name="collectorID">采集器编号</param>
        /// <param name="functionCode">MD5_KEY AES_KEY AES_IV</param>
        /// <param name="content">密钥内容</param>
        /// <returns>打包后的完整数据包</returns>
        public static byte[] GetSendMessageSetKEY(User user, string projectID, string collectorID, string functionCode, string content)
        {
            //string validData = GetSendXML_setkey(projectID, collectorID, typeString, functionCode, content);
            string validData = GetSendXML_id_validateANDstand(1, projectID, collectorID, functionCode, content);
            byte[] sendMessage = PacketMessage(user, validData);
            return sendMessage;
        }

        /// 发送采集器心跳包指令
        /// </summary>
        /// <param name="user">对象</param>
        /// <param name="projectID">项目编号</param>
        /// <param name="collectorID">采集器编号</param>
        /// <returns>打包后的完整数据包</returns>
        public static byte[] GetSendMessageHeart_result(User user, string projectID, string collectorID)
        {
            string validData = GetSendXML_heart_beat(projectID, collectorID);
            byte[] sendMessage = PacketMessage(user, validData);
            return sendMessage;
        }

        /// <summary>
        /// 发送采集器重启指令
        /// </summary>
        /// <param name="user">对象</param>
        /// <param name="projectID">项目编号</param>
        /// <param name="collectorID">采集器编号</param>
        /// <param name="typeString">类型码</param>
        /// <returns>打包后的完整数据包</returns>
        public static byte[] GetSendMessageRestart(User user, string projectID, string collectorID, string typeString)
        {
            string validData = GetSendXML_id_validateANDstand(1, projectID, collectorID, typeString, "1");//"1"表示重启命令
            byte[] sendMessage = PacketMessage(user, validData);
            return sendMessage;
        }


        /// <summary>
        /// 发送周期设置
        /// </summary>
        /// <param name="user">对象</param>
        /// <param name="projectID">项目编号</param>
        /// <param name="collectorID">采集器编号</param>
        /// <param name="typeString">类型码</param>
        /// <returns>打包后的完整数据包</returns>
        public static byte[] GetSendMessagePeriod(User user, string projectID, string collectorID, string typeString, string period)
        {
            string validData = GetSendXML_id_validateANDstand(1, projectID, collectorID, typeString, period);
            byte[] sendMessage = PacketMessage(user, validData);
            return sendMessage;
        }

        #endregion

        #region 接收处理

        /// <summary>
        /// MD5值是否正确
        /// </summary>
        /// <param name="user">客户端</param>
        /// <param name="xd">XmlDocument xd</param>
        /// <returns>MD5值是否正确</returns>
        public static bool IsMD5True(User user, XmlDocument xd)
        {
            XmlNode root = xd.DocumentElement;
            string MD5 = root.SelectSingleNode("//md5").InnerText;
            if (user.MD5 == MD5.ToLower())
            {
                return true;
            }
            else
            { return false; }
        }

        /// <summary>
        /// 填充对象信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ProjectCode">项目名称</param>
        /// <param name="CollectorCode">采集器名称</param>
        /// <returns>0 正确 1 对象不存在或者数据库操作错误</returns>
        public static int FillUserInfo(User user, string ProjectCode, string CollectorCode)
        {
            try
            {
                string areaCode = ProjectCode.Substring(0, 6);//地区编码
                string projectCode = ProjectCode.Substring(6, 3);//项目编码
                string collectorCode = CollectorCode;//采集器编码

                StringBuilder totalCode = new StringBuilder(ProjectCode);
                totalCode = totalCode.Append(CollectorCode);

                switch (totalCode.ToString())
                {
                    case "53342100101": 
                        user.AreaName = "云南省香格里拉市" ;//添加用户地区名称
                        user.AreaCode = areaCode;
                        user.ProjectName = "香格里拉单水箱供暖系统";//添加项目地区名称
                        user.ProjectID = projectCode;//项目编号
                        user.TecType = "单水箱供热系统";//添加技术类型
                        user.CollectorName = "系统采集器";//添加采集器名称
                        user.CollectorID = collectorCode;//添加采集器编号
                        user.UserId = user.AreaName + user.ProjectName + user.CollectorName;//userID ！！！！项目名称！！！！
                        user.CollectPointNum = 14;//定义采集点数量
                        return 0;
                        //break; 
                    case "53342100202":
                        user.AreaName = "云南省香格里拉市";//添加用户地区名称
                        user.AreaCode = areaCode;
                        user.ProjectName = "小中甸纯热泵干燥系统";//添加项目地区名称
                        user.ProjectID = projectCode;//项目编号
                        user.TecType = "纯热泵干燥系统";//添加技术类型
                        user.CollectorName = "系统采集器";//添加采集器名称
                        user.CollectorID = collectorCode;//添加采集器编号
                        user.UserId = user.AreaName + user.ProjectName + user.CollectorName;//userID ！！！！项目名称！！！！
                        user.CollectPointNum = 9;//定义采集点数量
                        return 0;
                        //break; 
                    case "85602100101":
                        user.AreaName = "老挝行政区万象市";//添加用户地区名称
                        user.AreaCode = areaCode;
                        user.ProjectName = "妇幼保健院热水系统";//添加项目地区名称
                        user.ProjectID = projectCode;//项目编号
                        user.TecType = "复杂热水系统";//添加技术类型
                        user.CollectorName = "系统采集器";//添加采集器名称
                        user.CollectorID = collectorCode;//添加采集器编号
                        user.UserId = user.AreaName + user.ProjectName + user.CollectorName;//userID ！！！！项目名称！！！！
                        user.CollectPointNum = 14;//定义采集点数量
                        return 0;
                        //break; 
                    case "85602100202":
                        user.AreaName = "老挝行政区万象市";//添加用户地区名称
                        user.AreaCode = areaCode;
                        user.ProjectName = "研究所热水系统";//添加项目地区名称
                        user.ProjectID = projectCode;//项目编号
                        user.TecType = "简单热水系统";//添加技术类型
                        user.CollectorName = "系统采集器";//添加采集器名称
                        user.CollectorID = collectorCode;//添加采集器编号
                        user.UserId = user.AreaName + user.ProjectName + user.CollectorName;//userID ！！！！项目名称！！！！
                        user.CollectPointNum = 14;//定义采集点数量
                        return 0;
                        //break; 
                    default: 
                        return 0; 
                        //break; 
                }

                #region 2017.01.20前代码
                //string SQL_Area = @"select Province,City from AreaInfo where Code='" + areaCode + "'";
                //DataSet ds_Area = dataaccess.GetDataSet(SQL_Area, GlobalInfo.DefaultDatabase);
                //DataTable dt_Area = ds_Area.Tables[0];
                //string areaName = dt_Area.Rows[0][0].ToString() + dt_Area.Rows[0][1].ToString();

                //user.AreaName = areaName;//添加用户地区名称
                //user.AreaCode = areaCode;

                //string SQL_Project = @"select ProjectName,TecNum from ProjectDetailInfo where AreaName='" + areaName + "' and ProjectCode='" + projectCode + "'";
                //DataSet ds_project = dataaccess.GetDataSet(SQL_Project, GlobalInfo.DefaultDatabase);
                //DataTable dt_project = ds_project.Tables[0];
                //string projectName = dt_project.Rows[0][0].ToString();
                //string tectype = dt_project.Rows[0][1].ToString();

                //user.ProjectName = projectName;//添加项目地区名称
                //user.ProjectID = projectCode;//项目编号
                //user.TecType = tectype;//添加技术类型

                //string SQL_Collector = @"select CollectorName,CollectPointNum from CollectorInfo where AreaName='" + areaName + "' and ProjectName='" + projectName + "' and CollectorCode='" + collectorCode + "'";
                //DataSet ds_collector = dataaccess.GetDataSet(SQL_Collector, GlobalInfo.DefaultDatabase);
                //DataTable dt_collector = ds_collector.Tables[0];
                //string collectorName = dt_collector.Rows[0][0].ToString();
                //string collectPointNum = dt_collector.Rows[0][1].ToString();

                //user.CollectorName = collectorName;//添加采集器名称
                //user.CollectorID = collectorCode;//添加采集器编号

                //user.UserId = areaName + projectName + collectorName;//userID ！！！！项目名称！！！！
                //user.CollectPointNum = Convert.ToInt32(collectPointNum);//定义采集点数量
                //return 0;
                #endregion

            }
            catch
            { 
                return 1; 
            }
        }


        #endregion

        #endregion

        #region xml指令生成及封包函数
        /// <summary>
        /// id_validate（MD5）与stand（设置）功能xml指令获得
        /// </summary>
        /// <param name="code">区别码 0：id_validate 1:stand</param>
        /// <param name="project_id">项目号</param>
        /// <param name="gateway_id">采集装置编号</param>
        /// <param name="type">指令类型</param>
        /// <param name="content">具体内容 采集器重启，定时上传开启，关闭 该值等于0</param>
        /// <returns></returns>
        private static string GetSendXML_id_validateANDstand(int code, string projectId, string gatewayId, string type, string content)
        {
            string path;
            if (code == 0)
            {
                //path = @"id_validate.xml";
                path = GlobalInfo.id_validate;
            }
            else
            {
                //path = @"stand.xml";
                path = GlobalInfo.stand;
            }
            try
            {
                XmlDocument xd = new XmlDocument();
                //xd.Load(path);
                xd.LoadXml(path);
                XmlNode root = xd.DocumentElement;//根节点root
                XmlNodeList rootChild = root.ChildNodes;//获得根节点的子节点
                foreach (XmlNode xn in rootChild)
                {
                    if (xn.Name.ToString() == "common")//子节点common
                    {
                        XmlNodeList commonXNL = xn.ChildNodes;
                        foreach (XmlNode commonXN in commonXNL)//遍历common的子节点
                        {
                            if (commonXN.Name.ToString() == "project_id")
                            {
                                commonXN.InnerText = projectId;//写入工程编号
                            }
                            if (commonXN.Name.ToString() == "gateway_id")
                            {
                                commonXN.InnerText = gatewayId;//写入采集器编号
                            }
                            if (commonXN.Name.ToString() == "type")
                            {
                                commonXN.InnerText = type;//写入指令类型
                            }
                        }
                    }
                    if (xn.Name.ToString() == "id_validate" || xn.Name.ToString() == "stand")//id_validate子节点或stand节点
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute("operation", type);//设置operation的值

                        XmlElement xmlelement = xd.CreateElement(type);//创建与type同名的节点 用于存放内容content
                        xmlelement.InnerText = content;//新的节点
                        xe.RemoveChild(xe.FirstChild);//删除旧的节点
                        xe.AppendChild(xmlelement);//添加节点
                    }
                }
                return xd.OuterXml;
            }
            catch
            { return null; }
        }
        /// <summary>
        /// xml心跳指令
        /// </summary>
        /// <returns></returns>
        private static string GetSendXML_heart_beat(string projectId, string gatewayId)
        {
            //string path = @"heart_beat.xml";
            string path = GlobalInfo.heart_beat;
            try
            {
                XmlDocument xd = new XmlDocument();
                //xd.Load(path);
                xd.LoadXml(path);
                XmlNode root = xd.DocumentElement;//根节点root
                XmlNodeList rootChild = root.ChildNodes;//获得根节点的子节点
                foreach (XmlNode xn in rootChild)
                {
                    if (xn.Name.ToString() == "common")//子节点common
                    {
                        XmlNodeList commonXNL = xn.ChildNodes;
                        foreach (XmlNode commonXN in commonXNL)//遍历common的子节点
                        {
                            if (commonXN.Name.ToString() == "project_id")
                            {
                                commonXN.InnerText = projectId;//写入工程编号
                            }
                            if (commonXN.Name.ToString() == "gateway_id")
                            {
                                commonXN.InnerText = gatewayId;//写入采集器编号
                            }
                        }
                    }
                }
                return xd.OuterXml;
            }
            catch
            { return null; }
        }

        /// <summary>
        /// xml查询（实时采集）指令
        /// </summary>
        /// <param name="project_id"></param>
        /// <param name="gateway_id"></param>
        /// <returns></returns>
        private static string GetSendXML_data(string projectId, string gatewayId)
        {
            //string path = @"data.xml";
            string path = GlobalInfo.data;
            try
            {
                XmlDocument xd = new XmlDocument();
                //xd.Load(path);
                xd.LoadXml(path);
                XmlNode root = xd.DocumentElement;//根节点root
                XmlNodeList rootChild = root.ChildNodes;//获得根节点的子节点
                foreach (XmlNode xn in rootChild)
                {
                    if (xn.Name.ToString() == "common")//子节点common
                    {
                        XmlNodeList commonXNL = xn.ChildNodes;
                        foreach (XmlNode commonXN in commonXNL)//遍历common的子节点
                        {
                            if (commonXN.Name.ToString() == "project_id")
                            {
                                commonXN.InnerText = projectId;//写入工程编号
                            }
                            if (commonXN.Name.ToString() == "gateway_id")
                            {
                                commonXN.InnerText = gatewayId;//写入采集器编号
                            }
                        }
                    }
                }
                return xd.OuterXml;
            }
            catch
            { return null; }
        }

        /// <summary>
        /// xml设置密钥指令
        /// </summary>
        /// <param name="project_id">项目号</param>
        /// <param name="gateway_id">采集器编号</param>
        /// <param name="type">指令类型(setkey)</param>
        /// <param name="code">0：MD5_KEY 1：AES_KEY 2：AES_IV</param>
        /// <param name="content"></param>
        /// <returns></returns>
        private static string GetSendXML_setkey(string projectId, string gatewayId, string type, string functioncode, string content)
        {
            //string path = @"setkey.xml";
            string path = GlobalInfo.setkey;
            try
            {
                XmlDocument xd = new XmlDocument();
                //xd.Load(path);
                xd.LoadXml(path);
                XmlNode root = xd.DocumentElement;//根节点root
                XmlNodeList rootChild = root.ChildNodes;//获得根节点的子节点
                foreach (XmlNode xn in rootChild)
                {
                    if (xn.Name.ToString() == "common")//子节点common
                    {
                        XmlNodeList commonXNL = xn.ChildNodes;
                        foreach (XmlNode commonXN in commonXNL)//遍历common的子节点
                        {
                            if (commonXN.Name.ToString() == "project_id")
                            {
                                commonXN.InnerText = projectId;//写入工程编号
                            }
                            if (commonXN.Name.ToString() == "gateway_id")
                            {
                                commonXN.InnerText = gatewayId;//写入采集器编号
                            }
                            if (commonXN.Name.ToString() == "type")
                            {
                                commonXN.InnerText = type;//写入指令类型
                            }
                        }
                    }
                    if (xn.Name.ToString() == "stand")//stand节点
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute("operation", type);//设置operation的值

                        xe.SelectSingleNode("type").InnerText = functioncode;//类型值
                        xe.SelectSingleNode("key").InnerText = content;//密钥值
                    }
                }
                //xd.PreserveWhitespace = true;
                return xd.OuterXml;
            }
            catch
            { return null; }
        }

        /// <summary>
        /// 加密封包函数（包含CRC校验）
        /// </summary>
        ///// <param name="user">对象</param>
        /// <param name="message">要进行封包的有效数据字符串（未进行加密）</param>
        /// <returns></returns>
        public static byte[] PacketMessage(User user, string message)
        {
            try
            {
                string path = "keyInfo.xml";
                XmlDocument xd = new XmlDocument();
                xd.Load(path);
                XmlNode root = xd.DocumentElement;
                byte[] key = Encoding.ASCII.GetBytes(root.SelectSingleNode("key").InnerText);//获得AES密钥
                byte[] IV = Encoding.ASCII.GetBytes(root.SelectSingleNode("IV").InnerText);//获得AES初始量

                // 1 对xml指令进行AES加密 形成有效数据

                // byte[] key = Encoding.ASCII.GetBytes(GlobalInfo.Key);//获得AES密钥
                //byte[] IV = Encoding.ASCII.GetBytes(GlobalInfo.IV);//获得AES初始量
                byte[] crypted = AES.AEScryptedDataToByte(message, key, IV);//加密后的XML文本内容

                //byte[] crypted = aes.AEScryptedDataToByte("1234567890123456", key, IV);
                //byte[] crypted = Encoding.ASCII.GetBytes(message);
                int count = crypted.Length + 4;//加上四个字节的指令序号 这部分构成有限数据

                //2 封包

                //计算有效数据长度（高位在前（command[6]）） 
                //经过计算，两个字节可以代表的最大（FFFF）65536个字节约为64k 对于发送的数据长度来说足够了
                //user.command[4] = 0x00;
                //user.command[5] = 0x00;
                //user.command[6] = Convert.ToByte((int)count / 256);//有效数据除以256取整 高位字节的大小
                //user.command[7] = Convert.ToByte(count - ((int)count / 256) * 256);//总长度-高位字节的大小=剩余的字节数（由低位字节表示）

                switch (count.ToString().Length)
                {
                    case 0:
                        user.command[4] = 0x30;
                        user.command[5] = 0x30;
                        user.command[6] = 0x30;
                        user.command[7] = 0x30;
                        break;
                    case 1:
                        user.command[4] = 0x30;
                        user.command[5] = 0x30;
                        user.command[6] = 0x30;
                        user.command[7] = Convert.ToByte(count + 30);
                        break;
                    case 2:
                        user.command[4] = 0x30;
                        user.command[5] = 0x30;
                        user.command[6] = Convert.ToByte(int.Parse(count.ToString().Substring(0, 1)) + 48);
                        user.command[7] = Convert.ToByte(int.Parse(count.ToString().Substring(1, 1)) + 48);
                        break;
                    case 3:
                        user.command[4] = 0x30;
                        user.command[5] = Convert.ToByte(int.Parse(count.ToString().Substring(0, 1)) + 48);
                        user.command[6] = Convert.ToByte(int.Parse(count.ToString().Substring(1, 1)) + 48);
                        user.command[7] = Convert.ToByte(int.Parse(count.ToString().Substring(2, 1)) + 48);
                        break;
                    case 4:
                        user.command[4] = Convert.ToByte(int.Parse(count.ToString().Substring(0, 1)) + 48);
                        user.command[5] = Convert.ToByte(int.Parse(count.ToString().Substring(1, 1)) + 48);
                        user.command[6] = Convert.ToByte(int.Parse(count.ToString().Substring(2, 1)) + 48);
                        user.command[7] = Convert.ToByte(int.Parse(count.ToString().Substring(3, 1)) + 48);
                        break;
                    default:
                        break;
                }

                //指令序号(四个字节)
                user.command[8] = 0x30;//指令序号
                user.command[9] = 0x30;//指令序号
                user.command[10] = 0x30;//指令序号
                user.command[11] = 0x30;//指令序号

                //将有效数据写入command指令
                for (int i = 0; i < (count - 4); i++)//command[8+count]是第一位CRC校验 count中包含4个字节的指令序号
                {
                    user.command[12 + i] = crypted[i];
                }

                //3 生成CRC校验码 由包头至有效数据部分生成
                user.checkCRC = 0;//crc校验码清零 很重要
                for (int i = 0; i < (count + 8); i++)//CRC校验
                {
                    user.checkCRC = CRCtest.CRC(user.command[i], user.checkCRC);
                }
                user.command[8 + count] = (byte)(user.checkCRC >> 8);//CRC高位
                user.command[9 + count] = (byte)user.checkCRC;//CRC低位

                //包尾
                user.command[10 + count] = 0x55;
                user.command[11 + count] = 0xAA;
                user.command[12 + count] = 0x55;
                user.command[13 + count] = 0xAA;

                //4 截取command的有效数据（去掉未赋值的空字节）
                byte[] command_Result = new byte[14 + count];
                for (int i = 0; i <= (13 + count); i++)
                {
                    command_Result[i] = user.command[i];
                }
                return command_Result;//返回具有一定长度的字节数组（即发送指令）
            }
            catch
            { return null; }
        }
        #endregion
    }
}
