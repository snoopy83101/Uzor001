using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Model;
using DBTools;
using Common;
namespace DAL
{
    //KfJieDai
    public partial class KfJieDaiDAL
    {

        #region  //数据操作

        /// <summary>
        /// 数据库帮助对象
        /// </summary>
        private MSSQLHelper helper = new MSSQLHelper();
        #endregion

        /// <summary>
        /// 检查是否存在
        /// </summary>
        public int ExInt(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(0) ");
            strSql.Append(" FROM  YYHD.dbo.KfJieDai ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            int i = int.Parse(helper.ExecuteSqlScalar(strSql.ToString()));
            return i;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(KfJieDaiModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YYHD.dbo.KfJieDai (");
            strSql.Append("DeviceId,UserId,CreateTime,LastTime,JieDaiStatus");
            strSql.Append(") values (");
            strSql.Append("@DeviceId,@UserId,@CreateTime,@LastTime,@JieDaiStatus");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@DeviceId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@LastTime", SqlDbType.DateTime) ,
                        new SqlParameter("@JieDaiStatus", SqlDbType.Int,4)

            };

            parameters[0].Value = model.DeviceId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.CreateTime;
            parameters[3].Value = model.LastTime;
            parameters[4].Value = model.JieDaiStatus;

            bool result = false;
            try
            {
                helper.ExecSqlReInt(strSql.ToString(), parameters);
                result = true;
            }
            catch (Exception ex)
            {

                this.helper.Close();
                throw ex;
            }
            finally
            {

            }
            return result;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(KfJieDaiModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YYHD.dbo.KfJieDai set ");

            strSql.Append(" DeviceId = @DeviceId , ");
            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" LastTime = @LastTime , ");
            strSql.Append(" JieDaiStatus = @JieDaiStatus  ");
            strSql.Append(" where KfJieDaiId=@KfJieDaiId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@KfJieDaiId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@DeviceId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@LastTime", SqlDbType.DateTime) ,
                        new SqlParameter("@JieDaiStatus", SqlDbType.Int,4)

            };

            parameters[0].Value = model.KfJieDaiId;
            parameters[1].Value = model.DeviceId;
            parameters[2].Value = model.UserId;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.LastTime;
            parameters[5].Value = model.JieDaiStatus; try
            {//异常处理
                reCount = this.helper.ExecSqlReInt(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {

                this.helper.Close();
                throw ex;
            }
            if (reCount <= 0)
            {
                reValue = false;
            }
            return reValue;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public KfJieDaiModel GetModel(decimal KfJieDaiId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select KfJieDaiId, DeviceId, UserId, CreateTime, LastTime, JieDaiStatus  ");
            strSql.Append("  from YYHD.dbo.KfJieDai ");
            strSql.Append(" where KfJieDaiId=@KfJieDaiId");
            SqlParameter[] parameters = {
                    new SqlParameter("@KfJieDaiId", SqlDbType.Decimal)
            };
            parameters[0].Value = KfJieDaiId;


            KfJieDaiModel model = new KfJieDaiModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["KfJieDaiId"].ToString() != "")
                {
                    model.KfJieDaiId = decimal.Parse(ds.Tables[0].Rows[0]["KfJieDaiId"].ToString());
                }
                model.DeviceId = ds.Tables[0].Rows[0]["DeviceId"].ToString();
                model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastTime"].ToString() != "")
                {
                    model.LastTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JieDaiStatus"].ToString() != "")
                {
                    model.JieDaiStatus = int.Parse(ds.Tables[0].Rows[0]["JieDaiStatus"].ToString());
                }

                return model;
            }
            else
            {
                return model;
            }
        }


        /// <summary>
        /// 删除duo条数据
        /// </summary>
        public bool DeleteList(string strWhere)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from YYHD.dbo.KfJieDai ");
            strSql.Append(" where " + strWhere);
            try
            {//异常处理
                reCount = this.helper.ExecSqlReInt(strSql.ToString());
            }
            catch (Exception ex)
            {

                this.helper.Close();
                throw ex;
            }
            if (reCount <= 0)
            {
                reValue = false;
            }
            return reValue;
        }


        /// <summary>
        /// 获得fenye数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, string Order, int currentpage, int pagesize, string col)
        {
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "TableName", "ReFieldsStr", "OrderString", "WhereString", "PageSize", "PageIndex", "TotalRecord" };
            object[] fenyeParmValue = new object[] { "YYHD.dbo.KfJieDai  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
            ds = helper.ExecProc_ReDs("dbo.fenye2", fenyeParmName, fenyeParmValue);
            ds = Common.DataSetting.DataPageSetting(ds, pagesize, currentpage);
            return ds;


        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize, string cols)
        {
            if (cols == "")
            {
                cols = " * ";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 " + cols + " ");
            strSql.Append(" FROM YYHD.dbo.KfJieDai  WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("YYHD.dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM YYHD.dbo.KfJieDai  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return helper.ExecSqlReDs(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM YYHD.dbo.KfJieDai  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

