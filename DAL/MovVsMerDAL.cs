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
    //MovVsMer
    public partial class MovVsMerDAL
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
            strSql.Append(" FROM MovVsMer ");
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
        public bool Add(MovVsMerModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MovVsMer(");
            strSql.Append("MovId,MerchantId,vsType");
            strSql.Append(") values (");
            strSql.Append("@MovId,@MerchantId,@vsType");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@MovId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@vsType", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.MovId;
            parameters[1].Value = model.MerchantId;
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
        public bool Update(MovVsMerModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MovVsMer set ");

            strSql.Append(" MovId = @MovId , ");
            strSql.Append(" MerchantId = @MerchantId , ");
            strSql.Append(" vsType = @vsType  ");
            strSql.Append(" where MovId=@MovId and MerchantId=@MerchantId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@MovId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@vsType", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.MovId;
            parameters[1].Value = model.MerchantId;
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
        public MovVsMerModel GetModel(string MovId, decimal MerchantId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MovId, MerchantId, vsType  ");
            strSql.Append("  from MovVsMer ");
            strSql.Append(" where MovId=@MovId and MerchantId=@MerchantId ");
            SqlParameter[] parameters = {
					new SqlParameter("@MovId", SqlDbType.VarChar,50),
					new SqlParameter("@MerchantId", SqlDbType.Decimal,9)			};
            parameters[0].Value = MovId;
            parameters[1].Value = MerchantId;


            MovVsMerModel model = new MovVsMerModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.MovId = ds.Tables[0].Rows[0]["MovId"].ToString();
                if (ds.Tables[0].Rows[0]["MerchantId"].ToString() != "")
                {
                    model.MerchantId = decimal.Parse(ds.Tables[0].Rows[0]["MerchantId"].ToString());
                }
                model.vsType = ds.Tables[0].Rows[0]["vsType"].ToString();

                return model;
            }
            else
            {
                return null;
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
            strSql.Append("delete from MovVsMer ");
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
            strSql.Append(" FROM MovVsMer  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM MovVsMer  WITH(NOLOCK) ");
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
            strSql.Append(" FROM MovVsMer  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

