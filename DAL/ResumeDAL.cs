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
    //Resume
    public partial class ResumeDAL
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
        public bool Add(ResumeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Resume(");
            strSql.Append("ResumeId,JobPayId,SchoolExp,TownId,CreateTime,CreateUser,IsTop,TopLv,PicImgId,JobTarget,ResumeName,ResumeSex,ResumeAge,ResumeQQ,ResumeEmail,ResumeTell,ResumeMemo,WorkYear");
            strSql.Append(") values (");
            strSql.Append("@ResumeId,@JobPayId,@SchoolExp,@TownId,@CreateTime,@CreateUser,@IsTop,@TopLv,@PicImgId,@JobTarget,@ResumeName,@ResumeSex,@ResumeAge,@ResumeQQ,@ResumeEmail,@ResumeTell,@ResumeMemo,@WorkYear");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ResumeId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JobPayId", SqlDbType.Int,4) ,            
                        new SqlParameter("@SchoolExp", SqlDbType.Int,4) ,            
                        new SqlParameter("@TownId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@IsTop", SqlDbType.Bit,1) ,            
                        new SqlParameter("@TopLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@PicImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JobTarget", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ResumeName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ResumeSex", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@ResumeAge", SqlDbType.Int,4) ,            
                        new SqlParameter("@ResumeQQ", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ResumeEmail", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ResumeTell", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ResumeMemo", SqlDbType.VarChar,300) ,            
                        new SqlParameter("@WorkYear", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.ResumeId;
            parameters[1].Value = model.JobPayId;
            parameters[2].Value = model.SchoolExp;
            parameters[3].Value = model.TownId;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.CreateUser;
            parameters[6].Value = model.IsTop;
            parameters[7].Value = model.TopLv;
            parameters[8].Value = model.PicImgId;
            parameters[9].Value = model.JobTarget;
            parameters[10].Value = model.ResumeName;
            parameters[11].Value = model.ResumeSex;
            parameters[12].Value = model.ResumeAge;
            parameters[13].Value = model.ResumeQQ;
            parameters[14].Value = model.ResumeEmail;
            parameters[15].Value = model.ResumeTell;
            parameters[16].Value = model.ResumeMemo;
            parameters[17].Value = model.WorkYear;

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
        public bool Update(ResumeModel model)
        {
            bool reValue = true;
            int reCount = 0;
            model.CreateTime = DateTime.Now;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Resume set ");

            strSql.Append(" ResumeId = @ResumeId , ");
            strSql.Append(" JobPayId = @JobPayId , ");
            strSql.Append(" SchoolExp = @SchoolExp , ");
            strSql.Append(" TownId = @TownId , ");
            strSql.Append(" CreateTime = @CreateTime , ");   //还是有必要更新的,因为简历是可以完善刷新的
            strSql.Append(" CreateUser = @CreateUser , ");
            strSql.Append(" IsTop = @IsTop , ");
            strSql.Append(" TopLv = @TopLv , ");
            strSql.Append(" PicImgId = @PicImgId , ");
            strSql.Append(" JobTarget = @JobTarget , ");
            strSql.Append(" ResumeName = @ResumeName , ");
            strSql.Append(" ResumeSex = @ResumeSex , ");
            strSql.Append(" ResumeAge = @ResumeAge , ");
            strSql.Append(" ResumeQQ = @ResumeQQ , ");
            strSql.Append(" ResumeEmail = @ResumeEmail , ");
            strSql.Append(" ResumeTell = @ResumeTell , ");
            strSql.Append(" ResumeMemo = @ResumeMemo , ");
            strSql.Append(" WorkYear = @WorkYear  ");
            strSql.Append(" where ResumeId=@ResumeId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ResumeId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JobPayId", SqlDbType.Int,4) ,            
                        new SqlParameter("@SchoolExp", SqlDbType.Int,4) ,            
                        new SqlParameter("@TownId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@IsTop", SqlDbType.Bit,1) ,            
                        new SqlParameter("@TopLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@PicImgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JobTarget", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ResumeName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ResumeSex", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@ResumeAge", SqlDbType.Int,4) ,            
                        new SqlParameter("@ResumeQQ", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ResumeEmail", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ResumeTell", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ResumeMemo", SqlDbType.VarChar,300) ,            
                        new SqlParameter("@WorkYear", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.ResumeId;
            parameters[1].Value = model.JobPayId;
            parameters[2].Value = model.SchoolExp;
            parameters[3].Value = model.TownId;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.CreateUser;
            parameters[6].Value = model.IsTop;
            parameters[7].Value = model.TopLv;
            parameters[8].Value = model.PicImgId;
            parameters[9].Value = model.JobTarget;
            parameters[10].Value = model.ResumeName;
            parameters[11].Value = model.ResumeSex;
            parameters[12].Value = model.ResumeAge;
            parameters[13].Value = model.ResumeQQ;
            parameters[14].Value = model.ResumeEmail;
            parameters[15].Value = model.ResumeTell;
            parameters[16].Value = model.ResumeMemo;
            parameters[17].Value = model.WorkYear; try
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
            strSql.Append("delete from Resume ");
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
            strSql.Append(" FROM ResumeView ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }



        /// <summary>
        /// 根据用户名,取得用户简历(目前的逻辑,用户只能有一份简历)
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public DataSet GetResumeListByUserId(string UserId)
        {



            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DECLARE @ResumeId AS VARCHAR(50) = (SELECT TOP 1 ResumeId FROM  dbo.Resume WHERE CreateUser='" + UserId + "')  ");
            strSql.Append("select * ");
            strSql.Append(" FROM ResumeView  WITH(NOLOCK)  where ResumeId=@ResumeId ");//简历内容
            strSql.Append(" SELECT * FROM  dbo.ResumeVsJobTypeView  WITH(NOLOCK)  WHERE ResumeId=@ResumeId "); //工作意向职位
            strSql.Append(" SELECT * FROM dbo.Education  WITH(NOLOCK)  WHERE ResumeId=@ResumeId "); //工作教育经历
            return helper.ExecSqlReDs(strSql.ToString());
        
        }


        /// <summary>
        /// 根据用户名,取得用户简历(目前的逻辑,用户只能有一份简历)
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public DataSet GetResumeInfoByResumeId(string ResumeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DECLARE @ResumeId AS VARCHAR(50) ='" + ResumeId + "' ");
            strSql.Append("select * ");
            strSql.Append(" FROM ResumeView  WITH(NOLOCK)  where ResumeId=@ResumeId ");//简历内容
            strSql.Append(" SELECT * FROM  dbo.ResumeVsJobTypeView  WITH(NOLOCK)  WHERE ResumeId=@ResumeId "); //工作意向职位
            strSql.Append(" SELECT * FROM dbo.Education  WITH(NOLOCK)  WHERE ResumeId=@ResumeId "); //工作教育经历
            return helper.ExecSqlReDs(strSql.ToString());

        }





        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Resume ");
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
            strSql.Append(" FROM Resume ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

