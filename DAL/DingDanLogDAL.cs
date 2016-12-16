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
    //DingDanLog
    public partial class DingDanLogDAL
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
            strSql.Append(" FROM  CORE.dbo.DingDanLog ");
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
        public bool Add(DingDanLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.DingDanLog (");
            strSql.Append("DingDanId,DingDanLogTypeId,DingDanClassId,Memo,CreateTime");
            strSql.Append(") values (");
            strSql.Append("@DingDanId,@DingDanLogTypeId,@DingDanClassId,@Memo,@CreateTime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@DingDanId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DingDanLogTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@DingDanClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.DingDanId;
            parameters[1].Value = model.DingDanLogTypeId;
            parameters[2].Value = model.DingDanClassId;
            parameters[3].Value = model.Memo;
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
        public bool Update(DingDanLogModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.DingDanLog set ");

            strSql.Append(" DingDanId = @DingDanId , ");
            strSql.Append(" DingDanLogTypeId = @DingDanLogTypeId , ");
            strSql.Append(" DingDanClassId = @DingDanClassId , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" CreateTime = @CreateTime  ");
            strSql.Append(" where DingDanLogId=@DingDanLogId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@DingDanLogId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@DingDanId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DingDanLogTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@DingDanClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.DingDanLogId;
            parameters[1].Value = model.DingDanId;
            parameters[2].Value = model.DingDanLogTypeId;
            parameters[3].Value = model.DingDanClassId;
            parameters[4].Value = model.Memo;
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
        public DingDanLogModel GetModel(decimal DingDanLogId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DingDanLogId, DingDanId, DingDanLogTypeId, DingDanClassId, Memo, CreateTime  ");
            strSql.Append("  from CORE.dbo.DingDanLog ");
            strSql.Append(" where DingDanLogId=@DingDanLogId");
            SqlParameter[] parameters = {
					new SqlParameter("@DingDanLogId", SqlDbType.Decimal)
			};
            parameters[0].Value = DingDanLogId;


            DingDanLogModel model = new DingDanLogModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["DingDanLogId"].ToString() != "")
                {
                    model.DingDanLogId = decimal.Parse(ds.Tables[0].Rows[0]["DingDanLogId"].ToString());
                }
                model.DingDanId = ds.Tables[0].Rows[0]["DingDanId"].ToString();
                if (ds.Tables[0].Rows[0]["DingDanLogTypeId"].ToString() != "")
                {
                    model.DingDanLogTypeId = int.Parse(ds.Tables[0].Rows[0]["DingDanLogTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DingDanClassId"].ToString() != "")
                {
                    model.DingDanClassId = int.Parse(ds.Tables[0].Rows[0]["DingDanClassId"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
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
            strSql.Append("delete from CORE.dbo.DingDanLog ");
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
            strSql.Append(" FROM CORE.dbo.DingDanLog  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.DingDanLog  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.DingDanLogView  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

