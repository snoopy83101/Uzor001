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
    //MsgTypeVsMerRole
    public partial class MsgTypeVsMerRoleDAL
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
            strSql.Append(" FROM  DBMSG.dbo.MsgTypeVsMerRole ");
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
        public bool Add(MsgTypeVsMerRoleModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DBMSG.dbo.MsgTypeVsMerRole (");
            strSql.Append("MsgTypeId,MerRoleId,OrderNo");
            strSql.Append(") values (");
            strSql.Append("@MsgTypeId,@MerRoleId,@OrderNo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@MsgTypeId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerRoleId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)

            };

            parameters[0].Value = model.MsgTypeId;
            parameters[1].Value = model.MerRoleId;
            parameters[2].Value = model.OrderNo;

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
        public bool Update(MsgTypeVsMerRoleModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DBMSG.dbo.MsgTypeVsMerRole set ");

            strSql.Append(" MsgTypeId = @MsgTypeId , ");
            strSql.Append(" MerRoleId = @MerRoleId , ");
            strSql.Append(" OrderNo = @OrderNo  ");
            strSql.Append(" where MsgTypeId=@MsgTypeId and MerRoleId=@MerRoleId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@MsgTypeId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerRoleId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)

            };

            parameters[0].Value = model.MsgTypeId;
            parameters[1].Value = model.MerRoleId;
            parameters[2].Value = model.OrderNo; try
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
        public MsgTypeVsMerRoleModel GetModel(string MsgTypeId, decimal MerRoleId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MsgTypeId, MerRoleId, OrderNo  ");
            strSql.Append("  from DBMSG.dbo.MsgTypeVsMerRole ");
            strSql.Append(" where MsgTypeId=@MsgTypeId and MerRoleId=@MerRoleId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@MsgTypeId", SqlDbType.VarChar,50),
                    new SqlParameter("@MerRoleId", SqlDbType.Decimal,9)         };
            parameters[0].Value = MsgTypeId;
            parameters[1].Value = MerRoleId;


            MsgTypeVsMerRoleModel model = new MsgTypeVsMerRoleModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.MsgTypeId = ds.Tables[0].Rows[0]["MsgTypeId"].ToString();
                if (ds.Tables[0].Rows[0]["MerRoleId"].ToString() != "")
                {
                    model.MerRoleId = decimal.Parse(ds.Tables[0].Rows[0]["MerRoleId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(ds.Tables[0].Rows[0]["OrderNo"].ToString());
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
            strSql.Append("delete from DBMSG.dbo.MsgTypeVsMerRole ");
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
            object[] fenyeParmValue = new object[] { "DBMSG.dbo.MsgTypeVsMerRole  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM DBMSG.dbo.MsgTypeVsMerRole  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM DBMSG.dbo.MsgTypeVsMerRole  WITH(NOLOCK) ");
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
            strSql.Append(" FROM DBMSG.dbo.MsgTypeVsMerRole  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

