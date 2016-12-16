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
    //AppMenuInfo
    public partial class AppMenuInfoDAL
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
            strSql.Append(" FROM  CORE.dbo.AppMenuInfo ");
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
        public bool Add(AppMenuInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.AppMenuInfo (");
            strSql.Append("AppMenuId,AdminPower,AppMenuName,AppMenuMemo,OrderNo,ParentAppMenuId,AppMenuType,Url,EventName,Icon");
            strSql.Append(") values (");
            strSql.Append("@AppMenuId,@AdminPower,@AppMenuName,@AppMenuMemo,@OrderNo,@ParentAppMenuId,@AppMenuType,@Url,@EventName,@Icon");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@AppMenuId", SqlDbType.VarChar,30) ,
                        new SqlParameter("@AdminPower", SqlDbType.Int,4) ,
                        new SqlParameter("@AppMenuName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@AppMenuMemo", SqlDbType.VarChar,150) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@ParentAppMenuId", SqlDbType.VarChar,30) ,
                        new SqlParameter("@AppMenuType", SqlDbType.VarChar,10) ,
                        new SqlParameter("@Url", SqlDbType.VarChar,200) ,
                        new SqlParameter("@EventName", SqlDbType.VarChar,30) ,
                        new SqlParameter("@Icon", SqlDbType.VarChar,20)

            };

            parameters[0].Value = model.AppMenuId;
            parameters[1].Value = model.AdminPower;
            parameters[2].Value = model.AppMenuName;
            parameters[3].Value = model.AppMenuMemo;
            parameters[4].Value = model.OrderNo;
            parameters[5].Value = model.ParentAppMenuId;
            parameters[6].Value = model.AppMenuType;
            parameters[7].Value = model.Url;
            parameters[8].Value = model.EventName;
            parameters[9].Value = model.Icon;

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
        public bool Update(AppMenuInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.AppMenuInfo set ");

            strSql.Append(" AppMenuId = @AppMenuId , ");
            strSql.Append(" AdminPower = @AdminPower , ");
            strSql.Append(" AppMenuName = @AppMenuName , ");
            strSql.Append(" AppMenuMemo = @AppMenuMemo , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" ParentAppMenuId = @ParentAppMenuId , ");
            strSql.Append(" AppMenuType = @AppMenuType , ");
            strSql.Append(" Url = @Url , ");
            strSql.Append(" EventName = @EventName , ");
            strSql.Append(" Icon = @Icon  ");
            strSql.Append(" where AppMenuId=@AppMenuId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@AppMenuId", SqlDbType.VarChar,30) ,
                        new SqlParameter("@AdminPower", SqlDbType.Int,4) ,
                        new SqlParameter("@AppMenuName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@AppMenuMemo", SqlDbType.VarChar,150) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@ParentAppMenuId", SqlDbType.VarChar,30) ,
                        new SqlParameter("@AppMenuType", SqlDbType.VarChar,10) ,
                        new SqlParameter("@Url", SqlDbType.VarChar,200) ,
                        new SqlParameter("@EventName", SqlDbType.VarChar,30) ,
                        new SqlParameter("@Icon", SqlDbType.VarChar,20)

            };

            parameters[0].Value = model.AppMenuId;
            parameters[1].Value = model.AdminPower;
            parameters[2].Value = model.AppMenuName;
            parameters[3].Value = model.AppMenuMemo;
            parameters[4].Value = model.OrderNo;
            parameters[5].Value = model.ParentAppMenuId;
            parameters[6].Value = model.AppMenuType;
            parameters[7].Value = model.Url;
            parameters[8].Value = model.EventName;
            parameters[9].Value = model.Icon; try
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
        public AppMenuInfoModel GetModel(string AppMenuId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AppMenuId, AdminPower, AppMenuName, AppMenuMemo, OrderNo, ParentAppMenuId, AppMenuType, Url, EventName, Icon  ");
            strSql.Append("  from CORE.dbo.AppMenuInfo ");
            strSql.Append(" where AppMenuId=@AppMenuId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@AppMenuId", SqlDbType.VarChar,30)            };
            parameters[0].Value = AppMenuId;


            AppMenuInfoModel model = new AppMenuInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.AppMenuId = ds.Tables[0].Rows[0]["AppMenuId"].ToString();
                if (ds.Tables[0].Rows[0]["AdminPower"].ToString() != "")
                {
                    model.AdminPower = int.Parse(ds.Tables[0].Rows[0]["AdminPower"].ToString());
                }
                model.AppMenuName = ds.Tables[0].Rows[0]["AppMenuName"].ToString();
                model.AppMenuMemo = ds.Tables[0].Rows[0]["AppMenuMemo"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
                }
                model.ParentAppMenuId = ds.Tables[0].Rows[0]["ParentAppMenuId"].ToString();
                model.AppMenuType = ds.Tables[0].Rows[0]["AppMenuType"].ToString();
                model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                model.EventName = ds.Tables[0].Rows[0]["EventName"].ToString();
                model.Icon = ds.Tables[0].Rows[0]["Icon"].ToString();

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
            strSql.Append("delete from CORE.dbo.AppMenuInfo ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.AppMenuInfo  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.AppMenuInfo  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.AppMenuInfo  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.AppMenuInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

