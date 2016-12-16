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
    //AuthorInfo
    public partial class AuthorInfoDAL
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
            strSql.Append(" FROM  CORE.dbo.AuthorInfo ");
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
        public bool Add(AuthorInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.AuthorInfo (");
            strSql.Append("AuthorId,AuthorName,InputCode,AuthorMemo,MerchantId,CreateTime,PicImgId,Invalid");
            strSql.Append(") values (");
            strSql.Append("@AuthorId,@AuthorName,@InputCode,@AuthorMemo,@MerchantId,@CreateTime,@PicImgId,@Invalid");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@AuthorId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AuthorName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@InputCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AuthorMemo", SqlDbType.VarChar,1000) ,            
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@PicImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1)             
              
            };

            parameters[0].Value = model.AuthorId;
            parameters[1].Value = model.AuthorName;
            parameters[2].Value = model.InputCode;
            parameters[3].Value = model.AuthorMemo;
            parameters[4].Value = model.MerchantId;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.PicImgId;
            parameters[7].Value = model.Invalid;

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
        public bool Update(AuthorInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.AuthorInfo set ");

            strSql.Append(" AuthorId = @AuthorId , ");
            strSql.Append(" AuthorName = @AuthorName , ");
            strSql.Append(" InputCode = @InputCode , ");
            strSql.Append(" AuthorMemo = @AuthorMemo , ");
            strSql.Append(" MerchantId = @MerchantId , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" PicImgId = @PicImgId , ");
            strSql.Append(" Invalid = @Invalid  ");
            strSql.Append(" where AuthorId=@AuthorId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@AuthorId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AuthorName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@InputCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AuthorMemo", SqlDbType.VarChar,1000) ,            
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@PicImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1)             
              
            };

            parameters[0].Value = model.AuthorId;
            parameters[1].Value = model.AuthorName;
            parameters[2].Value = model.InputCode;
            parameters[3].Value = model.AuthorMemo;
            parameters[4].Value = model.MerchantId;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.PicImgId;
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
        public AuthorInfoModel GetModel(string AuthorId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AuthorId, AuthorName, InputCode, AuthorMemo, MerchantId, CreateTime, PicImgId, Invalid  ");
            strSql.Append("  from CORE.dbo.AuthorInfo ");
            strSql.Append(" where AuthorId=@AuthorId ");
            SqlParameter[] parameters = {
					new SqlParameter("@AuthorId", SqlDbType.VarChar,50)			};
            parameters[0].Value = AuthorId;


            AuthorInfoModel model = new AuthorInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.AuthorId = ds.Tables[0].Rows[0]["AuthorId"].ToString();
                model.AuthorName = ds.Tables[0].Rows[0]["AuthorName"].ToString();
                model.InputCode = ds.Tables[0].Rows[0]["InputCode"].ToString();
                model.AuthorMemo = ds.Tables[0].Rows[0]["AuthorMemo"].ToString();
                if (ds.Tables[0].Rows[0]["MerchantId"].ToString() != "")
                {
                    model.MerchantId = decimal.Parse(ds.Tables[0].Rows[0]["MerchantId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.PicImgId = ds.Tables[0].Rows[0]["PicImgId"].ToString();
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
            strSql.Append("delete from CORE.dbo.AuthorInfo ");
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
            strSql.Append(" FROM CORE.dbo.AuthorView  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.AuthorView  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return helper.ExecSqlReDs(strSql.ToString());
        }

        public DataSet GetAuthorInfo(string AuthorId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM CORE.dbo.AuthorView  WITH(NOLOCK) where AuthorId='"+AuthorId+"' ");
            DataSet ds=helper.ExecSqlReDs(strSql.ToString());
            ds.Tables[0].TableName="Info";
            return ds;
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
            strSql.Append(" FROM CORE.dbo.AuthorInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

