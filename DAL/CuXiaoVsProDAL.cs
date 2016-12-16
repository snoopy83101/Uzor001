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
    //CuXiaoVsPro
    public partial class CuXiaoVsProDAL
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
            strSql.Append(" FROM  CORE.dbo.CuXiaoVsPro ");
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
        public bool Add(CuXiaoVsProModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.CuXiaoVsPro (");
            strSql.Append("CuXiaoId,ProId,BranchId,VsProName,OrderNo,VsKey");
            strSql.Append(") values (");
            strSql.Append("@CuXiaoId,@ProId,@BranchId,@VsProName,@OrderNo,@VsKey");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@CuXiaoId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@VsProName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@VsKey", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.CuXiaoId;
            parameters[1].Value = model.ProId;
            parameters[2].Value = model.BranchId;
            parameters[3].Value = model.VsProName;
            parameters[4].Value = model.OrderNo;
            parameters[5].Value = model.VsKey;

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
        public bool Update(CuXiaoVsProModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.CuXiaoVsPro set ");

            strSql.Append(" CuXiaoId = @CuXiaoId , ");
            strSql.Append(" ProId = @ProId , ");
            strSql.Append(" BranchId = @BranchId , ");
            strSql.Append(" VsProName = @VsProName , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" VsKey = @VsKey  ");
            strSql.Append(" where CuXiaoId=@CuXiaoId and ProId=@ProId and BranchId=@BranchId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@CuXiaoId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@VsProName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@VsKey", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.CuXiaoId;
            parameters[1].Value = model.ProId;
            parameters[2].Value = model.BranchId;
            parameters[3].Value = model.VsProName;
            parameters[4].Value = model.OrderNo;
            parameters[5].Value = model.VsKey; try
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
        public CuXiaoVsProModel GetModel(decimal CuXiaoId, string ProId, string BranchId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CuXiaoId, ProId, BranchId, VsProName, OrderNo, VsKey  ");
            strSql.Append("  from CORE.dbo.CuXiaoVsPro ");
            strSql.Append(" where CuXiaoId=@CuXiaoId and ProId=@ProId and BranchId=@BranchId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CuXiaoId", SqlDbType.Decimal,9),
                    new SqlParameter("@ProId", SqlDbType.VarChar,50),
                    new SqlParameter("@BranchId", SqlDbType.VarChar,50)         };
            parameters[0].Value = CuXiaoId;
            parameters[1].Value = ProId;
            parameters[2].Value = BranchId;


            CuXiaoVsProModel model = new CuXiaoVsProModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CuXiaoId"].ToString() != "")
                {
                    model.CuXiaoId = decimal.Parse(ds.Tables[0].Rows[0]["CuXiaoId"].ToString());
                }
                model.ProId = ds.Tables[0].Rows[0]["ProId"].ToString();
                model.BranchId = ds.Tables[0].Rows[0]["BranchId"].ToString();
                model.VsProName = ds.Tables[0].Rows[0]["VsProName"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
                }
                model.VsKey = ds.Tables[0].Rows[0]["VsKey"].ToString();

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
            strSql.Append("delete from CORE.dbo.CuXiaoVsPro ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.CuXiaoProView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.CuXiaoVsPro  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.CuXiaoVsPro  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.CuXiaoVsPro  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

