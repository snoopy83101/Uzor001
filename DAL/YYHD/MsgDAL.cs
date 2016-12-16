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
    //Msg
    public partial class MsgDAL
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
            strSql.Append(" FROM  DBMSG.dbo.Msg ");
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
        public bool Add(MsgModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DBMSG.dbo.Msg (");
            strSql.Append("SendDeviceId,TargetDeviceId,MsgTextId,ZoneId,SiteId,MsgStatusId");
            strSql.Append(") values (");
            strSql.Append("@SendDeviceId,@TargetDeviceId,@MsgTextId,@ZoneId,@SiteId,@MsgStatusId");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@SendDeviceId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@TargetDeviceId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MsgTextId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ZoneId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@SiteId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MsgStatusId", SqlDbType.Int,4)

            };

            parameters[0].Value = model.SendDeviceId;
            parameters[1].Value = model.TargetDeviceId;
            parameters[2].Value = model.MsgTextId;
            parameters[3].Value = model.ZoneId;
            parameters[4].Value = model.SiteId;
            parameters[5].Value = model.MsgStatusId;

            bool result = false;
            try
            {


                model.MsgId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "MsgId", parameters));


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
        public bool Update(MsgModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DBMSG.dbo.Msg set ");

            strSql.Append(" SendDeviceId = @SendDeviceId , ");
            strSql.Append(" TargetDeviceId = @TargetDeviceId , ");
            strSql.Append(" MsgTextId = @MsgTextId , ");
            strSql.Append(" ZoneId = @ZoneId , ");
            strSql.Append(" SiteId = @SiteId , ");
            strSql.Append(" MsgStatusId = @MsgStatusId  ");
            strSql.Append(" where MsgId=@MsgId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@MsgId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SendDeviceId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@TargetDeviceId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MsgTextId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ZoneId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@SiteId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MsgStatusId", SqlDbType.Int,4)

            };

            parameters[0].Value = model.MsgId;
            parameters[1].Value = model.SendDeviceId;
            parameters[2].Value = model.TargetDeviceId;
            parameters[3].Value = model.MsgTextId;
            parameters[4].Value = model.ZoneId;
            parameters[5].Value = model.SiteId;
            parameters[6].Value = model.MsgStatusId; try
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
        public MsgModel GetModel(decimal MsgId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MsgId, SendDeviceId, TargetDeviceId, MsgTextId, ZoneId, SiteId, MsgStatusId  ");
            strSql.Append("  from DBMSG.dbo.Msg ");
            strSql.Append(" where MsgId=@MsgId");
            SqlParameter[] parameters = {
                    new SqlParameter("@MsgId", SqlDbType.Decimal)
            };
            parameters[0].Value = MsgId;


            MsgModel model = new MsgModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MsgId"].ToString() != "")
                {
                    model.MsgId = decimal.Parse(ds.Tables[0].Rows[0]["MsgId"].ToString());
                }
                model.SendDeviceId = ds.Tables[0].Rows[0]["SendDeviceId"].ToString();
                model.TargetDeviceId = ds.Tables[0].Rows[0]["TargetDeviceId"].ToString();
                if (ds.Tables[0].Rows[0]["MsgTextId"].ToString() != "")
                {
                    model.MsgTextId = decimal.Parse(ds.Tables[0].Rows[0]["MsgTextId"].ToString());
                }
                model.ZoneId = ds.Tables[0].Rows[0]["ZoneId"].ToString();
                if (ds.Tables[0].Rows[0]["SiteId"].ToString() != "")
                {
                    model.SiteId = decimal.Parse(ds.Tables[0].Rows[0]["SiteId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MsgStatusId"].ToString() != "")
                {
                    model.MsgStatusId = int.Parse(ds.Tables[0].Rows[0]["MsgStatusId"].ToString());
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
            strSql.Append("delete from DBMSG.dbo.Msg ");
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
            object[] fenyeParmValue = new object[] { "DBMSG.dbo.MsgView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM DBMSG.dbo.Msg  WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("DBMSG.dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM DBMSG.dbo.Msg  WITH(NOLOCK) ");
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
            strSql.Append(" FROM DBMSG.dbo.Msg  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

