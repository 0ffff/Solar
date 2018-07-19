/***************************************************
 **类 名 称:ProjectDetailInfoController 
 **命名空间:SolarhotwaterprojectAPI.Controllers
 **创建时间:2018-04-18 14:15:58
 **作    者:李沈杰
 **描    述：工程信息页专用
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
    public class ProjectDetailInfoController : ApiController
    {
        /// <summary>
        /// 项目详细信息
        /// </summary>
        /// <remarks>
        /// 创 建 人：李沈杰
        /// 创建时间：2018-04-18 13:48:11
        /// 修 订 人：李沈杰
        /// 修订时间：
        /// </remarks>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetProject()
        {
            ResponseMessage responseMessage = new ResponseMessage(0);
            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                bool flag = false;
                HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                Dictionary<string, object> dcParams = new Dictionary<string, object>();
                dcParams = DictionaryPro.RequestToDic(context);
                string ProjectCode = DictionaryPro.GetDicValue(dcParams, "ProjectCode");//工程编号

                DataTable td = new DataTable();
                ProjectDetailInfo_DAL dal = new ProjectDetailInfo_DAL();
                 td = dal.GetProjectDetailInfo(ProjectCode);

                responseMessage.message = new { rows = td };
                result.Content = new StringContent(JsonHelper.Object2Json(responseMessage), Encoding.GetEncoding("utf-8"), "application/json");
            }

            catch (Exception ex)
            {
                //返回错误信息
                //返回错误信息
                responseMessage.status = -1;
                responseMessage.message = ex.Message;
            }
            return result;

        }

        /// <summary>
        /// 获取最新一条实时数据
        /// </summary>
        /// <remarks>
        /// 创 建 人：李沈杰
        /// 创建时间：2018-04-18 13:48:11
        /// 修 订 人：张超 
        /// 修订时间：2018-7-12
        /// 修改内容：修改“系统采集器_DAL”为"系统采集器实时_DAL"
        /// </remarks>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetRealtimedata()
        {
            ResponseMessage responseMessage = new ResponseMessage(0);
            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                bool flag = false;
                HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                Dictionary<string, object> dcParams = new Dictionary<string, object>();
                dcParams = DictionaryPro.RequestToDic(context);

                DataTable td = new DataTable();
                系统采集器实时_DAL dal = new 系统采集器实时_DAL();
                 td = dal.GetLastDate();

                responseMessage.message = new { rows = td };
                result.Content = new StringContent(JsonHelper.Object2Json(responseMessage), Encoding.GetEncoding("utf-8"), "application/json");
            }

            catch (Exception ex)
            {
                //返回错误信息
                //返回错误信息
                responseMessage.status = -1;
                responseMessage.message = ex.Message;
            }
            return result;

        }


        /// <summary>
        /// 获取最新一条flash 相关的信息
        /// </summary>
        /// <returns></returns>
        /// 
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetRealTimeFlash()
        {
            ResponseMessage responseMessage = new ResponseMessage(0);
            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                Dictionary<string, object> dcParams = new Dictionary<string, object>();
                dcParams = DictionaryPro.RequestToDic(context);

                DataTable td = new DataTable();
                系统采集器实时_DAL dal = new 系统采集器实时_DAL();
                td = dal.GetFlashData();

                responseMessage.message = new { rows = td };
                result.Content = new StringContent(JsonHelper.Object2Json(responseMessage), Encoding.GetEncoding("utf-8"), "application/json");

            }
            catch (Exception ex)
            {
                //返回错误信息
                //返回错误信息
                responseMessage.status = -1;
                responseMessage.message = ex.Message;

            }
            return result;
        }


        /// <summary>
        /// 获取最新一条参数
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetLastCanshu()
        {
            ResponseMessage responseMessage = new ResponseMessage(0);
            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                bool flag = false;
                HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                Dictionary<string, object> dcParams = new Dictionary<string, object>();
                dcParams = DictionaryPro.RequestToDic(context);

                DataTable td = new DataTable();
                系统采集器参数_DAL dal = new 系统采集器参数_DAL();
                td = dal.GetLastCanShu();

                responseMessage.message = new { rows = td };
                result.Content = new StringContent(JsonHelper.Object2Json(responseMessage), Encoding.GetEncoding("utf-8"), "application/json");
            }

            catch (Exception ex)
            {
                //返回错误信息
                //返回错误信息
                responseMessage.status = -1;
                responseMessage.message = ex.Message;
            }
            return result;

        }

        /// <summary>
        /// 获取最新一条电量消耗(用于首页右侧数据)
        /// </summary>
        /// <remarks>
        /// 创 建 人：李沈杰
        /// 创建时间：2018-04-19 13:48:11
        /// 修 订 人：张超
        /// 修订时间：2018-7-17
        /// 把之前 浙江丽水项目的计量数据 改为 云师大光伏制冷的 电能数据
        /// </remarks>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetLastQoeIndex() //get last quantity of electric index
        {
            ResponseMessage responseMessage = new ResponseMessage(0);
            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                bool flag = false;
                HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                Dictionary<string, object> dcParams = new Dictionary<string, object>();
                dcParams = DictionaryPro.RequestToDic(context);

                DataTable td = new DataTable();
                系统采集器实时_DAL dal = new 系统采集器实时_DAL();
                td = dal.GetLastQoeIndex();

                responseMessage.message = new { rows = td };
                result.Content = new StringContent(JsonHelper.Object2Json(responseMessage), Encoding.GetEncoding("utf-8"), "application/json");
            }

            catch (Exception ex)
            {
                //返回错误信息
                //返回错误信息
                responseMessage.status = -1;
                responseMessage.message = ex.Message;
            }
            return result;

        }

        /// <summary>
        /// 获取最新一条计量数据(用于首页FLASH数据)
        /// </summary>
        /// <remarks>
        /// 创 建 人：李沈杰
        /// 创建时间：2018-04-19 13:48:11
        /// 修 订 人：李沈杰
        /// 修订时间：
        /// </remarks>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetLastNewIndex()
        {
            ResponseMessage responseMessage = new ResponseMessage(0);
            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                bool flag = false;
                HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                Dictionary<string, object> dcParams = new Dictionary<string, object>();
                dcParams = DictionaryPro.RequestToDic(context);

                DataTable td = new DataTable();
                系统采集器Index_DAL dal = new 系统采集器Index_DAL();
                td = dal.GetLastNewIndex();

                responseMessage.message = new { rows = td };
                result.Content = new StringContent(JsonHelper.Object2Json(responseMessage), Encoding.GetEncoding("utf-8"), "application/json");
            }

            catch (Exception ex)
            {
                //返回错误信息
                //返回错误信息
                responseMessage.status = -1;
                responseMessage.message = ex.Message;
            }
            return result;

        }
    }
}
