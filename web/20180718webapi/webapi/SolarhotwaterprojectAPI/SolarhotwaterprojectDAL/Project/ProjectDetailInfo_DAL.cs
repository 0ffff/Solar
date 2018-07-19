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
    public class ProjectDetailInfo_DAL : BaseDAL<ProjectDetailInfo>
    {
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：获取项目详细信息
        /// </summary>
        /// <param name="ProjectCode">项目编号</param>
        /// <returns></returns>
        public DataTable GetProjectDetailInfo(string ProjectCode)
        {
            var data = DB_Conn1.Context.From<ProjectDetailInfo>();
            Where<ProjectDetailInfo> where = new Where<ProjectDetailInfo>();
            where.And(t => t.ProjectCode == ProjectCode);
            data = data.Where(where);
            DataTable tb = data.ToDataTable();
            return tb;
        }

      



    }
}
