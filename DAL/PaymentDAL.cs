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
    //Payment
    public partial class PaymentDAL
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
            strSql.Append(" FROM  CORE.dbo.Payment ");
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
        public bool Add(PaymentModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.Payment (");
            strSql.Append("PaymentTitle,PaymentMemo,MemberId,CreateTime,PaymentType,ReKey,PayAmount,PayTypeId");
            strSql.Append(") values (");
            strSql.Append("@PaymentTitle,@PaymentMemo,@MemberId,@CreateTime,@PaymentType,@ReKey,@PayAmount,@PayTypeId");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@PaymentTitle", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PaymentMemo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@PaymentType", SqlDbType.VarChar,10) ,
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PayAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PayTypeId", SqlDbType.Int,4)

            };

            parameters[0].Value = model.PaymentTitle;
            parameters[1].Value = model.PaymentMemo;
            parameters[2].Value = model.MemberId;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.PaymentType;
            parameters[5].Value = model.ReKey;
            parameters[6].Value = model.PayAmount;
            parameters[7].Value = model.PayTypeId;

            bool result = false;
            try
            {


                model.PaymentId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "PaymentId", parameters));


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
        public bool Update(PaymentModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.Payment set ");

            strSql.Append(" PaymentTitle = @PaymentTitle , ");
            strSql.Append(" PaymentMemo = @PaymentMemo , ");
            strSql.Append(" MemberId = @MemberId , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" PaymentType = @PaymentType , ");
            strSql.Append(" ReKey = @ReKey , ");
            strSql.Append(" PayAmount = @PayAmount , ");
            strSql.Append(" PayTypeId = @PayTypeId  ");
            strSql.Append(" where PaymentId=@PaymentId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@PaymentId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PaymentTitle", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PaymentMemo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@PaymentType", SqlDbType.VarChar,10) ,
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PayAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PayTypeId", SqlDbType.Int,4)

            };

            parameters[0].Value = model.PaymentId;
            parameters[1].Value = model.PaymentTitle;
            parameters[2].Value = model.PaymentMemo;
            parameters[3].Value = model.MemberId;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.PaymentType;
            parameters[6].Value = model.ReKey;
            parameters[7].Value = model.PayAmount;
            parameters[8].Value = model.PayTypeId; try
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
        public PaymentModel GetModel(decimal PaymentId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PaymentId, PaymentTitle, PaymentMemo, MemberId, CreateTime, PaymentType, ReKey, PayAmount, PayTypeId  ");
            strSql.Append("  from CORE.dbo.Payment ");
            strSql.Append(" where PaymentId=@PaymentId");
            SqlParameter[] parameters = {
                    new SqlParameter("@PaymentId", SqlDbType.Decimal)
            };
            parameters[0].Value = PaymentId;


            PaymentModel model = new PaymentModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PaymentId"].ToString() != "")
                {
                    model.PaymentId = decimal.Parse(ds.Tables[0].Rows[0]["PaymentId"].ToString());
                }
                model.PaymentTitle = ds.Tables[0].Rows[0]["PaymentTitle"].ToString();
                model.PaymentMemo = ds.Tables[0].Rows[0]["PaymentMemo"].ToString();
                if (ds.Tables[0].Rows[0]["MemberId"].ToString() != "")
                {
                    model.MemberId = decimal.Parse(ds.Tables[0].Rows[0]["MemberId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.PaymentType = ds.Tables[0].Rows[0]["PaymentType"].ToString();
                model.ReKey = ds.Tables[0].Rows[0]["ReKey"].ToString();
                if (ds.Tables[0].Rows[0]["PayAmount"].ToString() != "")
                {
                    model.PayAmount = decimal.Parse(ds.Tables[0].Rows[0]["PayAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PayTypeId"].ToString() != "")
                {
                    model.PayTypeId = int.Parse(ds.Tables[0].Rows[0]["PayTypeId"].ToString());
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
            strSql.Append("delete from CORE.dbo.Payment ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.Payment  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.Payment  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.Payment  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.Payment  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

