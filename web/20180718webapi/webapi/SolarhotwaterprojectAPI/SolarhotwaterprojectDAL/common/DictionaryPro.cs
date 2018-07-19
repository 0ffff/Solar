using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolarhotwaterprojectDAL.common
{
    public class DictionaryPro
    {
        public static Dictionary<string, object> RequestToDic(HttpContextBase context)
        {
            Dictionary<string, object> dcParams = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            foreach (string key in context.Request.QueryString.AllKeys)
            {
                string val = context.Request.QueryString[key];
                if (val == "null")
                {
                    val = null;
                }
                dcParams.Add(key, val);
            }
            foreach (string key in context.Request.Form.AllKeys)
            {

                if (!dcParams.ContainsKey(key))
                {
                    string val = context.Request.Form[key];
                    if (val == "null")
                    {
                        val = null;
                    }
                    dcParams.Add(key, val);
                }
            }
            return dcParams;
        }

        /// <summary>
        /// 根据key获取字典对象的值
        /// </summary>
        /// <param name="dcParams"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDicValue(Dictionary<string, object> dcParams, string key)
        {
            if (dcParams == null || !dcParams.ContainsKey(key) || dcParams[key] == null)
            {
                return "";
            }
            return dcParams[key].ToString();

        }

        /// <summary>
        /// 根据key获取字典对象的值,key不存在抛出异常
        /// </summary>
        /// <param name="dcParams"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDicValueMust(Dictionary<string, object> dcParams, string key)
        {
            if (dcParams == null || !dcParams.ContainsKey(key))
            {
                throw new Exception("缺少参数：" + key);
            }
            return GetDicValue(dcParams, key);

        }
    }
}
