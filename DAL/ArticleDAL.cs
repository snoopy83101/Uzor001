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
    //Article
    public partial class ArticleDAL
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
        public bool Add(ArticleModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Article(");
            strSql.Append("ArticleId,Author,ArticleImgId,Memo,Invalid,ArticleTitle,ArticleSummary,ArticleContent,ArticleSource,CreateTime,CreateUser,ArticleTypeId,ArticleClassId");
            strSql.Append(") values (");
            strSql.Append("@ArticleId,@Author,@ArticleImgId,@Memo,@Invalid,@ArticleTitle,@ArticleSummary,@ArticleContent,@ArticleSource,@CreateTime,@CreateUser,@ArticleTypeId,@ArticleClassId");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ArticleId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Author", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ArticleImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@ArticleTitle", SqlDbType.VarChar,150) ,            
                        new SqlParameter("@ArticleSummary", SqlDbType.NVarChar,800) ,            
                        new SqlParameter("@ArticleContent", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@ArticleSource", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ArticleTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ArticleClassId", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.ArticleId;
            parameters[1].Value = model.Author;
            parameters[2].Value = model.ArticleImgId;
            parameters[3].Value = model.Memo;
            parameters[4].Value = model.Invalid;
            parameters[5].Value = model.ArticleTitle;
            parameters[6].Value = model.ArticleSummary;
            parameters[7].Value = model.ArticleContent;
            parameters[8].Value = model.ArticleSource;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.CreateUser;
            parameters[11].Value = model.ArticleTypeId;
            parameters[12].Value = model.ArticleClassId;

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
        public bool Update(ArticleModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Article set ");

            strSql.Append(" ArticleId = @ArticleId , ");
            strSql.Append(" Author = @Author , ");
            strSql.Append(" ArticleImgId = @ArticleImgId , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" ArticleTitle = @ArticleTitle , ");
            strSql.Append(" ArticleSummary = @ArticleSummary , ");
            strSql.Append(" ArticleContent = @ArticleContent , ");
            strSql.Append(" ArticleSource = @ArticleSource , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" CreateUser = @CreateUser , ");
            strSql.Append(" ArticleTypeId = @ArticleTypeId , ");
            strSql.Append(" ArticleClassId = @ArticleClassId  ");
            strSql.Append(" where ArticleId=@ArticleId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ArticleId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Author", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ArticleImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@ArticleTitle", SqlDbType.VarChar,150) ,            
                        new SqlParameter("@ArticleSummary", SqlDbType.NVarChar,800) ,            
                        new SqlParameter("@ArticleContent", SqlDbType.NVarChar,-1) ,            
                        new SqlParameter("@ArticleSource", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ArticleTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ArticleClassId", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.ArticleId;
            parameters[1].Value = model.Author;
            parameters[2].Value = model.ArticleImgId;
            parameters[3].Value = model.Memo;
            parameters[4].Value = model.Invalid;
            parameters[5].Value = model.ArticleTitle;
            parameters[6].Value = model.ArticleSummary;
            parameters[7].Value = model.ArticleContent;
            parameters[8].Value = model.ArticleSource;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.CreateUser;
            parameters[11].Value = model.ArticleTypeId;
            parameters[12].Value = model.ArticleClassId; try
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
        public ArticleModel GetModel(string ArticleId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ArticleId, Author, ArticleImgId, Memo, Invalid, ArticleTitle, ArticleSummary, ArticleContent, ArticleSource, CreateTime, CreateUser, ArticleTypeId, ArticleClassId  ");
            strSql.Append("  from Article WITH(NOLOCK)  ");
            strSql.Append(" where ArticleId=@ArticleId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ArticleId", SqlDbType.VarChar,50)			};
            parameters[0].Value = ArticleId;


            ArticleModel model = new ArticleModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.ArticleId = ds.Tables[0].Rows[0]["ArticleId"].ToString();
                model.Author = ds.Tables[0].Rows[0]["Author"].ToString();
                model.ArticleImgId = ds.Tables[0].Rows[0]["ArticleImgId"].ToString();
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
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
                model.ArticleTitle = ds.Tables[0].Rows[0]["ArticleTitle"].ToString();
                model.ArticleSummary = ds.Tables[0].Rows[0]["ArticleSummary"].ToString();
                model.ArticleContent = ds.Tables[0].Rows[0]["ArticleContent"].ToString();
                model.ArticleSource = ds.Tables[0].Rows[0]["ArticleSource"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.CreateUser = ds.Tables[0].Rows[0]["CreateUser"].ToString();
                if (ds.Tables[0].Rows[0]["ArticleTypeId"].ToString() != "")
                {
                    model.ArticleTypeId = int.Parse(ds.Tables[0].Rows[0]["ArticleTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ArticleClassId"].ToString() != "")
                {
                    model.ArticleClassId = int.Parse(ds.Tables[0].Rows[0]["ArticleClassId"].ToString());
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
            strSql.Append("delete from Article ");
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
            strSql.Append("select top 3000  ");
            strSql.Append(@" ArticleId ,
        ArticleTitle ,
        ArticleSummary ,
        ArticleSource ,
        CreateTime ,
        CreateUser ,
        ArticleTypeId ,
        ArticleClassId ,
        Author ,
        ArticleImgId ,
        Memo ,
        Invalid ,
        RecommendLv ,
        ArticleClassName ,
        MerId ,
        ArticleImgUrl ");
            strSql.Append(" FROM ArticleView WITH(NOLOCK)  ");
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



        public DataSet GetArticleDataById(string ArticleId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @MerId AS VARCHAR(50) = ( SELECT TOP 1  MerId  FROM      dbo.ArticleView WHERE     ArticleId = '"+ArticleId+"')");

            strSql.Append("select * ");
            strSql.Append(" FROM ArticleView  WITH(NOLOCK) where ArticleId='" + ArticleId + "' ");
            strSql.Append(" select * from dbo.ArticleClass WITH(NOLOCK)  where MerId=@MerId ");
            strSql.Append(" select * from dbo.ArticleType  WITH(NOLOCK) where MerId=@MerId ");
            strSql.Append(" SELECT i.* FROM dbo.ArticleVsImage avi WITH(NOLOCK)  LEFT JOIN dbo.ImageInfo i WITH(NOLOCK)  ON avi.ImgId = i.ImgId WHERE AricleId='" + ArticleId + "' ");
            return helper.ExecSqlReDs(strSql.ToString());

        }

        public DataSet GetArticleDataById(string ArticleId,decimal MerId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @MerId AS VARCHAR(50) = '"+MerId+"'");

            strSql.Append("select * ");
            strSql.Append(" FROM ArticleView  WITH(NOLOCK)  where ArticleId='" + ArticleId + "' ");
            strSql.Append(" select * from dbo.ArticleClass  WITH(NOLOCK)  where MerId=@MerId and Invalid=0 and ParentArticleClassId=0 ");
            strSql.Append(" select * from dbo.ArticleType  WITH(NOLOCK)  where MerId=@MerId ");
            strSql.Append(" SELECT i.* FROM dbo.ArticleVsImage avi  WITH(NOLOCK)  LEFT JOIN dbo.ImageInfo i  WITH(NOLOCK)  ON avi.ImgId = i.ImgId WHERE AricleId='" + ArticleId + "' ");
            return helper.ExecSqlReDs(strSql.ToString());

        }

        public DataSet GetArticleMerDataById(string ArticleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DECLARE @MerId AS VARCHAR(50) = ( SELECT TOP 1  MerId  FROM      dbo.ArticleView  WITH(NOLOCK)  WHERE     ArticleId = '" + ArticleId + "')");

            strSql.Append("select * ");
            strSql.Append(" FROM ArticleView  WITH(NOLOCK)  where ArticleId='" + ArticleId + "' ");
            strSql.Append(" SELECT * FROM dbo.MerchantView  WITH(NOLOCK)  WHERE MerchantId=@MerId  ");
            return helper.ExecSqlReDs(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ArticleView  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM Article ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

