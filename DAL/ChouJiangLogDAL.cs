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
    //抽奖记录表
    public partial class ChouJiangLogDAL
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
            strSql.Append(" FROM  YYHD.dbo.ChouJiangLog ");
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
        public bool Add(ChouJiangLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into YYHD.dbo.ChouJiangLog (");
            strSql.Append("ChouJiangId,MemberId,JiangPin,CreateTime");
            strSql.Append(") values (");
            strSql.Append("@ChouJiangId,@MemberId,@JiangPin,@CreateTime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@ChouJiangId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@JiangPin", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.ChouJiangId;
            parameters[1].Value = model.MemberId;
            parameters[2].Value = model.JiangPin;
            parameters[3].Value = model.CreateTime;

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
        public bool Update(ChouJiangLogModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update YYHD.dbo.ChouJiangLog set ");

            strSql.Append(" ChouJiangId = @ChouJiangId , ");
            strSql.Append(" MemberId = @MemberId , ");
            strSql.Append(" JiangPin = @JiangPin , ");
            strSql.Append(" CreateTime = @CreateTime  ");
            strSql.Append(" where ChouJiangLogId=@ChouJiangLogId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ChouJiangLogId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ChouJiangId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@JiangPin", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.ChouJiangLogId;
            parameters[1].Value = model.ChouJiangId;
            parameters[2].Value = model.MemberId;
            parameters[3].Value = model.JiangPin;
            parameters[4].Value = model.CreateTime; try
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
        public ChouJiangLogModel GetModel(decimal ChouJiangLogId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ChouJiangLogId, ChouJiangId, MemberId, JiangPin, CreateTime  ");
            strSql.Append("  from YYHD.dbo.ChouJiangLog ");
            strSql.Append(" where ChouJiangLogId=@ChouJiangLogId");
            SqlParameter[] parameters = {
					new SqlParameter("@ChouJiangLogId", SqlDbType.Decimal)
			};
            parameters[0].Value = ChouJiangLogId;


            ChouJiangLogModel model = new ChouJiangLogModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ChouJiangLogId"].ToString() != "")
                {
                    model.ChouJiangLogId = decimal.Parse(ds.Tables[0].Rows[0]["ChouJiangLogId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChouJiangId"].ToString() != "")
                {
                    model.ChouJiangId = decimal.Parse(ds.Tables[0].Rows[0]["ChouJiangId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberId"].ToString() != "")
                {
                    model.MemberId = decimal.Parse(ds.Tables[0].Rows[0]["MemberId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JiangPin"].ToString() != "")
                {
                    model.JiangPin = decimal.Parse(ds.Tables[0].Rows[0]["JiangPin"].ToString());
                }
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
            strSql.Append("delete from YYHD.dbo.ChouJiangLog ");
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
            strSql.Append(" FROM YYHD.dbo.ChouJiangLog  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM YYHD.dbo.ChouJiangLog  WITH(NOLOCK) ");
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
            strSql.Append(" FROM YYHD.dbo.ChouJiangLog  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

