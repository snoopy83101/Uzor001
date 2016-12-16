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
    //WxPtInfo
    public partial class WxPtInfoDAL
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
            strSql.Append(" FROM  CORE.dbo.WxPtInfo ");
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
        public bool Add(WxPtInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.WxPtInfo (");
            strSql.Append("MerId,WxPtTypeId,YuanShiId,ArticleUrl,ProUrl,TieZiUrl,Invalid,WxPtCode,AccessToken,AccessTokenCreateTime,WxPtName,ReUrl,ReToken,AppId,AppSecret");
            strSql.Append(") values (");
            strSql.Append("@MerId,@WxPtTypeId,@YuanShiId,@ArticleUrl,@ProUrl,@TieZiUrl,@Invalid,@WxPtCode,@AccessToken,@AccessTokenCreateTime,@WxPtName,@ReUrl,@ReToken,@AppId,@AppSecret");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@MerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@WxPtTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@YuanShiId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ArticleUrl", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@ProUrl", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@TieZiUrl", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@WxPtCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AccessToken", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@AccessTokenCreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@WxPtName", SqlDbType.NChar,10) ,            
                        new SqlParameter("@ReUrl", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@ReToken", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AppId", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@AppSecret", SqlDbType.VarChar,100)             
              
            };

            parameters[0].Value = model.MerId;
            parameters[1].Value = model.WxPtTypeId;
            parameters[2].Value = model.YuanShiId;
            parameters[3].Value = model.ArticleUrl;
            parameters[4].Value = model.ProUrl;
            parameters[5].Value = model.TieZiUrl;
            parameters[6].Value = model.Invalid;
            parameters[7].Value = model.WxPtCode;
            parameters[8].Value = model.AccessToken;
            parameters[9].Value = model.AccessTokenCreateTime;
            parameters[10].Value = model.WxPtName;
            parameters[11].Value = model.ReUrl;
            parameters[12].Value = model.ReToken;
            parameters[13].Value = model.AppId;
            parameters[14].Value = model.AppSecret;

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
        public bool Update(WxPtInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.WxPtInfo set ");

            strSql.Append(" MerId = @MerId , ");
            strSql.Append(" WxPtTypeId = @WxPtTypeId , ");
            strSql.Append(" YuanShiId = @YuanShiId , ");
            strSql.Append(" ArticleUrl = @ArticleUrl , ");
            strSql.Append(" ProUrl = @ProUrl , ");
            strSql.Append(" TieZiUrl = @TieZiUrl , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" WxPtCode = @WxPtCode , ");
            strSql.Append(" AccessToken = @AccessToken , ");
            strSql.Append(" AccessTokenCreateTime = @AccessTokenCreateTime , ");
            strSql.Append(" WxPtName = @WxPtName , ");
            strSql.Append(" ReUrl = @ReUrl , ");
            strSql.Append(" ReToken = @ReToken , ");
            strSql.Append(" AppId = @AppId , ");
            strSql.Append(" AppSecret = @AppSecret  ");
            strSql.Append(" where WxPtId=@WxPtId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@WxPtId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@WxPtTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@YuanShiId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ArticleUrl", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@ProUrl", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@TieZiUrl", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@WxPtCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AccessToken", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@AccessTokenCreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@WxPtName", SqlDbType.NChar,10) ,            
                        new SqlParameter("@ReUrl", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@ReToken", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AppId", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@AppSecret", SqlDbType.VarChar,100)             
              
            };

            parameters[0].Value = model.WxPtId;
            parameters[1].Value = model.MerId;
            parameters[2].Value = model.WxPtTypeId;
            parameters[3].Value = model.YuanShiId;
            parameters[4].Value = model.ArticleUrl;
            parameters[5].Value = model.ProUrl;
            parameters[6].Value = model.TieZiUrl;
            parameters[7].Value = model.Invalid;
            parameters[8].Value = model.WxPtCode;
            parameters[9].Value = model.AccessToken;
            parameters[10].Value = model.AccessTokenCreateTime;
            parameters[11].Value = model.WxPtName;
            parameters[12].Value = model.ReUrl;
            parameters[13].Value = model.ReToken;
            parameters[14].Value = model.AppId;
            parameters[15].Value = model.AppSecret; try
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
        public WxPtInfoModel GetModel(decimal WxPtId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select WxPtId, MerId, WxPtTypeId, YuanShiId, ArticleUrl, ProUrl, TieZiUrl, Invalid, WxPtCode, AccessToken, AccessTokenCreateTime, WxPtName, ReUrl, ReToken, AppId, AppSecret  ");
            strSql.Append("  from CORE.dbo.WxPtInfo ");
            strSql.Append(" where WxPtId=@WxPtId");
            SqlParameter[] parameters = {
					new SqlParameter("@WxPtId", SqlDbType.Decimal)
			};
            parameters[0].Value = WxPtId;


            WxPtInfoModel model = new WxPtInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["WxPtId"].ToString() != "")
                {
                    model.WxPtId = decimal.Parse(ds.Tables[0].Rows[0]["WxPtId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MerId"].ToString() != "")
                {
                    model.MerId = decimal.Parse(ds.Tables[0].Rows[0]["MerId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WxPtTypeId"].ToString() != "")
                {
                    model.WxPtTypeId = int.Parse(ds.Tables[0].Rows[0]["WxPtTypeId"].ToString());
                }
                model.YuanShiId = ds.Tables[0].Rows[0]["YuanShiId"].ToString();
                model.ArticleUrl = ds.Tables[0].Rows[0]["ArticleUrl"].ToString();
                model.ProUrl = ds.Tables[0].Rows[0]["ProUrl"].ToString();
                model.TieZiUrl = ds.Tables[0].Rows[0]["TieZiUrl"].ToString();
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
                model.WxPtCode = ds.Tables[0].Rows[0]["WxPtCode"].ToString();
                model.AccessToken = ds.Tables[0].Rows[0]["AccessToken"].ToString();
                if (ds.Tables[0].Rows[0]["AccessTokenCreateTime"].ToString() != "")
                {
                    model.AccessTokenCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["AccessTokenCreateTime"].ToString());
                }
                model.WxPtName = ds.Tables[0].Rows[0]["WxPtName"].ToString();
                model.ReUrl = ds.Tables[0].Rows[0]["ReUrl"].ToString();
                model.ReToken = ds.Tables[0].Rows[0]["ReToken"].ToString();
                model.AppId = ds.Tables[0].Rows[0]["AppId"].ToString();
                model.AppSecret = ds.Tables[0].Rows[0]["AppSecret"].ToString();

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
            strSql.Append("delete from CORE.dbo.WxPtInfo ");
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
            strSql.Append(" FROM CORE.dbo.WxPtView  WITH(NOLOCK)  ");
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

        public DataSet GetInfoData(decimal WxPtId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM CORE.dbo.WxPtInfo  WITH(NOLOCK) where WxPtId='"+WxPtId+"' ");
  
            return helper.ExecSqlReDs(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM CORE.dbo.WxPtInfo  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.WxPtInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

