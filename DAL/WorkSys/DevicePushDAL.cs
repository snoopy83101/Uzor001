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
    //DevicePush
    public partial class DevicePushDAL
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
            strSql.Append(" FROM  DBMSG.dbo.DevicePush ");
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
        public bool Add(DevicePushModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DBMSG.dbo.DevicePush (");
            strSql.Append("DeviceId,PushType,ReKey,Memo");
            strSql.Append(") values (");
            strSql.Append("@DeviceId,@PushType,@ReKey,@Memo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@DeviceId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PushType", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,250)

            };

            parameters[0].Value = model.DeviceId;
            parameters[1].Value = model.PushType;
            parameters[2].Value = model.ReKey;
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
        public bool Update(DevicePushModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DBMSG.dbo.DevicePush set ");

            strSql.Append(" DeviceId = @DeviceId , ");
            strSql.Append(" PushType = @PushType , ");
            strSql.Append(" ReKey = @ReKey , ");
            strSql.Append(" Memo = @Memo  ");
            strSql.Append(" where DeviceId=@DeviceId and PushType=@PushType and ReKey=@ReKey  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@DeviceId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@PushType", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,250)

            };

            parameters[0].Value = model.DeviceId;
            parameters[1].Value = model.PushType;
            parameters[2].Value = model.ReKey;
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
        public DevicePushModel GetModel(string DeviceId, string PushType, string ReKey)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DeviceId, PushType, ReKey, Memo  ");
            strSql.Append("  from DBMSG.dbo.DevicePush ");
            strSql.Append(" where DeviceId=@DeviceId and PushType=@PushType and ReKey=@ReKey ");
            SqlParameter[] parameters = {
                    new SqlParameter("@DeviceId", SqlDbType.VarChar,50),
                    new SqlParameter("@PushType", SqlDbType.VarChar,50),
                    new SqlParameter("@ReKey", SqlDbType.VarChar,50)            };
            parameters[0].Value = DeviceId;
            parameters[1].Value = PushType;
            parameters[2].Value = ReKey;


            DevicePushModel model = new DevicePushModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.DeviceId = ds.Tables[0].Rows[0]["DeviceId"].ToString();
                model.PushType = ds.Tables[0].Rows[0]["PushType"].ToString();
                model.ReKey = ds.Tables[0].Rows[0]["ReKey"].ToString();
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
            strSql.Append("delete from DBMSG.dbo.DevicePush ");
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
            object[] fenyeParmValue = new object[] { "DBMSG.dbo.DevicePush  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM DBMSG.dbo.DevicePush  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM DBMSG.dbo.DevicePush  WITH(NOLOCK) ");
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
            strSql.Append(" FROM DBMSG.dbo.DevicePush  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

