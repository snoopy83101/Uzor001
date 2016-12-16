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
    //商家和用户关联表
    public partial class MerchantVsUserDAL
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
            strSql.Append(" FROM MerchantVsUser ");
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
        public bool Add(MerchantVsUserModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MerchantVsUser(");
            strSql.Append("MerchantId,UserId,Power,Memo");
            strSql.Append(") values (");
            strSql.Append("@MerchantId,@UserId,@Power,@Memo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Power", SqlDbType.Int,4) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,200)             
              
            };

            parameters[0].Value = model.MerchantId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.Power;
            parameters[3].Value = model.Memo;

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
        public bool Update(MerchantVsUserModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MerchantVsUser set ");

            strSql.Append(" MerchantId = @MerchantId , ");
            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" Power = @Power , ");
            strSql.Append(" Memo = @Memo  ");
            strSql.Append(" where MerchantId=@MerchantId and UserId=@UserId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Power", SqlDbType.Int,4) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,200)             
              
            };

            parameters[0].Value = model.MerchantId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.Power;
            parameters[3].Value = model.Memo; try
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
        public MerchantVsUserModel GetModel(decimal MerchantId, string UserId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MerchantId, UserId, Power, Memo  ");
            strSql.Append("  from MerchantVsUser ");
            strSql.Append(" where MerchantId=@MerchantId and UserId=@UserId ");
            SqlParameter[] parameters = {
					new SqlParameter("@MerchantId", SqlDbType.Decimal,9),
					new SqlParameter("@UserId", SqlDbType.VarChar,50)			};
            parameters[0].Value = MerchantId;
            parameters[1].Value = UserId;


            MerchantVsUserModel model = new MerchantVsUserModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MerchantId"].ToString() != "")
                {
                    model.MerchantId = decimal.Parse(ds.Tables[0].Rows[0]["MerchantId"].ToString());
                }
                model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                if (ds.Tables[0].Rows[0]["Power"].ToString() != "")
                {
                    model.Power = int.Parse(ds.Tables[0].Rows[0]["Power"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();

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
            strSql.Append("delete from MerchantVsUser ");
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
            strSql.Append(" FROM MerchantVsUser ");
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
            strSql.Append(" FROM MerchantVsUser ");
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
            strSql.Append(" FROM MerchantVsUser ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

