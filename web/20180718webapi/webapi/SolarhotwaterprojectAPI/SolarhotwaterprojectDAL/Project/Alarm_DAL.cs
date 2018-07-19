/***************************************************
 **类 名 称:Alarm_DAL
 **命名空间:SolarhotwaterprojectDAL.Project
 **创建时间:2018-04-18 14:15:58
 **作    者:李沈杰
 **描    述：报警页专用
 **修改时间:
 **修 改 人:
***********************************************/
using System.Data;
using System;
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
    public class tb_Alarm_DAL
    {
        #region 获取报警数据
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：(获取报警数据)
        /// </summary>
        /// <returns></returns>
        public string Get_tb_Alarm(DateTime starttm, DateTime endtm,int pageNumber, int pageSize,out int cnt)
        {
            var data = DB_Conn1.Context.From<tb_Alarm>();
            Where<tb_Alarm> where = new Where<tb_Alarm>();
            where.And(t => t.TimeStamp >= starttm);
            where.And(t => t.TimeStamp <= endtm);
            data = data.Where(where).OrderBy(tb_Alarm._.TimeStamp.Asc);
            cnt = Convert.ToInt32(data.Count());
            data = data.Where(where).Page(pageSize, pageNumber).OrderBy(tb_Alarm._.TimeStamp.Asc);
            DataTable tb = data.ToDataTable();
            return JsonConvert.SerializeObject(tb); 
        }
        #endregion
    }
}
