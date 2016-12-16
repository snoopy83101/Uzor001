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
    //奖品字典表
    public partial class JiangPinDAL
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
            strSql.Append(" FROM  YYHD.dbo.JiangPin ");
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
        public bool Add(JiangPinModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YYHD.dbo.JiangPin (");
            strSql.Append("Num,Invalid,JiangPinName,ChouJiangId,JiangPinLv,JiangPinMemo,ReKey,JiangPinTypeId,JiangPinClassId,GaiLv");
            strSql.Append(") values (");
            strSql.Append("@Num,@Invalid,@JiangPinName,@ChouJiangId,@JiangPinLv,@JiangPinMemo,@ReKey,@JiangPinTypeId,@JiangPinClassId,@GaiLv");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Num", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@JiangPinName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChouJiangId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@JiangPinLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@JiangPinMemo", SqlDbType.VarChar,5000) ,            
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JiangPinTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@JiangPinClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@GaiLv", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.Num;
            parameters[1].Value = model.Invalid;
            parameters[2].Value = model.JiangPinName;
            parameters[3].Value = model.ChouJiangId;
            parameters[4].Value = model.JiangPinLv;
            parameters[5].Value = model.JiangPinMemo;
            parameters[6].Value = model.ReKey;
            parameters[7].Value = model.JiangPinTypeId;
            parameters[8].Value = model.JiangPinClassId;
            parameters[9].Value = model.GaiLv;

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
        public bool Update(JiangPinModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YYHD.dbo.JiangPin set ");

            strSql.Append(" Num = @Num , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" JiangPinName = @JiangPinName , ");
            strSql.Append(" ChouJiangId = @ChouJiangId , ");
            strSql.Append(" JiangPinLv = @JiangPinLv , ");
            strSql.Append(" JiangPinMemo = @JiangPinMemo , ");
            strSql.Append(" ReKey = @ReKey , ");
            strSql.Append(" JiangPinTypeId = @JiangPinTypeId , ");
            strSql.Append(" JiangPinClassId = @JiangPinClassId , ");
            strSql.Append(" GaiLv = @GaiLv  ");
            strSql.Append(" where JiangPinId=@JiangPinId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@JiangPinId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Num", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@JiangPinName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChouJiangId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@JiangPinLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@JiangPinMemo", SqlDbType.VarChar,5000) ,            
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JiangPinTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@JiangPinClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@GaiLv", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.JiangPinId;
            parameters[1].Value = model.Num;
            parameters[2].Value = model.Invalid;
            parameters[3].Value = model.JiangPinName;
            parameters[4].Value = model.ChouJiangId;
            parameters[5].Value = model.JiangPinLv;
            parameters[6].Value = model.JiangPinMemo;
            parameters[7].Value = model.ReKey;
            parameters[8].Value = model.JiangPinTypeId;
            parameters[9].Value = model.JiangPinClassId;
            parameters[10].Value = model.GaiLv; try
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
        public JiangPinModel GetModel(decimal JiangPinId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select JiangPinId, Num, Invalid, JiangPinName, ChouJiangId, JiangPinLv, JiangPinMemo, ReKey, JiangPinTypeId, JiangPinClassId, GaiLv  ");
            strSql.Append("  from YYHD.dbo.JiangPin ");
            strSql.Append(" where JiangPinId=@JiangPinId");
            SqlParameter[] parameters = {
					new SqlParameter("@JiangPinId", SqlDbType.Decimal)
			};
            parameters[0].Value = JiangPinId;


            JiangPinModel model = new JiangPinModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["JiangPinId"].ToString() != "")
                {
                    model.JiangPinId = decimal.Parse(ds.Tables[0].Rows[0]["JiangPinId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Num"].ToString() != "")
                {
                    model.Num = decimal.Parse(ds.Tables[0].Rows[0]["Num"].ToString());
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
                model.JiangPinName = ds.Tables[0].Rows[0]["JiangPinName"].ToString();
                if (ds.Tables[0].Rows[0]["ChouJiangId"].ToString() != "")
                {
                    model.ChouJiangId = decimal.Parse(ds.Tables[0].Rows[0]["ChouJiangId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JiangPinLv"].ToString() != "")
                {
                    model.JiangPinLv = int.Parse(ds.Tables[0].Rows[0]["JiangPinLv"].ToString());
                }
                model.JiangPinMemo = ds.Tables[0].Rows[0]["JiangPinMemo"].ToString();
                model.ReKey = ds.Tables[0].Rows[0]["ReKey"].ToString();
                if (ds.Tables[0].Rows[0]["JiangPinTypeId"].ToString() != "")
                {
                    model.JiangPinTypeId = int.Parse(ds.Tables[0].Rows[0]["JiangPinTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JiangPinClassId"].ToString() != "")
                {
                    model.JiangPinClassId = int.Parse(ds.Tables[0].Rows[0]["JiangPinClassId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GaiLv"].ToString() != "")
                {
                    model.GaiLv = decimal.Parse(ds.Tables[0].Rows[0]["GaiLv"].ToString());
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
            strSql.Append("delete from YYHD.dbo.JiangPin ");
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
            strSql.Append(" FROM YYHD.dbo.JiangPin  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM YYHD.dbo.JiangPin  WITH(NOLOCK) ");
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
            strSql.Append(" FROM YYHD.dbo.JiangPin  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

