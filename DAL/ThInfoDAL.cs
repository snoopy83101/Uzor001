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
    //ThInfo
    public partial class ThInfoDAL
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
            strSql.Append(" FROM  CORE.dbo.ThInfo ");
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
        public bool Add(ThInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.ThInfo (");
            strSql.Append("ThId,DingDanId,ThTitle,ThJiFen,ThAmount,ThTypeId,Memo,ThCreateTime");
            strSql.Append(") values (");
            strSql.Append("@ThId,@DingDanId,@ThTitle,@ThJiFen,@ThAmount,@ThTypeId,@Memo,@ThCreateTime");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ThId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DingDanId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ThTitle", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ThJiFen", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ThAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ThTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@ThCreateTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.ThId;
            parameters[1].Value = model.DingDanId;
            parameters[2].Value = model.ThTitle;
            parameters[3].Value = model.ThJiFen;
            parameters[4].Value = model.ThAmount;
            parameters[5].Value = model.ThTypeId;
            parameters[6].Value = model.Memo;
            parameters[7].Value = model.ThCreateTime;

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
        public bool Update(ThInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.ThInfo set ");

            strSql.Append(" ThId = @ThId , ");
            strSql.Append(" DingDanId = @DingDanId , ");
            strSql.Append(" ThTitle = @ThTitle , ");
            strSql.Append(" ThJiFen = @ThJiFen , ");
            strSql.Append(" ThAmount = @ThAmount , ");
            strSql.Append(" ThTypeId = @ThTypeId , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" ThCreateTime = @ThCreateTime  ");
            strSql.Append(" where ThId=@ThId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ThId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DingDanId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ThTitle", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ThJiFen", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ThAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ThTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@ThCreateTime", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.ThId;
            parameters[1].Value = model.DingDanId;
            parameters[2].Value = model.ThTitle;
            parameters[3].Value = model.ThJiFen;
            parameters[4].Value = model.ThAmount;
            parameters[5].Value = model.ThTypeId;
            parameters[6].Value = model.Memo;
            parameters[7].Value = model.ThCreateTime; try
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
        public ThInfoModel GetModel(string ThId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ThId, DingDanId, ThTitle, ThJiFen, ThAmount, ThTypeId, Memo, ThCreateTime  ");
            strSql.Append("  from CORE.dbo.ThInfo ");
            strSql.Append(" where ThId=@ThId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ThId", SqlDbType.VarChar,50)			};
            parameters[0].Value = ThId;


            ThInfoModel model = new ThInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.ThId = ds.Tables[0].Rows[0]["ThId"].ToString();
                model.DingDanId = ds.Tables[0].Rows[0]["DingDanId"].ToString();
                model.ThTitle = ds.Tables[0].Rows[0]["ThTitle"].ToString();
                if (ds.Tables[0].Rows[0]["ThJiFen"].ToString() != "")
                {
                    model.ThJiFen = decimal.Parse(ds.Tables[0].Rows[0]["ThJiFen"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ThAmount"].ToString() != "")
                {
                    model.ThAmount = decimal.Parse(ds.Tables[0].Rows[0]["ThAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ThTypeId"].ToString() != "")
                {
                    model.ThTypeId = int.Parse(ds.Tables[0].Rows[0]["ThTypeId"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                if (ds.Tables[0].Rows[0]["ThCreateTime"].ToString() != "")
                {
                    model.ThCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["ThCreateTime"].ToString());
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
            strSql.Append("delete from CORE.dbo.ThInfo ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.ThInfo  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM CORE.dbo.ThInfo  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.ThInfo  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.ThInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

