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
    //短信息表
    public partial class StMsgDAL
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
            strSql.Append(" FROM  DBLOG.dbo.StMsg ");
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
        public bool Add(StMsgModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DBLOG.dbo.StMsg (");
            strSql.Append("Invalid,Ip,StMsgCode,StMsgTypeId,StMsgClassId,StMsgContent,PhoneNo,CreateTime,ReKey,MerId");
            strSql.Append(") values (");
            strSql.Append("@Invalid,@Ip,@StMsgCode,@StMsgTypeId,@StMsgClassId,@StMsgContent,@PhoneNo,@CreateTime,@ReKey,@MerId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@Ip", SqlDbType.VarChar,50) ,
                        new SqlParameter("@StMsgCode", SqlDbType.VarChar,20) ,
                        new SqlParameter("@StMsgTypeId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@StMsgClassId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@StMsgContent", SqlDbType.VarChar,500) ,
                        new SqlParameter("@PhoneNo", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerId", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.Invalid;
            parameters[1].Value = model.Ip;
            parameters[2].Value = model.StMsgCode;
            parameters[3].Value = model.StMsgTypeId;
            parameters[4].Value = model.StMsgClassId;
            parameters[5].Value = model.StMsgContent;
            parameters[6].Value = model.PhoneNo;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.ReKey;
            parameters[9].Value = model.MerId;

            bool result = false;
            try
            {
                model.StMsgId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "StMsgId", parameters));
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
        public bool Update(StMsgModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DBLOG.dbo.StMsg set ");

            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" Ip = @Ip , ");
            strSql.Append(" StMsgCode = @StMsgCode , ");
            strSql.Append(" StMsgTypeId = @StMsgTypeId , ");
            strSql.Append(" StMsgClassId = @StMsgClassId , ");
            strSql.Append(" StMsgContent = @StMsgContent , ");
            strSql.Append(" PhoneNo = @PhoneNo , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" ReKey = @ReKey , ");
            strSql.Append(" MerId = @MerId  ");
            strSql.Append(" where StMsgId=@StMsgId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@StMsgId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@Ip", SqlDbType.VarChar,50) ,
                        new SqlParameter("@StMsgCode", SqlDbType.VarChar,20) ,
                        new SqlParameter("@StMsgTypeId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@StMsgClassId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@StMsgContent", SqlDbType.VarChar,500) ,
                        new SqlParameter("@PhoneNo", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@ReKey", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerId", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.StMsgId;
            parameters[1].Value = model.Invalid;
            parameters[2].Value = model.Ip;
            parameters[3].Value = model.StMsgCode;
            parameters[4].Value = model.StMsgTypeId;
            parameters[5].Value = model.StMsgClassId;
            parameters[6].Value = model.StMsgContent;
            parameters[7].Value = model.PhoneNo;
            parameters[8].Value = model.CreateTime;
            parameters[9].Value = model.ReKey;
            parameters[10].Value = model.MerId; try
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
        public StMsgModel GetModel(decimal StMsgId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select StMsgId, Invalid, Ip, StMsgCode, StMsgTypeId, StMsgClassId, StMsgContent, PhoneNo, CreateTime, ReKey, MerId  ");
            strSql.Append("  from DBLOG.dbo.StMsg ");
            strSql.Append(" where StMsgId=@StMsgId");
            SqlParameter[] parameters = {
                    new SqlParameter("@StMsgId", SqlDbType.Decimal)
            };
            parameters[0].Value = StMsgId;


            StMsgModel model = new StMsgModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["StMsgId"].ToString() != "")
                {
                    model.StMsgId = decimal.Parse(ds.Tables[0].Rows[0]["StMsgId"].ToString());
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
                model.Ip = ds.Tables[0].Rows[0]["Ip"].ToString();
                model.StMsgCode = ds.Tables[0].Rows[0]["StMsgCode"].ToString();
                if (ds.Tables[0].Rows[0]["StMsgTypeId"].ToString() != "")
                {
                    model.StMsgTypeId = decimal.Parse(ds.Tables[0].Rows[0]["StMsgTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["StMsgClassId"].ToString() != "")
                {
                    model.StMsgClassId = decimal.Parse(ds.Tables[0].Rows[0]["StMsgClassId"].ToString());
                }
                model.StMsgContent = ds.Tables[0].Rows[0]["StMsgContent"].ToString();
                model.PhoneNo = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.ReKey = ds.Tables[0].Rows[0]["ReKey"].ToString();
                if (ds.Tables[0].Rows[0]["MerId"].ToString() != "")
                {
                    model.MerId = decimal.Parse(ds.Tables[0].Rows[0]["MerId"].ToString());
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
            strSql.Append("delete from DBLOG.dbo.StMsg ");
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
            object[] fenyeParmValue = new object[] { "DBLOG.dbo.StMsgView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
            ds = helper.ExecProc_ReDs("dbo.fenye2", fenyeParmName, fenyeParmValue);
            ds = Common.DataSetting.DataPageSetting(ds, pagesize, currentpage);
            return ds;


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
            strSql.Append(" FROM DBLOG.dbo.StMsg  WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("DBLOG.dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM DBLOG.dbo.StMsg  WITH(NOLOCK) ");
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
            strSql.Append(" FROM DBLOG.dbo.StMsg  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

