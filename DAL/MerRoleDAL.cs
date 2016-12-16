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
    //MerRole
    public partial class MerRoleDAL
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
            strSql.Append(" FROM  CORE.dbo.MerRole ");
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
        public bool Add(MerRoleModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.MerRole (");
            strSql.Append("MerRoleName,MerRoleMemo,MerId,Power");
            strSql.Append(") values (");
            strSql.Append("@MerRoleName,@MerRoleMemo,@MerId,@Power");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@MerRoleName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MerRoleMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Power", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.MerRoleName;
            parameters[1].Value = model.MerRoleMemo;
            parameters[2].Value = model.MerId;
            parameters[3].Value = model.Power;

            bool result = false;
            try
            {
                model.MerRoleId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "MerRoleId", parameters));
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
        public bool Update(MerRoleModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.MerRole set ");

            strSql.Append(" MerRoleName = @MerRoleName , ");
            strSql.Append(" MerRoleMemo = @MerRoleMemo , ");
            strSql.Append(" MerId = @MerId , ");
            strSql.Append(" Power = @Power  ");
            strSql.Append(" where MerRoleId=@MerRoleId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@MerRoleId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MerRoleName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MerRoleMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Power", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.MerRoleId;
            parameters[1].Value = model.MerRoleName;
            parameters[2].Value = model.MerRoleMemo;
            parameters[3].Value = model.MerId;
            parameters[4].Value = model.Power; try
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
        public MerRoleModel GetModel(decimal MerRoleId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MerRoleId, MerRoleName, MerRoleMemo, MerId, Power  ");
            strSql.Append("  from CORE.dbo.MerRole ");
            strSql.Append(" where MerRoleId=@MerRoleId");
            SqlParameter[] parameters = {
					new SqlParameter("@MerRoleId", SqlDbType.Decimal)
			};
            parameters[0].Value = MerRoleId;


            MerRoleModel model = new MerRoleModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MerRoleId"].ToString() != "")
                {
                    model.MerRoleId = decimal.Parse(ds.Tables[0].Rows[0]["MerRoleId"].ToString());
                }
                model.MerRoleName = ds.Tables[0].Rows[0]["MerRoleName"].ToString();
                model.MerRoleMemo = ds.Tables[0].Rows[0]["MerRoleMemo"].ToString();
                if (ds.Tables[0].Rows[0]["MerId"].ToString() != "")
                {
                    model.MerId = decimal.Parse(ds.Tables[0].Rows[0]["MerId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Power"].ToString() != "")
                {
                    model.Power = int.Parse(ds.Tables[0].Rows[0]["Power"].ToString());
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
            strSql.Append("delete from CORE.dbo.MerRole ");
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
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize, string cols)
        {
            if (cols == "")
            {
                cols = " * ";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 " + cols + " ");
            strSql.Append(" FROM CORE.dbo.MerRole  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.MerRole  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.MerRole  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

