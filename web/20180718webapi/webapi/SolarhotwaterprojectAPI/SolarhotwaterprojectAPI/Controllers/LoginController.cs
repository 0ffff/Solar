/***************************************************
 **类 名 称:LoginController
 **命名空间:SolarhotwaterprojectAPI.Controllers
 **创建时间:2018-04-18 14:15:58
 **作    者:李沈杰
 **描    述：登录页专用
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
    public class LoginController : ApiController
    {
        #region 登录验证服务
        /// <summary>
        /// 登录验证服务
        /// </summary>
        /// <remarks>
        /// 创 建 人：李沈杰
        /// 创建时间：2018-04-18 14:48:11
        /// 修 订 人：李沈杰
        /// 修订时间：
        /// </remarks>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Login()
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
                string UserID = DictionaryPro.GetDicValue(dcParams, "UserID");//用户名
                string UserPassword = DictionaryPro.GetDicValue(dcParams, "UserPassword");//密码
                                                                                          //DAL
                DataTable account = new DataTable();
                ProjectInfo_DAL dal = new ProjectInfo_DAL();
                //验证密码
                account = dal.Getaccount(UserID);
                if (account != null)
                {
                    //验证通过标志

                    //用户名
                    string username = "";

                    if (account.Rows[0][7].Equals(UserPassword))
                    {
                        flag = true;
                    }

                }

                responseMessage.message = new { rows = flag };
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

        #region 获取用户名以及项目名称等信息
        /// <summary>
        /// 获取用户名以及项目名称等信息
        /// </summary>
        /// <remarks>
        /// 创 建 人：李沈杰
        /// 创建时间：2018-04-18 13:48:11
        /// 修 订 人：李沈杰
        /// 修订时间：
        /// </remarks>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GettrueName()
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
                string UserID = DictionaryPro.GetDicValue(dcParams, "UserID");//用户名
                DataTable account = new DataTable();
                ProjectInfo_DAL dal = new ProjectInfo_DAL();
                account = dal.Getaccount(UserID);

                responseMessage.message = new { rows = account };
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

        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <remarks>
        /// 创 建 人：李沈杰
        /// 创建时间：2018-04-18 14:48:11
        /// 修 订 人：李沈杰
        /// 修订时间：
        /// </remarks>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage UpdateProjectInfo()
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
                string UserID = DictionaryPro.GetDicValue(dcParams, "UserID");//用户名
                string OldUserPassword = DictionaryPro.GetDicValue(dcParams, "OldUserPassword");//密码
                string newUserPassword = DictionaryPro.GetDicValue(dcParams, "newUserPassword");//密码
                                                                                        
                DataTable account = new DataTable();
                ProjectInfo_DAL dal = new ProjectInfo_DAL();
                //验证密码
                account = dal.Getaccount(UserID);
                if (account != null)
                {
                    //验证通过标志

                    //用户名
                    string username = "";

                    if (account.Rows[0][7].Equals(OldUserPassword))
                    {

                        flag = dal.UpdateProjectInfo(UserID, newUserPassword);
                    }

                }

                responseMessage.message = new { rows = flag };
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
    }
}
