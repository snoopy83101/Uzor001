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
    //Branch
    public partial class BranchDAL
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
            strSql.Append(" FROM  CORE.dbo.Branch ");
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
        public bool Add(BranchModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.Branch (");
            strSql.Append("BranchId,Invalid,BranchTel,BranchPhoneNo,QQ,QQUrl,BranchName,BranchVsId,MerchantId,BranchMemo,Lng,Lat,ImgId,OrderNo");
            strSql.Append(") values (");
            strSql.Append("@BranchId,@Invalid,@BranchTel,@BranchPhoneNo,@QQ,@QQUrl,@BranchName,@BranchVsId,@MerchantId,@BranchMemo,@Lng,@Lat,@ImgId,@OrderNo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@BranchTel", SqlDbType.VarChar,50) ,
                        new SqlParameter("@BranchPhoneNo", SqlDbType.VarChar,50) ,
                        new SqlParameter("@QQ", SqlDbType.VarChar,50) ,
                        new SqlParameter("@QQUrl", SqlDbType.VarChar,250) ,
                        new SqlParameter("@BranchName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@BranchVsId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BranchMemo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@Lng", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Lat", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ImgId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)

            };

            parameters[0].Value = model.BranchId;
            parameters[1].Value = model.Invalid;
            parameters[2].Value = model.BranchTel;
            parameters[3].Value = model.BranchPhoneNo;
            parameters[4].Value = model.QQ;
            parameters[5].Value = model.QQUrl;
            parameters[6].Value = model.BranchName;
            parameters[7].Value = model.BranchVsId;
            parameters[8].Value = model.MerchantId;
            parameters[9].Value = model.BranchMemo;
            parameters[10].Value = model.Lng;
            parameters[11].Value = model.Lat;
            parameters[12].Value = model.ImgId;
            parameters[13].Value = model.OrderNo;

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
        public bool Update(BranchModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.Branch set ");

            strSql.Append(" BranchId = @BranchId , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" BranchTel = @BranchTel , ");
            strSql.Append(" BranchPhoneNo = @BranchPhoneNo , ");
            strSql.Append(" QQ = @QQ , ");
            strSql.Append(" QQUrl = @QQUrl , ");
            strSql.Append(" BranchName = @BranchName , ");
            strSql.Append(" BranchVsId = @BranchVsId , ");
            strSql.Append(" MerchantId = @MerchantId , ");
            strSql.Append(" BranchMemo = @BranchMemo , ");
            strSql.Append(" Lng = @Lng , ");
            strSql.Append(" Lat = @Lat , ");
            strSql.Append(" ImgId = @ImgId , ");
            strSql.Append(" OrderNo = @OrderNo  ");
            strSql.Append(" where BranchId=@BranchId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@BranchTel", SqlDbType.VarChar,50) ,
                        new SqlParameter("@BranchPhoneNo", SqlDbType.VarChar,50) ,
                        new SqlParameter("@QQ", SqlDbType.VarChar,50) ,
                        new SqlParameter("@QQUrl", SqlDbType.VarChar,250) ,
                        new SqlParameter("@BranchName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@BranchVsId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BranchMemo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@Lng", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Lat", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ImgId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)

            };

            parameters[0].Value = model.BranchId;
            parameters[1].Value = model.Invalid;
            parameters[2].Value = model.BranchTel;
            parameters[3].Value = model.BranchPhoneNo;
            parameters[4].Value = model.QQ;
            parameters[5].Value = model.QQUrl;
            parameters[6].Value = model.BranchName;
            parameters[7].Value = model.BranchVsId;
            parameters[8].Value = model.MerchantId;
            parameters[9].Value = model.BranchMemo;
            parameters[10].Value = model.Lng;
            parameters[11].Value = model.Lat;
            parameters[12].Value = model.ImgId;
            parameters[13].Value = model.OrderNo; try
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
        public BranchModel GetModel(string BranchId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BranchId, Invalid, BranchTel, BranchPhoneNo, QQ, QQUrl, BranchName, BranchVsId, MerchantId, BranchMemo, Lng, Lat, ImgId, OrderNo  ");
            strSql.Append("  from CORE.dbo.Branch ");
            strSql.Append(" where BranchId=@BranchId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@BranchId", SqlDbType.VarChar,50)         };
            parameters[0].Value = BranchId;


            BranchModel model = new BranchModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.BranchId = ds.Tables[0].Rows[0]["BranchId"].ToString();
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
                model.BranchTel = ds.Tables[0].Rows[0]["BranchTel"].ToString();
                model.BranchPhoneNo = ds.Tables[0].Rows[0]["BranchPhoneNo"].ToString();
                model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                model.QQUrl = ds.Tables[0].Rows[0]["QQUrl"].ToString();
                model.BranchName = ds.Tables[0].Rows[0]["BranchName"].ToString();
                model.BranchVsId = ds.Tables[0].Rows[0]["BranchVsId"].ToString();
                if (ds.Tables[0].Rows[0]["MerchantId"].ToString() != "")
                {
                    model.MerchantId = decimal.Parse(ds.Tables[0].Rows[0]["MerchantId"].ToString());
                }
                model.BranchMemo = ds.Tables[0].Rows[0]["BranchMemo"].ToString();
                if (ds.Tables[0].Rows[0]["Lng"].ToString() != "")
                {
                    model.Lng = decimal.Parse(ds.Tables[0].Rows[0]["Lng"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lat"].ToString() != "")
                {
                    model.Lat = decimal.Parse(ds.Tables[0].Rows[0]["Lat"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ImgId"].ToString() != "")
                {
                    model.ImgId = decimal.Parse(ds.Tables[0].Rows[0]["ImgId"].ToString());
                }
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
            strSql.Append("delete from CORE.dbo.Branch ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.Branch  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.Branch  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.Branch  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.Branch  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

