using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http;
using SolarhotwaterprojectModel;

using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;

using System.Xml;
using System.Net;

namespace Solarhotwaterproject.DataServices
{
    /// <summary>
    /// Handler1 的摘要说明
    /// 创建人：李沈杰
    /// 修改人：张超
    /// 修改内容：把丽水项目修改为云师大项目、修改flash下xml参数
    /// 
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
            string type = context.Request["type"];
            string datajson = context.Request["datajson"];//实时数据
            //string datajson1 = context.Request["datajson1"];//计量数据



            switch (type)
            {
                case "Realtimedata":
                    List<系统采集器实时> List1 = JsonConvert.DeserializeObject<List<系统采集器实时>>(datajson);
                    //List<系统采集器实时> Listed= JsonConvert.DeserializeObject<List>
                    string path = HttpContext.Current.Server.MapPath("~/view/8.xml");
                    XmlDocument xdflash = new XmlDocument();
                    xdflash.Load(path);
                    XmlNode rootflash = xdflash.DocumentElement;
                    XmlElement xeTest = (XmlElement)rootflash.SelectSingleNode("test");

                    #region 写入XML

                    //string sun = "";//光照度
                    string temp0 = " ";//压缩机出口温度
                    string temp1 = "";//冷凝器出口温度
                    string temp2 = "";//蒸发器出口温度
                    string temp3 = "";//蒸发器入口温度
                    string temp4 = "";//冰层温度
                    string temp5 = "";//水箱上层温度
                    string temp6 = "";//供冷循环回水温度
                    string temp7 = "";//水箱下层温度
                    string temp8 = "";//供冷循环供水温度
                    string temp9 = "";//风机出口温度
                    string temp10 = "";//房间温度
                    //string temp11 = "";//设定温度
                    //string temp12 = "";//环境温度

                    string yl1 = "";//压缩机出口压力
                    string yl2 = "";//蒸发器出口压力
                    string ll1 = "";//供冷冷水流量
                    string jinshuifa = "1";//进水阀
                    string chushuifa = "0";//出水阀
                    string zhileng = "1";//制冷
                    string yongleng = "1";//用冷  

                    #endregion

                    #region 写入
                    temp0 = List1[0].压缩机排气温度 ;
                    temp1 = List1[0].冷凝器出口温度 ;
                    temp2 = List1[0].蒸发器出口温度 ;
                    temp3 = List1[0].蒸发器入口温度 ;
                    temp4 = List1[0].冰块温度 ;
                    temp5 = List1[0].水箱上层水温 ;
                    temp6 = List1[0].供冷循环中回水温度 ;
                    temp7 = List1[0].水箱下层水温 ;
                    temp8 = List1[0].供冷循环中供水温度 ;
                    temp9 = List1[0].风机盘管出风口温度 ;
                    temp10 = List1[0].房间温度 ;
                    //temp12 = List1[0].环境温度 ;

                    yl1 = List1[0].压缩机排气压力 ;
                    yl2 = List1[0].蒸发器出口压力 ;
                    ll1 = List1[0].供冷冷水质量流量 ;


                    //写入
                    xeTest.SetAttribute("temp0", temp0);
                    xeTest.SetAttribute("temp1", temp1);
                    xeTest.SetAttribute("temp2", temp2);
                    xeTest.SetAttribute("temp3", temp3);
                    xeTest.SetAttribute("tem4", temp4);
                    xeTest.SetAttribute("temp5", temp6);
                    xeTest.SetAttribute("temp7", temp7);
                    xeTest.SetAttribute("temp8", temp8);
                    xeTest.SetAttribute("temp9", temp9);
                    xeTest.SetAttribute("temp10", temp10);
                    //xeTest.SetAttribute("temp12", temp12);
                    xeTest.SetAttribute("yl1", yl1);
                    xeTest.SetAttribute("yl2", yl2);
                    xeTest.SetAttribute("ll1", ll1);
                    xeTest.SetAttribute("jinshuifa", jinshuifa);
                    xeTest.SetAttribute("chushuifa", chushuifa);
                    xeTest.SetAttribute("zhileng", zhileng);
                    xeTest.SetAttribute("yongleng", yongleng);

                    #endregion

                    xdflash.Save(path);//保存xml
                    break;
               
            }
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}