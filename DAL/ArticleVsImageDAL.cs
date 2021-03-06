﻿using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Model;
using DBTools;
using Common;
namespace DAL
{
    //ArticleVsImage
    public partial class ArticleVsImageDAL
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
        public bool Add(ArticleVsImageModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ArticleVsImage(");
            strSql.Append("AricleId,ImgId,VsType");
            strSql.Append(") values (");
            strSql.Append("@AricleId,@ImgId,@VsType");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@AricleId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@VsType", SqlDbType.VarChar,10)             
              
            };

            parameters[0].Value = model.AricleId;
            parameters[1].Value = model.ImgId;
            parameters[2].Value = model.VsType;

            bool result = false;
            strSql.Append(Common.SqlStrHelper.BindImgSqlStr(model.ImgId));  //顺便绑定
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
        public bool Update(ArticleVsImageModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ArticleVsImage set ");

            strSql.Append(" AricleId = @AricleId , ");
            strSql.Append(" ImgId = @ImgId , ");
            strSql.Append(" VsType = @VsType  ");
            strSql.Append(" where AricleId=@AricleId and ImgId=@ImgId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@AricleId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@VsType", SqlDbType.VarChar,10)             
              
            };

            parameters[0].Value = model.AricleId;
            parameters[1].Value = model.ImgId;
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
        public ArticleVsImageModel GetModel(string AricleId, string ImgId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AricleId, ImgId, VsType  ");
            strSql.Append("  from ArticleVsImage ");
            strSql.Append(" where AricleId=@AricleId and ImgId=@ImgId ");
            SqlParameter[] parameters = {
					new SqlParameter("@AricleId", SqlDbType.VarChar,50),
					new SqlParameter("@ImgId", SqlDbType.VarChar,50)			};
            parameters[0].Value = AricleId;
            parameters[1].Value = ImgId;


            ArticleVsImageModel model = new ArticleVsImageModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.AricleId = ds.Tables[0].Rows[0]["AricleId"].ToString();
                model.ImgId = ds.Tables[0].Rows[0]["ImgId"].ToString();
                model.VsType = ds.Tables[0].Rows[0]["VsType"].ToString();

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
            strSql.Append("delete from ArticleVsImage ");
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
            strSql.Append(" FROM ArticleVsImage ");
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
            strSql.Append(" FROM ArticleVsImage ");
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
            strSql.Append(" FROM ArticleVsImage ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

