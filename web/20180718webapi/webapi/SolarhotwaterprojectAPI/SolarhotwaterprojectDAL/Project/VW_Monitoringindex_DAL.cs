/***************************************************
 **类 名 称:VW_Monitoringindex_DAL
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
    public class VW_Monitoringindex_DAL
    {
        #region 获取项目监测指标
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：获取项目监测指标
        /// </summary>
        /// <returns></returns>
        public DataTable GetVW_Monitoringindex()
        {

            var data = DB_Conn1.Context.From<VW_Monitoringindex>();
            Where<VW_Monitoringindex> where = new Where<VW_Monitoringindex>();
            data = data.Where(where).OrderBy(VW_Monitoringindex._.CollectPointCode.Asc);
            DataTable tb = data.ToDataTable();
            return tb;
        }
        #endregion
        #region 查询监测指标曲线（没有时间限制，自取前几百条）
        /// <summary>
        /// 查询历史水温曲线
        /// </summary>
        /// <param name="stcd"></param>
        /// <returns></returns>
        public string GetMonitoringcurve(string stcd)
        {

            DataTable data = DB_Conn.Context.FromProc("pro_Monitoringindex")
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
            return JsonConvert.SerializeObject(dtCopy); 

        }
        #endregion
    }
}
