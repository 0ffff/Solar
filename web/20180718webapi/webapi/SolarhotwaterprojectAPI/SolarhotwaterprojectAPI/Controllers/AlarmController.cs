/***************************************************
 **类 名 称:AlarmController
 **命名空间:SolarhotwaterprojectAPI.Controllers
 **创建时间:2018-04-18 14:15:58
 **作    者:李沈杰
 **描    述：报警页专用
 **修改时间:
 **修 改 人:
***********************************************/
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Security;
using Dos.ORM;
using DCSoft.Utility.Web;
using DCSoft.Utility.Utils;
using Newtonsoft.Json;
using SolarhotwaterprojectDAL.common;
using DCSoft.DBUtilityGeneric.DAL;
using SolarhotwaterprojectModel;
using SolarhotwaterprojectDAL.Project;
using System.Text;
namespace SolarhotwaterprojectAPI.Controllers
{
    public class AlarmController : ApiController
    {

        #region 获取报警信息
        /// <summary>
        /// 获取报警信息
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Get_tb_Alarm()
        {
            ResponseMessage responseMessage = new ResponseMessage(0);
            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                Dictionary<string, object> dcParams = new Dictionary<string, object>();
                dcParams = DictionaryPro.RequestToDic(context);
                int cnt = 0;
                string ProjectCode = DictionaryPro.GetDicValue(dcParams, "ProjectCode");//工程编号
                int pageNumber = int.Parse(DictionaryPro.GetDicValue(dcParams, "pageNumber"));//页码
                int pageSize = int.Parse(DictionaryPro.GetDicValue(dcParams, "pageSize")) ;//页面信息数量
                DateTime startm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "startm"));
                DateTime endtm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "endtm"));
                //DAL
                tb_Alarm_DAL dal = new tb_Alarm_DAL();
                string list = dal.Get_tb_Alarm(startm, endtm, pageNumber, pageSize,out cnt);

                //返回数据
                responseMessage.message = new { total = cnt, rows = list };
                result.Content = new StringContent(JsonHelper.Object2Json(responseMessage), Encoding.GetEncoding("utf-8"), "application/json");
            }
            catch (Exception ex)
            {
                //返回错误信息
                //responseMessage.status = -1;
                //responseMessage.message = ex.Message;

            }

            return result;
        }
        #endregion
    }
}
