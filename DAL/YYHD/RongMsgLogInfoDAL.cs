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
    //RongMsgLogInfo
    public partial class RongMsgLogInfoDAL
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
            strSql.Append(" FROM  YYHD.dbo.RongMsgLogInfo ");
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
        public bool Add(RongMsgLogInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YYHD.dbo.RongMsgLogInfo (");
            strSql.Append("MessageId,RongMsgLogTypeId,ImgUrl,Extra,Title,SendRole,ReRole,ReDeviceId,ReMemberId,ReUserId,ContentText,CreateTime,RongUserId");
            strSql.Append(") values (");
            strSql.Append("@MessageId,@RongMsgLogTypeId,@ImgUrl,@Extra,@Title,@SendRole,@ReRole,@ReDeviceId,@ReMemberId,@ReUserId,@ContentText,@CreateTime,@RongUserId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@MessageId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@RongMsgLogTypeId", SqlDbType.VarChar,20) ,
                        new SqlParameter("@ImgUrl", SqlDbType.VarChar,250) ,
                        new SqlParameter("@Extra", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@Title", SqlDbType.VarChar,100) ,
                        new SqlParameter("@SendRole", SqlDbType.VarChar,20) ,
                        new SqlParameter("@ReRole", SqlDbType.VarChar,20) ,
                        new SqlParameter("@ReDeviceId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ReMemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ReUserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ContentText", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@RongUserId", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.MessageId;
            parameters[1].Value = model.RongMsgLogTypeId;
            parameters[2].Value = model.ImgUrl;
            parameters[3].Value = model.Extra;
            parameters[4].Value = model.Title;
            parameters[5].Value = model.SendRole;
            parameters[6].Value = model.ReRole;
            parameters[7].Value = model.ReDeviceId;
            parameters[8].Value = model.ReMemberId;
            parameters[9].Value = model.ReUserId;
            parameters[10].Value = model.ContentText;
            parameters[11].Value = model.CreateTime;
            parameters[12].Value = model.RongUserId;
            bool result = false;
            try
            {
                model.RongMsgLogId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "RongMsgLogId", parameters));
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
        public bool Update(RongMsgLogInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YYHD.dbo.RongMsgLogInfo set ");

            strSql.Append(" MessageId = @MessageId , ");
            strSql.Append(" RongMsgLogTypeId = @RongMsgLogTypeId , ");
            strSql.Append(" ImgUrl = @ImgUrl , ");
            strSql.Append(" Extra = @Extra , ");
            strSql.Append(" Title = @Title , ");
            strSql.Append(" SendRole = @SendRole , ");
            strSql.Append(" ReRole = @ReRole , ");
            strSql.Append(" ReDeviceId = @ReDeviceId , ");
            strSql.Append(" ReMemberId = @ReMemberId , ");
            strSql.Append(" ReUserId = @ReUserId , ");
            strSql.Append(" ContentText = @ContentText , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" RongUserId = @RongUserId  ");
            strSql.Append(" where RongMsgLogId=@RongMsgLogId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@RongMsgLogId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MessageId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@RongMsgLogTypeId", SqlDbType.VarChar,20) ,
                        new SqlParameter("@ImgUrl", SqlDbType.VarChar,250) ,
                        new SqlParameter("@Extra", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@Title", SqlDbType.VarChar,100) ,
                        new SqlParameter("@SendRole", SqlDbType.VarChar,20) ,
                        new SqlParameter("@ReRole", SqlDbType.VarChar,20) ,
                        new SqlParameter("@ReDeviceId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ReMemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ReUserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ContentText", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@RongUserId", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.RongMsgLogId;
            parameters[1].Value = model.MessageId;
            parameters[2].Value = model.RongMsgLogTypeId;
            parameters[3].Value = model.ImgUrl;
            parameters[4].Value = model.Extra;
            parameters[5].Value = model.Title;
            parameters[6].Value = model.SendRole;
            parameters[7].Value = model.ReRole;
            parameters[8].Value = model.ReDeviceId;
            parameters[9].Value = model.ReMemberId;
            parameters[10].Value = model.ReUserId;
            parameters[11].Value = model.ContentText;
            parameters[12].Value = model.CreateTime;
            parameters[13].Value = model.RongUserId; try
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
        public RongMsgLogInfoModel GetModel(decimal RongMsgLogId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select RongMsgLogId, MessageId, RongMsgLogTypeId, ImgUrl, Extra, Title, SendRole, ReRole, ReDeviceId, ReMemberId, ReUserId, ContentText, CreateTime, RongUserId  ");
            strSql.Append("  from YYHD.dbo.RongMsgLogInfo ");
            strSql.Append(" where RongMsgLogId=@RongMsgLogId");
            SqlParameter[] parameters = {
                    new SqlParameter("@RongMsgLogId", SqlDbType.Decimal)
            };
            parameters[0].Value = RongMsgLogId;


            RongMsgLogInfoModel model = new RongMsgLogInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["RongMsgLogId"].ToString() != "")
                {
                    model.RongMsgLogId = decimal.Parse(ds.Tables[0].Rows[0]["RongMsgLogId"].ToString());
                }
                model.MessageId = ds.Tables[0].Rows[0]["MessageId"].ToString();
                model.RongMsgLogTypeId = ds.Tables[0].Rows[0]["RongMsgLogTypeId"].ToString();
                model.ImgUrl = ds.Tables[0].Rows[0]["ImgUrl"].ToString();
                model.Extra = ds.Tables[0].Rows[0]["Extra"].ToString();
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                model.SendRole = ds.Tables[0].Rows[0]["SendRole"].ToString();
                model.ReRole = ds.Tables[0].Rows[0]["ReRole"].ToString();
                model.ReDeviceId = ds.Tables[0].Rows[0]["ReDeviceId"].ToString();
                if (ds.Tables[0].Rows[0]["ReMemberId"].ToString() != "")
                {
                    model.ReMemberId = decimal.Parse(ds.Tables[0].Rows[0]["ReMemberId"].ToString());
                }
                model.ReUserId = ds.Tables[0].Rows[0]["ReUserId"].ToString();
                model.ContentText = ds.Tables[0].Rows[0]["ContentText"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.RongUserId = ds.Tables[0].Rows[0]["RongUserId"].ToString();

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
            strSql.Append("delete from YYHD.dbo.RongMsgLogInfo ");
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
        public DataSet GetPageList(string strWhere, string Order, int currentpage, int pagesize, string col)
        {
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "TableName", "ReFieldsStr", "OrderString", "WhereString", "PageSize", "PageIndex", "TotalRecord" };
            object[] fenyeParmValue = new object[] { "YYHD.dbo.RongMsgLogInfo  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
            ds = helper.ExecProc_ReDs("dbo.fenye2", fenyeParmName, fenyeParmValue);
            ds = Common.DataSetting.DataPageSetting(ds, pagesize, currentpage);
            return ds;


        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize, string cols)
        {
            if (cols == "")
            {
                cols = " * ";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 " + cols + " ");
            strSql.Append(" FROM YYHD.dbo.RongMsgLogInfo  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM YYHD.dbo.RongMsgLogInfo  WITH(NOLOCK) ");
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
            strSql.Append(" FROM YYHD.dbo.RongMsgLogInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

