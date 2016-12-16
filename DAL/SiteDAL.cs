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
    //Site
    public partial class SiteDAL
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
            strSql.Append(" FROM  CORE.dbo.Site ");
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
        public bool Add(SiteModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.Site (");
            strSql.Append("ZoneId,SiteName,SiteMemo,SiteLng,SiteLat,ParentSiteId,Unit,SiteType");
            strSql.Append(") values (");
            strSql.Append("@ZoneId,@SiteName,@SiteMemo,@SiteLng,@SiteLat,@ParentSiteId,@Unit,@SiteType");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@ZoneId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SiteName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@SiteMemo", SqlDbType.VarChar,1000) ,
                        new SqlParameter("@SiteLng", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SiteLat", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ParentSiteId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Unit", SqlDbType.VarChar,20) ,
                        new SqlParameter("@SiteType", SqlDbType.VarChar,10)

            };

            parameters[0].Value = model.ZoneId;
            parameters[1].Value = model.SiteName;
            parameters[2].Value = model.SiteMemo;
            parameters[3].Value = model.SiteLng;
            parameters[4].Value = model.SiteLat;
            parameters[5].Value = model.ParentSiteId;
            parameters[6].Value = model.Unit;
            parameters[7].Value = model.SiteType;

            bool result = false;
            try
            {


                model.SiteId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "SiteId", parameters));


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
        public bool Update(SiteModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.Site set ");

            strSql.Append(" ZoneId = @ZoneId , ");
            strSql.Append(" SiteName = @SiteName , ");
            strSql.Append(" SiteMemo = @SiteMemo , ");
            strSql.Append(" SiteLng = @SiteLng , ");
            strSql.Append(" SiteLat = @SiteLat , ");
            strSql.Append(" ParentSiteId = @ParentSiteId , ");
            strSql.Append(" Unit = @Unit , ");
            strSql.Append(" SiteType = @SiteType  ");
            strSql.Append(" where SiteId=@SiteId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@SiteId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ZoneId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SiteName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@SiteMemo", SqlDbType.VarChar,1000) ,
                        new SqlParameter("@SiteLng", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SiteLat", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ParentSiteId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Unit", SqlDbType.VarChar,20) ,
                        new SqlParameter("@SiteType", SqlDbType.VarChar,10)

            };

            parameters[0].Value = model.SiteId;
            parameters[1].Value = model.ZoneId;
            parameters[2].Value = model.SiteName;
            parameters[3].Value = model.SiteMemo;
            parameters[4].Value = model.SiteLng;
            parameters[5].Value = model.SiteLat;
            parameters[6].Value = model.ParentSiteId;
            parameters[7].Value = model.Unit;
            parameters[8].Value = model.SiteType; try
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
        public SiteModel GetModel(decimal SiteId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SiteId, ZoneId, SiteName, SiteMemo, SiteLng, SiteLat, ParentSiteId, Unit, SiteType  ");
            strSql.Append("  from CORE.dbo.Site ");
            strSql.Append(" where SiteId=@SiteId");
            SqlParameter[] parameters = {
                    new SqlParameter("@SiteId", SqlDbType.Decimal)
            };
            parameters[0].Value = SiteId;


            SiteModel model = new SiteModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SiteId"].ToString() != "")
                {
                    model.SiteId = decimal.Parse(ds.Tables[0].Rows[0]["SiteId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZoneId"].ToString() != "")
                {
                    model.ZoneId = decimal.Parse(ds.Tables[0].Rows[0]["ZoneId"].ToString());
                }
                model.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                model.SiteMemo = ds.Tables[0].Rows[0]["SiteMemo"].ToString();
                if (ds.Tables[0].Rows[0]["SiteLng"].ToString() != "")
                {
                    model.SiteLng = decimal.Parse(ds.Tables[0].Rows[0]["SiteLng"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SiteLat"].ToString() != "")
                {
                    model.SiteLat = decimal.Parse(ds.Tables[0].Rows[0]["SiteLat"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParentSiteId"].ToString() != "")
                {
                    model.ParentSiteId = decimal.Parse(ds.Tables[0].Rows[0]["ParentSiteId"].ToString());
                }
                model.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
                model.SiteType = ds.Tables[0].Rows[0]["SiteType"].ToString();

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
            strSql.Append("delete from CORE.dbo.Site ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.SiteView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.Site  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.Site  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.Site  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

