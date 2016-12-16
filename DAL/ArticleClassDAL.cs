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
    //ArticleClass
    public partial class ArticleClassDAL
    {

        #region  //数据操作

        /// <summary>
        /// 数据库帮助对象
        /// </summary>
        private MSSQLHelper helper = new MSSQLHelper();
        #endregion


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ArticleClassModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ArticleClass(");
            strSql.Append("ArticleClassName,MerId,OrderNo,ParentArticleClassId,ArticleClassMemo,ArticleClassImgId,Invalid");
            strSql.Append(") values (");
            strSql.Append("@ArticleClassName,@MerId,@OrderNo,@ParentArticleClassId,@ArticleClassMemo,@ArticleClassImgId,@Invalid");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@ArticleClassName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ParentArticleClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ArticleClassMemo", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ArticleClassImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1)             
              
            };

            parameters[0].Value = model.ArticleClassName;
            parameters[1].Value = model.MerId;
            parameters[2].Value = model.OrderNo;
            parameters[3].Value = model.ParentArticleClassId;
            parameters[4].Value = model.ArticleClassMemo;
            parameters[5].Value = model.ArticleClassImgId;
            parameters[6].Value = model.Invalid;

            bool result = false;
            try
            {
                model.ArticleClassId = int.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "ArticleClassId", parameters));
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
        public bool Update(ArticleClassModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ArticleClass set ");

            strSql.Append(" ArticleClassName = @ArticleClassName , ");
            strSql.Append(" MerId = @MerId , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" ParentArticleClassId = @ParentArticleClassId , ");
            strSql.Append(" ArticleClassMemo = @ArticleClassMemo , ");
            strSql.Append(" ArticleClassImgId = @ArticleClassImgId , ");
            strSql.Append(" Invalid = @Invalid  ");
            strSql.Append(" where ArticleClassId=@ArticleClassId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ArticleClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ArticleClassName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ParentArticleClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ArticleClassMemo", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ArticleClassImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1)             
              
            };

            parameters[0].Value = model.ArticleClassId;
            parameters[1].Value = model.ArticleClassName;
            parameters[2].Value = model.MerId;
            parameters[3].Value = model.OrderNo;
            parameters[4].Value = model.ParentArticleClassId;
            parameters[5].Value = model.ArticleClassMemo;
            parameters[6].Value = model.ArticleClassImgId;
            parameters[7].Value = model.Invalid; try
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
        public ArticleClassModel GetModel(int ArticleClassId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ArticleClassId, ArticleClassName, MerId, OrderNo, ParentArticleClassId, ArticleClassMemo, ArticleClassImgId, Invalid  ");
            strSql.Append("  from ArticleClass ");
            strSql.Append(" where ArticleClassId=@ArticleClassId");
            SqlParameter[] parameters = {
					new SqlParameter("@ArticleClassId", SqlDbType.Int,4)
			};
            parameters[0].Value = ArticleClassId;


            ArticleClassModel model = new ArticleClassModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ArticleClassId"].ToString() != "")
                {
                    model.ArticleClassId = int.Parse(ds.Tables[0].Rows[0]["ArticleClassId"].ToString());
                }
                model.ArticleClassName = ds.Tables[0].Rows[0]["ArticleClassName"].ToString();
                if (ds.Tables[0].Rows[0]["MerId"].ToString() != "")
                {
                    model.MerId = decimal.Parse(ds.Tables[0].Rows[0]["MerId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParentArticleClassId"].ToString() != "")
                {
                    model.ParentArticleClassId = int.Parse(ds.Tables[0].Rows[0]["ParentArticleClassId"].ToString());
                }
                model.ArticleClassMemo = ds.Tables[0].Rows[0]["ArticleClassMemo"].ToString();
                model.ArticleClassImgId = ds.Tables[0].Rows[0]["ArticleClassImgId"].ToString();
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

                return model;
            }
            else
            {
                return null;
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
            strSql.Append("delete from ArticleClass WITH(NOLOCK)  ");
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
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 * ");
            strSql.Append(" FROM ArticleClass WITH(NOLOCK)  ");
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
            strSql.Append(" FROM ArticleClass  WITH(NOLOCK) ");
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
            strSql.Append(" FROM ArticleClass ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

