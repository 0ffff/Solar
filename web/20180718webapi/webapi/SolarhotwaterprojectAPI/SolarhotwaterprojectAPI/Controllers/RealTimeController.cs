/***************************************************
 **类 名 称:ProjectDetailInfoController 
 **命名空间:SolarhotwaterprojectAPI.Controllers
 **创建时间:2018-04-18 14:15:58
 **作    者:李沈杰
 **描    述：能耗监测页专用
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
    public class RealTimeController : ApiController
    {
        #region 获取监测指标
        /// <summary>
        /// 获取监测指标
        /// </summary>
        /// <remarks>
        /// 创 建 人：李沈杰
        /// 创建时间：2018-04-19 13:48:11
        /// 修 订 人：李沈杰
        /// 修订时间：
        /// </remarks>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetMonitoringindex()
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
                VW_Monitoringindex_DAL dal = new VW_Monitoringindex_DAL();
                td = dal.GetVW_Monitoringindex();

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
        #endregion

        #region 查询监测温度曲线

        /// <summary>
        /// 查询检测指标曲线
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetMonitoringcurve()
        {
            ResponseMessage responseMessage = new ResponseMessage(0);
            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");//解决跨域问题
                Dictionary<string, object> dcParams = new Dictionary<string, object>();
                dcParams = DictionaryPro.RequestToDic(context);
                string stcd = DictionaryPro.GetDicValue(dcParams, "stcd");
                string list = "";
                //DAL
                VW_Monitoringindex_DAL dal = new VW_Monitoringindex_DAL();

                if (stcd!="")
                {
                    list = dal.GetMonitoringcurve(stcd);
                }
                 
                int cnt = 0;
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

        #region 查询当天减排量
        /// <summary>
        /// 查询历史水温曲线
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetVW_TodayEmissionreduction()
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
                DateTime starttm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "starttm"));
                DateTime endtm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "endtm"));
                DataTable MyDataTable = new DataTable();
                //DAL
                系统采集器Index_DAL dal = new 系统采集器Index_DAL();
                DataTable list = dal.GetVW_TodayEmissionreduction(starttm, endtm);

                //构造列
                MyDataTable.Columns.Add(new DataColumn("AllQuse", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQss", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("Allmco2", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("Allmso2", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("Allmnox", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("Allmfc", typeof(float)));
                //构造行
                DataRow dr;
   
                    dr = MyDataTable.NewRow();
                    dr["AllQuse"] = Convert.ToDouble(list.Rows[list.Rows.Count - 1][1]) - Convert.ToDouble(list.Rows[0][1]);
                    dr["AllQss"] = (Convert.ToDouble(list.Rows[list.Rows.Count - 1][2]) - Convert.ToDouble(list.Rows[0][2])) * 0.001; //*0.001是为了单位换算
                    dr["Allmco2"] = (Convert.ToDouble(list.Rows[list.Rows.Count - 1][3]) - Convert.ToDouble(list.Rows[0][3])) * 0.001; 
                    dr["Allmso2"] = (Convert.ToDouble(list.Rows[list.Rows.Count - 1][4]) - Convert.ToDouble(list.Rows[0][4])) * 0.001; 
                    dr["Allmnox"] = (Convert.ToDouble(list.Rows[list.Rows.Count - 1][5]) - Convert.ToDouble(list.Rows[0][5])) * 0.001; 
                    dr["Allmfc"] = (Convert.ToDouble(list.Rows[list.Rows.Count - 1][6]) - Convert.ToDouble(list.Rows[0][6])) * 0.001; 
                MyDataTable.Rows.Add(dr);
                    //返回数据
                    responseMessage.message = new { total = cnt, rows = MyDataTable };
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

        #region 查询温度和开关量当天曲线
        /// <summary>
        /// 查询温度和开关量当天曲线
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetTemperatureandswitch()
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
                string stcd = DictionaryPro.GetDicValue(dcParams, "stcd");
                DateTime starttm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "starttm"));
                DateTime endtm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "endtm"));
                //DAL
                RealTime_DAL dal = new RealTime_DAL();
                DataTable list = dal.GetTemperatureandswitch(starttm, endtm, stcd);
                DataTable MyDataTable = new DataTable();
                //构造列
                MyDataTable.Columns.Add(new DataColumn("Num", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("time", typeof(DateTime)));
                DataRow dr;
              
                for (int i=0;i<list.Rows.Count-1;i++)
                {
                    dr = MyDataTable.NewRow();
                    dr["time"] = list.Rows[i][0];
                    dr["Num"] = list.Rows[i][1];
                    MyDataTable.Rows.Add(dr);
                }

                //返回数据
                responseMessage.message = new { total = cnt, rows = MyDataTable };
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

        #region 查询计量包数据指标当天曲线
        /// <summary>
        /// 查询计量包数据指标当天曲线
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetMeasurementIndex()
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
                string stcd = DictionaryPro.GetDicValue(dcParams, "stcd");
                DateTime starttm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "starttm"));
                DateTime endtm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "endtm"));
                //DAL
                RealTime_DAL dal = new RealTime_DAL();
                DataTable list = dal.GetMeasurementIndex(starttm, endtm, stcd);
                DataTable MyDataTable = new DataTable();
                //构造列
                MyDataTable.Columns.Add(new DataColumn("Num", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("time", typeof(DateTime)));
                DataRow dr;

                for (int i = 0; i < list.Rows.Count - 1; i++)
                {
                    dr = MyDataTable.NewRow();
                    dr["time"] = list.Rows[i][0];
                    dr["Num"] = list.Rows[i][1];
                    MyDataTable.Rows.Add(dr);
                }
                //返回数据
                responseMessage.message = new { total = cnt, rows = MyDataTable };
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
