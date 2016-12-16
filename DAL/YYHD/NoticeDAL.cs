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
    //Notice
    public partial class NoticeDAL
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
            strSql.Append(" FROM  YYHD.dbo.Notice ");
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
        public bool Add(NoticeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YYHD.dbo.Notice (");
            strSql.Append("NoticeTitle,NoticeContent,NoticeType,CreateTime,RongUserId,Extra");
            strSql.Append(") values (");
            strSql.Append("@NoticeTitle,@NoticeContent,@NoticeType,@CreateTime,@RongUserId,@Extra");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@NoticeTitle", SqlDbType.VarChar,250) ,
                        new SqlParameter("@NoticeContent", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@NoticeType", SqlDbType.VarChar,20) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@RongUserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Extra", SqlDbType.VarChar,-1)

            };

            parameters[0].Value = model.NoticeTitle;
            parameters[1].Value = model.NoticeContent;
            parameters[2].Value = model.NoticeType;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.RongUserId;
            parameters[5].Value = model.Extra;

            bool result = false;
            try
            {
                model.NoticeId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "NoticeId", parameters));
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
        public bool Update(NoticeModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YYHD.dbo.Notice set ");

            strSql.Append(" NoticeTitle = @NoticeTitle , ");
            strSql.Append(" NoticeContent = @NoticeContent , ");
            strSql.Append(" NoticeType = @NoticeType , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" RongUserId = @RongUserId , ");
            strSql.Append(" Extra = @Extra  ");
            strSql.Append(" where NoticeId=@NoticeId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@NoticeId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@NoticeTitle", SqlDbType.VarChar,250) ,
                        new SqlParameter("@NoticeContent", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@NoticeType", SqlDbType.VarChar,20) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@RongUserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Extra", SqlDbType.VarChar,-1)

            };

            parameters[0].Value = model.NoticeId;
            parameters[1].Value = model.NoticeTitle;
            parameters[2].Value = model.NoticeContent;
            parameters[3].Value = model.NoticeType;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.RongUserId;
            parameters[6].Value = model.Extra; try
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
        public NoticeModel GetModel(decimal NoticeId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select NoticeId, NoticeTitle, NoticeContent, NoticeType, CreateTime, RongUserId, Extra  ");
            strSql.Append("  from YYHD.dbo.Notice ");
            strSql.Append(" where NoticeId=@NoticeId");
            SqlParameter[] parameters = {
                    new SqlParameter("@NoticeId", SqlDbType.Decimal)
            };
            parameters[0].Value = NoticeId;


            NoticeModel model = new NoticeModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["NoticeId"].ToString() != "")
                {
                    model.NoticeId = decimal.Parse(ds.Tables[0].Rows[0]["NoticeId"].ToString());
                }
                model.NoticeTitle = ds.Tables[0].Rows[0]["NoticeTitle"].ToString();
                model.NoticeContent = ds.Tables[0].Rows[0]["NoticeContent"].ToString();
                model.NoticeType = ds.Tables[0].Rows[0]["NoticeType"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.RongUserId = ds.Tables[0].Rows[0]["RongUserId"].ToString();
                model.Extra = ds.Tables[0].Rows[0]["Extra"].ToString();

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
            strSql.Append("delete from YYHD.dbo.Notice ");
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
            object[] fenyeParmValue = new object[] { "YYHD.dbo.Notice  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM YYHD.dbo.Notice  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM YYHD.dbo.Notice  WITH(NOLOCK) ");
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
            strSql.Append(" FROM YYHD.dbo.Notice  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

