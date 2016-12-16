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
    //回复列表
    public partial class WxSendInfoDAL
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
            strSql.Append(" FROM  CORE.dbo.WxSendInfo ");
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
        public bool Add(WxSendInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.WxSendInfo (");
            strSql.Append("FmImgId,WxSuCaiId,SendContent,InputCode,WxSendTitle,CreateTime,CreateUser,WxSendType,WxSendClassId,WxPtId,Memo");
            strSql.Append(") values (");
            strSql.Append("@FmImgId,@WxSuCaiId,@SendContent,@InputCode,@WxSendTitle,@CreateTime,@CreateUser,@WxSendType,@WxSendClassId,@WxPtId,@Memo");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@FmImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WxSuCaiId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@SendContent", SqlDbType.NText) ,            
                        new SqlParameter("@InputCode", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@WxSendTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WxSendType", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WxSendClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@WxPtId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,500)             
              
            };

            parameters[0].Value = model.FmImgId;
            parameters[1].Value = model.WxSuCaiId;
            parameters[2].Value = model.SendContent;
            parameters[3].Value = model.InputCode;
            parameters[4].Value = model.WxSendTitle;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.CreateUser;
            parameters[7].Value = model.WxSendType;
            parameters[8].Value = model.WxSendClassId;
            parameters[9].Value = model.WxPtId;
            parameters[10].Value = model.Memo;

            bool result = false;
            try
            {
                model.WxSendId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "WxSendId", parameters));
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
        public bool Update(WxSendInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.WxSendInfo set ");

            strSql.Append(" FmImgId = @FmImgId , ");
            strSql.Append(" WxSuCaiId = @WxSuCaiId , ");
            strSql.Append(" SendContent = @SendContent , ");
            strSql.Append(" InputCode = @InputCode , ");
            strSql.Append(" WxSendTitle = @WxSendTitle , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" CreateUser = @CreateUser , ");
            strSql.Append(" WxSendType = @WxSendType , ");
            strSql.Append(" WxSendClassId = @WxSendClassId , ");
            strSql.Append(" WxPtId = @WxPtId , ");
            strSql.Append(" Memo = @Memo  ");
            strSql.Append(" where WxSendId=@WxSendId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@WxSendId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FmImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WxSuCaiId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@SendContent", SqlDbType.NText) ,            
                        new SqlParameter("@InputCode", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@WxSendTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WxSendType", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WxSendClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@WxPtId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,500)             
              
            };

            parameters[0].Value = model.WxSendId;
            parameters[1].Value = model.FmImgId;
            parameters[2].Value = model.WxSuCaiId;
            parameters[3].Value = model.SendContent;
            parameters[4].Value = model.InputCode;
            parameters[5].Value = model.WxSendTitle;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.CreateUser;
            parameters[8].Value = model.WxSendType;
            parameters[9].Value = model.WxSendClassId;
            parameters[10].Value = model.WxPtId;
            parameters[11].Value = model.Memo; try
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
        public WxSendInfoModel GetModel(decimal WxSendId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select WxSendId, FmImgId, WxSuCaiId, SendContent, InputCode, WxSendTitle, CreateTime, CreateUser, WxSendType, WxSendClassId, WxPtId, Memo  ");
            strSql.Append("  from CORE.dbo.WxSendInfo ");
            strSql.Append(" where WxSendId=@WxSendId");
            SqlParameter[] parameters = {
					new SqlParameter("@WxSendId", SqlDbType.Decimal)
			};
            parameters[0].Value = WxSendId;


            WxSendInfoModel model = new WxSendInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["WxSendId"].ToString() != "")
                {
                    model.WxSendId = decimal.Parse(ds.Tables[0].Rows[0]["WxSendId"].ToString());
                }
                model.FmImgId = ds.Tables[0].Rows[0]["FmImgId"].ToString();
                if (ds.Tables[0].Rows[0]["WxSuCaiId"].ToString() != "")
                {
                    model.WxSuCaiId = decimal.Parse(ds.Tables[0].Rows[0]["WxSuCaiId"].ToString());
                }
                model.SendContent = ds.Tables[0].Rows[0]["SendContent"].ToString();
                model.InputCode = ds.Tables[0].Rows[0]["InputCode"].ToString();
                model.WxSendTitle = ds.Tables[0].Rows[0]["WxSendTitle"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.CreateUser = ds.Tables[0].Rows[0]["CreateUser"].ToString();
                model.WxSendType = ds.Tables[0].Rows[0]["WxSendType"].ToString();
                if (ds.Tables[0].Rows[0]["WxSendClassId"].ToString() != "")
                {
                    model.WxSendClassId = int.Parse(ds.Tables[0].Rows[0]["WxSendClassId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WxPtId"].ToString() != "")
                {
                    model.WxPtId = decimal.Parse(ds.Tables[0].Rows[0]["WxPtId"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();

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
            strSql.Append("delete from CORE.dbo.WxSendInfo ");
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
            strSql.Append(" FROM CORE.dbo.WxSendView  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.WxSendInfo  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.WxSendInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

