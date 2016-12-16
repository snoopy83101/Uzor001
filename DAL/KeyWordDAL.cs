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
    //KeyWord
    public partial class KeyWordDAL
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
            strSql.Append(" FROM  CORE.dbo.KeyWord ");
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
        public bool Add(KeyWordModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.KeyWord (");
            strSql.Append("KeyWord,OrderNo,KeyWordType");
            strSql.Append(") values (");
            strSql.Append("@KeyWord,@OrderNo,@KeyWordType");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@KeyWord", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@KeyWordType", SqlDbType.VarChar,10)             
              
            };

            parameters[0].Value = model.KeyWord;
            parameters[1].Value = model.OrderNo;
            parameters[2].Value = model.KeyWordType;

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
        public bool Update(KeyWordModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.KeyWord set ");

            strSql.Append(" KeyWord = @KeyWord , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" KeyWordType = @KeyWordType  ");
            strSql.Append(" where KeyWord=@KeyWord  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@KeyWord", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@KeyWordType", SqlDbType.VarChar,10)             
              
            };

            parameters[0].Value = model.KeyWord;
            parameters[1].Value = model.OrderNo;
            parameters[2].Value = model.KeyWordType; try
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
        public KeyWordModel GetModel(string KeyWord)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select KeyWord, OrderNo, KeyWordType  ");
            strSql.Append("  from CORE.dbo.KeyWord ");
            strSql.Append(" where KeyWord=@KeyWord ");
            SqlParameter[] parameters = {
					new SqlParameter("@KeyWord", SqlDbType.VarChar,30)			};
            parameters[0].Value = KeyWord;


            KeyWordModel model = new KeyWordModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.KeyWord = ds.Tables[0].Rows[0]["KeyWord"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
                }
                model.KeyWordType = ds.Tables[0].Rows[0]["KeyWordType"].ToString();

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
            strSql.Append("delete from CORE.dbo.KeyWord ");
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
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize, string cols)
        {
            if (cols == "")
            {
                cols = " * ";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 " + cols + " ");
            strSql.Append(" FROM CORE.dbo.KeyWord  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.KeyWord  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.KeyWord  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

