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
    //ChouJiangType
    public partial class ChouJiangTypeDAL
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
            strSql.Append(" FROM  YYHD.dbo.ChouJiangType ");
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
        public bool Add(ChouJiangTypeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YYHD.dbo.ChouJiangType (");
            strSql.Append("ChouJiangTypeId,ChouJiangTypeName,ChouJiangTypeMemo");
            strSql.Append(") values (");
            strSql.Append("@ChouJiangTypeId,@ChouJiangTypeName,@ChouJiangTypeMemo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ChouJiangTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ChouJiangTypeName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChouJiangTypeMemo", SqlDbType.VarChar,500)             
              
            };

            parameters[0].Value = model.ChouJiangTypeId;
            parameters[1].Value = model.ChouJiangTypeName;
            parameters[2].Value = model.ChouJiangTypeMemo;

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
        public bool Update(ChouJiangTypeModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YYHD.dbo.ChouJiangType set ");

            strSql.Append(" ChouJiangTypeId = @ChouJiangTypeId , ");
            strSql.Append(" ChouJiangTypeName = @ChouJiangTypeName , ");
            strSql.Append(" ChouJiangTypeMemo = @ChouJiangTypeMemo  ");
            strSql.Append(" where ChouJiangTypeId=@ChouJiangTypeId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ChouJiangTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ChouJiangTypeName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChouJiangTypeMemo", SqlDbType.VarChar,500)             
              
            };

            parameters[0].Value = model.ChouJiangTypeId;
            parameters[1].Value = model.ChouJiangTypeName;
            parameters[2].Value = model.ChouJiangTypeMemo; try
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
        public ChouJiangTypeModel GetModel(int ChouJiangTypeId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ChouJiangTypeId, ChouJiangTypeName, ChouJiangTypeMemo  ");
            strSql.Append("  from YYHD.dbo.ChouJiangType ");
            strSql.Append(" where ChouJiangTypeId=@ChouJiangTypeId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ChouJiangTypeId", SqlDbType.Int,4)			};
            parameters[0].Value = ChouJiangTypeId;


            ChouJiangTypeModel model = new ChouJiangTypeModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ChouJiangTypeId"].ToString() != "")
                {
                    model.ChouJiangTypeId = int.Parse(ds.Tables[0].Rows[0]["ChouJiangTypeId"].ToString());
                }
                model.ChouJiangTypeName = ds.Tables[0].Rows[0]["ChouJiangTypeName"].ToString();
                model.ChouJiangTypeMemo = ds.Tables[0].Rows[0]["ChouJiangTypeMemo"].ToString();

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
            strSql.Append("delete from YYHD.dbo.ChouJiangType ");
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
            strSql.Append(" FROM YYHD.dbo.ChouJiangType  WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("YYHD.dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM YYHD.dbo.ChouJiangType  WITH(NOLOCK) ");
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
            strSql.Append(" FROM YYHD.dbo.ChouJiangType  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

