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
    //BranchVsSite
    public partial class BranchVsSiteDAL
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
            strSql.Append(" FROM  CORE.dbo.BranchVsSite ");
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
        public bool Add(BranchVsSiteModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.BranchVsSite (");
            strSql.Append("BranchId,SiteId,VsOrder");
            strSql.Append(") values (");
            strSql.Append("@BranchId,@SiteId,@VsOrder");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@SiteId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@VsOrder", SqlDbType.Int,4)

            };

            parameters[0].Value = model.BranchId;
            parameters[1].Value = model.SiteId;
            parameters[2].Value = model.VsOrder;

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
        public bool Update(BranchVsSiteModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.BranchVsSite set ");

            strSql.Append(" BranchId = @BranchId , ");
            strSql.Append(" SiteId = @SiteId , ");
            strSql.Append(" VsOrder = @VsOrder  ");
            strSql.Append(" where BranchId=@BranchId and SiteId=@SiteId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@SiteId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@VsOrder", SqlDbType.Int,4)

            };

            parameters[0].Value = model.BranchId;
            parameters[1].Value = model.SiteId;
            parameters[2].Value = model.VsOrder; try
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
        public BranchVsSiteModel GetModel(string BranchId, decimal SiteId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BranchId, SiteId, VsOrder  ");
            strSql.Append("  from CORE.dbo.BranchVsSite ");
            strSql.Append(" where BranchId=@BranchId and SiteId=@SiteId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@BranchId", SqlDbType.VarChar,50),
                    new SqlParameter("@SiteId", SqlDbType.Decimal,9)            };
            parameters[0].Value = BranchId;
            parameters[1].Value = SiteId;


            BranchVsSiteModel model = new BranchVsSiteModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.BranchId = ds.Tables[0].Rows[0]["BranchId"].ToString();
                if (ds.Tables[0].Rows[0]["SiteId"].ToString() != "")
                {
                    model.SiteId = decimal.Parse(ds.Tables[0].Rows[0]["SiteId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VsOrder"].ToString() != "")
                {
                    model.VsOrder = int.Parse(ds.Tables[0].Rows[0]["VsOrder"].ToString());
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
            strSql.Append("delete from CORE.dbo.BranchVsSite ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.BranchVsSite  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.BranchVsSite  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.BranchVsSite  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.BranchVsSite  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

