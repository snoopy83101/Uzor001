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
    //QianDaoLog
    public partial class QianDaoLogDAL
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
            strSql.Append(" FROM  CORE.dbo.QianDaoLog ");
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
        public bool Add(QianDaoLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.QianDaoLog (");
            strSql.Append("QianDaoMemberId,QianDaoMemo,DayNum,JiFenChangeId,CreateTime");
            strSql.Append(") values (");
            strSql.Append("@QianDaoMemberId,@QianDaoMemo,@DayNum,@JiFenChangeId,@CreateTime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@QianDaoMemberId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@QianDaoMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@DayNum", SqlDbType.Int,4) ,            
                        new SqlParameter("@JiFenChangeId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.QianDaoMemberId;
            parameters[1].Value = model.QianDaoMemo;
            parameters[2].Value = model.DayNum;
            parameters[3].Value = model.JiFenChangeId;
            parameters[4].Value = model.CreateTime;

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
        public bool Update(QianDaoLogModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.QianDaoLog set ");

            strSql.Append(" QianDaoMemberId = @QianDaoMemberId , ");
            strSql.Append(" QianDaoMemo = @QianDaoMemo , ");
            strSql.Append(" DayNum = @DayNum , ");
            strSql.Append(" JiFenChangeId = @JiFenChangeId , ");
            strSql.Append(" CreateTime = @CreateTime  ");
            strSql.Append(" where QianDaoLogId=@QianDaoLogId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@QianDaoLogId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@QianDaoMemberId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@QianDaoMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@DayNum", SqlDbType.Int,4) ,            
                        new SqlParameter("@JiFenChangeId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.QianDaoLogId;
            parameters[1].Value = model.QianDaoMemberId;
            parameters[2].Value = model.QianDaoMemo;
            parameters[3].Value = model.DayNum;
            parameters[4].Value = model.JiFenChangeId;
            parameters[5].Value = model.CreateTime; try
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
        public QianDaoLogModel GetModel(decimal QianDaoLogId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select QianDaoLogId, QianDaoMemberId, QianDaoMemo, DayNum, JiFenChangeId, CreateTime  ");
            strSql.Append("  from CORE.dbo.QianDaoLog ");
            strSql.Append(" where QianDaoLogId=@QianDaoLogId");
            SqlParameter[] parameters = {
					new SqlParameter("@QianDaoLogId", SqlDbType.Decimal)
			};
            parameters[0].Value = QianDaoLogId;


            QianDaoLogModel model = new QianDaoLogModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["QianDaoLogId"].ToString() != "")
                {
                    model.QianDaoLogId = decimal.Parse(ds.Tables[0].Rows[0]["QianDaoLogId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QianDaoMemberId"].ToString() != "")
                {
                    model.QianDaoMemberId = decimal.Parse(ds.Tables[0].Rows[0]["QianDaoMemberId"].ToString());
                }
                model.QianDaoMemo = ds.Tables[0].Rows[0]["QianDaoMemo"].ToString();
                if (ds.Tables[0].Rows[0]["DayNum"].ToString() != "")
                {
                    model.DayNum = int.Parse(ds.Tables[0].Rows[0]["DayNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JiFenChangeId"].ToString() != "")
                {
                    model.JiFenChangeId = decimal.Parse(ds.Tables[0].Rows[0]["JiFenChangeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
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
            strSql.Append("delete from CORE.dbo.QianDaoLog ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.QianDaoLog  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.QianDaoLog  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.QianDaoLog  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.QianDaoLog  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

