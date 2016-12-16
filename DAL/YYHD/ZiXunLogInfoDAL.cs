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
    //ZiXunLogInfo
    public partial class ZiXunLogInfoDAL
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
            strSql.Append(" FROM  YYHD.dbo.ZiXunLogInfo ");
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
        public bool Add(ZiXunLogInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YYHD.dbo.ZiXunLogInfo (");
            strSql.Append("RongUserId,ZiXunLogTypeId,ZiXunLogReKey,ZiXunLogJson,CreateTime,Memo");
            strSql.Append(") values (");
            strSql.Append("@RongUserId,@ZiXunLogTypeId,@ZiXunLogReKey,@ZiXunLogJson,@CreateTime,@Memo");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@RongUserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ZiXunLogTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@ZiXunLogReKey", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ZiXunLogJson", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,200)

            };

            parameters[0].Value = model.RongUserId;
            parameters[1].Value = model.ZiXunLogTypeId;
            parameters[2].Value = model.ZiXunLogReKey;
            parameters[3].Value = model.ZiXunLogJson;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.Memo;

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
        public bool Update(ZiXunLogInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YYHD.dbo.ZiXunLogInfo set ");

            strSql.Append(" RongUserId = @RongUserId , ");
            strSql.Append(" ZiXunLogTypeId = @ZiXunLogTypeId , ");
            strSql.Append(" ZiXunLogReKey = @ZiXunLogReKey , ");
            strSql.Append(" ZiXunLogJson = @ZiXunLogJson , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" Memo = @Memo  ");
            strSql.Append(" where ZiXunLogId=@ZiXunLogId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@ZiXunLogId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@RongUserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ZiXunLogTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@ZiXunLogReKey", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ZiXunLogJson", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,200)

            };

            parameters[0].Value = model.ZiXunLogId;
            parameters[1].Value = model.RongUserId;
            parameters[2].Value = model.ZiXunLogTypeId;
            parameters[3].Value = model.ZiXunLogReKey;
            parameters[4].Value = model.ZiXunLogJson;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.Memo; try
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
        public ZiXunLogInfoModel GetModel(decimal ZiXunLogId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ZiXunLogId, RongUserId, ZiXunLogTypeId, ZiXunLogReKey, ZiXunLogJson, CreateTime, Memo  ");
            strSql.Append("  from YYHD.dbo.ZiXunLogInfo ");
            strSql.Append(" where ZiXunLogId=@ZiXunLogId");
            SqlParameter[] parameters = {
                    new SqlParameter("@ZiXunLogId", SqlDbType.Decimal)
            };
            parameters[0].Value = ZiXunLogId;


            ZiXunLogInfoModel model = new ZiXunLogInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ZiXunLogId"].ToString() != "")
                {
                    model.ZiXunLogId = decimal.Parse(ds.Tables[0].Rows[0]["ZiXunLogId"].ToString());
                }
                model.RongUserId = ds.Tables[0].Rows[0]["RongUserId"].ToString();
                if (ds.Tables[0].Rows[0]["ZiXunLogTypeId"].ToString() != "")
                {
                    model.ZiXunLogTypeId = int.Parse(ds.Tables[0].Rows[0]["ZiXunLogTypeId"].ToString());
                }
                model.ZiXunLogReKey = ds.Tables[0].Rows[0]["ZiXunLogReKey"].ToString();
                model.ZiXunLogJson = ds.Tables[0].Rows[0]["ZiXunLogJson"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();

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
            strSql.Append("delete from YYHD.dbo.ZiXunLogInfo ");
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
            object[] fenyeParmValue = new object[] { "YYHD.dbo.ZiXunLogInfo  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM YYHD.dbo.ZiXunLogInfo  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM YYHD.dbo.ZiXunLogInfo  WITH(NOLOCK) ");
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
            strSql.Append(" FROM YYHD.dbo.ZiXunLogInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

