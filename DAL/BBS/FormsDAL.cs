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
    //Forms
    public partial class FormsDAL
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
            strSql.Append(" FROM Forms ");
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
        public bool Add(FormsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Forms(");
            strSql.Append("ForumId,ForumName,ForumMemo,OrderNo,ForumType,ForumClass");
            strSql.Append(") values (");
            strSql.Append("@ForumId,@ForumName,@ForumMemo,@OrderNo,@ForumType,@ForumClass");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ForumId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ForumName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ForumMemo", SqlDbType.VarChar,1000) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ForumType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ForumClass", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.ForumId;
            parameters[1].Value = model.ForumName;
            parameters[2].Value = model.ForumMemo;
            parameters[3].Value = model.OrderNo;
            parameters[4].Value = model.ForumType;
            parameters[5].Value = model.ForumClass;

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
        public bool Update(FormsModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Forms set ");

            strSql.Append(" ForumId = @ForumId , ");
            strSql.Append(" ForumName = @ForumName , ");
            strSql.Append(" ForumMemo = @ForumMemo , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" ForumType = @ForumType , ");
            strSql.Append(" ForumClass = @ForumClass  ");
            strSql.Append(" where ForumId=@ForumId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ForumId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ForumName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ForumMemo", SqlDbType.VarChar,1000) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ForumType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ForumClass", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.ForumId;
            parameters[1].Value = model.ForumName;
            parameters[2].Value = model.ForumMemo;
            parameters[3].Value = model.OrderNo;
            parameters[4].Value = model.ForumType;
            parameters[5].Value = model.ForumClass; try
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
        public FormsModel GetModel(decimal ForumId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ForumId, ForumName, ForumMemo, OrderNo, ForumType, ForumClass  ");
            strSql.Append("  from Forms ");
            strSql.Append(" where ForumId=@ForumId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ForumId", SqlDbType.Decimal,9)			};
            parameters[0].Value = ForumId;


            FormsModel model = new FormsModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ForumId"].ToString() != "")
                {
                    model.ForumId = decimal.Parse(ds.Tables[0].Rows[0]["ForumId"].ToString());
                }
                model.ForumName = ds.Tables[0].Rows[0]["ForumName"].ToString();
                model.ForumMemo = ds.Tables[0].Rows[0]["ForumMemo"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
                }
                model.ForumType = ds.Tables[0].Rows[0]["ForumType"].ToString();
                model.ForumClass = ds.Tables[0].Rows[0]["ForumClass"].ToString();

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
            strSql.Append("delete from Forms ");
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
            strSql.Append(" FROM Forms  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM BBS.DBO.Forms  WITH(NOLOCK) ");
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
            strSql.Append(" FROM Forms  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

