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
    //MsgText
    public partial class MsgTextDAL
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
            strSql.Append(" FROM  DBMSG.dbo.MsgText ");
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
        public bool Add(MsgTextModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DBMSG.dbo.MsgText (");
            strSql.Append("MsgClassId,MsgTitle,MsgContent,MsgType,CreateTime,EndTime,Extra,SiteId,ZoneId");
            strSql.Append(") values (");
            strSql.Append("@MsgClassId,@MsgTitle,@MsgContent,@MsgType,@CreateTime,@EndTime,@Extra,@SiteId,@ZoneId");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@MsgClassId", SqlDbType.Int,4) ,
                        new SqlParameter("@MsgTitle", SqlDbType.VarChar,200) ,
                        new SqlParameter("@MsgContent", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@MsgType", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@EndTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Extra", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@SiteId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ZoneId", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.MsgClassId;
            parameters[1].Value = model.MsgTitle;
            parameters[2].Value = model.MsgContent;
            parameters[3].Value = model.MsgType;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.EndTime;
            parameters[6].Value = model.Extra;
            parameters[7].Value = model.SiteId;
            parameters[8].Value = model.ZoneId;

            bool result = false;
            try
            {


                model.MsgTextId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "MsgTextId", parameters));


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
        public bool Update(MsgTextModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DBMSG.dbo.MsgText set ");

            strSql.Append(" MsgClassId = @MsgClassId , ");
            strSql.Append(" MsgTitle = @MsgTitle , ");
            strSql.Append(" MsgContent = @MsgContent , ");
            strSql.Append(" MsgType = @MsgType , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" EndTime = @EndTime , ");
            strSql.Append(" Extra = @Extra , ");
            strSql.Append(" SiteId = @SiteId , ");
            strSql.Append(" ZoneId = @ZoneId  ");
            strSql.Append(" where MsgTextId=@MsgTextId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@MsgTextId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MsgClassId", SqlDbType.Int,4) ,
                        new SqlParameter("@MsgTitle", SqlDbType.VarChar,200) ,
                        new SqlParameter("@MsgContent", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@MsgType", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@EndTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Extra", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@SiteId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ZoneId", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.MsgTextId;
            parameters[1].Value = model.MsgClassId;
            parameters[2].Value = model.MsgTitle;
            parameters[3].Value = model.MsgContent;
            parameters[4].Value = model.MsgType;
            parameters[5].Value = model.CreateTime;
            parameters[6].Value = model.EndTime;
            parameters[7].Value = model.Extra;
            parameters[8].Value = model.SiteId;
            parameters[9].Value = model.ZoneId; try
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
        /// 删除duo条数据
        /// </summary>
        public bool DeleteList(string strWhere)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from DBMSG.dbo.MsgText ");
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
            object[] fenyeParmValue = new object[] { "DBMSG.dbo.MsgText  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM DBMSG.dbo.MsgText  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM DBMSG.dbo.MsgText  WITH(NOLOCK) ");
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
            strSql.Append(" FROM DBMSG.dbo.MsgText  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

