﻿using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Model;
using DBTools;
using Common;
namespace DAL
{
    //BmTel
    public partial class BmTelDAL
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
            strSql.Append(" FROM  CORE.dbo.BmTel ");
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
        public bool Add(BmTelModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.BmTel (");
            strSql.Append("BmTelNo,BmTelTitle,BmTelTypeId,BmTelMemo,OrderNo");
            strSql.Append(") values (");
            strSql.Append("@BmTelNo,@BmTelTitle,@BmTelTypeId,@BmTelMemo,@OrderNo");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@BmTelNo", SqlDbType.VarChar,50) ,
                        new SqlParameter("@BmTelTitle", SqlDbType.VarChar,50) ,
                        new SqlParameter("@BmTelTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@BmTelMemo", SqlDbType.VarChar,250) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)

            };

            parameters[0].Value = model.BmTelNo;
            parameters[1].Value = model.BmTelTitle;
            parameters[2].Value = model.BmTelTypeId;
            parameters[3].Value = model.BmTelMemo;
            parameters[4].Value = model.OrderNo;

            bool result = false;
            try
            {


                model.BmTelId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "BmTelId", parameters));


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
        public bool Update(BmTelModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.BmTel set ");

            strSql.Append(" BmTelNo = @BmTelNo , ");
            strSql.Append(" BmTelTitle = @BmTelTitle , ");
            strSql.Append(" BmTelTypeId = @BmTelTypeId , ");
            strSql.Append(" BmTelMemo = @BmTelMemo , ");
            strSql.Append(" OrderNo = @OrderNo  ");
            strSql.Append(" where BmTelId=@BmTelId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@BmTelId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BmTelNo", SqlDbType.VarChar,50) ,
                        new SqlParameter("@BmTelTitle", SqlDbType.VarChar,50) ,
                        new SqlParameter("@BmTelTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@BmTelMemo", SqlDbType.VarChar,250) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)

            };

            parameters[0].Value = model.BmTelId;
            parameters[1].Value = model.BmTelNo;
            parameters[2].Value = model.BmTelTitle;
            parameters[3].Value = model.BmTelTypeId;
            parameters[4].Value = model.BmTelMemo;
            parameters[5].Value = model.OrderNo; try
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
        public BmTelModel GetModel(decimal BmTelId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BmTelId, BmTelNo, BmTelTitle, BmTelTypeId, BmTelMemo, OrderNo  ");
            strSql.Append("  from CORE.dbo.BmTel ");
            strSql.Append(" where BmTelId=@BmTelId");
            SqlParameter[] parameters = {
                    new SqlParameter("@BmTelId", SqlDbType.Decimal)
            };
            parameters[0].Value = BmTelId;


            BmTelModel model = new BmTelModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["BmTelId"].ToString() != "")
                {
                    model.BmTelId = decimal.Parse(ds.Tables[0].Rows[0]["BmTelId"].ToString());
                }
                model.BmTelNo = ds.Tables[0].Rows[0]["BmTelNo"].ToString();
                model.BmTelTitle = ds.Tables[0].Rows[0]["BmTelTitle"].ToString();
                if (ds.Tables[0].Rows[0]["BmTelTypeId"].ToString() != "")
                {
                    model.BmTelTypeId = int.Parse(ds.Tables[0].Rows[0]["BmTelTypeId"].ToString());
                }
                model.BmTelMemo = ds.Tables[0].Rows[0]["BmTelMemo"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
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
            strSql.Append("delete from CORE.dbo.BmTel ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.BmTelView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.BmTel  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.BmTel  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.BmTel  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

