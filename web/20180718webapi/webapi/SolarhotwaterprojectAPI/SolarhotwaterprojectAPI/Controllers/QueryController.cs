/***************************************************
 **类 名 称:QueryController 
 **命名空间:SolarhotwaterprojectAPI.Controllers
 **创建时间:2018-04-18 14:15:58
 **作    者:李沈杰
 **描    述：计量页专用
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
    public class QueryController : ApiController
    {
        #region 按时间查询监测指标曲线
        /// <summary>
        /// 按时间查询监测指标曲线
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
                Query_DAL dal = new Query_DAL();
                DataTable list = dal.GetMeasurementIndexBytime(starttm, endtm, stcd);

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

        #region 按时间查询监测表格数据
        /// <summary>
        /// 按时间查询监测指标曲线
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetDateBytime()
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
                int pageNumber = int.Parse(DictionaryPro.GetDicValue(dcParams, "pageNumber"));//页码
                int pageSize = int.Parse(DictionaryPro.GetDicValue(dcParams, "pageSize"));//页面信息数量
                //DAL
                系统采集器实时_DAL dal = new 系统采集器实时_DAL();
                DataTable list = dal.GetDateBytime(starttm, endtm, pageNumber, pageSize, out cnt);

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

        #region 获取每月所有天的计量数据
        /// <summary>
        /// 获取每月所有天的计量数据
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Get_tb_DayIndex()
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
                string time = DictionaryPro.GetDicValue(dcParams, "time");
                string stcd = DictionaryPro.GetDicValue(dcParams, "stcd");
                DateTime tm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "tm"));//年初时间
                // DateTime starttm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "starttm"));
                //DateTime endtm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "endtm"));
                //DAL
                tb_DayIndex_DAL dal = new tb_DayIndex_DAL();
                DataTable lastone = dal.Get_tb_MonthIndex_beforeone(stcd, tm);//前月最后一条数据
                DataTable list = dal.Get_tb_DayIndex(time, stcd);

                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add(new DataColumn("TimeStamp", typeof(DateTime)));
                MyDataTable.Columns.Add(new DataColumn("Index_tag", typeof(float)));
                //构造行list.Rows.Count年数据只有一条
                DataRow dr;
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    dr = MyDataTable.NewRow();
                    dr["TimeStamp"] = Convert.ToDateTime(list.Rows[i][0]);
                    if (lastone.Rows.Count > 0 && i == 0)
                    {
                        dr["Index_tag"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][1]) - Convert.ToDouble(lastone.Rows[0][stcd]));
                    }
                    else
                    {
                        dr["Index_tag"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][1]) - Convert.ToDouble(list.Rows[i - 1][1]));
                    }


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

        #region 获取每天24小时的计量数据 
        /// <summary>
        /// 获取每天24小时的计量数据
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Get_tb_HourIndex()
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
                //DateTime starttm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "starttm"));
                //DateTime endtm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "endtm"));
                string time = DictionaryPro.GetDicValue(dcParams, "time");
                string stcd = DictionaryPro.GetDicValue(dcParams, "stcd");
                DateTime tm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "tm"));//年初时间
                //DAL
                tb_DayIndex_DAL dal = new tb_DayIndex_DAL();
                DataTable lastone = dal.Get_tb_HourIndex_beforeone(stcd, tm);//前月最后一条数据
                DataTable list = dal.Get_tb_HourIndex(time, stcd);
                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add(new DataColumn("TimeStamp", typeof(DateTime)));
                MyDataTable.Columns.Add(new DataColumn("Index_tag", typeof(float)));
                //构造行list.Rows.Count年数据只有一条
                DataRow dr;
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    dr = MyDataTable.NewRow();
                    dr["TimeStamp"] = Convert.ToDateTime(list.Rows[i][0]);
                    if (lastone.Rows.Count > 0 && i == 0)
                    {
                        dr["Index_tag"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][1]) - Convert.ToDouble(lastone.Rows[0][stcd]));
                    }
                    else
                    {
                        dr["Index_tag"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][1]) - Convert.ToDouble(list.Rows[i - 1][1]));
                    }


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

        #region 获取每年所有月的计量数据 
        /// <summary>
        /// 获取每年所有月的计量数据
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Get_tb_MonthIndex()
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
                //DAL
                tb_DayIndex_DAL dal = new tb_DayIndex_DAL();
                DataTable list = dal.Get_tb_MonthIndex(starttm, endtm);

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

        #region 获取每年所有的计量数据 
        /// <summary>
        /// 获取每年所有的计量数据
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Get_tb_YearIndex()
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
                tb_DayIndex_DAL dal = new tb_DayIndex_DAL();
                DataTable list = dal.Get_tb_YearIndex(stcd,starttm, endtm);

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

        #region 按年份查询计量数据 
        /// <summary>
        /// 按年份查询计量数据
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetIndexByyear()
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
                string time = DictionaryPro.GetDicValue(dcParams, "time");
                DateTime tm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "tm"));//年初时间
                //DAL
                tb_DayIndex_DAL dal = new tb_DayIndex_DAL();
                DataTable lastone = dal.Get_tb_YearIndex_beforeone( tm);//去年最后一条数据
                DataTable list = dal.GetIndexByyear(stcd, time);
                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add(new DataColumn("TimeStamp", typeof(DateTime)));
                MyDataTable.Columns.Add(new DataColumn("Index_tag", typeof(float)));
                //构造行list.Rows.Count年数据只有一条
                DataRow dr;
                for (int i=0;i<list.Rows.Count;i++)
                {
                    dr = MyDataTable.NewRow();
                    dr["TimeStamp"] = Convert.ToDateTime(list.Rows[i][0]);
                    if (lastone.Rows.Count>0)
                    {
                   
                   
                      dr["Index_tag"] = Convert.ToDouble(Convert.ToDouble(list.Rows[0][1]) - Convert.ToDouble(lastone.Rows[0][stcd]));
             
                       
                    }
                    else
                    {
                        dr["Index_tag"] = Convert.ToDouble(Convert.ToDouble(list.Rows[0][1]));
                    }
                  

                    MyDataTable.Rows.Add(dr);
                }
              
                //返回数据
                responseMessage.message = new { total = cnt, rows = MyDataTable };
                result.Content = new StringContent(JsonHelper.Object2Json(responseMessage), Encoding.GetEncoding("utf-8"), "application/json");
            }
            catch (Exception ex)
            {
                //返回错误信息
                responseMessage.status = -1;
                responseMessage.message = ex.Message;

            }

            return result;
        }
        #endregion

        #region 按月查询计量数据 
        /// <summary>
        /// 按年份查询计量数据
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetIndexBymonth()
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
                string time = DictionaryPro.GetDicValue(dcParams, "time");
                DateTime tm = DateTime.Parse(DictionaryPro.GetDicValue(dcParams, "tm"));//年初时间
                //DAL
                //DAL
                tb_DayIndex_DAL dal = new tb_DayIndex_DAL();
                DataTable lastone = dal.Get_tb_MonthIndex_beforeone(stcd, tm);//去年最后一条前一条数据的计量数据
                DataTable list = dal.GetIndexBymonth(stcd, time);
                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add(new DataColumn("TimeStamp", typeof(DateTime)));
                MyDataTable.Columns.Add(new DataColumn("Index_tag", typeof(float)));
                //构造行list.Rows.Count年数据只有一条
                DataRow dr;
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    dr = MyDataTable.NewRow();
                    dr["TimeStamp"] = Convert.ToDateTime(list.Rows[i][0]);
                    if (lastone.Rows.Count >0&&i==0)
                    {
                        dr["Index_tag"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][1]) - Convert.ToDouble(lastone.Rows[0][stcd]));
                    }
                    else
                    {
                     
                    
                            dr["Index_tag"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][1]) - Convert.ToDouble(list.Rows[i - 1][1]));

                       
                    }


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

        #region 获取项目某项指标最新的计量数据 
        /// <summary>
        /// 获取项目某项指标最新的计量数据
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetsomeoneLastNewIndex()
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
                //DAL
                系统采集器Index_DAL dal = new 系统采集器Index_DAL();
                DataTable lastone = dal.GetsomeoneLastNewIndex(stcd);//前月最后一条数据

               

                //返回数据
                responseMessage.message = new { total = cnt, rows = lastone };
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

        #region 获取去年最后一条的计量数据 
        /// <summary>
        /// 获取去年最后一条的计量数据
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetsomeoneLastyearlastdataIndex()
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
                string tm = DictionaryPro.GetDicValue(dcParams, "tm");//年初时间
                tb_YearIndex_DAL dal = new tb_YearIndex_DAL();
                DataTable lastone = dal.Get_tb_yearlastoneIndex(tm, stcd);//获取上月最后一条的计量数据



                //返回数据
                responseMessage.message = new { total = cnt, rows = lastone };
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

        #region 获取上月最后一条的计量数据 
        /// <summary>
        /// 获取上月最后一条的计量数据
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetsomeoneLastmonthlastdataIndex()
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
                string tm =DictionaryPro.GetDicValue(dcParams, "tm");
                tb_MonthIndex_DAL dal = new tb_MonthIndex_DAL();
                DataTable lastone = dal.Get_tb_MonthlastoneIndex(tm, stcd);//获取上月最后一条的计量数据



                //返回数据
                responseMessage.message = new { total = cnt, rows = lastone };
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

        #region 获取昨天最后一条的计量数据 
        /// <summary>
        /// 获取昨天最后一条的计量数据
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetsomeoneLastDaylastIndex()
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
                string tm = DictionaryPro.GetDicValue(dcParams, "tm");
                tb_DayIndex_DAL dal = new tb_DayIndex_DAL();
                DataTable lastone = dal.Get_tb_DaylastoneIndex(tm, stcd);



                //返回数据
                responseMessage.message = new { total = cnt, rows = lastone };
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

        #region 获取去年同期的计量数据 
        /// <summary>
        /// 获取去年同期的计量数据
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Get_lastyearsomedataIndex()
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
                string starttm = DictionaryPro.GetDicValue(dcParams, "starttm");//
                string endtm = DictionaryPro.GetDicValue(dcParams, "endtm");//
                tb_MonthIndex_DAL dal = new tb_MonthIndex_DAL();
                DataTable lastone = dal.Get_lastyearsomedataIndex(starttm, endtm, stcd);



                //返回数据
                responseMessage.message = new { total = cnt, rows = lastone };
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

        #region 获取上月计量数据累计量 
        /// <summary>
        /// 获取去年同期的计量数据
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Get_lastmonthsummaryindex()
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
                tb_MonthIndex_DAL dal = new tb_MonthIndex_DAL();
                DataTable lastone = dal.Get_lastmonthsummaryindex(stcd);

                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add(new DataColumn("TimeStamp", typeof(DateTime)));
                MyDataTable.Columns.Add(new DataColumn("Index_tag", typeof(float)));
                DataRow dr;
                   dr = MyDataTable.NewRow();
                   dr["TimeStamp"] = Convert.ToDateTime(lastone.Rows[0][0]);
                   dr["Index_tag"] = Convert.ToDouble(Convert.ToDouble(lastone.Rows[1][1]) - Convert.ToDouble(lastone.Rows[0][1]));
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

        #region 获取昨天计量数据累计量 
        /// <summary>
        /// 获取昨天计量数据累计量
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Get_lastdayhsummaryindex()
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
                tb_DayIndex_DAL dal = new tb_DayIndex_DAL();
                DataTable lastone = dal.Get_lastdayhsummaryindex(stcd);

                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add(new DataColumn("TimeStamp", typeof(DateTime)));
                MyDataTable.Columns.Add(new DataColumn("Index_tag", typeof(float)));

                DataRow dr;
                dr = MyDataTable.NewRow();
                dr["TimeStamp"] = Convert.ToDateTime(lastone.Rows[0][0]);
                dr["Index_tag"] = Convert.ToDouble(Convert.ToDouble(lastone.Rows[1][1]) - Convert.ToDouble(lastone.Rows[0][1]));
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

        #region 获取某指标计量数据的年平均值 
        /// <summary>
        /// 获取某指标计量数据的年平均值
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Get_yearavgIndesx()
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
                string tm = DictionaryPro.GetDicValue(dcParams, "tm");
                tb_YearIndex_DAL dal = new tb_YearIndex_DAL();
                DataTable lastone = dal.Get_yearavgIndesx(tm,stcd);

                //返回数据
                responseMessage.message = new { total = cnt, rows = lastone };
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

        #region 获取某指标计量数据的月平均值 
        /// <summary>
        /// 获取某指标计量数据的月平均值
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Get_monthavgIndesx()
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
                string tm = DictionaryPro.GetDicValue(dcParams, "tm");
                tb_MonthIndex_DAL dal = new tb_MonthIndex_DAL();
                DataTable lastone = dal.Get_monthavgIndesx(tm, stcd);

                //返回数据
                responseMessage.message = new { total = cnt, rows = lastone };
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

        #region 获取某指标计量数据的天平均值 
        /// <summary>
        /// 获取某指标计量数据的天平均值
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Get_dayavgIndesx()
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
                string tm = DictionaryPro.GetDicValue(dcParams, "tm");
                string month = DictionaryPro.GetDicValue(dcParams, "month");
                tb_DayIndex_DAL dal = new tb_DayIndex_DAL();
                DataTable lastone = dal.Get_dayavgIndesx(month,tm, stcd);

                //返回数据
                responseMessage.message = new { total = cnt, rows = lastone };
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

        #region 获取某指标计量数据的天平均值 
        /// <summary>
        /// 获取某指标计量数据的天平均值
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Get_hourfavgIndesx()
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
                string tm = DictionaryPro.GetDicValue(dcParams, "tm");
                string month = DictionaryPro.GetDicValue(dcParams, "month");
                string day = DictionaryPro.GetDicValue(dcParams, "day");
                tb_DayIndex_DAL dal = new tb_DayIndex_DAL();
                DataTable lastone = dal.Get_houravgIndesx(day,month, tm, stcd);

                //返回数据
                responseMessage.message = new { total = cnt, rows = lastone };
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

        #region 获取年指标对比 
        /// <summary>
        /// 获取年指标对比
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Get_tb_YearcompareIndex()
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
                string tm = DictionaryPro.GetDicValue(dcParams, "tm");
                //DAL
                tb_YearIndex_DAL dal = new tb_YearIndex_DAL();
                DataTable list = dal.Get_tb_YearcompareIndex(tm);
                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add(new DataColumn("TimeStamp", typeof(DateTime)));
                MyDataTable.Columns.Add(new DataColumn("AllQs", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQc", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQhp", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQsh", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQuse", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQu", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllEc", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllEhp", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllEsys", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("Allc", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllMv", typeof(float)));
                //构造行list.Rows.Count年数据只有一条
                DataRow dr;
                for (int i = 0; i < list.Rows.Count-1; i++)
                {
                    dr = MyDataTable.NewRow();
                    dr["TimeStamp"] = Convert.ToDateTime(list.Rows[i][0]);
                    dr["AllQs"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][1]) - Convert.ToDouble(list.Rows[i+1][1]));
                    dr["AllQc"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][2]) - Convert.ToDouble(list.Rows[i + 1][2]));
                    dr["AllQhp"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][3]) - Convert.ToDouble(list.Rows[i + 1][3]));
                    dr["AllQsh"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][4]) - Convert.ToDouble(list.Rows[i + 1][4]));
                    dr["AllQuse"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][5]) - Convert.ToDouble(list.Rows[i + 1][5]));
                    dr["AllQu"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][6]) - Convert.ToDouble(list.Rows[i + 1][6]));
                    dr["AllEc"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][7]) - Convert.ToDouble(list.Rows[i + 1][7]));
                    dr["AllEhp"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][8]) - Convert.ToDouble(list.Rows[i + 1][8]));
                    dr["AllEsys"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][9]) - Convert.ToDouble(list.Rows[i + 1][9]));
                    dr["Allc"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][10]) - Convert.ToDouble(list.Rows[i + 1][10]));
                    dr["AllMv"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][11]) - Convert.ToDouble(list.Rows[i + 1][11]));




                    MyDataTable.Rows.Add(dr);
                }

                //返回数据
                responseMessage.message = new { total = cnt, rows = MyDataTable };
                result.Content = new StringContent(JsonHelper.Object2Json(responseMessage), Encoding.GetEncoding("utf-8"), "application/json");
            }
            catch (Exception ex)
            {
                //返回错误信息
                responseMessage.status = -1;
                responseMessage.message = ex.Message;

            }

            return result;
        }
        #endregion

        #region 获取月指标对比 
        /// <summary>
        /// 获取年指标对比
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Get_tb_monthcompareIndex()
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
                string tm = DictionaryPro.GetDicValue(dcParams, "tm");
                //DAL
                tb_YearIndex_DAL dal = new tb_YearIndex_DAL();
                DataTable list = dal.Get_tb_monthcompareIndex(tm);
                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add(new DataColumn("TimeStamp", typeof(DateTime)));
                MyDataTable.Columns.Add(new DataColumn("AllQs", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQc", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQhp", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQsh", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQuse", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQu", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllEc", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllEhp", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllEsys", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("Allc", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllMv", typeof(float)));
                //构造行list.Rows.Count年数据只有一条
                DataRow dr;
                for (int i = 0; i < list.Rows.Count - 1; i++)
                {
                    dr = MyDataTable.NewRow();
                    dr["TimeStamp"] = Convert.ToDateTime(list.Rows[i][0]);
                    dr["AllQs"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][1]) - Convert.ToDouble(list.Rows[i + 1][1]));
                    dr["AllQc"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][2]) - Convert.ToDouble(list.Rows[i + 1][2]));
                    dr["AllQhp"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][3]) - Convert.ToDouble(list.Rows[i + 1][3]));
                    dr["AllQsh"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][4]) - Convert.ToDouble(list.Rows[i + 1][4]));
                    dr["AllQuse"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][5]) - Convert.ToDouble(list.Rows[i + 1][5]));
                    dr["AllQu"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][6]) - Convert.ToDouble(list.Rows[i + 1][6]));
                    dr["AllEc"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][7]) - Convert.ToDouble(list.Rows[i + 1][7]));
                    dr["AllEhp"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][8]) - Convert.ToDouble(list.Rows[i + 1][8]));
                    dr["AllEsys"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][9]) - Convert.ToDouble(list.Rows[i + 1][9]));
                    dr["Allc"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][10]) - Convert.ToDouble(list.Rows[i + 1][10]));
                    dr["AllMv"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][11]) - Convert.ToDouble(list.Rows[i + 1][11]));




                    MyDataTable.Rows.Add(dr);
                }

                //返回数据
                responseMessage.message = new { total = cnt, rows = MyDataTable };
                result.Content = new StringContent(JsonHelper.Object2Json(responseMessage), Encoding.GetEncoding("utf-8"), "application/json");
            }
            catch (Exception ex)
            {
                //返回错误信息
                responseMessage.status = -1;
                responseMessage.message = ex.Message;

            }

            return result;
        }
        #endregion

        #region 获取天指标对比 
        /// <summary>
        /// 获取年指标对比
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Get_tb_daycompareIndex()
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
                string tm = DictionaryPro.GetDicValue(dcParams, "tm");
                //DAL
                tb_YearIndex_DAL dal = new tb_YearIndex_DAL();
                DataTable list = dal.Get_tb_daycompareIndex(tm);
                DataTable MyDataTable = new DataTable();
                MyDataTable.Columns.Add(new DataColumn("TimeStamp", typeof(DateTime)));
                MyDataTable.Columns.Add(new DataColumn("AllQs", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQc", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQhp", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQsh", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQuse", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllQu", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllEc", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllEhp", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllEsys", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("Allc", typeof(float)));
                MyDataTable.Columns.Add(new DataColumn("AllMv", typeof(float)));
                //构造行list.Rows.Count年数据只有一条
                DataRow dr;
                for (int i = 0; i < list.Rows.Count - 1; i++)
                {
                    dr = MyDataTable.NewRow();
                    dr["TimeStamp"] = Convert.ToDateTime(list.Rows[i][0]);
                    dr["AllQs"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][1]) - Convert.ToDouble(list.Rows[i + 1][1]));
                    dr["AllQc"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][2]) - Convert.ToDouble(list.Rows[i + 1][2]));
                    dr["AllQhp"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][3]) - Convert.ToDouble(list.Rows[i + 1][3]));
                    dr["AllQsh"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][4]) - Convert.ToDouble(list.Rows[i + 1][4]));
                    dr["AllQuse"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][5]) - Convert.ToDouble(list.Rows[i + 1][5]));
                    dr["AllQu"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][6]) - Convert.ToDouble(list.Rows[i + 1][6]));
                    dr["AllEc"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][7]) - Convert.ToDouble(list.Rows[i + 1][7]));
                    dr["AllEhp"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][8]) - Convert.ToDouble(list.Rows[i + 1][8]));
                    dr["AllEsys"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][9]) - Convert.ToDouble(list.Rows[i + 1][9]));
                    dr["Allc"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][10]) - Convert.ToDouble(list.Rows[i + 1][10]));
                    dr["AllMv"] = Convert.ToDouble(Convert.ToDouble(list.Rows[i][11]) - Convert.ToDouble(list.Rows[i + 1][11]));




                    MyDataTable.Rows.Add(dr);
                }

                //返回数据
                responseMessage.message = new { total = cnt, rows = MyDataTable };
                result.Content = new StringContent(JsonHelper.Object2Json(responseMessage), Encoding.GetEncoding("utf-8"), "application/json");
            }
            catch (Exception ex)
            {
                //返回错误信息
                responseMessage.status = -1;
                responseMessage.message = ex.Message;

            }

            return result;
        }
        #endregion
    }
}
