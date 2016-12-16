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
    //PeiSongTypeVsProductClass
    public partial class PeiSongTypeVsProductClassDAL
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
            strSql.Append(" FROM  CORE.dbo.PeiSongTypeVsProductClass ");
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
        public bool Add(PeiSongTypeVsProductClassModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.PeiSongTypeVsProductClass (");
            strSql.Append("PeiSongTypeId,ProductClassId,VsType");
            strSql.Append(") values (");
            strSql.Append("@PeiSongTypeId,@ProductClassId,@VsType");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@PeiSongTypeId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ProductClassId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@VsType", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.PeiSongTypeId;
            parameters[1].Value = model.ProductClassId;
            parameters[2].Value = model.VsType;

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
        public bool Update(PeiSongTypeVsProductClassModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.PeiSongTypeVsProductClass set ");

            strSql.Append(" PeiSongTypeId = @PeiSongTypeId , ");
            strSql.Append(" ProductClassId = @ProductClassId , ");
            strSql.Append(" VsType = @VsType  ");
            strSql.Append(" where PeiSongTypeId=@PeiSongTypeId and ProductClassId=@ProductClassId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@PeiSongTypeId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ProductClassId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@VsType", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.PeiSongTypeId;
            parameters[1].Value = model.ProductClassId;
            parameters[2].Value = model.VsType; try
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
        public PeiSongTypeVsProductClassModel GetModel(decimal PeiSongTypeId, decimal ProductClassId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PeiSongTypeId, ProductClassId, VsType  ");
            strSql.Append("  from CORE.dbo.PeiSongTypeVsProductClass ");
            strSql.Append(" where PeiSongTypeId=@PeiSongTypeId and ProductClassId=@ProductClassId ");
            SqlParameter[] parameters = {
					new SqlParameter("@PeiSongTypeId", SqlDbType.Decimal,9),
					new SqlParameter("@ProductClassId", SqlDbType.Decimal,9)			};
            parameters[0].Value = PeiSongTypeId;
            parameters[1].Value = ProductClassId;


            PeiSongTypeVsProductClassModel model = new PeiSongTypeVsProductClassModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PeiSongTypeId"].ToString() != "")
                {
                    model.PeiSongTypeId = decimal.Parse(ds.Tables[0].Rows[0]["PeiSongTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProductClassId"].ToString() != "")
                {
                    model.ProductClassId = decimal.Parse(ds.Tables[0].Rows[0]["ProductClassId"].ToString());
                }
                model.VsType = ds.Tables[0].Rows[0]["VsType"].ToString();

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
            strSql.Append("delete from CORE.dbo.PeiSongTypeVsProductClass ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.PeiSongTypeVsProductClass  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.PeiSongTypeVsProductClass  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.PeiSongTypeVsProductClass  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.PeiSongTypeVsProductClass  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

