/***************************************************
 **类 名 称:ProjectDetailInfo_DAL
 **命名空间:SolarhotwaterprojectDAL.Project
 **创建时间:2018-04-18 14:15:58
 **作    者:李沈杰
 **描    述：工程信息页专用
 **修改时间:2018-07-12
 **修 改 人:张超
 * 修改内容：修改 系统采集器_DAL 为系统采集器实时_DAL
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
   public class 系统采集器实时_DAL : BaseDAL<系统采集器实时>
    {
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：获取项目最新一条数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetLastDate()
        {

            var data = DB_Conn.Context.From<系统采集器实时>();

            Where<系统采集器实时> where = new Where<系统采集器实时>();
            data = data.Where(where).OrderBy(系统采集器实时._.TimeStamp.Desc).Top(1);
            DataTable tb = data.ToDataTable();
            return tb;
        }

        /// <summary>
        /// 描述：获取flash 相关最新一条数据
        /// 创建人：张超
        /// 创建日期：2018.7.16
        /// </summary>
        /// <returns></returns>
        public DataTable GetFlashData()
        {
            var data = DB_Conn.Context.From<系统采集器实时>();
           
            Where<系统采集器实时> where = new Where<系统采集器实时>();
            data = data.Select(d => new { d.压缩机排气温度, d.冷凝器出口温度, d.蒸发器出口温度,d.蒸发器入口温度,d.冰块温度,d.水箱上层水温,d.供冷循环中回水温度,d.水箱下层水温,d.供冷循环中供水温度,d.风机盘管出风口温度,d.房间温度,d.环境温度,d.压缩机排气压力,d.蒸发器出口压力,d.供冷冷水质量流量 })
                .Where(where)
                .OrderBy(系统采集器实时._.TimeStamp.Desc)
                .Top(1);
            DataTable tb = data.ToDataTable();
            return tb;

        }

        /// <summary>
        /// 描述：获取最后一条 电能消耗数据
        /// </summary>
        /// 创建人：张超
        /// 创建日期：2018-7-17
        /// 
        /// <returns></returns>
        public DataTable GetLastQoeIndex()
        {
            var data = DB_Conn.Context.From<系统采集器实时>();

            Where<系统采集器实时> where = new Where<系统采集器实时>();
            data = data.Select(d => new { d.总电能, d.压缩机电能, d.光伏输出电能 })
                .Where(where)
                .OrderBy(系统采集器实时._.TimeStamp.Desc)
                .Top(1);
            DataTable tb = data.ToDataTable();
            return tb;
        }
        
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：获取项目最新一条数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetDateBytime(DateTime starttm, DateTime endtm, int pageNumber, int pageSize, out int cnt)
        {

            var data = DB_Conn.Context.From<系统采集器实时>();
            Where<系统采集器实时> where = new Where<系统采集器实时>();
            where.And(t => t.TimeStamp >= starttm);
            where.And(t => t.TimeStamp <= endtm);
            data = data.Where(where).OrderBy(系统采集器实时._.TimeStamp.Asc);
            cnt = Convert.ToInt32(data.Count());
            data = data.Where(where).Page(pageSize, pageNumber).OrderBy(系统采集器实时._.TimeStamp.Asc);
            DataTable tb = data.ToDataTable();
            return tb;
        }
    }
}

