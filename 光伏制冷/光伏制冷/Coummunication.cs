using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Configuration;

namespace 光伏制冷
{
    class Communication
    {
        //private AES aes = new AES();

        #region 接收数据函数
        //**************************************************************
        //下位机每帧数据包含10个采集点将32个采集点分成4帧发送
        //修改时间：20121218
        //修改人：马辰
        //**************************************************************
        /// <summary>
        /// 接收函数
        /// </summary>
        /// <param name="user">客户端</param>
        /// <param name="receiveMessage">有效数据的字符串</param>
        public static void ReceiveMessage(User user, out string receiveMessage)
        {
            try
            {
                TcpClient client = user.client;
                client.ReceiveBufferSize = 4096;//4k数据时接收比较稳定

                int count = 0;
                while (count == 0)//重复判断是否有数据进来
                {
                    //如果停止服务或者该用户掉线则跳出循环
                    if ((GlobalInfo.IsExit == true) || (user.IsOnline == false))
                    {
                        break;
                    }
                    Thread.Sleep(20);
                    count = client.Available;
                }

                Thread.Sleep(500);

                #region 数据读取
                //循环接收数据
                string result = "";//最终读取的内容转化的字符串
                for (int i = 0; i < 10; i++)//10是估计值最大容量40k数据 每帧4k
                {
                    int num = client.Available;//可读数据量
                    if (num > 0)
                    {
                        byte[] receiveMid = new byte[num];
                        string resultMid;//临时存储每次读取的数据
                        try
                        {
                            receiveMid = user.br.ReadBytes(num);//读取数据
                            resultMid = byteToHexStr(receiveMid);//转化为16进制的字符串
                            result += resultMid;//拼接
                            //检查长度和最后四个字节
                            //if ((num != 1350) && (receiveMid[num - 1] == 0xAA) && (receiveMid[num - 2] == 0x55) && (receiveMid[num - 3] == 0xAA) && (receiveMid[num - 4] == 0x55))
                            //{
                            //    user.IsFullReceive = false;//数据没有满包
                            //}
                            //else
                            //{
                            //    user.IsFullReceive = true;//数据满包
                            //}
                        }
                        catch
                        {
                            receiveMessage = null;
                            return;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                #endregion

                //*********************数据满包处理程序段****************************
                var zResult = new List<byte>();

                byte[] receive = strToHexByte(result);//转化为16进制的字节 

                zResult.AddRange(receive);

                string receiveM;
                int zCount = 0;
                receiveMessage = null;

                while (zResult.Count != 0)
                {
                    byte[] buffers = zResult.ToArray();
                    zCount = (buffers[4] - 48) * 1000 + (buffers[5] - 48) * 100 + (buffers[6] - 48) * 10 + (buffers[7] - 48);
                    zCount += 14;

                    byte[] buffer = new byte[zCount];
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        buffer[i] = buffers[i];
                    }


                    if (IsLengthOK(buffer))
                    {
                        if (IsCRCOK(user, buffer))
                        {
                            receiveM = GetFinalMessage(buffer);
                            receiveMessage += receiveM + "|";
                        }
                        else
                        {
                            receiveMessage = "";
                        }
                    }
                    else
                    {
                        receiveMessage = "";
                    }
                    zResult.RemoveRange(0, zCount);
                    zCount = 0;
                }


                #region 源代码
                //if (receive[0] == 104)//判断头帧 0x68
                //{
                //    //判断数据长度是否正确
                //    int Count = (receive[4] - 48) * 1000 + (receive[5] - 48) * 100 + (receive[6] - 48) * 10 + (receive[7] - 48);
                //    if (Count == receive.Length - 14)// 4个包头 4个数据长度 2个CRC 4个数据包尾
                //    {
                //        user.checkCRC = 0;//CRC校码清零
                //        //由收上来的数据生成CRC检验码
                //        for (int i = 0; i < (receive.Length - 6); i++)//CRC校验
                //        {
                //            user.checkCRC = CRCtest.CRC(receive[i], user.checkCRC);
                //        }

                //        //CRC校验  receive[receive.Length - 6]CRC高位    receive[receive.Length - 5]CRC低位   

                //        if ((receive[receive.Length - 6] == (byte)(user.checkCRC >> 8)) && (receive[receive.Length - 5] == (byte)user.checkCRC))
                //        {  //第三步 提取有效数据
                //            byte[] EfficientData = new byte[receive.Length - 14]; //receive.Length-18 M+4字节长度
                //            string DecrypedString = "";//解密后的字符串

                //            for (int i = 0; i < (receive.Length - 14); i++)
                //            {
                //                EfficientData[i] = receive[i + 8];
                //            }

                //            //解密
                //            try
                //            {
                //                //string pathkey = "keyInfo.xml";
                //                //XmlDocument xdkey = new XmlDocument();
                //                //xdkey.Load(pathkey);
                //                //XmlNode rootkey = xdkey.DocumentElement;
                //                //byte[] key = Encoding.ASCII.GetBytes(rootkey.SelectSingleNode("key").InnerText);//获得AES密钥
                //                //byte[] IV = Encoding.ASCII.GetBytes(rootkey.SelectSingleNode("IV").InnerText);//获得AES初始量

                //                //**************解密过程******************//
                //                byte[] key = Encoding.ASCII.GetBytes(GlobalInfo.Key); //获得AES密钥
                //                byte[] IV = Encoding.ASCII.GetBytes(GlobalInfo.IV);   //获得AES初始量

                //                //解密成字符串（该步有可能会添加相应的ASCII码解析）
                //                DecrypedString = AES.AESDecryptedDataFromstringToString(EfficientData, key, IV);
                //                ////richTextBox2.Text = DecrypedString;
                //                //receiveMessage = DecrypedString;//解密后的字符串


                //                //没有解密直接ASCII提取数据
                //                //DecrypedString = Encoding.ASCII.GetString(EfficientData);
                //                //richTextBox2.Text = DecrypedString;
                //                receiveMessage = DecrypedString.Substring(0, DecrypedString.Length);//解密后的字符串 下位机是将M+4个字节全部加密
                //            }
                //            //解密错误
                //            catch
                //            { receiveMessage = ""; }

                //        }
                //        else //CRC校验不正确
                //        {
                //            receiveMessage = "";
                //        }
                //    }
                //    else//数据长度不正确
                //    {
                //        receiveMessage = "";
                //    }
                //}
                //else//数据头不是0x86
                //{
                //    receiveMessage = "";
                //}

                #endregion




            }
            catch
            {
                receiveMessage = "";
            }
        }
        #endregion


        //判断数据长度是否正确
        private static bool IsLengthOK(byte[] receive)
        {
            //第一步 判断数据长度是否正确
            int Count = 0;
            Count = (receive[4] - 48) * 1000 + (receive[5] - 48) * 100 + (receive[6] - 48) * 10 + (receive[7] - 48);
            //判断数据长度是否正确
            //if(Convert.ToInt32(Count,16)==receive.Length - 14)
            if (Count == receive.Length - 14)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //由包头至有效数据生成CRC与收上来的数据CRC进行比较
        private static bool IsCRCOK(User user, byte[] receive)
        {
            //第二步 由包头至有效数据生成CRC与收上来的数据CRC进行比较
            user.checkCRC = 0;//crc校验码清零 很重要
            //由收上来的数据生成CRC检验码
            for (int i = 0; i < (receive.Length - 6); i++)//CRC校验
            {
                user.checkCRC = CRCtest.CRC(receive[i], user.checkCRC);
            }

            //CRC校验  receive[receive.Length - 6]CRC高位    receive[receive.Length - 5]CRC低位    
            if ((receive[receive.Length - 6] == (byte)(user.checkCRC >> 8)) && ((byte)user.checkCRC == receive[receive.Length - 5]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //获取最终数据 包括提取有效数据和解密
        private static string GetFinalMessage(byte[] receive)
        {
            byte[] EfficientData = new byte[receive.Length - 14];//receive.Length-18 M+4字节长度
            string DecrypedString = "";//解密后的字符串

            for (int i = 0; i < (receive.Length - 14); i++)
            {
                EfficientData[i] = receive[i + 8];
            }
            //解密
            try
            {
                //**************解密过程******************//
                byte[] key = Encoding.ASCII.GetBytes(GlobalInfo.Key);//获得AES密钥
                byte[] IV = Encoding.ASCII.GetBytes(GlobalInfo.IV);//获得AES初始量

                //解密成字符串（该步有可能会添加相应的ASCII码解析）
                DecrypedString = AES.AESDecryptedDataFromstringToString(EfficientData, key, IV);
                //receiveMessage = DecrypedString.Substring(4, DecrypedString.Length - 4);//解密后的字符串 下位机是将M+4个字节全部加密
                //return DecrypedString.Substring(4, DecrypedString.Length - 4);

                return DecrypedString.Substring(0, DecrypedString.Length);
            }
            //解密错误
            catch
            {
                return "";
            
            }
        }

        #region 发送数据函数

        public static void SendToClient(User user, byte[] command)
        {
            try
            {
                user.bw.Write(command);
                user.bw.Flush();
            }

            catch
            { }
        }
        #endregion

        #region 转换函数
        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)

                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)

                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);


            return returnBytes;
        }

        private static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("x2");
                }
            }
            return returnStr;
        }

        #endregion

    }
}
