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
    //Job
    public partial class JobDAL
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
            strSql.Append(" FROM Job ");
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
        public bool Add(JobModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Job(");
            strSql.Append("JobId,CreateTime,ContactName,ContactQQ,ContactEmail,ContactTell,ContactPhone,JobTypeId,MerchantId,IsTop,RecommendLv,JobtTitle,TownId,JobLng,JobLat,JobMemo,HumNum,Sex,WorkYearId,JobPayId,SchoolExpId,CreateUser");
            strSql.Append(") values (");
            strSql.Append("@JobId,@CreateTime,@ContactName,@ContactQQ,@ContactEmail,@ContactTell,@ContactPhone,@JobTypeId,@MerchantId,@IsTop,@RecommendLv,@JobtTitle,@TownId,@JobLng,@JobLat,@JobMemo,@HumNum,@Sex,@WorkYearId,@JobPayId,@SchoolExpId,@CreateUser");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@JobId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@ContactName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactQQ", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@ContactEmail", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactTell", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactPhone", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@JobTypeId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@IsTop", SqlDbType.Bit,1) ,            
                        new SqlParameter("@RecommendLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@JobtTitle", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@TownId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@JobLng", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@JobLat", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@JobMemo", SqlDbType.NVarChar,1000) ,            
                        new SqlParameter("@HumNum", SqlDbType.Int,4) ,            
                        new SqlParameter("@Sex", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@WorkYearId", SqlDbType.Int,4) ,            
                        new SqlParameter("@JobPayId", SqlDbType.Int,4) ,            
                        new SqlParameter("@SchoolExpId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.JobId;
            parameters[1].Value = model.CreateTime;
            parameters[2].Value = model.ContactName;
            parameters[3].Value = model.ContactQQ;
            parameters[4].Value = model.ContactEmail;
            parameters[5].Value = model.ContactTell;
            parameters[6].Value = model.ContactPhone;
            parameters[7].Value = model.JobTypeId;
            parameters[8].Value = model.MerchantId;
            parameters[9].Value = model.IsTop;
            parameters[10].Value = model.RecommendLv;
            parameters[11].Value = model.JobtTitle;
            parameters[12].Value = model.TownId;
            parameters[13].Value = model.JobLng;
            parameters[14].Value = model.JobLat;
            parameters[15].Value = model.JobMemo;
            parameters[16].Value = model.HumNum;
            parameters[17].Value = model.Sex;
            parameters[18].Value = model.WorkYearId;
            parameters[19].Value = model.JobPayId;
            parameters[20].Value = model.SchoolExpId;
            parameters[21].Value = model.CreateUser;

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
        public bool Update(JobModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Job set ");

            strSql.Append(" JobId = @JobId , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" ContactName = @ContactName , ");
            strSql.Append(" ContactQQ = @ContactQQ , ");
            strSql.Append(" ContactEmail = @ContactEmail , ");
            strSql.Append(" ContactTell = @ContactTell , ");
            strSql.Append(" ContactPhone = @ContactPhone , ");
            strSql.Append(" JobTypeId = @JobTypeId , ");
            strSql.Append(" MerchantId = @MerchantId , ");
            strSql.Append(" IsTop = @IsTop , ");
            strSql.Append(" RecommendLv = @RecommendLv , ");
            strSql.Append(" JobtTitle = @JobtTitle , ");
            strSql.Append(" TownId = @TownId , ");
            strSql.Append(" JobLng = @JobLng , ");
            strSql.Append(" JobLat = @JobLat , ");
            strSql.Append(" JobMemo = @JobMemo , ");
            strSql.Append(" HumNum = @HumNum , ");
            strSql.Append(" Sex = @Sex , ");
            strSql.Append(" WorkYearId = @WorkYearId , ");
            strSql.Append(" JobPayId = @JobPayId , ");
            strSql.Append(" SchoolExpId = @SchoolExpId  ");  //这里少了一个逗号
           //   strSql.Append(" CreateUser = @CreateUser  ");  这是无论如何不能修改的
            strSql.Append(" where JobId=@JobId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@JobId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@ContactName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactQQ", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@ContactEmail", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactTell", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ContactPhone", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@JobTypeId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@IsTop", SqlDbType.Bit,1) ,            
                        new SqlParameter("@RecommendLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@JobtTitle", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@TownId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@JobLng", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@JobLat", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@JobMemo", SqlDbType.NVarChar,1000) ,            
                        new SqlParameter("@HumNum", SqlDbType.Int,4) ,            
                        new SqlParameter("@Sex", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@WorkYearId", SqlDbType.Int,4) ,            
                        new SqlParameter("@JobPayId", SqlDbType.Int,4) ,            
                        new SqlParameter("@SchoolExpId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.JobId;
            parameters[1].Value = model.CreateTime;
            parameters[2].Value = model.ContactName;
            parameters[3].Value = model.ContactQQ;
            parameters[4].Value = model.ContactEmail;
            parameters[5].Value = model.ContactTell;
            parameters[6].Value = model.ContactPhone;
            parameters[7].Value = model.JobTypeId;
            parameters[8].Value = model.MerchantId;
            parameters[9].Value = model.IsTop;
            parameters[10].Value = model.RecommendLv;
            parameters[11].Value = model.JobtTitle;
            parameters[12].Value = model.TownId;
            parameters[13].Value = model.JobLng;
            parameters[14].Value = model.JobLat;
            parameters[15].Value = model.JobMemo;
            parameters[16].Value = model.HumNum;
            parameters[17].Value = model.Sex;
            parameters[18].Value = model.WorkYearId;
            parameters[19].Value = model.JobPayId;
            parameters[20].Value = model.SchoolExpId;
            parameters[21].Value = model.CreateUser; 

            try
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
        public JobModel GetModel(string JobId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select JobId, CreateTime, ContactName, ContactQQ, ContactEmail, ContactTell, ContactPhone, JobTypeId, MerchantId, IsTop, RecommendLv, JobtTitle, TownId, JobLng, JobLat, JobMemo, HumNum, Sex, WorkYearId, JobPayId, SchoolExpId, CreateUser  ");
            strSql.Append("  from Job ");
            strSql.Append(" where JobId=@JobId ");
            SqlParameter[] parameters = {
					new SqlParameter("@JobId", SqlDbType.VarChar,50)			};
            parameters[0].Value = JobId;


            JobModel model = new JobModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.JobId = ds.Tables[0].Rows[0]["JobId"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.ContactName = ds.Tables[0].Rows[0]["ContactName"].ToString();
                model.ContactQQ = ds.Tables[0].Rows[0]["ContactQQ"].ToString();
                model.ContactEmail = ds.Tables[0].Rows[0]["ContactEmail"].ToString();
                model.ContactTell = ds.Tables[0].Rows[0]["ContactTell"].ToString();
                model.ContactPhone = ds.Tables[0].Rows[0]["ContactPhone"].ToString();
                if (ds.Tables[0].Rows[0]["JobTypeId"].ToString() != "")
                {
                    model.JobTypeId = decimal.Parse(ds.Tables[0].Rows[0]["JobTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MerchantId"].ToString() != "")
                {
                    model.MerchantId = decimal.Parse(ds.Tables[0].Rows[0]["MerchantId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsTop"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsTop"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsTop"].ToString().ToLower() == "true"))
                    {
                        model.IsTop = true;
                    }
                    else
                    {
                        model.IsTop = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["RecommendLv"].ToString() != "")
                {
                    model.RecommendLv = int.Parse(ds.Tables[0].Rows[0]["RecommendLv"].ToString());
                }
                model.JobtTitle = ds.Tables[0].Rows[0]["JobtTitle"].ToString();
                if (ds.Tables[0].Rows[0]["TownId"].ToString() != "")
                {
                    model.TownId = decimal.Parse(ds.Tables[0].Rows[0]["TownId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JobLng"].ToString() != "")
                {
                    model.JobLng = decimal.Parse(ds.Tables[0].Rows[0]["JobLng"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JobLat"].ToString() != "")
                {
                    model.JobLat = decimal.Parse(ds.Tables[0].Rows[0]["JobLat"].ToString());
                }
                model.JobMemo = ds.Tables[0].Rows[0]["JobMemo"].ToString();
                if (ds.Tables[0].Rows[0]["HumNum"].ToString() != "")
                {
                    model.HumNum = int.Parse(ds.Tables[0].Rows[0]["HumNum"].ToString());
                }
                model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                if (ds.Tables[0].Rows[0]["WorkYearId"].ToString() != "")
                {
                    model.WorkYearId = int.Parse(ds.Tables[0].Rows[0]["WorkYearId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JobPayId"].ToString() != "")
                {
                    model.JobPayId = int.Parse(ds.Tables[0].Rows[0]["JobPayId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SchoolExpId"].ToString() != "")
                {
                    model.SchoolExpId = int.Parse(ds.Tables[0].Rows[0]["SchoolExpId"].ToString());
                }
                model.CreateUser = ds.Tables[0].Rows[0]["CreateUser"].ToString();

                return model;
            }
            else
            {
                return null;
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
            strSql.Append("delete from Job ");
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
            strSql.Append("select top 3000 ");

            strSql.Append(@"JobId ,
        JobtTitle ,
        HumNum ,
        Sex ,
        WorkYearId ,
        JobPayId ,
        SchoolExpId ,
        CreateUser ,
        CreateTime ,
        ContactName ,
        ContactQQ ,
        ContactEmail ,
        ContactTell ,
        ContactPhone ,
        JobTypeId ,
        MerchantId ,
        IsTop ,
        RecommendLv ,
        TownId ,
        JobLat ,
        JobLng ,
        MerchantName ,
        TownName ,
        JobPayTitle ,
        WorkYearTItle ,
        SchoolExpTitle ,
        JobTypeName ,
        MerchantMemo ,
        ParentJobTypeId");
            strSql.Append(" FROM JobView  WITH(NOLOCK) ");
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
        /// 取得JobInfo
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        public DataSet GetJobInfoByJobId(string JobId)
        {
            StringBuilder s = new StringBuilder();
            s.Append(" select * from JobView WITH(NOLOCK)  where JobId='" + JobId + "' ");
            s.Append(@" select ResumeName,r.ImgUrl ,r.ResumeId from  ResumeVsJob rvj WITH(NOLOCK)  LEFT JOIN dbo.ResumeView r WITH(NOLOCK)  ON rvj.ResumeId = r.ResumeId LEFT JOIN dbo.UserView u  WITH(NOLOCK) ON u.UserId=r.CreateUser ");

            s.Append(" where JobId='" + JobId + "' ");
            return helper.ExecSqlReDs(s.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM JobView  WITH(NOLOCK) ");
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
            strSql.Append(" FROM Job ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

