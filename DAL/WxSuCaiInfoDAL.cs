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
    //WxSuCaiInfo
    public partial class WxSuCaiInfoDAL
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
            strSql.Append(" FROM  CORE.dbo.WxSuCaiInfo ");
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
        public bool Add(WxSuCaiInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.WxSuCaiInfo (");
            strSql.Append("Invalid,FmImgId,MerId,InputCode,WxSuCaiTitle,WxSuCaiContent,Memo,WxSuCaiTypeId,WxSuCaiClassId,CreateTime,CreateUser");
            strSql.Append(") values (");
            strSql.Append("@Invalid,@FmImgId,@MerId,@InputCode,@WxSuCaiTitle,@WxSuCaiContent,@Memo,@WxSuCaiTypeId,@WxSuCaiClassId,@CreateTime,@CreateUser");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@FmImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@InputCode", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@WxSuCaiTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WxSuCaiContent", SqlDbType.NText) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@WxSuCaiTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@WxSuCaiClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.Invalid;
            parameters[1].Value = model.FmImgId;
            parameters[2].Value = model.MerId;
            parameters[3].Value = model.InputCode;
            parameters[4].Value = model.WxSuCaiTitle;
            parameters[5].Value = model.WxSuCaiContent;
            parameters[6].Value = model.Memo;
            parameters[7].Value = model.WxSuCaiTypeId;
            parameters[8].Value = model.WxSuCaiClassId;
            parameters[9].Value = model.CreateTime;
            parameters[10].Value = model.CreateUser;

            bool result = false;
            try
            {
                model.WxSuCaiInfoId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "WxSuCaiInfoId", parameters));  //反写
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
        public bool Update(WxSuCaiInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.WxSuCaiInfo set ");

            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" FmImgId = @FmImgId , ");
            strSql.Append(" MerId = @MerId , ");
            strSql.Append(" InputCode = @InputCode , ");
            strSql.Append(" WxSuCaiTitle = @WxSuCaiTitle , ");
            strSql.Append(" WxSuCaiContent = @WxSuCaiContent , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" WxSuCaiTypeId = @WxSuCaiTypeId , ");
            strSql.Append(" WxSuCaiClassId = @WxSuCaiClassId , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" CreateUser = @CreateUser  ");
            strSql.Append(" where WxSuCaiInfoId=@WxSuCaiInfoId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@WxSuCaiInfoId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,            
                        new SqlParameter("@FmImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@InputCode", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@WxSuCaiTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WxSuCaiContent", SqlDbType.NText) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@WxSuCaiTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@WxSuCaiClassId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.WxSuCaiInfoId;
            parameters[1].Value = model.Invalid;
            parameters[2].Value = model.FmImgId;
            parameters[3].Value = model.MerId;
            parameters[4].Value = model.InputCode;
            parameters[5].Value = model.WxSuCaiTitle;
            parameters[6].Value = model.WxSuCaiContent;
            parameters[7].Value = model.Memo;
            parameters[8].Value = model.WxSuCaiTypeId;
            parameters[9].Value = model.WxSuCaiClassId;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.CreateUser; try
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
        public WxSuCaiInfoModel GetModel(decimal WxSuCaiInfoId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select WxSuCaiInfoId, Invalid, FmImgId, MerId, InputCode, WxSuCaiTitle, WxSuCaiContent, Memo, WxSuCaiTypeId, WxSuCaiClassId, CreateTime, CreateUser  ");
            strSql.Append("  from CORE.dbo.WxSuCaiInfo ");
            strSql.Append(" where WxSuCaiInfoId=@WxSuCaiInfoId");
            SqlParameter[] parameters = {
					new SqlParameter("@WxSuCaiInfoId", SqlDbType.Decimal)
			};
            parameters[0].Value = WxSuCaiInfoId;


            WxSuCaiInfoModel model = new WxSuCaiInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["WxSuCaiInfoId"].ToString() != "")
                {
                    model.WxSuCaiInfoId = decimal.Parse(ds.Tables[0].Rows[0]["WxSuCaiInfoId"].ToString());
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
                model.FmImgId = ds.Tables[0].Rows[0]["FmImgId"].ToString();
                if (ds.Tables[0].Rows[0]["MerId"].ToString() != "")
                {
                    model.MerId = decimal.Parse(ds.Tables[0].Rows[0]["MerId"].ToString());
                }
                model.InputCode = ds.Tables[0].Rows[0]["InputCode"].ToString();
                model.WxSuCaiTitle = ds.Tables[0].Rows[0]["WxSuCaiTitle"].ToString();
                model.WxSuCaiContent = ds.Tables[0].Rows[0]["WxSuCaiContent"].ToString();
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                if (ds.Tables[0].Rows[0]["WxSuCaiTypeId"].ToString() != "")
                {
                    model.WxSuCaiTypeId = int.Parse(ds.Tables[0].Rows[0]["WxSuCaiTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WxSuCaiClassId"].ToString() != "")
                {
                    model.WxSuCaiClassId = int.Parse(ds.Tables[0].Rows[0]["WxSuCaiClassId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.CreateUser = ds.Tables[0].Rows[0]["CreateUser"].ToString();

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
            strSql.Append("delete from CORE.dbo.WxSuCaiInfo ");
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
            strSql.Append(" FROM CORE.dbo.WxSuCaiInfo  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.WxSuCaiInfo  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.WxSuCaiInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

