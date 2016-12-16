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
    //InformationVsKeyWord
    public partial class InformationVsKeyWordDAL
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
            strSql.Append(" FROM  CORE.dbo.InformationVsKeyWord ");
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
        public bool Add(InformationVsKeyWordModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.InformationVsKeyWord (");
            strSql.Append("KeyWord,InformationId,vsType");
            strSql.Append(") values (");
            strSql.Append("@KeyWord,@InformationId,@vsType");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@KeyWord", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@InformationId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@vsType", SqlDbType.VarChar,10)             
              
            };

            parameters[0].Value = model.KeyWord;
            parameters[1].Value = model.InformationId;
            parameters[2].Value = model.vsType;

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
        public bool Update(InformationVsKeyWordModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.InformationVsKeyWord set ");

            strSql.Append(" KeyWord = @KeyWord , ");
            strSql.Append(" InformationId = @InformationId , ");
            strSql.Append(" vsType = @vsType  ");
            strSql.Append(" where KeyWord=@KeyWord and InformationId=@InformationId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@KeyWord", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@InformationId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@vsType", SqlDbType.VarChar,10)             
              
            };

            parameters[0].Value = model.KeyWord;
            parameters[1].Value = model.InformationId;
            parameters[2].Value = model.vsType; try
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
        public InformationVsKeyWordModel GetModel(string KeyWord, decimal InformationId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select KeyWord, InformationId, vsType  ");
            strSql.Append("  from CORE.dbo.InformationVsKeyWord ");
            strSql.Append(" where KeyWord=@KeyWord and InformationId=@InformationId ");
            SqlParameter[] parameters = {
					new SqlParameter("@KeyWord", SqlDbType.VarChar,30),
					new SqlParameter("@InformationId", SqlDbType.Decimal,9)			};
            parameters[0].Value = KeyWord;
            parameters[1].Value = InformationId;


            InformationVsKeyWordModel model = new InformationVsKeyWordModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.KeyWord = ds.Tables[0].Rows[0]["KeyWord"].ToString();
                if (ds.Tables[0].Rows[0]["InformationId"].ToString() != "")
                {
                    model.InformationId = decimal.Parse(ds.Tables[0].Rows[0]["InformationId"].ToString());
                }
                model.vsType = ds.Tables[0].Rows[0]["vsType"].ToString();

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
            strSql.Append("delete from CORE.dbo.InformationVsKeyWord ");
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
            strSql.Append(" FROM CORE.dbo.InformationVsKeyWord  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.InformationVsKeyWord  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.InformationVsKeyWord  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

