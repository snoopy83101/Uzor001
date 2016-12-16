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
    //QyWxPtVsUser
    public partial class QyWxPtVsUserDAL
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
            strSql.Append(" FROM  CORE.dbo.QyWxPtVsUser ");
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
        public bool Add(QyWxPtVsUserModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.QyWxPtVsUser (");
            strSql.Append("UserId,QyWxPtId,QyWxPtUserId,Memo");
            strSql.Append(") values (");
            strSql.Append("@UserId,@QyWxPtId,@QyWxPtUserId,@Memo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@UserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@QyWxPtId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@QyWxPtUserId", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.UserId;
            parameters[1].Value = model.QyWxPtId;
            parameters[2].Value = model.QyWxPtUserId;
            parameters[3].Value = model.Memo;

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
        public bool Update(QyWxPtVsUserModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.QyWxPtVsUser set ");

            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" QyWxPtId = @QyWxPtId , ");
            strSql.Append(" QyWxPtUserId = @QyWxPtUserId , ");
            strSql.Append(" Memo = @Memo  ");
            strSql.Append(" where UserId=@UserId and QyWxPtId=@QyWxPtId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@UserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@QyWxPtId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@QyWxPtUserId", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.UserId;
            parameters[1].Value = model.QyWxPtId;
            parameters[2].Value = model.QyWxPtUserId;
            parameters[3].Value = model.Memo; try
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
        public QyWxPtVsUserModel GetModel(string UserId, decimal QyWxPtId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserId, QyWxPtId, QyWxPtUserId, Memo  ");
            strSql.Append("  from CORE.dbo.QyWxPtVsUser ");
            strSql.Append(" where UserId=@UserId and QyWxPtId=@QyWxPtId ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.VarChar,50),
					new SqlParameter("@QyWxPtId", SqlDbType.Decimal,9)			};
            parameters[0].Value = UserId;
            parameters[1].Value = QyWxPtId;


            QyWxPtVsUserModel model = new QyWxPtVsUserModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                if (ds.Tables[0].Rows[0]["QyWxPtId"].ToString() != "")
                {
                    model.QyWxPtId = decimal.Parse(ds.Tables[0].Rows[0]["QyWxPtId"].ToString());
                }
                model.QyWxPtUserId = ds.Tables[0].Rows[0]["QyWxPtUserId"].ToString();
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();

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
            strSql.Append("delete from CORE.dbo.QyWxPtVsUser ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.QyWxPtVsUserView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.QyWxPtVsUser  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.QyWxPtVsUser  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.QyWxPtVsUser  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

