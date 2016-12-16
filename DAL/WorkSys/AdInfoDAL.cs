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
    //AdInfo
    public partial class AdInfoDAL
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
            strSql.Append(" FROM  CORE.dbo.AdInfo ");
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
        public bool Add(AdInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.AdInfo (");
            strSql.Append("CreateUser,OrderNo,EndTime,Invalid,Location,W,H,ImgId,StyleStr,AdLabel,AdTitle,AdContent,AdMemo,Url,BindEvent,AdType,AdClass,CreateTime");
            strSql.Append(") values (");
            strSql.Append("@CreateUser,@OrderNo,@EndTime,@Invalid,@Location,@W,@H,@ImgId,@StyleStr,@AdLabel,@AdTitle,@AdContent,@AdMemo,@Url,@BindEvent,@AdType,@AdClass,@CreateTime");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@EndTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@Location", SqlDbType.VarChar,50) ,
                        new SqlParameter("@W", SqlDbType.Int,4) ,
                        new SqlParameter("@H", SqlDbType.Int,4) ,
                        new SqlParameter("@ImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@StyleStr", SqlDbType.VarChar,2000) ,
                        new SqlParameter("@AdLabel", SqlDbType.VarChar,50) ,
                        new SqlParameter("@AdTitle", SqlDbType.VarChar,100) ,
                        new SqlParameter("@AdContent", SqlDbType.VarChar,800) ,
                        new SqlParameter("@AdMemo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@Url", SqlDbType.VarChar,500) ,
                        new SqlParameter("@BindEvent", SqlDbType.VarChar,500) ,
                        new SqlParameter("@AdType", SqlDbType.VarChar,20) ,
                        new SqlParameter("@AdClass", SqlDbType.VarChar,20) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.CreateUser;
            parameters[1].Value = model.OrderNo;
            parameters[2].Value = model.EndTime;
            parameters[3].Value = model.Invalid;
            parameters[4].Value = model.Location;
            parameters[5].Value = model.W;
            parameters[6].Value = model.H;
            parameters[7].Value = model.ImgId;
            parameters[8].Value = model.StyleStr;
            parameters[9].Value = model.AdLabel;
            parameters[10].Value = model.AdTitle;
            parameters[11].Value = model.AdContent;
            parameters[12].Value = model.AdMemo;
            parameters[13].Value = model.Url;
            parameters[14].Value = model.BindEvent;
            parameters[15].Value = model.AdType;
            parameters[16].Value = model.AdClass;
            parameters[17].Value = model.CreateTime;

            bool result = false;
            try
            {


                model.AdId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "AdId", parameters));


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
        public bool Update(AdInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.AdInfo set ");

            strSql.Append(" CreateUser = @CreateUser , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" EndTime = @EndTime , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" Location = @Location , ");
            strSql.Append(" W = @W , ");
            strSql.Append(" H = @H , ");
            strSql.Append(" ImgId = @ImgId , ");
            strSql.Append(" StyleStr = @StyleStr , ");
            strSql.Append(" AdLabel = @AdLabel , ");
            strSql.Append(" AdTitle = @AdTitle , ");
            strSql.Append(" AdContent = @AdContent , ");
            strSql.Append(" AdMemo = @AdMemo , ");
            strSql.Append(" Url = @Url , ");
            strSql.Append(" BindEvent = @BindEvent , ");
            strSql.Append(" AdType = @AdType , ");
            strSql.Append(" AdClass = @AdClass , ");
            strSql.Append(" CreateTime = @CreateTime  ");
            strSql.Append(" where AdId=@AdId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@AdId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@EndTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@Location", SqlDbType.VarChar,50) ,
                        new SqlParameter("@W", SqlDbType.Int,4) ,
                        new SqlParameter("@H", SqlDbType.Int,4) ,
                        new SqlParameter("@ImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@StyleStr", SqlDbType.VarChar,2000) ,
                        new SqlParameter("@AdLabel", SqlDbType.VarChar,50) ,
                        new SqlParameter("@AdTitle", SqlDbType.VarChar,100) ,
                        new SqlParameter("@AdContent", SqlDbType.VarChar,800) ,
                        new SqlParameter("@AdMemo", SqlDbType.VarChar,500) ,
                        new SqlParameter("@Url", SqlDbType.VarChar,500) ,
                        new SqlParameter("@BindEvent", SqlDbType.VarChar,500) ,
                        new SqlParameter("@AdType", SqlDbType.VarChar,20) ,
                        new SqlParameter("@AdClass", SqlDbType.VarChar,20) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.AdId;
            parameters[1].Value = model.CreateUser;
            parameters[2].Value = model.OrderNo;
            parameters[3].Value = model.EndTime;
            parameters[4].Value = model.Invalid;
            parameters[5].Value = model.Location;
            parameters[6].Value = model.W;
            parameters[7].Value = model.H;
            parameters[8].Value = model.ImgId;
            parameters[9].Value = model.StyleStr;
            parameters[10].Value = model.AdLabel;
            parameters[11].Value = model.AdTitle;
            parameters[12].Value = model.AdContent;
            parameters[13].Value = model.AdMemo;
            parameters[14].Value = model.Url;
            parameters[15].Value = model.BindEvent;
            parameters[16].Value = model.AdType;
            parameters[17].Value = model.AdClass;
            parameters[18].Value = model.CreateTime; try
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
        public AdInfoModel GetModel(decimal AdId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AdId, CreateUser, OrderNo, EndTime, Invalid, Location, W, H, ImgId, StyleStr, AdLabel, AdTitle, AdContent, AdMemo, Url, BindEvent, AdType, AdClass, CreateTime  ");
            strSql.Append("  from CORE.dbo.AdInfo ");
            strSql.Append(" where AdId=@AdId");
            SqlParameter[] parameters = {
                    new SqlParameter("@AdId", SqlDbType.Decimal)
            };
            parameters[0].Value = AdId;


            AdInfoModel model = new AdInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["AdId"].ToString() != "")
                {
                    model.AdId = decimal.Parse(ds.Tables[0].Rows[0]["AdId"].ToString());
                }
                model.CreateUser = ds.Tables[0].Rows[0]["CreateUser"].ToString();
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndTime"].ToString() != "")
                {
                    model.EndTime = DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
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
                model.Location = ds.Tables[0].Rows[0]["Location"].ToString();
                if (ds.Tables[0].Rows[0]["W"].ToString() != "")
                {
                    model.W = int.Parse(ds.Tables[0].Rows[0]["W"].ToString());
                }
                if (ds.Tables[0].Rows[0]["H"].ToString() != "")
                {
                    model.H = int.Parse(ds.Tables[0].Rows[0]["H"].ToString());
                }
                model.ImgId = ds.Tables[0].Rows[0]["ImgId"].ToString();
                model.StyleStr = ds.Tables[0].Rows[0]["StyleStr"].ToString();
                model.AdLabel = ds.Tables[0].Rows[0]["AdLabel"].ToString();
                model.AdTitle = ds.Tables[0].Rows[0]["AdTitle"].ToString();
                model.AdContent = ds.Tables[0].Rows[0]["AdContent"].ToString();
                model.AdMemo = ds.Tables[0].Rows[0]["AdMemo"].ToString();
                model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                model.BindEvent = ds.Tables[0].Rows[0]["BindEvent"].ToString();
                model.AdType = ds.Tables[0].Rows[0]["AdType"].ToString();
                model.AdClass = ds.Tables[0].Rows[0]["AdClass"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
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
            strSql.Append("delete from CORE.dbo.AdInfo ");
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
            strSql.Append(" FROM CORE.dbo.AdInfo  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.AdInfo  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.AdInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

