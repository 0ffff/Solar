/***************************************************
 **类 名 称:ProjectDetailInfo_DAL
 **命名空间:SolarhotwaterprojectDAL.Project
 **创建时间:2018-04-18 14:15:58
 **作    者:李沈杰
 **描    述：工程信息页专用
 **修改时间:2018-07-12
 **修 改 人:张超
 * 修改内容：修改 系统采集器_DAL 为系统采集器参数_DAL
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
    public class 系统采集器参数_DAL : BaseDAL<系统采集器参数>
    {
        /// <summary>
        /// 创建人：张超
        /// 描    述：获取项目最新一条数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetLastCanShu()
        {

            var data = DB_Conn.Context.From<系统采集器参数>();
            Where<系统采集器参数> where = new Where<系统采集器参数>();
            data = data.Where(where).OrderBy(系统采集器参数._.TimeStamp.Desc).Top(1);
            DataTable tb = data.ToDataTable();
            return tb;
        }

    }
}

