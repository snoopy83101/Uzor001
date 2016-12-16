


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Resume

    public class ResumeModel
    {

        public ResumeModel()
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
        /// 简历名称
        /// </summary>		
        public string ResumeName
        {
            get;
            set;
        }
        /// <summary>
        /// 性别
        /// </summary>		
        public string ResumeSex
        {
            get;
            set;
        }
        /// <summary>
        /// 年龄
        /// </summary>		
        public int ResumeAge
        {
            get;
            set;
        }
        /// <summary>
        /// qq
        /// </summary>		
        public string ResumeQQ
        {
            get;
            set;
        }
        /// <summary>
        /// 邮件
        /// </summary>		
        public string ResumeEmail
        {
            get;
            set;
        }
        /// <summary>
        /// 联系电话
        /// </summary>		
        public string ResumeTell
        {
            get;
            set;
        }
        /// <summary>
        /// 个人简介
        /// </summary>		
        public string ResumeMemo
        {
            get;
            set;
        }
        /// <summary>
        /// 工作年限
        /// </summary>		
        public int WorkYear
        {
            get;
            set;
        }
        /// <summary>
        /// JobPayId
        /// </summary>		
        public int JobPayId
        {
            get;
            set;
        }
        /// <summary>
        /// 学历
        /// </summary>		
        public int SchoolExp
        {
            get;
            set;
        }
        /// <summary>
        /// 工作地点意向
        /// </summary>		
        public int TownId
        {
            get;
            set;
        }
        /// <summary>
        /// CreateTime
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// CreateUser
        /// </summary>		
        public string CreateUser
        {
            get;
            set;
        }
        /// <summary>
        /// IsTop
        /// </summary>		
        public bool IsTop
        {
            get;
            set;
        }
        /// <summary>
        /// 置顶级别(推荐级别)
        /// </summary>		
        public int TopLv
        {
            get;
            set;
        }
        /// <summary>
        /// 简历照片
        /// </summary>		
        public string PicImgId
        {
            get;
            set;
        }
        /// <summary>
        /// 求职意向,冗余存储字段
        /// </summary>		
        public string JobTarget
        {
            get;
            set;
        }

    }
}


