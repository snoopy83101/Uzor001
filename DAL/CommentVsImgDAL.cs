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
    //CommentVsImg
    public partial class CommentVsImgDAL
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
            strSql.Append(" FROM CommentVsImg  WITH(NOLOCK)  ");
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
        public bool Add(CommentVsImgModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CommentVsImg(");
            strSql.Append("CommentId,ImgId,vsType");
            strSql.Append(") values (");
            strSql.Append("@CommentId,@ImgId,@vsType");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@CommentId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@vsType", SqlDbType.VarChar,10)             
              
            };

            parameters[0].Value = model.CommentId;
            parameters[1].Value = model.ImgId;
            parameters[2].Value = model.vsType;

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
        public bool Update(CommentVsImgModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CommentVsImg set ");

            strSql.Append(" CommentId = @CommentId , ");
            strSql.Append(" ImgId = @ImgId , ");
            strSql.Append(" vsType = @vsType  ");
            strSql.Append(" where CommentId=@CommentId and ImgId=@ImgId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@CommentId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@vsType", SqlDbType.VarChar,10)             
              
            };

            parameters[0].Value = model.CommentId;
            parameters[1].Value = model.ImgId;
            parameters[2].Value = model.vsType; try
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
        public CommentVsImgModel GetModel(decimal CommentId, string ImgId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CommentId, ImgId, vsType  ");
            strSql.Append("  from CommentVsImg WITH(NOLOCK)  ");
            strSql.Append(" where CommentId=@CommentId and ImgId=@ImgId ");
            SqlParameter[] parameters = {
					new SqlParameter("@CommentId", SqlDbType.Decimal,9),
					new SqlParameter("@ImgId", SqlDbType.VarChar,50)			};
            parameters[0].Value = CommentId;
            parameters[1].Value = ImgId;


            CommentVsImgModel model = new CommentVsImgModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CommentId"].ToString() != "")
                {
                    model.CommentId = decimal.Parse(ds.Tables[0].Rows[0]["CommentId"].ToString());
                }
                model.ImgId = ds.Tables[0].Rows[0]["ImgId"].ToString();
                model.vsType = ds.Tables[0].Rows[0]["vsType"].ToString();

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
            strSql.Append("delete from CommentVsImg  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CommentVsImg WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CommentVsImg WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CommentVsImg ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

