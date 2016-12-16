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
    //UserVsRole
    public partial class UserVsRoleDAL
    {

        #region  //数据操作

        /// <summary>
        /// 数据库帮助对象
        /// </summary>
        private MSSQLHelper helper = new MSSQLHelper();
        #endregion


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(UserVsRoleModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserVsRole(");
            strSql.Append("RoleName,RoleUserId,RoleMemo,RoleLv");
            strSql.Append(") values (");
            strSql.Append("@RoleName,@RoleUserId,@RoleMemo,@RoleLv");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@RoleName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@RoleUserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@RoleMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@RoleLv", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.RoleName;
            parameters[1].Value = model.RoleUserId;
            parameters[2].Value = model.RoleMemo;
            parameters[3].Value = model.RoleLv;

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
        public bool Update(UserVsRoleModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserVsRole set ");

            strSql.Append(" RoleName = @RoleName , ");
            strSql.Append(" RoleUserId = @RoleUserId , ");
            strSql.Append(" RoleMemo = @RoleMemo , ");
            strSql.Append(" RoleLv = @RoleLv  ");
            strSql.Append(" where RoleName=@RoleName and RoleUserId=@RoleUserId and RoleMemo=@RoleMemo and RoleLv=@RoleLv  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@RoleName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@RoleUserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@RoleMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@RoleLv", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.RoleName;
            parameters[1].Value = model.RoleUserId;
            parameters[2].Value = model.RoleMemo;
            parameters[3].Value = model.RoleLv; try
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
        /// 删除duo条数据
        /// </summary>
        public bool DeleteList(string strWhere)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserVsRole ");
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
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 * ");
            strSql.Append(" FROM UserVsRole  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("PDIMS.dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM UserVsRole WITH(NOLOCK)  ");
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
            strSql.Append(" FROM UserVsRole  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

