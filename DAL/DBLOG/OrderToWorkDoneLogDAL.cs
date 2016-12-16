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
    //OrderToWorkDoneLog
    public partial class OrderToWorkDoneLogDAL
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
            strSql.Append(" FROM  DBLOG.dbo.OrderToWorkDoneLog ");
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
        public bool Add(OrderToWorkDoneLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DBLOG.dbo.OrderToWorkDoneLog (");
            strSql.Append("OrderToWorkDetailId,ClothesSizeId,ChangeDoneNum,OldDoneNum,CreatTime,DoneLogTypeId,ReKey");
            strSql.Append(") values (");
            strSql.Append("@OrderToWorkDetailId,@ClothesSizeId,@ChangeDoneNum,@OldDoneNum,@CreatTime,@DoneLogTypeId,@ReKey");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@OrderToWorkDetailId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ClothesSizeId", SqlDbType.Int,4) ,
                        new SqlParameter("@ChangeDoneNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OldDoneNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreatTime", SqlDbType.DateTime) ,
                        new SqlParameter("@DoneLogTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.OrderToWorkDetailId;
            parameters[1].Value = model.ClothesSizeId;
            parameters[2].Value = model.ChangeDoneNum;
            parameters[3].Value = model.OldDoneNum;
            parameters[4].Value = model.CreatTime;
            parameters[5].Value = model.DoneLogTypeId;
            parameters[6].Value = model.ReKey;

            bool result = false;
            try
            {


                model.OrderToWorkDoneLogId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "OrderToWorkDoneLogId", parameters));


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
        public bool Update(OrderToWorkDoneLogModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DBLOG.dbo.OrderToWorkDoneLog set ");

            strSql.Append(" OrderToWorkDetailId = @OrderToWorkDetailId , ");
            strSql.Append(" ClothesSizeId = @ClothesSizeId , ");
            strSql.Append(" ChangeDoneNum = @ChangeDoneNum , ");
            strSql.Append(" OldDoneNum = @OldDoneNum , ");
            strSql.Append(" CreatTime = @CreatTime , ");
            strSql.Append(" DoneLogTypeId = @DoneLogTypeId , ");
            strSql.Append(" ReKey = @ReKey  ");
            strSql.Append(" where OrderToWorkDoneLogId=@OrderToWorkDoneLogId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@OrderToWorkDoneLogId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OrderToWorkDetailId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ClothesSizeId", SqlDbType.Int,4) ,
                        new SqlParameter("@ChangeDoneNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OldDoneNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreatTime", SqlDbType.DateTime) ,
                        new SqlParameter("@DoneLogTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.OrderToWorkDoneLogId;
            parameters[1].Value = model.OrderToWorkDetailId;
            parameters[2].Value = model.ClothesSizeId;
            parameters[3].Value = model.ChangeDoneNum;
            parameters[4].Value = model.OldDoneNum;
            parameters[5].Value = model.CreatTime;
            parameters[6].Value = model.DoneLogTypeId;
            parameters[7].Value = model.ReKey; try
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
        public OrderToWorkDoneLogModel GetModel(decimal OrderToWorkDoneLogId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderToWorkDoneLogId, OrderToWorkDetailId, ClothesSizeId, ChangeDoneNum, OldDoneNum, CreatTime, DoneLogTypeId, ReKey  ");
            strSql.Append("  from DBLOG.dbo.OrderToWorkDoneLog ");
            strSql.Append(" where OrderToWorkDoneLogId=@OrderToWorkDoneLogId");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderToWorkDoneLogId", SqlDbType.Decimal)
            };
            parameters[0].Value = OrderToWorkDoneLogId;


            OrderToWorkDoneLogModel model = new OrderToWorkDoneLogModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["OrderToWorkDoneLogId"].ToString() != "")
                {
                    model.OrderToWorkDoneLogId = decimal.Parse(ds.Tables[0].Rows[0]["OrderToWorkDoneLogId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderToWorkDetailId"].ToString() != "")
                {
                    model.OrderToWorkDetailId = decimal.Parse(ds.Tables[0].Rows[0]["OrderToWorkDetailId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClothesSizeId"].ToString() != "")
                {
                    model.ClothesSizeId = int.Parse(ds.Tables[0].Rows[0]["ClothesSizeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChangeDoneNum"].ToString() != "")
                {
                    model.ChangeDoneNum = decimal.Parse(ds.Tables[0].Rows[0]["ChangeDoneNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OldDoneNum"].ToString() != "")
                {
                    model.OldDoneNum = decimal.Parse(ds.Tables[0].Rows[0]["OldDoneNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatTime"].ToString() != "")
                {
                    model.CreatTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DoneLogTypeId"].ToString() != "")
                {
                    model.DoneLogTypeId = int.Parse(ds.Tables[0].Rows[0]["DoneLogTypeId"].ToString());
                }
                model.ReKey = ds.Tables[0].Rows[0]["ReKey"].ToString();

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
            strSql.Append("delete from DBLOG.dbo.OrderToWorkDoneLog ");
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
            object[] fenyeParmValue = new object[] { "DBLOG.dbo.OrderToWorkDoneLog  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM DBLOG.dbo.OrderToWorkDoneLog  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM DBLOG.dbo.OrderToWorkDoneLog  WITH(NOLOCK) ");
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
            strSql.Append(" FROM DBLOG.dbo.OrderToWorkDoneLog  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

