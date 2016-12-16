using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Model;
using Common;
using System.Transactions;

    public partial class JobAjax : Common.BPageSetting
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string para = ReStr("para");
                switch (para)
                {

                    case "InvalidJob":
                        InvalidJob();
                        break;

                    case "doBindMer":
                        doBindMer();
                        break;

                    case "MyJobList":
                        MyJobList();
                        break;

                    case "GetResumeList":
                        GetResumeList();
                        break;
                    case "GetUserResumeInfo":
                        GetUserResumeInfo();
                        break;
                    case "GetMyResumeInfo":
                        GetMyResumeInfo();   //取得我的简历
                        break;

                    case "CheckSubResume":
                        CheckSubResume();
                        break;

                    case "SubResume":
                        SubResume();  //投递简历
                        break;
                    case "SaveResume":

                        SaveResume();  //保存一份简历
                        break;
                    case "SaveEducation":

                        SaveEducation();  //保存一条教育经历
                        break;

                    case "GetJobVsComment":
                        GetJobVsComment();
                        break;

                    case "GetJob":
                        GetJob();
                        break;

                    case "GetJobInfo":
                        GetJobInfo();
                        break;
                    case "GetJobList":
                        GetJobList();
                        break;
                    case "GetJobType":
                        GetJobType();   //取得职位类别列表
                        break;

                    case "SaveJob":

                        SaveJob();
                        break;
                }
            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }
            Response.End();

        }

        private void InvalidJob()
        {
            string JobId = ReStr("JobId", "");
            bool Invalid = ReBool("Invalid", true);   //默认是作废的
            DAL.DalComm.ExStr(" update CORE.dbo.Job set Invalid='true'  where  JobId='" + JobId + "'  ");
            ReTrue();
        }

        private void doBindMer()
        {
            Model.JobModel model = new JobModel();
            decimal MerchantId = ReDecimal("MerchantId", 0);
            string JobId = ReStr("JobId");
            DAL.DalComm.ExReInt(" Update CORE.dbo.Job set MerchantId='" + MerchantId + "' where  JobId='" + JobId + "' ");
            ReTrue();
        }

        private void MyJobList()
        {
            BLL.JobBLL bll = new BLL.JobBLL();
            int CurrentPage = ReInt("CurrentPage");
            DataSet ds = bll.GetJobPageList(" CreateUser='" + Common.CookieSings.GetCurrentUserId() + "' and Invalid=0 order by createTime desc ", CurrentPage, 20);


            RePage(ds);

        }

        private void GetUserResumeInfo()
        {

            BLL.JobBLL bll = new BLL.JobBLL();
            string ResumeId = ReStr("ResumeId");
            DataSet ds = bll.GetResumeInfoByResumeId(ResumeId);
            string resumeJson = JsonHelper.ToJsonNo1(ds.Tables[0]);
            string resumeVsJobType = JsonHelper.ToJson(ds.Tables[1]);
            string Education = JsonHelper.ToJson(ds.Tables[2]);
            ReDict.Add("resumeJson", resumeJson);  //简历主体
            ReDict.Add("resumeVsJobTypeList", resumeVsJobType); //意向职位列表
            ReDict.Add("EducationList", Education);  //教育工作经历列表
            ReTrue();
        }

        private void GetResumeList()
        {
            decimal JobTypeId = ReDecimal("JobTypeId", 0);
            decimal WorkYearId = ReDecimal("WorkYearId", 0);
            decimal JobPayId = ReDecimal("JobPayId", 0);
            decimal TownId = ReDecimal("TownId", 0);
            decimal SchoolExpId = ReDecimal("SchoolExpId", 0);
            decimal ParentJobTypeId = ReDecimal("ParentJobTypeId", 0);

            string inputStr = ReStr("inputStr", "");

            int PageInt = ReInt("PageInt", 40);
            bool Invalid = ReBool("Invalid", false);

            int currentPage = ReInt("currentPage");
            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");


            if (ParentJobTypeId == 0)
            {

            }
            else
            {

                //选择了一级类别
                if (JobTypeId != 0)
                {//选择了二级类别
                    s.Append(" and  ResumeId IN (SELECT ResumeId FROM dbo.ResumeVsJobType WHERE JobTypeId='" + JobTypeId + "'  )  ");
                }
                else
                {
                    //选择了一级类别没有选择二级类别

                    s.Append(@"and ResumeId in (SELECT   ResumeId
FROM    dbo.ResumeVsJobType rvjt
        LEFT JOIN dbo.JobType jt ON rvjt.JobTypeId = jt.JobTypeId
        LEFT JOIN dbo.JobType pjt ON jt.ParentJobTypeId = pjt.JobTypeId
        WHERE pjt.JobTypeId='" + ParentJobTypeId + "')");
                }

            }



            if (WorkYearId != 0)
            {
                s.Append(" and WorkYearId='" + WorkYearId + "' ");
            }
            if (JobPayId != 0)
            {


                //选择的子类别

                s.Append(" and JobPayId='" + JobPayId + "' ");

            }
            if (SchoolExpId != 0)
            {
                s.Append(" and SchoolExpId ='" + SchoolExpId + "' ");

            }

            if (TownId != 0)
            {

                s.Append(" and TownId='" + TownId + "' ");
            }


            s.Append(" order by CreateTime desc ");
            BLL.JobBLL bll = new BLL.JobBLL();

            DataSet ds = bll.GetResumePageList(s.ToString(), currentPage);
            RePage(ds);
        }

        private void GetMyResumeInfo()
        {
            BLL.UserBLL ubll = new BLL.UserBLL();
            string UserId = ubll.CurrentUserId();



            BLL.JobBLL bll = new BLL.JobBLL();



            DataSet ds = bll.GetResumeListByUserId(UserId);

            string resumeJson = JsonHelper.ToJsonNo1(ds.Tables[0]);
            string resumeVsJobType = JsonHelper.ToJson(ds.Tables[1]);
            string Education = JsonHelper.ToJson(ds.Tables[2]);
            ReDict.Add("resumeJson", resumeJson);  //简历主体
            ReDict.Add("resumeVsJobTypeList", resumeVsJobType); //意向职位列表
            ReDict.Add("EducationList", Education);  //教育工作经历列表
            ReTrue();
        }

        private void CheckSubResume()
        {
            BLL.JobBLL bll = new BLL.JobBLL();
            BLL.UserBLL ubll = new BLL.UserBLL();
            DataTable dt = bll.GetSubResumeList(" JobId='" + ReStr("JobId") + "' AND CreateUser='" + ubll.CurrentUserId() + "'  ");

            if (dt.Rows.Count > 0)
            {
                throw new Exception("你已经投递了这份职位!");
            }
            else
            {

                ReTrue();
            }
        }


        /// <summary>
        /// 投递简历
        /// </summary>
        private void SubResume()
        {
            BLL.JobBLL bll = new BLL.JobBLL();
            BLL.UserBLL ubll = new BLL.UserBLL();
            DataTable dt = bll.GetResumeList(" CreateUser='" + ubll.CurrentUserId() + "' ");
            if (dt.Rows.Count == 0)
            {

                throw new Exception("您还没有简历请先创建一份简历!");
            }
            else
            {

                DataRow dr = dt.Rows[0];
                Model.ResumeVsJobModel model = new ResumeVsJobModel();
                model.JobId = ReStr("JobId");
                model.Memo = "";
                model.ResumeId = dr["ResumeId"].ToString();
                model.VsType = "正常投递";
                try
                {
                    bll.SubResume(model);
                }
                catch
                {
                    throw new Exception("投递错误! 您是否已经投递了此职位?!");

                }
                ReTrue();
            }
        }

        private void SaveEducation()
        {
            throw new NotImplementedException();
        }

        private void SaveResume()
        {
            BLL.UserBLL uBll = new BLL.UserBLL();
            ResumeModel model = new ResumeModel();
            BLL.JobBLL bll = new BLL.JobBLL();

            model.CreateUser = uBll.CurrentUserId();
            DataTable dtResumeByUser = bll.GetResumeList(" CreateUser='" + model.CreateUser + "' ");
            if (dtResumeByUser.Rows.Count > 0)
            {//这个用户已经有简历了
                DataRow dr = dtResumeByUser.Rows[0];
                model.ResumeId = dr["ResumeId"].ToString();
            }
            else
            {
                //还没有创建简历
                model.ResumeId = ReStr("ResumeId").Trim(); //传过来一般也是空的
            }
            model.IsTop = false;
            model.JobPayId = ReInt("JobPayId");
            model.ResumeAge = ReInt("ResumeAge");
            model.ResumeEmail = ReStr("ResumeEmail");
            model.JobTarget = ReStr("JobTarget");
            model.ResumeMemo = ReStr("ResumeMemo");
            model.ResumeName = ReStr("ResumeName");
            model.ResumeQQ = ReStr("ResumeQQ");
            model.ResumeSex = ReStr("ResumeSex");
            model.ResumeTell = ReStr("ResumeTell");
            model.SchoolExp = ReInt("SchoolExp");
            model.TopLv = ReInt("TopLv", 0);
            model.TownId = ReInt("TownId");
            model.WorkYear = ReInt("WorkYear");
            model.PicImgId = ReStr("PicImgId");

            #region 事务开启
            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
            #endregion
                bll.SaveResume(model);
                //string EduArrayStr = ReStr("EduArrayStr").Trim();

                //string[] EduArray = EduArrayStr.Split('|');
                //int i = EduArray.Length;



                bll.DeleteEdu(" ResumeId='" + model.ResumeId + "' ");
                DataTable dtEduArray = ReTable("EduArrayStr");

                if (dtEduArray != null)
                {
                    foreach (DataRow dr in dtEduArray.Rows)
                    {
                        EducationModel eduModel = new EducationModel();
                        eduModel.EducationSchool = dr["EducationSchool"].ToString();
                        eduModel.SubName = dr["SubName"].ToString();
                        eduModel.BeginDate = DateTime.Parse(dr["BeginDate"].ToString());
                        eduModel.EndDate = DateTime.Parse(dr["EndDate"].ToString());
                        eduModel.EducationMemo = "";
                        eduModel.ResumeId = model.ResumeId;
                        eduModel.OrderNo = 1;
                        bll.SaveEduCation(eduModel);
                    }
                }




                //开始添加求职意向的职位
                DataTable dtJobType = ReTable("JobTypeArrayStr");

                if (dtJobType != null)
                {
                    bll.DeleteResumeVsJobType(" ResumeId='" + model.ResumeId + "' ");

                    foreach (DataRow dr in dtJobType.Rows)
                    {
                        ResumeVsJobTypeModel vsmodel = new ResumeVsJobTypeModel();
                        vsmodel.JobTypeId = decimal.Parse(dr["JobTypeId"].ToString());
                        vsmodel.ResumeId = model.ResumeId;

                        bll.SaveResumeVsJobType(vsmodel);

                    }
                }
                else
                {


                }



                ReDict2.Add("ResumeId", model.ResumeId);

                #region 事务关闭
                transactionScope.Complete();


            }
                #endregion

            ReTrue();

        }



        private void GetJobVsComment()
        {
            string JobId = ReStr("JobId");
            int CurrentPage = ReInt("currentPage");
            BLL.JobBLL bll = new BLL.JobBLL();

            DataSet ds = bll.GetJobCommentPageList(" JobId='" + JobId + "' order by CreateTime Desc ", CurrentPage);
            RePage(ds);
        }

        private void GetJob()
        {


            string JobId = ReStr("JobId");

            BLL.JobBLL bll = new BLL.JobBLL();
            DataTable dt = bll.GetJobList(" JobId='" + JobId + "' ").Tables[0];
            string json = JsonHelper.ToJsonNo1(dt);
            ReDict.Add("JobInfo", json);
            ReTrue();
        }

        private void GetJobInfo()
        {
            string JobId = ReStr("JobId");
            BLL.JobBLL bll = new BLL.JobBLL();
            DataSet ds = bll.GetJobInfo(JobId);
            ReDict.Add("JobInfo", JsonHelper.ToJsonNo1(ds));
            ReTrue();
        }

        //获得职位列表
        private void GetJobList()
        {
            decimal JobTypeId = ReDecimal("JobTypeId", 0);    //
            decimal WorkYearId = ReDecimal("WorkYearId", 0);  //工作年限id
            decimal JobPayId = ReDecimal("JobPayId", 0);   //薪酬待遇ID
            decimal TownId = ReDecimal("TownId", 0);  //城镇ID
            decimal SchoolExpId = ReDecimal("SchoolExpId", 0);
            decimal ParentJobTypeId = ReDecimal("ParentJobTypeId", 0);

            decimal MerchantId = ReDecimal("MerchantId", 0);
            string inputStr = ReStr("inputStr", "");
            int currentPage = ReInt("currentPage");
            int PageInt = ReInt("PageInt", 40);
            bool Invalid = ReBool("Invalid", false);

            StringBuilder s = new StringBuilder();
            s.Append(" 1=1 ");

            s.Append(" and Invalid='" + Invalid.ToString() + "' ");


            if (MerchantId == 0)
            {

            }
            else
            {
                s.Append(" and MerchantId='" + MerchantId + "' ");
            }

            if (inputStr.Trim() != "")
            {
                s.Append(" and JobtTitle like '%" + inputStr + "%' or JobMemo like '%" + inputStr + "%' ");

            }

            if (JobTypeId != 0)
            {//如果选择了子类别


                s.Append(" and  JobTypeId='" + JobTypeId + "' ");

            }
            else
            {
                //如果没有选择子类别

                if (ParentJobTypeId == 0)
                {

                }
                else
                {

                    //选择的首级类别,则查询首级类别即可
                    s.Append(" and ParentJobTypeId='" + ParentJobTypeId + "' ");
                }
            }

            if (WorkYearId != 0)
            {
                s.Append(" and WorkYearId='" + WorkYearId + "' ");
            }
            if (JobPayId != 0)
            {


                //选择的子类别

                s.Append(" and JobPayId='" + JobPayId + "' ");

            }
            if (SchoolExpId != 0)
            {
                s.Append(" and SchoolExpId ='" + SchoolExpId + "' ");

            }

            if (TownId != 0)
            {

                s.Append(" and TownId='" + TownId + "' ");
            }


            s.Append(" order by RecommendLv desc, CreateTime desc ");
            BLL.JobBLL bll = new BLL.JobBLL();

            DataSet ds = bll.GetJobPageList(s.ToString(), currentPage, PageInt);
            RePage(ds);

        }

        private void GetJobType()
        {
            decimal JobTypeId = ReDecimal("JobTypeId");
            BLL.JobBLL bll = new BLL.JobBLL();
            DataTable dt = bll.GetJobType(" ParentJobTypeId='" + JobTypeId + "' ");
            string jsonList = Common.JsonHelper.ToJson(dt);
            ReDict.Add("JobTypeList", jsonList);
            ReTrue();

        }

        private void SaveJob()
        {


            BLL.UserBLL userbll = new BLL.UserBLL();

            BLL.JobBLL bll = new BLL.JobBLL();
            JobModel model = new JobModel();
            model.JobId = ReStr("JobId", "");

            if (userbll.HasPower(model.JobId))
            {
                //如果没有权限发布
            }
            else
            {
                ReTrue();
            }

            if (model.JobId.Trim() != "")
            {//修改 

                model = bll.GetJobModel(model.JobId);
                if (model.CreateUser == Common.CookieSings.GetCurrentUserId())
                {



                }
                else
                {
                    if (!userbll.IsAdministrator())
                    {
                        throw new Exception("您没有权限执行此操作!");

                    }
                }
            }
            else
            {
                model.RecommendLv = ReInt("RecommendLv", 0);
            }
            model.HumNum = ReInt("HumNum");
            model.ContactEmail = ReStr("ContactEmail");
            model.ContactName = ReStr("ContactName");
            model.ContactPhone = ReStr("ContactPhone", "");
            model.ContactQQ = ReStr("ContactQQ", "");
            model.ContactTell = ReStr("ContactTell", "");
            model.HumNum = ReInt("HumNum", 0);
            model.IsTop = ReBool("IsTop", false);
            model.JobMemo = ReStr("JobMemo");
            model.JobPayId = ReInt("JobPayId");
            model.JobtTitle = ReStr("JobtTitle");
            model.JobTypeId = ReInt("JobTypeId");
            model.MerchantId = ReDecimal("MerchantId", 0);
            model.SchoolExpId = ReInt("SchoolExpId");
            model.Sex = ReStr("Sex");
            model.WorkYearId = ReInt("WorkYearId");
            model.JobLat = ReDecimal("JobLat", 0);
            model.JobLng = ReDecimal("JobLng", 0);
            model.TownId = ReDecimal("TownId");
            bll.SaveJob(model);

            ReDict2.Add("JobId", model.JobId);
            ReTrue();

        }



    }
