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
    //Education
    public partial class EducationDAL
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
        public bool Add(EducationModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Education(");
            strSql.Append("EducationSchool,SubName,BeginDate,EndDate,EducationMemo,ResumeId,OrderNo");
            strSql.Append(") values (");
            strSql.Append("@EducationSchool,@SubName,@BeginDate,@EndDate,@EducationMemo,@ResumeId,@OrderNo");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@EducationSchool", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@SubName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BeginDate", SqlDbType.Date,3) ,            
                        new SqlParameter("@EndDate", SqlDbType.Date,3) ,            
                        new SqlParameter("@EducationMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@ResumeId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.EducationSchool;
            parameters[1].Value = model.SubName;
            parameters[2].Value = model.BeginDate;
            parameters[3].Value = model.EndDate;
            parameters[4].Value = model.EducationMemo;
            parameters[5].Value = model.ResumeId;
            parameters[6].Value = model.OrderNo;

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
        public bool Update(EducationModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Education set ");

            strSql.Append(" EducationSchool = @EducationSchool , ");
            strSql.Append(" SubName = @SubName , ");
            strSql.Append(" BeginDate = @BeginDate , ");
            strSql.Append(" EndDate = @EndDate , ");
            strSql.Append(" EducationMemo = @EducationMemo , ");
            strSql.Append(" ResumeId = @ResumeId , ");
            strSql.Append(" OrderNo = @OrderNo  ");
            strSql.Append(" where EducationId=@EducationId ");

            SqlParameter[] parameters = {
			            new SqlParameter("@EducationId", SqlDbType.Int,4) ,            
                        new SqlParameter("@EducationSchool", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@SubName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BeginDate", SqlDbType.Date,3) ,            
                        new SqlParameter("@EndDate", SqlDbType.Date,3) ,            
                        new SqlParameter("@EducationMemo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@ResumeId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OrderNo", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.EducationId;
            parameters[1].Value = model.EducationSchool;
            parameters[2].Value = model.SubName;
            parameters[3].Value = model.BeginDate;
            parameters[4].Value = model.EndDate;
            parameters[5].Value = model.EducationMemo;
            parameters[6].Value = model.ResumeId;
            parameters[7].Value = model.OrderNo; try
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
            strSql.Append("delete from Education ");
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
            strSql.Append(" FROM Education ");
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
            strSql.Append(" FROM Education ");
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
            strSql.Append(" FROM Education ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

