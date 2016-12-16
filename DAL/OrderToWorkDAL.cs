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
    //OrderToWork
    public partial class OrderToWorkDAL
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
            strSql.Append(" FROM  CORE.dbo.OrderToWork ");
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
        public bool Add(OrderToWorkModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.OrderToWork (");
            strSql.Append("Wages,CreateUserId,LimitTime,ReceivedTime,OrderToWorkTitle,OrderId,DoneTime,CreateTime,ReceivedImgId,MemberId,Invalid,OrderToWorkStatusId");
            strSql.Append(") values (");
            strSql.Append("@Wages,@CreateUserId,@LimitTime,@ReceivedTime,@OrderToWorkTitle,@OrderId,@DoneTime,@CreateTime,@ReceivedImgId,@MemberId,@Invalid,@OrderToWorkStatusId");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@Wages", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateUserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@LimitTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ReceivedTime", SqlDbType.DateTime) ,
                        new SqlParameter("@OrderToWorkTitle", SqlDbType.VarChar,200) ,
                        new SqlParameter("@OrderId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DoneTime", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ReceivedImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@OrderToWorkStatusId", SqlDbType.Int,4)

            };

            parameters[0].Value = model.Wages;
            parameters[1].Value = model.CreateUserId;
            parameters[2].Value = model.LimitTime;
            parameters[3].Value = model.ReceivedTime;
            parameters[4].Value = model.OrderToWorkTitle;
            parameters[5].Value = model.OrderId;
            parameters[6].Value = model.DoneTime;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.ReceivedImgId;
            parameters[9].Value = model.MemberId;
            parameters[10].Value = model.Invalid;
            parameters[11].Value = model.OrderToWorkStatusId;

            bool result = false;
            try
            {


                model.OrderToWorkId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "OrderToWorkId", parameters));


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
        public bool Update(OrderToWorkModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.OrderToWork set ");

            strSql.Append(" Wages = @Wages , ");
            strSql.Append(" CreateUserId = @CreateUserId , ");
            strSql.Append(" LimitTime = @LimitTime , ");
            strSql.Append(" ReceivedTime = @ReceivedTime , ");
            strSql.Append(" OrderToWorkTitle = @OrderToWorkTitle , ");
            strSql.Append(" OrderId = @OrderId , ");
            strSql.Append(" DoneTime = @DoneTime , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" ReceivedImgId = @ReceivedImgId , ");
            strSql.Append(" MemberId = @MemberId , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" OrderToWorkStatusId = @OrderToWorkStatusId  ");
            strSql.Append(" where OrderToWorkId=@OrderToWorkId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@OrderToWorkId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Wages", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateUserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@LimitTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ReceivedTime", SqlDbType.DateTime) ,
                        new SqlParameter("@OrderToWorkTitle", SqlDbType.VarChar,200) ,
                        new SqlParameter("@OrderId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DoneTime", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ReceivedImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@OrderToWorkStatusId", SqlDbType.Int,4)

            };

            parameters[0].Value = model.OrderToWorkId;
            parameters[1].Value = model.Wages;
            parameters[2].Value = model.CreateUserId;
            parameters[3].Value = model.LimitTime;
            parameters[4].Value = model.ReceivedTime;
            parameters[5].Value = model.OrderToWorkTitle;
            parameters[6].Value = model.OrderId;
            parameters[7].Value = model.DoneTime;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.ReceivedImgId;
            parameters[10].Value = model.MemberId;
            parameters[11].Value = model.Invalid;
            parameters[12].Value = model.OrderToWorkStatusId; try
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
        public OrderToWorkModel GetModel(decimal OrderToWorkId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderToWorkId, Wages, CreateUserId, LimitTime, ReceivedTime, OrderToWorkTitle, OrderId, DoneTime, CreateTime, ReceivedImgId, MemberId, Invalid, OrderToWorkStatusId  ");
            strSql.Append("  from CORE.dbo.OrderToWork ");
            strSql.Append(" where OrderToWorkId=@OrderToWorkId");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderToWorkId", SqlDbType.Decimal)
            };
            parameters[0].Value = OrderToWorkId;


            OrderToWorkModel model = new OrderToWorkModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["OrderToWorkId"].ToString() != "")
                {
                    model.OrderToWorkId = decimal.Parse(ds.Tables[0].Rows[0]["OrderToWorkId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Wages"].ToString() != "")
                {
                    model.Wages = decimal.Parse(ds.Tables[0].Rows[0]["Wages"].ToString());
                }
                model.CreateUserId = ds.Tables[0].Rows[0]["CreateUserId"].ToString();
                if (ds.Tables[0].Rows[0]["LimitTime"].ToString() != "")
                {
                    model.LimitTime = DateTime.Parse(ds.Tables[0].Rows[0]["LimitTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReceivedTime"].ToString() != "")
                {
                    model.ReceivedTime = DateTime.Parse(ds.Tables[0].Rows[0]["ReceivedTime"].ToString());
                }
                model.OrderToWorkTitle = ds.Tables[0].Rows[0]["OrderToWorkTitle"].ToString();
                model.OrderId = ds.Tables[0].Rows[0]["OrderId"].ToString();
                if (ds.Tables[0].Rows[0]["DoneTime"].ToString() != "")
                {
                    model.DoneTime = DateTime.Parse(ds.Tables[0].Rows[0]["DoneTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.ReceivedImgId = ds.Tables[0].Rows[0]["ReceivedImgId"].ToString();
                if (ds.Tables[0].Rows[0]["MemberId"].ToString() != "")
                {
                    model.MemberId = decimal.Parse(ds.Tables[0].Rows[0]["MemberId"].ToString());
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
                if (ds.Tables[0].Rows[0]["OrderToWorkStatusId"].ToString() != "")
                {
                    model.OrderToWorkStatusId = int.Parse(ds.Tables[0].Rows[0]["OrderToWorkStatusId"].ToString());
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
            strSql.Append("delete from CORE.dbo.OrderToWork ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.OrderToWorkView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.OrderToWork  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.OrderToWork  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.OrderToWork  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

