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
    //ChouJiang
    public partial class ChouJiangDAL
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
            strSql.Append(" FROM  YYHD.dbo.ChouJiang ");
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
        public bool Add(ChouJiangModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YYHD.dbo.ChouJiang (");
            strSql.Append("ChouJiangClassId,Memo,Invalid,ChouJiangCode,ChouJiangName,CreateTime,BgTime,EndTime,LimitNum,WxPtId,ChouJiangTypeId");
            strSql.Append(") values (");
            strSql.Append("@ChouJiangClassId,@Memo,@Invalid,@ChouJiangCode,@ChouJiangName,@CreateTime,@BgTime,@EndTime,@LimitNum,@WxPtId,@ChouJiangTypeId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@ChouJiangClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,4000) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@ChouJiangCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChouJiangName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@BgTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@EndTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@LimitNum", SqlDbType.Int,4) ,            
                        new SqlParameter("@WxPtId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ChouJiangTypeId", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.ChouJiangClassId;
            parameters[1].Value = model.Memo;
            parameters[2].Value = model.Invalid;
            parameters[3].Value = model.ChouJiangCode;
            parameters[4].Value = model.ChouJiangName;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.BgTime;
            parameters[7].Value = model.EndTime;
            parameters[8].Value = model.LimitNum;
            parameters[9].Value = model.WxPtId;
            parameters[10].Value = model.ChouJiangTypeId;

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
        public bool Update(ChouJiangModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YYHD.dbo.ChouJiang set ");

            strSql.Append(" ChouJiangClassId = @ChouJiangClassId , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" ChouJiangCode = @ChouJiangCode , ");
            strSql.Append(" ChouJiangName = @ChouJiangName , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" BgTime = @BgTime , ");
            strSql.Append(" EndTime = @EndTime , ");
            strSql.Append(" LimitNum = @LimitNum , ");
            strSql.Append(" WxPtId = @WxPtId , ");
            strSql.Append(" ChouJiangTypeId = @ChouJiangTypeId  ");
            strSql.Append(" where ChouJiangId=@ChouJiangId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ChouJiangId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ChouJiangClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,4000) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@ChouJiangCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ChouJiangName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@BgTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@EndTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@LimitNum", SqlDbType.Int,4) ,            
                        new SqlParameter("@WxPtId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ChouJiangTypeId", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.ChouJiangId;
            parameters[1].Value = model.ChouJiangClassId;
            parameters[2].Value = model.Memo;
            parameters[3].Value = model.Invalid;
            parameters[4].Value = model.ChouJiangCode;
            parameters[5].Value = model.ChouJiangName;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.BgTime;
            parameters[8].Value = model.EndTime;
            parameters[9].Value = model.LimitNum;
            parameters[10].Value = model.WxPtId;
            parameters[11].Value = model.ChouJiangTypeId; try
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
        public ChouJiangModel GetModel(decimal ChouJiangId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ChouJiangId, ChouJiangClassId, Memo, Invalid, ChouJiangCode, ChouJiangName, CreateTime, BgTime, EndTime, LimitNum, WxPtId, ChouJiangTypeId  ");
            strSql.Append("  from YYHD.dbo.ChouJiang ");
            strSql.Append(" where ChouJiangId=@ChouJiangId");
            SqlParameter[] parameters = {
					new SqlParameter("@ChouJiangId", SqlDbType.Decimal)
			};
            parameters[0].Value = ChouJiangId;


            ChouJiangModel model = new ChouJiangModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ChouJiangId"].ToString() != "")
                {
                    model.ChouJiangId = decimal.Parse(ds.Tables[0].Rows[0]["ChouJiangId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChouJiangClassId"].ToString() != "")
                {
                    model.ChouJiangClassId = int.Parse(ds.Tables[0].Rows[0]["ChouJiangClassId"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
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
                model.ChouJiangCode = ds.Tables[0].Rows[0]["ChouJiangCode"].ToString();
                model.ChouJiangName = ds.Tables[0].Rows[0]["ChouJiangName"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BgTime"].ToString() != "")
                {
                    model.BgTime = DateTime.Parse(ds.Tables[0].Rows[0]["BgTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndTime"].ToString() != "")
                {
                    model.EndTime = DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LimitNum"].ToString() != "")
                {
                    model.LimitNum = int.Parse(ds.Tables[0].Rows[0]["LimitNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WxPtId"].ToString() != "")
                {
                    model.WxPtId = decimal.Parse(ds.Tables[0].Rows[0]["WxPtId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChouJiangTypeId"].ToString() != "")
                {
                    model.ChouJiangTypeId = int.Parse(ds.Tables[0].Rows[0]["ChouJiangTypeId"].ToString());
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
            strSql.Append("delete from YYHD.dbo.ChouJiang ");
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
            strSql.Append(" FROM YYHD.dbo.ChouJiang  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM YYHD.dbo.ChouJiang  WITH(NOLOCK) ");
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
            strSql.Append(" FROM YYHD.dbo.ChouJiang  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

