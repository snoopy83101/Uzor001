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
    //PageInfo
    public partial class PageInfoDAL
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
            strSql.Append(" FROM  CORE.dbo.PageInfo ");
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
        public bool Add(PageInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.PageInfo (");
            strSql.Append("PageId,PageName,PageMemo,OrderNo,MerId,Invalid,PageLabel,BranchId,ZoneId");
            strSql.Append(") values (");
            strSql.Append("@PageId,@PageName,@PageMemo,@OrderNo,@MerId,@Invalid,@PageLabel,@BranchId,@ZoneId");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@PageId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PageName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PageMemo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@PageLabel", SqlDbType.VarChar,50) ,
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ZoneId", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.PageId;
            parameters[1].Value = model.PageName;
            parameters[2].Value = model.PageMemo;
            parameters[3].Value = model.OrderNo;
            parameters[4].Value = model.MerId;
            parameters[5].Value = model.Invalid;
            parameters[6].Value = model.PageLabel;
            parameters[7].Value = model.BranchId;
            parameters[8].Value = model.ZoneId;

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
        public bool Update(PageInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.PageInfo set ");

            strSql.Append(" PageId = @PageId , ");
            strSql.Append(" PageName = @PageName , ");
            strSql.Append(" PageMemo = @PageMemo , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" MerId = @MerId , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" PageLabel = @PageLabel , ");
            strSql.Append(" BranchId = @BranchId , ");
            strSql.Append(" ZoneId = @ZoneId  ");
            strSql.Append(" where PageId=@PageId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@PageId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PageName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PageMemo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@PageLabel", SqlDbType.VarChar,50) ,
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ZoneId", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.PageId;
            parameters[1].Value = model.PageName;
            parameters[2].Value = model.PageMemo;
            parameters[3].Value = model.OrderNo;
            parameters[4].Value = model.MerId;
            parameters[5].Value = model.Invalid;
            parameters[6].Value = model.PageLabel;
            parameters[7].Value = model.BranchId;
            parameters[8].Value = model.ZoneId; try
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
        public PageInfoModel GetModel(string PageId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PageId, PageName, PageMemo, OrderNo, MerId, Invalid, PageLabel, BranchId, ZoneId  ");
            strSql.Append("  from CORE.dbo.PageInfo ");
            strSql.Append(" where PageId=@PageId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@PageId", SqlDbType.VarChar,50)           };
            parameters[0].Value = PageId;


            PageInfoModel model = new PageInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.PageId = ds.Tables[0].Rows[0]["PageId"].ToString();
                model.PageName = ds.Tables[0].Rows[0]["PageName"].ToString();
                model.PageMemo = ds.Tables[0].Rows[0]["PageMemo"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MerId"].ToString() != "")
                {
                    model.MerId = decimal.Parse(ds.Tables[0].Rows[0]["MerId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Invalid"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Invalid"].ToString() == "1") || (ds.Tables[0].Rows[0]["Invalid"].ToString().ToLower() == "true"))
                    {
                        model.Invalid = true;
                    }
                    else
                    {
                        model.Invalid = false;
                    }
                }
                model.PageLabel = ds.Tables[0].Rows[0]["PageLabel"].ToString();
                model.BranchId = ds.Tables[0].Rows[0]["BranchId"].ToString();
                model.ZoneId = ds.Tables[0].Rows[0]["ZoneId"].ToString();

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
            strSql.Append("delete from CORE.dbo.PageInfo ");
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
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize, string cols)
        {
            if (cols == "")
            {
                cols = " * ";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 " + cols + " ");
            strSql.Append(" FROM CORE.dbo.PageInfo  WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("CORE.dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM CORE.dbo.PageInfo  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.PageInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

