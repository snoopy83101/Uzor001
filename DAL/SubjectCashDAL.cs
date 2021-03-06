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
    //SubjectCash
    public partial class SubjectCashDAL
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
            strSql.Append(" FROM  CORE.dbo.SubjectCash ");
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
        public bool Add(SubjectCashModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.SubjectCash (");
            strSql.Append("MemberId,CreateTime,Amount,SubjectCashStatusId,Memo,DoneTime,MemberBankCardId");
            strSql.Append(") values (");
            strSql.Append("@MemberId,@CreateTime,@Amount,@SubjectCashStatusId,@Memo,@DoneTime,@MemberBankCardId");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Amount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SubjectCashStatusId", SqlDbType.Int,4) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@DoneTime", SqlDbType.DateTime) ,
                        new SqlParameter("@MemberBankCardId", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.MemberId;
            parameters[1].Value = model.CreateTime;
            parameters[2].Value = model.Amount;
            parameters[3].Value = model.SubjectCashStatusId;
            parameters[4].Value = model.Memo;
            parameters[5].Value = model.DoneTime;
            parameters[6].Value = model.MemberBankCardId;

            bool result = false;
            try
            {


                model.SubjectCashId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "SubjectCashId", parameters));


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
        public bool Update(SubjectCashModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.SubjectCash set ");

            strSql.Append(" MemberId = @MemberId , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" Amount = @Amount , ");
            strSql.Append(" SubjectCashStatusId = @SubjectCashStatusId , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" DoneTime = @DoneTime , ");
            strSql.Append(" MemberBankCardId = @MemberBankCardId  ");
            strSql.Append(" where SubjectCashId=@SubjectCashId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@SubjectCashId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Amount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SubjectCashStatusId", SqlDbType.Int,4) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@DoneTime", SqlDbType.DateTime) ,
                        new SqlParameter("@MemberBankCardId", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.SubjectCashId;
            parameters[1].Value = model.MemberId;
            parameters[2].Value = model.CreateTime;
            parameters[3].Value = model.Amount;
            parameters[4].Value = model.SubjectCashStatusId;
            parameters[5].Value = model.Memo;
            parameters[6].Value = model.DoneTime;
            parameters[7].Value = model.MemberBankCardId; try
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
        public SubjectCashModel GetModel(decimal SubjectCashId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SubjectCashId, MemberId, CreateTime, Amount, SubjectCashStatusId, Memo, DoneTime, MemberBankCardId  ");
            strSql.Append("  from CORE.dbo.SubjectCash ");
            strSql.Append(" where SubjectCashId=@SubjectCashId");
            SqlParameter[] parameters = {
                    new SqlParameter("@SubjectCashId", SqlDbType.Decimal)
            };
            parameters[0].Value = SubjectCashId;


            SubjectCashModel model = new SubjectCashModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SubjectCashId"].ToString() != "")
                {
                    model.SubjectCashId = decimal.Parse(ds.Tables[0].Rows[0]["SubjectCashId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberId"].ToString() != "")
                {
                    model.MemberId = decimal.Parse(ds.Tables[0].Rows[0]["MemberId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    model.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SubjectCashStatusId"].ToString() != "")
                {
                    model.SubjectCashStatusId = int.Parse(ds.Tables[0].Rows[0]["SubjectCashStatusId"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                if (ds.Tables[0].Rows[0]["DoneTime"].ToString() != "")
                {
                    model.DoneTime = DateTime.Parse(ds.Tables[0].Rows[0]["DoneTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberBankCardId"].ToString() != "")
                {
                    model.MemberBankCardId = decimal.Parse(ds.Tables[0].Rows[0]["MemberBankCardId"].ToString());
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
            strSql.Append("delete from CORE.dbo.SubjectCash ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.SubjectCashView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.SubjectCash  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.SubjectCash  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.SubjectCash  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

