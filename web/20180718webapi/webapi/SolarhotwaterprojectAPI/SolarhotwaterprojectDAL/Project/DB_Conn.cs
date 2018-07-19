using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dos.ORM;

namespace SolarhotwaterprojectDAL.Project
{
    public class DB_Conn
    {
        public static readonly DbSession Context = new DbSession("conn");


        static DB_Conn()
        {
            //在控制台输出Sql
            Context.RegisterSqlLogger(delegate (string sql)
            {
                System.Diagnostics.Debug.Write(sql);
            });

        }
    }
}
