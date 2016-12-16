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
    //QyWxPtApp
    public partial class QyWxPtAppDAL
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
            strSql.Append(" FROM  CORE.dbo.QyWxPtApp ");
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
        public bool Add(QyWxPtAppModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.QyWxPtApp (");
            strSql.Append("AgentID,QyWxPtAppName,QyWxPtAppMemo,DefaultGroupId,QyWxPtId");
            strSql.Append(") values (");
            strSql.Append("@AgentID,@QyWxPtAppName,@QyWxPtAppMemo,@DefaultGroupId,@QyWxPtId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@AgentID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@QyWxPtAppName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@QyWxPtAppMemo", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@DefaultGroupId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@QyWxPtId", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.AgentID;
            parameters[1].Value = model.QyWxPtAppName;
            parameters[2].Value = model.QyWxPtAppMemo;
            parameters[3].Value = model.DefaultGroupId;
            parameters[4].Value = model.QyWxPtId;

            bool result = false;
            try
            {
                model.QyWxPtAppId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "QyWxPtAppId", parameters));
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
        public bool Update(QyWxPtAppModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.QyWxPtApp set ");

            strSql.Append(" AgentID = @AgentID , ");
            strSql.Append(" QyWxPtAppName = @QyWxPtAppName , ");
            strSql.Append(" QyWxPtAppMemo = @QyWxPtAppMemo , ");
            strSql.Append(" DefaultGroupId = @DefaultGroupId , ");
            strSql.Append(" QyWxPtId = @QyWxPtId  ");
            strSql.Append(" where QyWxPtAppId=@QyWxPtAppId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@QyWxPtAppId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AgentID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@QyWxPtAppName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@QyWxPtAppMemo", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@DefaultGroupId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@QyWxPtId", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.QyWxPtAppId;
            parameters[1].Value = model.AgentID;
            parameters[2].Value = model.QyWxPtAppName;
            parameters[3].Value = model.QyWxPtAppMemo;
            parameters[4].Value = model.DefaultGroupId;
            parameters[5].Value = model.QyWxPtId; try
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
        public QyWxPtAppModel GetModel(decimal QyWxPtAppId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select QyWxPtAppId, AgentID, QyWxPtAppName, QyWxPtAppMemo, DefaultGroupId, QyWxPtId  ");
            strSql.Append("  from CORE.dbo.QyWxPtApp ");
            strSql.Append(" where QyWxPtAppId=@QyWxPtAppId");
            SqlParameter[] parameters = {
					new SqlParameter("@QyWxPtAppId", SqlDbType.Decimal)
			};
            parameters[0].Value = QyWxPtAppId;


            QyWxPtAppModel model = new QyWxPtAppModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["QyWxPtAppId"].ToString() != "")
                {
                    model.QyWxPtAppId = decimal.Parse(ds.Tables[0].Rows[0]["QyWxPtAppId"].ToString());
                }
                model.AgentID = ds.Tables[0].Rows[0]["AgentID"].ToString();
                model.QyWxPtAppName = ds.Tables[0].Rows[0]["QyWxPtAppName"].ToString();
                model.QyWxPtAppMemo = ds.Tables[0].Rows[0]["QyWxPtAppMemo"].ToString();
                if (ds.Tables[0].Rows[0]["DefaultGroupId"].ToString() != "")
                {
                    model.DefaultGroupId = decimal.Parse(ds.Tables[0].Rows[0]["DefaultGroupId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QyWxPtId"].ToString() != "")
                {
                    model.QyWxPtId = decimal.Parse(ds.Tables[0].Rows[0]["QyWxPtId"].ToString());
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
            strSql.Append("delete from CORE.dbo.QyWxPtApp ");
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
        public DataSet GetPageList(string strWhere, string Order, int currentpage, int pagesize, string col)
        {
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "TableName", "ReFieldsStr", "OrderString", "WhereString", "PageSize", "PageIndex", "TotalRecord" };
            object[] fenyeParmValue = new object[] { "CORE.dbo.QyWxPtApp  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
            ds = helper.ExecProc_ReDs("dbo.fenye2", fenyeParmName, fenyeParmValue);
            ds = Common.DataSetting.DataPageSetting(ds, pagesize, currentpage);
            return ds;


        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize, string cols)
        {
            if (cols == "")
            {
                cols = " * ";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 " + cols + " ");
            strSql.Append(" FROM CORE.dbo.QyWxPtApp  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.QyWxPtApp  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.QyWxPtApp  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

