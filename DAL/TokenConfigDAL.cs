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
    //TokenConfig
    public partial class TokenConfigDAL
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
            strSql.Append(" FROM TokenConfig ");
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
        public bool Add(TokenConfigModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TokenConfig(");
            strSql.Append("AccessToken,CreateTime,Memo,Gzh,TokenType");
            strSql.Append(") values (");
            strSql.Append("@AccessToken,@CreateTime,@Memo,@Gzh,@TokenType");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@AccessToken", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Gzh", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@TokenType", SqlDbType.VarChar,10)             
              
            };

            parameters[0].Value = model.AccessToken;
            parameters[1].Value = model.CreateTime;
            parameters[2].Value = model.Memo;
            parameters[3].Value = model.Gzh;
            parameters[4].Value = model.TokenType;

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
        public bool Update(TokenConfigModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TokenConfig set ");

            strSql.Append(" AccessToken = @AccessToken , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" Gzh = @Gzh , ");
            strSql.Append(" TokenType = @TokenType  ");
            strSql.Append(" where AccessToken=@AccessToken  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@AccessToken", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Gzh", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@TokenType", SqlDbType.VarChar,10)             
              
            };

            parameters[0].Value = model.AccessToken;
            parameters[1].Value = model.CreateTime;
            parameters[2].Value = model.Memo;
            parameters[3].Value = model.Gzh;
            parameters[4].Value = model.TokenType; try
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
        public TokenConfigModel GetModel(string AccessToken)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AccessToken, CreateTime, Memo, Gzh, TokenType  ");
            strSql.Append("  from TokenConfig ");
            strSql.Append(" where AccessToken=@AccessToken ");
            SqlParameter[] parameters = {
					new SqlParameter("@AccessToken", SqlDbType.VarChar,100)			};
            parameters[0].Value = AccessToken;


            TokenConfigModel model = new TokenConfigModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.AccessToken = ds.Tables[0].Rows[0]["AccessToken"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                model.Gzh = ds.Tables[0].Rows[0]["Gzh"].ToString();
                model.TokenType = ds.Tables[0].Rows[0]["TokenType"].ToString();

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
            strSql.Append("delete from TokenConfig ");
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
            strSql.Append(" FROM TokenConfig  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM TokenConfig  WITH(NOLOCK) ");
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
            strSql.Append(" FROM TokenConfig  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

