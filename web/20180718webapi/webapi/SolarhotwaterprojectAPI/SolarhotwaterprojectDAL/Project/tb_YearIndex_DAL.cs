/***************************************************
 **类 名 称:tb_MonthIndex_DAL
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
    public class tb_YearIndex_DAL
    {
        #region 获取去年最后一条的计量数据
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取上月最后一条的计量数据)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_tb_yearlastoneIndex(string tm, string stcd)
        {
            DataTable data = DB_Conn.Context.FromProc("pr_Get_tb_YearlastoneIndex")
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

        #region 获取某指标计量数据的年平均值
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取某指标计量数据的年平均值)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_yearavgIndesx(string tm, string stcd)
        {
            DataTable data = DB_Conn.Context.FromProc("pr_Get_yearavgIndesx")
              .AddInParameter("tm", DbType.String, tm)
              .AddInParameter("stcd", DbType.String, stcd)
              .ToDataTable();
            //DataTable排序

            return data;
        }
        #endregion

        #region 获取年指标对比
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取年指标对比)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_tb_YearcompareIndex(string tm)
        {
            DataTable data = DB_Conn.Context.FromProc("pr_Get_tb_YearcompareIndex")
              .AddInParameter("tm", DbType.String, tm)
              .ToDataTable();
            //DataTable排序

            return data;
        }
        #endregion

        #region 获取月指标对比
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取月指标对比)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_tb_monthcompareIndex(string tm)
        {
            DataTable data = DB_Conn.Context.FromProc("pr_Get_tb_monthcompareIndex")
              .AddInParameter("tm", DbType.String, tm)
              .ToDataTable();
            //DataTable排序

            return data;
        }
        #endregion

        #region 获取天指标对比
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取月指标对比)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_tb_daycompareIndex(string tm)
        {
            DataTable data = DB_Conn.Context.FromProc("pr_Get_tb_daycompareIndex")
              .AddInParameter("tm", DbType.String, tm)
              .ToDataTable();
            //DataTable排序

            return data;
        }
        #endregion
    }
}
