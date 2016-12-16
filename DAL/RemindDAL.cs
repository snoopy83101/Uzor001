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
    //Remind
    public partial class RemindDAL
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
            strSql.Append(" FROM Remind ");
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
        public bool Add(RemindModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Remind(");
            strSql.Append("RemindId,ReKey,RemindTitle,CreateTime,RemindTypeId,Url,UserLook,MerLook,ReUserId,ReMerchantId");
            strSql.Append(") values (");
            strSql.Append("@RemindId,@ReKey,@RemindTitle,@CreateTime,@RemindTypeId,@Url,@UserLook,@MerLook,@ReUserId,@ReMerchantId");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@RemindId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@RemindTitle", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@RemindTypeId", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Url", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UserLook", SqlDbType.Bit,1) ,            
                        new SqlParameter("@MerLook", SqlDbType.Bit,1) ,            
                        new SqlParameter("@ReUserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ReMerchantId", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.RemindId;
            parameters[1].Value = model.ReKey;
            parameters[2].Value = model.RemindTitle;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.RemindTypeId;
            parameters[5].Value = model.Url;
            parameters[6].Value = model.UserLook;
            parameters[7].Value = model.MerLook;
            parameters[8].Value = model.ReUserId;
            parameters[9].Value = model.ReMerchantId;

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
        public bool Update(RemindModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Remind set ");

            strSql.Append(" RemindId = @RemindId , ");
            strSql.Append(" ReKey = @ReKey , ");
            strSql.Append(" RemindTitle = @RemindTitle , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" RemindTypeId = @RemindTypeId , ");
            strSql.Append(" Url = @Url , ");
            strSql.Append(" UserLook = @UserLook , ");
            strSql.Append(" MerLook = @MerLook , ");
            strSql.Append(" ReUserId = @ReUserId , ");
            strSql.Append(" ReMerchantId = @ReMerchantId  ");
            strSql.Append(" where RemindId=@RemindId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@RemindId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@RemindTitle", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@RemindTypeId", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Url", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UserLook", SqlDbType.Bit,1) ,            
                        new SqlParameter("@MerLook", SqlDbType.Bit,1) ,            
                        new SqlParameter("@ReUserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ReMerchantId", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.RemindId;
            parameters[1].Value = model.ReKey;
            parameters[2].Value = model.RemindTitle;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.RemindTypeId;
            parameters[5].Value = model.Url;
            parameters[6].Value = model.UserLook;
            parameters[7].Value = model.MerLook;
            parameters[8].Value = model.ReUserId;
            parameters[9].Value = model.ReMerchantId; try
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
        public RemindModel GetModel(string RemindId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select RemindId, ReKey, RemindTitle, CreateTime, RemindTypeId, Url, UserLook, MerLook, ReUserId, ReMerchantId  ");
            strSql.Append("  from Remind ");
            strSql.Append(" where RemindId=@RemindId ");
            SqlParameter[] parameters = {
					new SqlParameter("@RemindId", SqlDbType.VarChar,50)			};
            parameters[0].Value = RemindId;


            RemindModel model = new RemindModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.RemindId = ds.Tables[0].Rows[0]["RemindId"].ToString();
                model.ReKey = ds.Tables[0].Rows[0]["ReKey"].ToString();
                model.RemindTitle = ds.Tables[0].Rows[0]["RemindTitle"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.RemindTypeId = ds.Tables[0].Rows[0]["RemindTypeId"].ToString();
                model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                if (ds.Tables[0].Rows[0]["UserLook"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["UserLook"].ToString() == "1") || (ds.Tables[0].Rows[0]["UserLook"].ToString().ToLower() == "true"))
                    {
                        model.UserLook = true;
                    }
                    else
                    {
                        model.UserLook = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["MerLook"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["MerLook"].ToString() == "1") || (ds.Tables[0].Rows[0]["MerLook"].ToString().ToLower() == "true"))
                    {
                        model.MerLook = true;
                    }
                    else
                    {
                        model.MerLook = false;
                    }
                }
                model.ReUserId = ds.Tables[0].Rows[0]["ReUserId"].ToString();
                if (ds.Tables[0].Rows[0]["ReMerchantId"].ToString() != "")
                {
                    model.ReMerchantId = decimal.Parse(ds.Tables[0].Rows[0]["ReMerchantId"].ToString());
                }

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
            strSql.Append("delete from Remind ");
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
            strSql.Append("select top 1000 * ");
            strSql.Append(" FROM Remind  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM Remind ");
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
            strSql.Append(" FROM Remind ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

