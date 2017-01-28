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
    //OrderToWorkDetailVsClothesSize
    public partial class OrderToWorkDetailVsClothesSizeDAL
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
            strSql.Append(" FROM  CORE.dbo.OrderToWorkDetailVsClothesSizeView ");
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
        public bool Add(OrderToWorkDetailVsClothesSizeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.OrderToWorkDetailVsClothesSize (");
            strSql.Append("OrderToWorkDetailId,ClothesSizeId,Num,Memo,CheckNum,DoneNum,ChangeTime");
            strSql.Append(") values (");
            strSql.Append("@OrderToWorkDetailId,@ClothesSizeId,@Num,@Memo,@CheckNum,@DoneNum,@ChangeTime");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@OrderToWorkDetailId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ClothesSizeId", SqlDbType.Int,4) ,
                        new SqlParameter("@Num", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,200) ,
                        new SqlParameter("@CheckNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@DoneNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ChangeTime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.OrderToWorkDetailId;
            parameters[1].Value = model.ClothesSizeId;
            parameters[2].Value = model.Num;
            parameters[3].Value = model.Memo;
            parameters[4].Value = model.CheckNum;
            parameters[5].Value = model.DoneNum;
            parameters[6].Value = model.ChangeTime;

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
        public bool Update(OrderToWorkDetailVsClothesSizeModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.OrderToWorkDetailVsClothesSize set ");

            strSql.Append(" OrderToWorkDetailId = @OrderToWorkDetailId , ");
            strSql.Append(" ClothesSizeId = @ClothesSizeId , ");
            strSql.Append(" Num = @Num , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" CheckNum = @CheckNum , ");
            strSql.Append(" DoneNum = @DoneNum , ");
            strSql.Append(" ChangeTime = @ChangeTime  ");
            strSql.Append(" where OrderToWorkDetailId=@OrderToWorkDetailId and ClothesSizeId=@ClothesSizeId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@OrderToWorkDetailId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ClothesSizeId", SqlDbType.Int,4) ,
                        new SqlParameter("@Num", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,200) ,
                        new SqlParameter("@CheckNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@DoneNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ChangeTime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.OrderToWorkDetailId;
            parameters[1].Value = model.ClothesSizeId;
            parameters[2].Value = model.Num;
            parameters[3].Value = model.Memo;
            parameters[4].Value = model.CheckNum;
            parameters[5].Value = model.DoneNum;
            parameters[6].Value = model.ChangeTime; try
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
        public OrderToWorkDetailVsClothesSizeModel GetModel(decimal OrderToWorkDetailId, int ClothesSizeId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderToWorkDetailId, ClothesSizeId, Num, Memo, CheckNum, DoneNum, ChangeTime  ");
            strSql.Append("  from CORE.dbo.OrderToWorkDetailVsClothesSize ");
            strSql.Append(" where OrderToWorkDetailId=@OrderToWorkDetailId and ClothesSizeId=@ClothesSizeId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderToWorkDetailId", SqlDbType.Decimal,9),
                    new SqlParameter("@ClothesSizeId", SqlDbType.Int,4)         };
            parameters[0].Value = OrderToWorkDetailId;
            parameters[1].Value = ClothesSizeId;


            OrderToWorkDetailVsClothesSizeModel model = new OrderToWorkDetailVsClothesSizeModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["OrderToWorkDetailId"].ToString() != "")
                {
                    model.OrderToWorkDetailId = decimal.Parse(ds.Tables[0].Rows[0]["OrderToWorkDetailId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClothesSizeId"].ToString() != "")
                {
                    model.ClothesSizeId = int.Parse(ds.Tables[0].Rows[0]["ClothesSizeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Num"].ToString() != "")
                {
                    model.Num = decimal.Parse(ds.Tables[0].Rows[0]["Num"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                if (ds.Tables[0].Rows[0]["CheckNum"].ToString() != "")
                {
                    model.CheckNum = decimal.Parse(ds.Tables[0].Rows[0]["CheckNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DoneNum"].ToString() != "")
                {
                    model.DoneNum = decimal.Parse(ds.Tables[0].Rows[0]["DoneNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChangeTime"].ToString() != "")
                {
                    model.ChangeTime = DateTime.Parse(ds.Tables[0].Rows[0]["ChangeTime"].ToString());
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
            strSql.Append("delete from CORE.dbo.OrderToWorkDetailVsClothesSize ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.OrderToWorkDetailVsClothesSize  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.OrderToWorkDetailVsClothesSize  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.OrderToWorkDetailVsClothesSize  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.OrderToWorkDetailVsClothesSize  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

