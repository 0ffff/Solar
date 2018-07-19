/***************************************************
 **类 名 称:Query_DAL
 **命名空间:SolarhotwaterprojectDAL.Project
 **创建时间:2018-04-20 14:15:58
 **作    者:李沈杰
 **描    述：计量页专用
 **修改时间:
 **修 改 人:
***********************************************/
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolarhotwaterprojectModel;
using Dos.ORM;
using Newtonsoft.Json;
using DCSoft.DBUtilityGeneric.DAL;
namespace SolarhotwaterprojectDAL.Project
{
     public class Query_DAL
    {
        #region 按时间查询监测指标曲线
        /// <summary>
        /// 查询历史水温曲线
        /// </summary>
        /// <param name="stcd"></param>
        /// <returns></returns>
        public DataTable GetMeasurementIndexBytime(DateTime starttm, DateTime endtm, string stcd)
        {

            DataTable data = DB_Conn.Context.FromProc("Pro_MonitoringindexBytime")
                .AddInParameter("starttm", DbType.DateTime, starttm)
                .AddInParameter("endtm", DbType.DateTime, endtm)
                .AddInParameter("stcd", DbType.String, stcd)
                .ToDataTable();
            //DataTable排序
            DataTable dtCopy = data.Copy();
            DataView dv = data.DefaultView;
            dv.Sort = "TimeStamp";
            dtCopy = dv.ToTable();
            string[] adc = new string[dtCopy.Rows.Count];
            for (int i = 0; i < dtCopy.Rows.Count; i++)
            {
                adc[i] = dtCopy.Rows[i][0].ToString();
            }
            return dtCopy;

        }
        #endregion
    }
}
