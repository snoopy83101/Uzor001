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
    //MerConfig
    public partial class MerConfigDAL
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
            strSql.Append(" FROM  CORE.dbo.MerConfig ");
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
        public bool Add(MerConfigModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.MerConfig (");
            strSql.Append("MerConfigId,MerId,MerConfigTitle,MerConfigVal,MerConfigTypeId,Memo,OrderNo");
            strSql.Append(") values (");
            strSql.Append("@MerConfigId,@MerId,@MerConfigTitle,@MerConfigVal,@MerConfigTypeId,@Memo,@OrderNo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@MerConfigId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MerConfigTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MerConfigVal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MerConfigTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.MerConfigId;
            parameters[1].Value = model.MerId;
            parameters[2].Value = model.MerConfigTitle;
            parameters[3].Value = model.MerConfigVal;
            parameters[4].Value = model.MerConfigTypeId;
            parameters[5].Value = model.Memo;
            parameters[6].Value = model.OrderNo;

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
        public bool Update(MerConfigModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.MerConfig set ");

            strSql.Append(" MerConfigId = @MerConfigId , ");
            strSql.Append(" MerId = @MerId , ");
            strSql.Append(" MerConfigTitle = @MerConfigTitle , ");
            strSql.Append(" MerConfigVal = @MerConfigVal , ");
            strSql.Append(" MerConfigTypeId = @MerConfigTypeId , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" OrderNo = @OrderNo  ");
            strSql.Append(" where MerConfigId=@MerConfigId and MerId=@MerId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@MerConfigId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MerConfigTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MerConfigVal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MerConfigTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.MerConfigId;
            parameters[1].Value = model.MerId;
            parameters[2].Value = model.MerConfigTitle;
            parameters[3].Value = model.MerConfigVal;
            parameters[4].Value = model.MerConfigTypeId;
            parameters[5].Value = model.Memo;
            parameters[6].Value = model.OrderNo; try
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
        public MerConfigModel GetModel(string MerConfigId, decimal MerId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MerConfigId, MerId, MerConfigTitle, MerConfigVal, MerConfigTypeId, Memo, OrderNo  ");
            strSql.Append("  from CORE.dbo.MerConfig ");
            strSql.Append(" where MerConfigId=@MerConfigId and MerId=@MerId ");
            SqlParameter[] parameters = {
					new SqlParameter("@MerConfigId", SqlDbType.VarChar,50),
					new SqlParameter("@MerId", SqlDbType.Decimal,9)			};
            parameters[0].Value = MerConfigId;
            parameters[1].Value = MerId;


            MerConfigModel model = new MerConfigModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.MerConfigId = ds.Tables[0].Rows[0]["MerConfigId"].ToString();
                if (ds.Tables[0].Rows[0]["MerId"].ToString() != "")
                {
                    model.MerId = decimal.Parse(ds.Tables[0].Rows[0]["MerId"].ToString());
                }
                model.MerConfigTitle = ds.Tables[0].Rows[0]["MerConfigTitle"].ToString();
                model.MerConfigVal = ds.Tables[0].Rows[0]["MerConfigVal"].ToString();
                if (ds.Tables[0].Rows[0]["MerConfigTypeId"].ToString() != "")
                {
                    model.MerConfigTypeId = int.Parse(ds.Tables[0].Rows[0]["MerConfigTypeId"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
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
            strSql.Append("delete from CORE.dbo.MerConfig ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.MerConfig  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.MerConfig  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.MerConfig  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.MerConfig  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

