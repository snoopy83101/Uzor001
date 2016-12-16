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
    //CuXiao
    public partial class CuXiaoDAL
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
            strSql.Append(" FROM  CORE.dbo.CuXiao ");
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
        public bool Add(CuXiaoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.CuXiao (");
            strSql.Append("MerId,BranchId,CuXiaoLabel,Invalid,ZoneId,CuXiaoName,CuXiaoContent,CreateTime,CreateUser,PyCode,BgTime,EndTime,Status");
            strSql.Append(") values (");
            strSql.Append("@MerId,@BranchId,@CuXiaoLabel,@Invalid,@ZoneId,@CuXiaoName,@CuXiaoContent,@CreateTime,@CreateUser,@PyCode,@BgTime,@EndTime,@Status");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CuXiaoLabel", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@ZoneId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CuXiaoName", SqlDbType.VarChar,250) ,
                        new SqlParameter("@CuXiaoContent", SqlDbType.VarChar,500) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PyCode", SqlDbType.VarChar,250) ,
                        new SqlParameter("@BgTime", SqlDbType.DateTime) ,
                        new SqlParameter("@EndTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Status", SqlDbType.Int,4)

            };

            parameters[0].Value = model.MerId;
            parameters[1].Value = model.BranchId;
            parameters[2].Value = model.CuXiaoLabel;
            parameters[3].Value = model.Invalid;
            parameters[4].Value = model.ZoneId;
            parameters[5].Value = model.CuXiaoName;
            parameters[6].Value = model.CuXiaoContent;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.CreateUser;
            parameters[9].Value = model.PyCode;
            parameters[10].Value = model.BgTime;
            parameters[11].Value = model.EndTime;
            parameters[12].Value = model.Status;

            bool result = false;
            try
            {


                model.CuXiaoId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "CuXiaoId", parameters));


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
        public bool Update(CuXiaoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.CuXiao set ");

            strSql.Append(" MerId = @MerId , ");
            strSql.Append(" BranchId = @BranchId , ");
            strSql.Append(" CuXiaoLabel = @CuXiaoLabel , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" ZoneId = @ZoneId , ");
            strSql.Append(" CuXiaoName = @CuXiaoName , ");
            strSql.Append(" CuXiaoContent = @CuXiaoContent , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" CreateUser = @CreateUser , ");
            strSql.Append(" PyCode = @PyCode , ");
            strSql.Append(" BgTime = @BgTime , ");
            strSql.Append(" EndTime = @EndTime , ");
            strSql.Append(" Status = @Status  ");
            strSql.Append(" where CuXiaoId=@CuXiaoId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@CuXiaoId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CuXiaoLabel", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@ZoneId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CuXiaoName", SqlDbType.VarChar,250) ,
                        new SqlParameter("@CuXiaoContent", SqlDbType.VarChar,500) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PyCode", SqlDbType.VarChar,250) ,
                        new SqlParameter("@BgTime", SqlDbType.DateTime) ,
                        new SqlParameter("@EndTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Status", SqlDbType.Int,4)

            };

            parameters[0].Value = model.CuXiaoId;
            parameters[1].Value = model.MerId;
            parameters[2].Value = model.BranchId;
            parameters[3].Value = model.CuXiaoLabel;
            parameters[4].Value = model.Invalid;
            parameters[5].Value = model.ZoneId;
            parameters[6].Value = model.CuXiaoName;
            parameters[7].Value = model.CuXiaoContent;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.CreateUser;
            parameters[10].Value = model.PyCode;
            parameters[11].Value = model.BgTime;
            parameters[12].Value = model.EndTime;
            parameters[13].Value = model.Status; try
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
        public CuXiaoModel GetModel(decimal CuXiaoId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CuXiaoId, MerId, BranchId, CuXiaoLabel, Invalid, ZoneId, CuXiaoName, CuXiaoContent, CreateTime, CreateUser, PyCode, BgTime, EndTime, Status  ");
            strSql.Append("  from CORE.dbo.CuXiao ");
            strSql.Append(" where CuXiaoId=@CuXiaoId");
            SqlParameter[] parameters = {
                    new SqlParameter("@CuXiaoId", SqlDbType.Decimal)
            };
            parameters[0].Value = CuXiaoId;


            CuXiaoModel model = new CuXiaoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CuXiaoId"].ToString() != "")
                {
                    model.CuXiaoId = decimal.Parse(ds.Tables[0].Rows[0]["CuXiaoId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MerId"].ToString() != "")
                {
                    model.MerId = decimal.Parse(ds.Tables[0].Rows[0]["MerId"].ToString());
                }
                model.BranchId = ds.Tables[0].Rows[0]["BranchId"].ToString();
                model.CuXiaoLabel = ds.Tables[0].Rows[0]["CuXiaoLabel"].ToString();
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
                model.ZoneId = ds.Tables[0].Rows[0]["ZoneId"].ToString();
                model.CuXiaoName = ds.Tables[0].Rows[0]["CuXiaoName"].ToString();
                model.CuXiaoContent = ds.Tables[0].Rows[0]["CuXiaoContent"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.CreateUser = ds.Tables[0].Rows[0]["CreateUser"].ToString();
                model.PyCode = ds.Tables[0].Rows[0]["PyCode"].ToString();
                if (ds.Tables[0].Rows[0]["BgTime"].ToString() != "")
                {
                    model.BgTime = DateTime.Parse(ds.Tables[0].Rows[0]["BgTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndTime"].ToString() != "")
                {
                    model.EndTime = DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
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
            strSql.Append("delete from CORE.dbo.CuXiao ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.CuXiao  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.CuXiao  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.CuXiao  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.CuXiao  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

