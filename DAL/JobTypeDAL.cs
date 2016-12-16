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
    //JobType
    public partial class JobTypeDAL
    {

        #region  //数据操作

        /// <summary>
        /// 数据库帮助对象
        /// </summary>
        private MSSQLHelper helper = new MSSQLHelper();
        #endregion


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(JobTypeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into JobType(");
            strSql.Append("JobTypeName,OrderNo,Memo,ParentJobTypeId");
            strSql.Append(") values (");
            strSql.Append("@JobTypeName,@OrderNo,@Memo,@ParentJobTypeId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@JobTypeName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@ParentJobTypeId", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.JobTypeName;
            parameters[1].Value = model.OrderNo;
            parameters[2].Value = model.Memo;
            parameters[3].Value = model.ParentJobTypeId;

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
        public bool Update(JobTypeModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update JobType set ");

            strSql.Append(" JobTypeName = @JobTypeName , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" ParentJobTypeId = @ParentJobTypeId  ");
            strSql.Append(" where JobTypeId=@JobTypeId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@JobTypeId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@JobTypeName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@ParentJobTypeId", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.JobTypeId;
            parameters[1].Value = model.JobTypeName;
            parameters[2].Value = model.OrderNo;
            parameters[3].Value = model.Memo;
            parameters[4].Value = model.ParentJobTypeId; try
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
            strSql.Append("delete from JobType ");
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
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 * ");
            strSql.Append(" FROM JobType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("PDIMS.dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM JobType  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM JobType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


        public DataSet GetJobSetting()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM JobPay ");   //薪金字典
            strSql.Append("select * ");
            strSql.Append(" FROM dbo.WorkYear ");  //工作经历字典
            strSql.Append("select * "); 
            strSql.Append(" FROM dbo.SchoolExp ");   //学历字典

            strSql.Append("select * ");
            strSql.Append(" FROM dbo.JobType where ParentJobTypeId=0 ");   //第一级工作类别
            return helper.ExecSqlReDs(strSql.ToString());
        }



    }
}

