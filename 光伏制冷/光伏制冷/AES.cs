using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Xml;
using System.Configuration;
namespace 光伏制冷
{
    class AES
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">要加密的内容</param>
        /// <param name="key">密钥</param>
        /// <param name="IV">初始量</param>
        /// <returns></returns>
        public static byte[] AEScryptedDataToByte(string data, byte[] key, byte[] IV)
        {
            if (data == null || data.Length <= 0)
            {
                throw new ArgumentNullException("数据为空");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("密钥值为空");
            }
            if (IV == null || IV.Length <= 0)
            {
                throw new ArgumentNullException("初始值为空");
            }
            byte[] encrypted = null;
            //byte[] encrypted1 = null;
            using (AesManaged aes = new AesManaged())
            {
                aes.Key = key;
                aes.IV = IV;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;
                ICryptoTransform icp = aes.CreateEncryptor();
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cryptstream = new CryptoStream(ms, icp, CryptoStreamMode.Write))//用内存流定义加密流
                    {

                        using (StreamWriter sw = new StreamWriter(cryptstream))//写加密流
                        {
                            sw.Write(data);//将明文数据写入流
                            //sw.Flush();
                            //cryptstream.FlushFinalBlock();
                        }
                        encrypted = ms.ToArray();
                    }
                }

            }
            return encrypted;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">要解密的数据</param>
        /// <param name="key">密钥</param>
        /// <param name="IV">初始量</param>
        /// <returns></returns>
        public static string AESDecryptedDataFromstringToString(byte[] data, byte[] key, byte[] IV)
        {
            if (data == null || data.Length <= 0)
            {
                throw new ArgumentNullException("数据为空");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("密钥值为空");
            }
            if (IV == null || IV.Length <= 0)
            {
                throw new ArgumentNullException("初始值为空");
            }
            try
            {
                string receive = null;
                using (AesManaged aes = new AesManaged())
                {
                    aes.KeySize = 128;
                    aes.Key = key;
                    aes.IV = IV;
                    aes.Mode = CipherMode.ECB;
                    aes.Padding = PaddingMode.PKCS7;
                    ICryptoTransform icp = aes.CreateDecryptor(key, IV);

                    using (MemoryStream ms = new MemoryStream(data))//存储密文
                    {
                        using (CryptoStream cryptstream = new CryptoStream(ms, icp, CryptoStreamMode.Read))//用内存流定义加密流
                        {
                            //byte[] dataByte = new byte[data.Length];
                            //cryptstream.Read(dataByte, 0, dataByte.Length);
                            using (StreamReader sr = new StreamReader(cryptstream))//写解密流
                            {
                                receive = sr.ReadToEnd();
                            }
                        }
                    }

                }
                return receive;
            }
            catch
            { return null; }
        }

    }
}
