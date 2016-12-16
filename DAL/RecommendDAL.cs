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
    //Recommend
    public partial class RecommendDAL
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
            strSql.Append(" FROM Recommend ");
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
        public bool Add(RecommendModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Recommend(");
            strSql.Append("RecommendId,RecommendMemo,ReKey,RecommendType,BgTime,EndTime,Invalid");
            strSql.Append(") values (");
            strSql.Append("@RecommendId,@RecommendMemo,@ReKey,@RecommendType,@BgTime,@EndTime,@Invalid");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@RecommendId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@RecommendMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@RecommendType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BgTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@EndTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1)             
              
            };

            parameters[0].Value = model.RecommendId;
            parameters[1].Value = model.RecommendMemo;
            parameters[2].Value = model.ReKey;
            parameters[3].Value = model.RecommendType;
            parameters[4].Value = model.BgTime;
            parameters[5].Value = model.EndTime;
            parameters[6].Value = model.Invalid;

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
        public bool Update(RecommendModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Recommend set ");

            strSql.Append(" RecommendId = @RecommendId , ");
            strSql.Append(" RecommendMemo = @RecommendMemo , ");
            strSql.Append(" ReKey = @ReKey , ");
            strSql.Append(" RecommendType = @RecommendType , ");
            strSql.Append(" BgTime = @BgTime , ");
            strSql.Append(" EndTime = @EndTime , ");
            strSql.Append(" Invalid = @Invalid  ");
            strSql.Append(" where RecommendId=@RecommendId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@RecommendId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@RecommendMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@RecommendType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BgTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@EndTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1)             
              
            };

            parameters[0].Value = model.RecommendId;
            parameters[1].Value = model.RecommendMemo;
            parameters[2].Value = model.ReKey;
            parameters[3].Value = model.RecommendType;
            parameters[4].Value = model.BgTime;
            parameters[5].Value = model.EndTime;
            parameters[6].Value = model.Invalid; try
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
        public RecommendModel GetModel(string RecommendId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select RecommendId, RecommendMemo, ReKey, RecommendType, BgTime, EndTime, Invalid  ");
            strSql.Append("  from Recommend ");
            strSql.Append(" where RecommendId=@RecommendId ");
            SqlParameter[] parameters = {
					new SqlParameter("@RecommendId", SqlDbType.VarChar,50)			};
            parameters[0].Value = RecommendId;


            RecommendModel model = new RecommendModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.RecommendId = ds.Tables[0].Rows[0]["RecommendId"].ToString();
                model.RecommendMemo = ds.Tables[0].Rows[0]["RecommendMemo"].ToString();
                model.ReKey = ds.Tables[0].Rows[0]["ReKey"].ToString();
                model.RecommendType = ds.Tables[0].Rows[0]["RecommendType"].ToString();
                if (ds.Tables[0].Rows[0]["BgTime"].ToString() != "")
                {
                    model.BgTime = DateTime.Parse(ds.Tables[0].Rows[0]["BgTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndTime"].ToString() != "")
                {
                    model.EndTime = DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
                }
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
            strSql.Append("delete from Recommend ");
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
            strSql.Append(" FROM Recommend  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM Recommend  WITH(NOLOCK) ");
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
            strSql.Append(" FROM Recommend  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

