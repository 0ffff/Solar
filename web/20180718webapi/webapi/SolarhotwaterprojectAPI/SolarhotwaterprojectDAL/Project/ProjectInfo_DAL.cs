/***************************************************
 **类 名 称:ProjectInfo_DAL
 **命名空间:SolarhotwaterprojectDAL.Project
 **创建时间:2018-04-18 14:15:58
 **作    者:李沈杰
 **描    述：登录页专用
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
    public class ProjectInfo_DAL : BaseDAL<ProjectInfo>
    {
        #region 获取用户信息
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：获取用户信息
        /// </summary>
        /// <param name="UserID">用户名</param>
        /// <returns></returns>
        public DataTable Getaccount(string UserID)
        {
            var data = DB_Conn1.Context.From<ProjectInfo>();
            Where<ProjectInfo> where = new Where<ProjectInfo>();
            where.And(t => t.UserID == UserID);
            data = data.Where(where);
            DataTable tb = data.ToDataTable();
            return tb;
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 创建人：李沈杰
        /// 描    述：修改密码
        /// </summary>
        /// <param name="UserID">用户名</param>
        /// <returns></returns>
        public bool UpdateProjectInfo(string UserID, string UserPassword)
        {
            ProjectInfo opjson = new ProjectInfo();
            opjson.UserPassword = UserPassword;
            //事务定义执行
            DbTrans trans = DB_Conn1.Context.BeginTransaction();
            try
            {
                DB_Conn1.Context.Update(trans, opjson, ProjectInfo._.UserID == UserID);

                trans.Commit();
                return true;
            }
            catch
            {
                trans.Rollback();
                return false;
            }
           
        }
        #endregion
    }
}
