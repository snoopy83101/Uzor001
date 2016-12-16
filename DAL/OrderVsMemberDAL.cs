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
    //OrderVsMember
    public partial class OrderVsMemberDAL
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
            strSql.Append(" FROM  CORE.dbo.OrderVsMember ");
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
        public bool Add(OrderVsMemberModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.OrderVsMember (");
            strSql.Append("OrderId,MemberId,CreateTime,VsStatus,Invalid,VsType,Memo,VsPlaces");
            strSql.Append(") values (");
            strSql.Append("@OrderId,@MemberId,@CreateTime,@VsStatus,@Invalid,@VsType,@Memo,@VsPlaces");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@OrderId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@VsStatus", SqlDbType.Int,4) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@VsType", SqlDbType.Int,4) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@VsPlaces", SqlDbType.Int,4)

            };

            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.MemberId;
            parameters[2].Value = model.CreateTime;
            parameters[3].Value = model.VsStatus;
            parameters[4].Value = model.Invalid;
            parameters[5].Value = model.VsType;
            parameters[6].Value = model.Memo;
            parameters[7].Value = model.VsPlaces;

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
        public bool Update(OrderVsMemberModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.OrderVsMember set ");

            strSql.Append(" OrderId = @OrderId , ");
            strSql.Append(" MemberId = @MemberId , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" VsStatus = @VsStatus , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" VsType = @VsType , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" VsPlaces = @VsPlaces  ");
            strSql.Append(" where OrderId=@OrderId and MemberId=@MemberId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@OrderId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@VsStatus", SqlDbType.Int,4) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@VsType", SqlDbType.Int,4) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@VsPlaces", SqlDbType.Int,4)

            };

            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.MemberId;
            parameters[2].Value = model.CreateTime;
            parameters[3].Value = model.VsStatus;
            parameters[4].Value = model.Invalid;
            parameters[5].Value = model.VsType;
            parameters[6].Value = model.Memo;
            parameters[7].Value = model.VsPlaces; try
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
        public OrderVsMemberModel GetModel(string OrderId, decimal MemberId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderId, MemberId, CreateTime, VsStatus, Invalid, VsType, Memo, VsPlaces  ");
            strSql.Append("  from CORE.dbo.OrderVsMember ");
            strSql.Append(" where OrderId=@OrderId and MemberId=@MemberId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderId", SqlDbType.VarChar,50),
                    new SqlParameter("@MemberId", SqlDbType.Decimal,9)          };
            parameters[0].Value = OrderId;
            parameters[1].Value = MemberId;


            OrderVsMemberModel model = new OrderVsMemberModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.OrderId = ds.Tables[0].Rows[0]["OrderId"].ToString();
                if (ds.Tables[0].Rows[0]["MemberId"].ToString() != "")
                {
                    model.MemberId = decimal.Parse(ds.Tables[0].Rows[0]["MemberId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VsStatus"].ToString() != "")
                {
                    model.VsStatus = int.Parse(ds.Tables[0].Rows[0]["VsStatus"].ToString());
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
                if (ds.Tables[0].Rows[0]["VsType"].ToString() != "")
                {
                    model.VsType = int.Parse(ds.Tables[0].Rows[0]["VsType"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                if (ds.Tables[0].Rows[0]["VsPlaces"].ToString() != "")
                {
                    model.VsPlaces = int.Parse(ds.Tables[0].Rows[0]["VsPlaces"].ToString());
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
            strSql.Append("delete from CORE.dbo.OrderVsMember ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.OrderVsMemberView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.OrderVsMember  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.OrderVsMember  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.OrderVsMember  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

