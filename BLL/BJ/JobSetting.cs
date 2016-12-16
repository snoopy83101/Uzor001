using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace BLL.BJ
{
    public class JobSetting :UserSetting
    {


        BLL.JobBLL bll = new BLL.JobBLL();

         public  DataSet dsJobSetting = null;


        public JobSetting()
        {
            /// 返回薪金,工作经历, 学历的字典
           dsJobSetting=bll.GetJobSetting();
        }

        protected string SchoolExpHtml()
        {
            DataTable dt = dsJobSetting.Tables[2];
            StringBuilder w = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                w.Append("<option value='" + dr["SchoolExpId"] + "'>");
                w.Append(dr["SchoolExpTitle"]);
                w.Append("</option>");
            }
            return w.ToString();
         
          
        }
        protected string JobPayHtml()
        {

            DataTable dt = dsJobSetting.Tables[0];
            StringBuilder w = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                w.Append("<option value='" + dr["JobPayId"] + "'>");
                w.Append(dr["JobPayTitle"]);
                w.Append("</option>");
            }
            return w.ToString();

        }
        protected string WorkYearHtml()
        {

            DataTable dt = dsJobSetting.Tables[1];
            StringBuilder w = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                w.Append("<option value='" + dr["WorkYearId"] + "'>");
                w.Append(dr["WorkYearTItle"]);
                w.Append("</option>");
            }
            return w.ToString();
        }
        protected string JobTypePopHtml()
        {


            StringBuilder w = new StringBuilder();
            DataTable dt = dsJobSetting.Tables[3];
            w.Append("<div id='div_PopJobType' class='div_PopJobType'>");
            w.Append("<div class='clr_10px'></div>");
            w.Append("<h4>选择职位类别:</h4> <img src='/images/job/off.png' onclick='JobTypeOff()' class='img_off'  />");
            w.Append("<div   class='clr'></div>");
            w.Append("<hr class='hr_1' /> ");
            w.Append("<ul id='ul_PopJobList'>");

            w.Append("<li onclick='SelBigJobType(this)' class='JobAll' JobTypeId='0' ParentJobTypeId='0'  >");
            w.Append("");
            w.Append("全部类别");
            w.Append("");
            w.Append("</li>");
            w.Append("");
            foreach (DataRow dr in dt.Rows)
            {

                w.Append("<li onclick='SelBigJobType(this)' ParentJobTypeId='" + dr["ParentJobTypeId"] + "'  JobTypeId='" + dr["JobTypeId"] + "' >");
                w.Append("");
                w.Append(dr["JobTypeName"]);
                w.Append("");
                w.Append("</li>");
                w.Append("");
            }
            w.Append("</ul>");
            w.Append("<div class='clr_10px'></div>");
            w.Append("</div>");
 
            w.Append("");

            return w.ToString();
        }
    }
}


