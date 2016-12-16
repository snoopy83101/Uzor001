using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
namespace BLL
{
    public class JobBLL
    {

        DAL.JobTypeDAL JTDal = new DAL.JobTypeDAL();
        DAL.JobDAL JobDal = new DAL.JobDAL();
        DAL.JobVsCommentDAL JvcDal = new DAL.JobVsCommentDAL();

        #region 简历
  
  

        /// <summary>
        /// 获得投递简历的列表
        /// </summary>
        /// <param name="StrWhere"></param>
        /// <returns></returns>
        public DataTable GetSubResumeList(string StrWhere)
        {
            DAL.ResumeVsJobDAL dal = new DAL.ResumeVsJobDAL();
            return dal.GetList(StrWhere).Tables[0];

        }


        /// <summary>
        /// 投递一份简历
        /// </summary>
        /// <param name="model"></param>
        public void SubResume(ResumeVsJobModel model)
        {

            DAL.ResumeVsJobDAL dal = new DAL.ResumeVsJobDAL();
            dal.Add(model);
        }

        /// <summary>
        /// 获得简历分页列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="currentpage"></param>
        /// <returns></returns>
        public DataSet GetResumePageList(string strWhere, int currentpage)
        {
            DAL.ResumeDAL dal = new DAL.ResumeDAL();
            return dal.GetPageList(strWhere, currentpage, 30);

        }
        /// <summary>
        /// 保存一份简历
        /// </summary>
        /// <param name="model"></param>
        public void SaveResume(ResumeModel model)
        {
            DAL.ResumeDAL dal = new DAL.ResumeDAL();

            
            switch (model.ResumeId)
            {

                case "":

                  
                    model.ResumeId = Common.TimeString.GetNowDifString();
                    model.CreateTime = DateTime.Now;
                    dal.Add(model);
                    break;
                default:

                    dal.Update(model);
                    break;
            }
        }

        /// <summary>
        /// 根据用户编号取出用户的简历主体(目前的逻辑,一个用户只有一份简历)
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public DataSet GetResumeListByUserId(string UserId)
        {

            DAL.ResumeDAL dal = new DAL.ResumeDAL();
            return dal.GetResumeListByUserId(UserId);
        }
        /// <summary>
        /// 根据简历编号取得简历主体
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public DataSet GetResumeInfoByResumeId(string ResumeId)
        {

            DAL.ResumeDAL dal = new DAL.ResumeDAL();
            return dal.GetResumeInfoByResumeId(ResumeId);
        }



        /// <summary>
        /// 取得简历列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetResumeList(string strWhere)
        {

            DAL.ResumeDAL dal = new DAL.ResumeDAL();
            return dal.GetList(strWhere).Tables[0];

        }



        /// <summary>
        /// 删除工作/教育经历和简历的关联
        /// </summary>
        /// <param name="strWhere"></param>
        public void DeleteEdu(string strWhere)
        {
            DAL.EducationDAL dal = new DAL.EducationDAL();
            dal.DeleteList(strWhere);
        }

        /// <summary>
        /// 删除职位类别和简历的关联
        /// </summary>
        /// <param name="StrWHere"></param>
        public void DeleteResumeVsJobType(string StrWHere)
        {
            DAL.ResumeVsJobTypeDAL dal = new DAL.ResumeVsJobTypeDAL();
            dal.DeleteList(StrWHere);

        }

        /// <summary>
        /// 获得职位信息列表
        /// </summary>
        /// <param name="StrWHere"></param>
        /// <returns></returns>
        public DataTable GetEduCation(string StrWHere)
        {
            DAL.EducationDAL dal = new DAL.EducationDAL();
            return dal.GetList(StrWHere).Tables[0];
        }
        public void SaveEduCation(Model.EducationModel model)
        {

            DAL.EducationDAL dal = new DAL.EducationDAL();
            dal.Add(model);
        }

        public void SaveResumeVsJobType(Model.ResumeVsJobTypeModel model)
        {
            DAL.ResumeVsJobTypeDAL dal = new DAL.ResumeVsJobTypeDAL();
            dal.Add(model);
        }


        /// <summary>
        /// 返回工作经历列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetEduCationPageList(string strWhere)
        {
            DAL.EducationDAL dal = new DAL.EducationDAL();
            return dal.GetList(strWhere);
        }


        #endregion


        #region 职位

        public JobModel GetJobModel(string JobId)
        {

            return JobDal.GetModel(JobId);
        }

        /// <summary>
        /// 依次返回薪金,工作经历, 学历的字典
        /// </summary>
        /// <returns></returns>
        public DataSet GetJobSetting()
        {
            return JTDal.GetJobSetting();

        }




        public DataSet GetJobInfo(string JobId)
        {
            DAL.JobDAL dal = new DAL.JobDAL();
            return dal.GetJobInfoByJobId(JobId);
        }

        /// <summary>
        /// 获得分页点评
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public DataSet GetJobCommentPageList(string strWhere, int currentPage)
        {
            return JvcDal.GetPageList(strWhere, currentPage, 10);
        }



        /// <summary>
        /// 分页取得工作信息列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public DataSet GetJobPageList(string strWhere, int currentPage,int PageInt)
        {
            return JobDal.GetPageList(strWhere, currentPage, PageInt);

        }

        public DataSet GetJobList(string strWhere)
        {

            return JobDal.GetList(strWhere);
        }


        public DataTable GetJobType(string StrWhere)
        {

            DataTable dt = JTDal.GetList(StrWhere).Tables[0];
            return dt;
        }



        /// <summary>
        /// 保存一个职位
        /// </summary>
        /// <param name="model"></param>
        public void SaveJob(JobModel model)
        {
            if (model.JobId == "")
            {
                //新增
                model.CreateUser = Common.CookieSings.GetCurrentUserId();
                model.CreateTime = DateTime.Now;
                model.JobId = Common.TimeString.GetNowDifString();
                JobDal.Add(model);
            }
            else
            {
                //修改则不修改

                model.CreateTime = DateTime.Now;
                JobDal.Update(model);

            }
        }

        #endregion

    }
}
