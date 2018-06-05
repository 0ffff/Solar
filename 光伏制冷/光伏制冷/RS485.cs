using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace 光伏制冷
{
    class RS485
    {
        /************************************************************************/
        /* 封装一个类用于RS485相关器件的通信                            */
        /************************************************************************/
        public static byte[] recieved = new byte[30];

        public static int CheckType_CRC=0;
        public static int CheckType_BC=1;
        /// <summary>
        /// 西门子控制模块
        /// 命令协议
        /// </summary>
        public static byte[] openX1 = { 0x01, 0x05, 0x00, 0x00, 0xFF, 0x00, 0x8C, 0x3A };//打开X1继电器
        public static byte[] openX2 = { 0x01, 0x05, 0x00, 0x01, 0xFF, 0x00, 0xDD, 0xFA };//打开X2继电器
        public static byte[] openX3 = { 0x01, 0x05, 0x00, 0x02, 0xFF, 0x00, 0x2D, 0xFA };//打开X3继电器
        public static byte[] closeX1 = { 0x01, 0x05, 0x00, 0x00, 0x00, 0x00, 0xCD, 0xCA };//打开X1继电器
        public static byte[] closeX2 = { 0x01, 0x05, 0x00, 0x01, 0x00, 0x00, 0x9C, 0x0A };//打开X2继电器
        public static byte[] closeX3 = { 0x01, 0x05, 0x00, 0x02, 0x00, 0x00, 0x6C, 0x0A };//打开X3继电器
        public static byte[] readStatusX11= { 0x01, 0x02, 0x00, 0x03, 0x00, 0x01, 0x49, 0xCA };//读取光伏/市电（X11）状态命令
        public static byte[] readStatusX12= { 0x01, 0x02, 0x00, 0x04, 0x00, 0x01, 0xF8, 0x0B };      
        public static byte[] readStatusX1 = { 0x01, 0x01, 0x00, 0x00, 0x00, 0x01, 0xFD, 0xCA };//读开关X1的状态
        public static byte[] readStatusX2 = { 0x01, 0x01, 0x00, 0x01, 0x00, 0x01, 0xAC, 0x0A };//读开关X2的状态
        public static byte[] readStatusX3 = { 0x01, 0x02, 0x00, 0x0A, 0x00, 0x01, 0x99, 0xC8};//读开关X3的状态
       
        /// <summary>
        ///  变频器M420通讯协议
        ///  命令协议
        ///  <虽然在RS485类中，但是并不是ModBUs协议>
        /// </summary>
        public static byte[] readM420f = { 0x02, 0x0E, 0x01, 0x10, 0x19, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04 };//读取M420电压
        public static byte[] readM420Volt = { 0x02, 0x0E, 0x01, 0x10, 0x1B, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x06 };//读取M420电流
        public static byte[] readM420Current= { 0x02, 0x0E, 0x01, 0x10, 0x18, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05 };//读取M420频率


        /// <summary>
        /// 直流电测仪表
        /// 命令协议
        /// </summary>
        public static byte[] readDCvolt = { 0x03, 0x04, 0x00, 0x0A, 0x00, 0x02, 0x50, 0x2B };//获取电压
        public static byte[] readDCCurrent = { 0x03, 0x04, 0x00, 0x0C, 0x00, 0x02, 0xB0, 0x2A };//获取电流
        public static byte[] readDCP = { 0x03, 0x04, 0x00, 0x0E, 0x00, 0x02, 0x11, 0xEA };//获取功率
        public static byte[] readDCW = { 0x03, 0x04, 0x00, 0x10, 0x00, 0x02, 0x71, 0xEC };//获取正向电能

        
        /// <summary>
        /// PIDU35a
        /// 命令协议
        /// </summary>
        public static byte[] readUT35ANowTemp = { 0x04, 0x03, 0x07, 0xD2, 0x00, 0x01, 0x25, 0x12 };//获取当前温度值
        public static byte[] readUT35ASetTemp = { 0x04, 0x03, 0x08, 0x34, 0x00, 0x01, 0xC7, 0xF1 };//获取设置温度值
        public static byte[] setUT35ATempValue = { 0x04, 0x06, 0x08, 0x34, 0x00, 0x00, 0x00, 0x00 };//设置温度值部分，后边需要加入 温度（2字节）+CRC校验（2字节）
        public static byte[] setUT35APValue = { 0x04, 0x06, 0x08, 0x41, 0x00, 0x00, 0x00, 0x00 };//设置P部分，后边需要加入 p有效值（2字节）+CRC校验（2字节）
        public static byte[] setUT35AIValue = { 0x04, 0x06, 0x08, 0x42, 0x00, 0x00, 0x00, 0x00 };//设置P部分，后边需要加入 p有效值（2字节）+CRC校验（2字节）
        public static byte[] setUT35ADValue = { 0x04, 0x06, 0x08, 0x43, 0x00, 0x00, 0x00, 0x00 };//设置P部分，后边需要加入 p有效值（2字节）+CRC校验（2字节）
        public static byte[] readUT35APValue = { 0x04, 0x03, 0x08, 0x41, 0x00, 0x01, 0xD6, 0x2B };//读取P值
        public static byte[] readUT35AIValue = { 0x04, 0x03, 0x08, 0x42, 0x00, 0x01, 0x26, 0x2B };//读取i值
        public static byte[] readUT35ADValue = { 0x04, 0x03, 0x08, 0x43, 0x00, 0x01, 0x77, 0xEB };//读取d值
        public static byte[] readUT35APWM = { 0x04, 0x03, 0x07, 0xD4, 0x00, 0x01, 0xC5, 0x13 };//读取占空比




        /// <summary>
        /// PAC3200通讯协议
        /// 命令协议
        /// 
        /// 命名说明：read读 PAC仪表名字 5 设备编号 U1n相电压
        /// </summary>
        //总电度表5
        public static byte[] readPAC5U1n = { 0x05, 0x04, 0x00, 0x01, 0x00, 0x02, 0x21, 0x8F };//（1）获取相电压UL1-N（单位：V）
        public static byte[] readPAC5U2n = { 0x05, 0x04, 0x00, 0x03, 0x00, 0x02, 0x80, 0x4F };//（2）获取相电压UL2-N（单位：V）
        public static byte[] readPAC5U3n = { 0x05, 0x04, 0x00, 0x05, 0x00, 0x02, 0x60, 0x4E };//（3）获取相电压UL3-N（单位：V）
        public static byte[] readPAC5Ia = { 0x05, 0x04, 0x00, 0x0D, 0x00, 0x02, 0xE1, 0x8C };//（4）获取a相电流（单位：A）
        public static byte[] readPAC5Ib = { 0x05, 0x04, 0x00, 0x0F, 0x00, 0x02, 0x40, 0x4C };//（5）获取b相电流（单位：A）
        public static byte[] readPAC5Ic = { 0x05, 0x04, 0x00, 0x11, 0x00, 0x02, 0x20, 0x4a };//（6）获取c相电流（单位：A）
        public static byte[] readPAC5P = { 0x05, 0x04, 0x00, 0x41, 0x00, 0x02, 0x20, 0x5B };//（7）获取总有功功率（单位：W）
        public static byte[] readPAC5f = { 0x05, 0x04, 0x00, 0x37, 0x00, 0x02, 0xC1, 0x81 };//（8）获取频率（单位：A）
        public static byte[] readPAC5W = { 0x05, 0x04, 0x03, 0x21, 0x00, 0x04, 0xA0, 0x03 };//（9）获取正向有功电能（单位：Wh）

        //压缩机电表6
        public static byte[] readPAC6Uab = { 0x06, 0x04, 0x00, 0x07, 0x00, 0x02, 0xC1, 0xBD };//（1）获取线电压Uab
        public static byte[] readPAC6Ubc = { 0x06, 0x04, 0x00, 0x09, 0x00, 0x02, 0xA0, 0x7E };//（2）获取线电压Ubc
        public static byte[] readPAC6Uca = { 0x06, 0x04, 0x00, 0x0B, 0x00, 0x02, 0x01, 0xBE };//（3）获取线电压Uca
        public static byte[] readPAC6Ia = { 0x06, 0x04, 0x00, 0x0D, 0x00, 0x02, 0xE1, 0xBF };//(4) 获取a相电流
        public static byte[] readPAC6Ib = { 0x06, 0x04, 0x00, 0x0F, 0x00, 0x02, 0x40, 0x7F };//（5）获取b相电流
        public static byte[] readPAC6Ic = { 0x06, 0x04, 0x00, 0x11, 0x00, 0x02, 0x20, 0x79 };//（6）获取c相电流
        public static byte[] readPAC6f = { 0x06, 0x04, 0x00, 0x37, 0x00, 0x02, 0xC1, 0xB2 };//（7）获取频率（单位：HZ）
        public static byte[] readPAC6Ps = { 0x06, 0x04, 0x00, 0x3F, 0x00, 0x02, 0x40, 0x70 };//总视在功率
        public static byte[] readPAC6Py = { 0x06, 0x04, 0x00, 0x41, 0x00, 0x02, 0x20, 0x68 };//总有功功率
        public static byte[] readPAC6Pw = { 0x06, 0x04, 0x00, 0x43, 0x00, 0x02, 0x81, 0xA8 };//总无功功率
        public static byte[] readPAC6Pn= { 0x06, 0x04, 0x00, 0x45, 0x00, 0x02, 0x61, 0xA9 };//功率因数
        public static byte[] readPAC6Uj = { 0x06, 0x04, 0x00, 0x2B, 0x00, 0x02, 0x00, 0x74 };//获取a相电压畸变率
        public static byte[] readPAC6Ij = { 0x06, 0x04, 0x00, 0x31, 0x00, 0x02, 0x21, 0xB3 };//获取a相电流畸变率
        public static byte[] readPAC6UB = { 0x06, 0x04, 0x00, 0x47, 0x00, 0x02, 0xC0, 0x69 };//获取电压幅值不平衡度
        public static byte[] readPAC6IB = { 0x06, 0x04, 0x00, 0x49, 0x00, 0x02, 0xA1, 0xAA };//获取电流幅值不平衡度

        public static byte[] readPAC6W = { 0x06, 0x04, 0x03, 0x21, 0x00, 0x04, 0xA0, 0x30 };//（8）获取正向有功电能（单位：Wh）


        //单向电度表7
        public static byte[] readPAC7U1n = { 0x07, 0x04, 0x00, 0x01, 0x00, 0x02, 0x20, 0x6D };//（1）获取相电压UL1-N（单位：V）
        public static byte[] readPAC7I = { 0x07, 0x04, 0x00, 0x0D, 0x00, 0x02, 0xE0, 0x6E };//（2）获取相电流（单位：A）
        public static byte[] readPAC7P = { 0x07, 0x04, 0x00, 0x41, 0x00, 0x02, 0x21, 0xB9 };//（3）获取总有功功率（单位：W）
        public static byte[] readPAC7W = { 0x07, 0x04, 0x03, 0x21, 0x00, 0x04, 0xA1, 0xE1};//（4）获取正向有功电能（单位：W）

        //SM1910B温湿度模块
        public static byte[] readTempAndWet = { 0x08, 0x03, 0x00, 0x00, 0x00, 0x02, 0xC4, 0x92};

        /// <summary>
        /// PID控制仪设置
        /// </summary>
        public double pValue = 0.00;
        public double iValue = 0.00;
        public double fValue = 0.00;

        //设置PID值
        //1. 从方框中获取 P ,I,D值
        //2. 捕捉按钮事件
        //3. 方框中的PID值 从字符串转换为浮点型
        //4. 浮点型值*10，转化为int32型
        //5. int32型转化为字节型
        //6.根据int32型的大小决定在不前边添加0x00
        //7.得到最终发送的字符串


        //读取PID值
        //1. 发送读取命令字符串
        //2. 校验
        //3. 取出有效数据，转换为int32型
        //4. int32型转换为double
        //5. double型/10
        //6. 填充到方框



        /// <summary>
        /// bCC校验，异或校验
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
        /// bcc校验：校验数据传输过程中是否发生改变或者丢失
        ///
        /// </summary>
        /// <param name="msg">需要检验字节的数组</param>
        /// <returns>返回true，表示通过验证;返回false，表示数据改变</returns>
        public static bool bccCheck(byte[] msg)
        {
            byte bccValue = 0;
            for (int i = 1; i < msg.Length - 2; i++)
            {
                bccValue ^= msg[0];
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
        /// bcc校验
        /// </summary>
        /// <param name="msg">传输数据数组</param>
        /// <returns>返回true，表示通过验证;返回false，表示数据改变</returns>
        public static bool crcCheck(byte[] usefulMsg,int Num)
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




        public static byte[] readFromSerialPort(SerialPort serialport)
        {
            //
            serialport.Read(recieved, 0, serialport.BytesToRead);
            serialport.DiscardOutBuffer();
            return recieved;

        }



    }
}
    

