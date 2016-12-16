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
    //JiFenChange
    public partial class JiFenChangeDAL
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
            strSql.Append(" FROM  CORE.dbo.JiFenChange ");
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
        public bool Add(JiFenChangeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.JiFenChange (");
            strSql.Append("JifenChangeTypeId,JiFenChangeClassId,JiFenChangeMemo,JiFenChangeNum,MemberId,CreateTime,ReKey");
            strSql.Append(") values (");
            strSql.Append("@JifenChangeTypeId,@JiFenChangeClassId,@JiFenChangeMemo,@JiFenChangeNum,@MemberId,@CreateTime,@ReKey");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@JifenChangeTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@JiFenChangeClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@JiFenChangeMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@JiFenChangeNum", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.JifenChangeTypeId;
            parameters[1].Value = model.JiFenChangeClassId;
            parameters[2].Value = model.JiFenChangeMemo;
            parameters[3].Value = model.JiFenChangeNum;
            parameters[4].Value = model.MemberId;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.ReKey;

            bool result = false;
            try
            {
                model.JifenChangeId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "JifenChangeId", parameters));
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
        public bool Update(JiFenChangeModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.JiFenChange set ");

            strSql.Append(" JifenChangeTypeId = @JifenChangeTypeId , ");
            strSql.Append(" JiFenChangeClassId = @JiFenChangeClassId , ");
            strSql.Append(" JiFenChangeMemo = @JiFenChangeMemo , ");
            strSql.Append(" JiFenChangeNum = @JiFenChangeNum , ");
            strSql.Append(" MemberId = @MemberId , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" ReKey = @ReKey  ");
            strSql.Append(" where JifenChangeId=@JifenChangeId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@JifenChangeId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@JifenChangeTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@JiFenChangeClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@JiFenChangeMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@JiFenChangeNum", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.JifenChangeId;
            parameters[1].Value = model.JifenChangeTypeId;
            parameters[2].Value = model.JiFenChangeClassId;
            parameters[3].Value = model.JiFenChangeMemo;
            parameters[4].Value = model.JiFenChangeNum;
            parameters[5].Value = model.MemberId;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.ReKey; try
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
        public JiFenChangeModel GetModel(decimal JifenChangeId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select JifenChangeId, JifenChangeTypeId, JiFenChangeClassId, JiFenChangeMemo, JiFenChangeNum, MemberId, CreateTime, ReKey  ");
            strSql.Append("  from CORE.dbo.JiFenChange ");
            strSql.Append(" where JifenChangeId=@JifenChangeId");
            SqlParameter[] parameters = {
					new SqlParameter("@JifenChangeId", SqlDbType.Decimal)
			};
            parameters[0].Value = JifenChangeId;


            JiFenChangeModel model = new JiFenChangeModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["JifenChangeId"].ToString() != "")
                {
                    model.JifenChangeId = decimal.Parse(ds.Tables[0].Rows[0]["JifenChangeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JifenChangeTypeId"].ToString() != "")
                {
                    model.JifenChangeTypeId = int.Parse(ds.Tables[0].Rows[0]["JifenChangeTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JiFenChangeClassId"].ToString() != "")
                {
                    model.JiFenChangeClassId = int.Parse(ds.Tables[0].Rows[0]["JiFenChangeClassId"].ToString());
                }
                model.JiFenChangeMemo = ds.Tables[0].Rows[0]["JiFenChangeMemo"].ToString();
                if (ds.Tables[0].Rows[0]["JiFenChangeNum"].ToString() != "")
                {
                    model.JiFenChangeNum = decimal.Parse(ds.Tables[0].Rows[0]["JiFenChangeNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberId"].ToString() != "")
                {
                    model.MemberId = decimal.Parse(ds.Tables[0].Rows[0]["MemberId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.ReKey = ds.Tables[0].Rows[0]["ReKey"].ToString();

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
            strSql.Append("delete from CORE.dbo.JiFenChange ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.JiFenChangeView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
            ds = helper.ExecProc_ReDs("dbo.fenye2", fenyeParmName, fenyeParmValue);
            ds = Common.DataSetting.DataPageSetting(ds, pagesize, currentpage);
            return ds;


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
            strSql.Append(" FROM CORE.dbo.JiFenChangeView  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.JiFenChange  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.JiFenChange  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

