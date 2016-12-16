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
    //ChouJiangClass
    public partial class ChouJiangClassDAL
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
            strSql.Append(" FROM  YYHD.dbo.ChouJiangClass ");
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
        public bool Add(ChouJiangClassModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YYHD.dbo.ChouJiangClass (");
            strSql.Append("ChouJiangClassName,ChouJiangMemo");
            strSql.Append(") values (");
            strSql.Append("@ChouJiangClassName,@ChouJiangMemo");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@ChouJiangClassName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChouJiangMemo", SqlDbType.VarChar,500)             
              
            };

            parameters[0].Value = model.ChouJiangClassName;
            parameters[1].Value = model.ChouJiangMemo;

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
        public bool Update(ChouJiangClassModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YYHD.dbo.ChouJiangClass set ");

            strSql.Append(" ChouJiangClassName = @ChouJiangClassName , ");
            strSql.Append(" ChouJiangMemo = @ChouJiangMemo  ");
            strSql.Append(" where ChouJiangClassId=@ChouJiangClassId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ChouJiangClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ChouJiangClassName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChouJiangMemo", SqlDbType.VarChar,500)             
              
            };

            parameters[0].Value = model.ChouJiangClassId;
            parameters[1].Value = model.ChouJiangClassName;
            parameters[2].Value = model.ChouJiangMemo; try
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
        public ChouJiangClassModel GetModel(int ChouJiangClassId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ChouJiangClassId, ChouJiangClassName, ChouJiangMemo  ");
            strSql.Append("  from YYHD.dbo.ChouJiangClass ");
            strSql.Append(" where ChouJiangClassId=@ChouJiangClassId");
            SqlParameter[] parameters = {
					new SqlParameter("@ChouJiangClassId", SqlDbType.Int,4)
			};
            parameters[0].Value = ChouJiangClassId;


            ChouJiangClassModel model = new ChouJiangClassModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ChouJiangClassId"].ToString() != "")
                {
                    model.ChouJiangClassId = int.Parse(ds.Tables[0].Rows[0]["ChouJiangClassId"].ToString());
                }
                model.ChouJiangClassName = ds.Tables[0].Rows[0]["ChouJiangClassName"].ToString();
                model.ChouJiangMemo = ds.Tables[0].Rows[0]["ChouJiangMemo"].ToString();

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
            strSql.Append("delete from YYHD.dbo.ChouJiangClass ");
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
            strSql.Append(" FROM YYHD.dbo.ChouJiangClass  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM YYHD.dbo.ChouJiangClass  WITH(NOLOCK) ");
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
            strSql.Append(" FROM YYHD.dbo.ChouJiangClass  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

