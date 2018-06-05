using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace 光伏制冷
{
    class MD5Crypted
    {
        //返回32个字符
        public static string Md532(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();

            //byte[] s = md5.ComputeHash(Encoding.Default.GetBytes(cl));
            byte[] s = md5.ComputeHash(Encoding.ASCII.GetBytes(cl));
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString("x2");
            }

            return pwd;
        }
    }
}
