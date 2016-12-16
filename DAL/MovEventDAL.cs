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
    //MovEvent
    public partial class MovEventDAL
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
            strSql.Append(" FROM MovEvent ");
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
        public bool Add(MovEventModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MovEvent(");
            strSql.Append("MovEventId,MovId,MovEventRePrice,MovEventMemo,MovEventBgTime");
            strSql.Append(") values (");
            strSql.Append("@MovEventId,@MovId,@MovEventRePrice,@MovEventMemo,@MovEventBgTime");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@MovEventId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MovId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MovEventRePrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MovEventMemo", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@MovEventBgTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.MovEventId;
            parameters[1].Value = model.MovId;
            parameters[2].Value = model.MovEventRePrice;
            parameters[3].Value = model.MovEventMemo;
            parameters[4].Value = model.MovEventBgTime;

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
        public bool Update(MovEventModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MovEvent set ");

            strSql.Append(" MovEventId = @MovEventId , ");
            strSql.Append(" MovId = @MovId , ");
            strSql.Append(" MovEventRePrice = @MovEventRePrice , ");
            strSql.Append(" MovEventMemo = @MovEventMemo , ");
            strSql.Append(" MovEventBgTime = @MovEventBgTime  ");
            strSql.Append(" where MovEventId=@MovEventId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@MovEventId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MovId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MovEventRePrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MovEventMemo", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@MovEventBgTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.MovEventId;
            parameters[1].Value = model.MovId;
            parameters[2].Value = model.MovEventRePrice;
            parameters[3].Value = model.MovEventMemo;
            parameters[4].Value = model.MovEventBgTime; try
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
        public MovEventModel GetModel(string MovEventId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MovEventId, MovId, MovEventRePrice, MovEventMemo, MovEventBgTime  ");
            strSql.Append("  from MovEvent ");
            strSql.Append(" where MovEventId=@MovEventId ");
            SqlParameter[] parameters = {
					new SqlParameter("@MovEventId", SqlDbType.VarChar,50)			};
            parameters[0].Value = MovEventId;


            MovEventModel model = new MovEventModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.MovEventId = ds.Tables[0].Rows[0]["MovEventId"].ToString();
                model.MovId = ds.Tables[0].Rows[0]["MovId"].ToString();
                if (ds.Tables[0].Rows[0]["MovEventRePrice"].ToString() != "")
                {
                    model.MovEventRePrice = decimal.Parse(ds.Tables[0].Rows[0]["MovEventRePrice"].ToString());
                }
                model.MovEventMemo = ds.Tables[0].Rows[0]["MovEventMemo"].ToString();
                if (ds.Tables[0].Rows[0]["MovEventBgTime"].ToString() != "")
                {
                    model.MovEventBgTime = DateTime.Parse(ds.Tables[0].Rows[0]["MovEventBgTime"].ToString());
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
            strSql.Append("delete from MovEvent ");
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
            strSql.Append(" FROM MovEvent  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM MovEvent  WITH(NOLOCK) ");
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
            strSql.Append(" FROM MovEvent  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

