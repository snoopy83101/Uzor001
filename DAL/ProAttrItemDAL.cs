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
    //默认查询项
    public partial class ProAttrItemDAL
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
            strSql.Append(" FROM  CORE.dbo.ProAttrItem ");
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
        public bool Add(ProAttrItemModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.ProAttrItem (");
            strSql.Append("ProAttrItemId,ProAttrId,BgIntKey,EndIntKey,StrKey,OrderNo");
            strSql.Append(") values (");
            strSql.Append("@ProAttrItemId,@ProAttrId,@BgIntKey,@EndIntKey,@StrKey,@OrderNo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ProAttrItemId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ProAttrId", SqlDbType.NChar,10) ,            
                        new SqlParameter("@BgIntKey", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@EndIntKey", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@StrKey", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.ProAttrItemId;
            parameters[1].Value = model.ProAttrId;
            parameters[2].Value = model.BgIntKey;
            parameters[3].Value = model.EndIntKey;
            parameters[4].Value = model.StrKey;
            parameters[5].Value = model.OrderNo;

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
        public bool Update(ProAttrItemModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.ProAttrItem set ");

            strSql.Append(" ProAttrItemId = @ProAttrItemId , ");
            strSql.Append(" ProAttrId = @ProAttrId , ");
            strSql.Append(" BgIntKey = @BgIntKey , ");
            strSql.Append(" EndIntKey = @EndIntKey , ");
            strSql.Append(" StrKey = @StrKey , ");
            strSql.Append(" OrderNo = @OrderNo  ");
            strSql.Append(" where ProAttrItemId=@ProAttrItemId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ProAttrItemId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ProAttrId", SqlDbType.NChar,10) ,            
                        new SqlParameter("@BgIntKey", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@EndIntKey", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@StrKey", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.ProAttrItemId;
            parameters[1].Value = model.ProAttrId;
            parameters[2].Value = model.BgIntKey;
            parameters[3].Value = model.EndIntKey;
            parameters[4].Value = model.StrKey;
            parameters[5].Value = model.OrderNo; try
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
        public ProAttrItemModel GetModel(decimal ProAttrItemId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ProAttrItemId, ProAttrId, BgIntKey, EndIntKey, StrKey, OrderNo  ");
            strSql.Append("  from CORE.dbo.ProAttrItem ");
            strSql.Append(" where ProAttrItemId=@ProAttrItemId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProAttrItemId", SqlDbType.Decimal,9)			};
            parameters[0].Value = ProAttrItemId;


            ProAttrItemModel model = new ProAttrItemModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ProAttrItemId"].ToString() != "")
                {
                    model.ProAttrItemId = decimal.Parse(ds.Tables[0].Rows[0]["ProAttrItemId"].ToString());
                }
                model.ProAttrId = ds.Tables[0].Rows[0]["ProAttrId"].ToString();
                if (ds.Tables[0].Rows[0]["BgIntKey"].ToString() != "")
                {
                    model.BgIntKey = decimal.Parse(ds.Tables[0].Rows[0]["BgIntKey"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndIntKey"].ToString() != "")
                {
                    model.EndIntKey = decimal.Parse(ds.Tables[0].Rows[0]["EndIntKey"].ToString());
                }
                model.StrKey = ds.Tables[0].Rows[0]["StrKey"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
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
            strSql.Append("delete from CORE.dbo.ProAttrItem ");
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
            strSql.Append(" FROM CORE.dbo.ProAttrItem  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.ProAttrItem  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.ProAttrItem  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

