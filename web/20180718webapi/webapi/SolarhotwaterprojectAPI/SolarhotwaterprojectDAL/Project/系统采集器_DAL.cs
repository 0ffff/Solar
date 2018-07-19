/***************************************************
 **类 名 称:ProjectDetailInfo_DAL
 **命名空间:SolarhotwaterprojectDAL.Project
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
using System.Text;
using System.Threading.Tasks;
using SolarhotwaterprojectModel;
using Dos.ORM;
using Newtonsoft.Json;
using DCSoft.DBUtilityGeneric.DAL;

namespace SolarhotwaterprojectDAL.Project
{
   public class 系统采集器_DAL : BaseDAL<系统采集器>
    {
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：获取项目最新一条数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetLastDate()
        {

            var data = DB_Conn.Context.From<系统采集器>();
            Where<系统采集器> where = new Where<系统采集器>();
            data = data.Where(where).OrderBy(系统采集器._.TimeStamp.Desc).Top(1);
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

            var data = DB_Conn.Context.From<系统采集器>();
            Where<系统采集器> where = new Where<系统采集器>();
            where.And(t => t.TimeStamp >= starttm);
            where.And(t => t.TimeStamp <= endtm);
            data = data.Where(where).OrderBy(系统采集器._.TimeStamp.Asc);
            cnt = Convert.ToInt32(data.Count());
            data = data.Where(where).Page(pageSize, pageNumber).OrderBy(系统采集器._.TimeStamp.Asc);
            DataTable tb = data.ToDataTable();
            return tb;
        }
    }
}
