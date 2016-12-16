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
    //QyWxPtGroup
    public partial class QyWxPtGroupDAL
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
            strSql.Append(" FROM  CORE.dbo.QyWxPtGroup ");
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
        public bool Add(QyWxPtGroupModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.QyWxPtGroup (");
            strSql.Append("QyWxPtId,Secret,AccessToken,AccessTokenCreateTime,QyWxPtGroupMemo");
            strSql.Append(") values (");
            strSql.Append("@QyWxPtId,@Secret,@AccessToken,@AccessTokenCreateTime,@QyWxPtGroupMemo");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@QyWxPtId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Secret", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@AccessToken", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@AccessTokenCreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@QyWxPtGroupMemo", SqlDbType.VarChar,250)             
              
            };

            parameters[0].Value = model.QyWxPtId;
            parameters[1].Value = model.Secret;
            parameters[2].Value = model.AccessToken;
            parameters[3].Value = model.AccessTokenCreateTime;
            parameters[4].Value = model.QyWxPtGroupMemo;

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
        public bool Update(QyWxPtGroupModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.QyWxPtGroup set ");

            strSql.Append(" QyWxPtId = @QyWxPtId , ");
            strSql.Append(" Secret = @Secret , ");
            strSql.Append(" AccessToken = @AccessToken , ");
            strSql.Append(" AccessTokenCreateTime = @AccessTokenCreateTime , ");
            strSql.Append(" QyWxPtGroupMemo = @QyWxPtGroupMemo  ");
            strSql.Append(" where QyWxPtGroupId=@QyWxPtGroupId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@QyWxPtGroupId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@QyWxPtId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Secret", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@AccessToken", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@AccessTokenCreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@QyWxPtGroupMemo", SqlDbType.VarChar,250)             
              
            };

            parameters[0].Value = model.QyWxPtGroupId;
            parameters[1].Value = model.QyWxPtId;
            parameters[2].Value = model.Secret;
            parameters[3].Value = model.AccessToken;
            parameters[4].Value = model.AccessTokenCreateTime;
            parameters[5].Value = model.QyWxPtGroupMemo; try
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
        public QyWxPtGroupModel GetModel(decimal QyWxPtGroupId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select QyWxPtGroupId, QyWxPtId, Secret, AccessToken, AccessTokenCreateTime, QyWxPtGroupMemo  ");
            strSql.Append("  from CORE.dbo.QyWxPtGroup ");
            strSql.Append(" where QyWxPtGroupId=@QyWxPtGroupId");
            SqlParameter[] parameters = {
					new SqlParameter("@QyWxPtGroupId", SqlDbType.Decimal)
			};
            parameters[0].Value = QyWxPtGroupId;


            QyWxPtGroupModel model = new QyWxPtGroupModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["QyWxPtGroupId"].ToString() != "")
                {
                    model.QyWxPtGroupId = decimal.Parse(ds.Tables[0].Rows[0]["QyWxPtGroupId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QyWxPtId"].ToString() != "")
                {
                    model.QyWxPtId = decimal.Parse(ds.Tables[0].Rows[0]["QyWxPtId"].ToString());
                }
                model.Secret = ds.Tables[0].Rows[0]["Secret"].ToString();
                model.AccessToken = ds.Tables[0].Rows[0]["AccessToken"].ToString();
                if (ds.Tables[0].Rows[0]["AccessTokenCreateTime"].ToString() != "")
                {
                    model.AccessTokenCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["AccessTokenCreateTime"].ToString());
                }
                model.QyWxPtGroupMemo = ds.Tables[0].Rows[0]["QyWxPtGroupMemo"].ToString();

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
            strSql.Append("delete from CORE.dbo.QyWxPtGroup ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.QyWxPtGroup  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.QyWxPtGroup  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.QyWxPtGorupView  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.QyWxPtGroup  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

