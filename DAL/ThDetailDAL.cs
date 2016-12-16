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
    //ThDetail
    public partial class ThDetailDAL
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
            strSql.Append(" FROM  CORE.dbo.ThDetail ");
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
        public bool Add(ThDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.ThDetail (");
            strSql.Append("ThId,DingDanDetailId,ThDetailTypeId,Memo,ThDetailAttr,ThQuantity");
            strSql.Append(") values (");
            strSql.Append("@ThId,@DingDanDetailId,@ThDetailTypeId,@Memo,@ThDetailAttr,@ThQuantity");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@ThId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DingDanDetailId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ThDetailTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,2000) ,            
                        new SqlParameter("@ThDetailAttr", SqlDbType.Xml,-1) ,            
                        new SqlParameter("@ThQuantity", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.ThId;
            parameters[1].Value = model.DingDanDetailId;
            parameters[2].Value = model.ThDetailTypeId;
            parameters[3].Value = model.Memo;
            parameters[4].Value = model.ThDetailAttr;
            parameters[5].Value = model.ThQuantity;

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
        public bool Update(ThDetailModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.ThDetail set ");

            strSql.Append(" ThId = @ThId , ");
            strSql.Append(" DingDanDetailId = @DingDanDetailId , ");
            strSql.Append(" ThDetailTypeId = @ThDetailTypeId , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" ThDetailAttr = @ThDetailAttr , ");
            strSql.Append(" ThQuantity = @ThQuantity  ");
            strSql.Append(" where ThDetailId=@ThDetailId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ThDetailId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ThId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DingDanDetailId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ThDetailTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,2000) ,            
                        new SqlParameter("@ThDetailAttr", SqlDbType.Xml,-1) ,            
                        new SqlParameter("@ThQuantity", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.ThDetailId;
            parameters[1].Value = model.ThId;
            parameters[2].Value = model.DingDanDetailId;
            parameters[3].Value = model.ThDetailTypeId;
            parameters[4].Value = model.Memo;
            parameters[5].Value = model.ThDetailAttr;
            parameters[6].Value = model.ThQuantity; try
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
        public ThDetailModel GetModel(decimal ThDetailId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ThDetailId, ThId, DingDanDetailId, ThDetailTypeId, Memo, ThDetailAttr, ThQuantity  ");
            strSql.Append("  from CORE.dbo.ThDetail ");
            strSql.Append(" where ThDetailId=@ThDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@ThDetailId", SqlDbType.Decimal)
			};
            parameters[0].Value = ThDetailId;


            ThDetailModel model = new ThDetailModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ThDetailId"].ToString() != "")
                {
                    model.ThDetailId = decimal.Parse(ds.Tables[0].Rows[0]["ThDetailId"].ToString());
                }
                model.ThId = ds.Tables[0].Rows[0]["ThId"].ToString();
                if (ds.Tables[0].Rows[0]["DingDanDetailId"].ToString() != "")
                {
                    model.DingDanDetailId = decimal.Parse(ds.Tables[0].Rows[0]["DingDanDetailId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ThDetailTypeId"].ToString() != "")
                {
                    model.ThDetailTypeId = int.Parse(ds.Tables[0].Rows[0]["ThDetailTypeId"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                model.ThDetailAttr = ds.Tables[0].Rows[0]["ThDetailAttr"].ToString();
                if (ds.Tables[0].Rows[0]["ThQuantity"].ToString() != "")
                {
                    model.ThQuantity = decimal.Parse(ds.Tables[0].Rows[0]["ThQuantity"].ToString());
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
            strSql.Append("delete from CORE.dbo.ThDetail ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.ThDetail  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.ThDetail  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.ThDetailView  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.ThDetail  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

