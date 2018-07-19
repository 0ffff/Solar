/***************************************************
 **类 名 称:系统采集器Index_DAL
 **命名空间:SolarhotwaterprojectDAL.Project
 **创建时间:2018-04-20 14:15:58
 **作    者:李沈杰
 **描    述：能耗监测页专用
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
   public class 系统采集器Index_DAL
    {
        #region 获取项目当天节能量
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：获取项目当天节能量
        /// </summary>
        /// <returns></returns>
        public DataTable GetVW_TodayEmissionreduction(DateTime starttm, DateTime endtm)
        {

            var data = DB_Conn.Context.From<VW_TodayEmissionreduction>();
            Where<VW_TodayEmissionreduction> where = new Where<VW_TodayEmissionreduction>();
            where.And(t => t.TimeStamp >= starttm);
            where.And(t => t.TimeStamp <= endtm);
            data = data.Where(where).OrderBy(VW_TodayEmissionreduction._.TimeStamp.Asc);
            DataTable tb = data.ToDataTable();
            return tb;
        }
        #endregion

        #region 获取项目最新的计量数据
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：获取项目最新的计量数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetLastNewIndex()
        {
            var data = DB_Conn.Context.From<系统采集器Index>();
            Where<系统采集器Index> where = new Where<系统采集器Index>();
            data = data.Where(where).OrderBy(系统采集器Index._.TimeStamp.Desc).Top(1);
            DataTable tb = data.ToDataTable();
            return tb;
        }
        #endregion
        #region 获取项目某项指标最新的计量数据
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：获取项目某项指标最新的计量数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetsomeoneLastNewIndex(string stcd)
        {
            DataTable data = DB_Conn.Context.FromProc("pr_GetsomeoneLastNewIndex")
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
