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
    //About
    public partial class AboutDAL
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
            strSql.Append(" FROM About ");
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
        public bool Add(AboutModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into About(");
            strSql.Append("AboutTitle,AboutContent,OrderId,AboutType");
            strSql.Append(") values (");
            strSql.Append("@AboutTitle,@AboutContent,@OrderId,@AboutType");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@AboutTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AboutContent", SqlDbType.VarChar,-1) ,            
                        new SqlParameter("@OrderId", SqlDbType.Int,4) ,            
                        new SqlParameter("@AboutType", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.AboutTitle;
            parameters[1].Value = model.AboutContent;
            parameters[2].Value = model.OrderId;
            parameters[3].Value = model.AboutType;

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
        public bool Update(AboutModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update About set ");

            strSql.Append(" AboutTitle = @AboutTitle , ");
            strSql.Append(" AboutContent = @AboutContent , ");
            strSql.Append(" OrderId = @OrderId , ");
            strSql.Append(" AboutType = @AboutType  ");
            strSql.Append(" where AboutId=@AboutId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@AboutId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AboutTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@AboutContent", SqlDbType.VarChar,-1) ,            
                        new SqlParameter("@OrderId", SqlDbType.Int,4) ,            
                        new SqlParameter("@AboutType", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.AboutId;
            parameters[1].Value = model.AboutTitle;
            parameters[2].Value = model.AboutContent;
            parameters[3].Value = model.OrderId;
            parameters[4].Value = model.AboutType; try
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
        public AboutModel GetModel(decimal AboutId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AboutId, AboutTitle, AboutContent, OrderId, AboutType  ");
            strSql.Append("  from About ");
            strSql.Append(" where AboutId=@AboutId");
            SqlParameter[] parameters = {
					new SqlParameter("@AboutId", SqlDbType.Decimal)
			};
            parameters[0].Value = AboutId;


            AboutModel model = new AboutModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["AboutId"].ToString() != "")
                {
                    model.AboutId = decimal.Parse(ds.Tables[0].Rows[0]["AboutId"].ToString());
                }
                model.AboutTitle = ds.Tables[0].Rows[0]["AboutTitle"].ToString();
                model.AboutContent = ds.Tables[0].Rows[0]["AboutContent"].ToString();
                if (ds.Tables[0].Rows[0]["OrderId"].ToString() != "")
                {
                    model.OrderId = int.Parse(ds.Tables[0].Rows[0]["OrderId"].ToString());
                }
                model.AboutType = ds.Tables[0].Rows[0]["AboutType"].ToString();

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
            strSql.Append("delete from About ");
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
            strSql.Append(" FROM About  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM About  WITH(NOLOCK) ");
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
            strSql.Append(" FROM About  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

