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
    //MemberAmountDetail
    public partial class MemberAmountDetailDAL
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
            strSql.Append(" FROM  CORE.dbo.MemberAmountDetail ");
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
        public bool Add(MemberAmountDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.MemberAmountDetail (");
            strSql.Append("Memo,MemberAmountChangeTypeId,OldAmount,NewAmount,MemberId,UserId,ChangeAmount,CreateTime,ReKey,ReKey2");
            strSql.Append(") values (");
            strSql.Append("@Memo,@MemberAmountChangeTypeId,@OldAmount,@NewAmount,@MemberId,@UserId,@ChangeAmount,@CreateTime,@ReKey,@ReKey2");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@MemberAmountChangeTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@OldAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@NewAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ChangeAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ReKey2", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.Memo;
            parameters[1].Value = model.MemberAmountChangeTypeId;
            parameters[2].Value = model.OldAmount;
            parameters[3].Value = model.NewAmount;
            parameters[4].Value = model.MemberId;
            parameters[5].Value = model.UserId;
            parameters[6].Value = model.ChangeAmount;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.ReKey;
            parameters[9].Value = model.ReKey2;

            bool result = false;
            try
            {


                model.MemberAmountDetailId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "MemberAmountDetailId", parameters));


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
        public bool Update(MemberAmountDetailModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.MemberAmountDetail set ");

            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" MemberAmountChangeTypeId = @MemberAmountChangeTypeId , ");
            strSql.Append(" OldAmount = @OldAmount , ");
            strSql.Append(" NewAmount = @NewAmount , ");
            strSql.Append(" MemberId = @MemberId , ");
            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" ChangeAmount = @ChangeAmount , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" ReKey = @ReKey , ");
            strSql.Append(" ReKey2 = @ReKey2  ");
            strSql.Append(" where MemberAmountDetailId=@MemberAmountDetailId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@MemberAmountDetailId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@MemberAmountChangeTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@OldAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@NewAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ChangeAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ReKey2", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.MemberAmountDetailId;
            parameters[1].Value = model.Memo;
            parameters[2].Value = model.MemberAmountChangeTypeId;
            parameters[3].Value = model.OldAmount;
            parameters[4].Value = model.NewAmount;
            parameters[5].Value = model.MemberId;
            parameters[6].Value = model.UserId;
            parameters[7].Value = model.ChangeAmount;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.ReKey;
            parameters[10].Value = model.ReKey2; try
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
        public MemberAmountDetailModel GetModel(decimal MemberAmountDetailId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MemberAmountDetailId, Memo, MemberAmountChangeTypeId, OldAmount, NewAmount, MemberId, UserId, ChangeAmount, CreateTime, ReKey, ReKey2  ");
            strSql.Append("  from CORE.dbo.MemberAmountDetail ");
            strSql.Append(" where MemberAmountDetailId=@MemberAmountDetailId");
            SqlParameter[] parameters = {
                    new SqlParameter("@MemberAmountDetailId", SqlDbType.Decimal)
            };
            parameters[0].Value = MemberAmountDetailId;


            MemberAmountDetailModel model = new MemberAmountDetailModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MemberAmountDetailId"].ToString() != "")
                {
                    model.MemberAmountDetailId = decimal.Parse(ds.Tables[0].Rows[0]["MemberAmountDetailId"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                if (ds.Tables[0].Rows[0]["MemberAmountChangeTypeId"].ToString() != "")
                {
                    model.MemberAmountChangeTypeId = int.Parse(ds.Tables[0].Rows[0]["MemberAmountChangeTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OldAmount"].ToString() != "")
                {
                    model.OldAmount = decimal.Parse(ds.Tables[0].Rows[0]["OldAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NewAmount"].ToString() != "")
                {
                    model.NewAmount = decimal.Parse(ds.Tables[0].Rows[0]["NewAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberId"].ToString() != "")
                {
                    model.MemberId = decimal.Parse(ds.Tables[0].Rows[0]["MemberId"].ToString());
                }
                model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                if (ds.Tables[0].Rows[0]["ChangeAmount"].ToString() != "")
                {
                    model.ChangeAmount = decimal.Parse(ds.Tables[0].Rows[0]["ChangeAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.ReKey = ds.Tables[0].Rows[0]["ReKey"].ToString();
                model.ReKey2 = ds.Tables[0].Rows[0]["ReKey2"].ToString();

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
            strSql.Append("delete from CORE.dbo.MemberAmountDetail ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.MemberAmountDetailView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.MemberAmountDetail  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.MemberAmountDetail  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.MemberAmountDetail  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

