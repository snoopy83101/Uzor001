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
    //SiteDetail
    public partial class SiteDetailDAL
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
            strSql.Append(" FROM  CORE.dbo.SiteDetail ");
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
        public bool Add(SiteDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.SiteDetail (");
            strSql.Append("SiteDetailName,OrderNo,SiteDetaiMemo,SiteId,SiteDetailType,Extra");
            strSql.Append(") values (");
            strSql.Append("@SiteDetailName,@OrderNo,@SiteDetaiMemo,@SiteId,@SiteDetailType,@Extra");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@SiteDetailName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@SiteDetaiMemo", SqlDbType.VarChar,100) ,
                        new SqlParameter("@SiteId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SiteDetailType", SqlDbType.VarChar,10) ,
                        new SqlParameter("@Extra", SqlDbType.VarChar,-1)

            };

            parameters[0].Value = model.SiteDetailName;
            parameters[1].Value = model.OrderNo;
            parameters[2].Value = model.SiteDetaiMemo;
            parameters[3].Value = model.SiteId;
            parameters[4].Value = model.SiteDetailType;
            parameters[5].Value = model.Extra;

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
        public bool Update(SiteDetailModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.SiteDetail set ");

            strSql.Append(" SiteDetailName = @SiteDetailName , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" SiteDetaiMemo = @SiteDetaiMemo , ");
            strSql.Append(" SiteId = @SiteId , ");
            strSql.Append(" SiteDetailType = @SiteDetailType , ");
            strSql.Append(" Extra = @Extra  ");
            strSql.Append(" where SiteDetailId=@SiteDetailId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@SiteDetailId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SiteDetailName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@SiteDetaiMemo", SqlDbType.VarChar,100) ,
                        new SqlParameter("@SiteId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SiteDetailType", SqlDbType.VarChar,10) ,
                        new SqlParameter("@Extra", SqlDbType.VarChar,-1)

            };

            parameters[0].Value = model.SiteDetailId;
            parameters[1].Value = model.SiteDetailName;
            parameters[2].Value = model.OrderNo;
            parameters[3].Value = model.SiteDetaiMemo;
            parameters[4].Value = model.SiteId;
            parameters[5].Value = model.SiteDetailType;
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
        public SiteDetailModel GetModel(decimal SiteDetailId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SiteDetailId, SiteDetailName, OrderNo, SiteDetaiMemo, SiteId, SiteDetailType, Extra  ");
            strSql.Append("  from CORE.dbo.SiteDetail ");
            strSql.Append(" where SiteDetailId=@SiteDetailId");
            SqlParameter[] parameters = {
                    new SqlParameter("@SiteDetailId", SqlDbType.Decimal)
            };
            parameters[0].Value = SiteDetailId;


            SiteDetailModel model = new SiteDetailModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SiteDetailId"].ToString() != "")
                {
                    model.SiteDetailId = decimal.Parse(ds.Tables[0].Rows[0]["SiteDetailId"].ToString());
                }
                model.SiteDetailName = ds.Tables[0].Rows[0]["SiteDetailName"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
                }
                model.SiteDetaiMemo = ds.Tables[0].Rows[0]["SiteDetaiMemo"].ToString();
                if (ds.Tables[0].Rows[0]["SiteId"].ToString() != "")
                {
                    model.SiteId = decimal.Parse(ds.Tables[0].Rows[0]["SiteId"].ToString());
                }
                model.SiteDetailType = ds.Tables[0].Rows[0]["SiteDetailType"].ToString();
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
            strSql.Append("delete from CORE.dbo.SiteDetail ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.SiteDetail  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.SiteDetail  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.SiteDetail  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.SiteDetail  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

