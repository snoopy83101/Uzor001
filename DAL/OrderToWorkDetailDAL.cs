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
    //OrderToWorkDetail
    public partial class OrderToWorkDetailDAL
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
            strSql.Append(" FROM  CORE.dbo.OrderToWorkDetail ");
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
        public bool Add(OrderToWorkDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.OrderToWorkDetail (");
            strSql.Append("Color,OrderToWorkId");
            strSql.Append(") values (");
            strSql.Append("@Color,@OrderToWorkId");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@Color", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderToWorkId", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.Color;
            parameters[1].Value = model.OrderToWorkId;

            bool result = false;
            try
            {


                model.OrderToWorkDetailId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "OrderToWorkDetailId", parameters));


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
        public bool Update(OrderToWorkDetailModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.OrderToWorkDetail set ");

            strSql.Append(" Color = @Color , ");
            strSql.Append(" OrderToWorkId = @OrderToWorkId  ");
            strSql.Append(" where OrderToWorkDetailId=@OrderToWorkDetailId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@OrderToWorkDetailId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Color", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderToWorkId", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.OrderToWorkDetailId;
            parameters[1].Value = model.Color;
            parameters[2].Value = model.OrderToWorkId; try
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
        public OrderToWorkDetailModel GetModel(decimal OrderToWorkDetailId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderToWorkDetailId, Color, OrderToWorkId  ");
            strSql.Append("  from CORE.dbo.OrderToWorkDetail ");
            strSql.Append(" where OrderToWorkDetailId=@OrderToWorkDetailId");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderToWorkDetailId", SqlDbType.Decimal)
            };
            parameters[0].Value = OrderToWorkDetailId;


            OrderToWorkDetailModel model = new OrderToWorkDetailModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["OrderToWorkDetailId"].ToString() != "")
                {
                    model.OrderToWorkDetailId = decimal.Parse(ds.Tables[0].Rows[0]["OrderToWorkDetailId"].ToString());
                }
                model.Color = ds.Tables[0].Rows[0]["Color"].ToString();
                if (ds.Tables[0].Rows[0]["OrderToWorkId"].ToString() != "")
                {
                    model.OrderToWorkId = decimal.Parse(ds.Tables[0].Rows[0]["OrderToWorkId"].ToString());
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
            strSql.Append("delete from CORE.dbo.OrderToWorkDetail ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.OrderToWorkDetail  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.OrderToWorkDetail  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.OrderToWorkDetail  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.OrderToWorkDetail  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

