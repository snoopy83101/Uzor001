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
    //Gwc
    public partial class GwcDAL
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
            strSql.Append(" FROM  CORE.dbo.Gwc ");
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
        public bool Add(GwcModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.Gwc (");
            strSql.Append("BranchId,ProId,Quantity,ProTeXing,ZlBs,hope,Invalid,CreateTime,MemberId");
            strSql.Append(") values (");
            strSql.Append("@BranchId,@ProId,@Quantity,@ProTeXing,@ZlBs,@hope,@Invalid,@CreateTime,@MemberId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Quantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProTeXing", SqlDbType.Int,4) ,
                        new SqlParameter("@ZlBs", SqlDbType.Decimal,9) ,
                        new SqlParameter("@hope", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.BranchId;
            parameters[1].Value = model.ProId;
            parameters[2].Value = model.Quantity;
            parameters[3].Value = model.ProTeXing;
            parameters[4].Value = model.ZlBs;
            parameters[5].Value = model.hope;
            parameters[6].Value = model.Invalid;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.MemberId;

            bool result = false;
            try
            {


                model.GwcId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "GwcId", parameters));


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
        public bool Update(GwcModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.Gwc set ");

            strSql.Append(" BranchId = @BranchId , ");
            strSql.Append(" ProId = @ProId , ");
            strSql.Append(" Quantity = @Quantity , ");
            strSql.Append(" ProTeXing = @ProTeXing , ");
            strSql.Append(" ZlBs = @ZlBs , ");
            strSql.Append(" hope = @hope , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" MemberId = @MemberId  ");
            strSql.Append(" where GwcId=@GwcId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@GwcId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ProId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Quantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProTeXing", SqlDbType.Int,4) ,
                        new SqlParameter("@ZlBs", SqlDbType.Decimal,9) ,
                        new SqlParameter("@hope", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.GwcId;
            parameters[1].Value = model.BranchId;
            parameters[2].Value = model.ProId;
            parameters[3].Value = model.Quantity;
            parameters[4].Value = model.ProTeXing;
            parameters[5].Value = model.ZlBs;
            parameters[6].Value = model.hope;
            parameters[7].Value = model.Invalid;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.MemberId; try
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
        public GwcModel GetModel(decimal GwcId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GwcId, BranchId, ProId, Quantity, ProTeXing, ZlBs, hope, Invalid, CreateTime, MemberId  ");
            strSql.Append("  from CORE.dbo.Gwc ");
            strSql.Append(" where GwcId=@GwcId");
            SqlParameter[] parameters = {
                    new SqlParameter("@GwcId", SqlDbType.Decimal)
            };
            parameters[0].Value = GwcId;


            GwcModel model = new GwcModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["GwcId"].ToString() != "")
                {
                    model.GwcId = decimal.Parse(ds.Tables[0].Rows[0]["GwcId"].ToString());
                }
                model.BranchId = ds.Tables[0].Rows[0]["BranchId"].ToString();
                model.ProId = ds.Tables[0].Rows[0]["ProId"].ToString();
                if (ds.Tables[0].Rows[0]["Quantity"].ToString() != "")
                {
                    model.Quantity = decimal.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProTeXing"].ToString() != "")
                {
                    model.ProTeXing = int.Parse(ds.Tables[0].Rows[0]["ProTeXing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZlBs"].ToString() != "")
                {
                    model.ZlBs = decimal.Parse(ds.Tables[0].Rows[0]["ZlBs"].ToString());
                }
                if (ds.Tables[0].Rows[0]["hope"].ToString() != "")
                {
                    model.hope = decimal.Parse(ds.Tables[0].Rows[0]["hope"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Invalid"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Invalid"].ToString() == "1") || (ds.Tables[0].Rows[0]["Invalid"].ToString().ToLower() == "true"))
                    {
                        model.Invalid = true;
                    }
                    else
                    {
                        model.Invalid = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberId"].ToString() != "")
                {
                    model.MemberId = decimal.Parse(ds.Tables[0].Rows[0]["MemberId"].ToString());
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
            strSql.Append("delete from CORE.dbo.Gwc ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.Gwc  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.Gwc  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.Gwc  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.Gwc  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

