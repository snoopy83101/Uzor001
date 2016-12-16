


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //ResumeVsJobType

    public class ResumeVsJobTypeModel
    {

        public ResumeVsJobTypeModel()
        {

        }




        /// <summary>
        /// 简历编号
        /// </summary>		
        public string ResumeId
        {
            get;
            set;
        }
        /// <summary>
        /// 职位
        /// </summary>		
        public decimal JobTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 关联属性
        /// </summary>		
        public string VsType
        {
            get;
            set;
        }
        /// <summary>
        /// 关联简介
        /// </summary>		
        public string Memo
        {
            get;
            set;
        }

    }
}


