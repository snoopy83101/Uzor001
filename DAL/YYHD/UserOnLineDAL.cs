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
    //UserOnLine
    public partial class UserOnLineDAL
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
            strSql.Append(" FROM  YYHD.dbo.UserOnLine ");
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
        public bool Add(UserOnLineModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YYHD.dbo.UserOnLine (");
            strSql.Append("UserId,MerId,UserOnlineStatusId,PushTypeId,LastTime");
            strSql.Append(") values (");
            strSql.Append("@UserId,@MerId,@UserOnlineStatusId,@PushTypeId,@LastTime");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@UserOnlineStatusId", SqlDbType.Int,4) ,
                        new SqlParameter("@PushTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@LastTime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.UserId;
            parameters[1].Value = model.MerId;
            parameters[2].Value = model.UserOnlineStatusId;
            parameters[3].Value = model.PushTypeId;
            parameters[4].Value = model.LastTime;

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
        public bool Update(UserOnLineModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YYHD.dbo.UserOnLine set ");

            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" MerId = @MerId , ");
            strSql.Append(" UserOnlineStatusId = @UserOnlineStatusId , ");
            strSql.Append(" PushTypeId = @PushTypeId , ");
            strSql.Append(" LastTime = @LastTime  ");
            strSql.Append(" where UserId=@UserId and MerId=@MerId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@UserOnlineStatusId", SqlDbType.Int,4) ,
                        new SqlParameter("@PushTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@LastTime", SqlDbType.DateTime)

            };

            parameters[0].Value = model.UserId;
            parameters[1].Value = model.MerId;
            parameters[2].Value = model.UserOnlineStatusId;
            parameters[3].Value = model.PushTypeId;
            parameters[4].Value = model.LastTime; try
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
        public UserOnLineModel GetModel(string UserId, decimal MerId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserId, MerId, UserOnlineStatusId, PushTypeId, LastTime  ");
            strSql.Append("  from YYHD.dbo.UserOnLine ");
            strSql.Append(" where UserId=@UserId and MerId=@MerId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserId", SqlDbType.VarChar,50),
                    new SqlParameter("@MerId", SqlDbType.Decimal,9)         };
            parameters[0].Value = UserId;
            parameters[1].Value = MerId;


            UserOnLineModel model = new UserOnLineModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                if (ds.Tables[0].Rows[0]["MerId"].ToString() != "")
                {
                    model.MerId = decimal.Parse(ds.Tables[0].Rows[0]["MerId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserOnlineStatusId"].ToString() != "")
                {
                    model.UserOnlineStatusId = int.Parse(ds.Tables[0].Rows[0]["UserOnlineStatusId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PushTypeId"].ToString() != "")
                {
                    model.PushTypeId = int.Parse(ds.Tables[0].Rows[0]["PushTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastTime"].ToString() != "")
                {
                    model.LastTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastTime"].ToString());
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
            strSql.Append("delete from YYHD.dbo.UserOnLine ");
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
            object[] fenyeParmValue = new object[] { "YYHD.dbo.UserOnLine  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM YYHD.dbo.UserOnLine  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM YYHD.dbo.UserOnLine  WITH(NOLOCK) ");
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
            strSql.Append(" FROM YYHD.dbo.UserOnLine  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

