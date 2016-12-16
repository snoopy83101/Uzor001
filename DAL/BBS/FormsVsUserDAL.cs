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
    //FormsVsUser
    public partial class FormsVsUserDAL
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
            strSql.Append(" FROM FormsVsUser ");
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
        public bool Add(FormsVsUserModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FormsVsUser(");
            strSql.Append("FormId,UserId,vsType,PowerLv");
            strSql.Append(") values (");
            strSql.Append("@FormId,@UserId,@vsType,@PowerLv");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@FormId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@vsType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@PowerLv", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.FormId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.vsType;
            parameters[3].Value = model.PowerLv;

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
        public bool Update(FormsVsUserModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FormsVsUser set ");

            strSql.Append(" FormId = @FormId , ");
            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" vsType = @vsType , ");
            strSql.Append(" PowerLv = @PowerLv  ");
            strSql.Append(" where FormId=@FormId and UserId=@UserId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@FormId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@vsType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@PowerLv", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.FormId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.vsType;
            parameters[3].Value = model.PowerLv; try
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
        public FormsVsUserModel GetModel(decimal FormId, string UserId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FormId, UserId, vsType, PowerLv  ");
            strSql.Append("  from FormsVsUser ");
            strSql.Append(" where FormId=@FormId and UserId=@UserId ");
            SqlParameter[] parameters = {
					new SqlParameter("@FormId", SqlDbType.Decimal,9),
					new SqlParameter("@UserId", SqlDbType.VarChar,50)			};
            parameters[0].Value = FormId;
            parameters[1].Value = UserId;


            FormsVsUserModel model = new FormsVsUserModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["FormId"].ToString() != "")
                {
                    model.FormId = decimal.Parse(ds.Tables[0].Rows[0]["FormId"].ToString());
                }
                model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                model.vsType = ds.Tables[0].Rows[0]["vsType"].ToString();
                if (ds.Tables[0].Rows[0]["PowerLv"].ToString() != "")
                {
                    model.PowerLv = int.Parse(ds.Tables[0].Rows[0]["PowerLv"].ToString());
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
            strSql.Append("delete from FormsVsUser ");
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
            strSql.Append(" FROM FormsVsUser  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM FormsVsUser  WITH(NOLOCK) ");
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
            strSql.Append(" FROM FormsVsUser  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

