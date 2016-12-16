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
    //TargetIds
    public partial class TargetIdsDAL
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
            strSql.Append(" FROM  YYHD.dbo.TargetIds ");
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
        public bool Add(TargetIdsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YYHD.dbo.TargetIds (");
            strSql.Append("RongMsgLogId,TargetId,MsgStatus");
            strSql.Append(") values (");
            strSql.Append("@RongMsgLogId,@TargetId,@MsgStatus");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@RongMsgLogId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@TargetId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MsgStatus", SqlDbType.Int,4)

            };

            parameters[0].Value = model.RongMsgLogId;
            parameters[1].Value = model.TargetId;
            parameters[2].Value = model.MsgStatus;

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
        public bool Update(TargetIdsModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YYHD.dbo.TargetIds set ");

            strSql.Append(" RongMsgLogId = @RongMsgLogId , ");
            strSql.Append(" TargetId = @TargetId , ");
            strSql.Append(" MsgStatus = @MsgStatus  ");
            strSql.Append(" where RongMsgLogId=@RongMsgLogId and TargetId=@TargetId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@RongMsgLogId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@TargetId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MsgStatus", SqlDbType.Int,4)

            };

            parameters[0].Value = model.RongMsgLogId;
            parameters[1].Value = model.TargetId;
            parameters[2].Value = model.MsgStatus; try
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
        public TargetIdsModel GetModel(decimal RongMsgLogId, string TargetId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select RongMsgLogId, TargetId, MsgStatus  ");
            strSql.Append("  from YYHD.dbo.TargetIds ");
            strSql.Append(" where RongMsgLogId=@RongMsgLogId and TargetId=@TargetId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RongMsgLogId", SqlDbType.Decimal,9),
                    new SqlParameter("@TargetId", SqlDbType.VarChar,50)         };
            parameters[0].Value = RongMsgLogId;
            parameters[1].Value = TargetId;


            TargetIdsModel model = new TargetIdsModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["RongMsgLogId"].ToString() != "")
                {
                    model.RongMsgLogId = decimal.Parse(ds.Tables[0].Rows[0]["RongMsgLogId"].ToString());
                }
                model.TargetId = ds.Tables[0].Rows[0]["TargetId"].ToString();
                if (ds.Tables[0].Rows[0]["MsgStatus"].ToString() != "")
                {
                    model.MsgStatus = int.Parse(ds.Tables[0].Rows[0]["MsgStatus"].ToString());
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
            strSql.Append("delete from YYHD.dbo.TargetIds ");
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
            object[] fenyeParmValue = new object[] { "YYHD.dbo.TargetIds  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM YYHD.dbo.TargetIds  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM YYHD.dbo.TargetIds  WITH(NOLOCK) ");
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
            strSql.Append(" FROM YYHD.dbo.TargetIds  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

