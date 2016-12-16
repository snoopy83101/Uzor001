


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Education

    public class EducationModel
    {

        public EducationModel()
        {

        }




        /// <summary>
        /// EducationId
        /// </summary>		
        public int EducationId
        {
            get;
            set;
        }
        /// <summary>
        /// 院校名称
        /// </summary>		
        public string EducationSchool
        {
            get;
            set;
        }
        /// <summary>
        /// 专业名称
        /// </summary>		
        public string SubName
        {
            get;
            set;
        }
        /// <summary>
        /// BeginDate
        /// </summary>		
        public DateTime BeginDate
        {
            get;
            set;
        }
        /// <summary>
        /// EndDate
        /// </summary>		
        public DateTime EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// EducationMemo
        /// </summary>		
        public string EducationMemo
        {
            get;
            set;
        }
        /// <summary>
        /// 关联的简历名称
        /// </summary>		
        public string ResumeId
        {
            get;
            set;
        }
        /// <summary>
        /// 排序序号
        /// </summary>		
        public int OrderNo
        {
            get;
            set;
        }

    }
}


