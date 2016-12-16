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
    //PingJiaInfo
    public partial class PingJiaInfoDAL
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
            strSql.Append(" FROM  CORE.dbo.PingJiaInfo ");
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
        public bool Add(PingJiaInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.PingJiaInfo (");
            strSql.Append("PingJiaContent,HuiPingContent,CreateTime,HuiPingTime,HuiPingUser,DingDanDetailId,PingJiaLv,Invalid");
            strSql.Append(") values (");
            strSql.Append("@PingJiaContent,@HuiPingContent,@CreateTime,@HuiPingTime,@HuiPingUser,@DingDanDetailId,@PingJiaLv,@Invalid");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@PingJiaContent", SqlDbType.VarChar,5000) ,            
                        new SqlParameter("@HuiPingContent", SqlDbType.VarChar,5000) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@HuiPingTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@HuiPingUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DingDanDetailId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@PingJiaLv", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1)             
              
            };

            parameters[0].Value = model.PingJiaContent;
            parameters[1].Value = model.HuiPingContent;
            parameters[2].Value = model.CreateTime;
            parameters[3].Value = model.HuiPingTime;
            parameters[4].Value = model.HuiPingUser;
            parameters[5].Value = model.DingDanDetailId;
            parameters[6].Value = model.PingJiaLv;
            parameters[7].Value = model.Invalid;

            bool result = false;
            try
            {
                model.PingJiaId=decimal.Parse( helper.ExecuteNonQueryBackId(strSql.ToString(),"PingJiaId", parameters));
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
        public bool Update(PingJiaInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.PingJiaInfo set ");

            strSql.Append(" PingJiaContent = @PingJiaContent , ");
            strSql.Append(" HuiPingContent = @HuiPingContent , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" HuiPingTime = @HuiPingTime , ");
            strSql.Append(" HuiPingUser = @HuiPingUser , ");
            strSql.Append(" DingDanDetailId = @DingDanDetailId , ");
            strSql.Append(" PingJiaLv = @PingJiaLv , ");
            strSql.Append(" Invalid = @Invalid  ");
            strSql.Append(" where PingJiaId=@PingJiaId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@PingJiaId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@PingJiaContent", SqlDbType.VarChar,5000) ,            
                        new SqlParameter("@HuiPingContent", SqlDbType.VarChar,5000) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@HuiPingTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@HuiPingUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DingDanDetailId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@PingJiaLv", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1)             
              
            };

            parameters[0].Value = model.PingJiaId;
            parameters[1].Value = model.PingJiaContent;
            parameters[2].Value = model.HuiPingContent;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.HuiPingTime;
            parameters[5].Value = model.HuiPingUser;
            parameters[6].Value = model.DingDanDetailId;
            parameters[7].Value = model.PingJiaLv;
            parameters[8].Value = model.Invalid; try
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
        public PingJiaInfoModel GetModel(decimal PingJiaId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PingJiaId, PingJiaContent, HuiPingContent, CreateTime, HuiPingTime, HuiPingUser, DingDanDetailId, PingJiaLv, Invalid  ");
            strSql.Append("  from CORE.dbo.PingJiaInfo ");
            strSql.Append(" where PingJiaId=@PingJiaId");
            SqlParameter[] parameters = {
					new SqlParameter("@PingJiaId", SqlDbType.Decimal)
			};
            parameters[0].Value = PingJiaId;


            PingJiaInfoModel model = new PingJiaInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PingJiaId"].ToString() != "")
                {
                    model.PingJiaId = decimal.Parse(ds.Tables[0].Rows[0]["PingJiaId"].ToString());
                }
                model.PingJiaContent = ds.Tables[0].Rows[0]["PingJiaContent"].ToString();
                model.HuiPingContent = ds.Tables[0].Rows[0]["HuiPingContent"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HuiPingTime"].ToString() != "")
                {
                    model.HuiPingTime = DateTime.Parse(ds.Tables[0].Rows[0]["HuiPingTime"].ToString());
                }
                model.HuiPingUser = ds.Tables[0].Rows[0]["HuiPingUser"].ToString();
                if (ds.Tables[0].Rows[0]["DingDanDetailId"].ToString() != "")
                {
                    model.DingDanDetailId = decimal.Parse(ds.Tables[0].Rows[0]["DingDanDetailId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PingJiaLv"].ToString() != "")
                {
                    model.PingJiaLv = decimal.Parse(ds.Tables[0].Rows[0]["PingJiaLv"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Invalid"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Invalid"].ToString() == "1") || (ds.Tables[0].Rows[0]["Invalid"].ToString().ToLower() == "true"))
                    {
                        model.Invalid = true;
                    }
                    else
                    {
                        model.Invalid = false;
                    }
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
            strSql.Append("delete from CORE.dbo.PingJiaInfo ");
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
            strSql.Append(" FROM CORE.dbo.PingJiaView  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.PingJiaInfo  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.PingJiaInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

