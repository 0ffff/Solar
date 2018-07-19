/***************************************************
 **类 名 称:tb_DayIndex_DAL
 **命名空间:SolarhotwaterprojectDAL.Project
 **创建时间:2018-04-20 14:15:58
 **作    者:李沈杰
 **描    述：计量页专用(获取每天24小时的计量数据)
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
    public class tb_DayIndex_DAL
    {
        #region 获取每月所有天的计量数据
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取每月所有天的计量数据)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_tb_DayIndex(string tm,string stcd)
        {
            DataTable data = DB_Conn.Context.FromProc("pro_getlndexByday")
               .AddInParameter("tm", DbType.String, tm)
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

        #region 获取每天24小时的计量数据
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取每天24小时的计量数据)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_tb_HourIndex(string tm, string stcd)
        {
            DataTable data = DB_Conn.Context.FromProc("pro_getlndexByhour")
                .AddInParameter("tm", DbType.String, tm)
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

        #region 获取每年所有月的计量数据
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取每年所有月的计量数据)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_tb_MonthIndex(DateTime starttm, DateTime endtm)
        {
            var data = DB_Conn.Context.From<tb_MonthIndex>();
            Where<tb_MonthIndex> where = new Where<tb_MonthIndex>();
            where.And(t => t.TimeStamp >= starttm);
            where.And(t => t.TimeStamp <= endtm);
            data = data.Where(where).OrderBy(tb_MonthIndex._.TimeStamp.Asc);
            DataTable tb = data.ToDataTable();
            return tb;
        }
        #endregion

        #region 获取每年的计量数据
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取每年所有月的计量数据)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_tb_YearIndex(string stcd,DateTime starttm, DateTime endtm)
        {
            var data = DB_Conn.Context.From<tb_YearIndex>();
            Where<tb_YearIndex> where = new Where<tb_YearIndex>();
            where.And(t => t.TimeStamp >= starttm);
            where.And(t => t.TimeStamp <= endtm);
            data = data.Where(where).OrderBy(tb_YearIndex._.TimeStamp.Asc);
            DataTable tb = data.ToDataTable();
            return tb;
        }
        #endregion

        #region 按年份查询计量数据
        /// <summary>
        /// 查询温度和开关量当天曲线
        /// </summary>
        /// <param name="stcd"></param>
        /// <returns></returns>
        public DataTable GetIndexByyear( string stcd, string time)
        {

            DataTable data = DB_Conn.Context.FromProc("pr_getIndexByyear")
                .AddInParameter("time", DbType.String, time)
                .AddInParameter("stcd", DbType.String, stcd)
                .ToDataTable();
           
            return data;

        }
        #endregion
        #region 按月份查询计量数据
        /// <summary>
        /// 查询温度和开关量当天曲线
        /// </summary>
        /// <param name="stcd"></param>
        /// <returns></returns>
        public DataTable GetIndexBymonth(string stcd, string time)
        {

            DataTable data = DB_Conn.Context.FromProc("pr_getIndexBymonth")
                .AddInParameter("time", DbType.String, time)
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


        #region 获取查询年份前一条数据的计量数据
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取查询年份前一条数据的计量数据)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_tb_YearIndex_beforeone( DateTime tm)
        {
            var data = DB_Conn.Context.From<tb_YearIndex>();
            Where<tb_YearIndex> where = new Where<tb_YearIndex>();
            where.And(t => t.TimeStamp <= tm);
            data = data.Where(where).OrderBy(tb_YearIndex._.TimeStamp.Desc).Top(1);
            DataTable tb = data.ToDataTable();
            return tb;
        }
        #endregion

        #region 获取查询月份---去年最后一条前一条数据的计量数据
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(去年最后一条前一条数据的计量数据)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_tb_MonthIndex_beforeone(string stcd, DateTime tm)
        {
            var data = DB_Conn.Context.From<tb_MonthIndex>();
            Where<tb_MonthIndex> where = new Where<tb_MonthIndex>();
            where.And(t => t.TimeStamp <= tm);
            data = data.Where(where).OrderBy(tb_MonthIndex._.TimeStamp.Desc).Top(1);
            DataTable tb = data.ToDataTable();
            return tb;
        }
        #endregion
        #region 获取查询当天前一条数据的计量数据
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取查询当天前一条数据的计量数据)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_tb_HourIndex_beforeone(string stcd, DateTime tm)
        {
            var data = DB_Conn.Context.From<tb_HourIndex>();
            Where<tb_HourIndex> where = new Where<tb_HourIndex>();
            where.And(t => t.TimeStamp <= tm);
            data = data.Where(where).OrderBy(tb_HourIndex._.TimeStamp.Desc).Top(1);
            DataTable tb = data.ToDataTable();
            return tb;
        }
        #endregion

        #region 获取昨天最后一条的计量数据
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取昨天最后一条的计量数据)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_tb_DaylastoneIndex(string tm, string stcd)
        {
            DataTable data = DB_Conn.Context.FromProc("pr_Get_tb_DaylastoneIndex")
              .AddInParameter("tm", DbType.String, tm)
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

        #region 获取昨天计量数据的累积量
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取上月计量数据的累积量)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_lastdayhsummaryindex(string stcd)
        {
            DataTable data = DB_Conn.Context.FromProc("pr_Get_lastdayhsummaryindex")
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

        #region 获取计量数据的天平均值+
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取计量数据的天平均值)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_dayavgIndesx(string month,string tm, string stcd)
        {
            DataTable data = DB_Conn.Context.FromProc("pr_Get_dayavgIndesx")
              .AddInParameter("stcd", DbType.String, stcd)
               .AddInParameter("tm", DbType.String, tm)
               .AddInParameter("month", DbType.String, month)
              .ToDataTable();
            //DataTable排序
            DataTable dtCopy = data.Copy();
            DataView dv = data.DefaultView;
            dtCopy = dv.ToTable();
            string[] adc = new string[dtCopy.Rows.Count];
            for (int i = 0; i < dtCopy.Rows.Count; i++)
            {
                adc[i] = dtCopy.Rows[i][0].ToString();
            }
            return dtCopy;
        }
        #endregion

        #region 获取计量数据的每小时平均值
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取计量数据的每小时平均值)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_houravgIndesx(string day,string month, string tm, string stcd)
        {
            DataTable data = DB_Conn.Context.FromProc("pr_Get_houravgIndesx")
              .AddInParameter("stcd", DbType.String, stcd)
               .AddInParameter("tm", DbType.String, tm)
               .AddInParameter("month", DbType.String, month)
               .AddInParameter("day", DbType.String, day)
              .ToDataTable();
            //DataTable排序
            DataTable dtCopy = data.Copy();
            DataView dv = data.DefaultView;
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
