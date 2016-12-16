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
    //PeiSongType
    public partial class PeiSongTypeDAL
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
            strSql.Append(" FROM  CORE.dbo.PeiSongType ");
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
        public bool Add(PeiSongTypeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.PeiSongType (");
            strSql.Append("PeiSongTime,PeiHuoTime,EndTimeForDay,BgTimeForDay,BranchId,PeiSongTypeName,MerId,DefaultPrice,FullPrice,PeiSongTypeMemo,OrderNo,PeiSongTeXing,SelDay");
            strSql.Append(") values (");
            strSql.Append("@PeiSongTime,@PeiHuoTime,@EndTimeForDay,@BgTimeForDay,@BranchId,@PeiSongTypeName,@MerId,@DefaultPrice,@FullPrice,@PeiSongTypeMemo,@OrderNo,@PeiSongTeXing,@SelDay");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@PeiSongTime", SqlDbType.Time,5) ,
                        new SqlParameter("@PeiHuoTime", SqlDbType.Time,5) ,
                        new SqlParameter("@EndTimeForDay", SqlDbType.Time,5) ,
                        new SqlParameter("@BgTimeForDay", SqlDbType.Time,5) ,
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PeiSongTypeName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@DefaultPrice", SqlDbType.Decimal,9) ,
                        new SqlParameter("@FullPrice", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PeiSongTypeMemo", SqlDbType.VarChar,1000) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@PeiSongTeXing", SqlDbType.Int,4) ,
                        new SqlParameter("@SelDay", SqlDbType.Int,4)

            };

            parameters[0].Value = model.PeiSongTime;
            parameters[1].Value = model.PeiHuoTime;
            parameters[2].Value = model.EndTimeForDay;
            parameters[3].Value = model.BgTimeForDay;
            parameters[4].Value = model.BranchId;
            parameters[5].Value = model.PeiSongTypeName;
            parameters[6].Value = model.MerId;
            parameters[7].Value = model.DefaultPrice;
            parameters[8].Value = model.FullPrice;
            parameters[9].Value = model.PeiSongTypeMemo;
            parameters[10].Value = model.OrderNo;
            parameters[11].Value = model.PeiSongTeXing;
            parameters[12].Value = model.SelDay;

            bool result = false;
            try
            {


                model.PeiSongTypeId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "PeiSongTypeId", parameters));


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
        public bool Update(PeiSongTypeModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.PeiSongType set ");

            strSql.Append(" PeiSongTime = @PeiSongTime , ");
            strSql.Append(" PeiHuoTime = @PeiHuoTime , ");
            strSql.Append(" EndTimeForDay = @EndTimeForDay , ");
            strSql.Append(" BgTimeForDay = @BgTimeForDay , ");
            strSql.Append(" BranchId = @BranchId , ");
            strSql.Append(" PeiSongTypeName = @PeiSongTypeName , ");
            strSql.Append(" MerId = @MerId , ");
            strSql.Append(" DefaultPrice = @DefaultPrice , ");
            strSql.Append(" FullPrice = @FullPrice , ");
            strSql.Append(" PeiSongTypeMemo = @PeiSongTypeMemo , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" PeiSongTeXing = @PeiSongTeXing , ");
            strSql.Append(" SelDay = @SelDay  ");
            strSql.Append(" where PeiSongTypeId=@PeiSongTypeId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@PeiSongTypeId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PeiSongTime", SqlDbType.Time,5) ,
                        new SqlParameter("@PeiHuoTime", SqlDbType.Time,5) ,
                        new SqlParameter("@EndTimeForDay", SqlDbType.Time,5) ,
                        new SqlParameter("@BgTimeForDay", SqlDbType.Time,5) ,
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PeiSongTypeName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@DefaultPrice", SqlDbType.Decimal,9) ,
                        new SqlParameter("@FullPrice", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PeiSongTypeMemo", SqlDbType.VarChar,1000) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@PeiSongTeXing", SqlDbType.Int,4) ,
                        new SqlParameter("@SelDay", SqlDbType.Int,4)

            };

            parameters[0].Value = model.PeiSongTypeId;
            parameters[1].Value = model.PeiSongTime;
            parameters[2].Value = model.PeiHuoTime;
            parameters[3].Value = model.EndTimeForDay;
            parameters[4].Value = model.BgTimeForDay;
            parameters[5].Value = model.BranchId;
            parameters[6].Value = model.PeiSongTypeName;
            parameters[7].Value = model.MerId;
            parameters[8].Value = model.DefaultPrice;
            parameters[9].Value = model.FullPrice;
            parameters[10].Value = model.PeiSongTypeMemo;
            parameters[11].Value = model.OrderNo;
            parameters[12].Value = model.PeiSongTeXing;
            parameters[13].Value = model.SelDay; try
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
        public PeiSongTypeModel GetModel(decimal PeiSongTypeId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PeiSongTypeId, PeiSongTime, PeiHuoTime, EndTimeForDay, BgTimeForDay, BranchId, PeiSongTypeName, MerId, DefaultPrice, FullPrice, PeiSongTypeMemo, OrderNo, PeiSongTeXing, SelDay  ");
            strSql.Append("  from CORE.dbo.PeiSongType ");
            strSql.Append(" where PeiSongTypeId=@PeiSongTypeId");
            SqlParameter[] parameters = {
                    new SqlParameter("@PeiSongTypeId", SqlDbType.Decimal)
            };
            parameters[0].Value = PeiSongTypeId;


            PeiSongTypeModel model = new PeiSongTypeModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PeiSongTypeId"].ToString() != "")
                {
                    model.PeiSongTypeId = decimal.Parse(ds.Tables[0].Rows[0]["PeiSongTypeId"].ToString());
                }
                model.BranchId = ds.Tables[0].Rows[0]["BranchId"].ToString();
                model.PeiSongTypeName = ds.Tables[0].Rows[0]["PeiSongTypeName"].ToString();
                if (ds.Tables[0].Rows[0]["MerId"].ToString() != "")
                {
                    model.MerId = decimal.Parse(ds.Tables[0].Rows[0]["MerId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DefaultPrice"].ToString() != "")
                {
                    model.DefaultPrice = decimal.Parse(ds.Tables[0].Rows[0]["DefaultPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FullPrice"].ToString() != "")
                {
                    model.FullPrice = decimal.Parse(ds.Tables[0].Rows[0]["FullPrice"].ToString());
                }
                model.PeiSongTypeMemo = ds.Tables[0].Rows[0]["PeiSongTypeMemo"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PeiSongTeXing"].ToString() != "")
                {
                    model.PeiSongTeXing = int.Parse(ds.Tables[0].Rows[0]["PeiSongTeXing"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SelDay"].ToString() != "")
                {
                    model.SelDay = int.Parse(ds.Tables[0].Rows[0]["SelDay"].ToString());
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
            strSql.Append("delete from CORE.dbo.PeiSongType ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.PeiSongType  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.PeiSongType  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.PeiSongType  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.PeiSongType  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

