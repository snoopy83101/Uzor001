/*----------------------------------------------------------------
   // Copyright (C) 2010 山东众阳软件公司
   // 版权所有。 
   //
   // 文件名：MSSQLHelper.cs
   // 文件功能描述：微软SQL Server 数据库执行脚本存储过程等执行辅助类MSSQLHelper的代码文件包含内容：
   // 类MSSQLHelper
   //
   // 
   // 创建标识： 李涛 20101130

----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DBTools
{
    /// <summary>
    /// 微软SQL Server 数据库执行脚本存储过程等执行辅助类，用于方便执行数据库查询，更新删除语句
    /// </summary>
    public class MSSQLHelper : IDBHelper
    {

        #region 私有变量和常用操作和属性
        private SqlConnection conn;         //SQL连接
        private SqlTransaction sqlTran;     //事务处理
        private bool isInTran = false;        //标识是否处于事务中
        private string connStr;             //连接字符串
        private bool isConnCloseWhenEveryRun = true; //连接是否在每一次执行相关方法后自动关闭 


        /// <summary>
        /// 获取或者设置连接是否在每一次执行相关方法后自动关闭,
        /// 如果不自动关闭需要手动调用Close方法关闭链接
        /// </summary>
        public bool IsConnCloseWhenEveryRun
        {
            get { return isConnCloseWhenEveryRun; }
            set { this.isConnCloseWhenEveryRun = value; }
        }

        /// <summary>
        /// 获取链接字符串 
        /// </summary>
        public string ConnectionString
        {
            get { return this.connStr; }
        }

        /// <summary>
        /// 获取是否正在事务中
        /// </summary>
        public bool IsOnTransaction
        {
            get
            {
                return this.isInTran;
            }
        }



        public MSSQLHelper()
        {
            this.connStr = System.Configuration.ConfigurationSettings.AppSettings["connectStr"].ToString();
            conn = new SqlConnection(this.connStr);
            this.isConnCloseWhenEveryRun = true;
        }

        /// <summary>
        /// 连接字符串,数字传参
        /// </summary>
        /// <param name="i">1为产品研发信息平台数据库(PDMIS),2为需求管理平台数据库(commpany_new)</param>
        public MSSQLHelper(int i)
        {
            string constr = "connectStr";
            if (i == 1)
            {
                constr = "connectStr";
            }
            else if (i == 2)
            {
                constr = "commpanyStr";

            }


            this.connStr = System.Configuration.ConfigurationSettings.AppSettings[constr].ToString();
            conn = new SqlConnection(this.connStr);
            this.isConnCloseWhenEveryRun = true;
        }
        /// <summary>
        /// 默认构造函数 用于初始化数据库连接，使用该方法初始化时，
        /// 每次执行非事务数据库相关的方法或者结束事物后都会关闭连接
        /// </summary>
        /// <param name="connstring">数据库连接字符串</param>
        public MSSQLHelper(string connstring)
        {
            this.connStr = connstring;
            conn = new SqlConnection(this.connStr);
            this.isConnCloseWhenEveryRun = true;
        }


        /// <summary>
        /// 默认构造函数 用于初始化数据库连接，使用该方法初始化时，
        /// 每次执行非事务数据库相关的方法或者结束事物后都会关闭连接
        /// </summary>
        /// <param name="connstring">数据库连接字符串</param>
        /// <param name="isConnCloseWhenEveryRun"> 获取或者设置连接是否在每一次执行相关方法后自动关闭,
        /// 如果不自动关闭需要手动调用Close方法关闭链接</param>
        public MSSQLHelper(string connstring, bool isConnCloseWhenEveryRun)
        {
            this.connStr = connstring;
            conn = new SqlConnection(this.connStr);
            this.isConnCloseWhenEveryRun = isConnCloseWhenEveryRun;
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void Open()
        {
            if (this.conn.State == ConnectionState.Closed)
            {
                this.conn.Open();
            }
            if (this.conn.State == ConnectionState.Broken)
            {
                this.conn.Close();
                this.conn.Open();
            }
        }


        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {

            if (this.IsOnTransaction)
            {
                throw new Exception("事务中不允许关闭链接，请先关闭事务!");
            }

            if (this.conn.State == ConnectionState.Closed)
            {
                return;
            }
            try
            {
                this.conn.Close();
            }
            catch
            {

            }
        }



        /// <summary>
        /// 开始一个事务
        /// </summary>
        public void BeginTran()
        {
            this.Open();
            if (this.isInTran)
            {
                return;
            }
            this.sqlTran = this.conn.BeginTransaction();
            this.isInTran = true;
        }


        /// <summary>
        /// 提交一个事务
        /// </summary>
        public void CommitTran()
        {
            this.sqlTran.Commit();
            this.isInTran = false;
            if (this.isConnCloseWhenEveryRun)
            {
                this.Close();
            }
        }


        /// <summary>
        /// 回滚当前事务
        /// </summary>
        public void RollbackTran()
        {
            this.sqlTran.Rollback();
            this.isInTran = false;
            if (this.isConnCloseWhenEveryRun)
            {
                this.Close();
            }
        }



        #endregion

        #region 执行语句

        public int ExecSqlReInt(string sqlStr, int CommandTimeout)
        {
            this.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.conn;
                if (this.isInTran)
                {
                    cmd.Transaction = this.sqlTran;
                }
                cmd.CommandText = sqlStr;
                cmd.CommandTimeout = CommandTimeout;
                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                return i;
            }
            catch (Exception ex)
            {
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                throw new Exception(ex.Message + "执行sql时错误：" + sqlStr);
            }

        }


        /// <summary>
        /// 执行sql语句返回受影响的行数
        /// </summary>
        /// <param name="sqlStr">执行的sql语句</param>
        /// <returns>受影响的行数</returns>
        public int ExecSqlReInt(string sqlStr)
        {
            this.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.conn;
                if (this.isInTran)
                {
                    cmd.Transaction = this.sqlTran;
                }
                cmd.CommandText = sqlStr;
                cmd.CommandTimeout = 60;
                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                return i;
            }
            catch (Exception ex)
            {
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                throw new Exception(ex.Message + "执行sql时错误：" + sqlStr);
            }

        }

        /// <summary>
        /// 执行带参数的sql语句返回受影响的行数
        /// </summary>
        /// <param name="sqlStr">执行的sql语句</param>
        /// <param name="para">参数列表</param>
        /// <returns>受影响的行数</returns>
        public int ExecSqlReInt(string sqlStr, IList<IDataParameter> para)
        {
            IDataParameter[] datas = new IDataParameter[para.Count];
            para.CopyTo(datas, 0);
            return this.ExecSqlReInt(sqlStr, datas);
        }



        /// <summary>
        /// 执行带参数的sql语句返回受影响的行数
        /// </summary>
        /// <param name="sqlStr">执行的sql语句</param>
        /// <param name="para">参数列表</param>
        /// <returns>受影响的行数</returns>
        public int ExecSqlReInt(string sqlStr, IDataParameter[] para)
        {
            this.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.conn;
                if (isInTran)
                {
                    cmd.Transaction = sqlTran;
                }
                cmd.CommandText = sqlStr;
                if (para != null)
                {
                    foreach (SqlParameter paramete in para)
                    {
                        if (paramete.SqlValue == null)
                        {
                            paramete.SqlValue = "";
                        }
                        SqlParameter SqlParams = new SqlParameter(paramete.ParameterName, paramete.SqlValue);
                        cmd.Parameters.Add(SqlParams);
                        //cmd.Parameters.Add(paramete);

                    }
                }
                int i = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.Dispose();
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                return i;
            }
            catch (Exception ex)
            {
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                StringBuilder errstr = new StringBuilder();
                foreach (SqlParameter parateme in para)
                {
                    errstr.Append(parateme.ParameterName);
                    errstr.Append(":");
                    errstr.Append(parateme.Value.ToString());
                }
                throw new Exception(ex.Message + "执行sql时错误：" + sqlStr + ";" + errstr.ToString());
            }

        }
        /// <summary>
        /// 执行操作并返回刚刚插入的字段值
        /// </summary>
        /// <param name="sql">要执行的语句</param>
        /// <param name="idName">传入要返回的字段值</param>
        /// <returns></returns>
        public string ExecuteNonQueryBackId(string sql, string idName)//执行并返回刚刚插入的ID
        {

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Connection = this.conn;
                conn.Open();
                cmd.ExecuteNonQuery();


                DataSet ds = new DataSet();
                SqlCommand cmd2 = new SqlCommand("select   @@IDENTITY   as   " + idName + "", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd2);
                da.Fill(ds);

                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];

                string id = dr[idName].ToString();
                conn.Close();
                return id;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        /// <summary>
        /// 执行操作并返回刚刚插入的字段值
        /// </summary>
        /// <param name="sql">要执行的语句</param>
        /// <param name="idName">传入要返回的字段值</param>
        /// <param name="idName">传入参数组</param>
        /// <returns></returns>
        public string ExecuteNonQueryBackId(string sql, string idName, IDataParameter[] para)//执行并返回刚刚插入的ID
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.conn;
                this.Open();
                if (isInTran)
                {
                    cmd.Transaction = sqlTran;
                }
                cmd.CommandText = sql;
                if (para != null)
                {
                    foreach (SqlParameter paramete in para)
                    {
                        if (paramete.SqlValue == null)
                        {
                            paramete.SqlValue = "";
                        }
                        SqlParameter SqlParams = new SqlParameter(paramete.ParameterName, paramete.SqlValue);
                        cmd.Parameters.Add(SqlParams);
                        //cmd.Parameters.Add(paramete);

                    }
                }
                cmd.ExecuteNonQuery();


                DataSet ds = new DataSet();
                SqlCommand cmd2 = new SqlCommand("select  @@IDENTITY   as   '" + idName + "' ", conn);
                if (isInTran)
                {
                    cmd2.Transaction = sqlTran;
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd2);
                da.Fill(ds);

                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];

                string id = dr[idName].ToString();
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                return id;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        /// <summary>
        /// 执行带参数sql语句返回受影响行数
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="ParamsName">参数名数组</param>
        /// <param name="ParamsValue">参数值数组</param>
        /// <returns>受影响函数</returns>
        //public int ExecSqlReInt(string sqlStr, string[] ParamsName, object[] ParamsValue)
        //{
        //    this.Open();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = this.conn;
        //    if (isInTran)
        //    {
        //        cmd.Transaction = sqlTran;
        //    }

        //    if ((ParamsName != null) && (ParamsName.Length != ParamsValue.Length))
        //    {
        //        throw new System.Exception("参数和值不对应!");
        //    }

        //    cmd.CommandText = sqlStr;
        //    if (ParamsName != null)
        //    {
        //        SqlParameter SqlParams;
        //        for (int i = 0; i < ParamsName.Length; i++)
        //        {
        //            SqlParams = new SqlParameter(ParamsName[i], ParamsValue[i]);
        //            cmd.Parameters.Add(SqlParams);

        //        }
        //    }
        //    int k = cmd.ExecuteNonQuery();
        //    cmd.Parameters.Clear();
        //    cmd.Dispose();
        //    if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
        //    {
        //        this.Close();
        //    }
        //    return k;
        //}


        #endregion

        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param name="sqlstr">传入需要执行的查询语句</param>
        /// <returns></returns>
        public string ExecuteSqlScalar(string sqlstr)
        {
            this.Open();
            string w;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.conn;
                if (isInTran)
                {
                    cmd.Transaction = sqlTran;
                }
                cmd.CommandText = sqlstr;
                w = Convert.ToString(cmd.ExecuteScalar());

                cmd.Dispose();
                return w;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
            finally
            {
                this.Close();
            }

        }

        public string ExecuteSqlScalar(string sqlstr, IList<IDataParameter> para)
        {
            this.Open();
            string w;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.conn;
                if (isInTran)
                {
                    cmd.Transaction = sqlTran;
                }


                if (para != null)
                {


                    foreach (SqlParameter paramete in para)
                    {
                        if (paramete.SqlValue == null)
                        {
                            paramete.SqlValue = "";
                        }
                        SqlParameter SqlParams = new SqlParameter(paramete.ParameterName, paramete.SqlValue);
                        cmd.Parameters.Add(SqlParams);
                        //cmd.Parameters.Add(paramete);

                    }

                }
                cmd.CommandText = sqlstr;
                w = Convert.ToString(cmd.ExecuteScalar());

                cmd.Dispose();
                return w;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
            finally
            {
                this.Close();
            }

        }



        #region DataReader
        /// <summary>
        /// 执行sql语句返回Reader执行该方法时，数据库连接不会关闭如果IsConnCloseWhenEveryRun 为true并且没有执行事务则
        /// 在关闭dr的时候数据库连接自动关闭。在提交或者回滚事物之前必须关闭dr
        /// </summary>
        /// <param name="commandBehavior">前置条件：必须将改数据连接类的IsConnCloseWhenEveryRun 设置为false或者在事物过程中执行语句时
        /// 该项起作用，否则commandBehavior 一直为CloseConnection 即关闭dr时自动关闭链接 查询结果对数据库连接影响的说明</param>
        /// <param name="sqlstr">执行的sql语句</param>
        /// <returns>返回的reader</returns>
        public SqlDataReader ExecSqlReDr(CommandBehavior commandBehavior, string sqlstr)
        {
            this.Open();
            SqlDataReader dr = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.conn;
                if (isInTran)
                {
                    cmd.Transaction = sqlTran;
                }
                cmd.CommandText = sqlstr;

                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    commandBehavior = CommandBehavior.CloseConnection;
                }
                dr = cmd.ExecuteReader(commandBehavior);
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                if (dr != null && (!dr.IsClosed))
                {
                    dr.Close();
                    if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                    {
                        this.Close();
                    }
                }
                throw new Exception(ex.Message + "获取dr时错误：" + sqlstr);
            }
            return dr;
        }

        /// <summary>
        /// 执行sql语句返回Reader执行该方法时，数据库连接不会关闭如果IsConnCloseWhenEveryRun 为true并且没有执行事务则
        /// 在关闭dr的时候数据库连接自动关闭。在提交或者回滚事物之前必须关闭dr
        /// </summary>
        /// <param name="sqlstr">执行的sql语句</param>
        /// <returns>返回的reader</returns>
        public Dictionary<string,string> ExecSqlReDr(string sqlstr)
        {
            CommandBehavior commandBehavior = CommandBehavior.Default;
            this.Open();
            SqlDataReader dr = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.conn;
                if (isInTran)
                {
                    cmd.Transaction = sqlTran;
                }
                cmd.CommandText = sqlstr;

                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    commandBehavior = CommandBehavior.CloseConnection;
                }




                dr = cmd.ExecuteReader(commandBehavior);


                Dictionary<string, string> ddr = GetDataRow1(dr);
                dr.Close();
                cmd.Parameters.Clear();
                cmd.Dispose();
                return ddr;
            }
            catch (Exception ex)
            {
                if (dr != null && (!dr.IsClosed))
                {
                    dr.Close();
                    if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                    {
                        this.Close();
                    }
                }
                throw new Exception(ex.Message + "获取dr时错误：" + sqlstr);
            }



        }
        public Dictionary<string,string> GetDataRow1(SqlDataReader dataReader)
        {

            ///定义DataTable
            Dictionary<string, string> d = new Dictionary<string, string>();

            dataReader.Read();
            try
            {    ///动态添加表的数据列
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
  
                    d.Add(dataReader.GetName(i), dataReader[i].ToString());

                }
                return d;
            }
            catch (Exception ex)
            {
                ///抛出类型转换错误
                //SystemError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);

            }
        }
        /// <summary>
        /// 执行sql语句返回Reader执行该方法时，数据库连接不会关闭如果IsConnCloseWhenEveryRun 为true并且没有执行事务则
        /// 在关闭dr的时候数据库连接自动关闭。在提交或者回滚事物之前必须关闭dr
        /// </summary>
        /// <param name="commandBehavior">前置条件：必须将改数据连接类的IsConnCloseWhenEveryRun 设置为false或者在事物过程中执行语句时
        /// 该项起作用，否则commandBehavior 一直为CloseConnection 即关闭dr时自动关闭链接 查询结果对数据库连接影响的说明</param>
        /// <param name="sqlstr">执行的sql语句</param>
        /// <param name="para">参数列表</param>
        /// <returns>返回的SqlDataReader</returns>
        public SqlDataReader ExecSqlReDr(CommandBehavior commandBehavior, string sqlstr, IDataParameter[] para)
        {
            this.Open();
            SqlDataReader dr = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.conn;
                if (isInTran)
                {
                    cmd.Transaction = sqlTran;
                }

                cmd.CommandText = sqlstr;
                if (para != null)
                {
                    foreach (SqlParameter parateme in para)
                    {
                        cmd.Parameters.Add(parateme);
                    }
                }

                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    commandBehavior = CommandBehavior.CloseConnection;
                }
                dr = cmd.ExecuteReader(commandBehavior);
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                if (dr != null && (!dr.IsClosed))
                {
                    dr.Close();
                    if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                    {
                        this.Close();
                    }
                }
                StringBuilder errstr = new StringBuilder();
                foreach (SqlParameter parateme in para)
                {
                    errstr.Append(parateme.ParameterName);
                    errstr.Append(":");
                    errstr.Append(parateme.Value.ToString());
                }
                throw new Exception(ex.Message + "获取dr时错误：" + sqlstr + ";" + errstr.ToString());
            }

            return dr;

        }

        /// <summary>
        /// 执行sql语句返回Reader执行该方法时，数据库连接不会关闭如果IsConnCloseWhenEveryRun 为true并且没有执行事务则
        /// 在关闭dr的时候数据库连接自动关闭。在提交或者回滚事物之前必须关闭dr
        /// </summary>
        /// <param name="sqlStr">执行的sql语句</param>
        /// <param name="para">参数列表</param>
        /// <returns>返回的SqlDataReader</returns>
        public SqlDataReader ExecSqlReDr(string sqlStr, IList<IDataParameter> para)
        {
            IDataParameter[] datas = new IDataParameter[para.Count];
            para.CopyTo(datas, 0);
            return this.ExecSqlReDr(sqlStr, datas);
        }

        /// <summary>
        /// 执行sql语句返回Reader执行该方法时，数据库连接不会关闭如果IsConnCloseWhenEveryRun 为true并且没有执行事务则
        /// 在关闭dr的时候数据库连接自动关闭。在提交或者回滚事物之前必须关闭dr
        /// </summary>
        /// <param name="sqlstr">执行的sql语句</param>
        /// <param name="para">参数列表</param>
        /// <returns>返回的SqlDataReader</returns>
        public SqlDataReader ExecSqlReDr(string sqlstr, IDataParameter[] para)
        {
            CommandBehavior commandBehavior = CommandBehavior.Default;
            this.Open();
            SqlDataReader dr = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.conn;
                if (isInTran)
                {
                    cmd.Transaction = sqlTran;
                }

                cmd.CommandText = sqlstr;
                if (para != null)
                {
                    foreach (SqlParameter parateme in para)
                    {
                        cmd.Parameters.Add(parateme);
                    }
                }
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    commandBehavior = CommandBehavior.CloseConnection;
                }
                dr = cmd.ExecuteReader(commandBehavior);
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                if (dr != null && (!dr.IsClosed))
                {
                    dr.Close();
                    if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                    {
                        this.Close();
                    }
                }
                StringBuilder errstr = new StringBuilder();
                foreach (SqlParameter parateme in para)
                {
                    errstr.Append(parateme.ParameterName);
                    errstr.Append(":");
                    errstr.Append(parateme.Value.ToString());
                }
                throw new Exception(ex.Message + "获取dr时错误：" + sqlstr + ";" + errstr.ToString());
            }

            return dr;

        }

        #endregion

        #region DataSet



        /// <summary>
        /// 执行sql语句返回数据集
        /// </summary>
        /// <param name="sqlStr">执行sql语句</param>
        /// <returns>返回的数据集</returns>
        public DataSet ExecSqlReDs(string sqlStr)
        {
            this.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = this.conn;
                cmd.CommandTimeout = 50000;
                if (isInTran)
                {
                    cmd.Transaction = sqlTran;
                }
                cmd.CommandText = sqlStr;
                DataSet ds = new DataSet();
                using (SqlDataAdapter ad = new SqlDataAdapter())
                {
                    ad.SelectCommand = cmd;
                    ad.Fill(ds);
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                throw new Exception(ex.Message + "获取ds时错误：" + sqlStr);
            }


        }

        /// <summary>
        /// 执行带参数sql语句返回数据集
        /// </summary>
        /// <param name="sqlStr">执行的sql语句</param>
        /// <param name="para">参数列表</param>
        /// <returns>返回的数据集</returns>
        public DataSet ExecSqlReDs(string sqlStr, IList<IDataParameter> para)
        {
            IDataParameter[] datas = new IDataParameter[para.Count];
            para.CopyTo(datas, 0);
            return this.ExecSqlReDs(sqlStr, datas);
        }


        /// <summary>
        /// 执行带参数sql语句返回数据集
        /// </summary>
        /// <param name="sqlStr">执行的sql语句</param>
        /// <param name="para">参数列表</param>
        /// <returns>返回的数据集</returns>
        public DataSet ExecSqlReDs(string sqlStr, IDataParameter[] para)
        {
            this.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.conn;
                if (isInTran)
                {
                    cmd.Transaction = sqlTran;
                }

                cmd.CommandText = sqlStr;
                if (para != null)
                {
                    foreach (SqlParameter parateme in para)
                    {
                        cmd.Parameters.Add(parateme);
                    }
                }
                DataSet ds = new DataSet();
                using (SqlDataAdapter ad = new SqlDataAdapter())
                {
                    ad.SelectCommand = cmd;
                    ad.Fill(ds);
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                StringBuilder errstr = new StringBuilder();
                foreach (SqlParameter parateme in para)
                {
                    errstr.Append(parateme.ParameterName);
                    errstr.Append(":");
                    errstr.Append(parateme.Value.ToString());
                }
                throw new Exception(ex.Message + "获取ds时错误：" + sqlStr + ";" + errstr.ToString());
            }

        }

        #endregion

        #region 存储过程


        /// <summary>
        /// 执行存储过程返回return值 string 类型
        /// </summary>
        /// <param name="StoreName">过程名</param>
        /// <param name="ParamsName">参数名数组</param>
        /// <param name="ParamsValue">参数值数组</param>
        /// <returns>Return值</returns>
        public string ExecProc_ReRet(string StoreName, string[] ParamsName, object[] ParamsValue)
        {
            this.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.conn;
                if (isInTran)
                {
                    cmd.Transaction = sqlTran;
                }

                if ((ParamsName != null) && (ParamsName.Length != ParamsValue.Length))
                {
                    throw new System.Exception("参数和值不对应!");
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 50000;
                cmd.CommandText = StoreName;

                if (ParamsName != null)
                {
                    SqlParameter SqlParams;
                    for (int i = 0; i < ParamsName.Length; i++)
                    {
                        SqlParams = new SqlParameter(ParamsName[i], ParamsValue[i]);
                        cmd.Parameters.Add(SqlParams);
                    }

                    //增加返回的参数
                    SqlParameter Re_params = new SqlParameter("Return_Value", SqlDbType.VarChar);
                    Re_params.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(Re_params);
                }
                cmd.ExecuteNonQuery();
                string k = cmd.Parameters["Return_Value"].Value.ToString();
                cmd.Parameters.Clear();
                cmd.Dispose();
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                return k;
            }
            catch (Exception ex)
            {
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                StringBuilder errstr = new StringBuilder();
                foreach (string parateme in ParamsName)
                {
                    errstr.Append(parateme);
                    errstr.Append(",");
                }
                errstr.Append(":");
                foreach (object parateme in ParamsValue)
                {
                    errstr.Append(parateme.ToString());
                    errstr.Append(",");
                }
                throw new Exception(ex.Message + "执行存储过程时错误：" + StoreName + ";" + errstr.ToString());
            }

        }

        /// <summary>
        /// 执行带参数的存储过程返回Int类型
        /// </summary>
        /// <param name="StoreName">存储过程名称</param>
        /// <param name="ParamsName">参数名称列表</param>
        /// <param name="ParamsValue">参数值列表</param>
        /// <param name="OutName">返回参数名</param>
        /// <returns>返回的Int类型值</returns>
        public int ExecProc_ReInt(string StoreName, string[] ParamsName, object[] ParamsValue, string OutName)
        {

            this.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.conn;
                if (isInTran)
                {
                    cmd.Transaction = sqlTran;
                }

                if ((ParamsName != null) && (ParamsName.Length != ParamsValue.Length))
                {
                    throw new System.Exception("参数和值不对应!");
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = StoreName;

                if (ParamsName != null)
                {
                    SqlParameter SqlParams;
                    for (int i = 0; i < ParamsName.Length; i++)
                    {
                        SqlParams = new SqlParameter(ParamsName[i], ParamsValue[i]);
                        if (ParamsName[i].ToString() == OutName)
                        {
                            SqlParams.Direction = ParameterDirection.Output;
                        }
                        cmd.Parameters.Add(SqlParams);
                    }
                }
                cmd.ExecuteNonQuery();
                int k = Convert.ToInt32(cmd.Parameters[OutName].Value.ToString());
                cmd.Parameters.Clear();
                cmd.Dispose();
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                return k;
            }
            catch (Exception ex)
            {
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                StringBuilder errstr = new StringBuilder();
                foreach (string parateme in ParamsName)
                {
                    errstr.Append(parateme);
                    errstr.Append(",");
                }
                errstr.Append(":");
                foreach (object parateme in ParamsValue)
                {
                    errstr.Append(parateme.ToString());
                    errstr.Append(",");
                }
                throw new Exception(ex.Message + "执行存储过程时错误：" + StoreName + ";" + errstr.ToString());
            }

        }

        /// <summary>
        /// 执行带参数的存储过程返回String类型
        /// </summary>
        /// <param name="StoreName">存储过程名称</param>
        /// <param name="ParamsName">参数名称列表</param>
        /// <param name="ParamsValue">参数值列表</param>
        /// <param name="OutName">返回参数名</param>
        /// <returns>返回的String类型值</returns>
        public string ExecProc_ReStr(string StoreName, string[] ParamsName, object[] ParamsValue, string OutName)
        {
            this.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.conn;
                if (isInTran)
                {
                    cmd.Transaction = sqlTran;
                }

                if ((ParamsName != null) && (ParamsName.Length != ParamsValue.Length))
                {
                    throw new System.Exception("参数和值不对应!");
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = StoreName;

                if (ParamsName != null)
                {
                    SqlParameter SqlParams;
                    for (int i = 0; i < ParamsName.Length; i++)
                    {
                        SqlParams = new SqlParameter(ParamsName[i], ParamsValue[i]);
                        if (ParamsName[i].ToString() == OutName)
                        {
                            SqlParams.Size = 255;
                            SqlParams.Direction = ParameterDirection.Output;
                        }
                        cmd.Parameters.Add(SqlParams);
                    }
                }
                cmd.ExecuteNonQuery();
                string k = Convert.ToString(cmd.Parameters[OutName].Value.ToString());
                cmd.Parameters.Clear();
                cmd.Dispose();
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                return k;
            }
            catch (Exception ex)
            {
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                StringBuilder errstr = new StringBuilder();
                foreach (string parateme in ParamsName)
                {
                    errstr.Append(parateme);
                    errstr.Append(",");
                }
                errstr.Append(":");
                foreach (object parateme in ParamsValue)
                {
                    errstr.Append(parateme.ToString());
                    errstr.Append(",");
                }
                throw new Exception(ex.Message + "执行存储过程时错误：" + StoreName + ";" + errstr.ToString());
            }

        }

        /// <summary>
        /// 执行带参数的存储过程返回数据集
        /// </summary>
        /// <param name="StoreName">过程名称</param>
        /// <param name="ParamsName">参数名称列表</param>
        /// <param name="ParamsValue">参数值列表</param>
        /// <returns>返回的数据集</returns>
        public DataSet ExecProc_ReDs(string StoreName, string[] ParamsName, object[] ParamsValue)
        {
            this.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.conn;
                if (isInTran)
                {
                    cmd.Transaction = sqlTran;
                }

                if ((ParamsName != null) && (ParamsName.Length != ParamsValue.Length))
                {
                    throw new System.Exception("参数和值不对应!");
                }
                cmd.CommandTimeout = 50000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = StoreName;

                if (ParamsName != null)
                {
                    SqlParameter OraParams;
                    for (int i = 0; i < ParamsName.Length; i++)
                    {
                        OraParams = new SqlParameter(ParamsName[i], ParamsValue[i]);
                        cmd.Parameters.Add(OraParams);
                    }
                }

                DataSet ds = new DataSet();
                using (SqlDataAdapter ad = new SqlDataAdapter())
                {
                    ad.SelectCommand = cmd;
                    ad.Fill(ds);
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                if ((!this.isInTran) && this.isConnCloseWhenEveryRun)
                {
                    this.Close();
                }
                StringBuilder errstr = new StringBuilder();
                foreach (string parateme in ParamsName)
                {
                    errstr.Append(parateme);
                    errstr.Append(",");
                }
                errstr.Append(":");
                foreach (object parateme in ParamsValue)
                {
                    errstr.Append(parateme.ToString());
                    errstr.Append(",");
                }
                throw new Exception(ex.Message + "执行存储过程时错误：" + StoreName + ";" + errstr.ToString());
            }

        }
        #endregion


        #region 其他




        #endregion


        /// <summary>
        /// 析构方法
        /// </summary>
        ~MSSQLHelper()
        {
            this.Dispose();
        }

        /// <summary>
        /// 释放占用的资源
        /// </summary>
        /// <param name="dis"></param>
        protected void Dispose(bool dis)
        {
            if (dis)
            {
                if (this.conn != null)
                {
                    this.Close();
                    this.conn.Dispose();
                }

                if (this.sqlTran != null)
                {
                    this.sqlTran.Dispose();
                }

            }

        }

        /// <summary>
        /// 释放数据库连接
        /// </summary>
        public void Dispose()
        {
            try
            {
                this.Dispose(true);
            }
            catch
            {

            }
            this.conn = null;
            this.sqlTran = null;

        }

        /// <summary>
        /// 返回指定字符串的实例
        /// </summary>
        /// <param name="connstr">链接字符串</param>
        /// <returns></returns>
        public IDBHelper GetDBHelperInstance(string connstr)
        {
            return new MSSQLHelper(connstr);
        }
    }

}
