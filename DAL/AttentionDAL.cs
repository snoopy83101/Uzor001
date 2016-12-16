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
    //关注表,关注人或者关注商家
    public partial class AttentionDAL
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
            strSql.Append(" FROM Attention ");
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
        public bool Add(AttentionModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Attention(");
            strSql.Append("UserId,AttentionUserId,AttentionMerId,AttentionType,DynamicLv,FlagInvalid");
            strSql.Append(") values (");
            strSql.Append("@UserId,@AttentionUserId,@AttentionMerId,@AttentionType,@DynamicLv,@FlagInvalid");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@UserId", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@AttentionUserId", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@AttentionMerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AttentionType", SqlDbType.Int,4) ,            
                        new SqlParameter("@DynamicLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@FlagInvalid", SqlDbType.Bit,1)             
              
            };

            parameters[0].Value = model.UserId;
            parameters[1].Value = model.AttentionUserId;
            parameters[2].Value = model.AttentionMerId;
            parameters[3].Value = model.AttentionType;
            parameters[4].Value = model.DynamicLv;
            parameters[5].Value = model.FlagInvalid;

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
        public bool Update(AttentionModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Attention set ");

            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" AttentionUserId = @AttentionUserId , ");
            strSql.Append(" AttentionMerId = @AttentionMerId , ");
            strSql.Append(" AttentionType = @AttentionType , ");
            strSql.Append(" DynamicLv = @DynamicLv , ");
            strSql.Append(" FlagInvalid = @FlagInvalid  ");
            strSql.Append(" where AutoId=@AutoId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@AutoId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@UserId", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@AttentionUserId", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@AttentionMerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AttentionType", SqlDbType.Int,4) ,            
                        new SqlParameter("@DynamicLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@FlagInvalid", SqlDbType.Bit,1)             
              
            };

            parameters[0].Value = model.AutoId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.AttentionUserId;
            parameters[3].Value = model.AttentionMerId;
            parameters[4].Value = model.AttentionType;
            parameters[5].Value = model.DynamicLv;
            parameters[6].Value = model.FlagInvalid; try
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
        public AttentionModel GetModel(decimal AutoId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AutoId, UserId, AttentionUserId, AttentionMerId, AttentionType, DynamicLv, FlagInvalid  ");
            strSql.Append("  from Attention ");
            strSql.Append(" where AutoId=@AutoId");
            SqlParameter[] parameters = {
					new SqlParameter("@AutoId", SqlDbType.Decimal)
			};
            parameters[0].Value = AutoId;


            AttentionModel model = new AttentionModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["AutoId"].ToString() != "")
                {
                    model.AutoId = decimal.Parse(ds.Tables[0].Rows[0]["AutoId"].ToString());
                }
                model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                model.AttentionUserId = ds.Tables[0].Rows[0]["AttentionUserId"].ToString();
                if (ds.Tables[0].Rows[0]["AttentionMerId"].ToString() != "")
                {
                    model.AttentionMerId = decimal.Parse(ds.Tables[0].Rows[0]["AttentionMerId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AttentionType"].ToString() != "")
                {
                    model.AttentionType = int.Parse(ds.Tables[0].Rows[0]["AttentionType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DynamicLv"].ToString() != "")
                {
                    model.DynamicLv = int.Parse(ds.Tables[0].Rows[0]["DynamicLv"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FlagInvalid"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["FlagInvalid"].ToString() == "1") || (ds.Tables[0].Rows[0]["FlagInvalid"].ToString().ToLower() == "true"))
                    {
                        model.FlagInvalid = true;
                    }
                    else
                    {
                        model.FlagInvalid = false;
                    }
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
            strSql.Append("delete from Attention ");
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
            strSql.Append(" FROM Attention ");
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
            strSql.Append(" FROM Attention ");
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
            strSql.Append(" FROM Attention ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

