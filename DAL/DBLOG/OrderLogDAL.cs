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
    //OrderLog
    public partial class OrderLogDAL
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
            strSql.Append(" FROM  DBLOG.dbo.OrderLog ");
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
        public bool Add(OrderLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DBLOG.dbo.OrderLog (");
            strSql.Append("MemberId,OrderLogTitle,OrderId,OrderToWorkId,CreateTime,ReKey,ReKey2,OrderLogClassId,UserId");
            strSql.Append(") values (");
            strSql.Append("@MemberId,@OrderLogTitle,@OrderId,@OrderToWorkId,@CreateTime,@ReKey,@ReKey2,@OrderLogClassId,@UserId");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OrderLogTitle", SqlDbType.VarChar,500) ,
                        new SqlParameter("@OrderId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderToWorkId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ReKey2", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderLogClassId", SqlDbType.Int,4) ,
                        new SqlParameter("@UserId", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.MemberId;
            parameters[1].Value = model.OrderLogTitle;
            parameters[2].Value = model.OrderId;
            parameters[3].Value = model.OrderToWorkId;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.ReKey;
            parameters[6].Value = model.ReKey2;
            parameters[7].Value = model.OrderLogClassId;
            parameters[8].Value = model.UserId;

            bool result = false;
            try
            {


                model.OrderLogId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "OrderLogId", parameters));


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
        public bool Update(OrderLogModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DBLOG.dbo.OrderLog set ");

            strSql.Append(" MemberId = @MemberId , ");
            strSql.Append(" OrderLogTitle = @OrderLogTitle , ");
            strSql.Append(" OrderId = @OrderId , ");
            strSql.Append(" OrderToWorkId = @OrderToWorkId , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" ReKey = @ReKey , ");
            strSql.Append(" ReKey2 = @ReKey2 , ");
            strSql.Append(" OrderLogClassId = @OrderLogClassId , ");
            strSql.Append(" UserId = @UserId  ");
            strSql.Append(" where OrderLogId=@OrderLogId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@OrderLogId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OrderLogTitle", SqlDbType.VarChar,500) ,
                        new SqlParameter("@OrderId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderToWorkId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ReKey2", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderLogClassId", SqlDbType.Int,4) ,
                        new SqlParameter("@UserId", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.OrderLogId;
            parameters[1].Value = model.MemberId;
            parameters[2].Value = model.OrderLogTitle;
            parameters[3].Value = model.OrderId;
            parameters[4].Value = model.OrderToWorkId;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.ReKey;
            parameters[7].Value = model.ReKey2;
            parameters[8].Value = model.OrderLogClassId;
            parameters[9].Value = model.UserId; try
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
        public OrderLogModel GetModel(decimal OrderLogId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderLogId, MemberId, OrderLogTitle, OrderId, OrderToWorkId, CreateTime, ReKey, ReKey2, OrderLogClassId, UserId  ");
            strSql.Append("  from DBLOG.dbo.OrderLog ");
            strSql.Append(" where OrderLogId=@OrderLogId");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderLogId", SqlDbType.Decimal)
            };
            parameters[0].Value = OrderLogId;


            OrderLogModel model = new OrderLogModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["OrderLogId"].ToString() != "")
                {
                    model.OrderLogId = decimal.Parse(ds.Tables[0].Rows[0]["OrderLogId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberId"].ToString() != "")
                {
                    model.MemberId = decimal.Parse(ds.Tables[0].Rows[0]["MemberId"].ToString());
                }
                model.OrderLogTitle = ds.Tables[0].Rows[0]["OrderLogTitle"].ToString();
                model.OrderId = ds.Tables[0].Rows[0]["OrderId"].ToString();
                if (ds.Tables[0].Rows[0]["OrderToWorkId"].ToString() != "")
                {
                    model.OrderToWorkId = decimal.Parse(ds.Tables[0].Rows[0]["OrderToWorkId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.ReKey = ds.Tables[0].Rows[0]["ReKey"].ToString();
                model.ReKey2 = ds.Tables[0].Rows[0]["ReKey2"].ToString();
                if (ds.Tables[0].Rows[0]["OrderLogClassId"].ToString() != "")
                {
                    model.OrderLogClassId = int.Parse(ds.Tables[0].Rows[0]["OrderLogClassId"].ToString());
                }
                model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();

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
            strSql.Append("delete from DBLOG.dbo.OrderLog ");
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
            object[] fenyeParmValue = new object[] { "DBLOG.dbo.OrderLogView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM DBLOG.dbo.OrderLog  WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("DBLOG.dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM DBLOG.dbo.OrderLog  WITH(NOLOCK) ");
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
            strSql.Append(" FROM DBLOG.dbo.OrderLog  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

