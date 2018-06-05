using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace 光伏制冷
{
    class DataAccess
    {
        string connstring = "";
        public SqlConnection conn;
        //自锁对象
        private static readonly object Exelockobj = new object();
        private static readonly object DataSetlockobj = new object();
        private static readonly object DataReaderlockobj = new object();
        //集成一个总锁
        private static readonly object OnlyLockObj = new object();

        public DataAccess()
        {
            //connstring = @"server=121.196.244.172,45000;user id=mylink;password=123456;connection timeout=5;database=EnergyTesting";//默认数据库为EnergyTesting
            //connstring = @"server=NIKO-PC\SQLEXPRESS;Integrated Security=true;database=EnergyTesting1";//默认数据库为EnergyTesting
            //connstring = @"server=iZ23ou9eiggZ;user id=mylink;password=123456;connection timeout=5;database=EnergyTesting";//默认数据库为EnergyTesting
            //connstring = @"server =101.37.149.108,36000;user id=sa;password=123456;connection timeout=5;database=EnergyTesting1";//默认数据库为EnergyTesting1
            connstring = @"server =.;user id=sa;password=123456;connection timeout=5;database=EnergyTesting1";//默认数据库为EnergyTesting1

            conn = new SqlConnection(connstring);
        }

        /// <summary>
        /// 对数据库的操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="database">数据库的名称</param>
        /// <returns>操作是否成功</returns>
        /// 若是默认数据库则直接进行操作若不是则先判断数据库是否存在
        public int ExeSQL(string sql, string database)
        {
            //lock (Exelockobj)
            lock (OnlyLockObj)
            {
                if (database == GlobalInfo.DefaultDatabase)//对默认数据库操作
                {
                    SqlCommand sqlcommand = new SqlCommand(sql, conn);
                    try
                    {
                        conn.Open();
                        sqlcommand.ExecuteNonQuery();
                        return 0;
                    }
                    catch
                    {
                        return -1;
                    }
                    finally
                    {
                        sqlcommand.Dispose();
                        conn.Close();
                    }
                }
                //对非默认数据库操作
                else
                {   //添加判断该数据库是否已经建立 若没有建立该数据库会产生错误 在默认数据库下新建一张表格存储所有数据库的名称来判断是否已经建立该数据库
                    //if (IsExist(database))
                    //{
                    SqlCommand sqlcommand = new SqlCommand(sql, conn);
                    try
                    {
                        conn.Open();
                        conn.ChangeDatabase(database);//改变conn口关联的数据库
                        sqlcommand.ExecuteNonQuery();
                        return 0;
                    }
                    catch
                    { return -1; }
                    finally
                    {
                        sqlcommand.Dispose();
                        conn.Close();
                    }
                    //}
                    //else
                    //{ return -1; }
                }
            }
        }

        /// <summary>
        /// 对数据库的操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="database">数据库的名称</param>
        /// <returns>操作是否成功</returns>
        /// 若是默认数据库则直接进行操作若不是则先判断数据库是否存在
        public SqlDataReader GetDataReader(string sql, string database)
        {
            //lock (DataReaderlockobj)
            lock (OnlyLockObj)
            {
                if (database == GlobalInfo.DefaultDatabase)//对默认数据库操作
                {
                    SqlCommand sqlcommand = new SqlCommand(sql, conn);
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = sqlcommand.ExecuteReader();
                        return dr;
                    }
                    catch
                    { return null; }
                }
                else
                { 
                    //添加判断该数据库是否已经建立 若没有建立该数据库会产生错误 在默认数据库下新建一张表格存储所有数据库的名称
                    SqlCommand sqlcommand = new SqlCommand(sql, conn);
                    try
                    {
                        conn.Open();
                        conn.ChangeDatabase(database);
                        SqlDataReader dr = sqlcommand.ExecuteReader();
                        return dr;
                    }
                    catch
                    { return null; }
                }
            }
        }

        /// <summary>
        /// 对数据库的操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="database">数据库的名称</param>
        /// <returns>操作是否成功</returns>
        /// 若是默认数据库则直接进行操作若不是则先判断数据库是否存在
        public DataSet GetDataSet(string sql, string database)
        {
            //lock (DataSetlockobj)
            lock (OnlyLockObj)
            {
                SqlDataAdapter SQLda = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                if (database == GlobalInfo.DefaultDatabase)//对默认数据库操作
                {
                    try
                    {
                        conn.Open();
                        SQLda.Fill(ds, "NewTable");
                        return ds;
                    }
                    catch
                    {
                        return null;
                    }
                    finally
                    {
                        SQLda.Dispose();
                        conn.Close();
                    }
                }
                else
                {
                    try
                    {
                        conn.Open();
                        conn.ChangeDatabase(database);
                        SQLda.Fill(ds, "NewTable");
                        return ds;
                    }
                    catch
                    {
                        return null;
                    }
                    finally
                    {
                        SQLda.Dispose();
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 对数据库的操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="database">数据库的名称</param>
        /// <returns>操作是否成功</returns>
        /// 若是默认数据库则直接进行操作若不是则先判断数据库是否存在
        public DataTable GetDataTable(string sql, string database)
        {
            //lock (DataSetlockobj)
            lock (OnlyLockObj)
            {
                SqlDataAdapter SQLda = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                if (database == GlobalInfo.DefaultDatabase)//对默认数据库操作
                {
                    try
                    {
                        conn.Open();
                        SQLda.Fill(dt);
                        return dt;
                    }
                    catch
                    {
                        SQLda.Dispose();
                        conn.Close();
                        return null;
                    }
                    finally
                    {
                        dt.Dispose();//&&
                        SQLda.Dispose();
                        conn.Close();
                    }
                }
                else
                {
                    try
                    {
                        conn.Open();
                        conn.ChangeDatabase(database);
                        SQLda.Fill(dt);
                        return dt;
                    }
                    catch
                    {
                        SQLda.Dispose();
                        conn.Close();
                        return null;
                    }
                    finally
                    {
                        dt.Dispose();//&&
                        SQLda.Dispose();
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 获得是否存在该采集器或采集点（在默认数据库下执行）
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns>true存在  false不存在</returns>
        public bool IsExistColletorOrPoint(string SQL)
        {
            lock (OnlyLockObj)//&&
            {
                SqlCommand sqlcommand = new SqlCommand(SQL, conn);
                try
                {
                    conn.Open();
                    if (sqlcommand.ExecuteScalar().ToString() != "0")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    conn.Close();
                    sqlcommand.Dispose();
                    return false;
                }
                finally
                {
                    conn.Close();
                    sqlcommand.Dispose();
                }
            }
        }

        /// <summary>
        /// 返回单个值
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns>  结果集中第一行的第一列 如果结果集为空，则为空引用</returns>
        public object ReturnSingleData(string SQL)
        {
            lock (OnlyLockObj)//&&
            {
                SqlCommand sqlcommand = new SqlCommand(SQL, conn);
                try
                {
                    conn.Open();
                    object obj = sqlcommand.ExecuteScalar();
                    return obj;
                }
                catch
                { return null; }
                finally
                {
                    conn.Close();
                    sqlcommand.Dispose();
                }
           }
        }

        //创建数据库函数不用判断该数据库是否存在
        public int CreateDataBase(string databaseName)
        {
            lock (OnlyLockObj)//&&
            {
                string sql = @"Create database " + databaseName;
                SqlCommand sqlcommand = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    sqlcommand.ExecuteNonQuery();
                    return 0;
                }
                catch
                { return -1; }
                finally
                {
                    sqlcommand.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 实时数据插入数据库时判断是否存在该表格（历史数据表和统计量表） 因为这两张表格有日期限制  若不存在则说明到了下一个月了 则新建一张表格插入内容 
        /// </summary>
        /// <param name="tableName">表格名称</param>
        /// <param name="dataBase">数据库名称</param>
        /// <returns>true 存在 false 不存在</returns>
        public bool IsExistTable(string tableName, string dataBase)
        {
            lock (OnlyLockObj)//&&
            {
                string SQL = "select count(*) from sysobjects where name='" + tableName + "'";
                SqlCommand sqlcommand = new SqlCommand(SQL, conn);
                try
                {
                    conn.Open();
                    conn.ChangeDatabase(dataBase);
                    if (sqlcommand.ExecuteScalar().ToString() != "0")
                    {
                        return true;
                    }
                    else
                    { return false; }
                }
                catch
                {
                    sqlcommand.Dispose();
                    conn.Close();
                    return true;//2017.01.21-清河-先让分月表有数据，新月份时手动加分月表
                    //return false;
                }
                finally
                {
                    sqlcommand.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 实时数据统计数据 插入数据库前要先判断该条数据在表格中是否存在
        /// </summary>
        /// <param name="tableName">表格名称</param>
        /// <param name="dataBase">数据库名称</param>
        /// <param name="para">时间条件</param>
        /// <returns></returns>
        public bool IsExistData(string tableName, string dataBase, string para)
        {
            lock (OnlyLockObj)//&&
            {
                string SQL = "select count(*) from " + tableName + " where TimeStamp='" + para + "'";
                SqlCommand sqlcommand = new SqlCommand(SQL, conn);
                try
                {
                    conn.Open();
                    conn.ChangeDatabase(dataBase);
                    if (sqlcommand.ExecuteScalar().ToString() != "0")
                    {
                        return true;
                    }
                    else
                    { return false; }
                }
                catch
                { return false; }
                finally
                {
                    sqlcommand.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 判断是否存在此项目
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        public bool IsExistProject(string AreaName,string ProjectName)
        {
            lock (OnlyLockObj)//&&
            {
                string SQL = "select count(*) from ProjectInfo where AreaName ='" + AreaName + "' and ProjectName ='" + ProjectName + "'";
                SqlCommand sqlcommand = new SqlCommand(SQL, conn);
                try
                {
                    conn.Open();
                    conn.ChangeDatabase(GlobalInfo.DefaultDatabase);
                    if (sqlcommand.ExecuteScalar().ToString() != "0")
                    {
                        return true;
                    }
                    else
                    { return false; }
                }
                catch
                { return false; }
                finally
                {
                    sqlcommand.Dispose();
                    conn.Close();
                }
            }
        }


        ///// <summary>
        ///// 返回某数据表格的数据行数
        ///// </summary>
        ///// <param name="tableName"></param>
        ///// <param name="dataBase"></param>
        ///// <returns>-1故障</returns>
        public int GetCount(string tableName, string dataBase)
        {
            lock (OnlyLockObj)//&&
            {
                string sql = "select count(*) from " + tableName;
                SqlCommand sqlcommand = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    conn.ChangeDatabase(dataBase);
                    return (int)sqlcommand.ExecuteScalar();
                }
                catch
                { return -1; }
                finally
                {
                    sqlcommand.Dispose();
                    conn.Close();
                }
            }
        }
    }
}
