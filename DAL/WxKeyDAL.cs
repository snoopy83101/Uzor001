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
    //WxKey
    public partial class WxKeyDAL
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
            strSql.Append(" FROM  CORE.dbo.WxKey ");
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
        public bool Add(WxKeyModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.WxKey (");
            strSql.Append("WxSendId,KeyTitle,KeyTypeId,KeyTypeDetailId");
            strSql.Append(") values (");
            strSql.Append("@WxSendId,@KeyTitle,@KeyTypeId,@KeyTypeDetailId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@WxSendId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@KeyTitle", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@KeyTypeId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@KeyTypeDetailId", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.WxSendId;
            parameters[1].Value = model.KeyTitle;
            parameters[2].Value = model.KeyTypeId;
            parameters[3].Value = model.KeyTypeDetailId;

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
        public bool Update(WxKeyModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.WxKey set ");

            strSql.Append(" WxSendId = @WxSendId , ");
            strSql.Append(" KeyTitle = @KeyTitle , ");
            strSql.Append(" KeyTypeId = @KeyTypeId , ");
            strSql.Append(" KeyTypeDetailId = @KeyTypeDetailId  ");
            strSql.Append(" where KeyId=@KeyId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@KeyId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@WxSendId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@KeyTitle", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@KeyTypeId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@KeyTypeDetailId", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.KeyId;
            parameters[1].Value = model.WxSendId;
            parameters[2].Value = model.KeyTitle;
            parameters[3].Value = model.KeyTypeId;
            parameters[4].Value = model.KeyTypeDetailId; try
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
        public WxKeyModel GetModel(decimal KeyId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select KeyId, WxSendId, KeyTitle, KeyTypeId, KeyTypeDetailId  ");
            strSql.Append("  from CORE.dbo.WxKey ");
            strSql.Append(" where KeyId=@KeyId");
            SqlParameter[] parameters = {
					new SqlParameter("@KeyId", SqlDbType.Decimal)
			};
            parameters[0].Value = KeyId;


            WxKeyModel model = new WxKeyModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["KeyId"].ToString() != "")
                {
                    model.KeyId = decimal.Parse(ds.Tables[0].Rows[0]["KeyId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WxSendId"].ToString() != "")
                {
                    model.WxSendId = decimal.Parse(ds.Tables[0].Rows[0]["WxSendId"].ToString());
                }
                model.KeyTitle = ds.Tables[0].Rows[0]["KeyTitle"].ToString();
                model.KeyTypeId = ds.Tables[0].Rows[0]["KeyTypeId"].ToString();
                model.KeyTypeDetailId = ds.Tables[0].Rows[0]["KeyTypeDetailId"].ToString();

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
            strSql.Append("delete from CORE.dbo.WxKey ");
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
            strSql.Append(" FROM CORE.dbo.WxKey  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.WxKey  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.WxKey  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

