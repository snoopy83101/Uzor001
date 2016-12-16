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
    //MerRoleVsUser
    public partial class MerRoleVsUserDAL
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
            strSql.Append(" FROM  CORE.dbo.MerRoleVsUser ");
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
        public bool Add(MerRoleVsUserModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.MerRoleVsUser (");
            strSql.Append("MerRoleId,UserId,WxOpenId,WorkStatusId,WordStatusPcId,BranchId");
            strSql.Append(") values (");
            strSql.Append("@MerRoleId,@UserId,@WxOpenId,@WorkStatusId,@WordStatusPcId,@BranchId");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@MerRoleId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@WxOpenId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@WorkStatusId", SqlDbType.Int,4) ,
                        new SqlParameter("@WordStatusPcId", SqlDbType.Int,4) ,
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.MerRoleId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.WxOpenId;
            parameters[3].Value = model.WorkStatusId;
            parameters[4].Value = model.WordStatusPcId;
            parameters[5].Value = model.BranchId;

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
        public bool Update(MerRoleVsUserModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.MerRoleVsUser set ");

            strSql.Append(" MerRoleId = @MerRoleId , ");
            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" WxOpenId = @WxOpenId , ");
            strSql.Append(" WorkStatusId = @WorkStatusId , ");
            strSql.Append(" WordStatusPcId = @WordStatusPcId , ");
            strSql.Append(" BranchId = @BranchId  ");
            strSql.Append(" where MerRoleId=@MerRoleId and UserId=@UserId and BranchId=@BranchId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@MerRoleId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@WxOpenId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@WorkStatusId", SqlDbType.Int,4) ,
                        new SqlParameter("@WordStatusPcId", SqlDbType.Int,4) ,
                        new SqlParameter("@BranchId", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.MerRoleId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.WxOpenId;
            parameters[3].Value = model.WorkStatusId;
            parameters[4].Value = model.WordStatusPcId;
            parameters[5].Value = model.BranchId; try
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
        public MerRoleVsUserModel GetModel(decimal MerRoleId, string UserId, string BranchId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MerRoleId, UserId, WxOpenId, WorkStatusId, WordStatusPcId, BranchId  ");
            strSql.Append("  from CORE.dbo.MerRoleVsUser ");
            strSql.Append(" where MerRoleId=@MerRoleId and UserId=@UserId and BranchId=@BranchId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@MerRoleId", SqlDbType.Decimal,9),
                    new SqlParameter("@UserId", SqlDbType.VarChar,50),
                    new SqlParameter("@BranchId", SqlDbType.VarChar,50)         };
            parameters[0].Value = MerRoleId;
            parameters[1].Value = UserId;
            parameters[2].Value = BranchId;


            MerRoleVsUserModel model = new MerRoleVsUserModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MerRoleId"].ToString() != "")
                {
                    model.MerRoleId = decimal.Parse(ds.Tables[0].Rows[0]["MerRoleId"].ToString());
                }
                model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                model.WxOpenId = ds.Tables[0].Rows[0]["WxOpenId"].ToString();
                if (ds.Tables[0].Rows[0]["WorkStatusId"].ToString() != "")
                {
                    model.WorkStatusId = int.Parse(ds.Tables[0].Rows[0]["WorkStatusId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WordStatusPcId"].ToString() != "")
                {
                    model.WordStatusPcId = int.Parse(ds.Tables[0].Rows[0]["WordStatusPcId"].ToString());
                }
                model.BranchId = ds.Tables[0].Rows[0]["BranchId"].ToString();

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
            strSql.Append("delete from CORE.dbo.MerRoleVsUser ");
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
        public DataSet GetPageListGroupByUserId(string strWhere, int currentpage, int pagesize)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 UserId ");
            strSql.Append(" FROM CORE.dbo.UserMerRoleView  WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" Group by UserId ");
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("CORE.dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }


        /// <summary>
        /// 获得fenye数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize, string cols)
        {
            if (cols == "")
            {
                cols = " * ";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 " + cols + " ");
            strSql.Append(" FROM CORE.dbo.UserMerRoleView  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.UserMerRoleView  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.MerRoleVsUser  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

