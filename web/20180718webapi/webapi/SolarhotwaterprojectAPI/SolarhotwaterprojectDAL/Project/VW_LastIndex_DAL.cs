/***************************************************
 **类 名 称:VW_LastIndex_DAL
 **命名空间:SolarhotwaterprojectDAL.Project
 **创建时间:2018-04-20 14:15:58
 **作    者:李沈杰
 **描    述：工程信息页专用(获取当前最新一条系统计量信息)
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
    public class VW_LastIndex_DAL
    {
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：获取项目最新一计量数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetLastIndex()
        {

            var data = DB_Conn.Context.From<VW_LastIndex>();
            Where<VW_LastIndex> where = new Where<VW_LastIndex>();
            data = data.Where(where).OrderBy(VW_LastIndex._.TimeStamp.Desc).Top(1);
            DataTable tb = data.ToDataTable();
            return tb;
        }
    }
}
