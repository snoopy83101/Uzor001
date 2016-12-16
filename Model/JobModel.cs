


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Job

    public class JobModel
    {

        public JobModel()
        {

        }




        /// <summary>
        /// JobId
        /// </summary>		
        public string JobId
        {
            get;
            set;
        }
        /// <summary>
        /// 招聘信息标题
        /// </summary>		
        public string JobtTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 招聘信息简介
        /// </summary>		
        public string JobMemo
        {
            get;
            set;
        }
        /// <summary>
        /// 招聘人数
        /// </summary>		
        public int HumNum
        {
            get;
            set;
        }
        /// <summary>
        /// 性别
        /// </summary>		
        public string Sex
        {
            get;
            set;
        }
        /// <summary>
        /// 工作资历
        /// </summary>		
        public int WorkYearId
        {
            get;
            set;
        }
        /// <summary>
        /// 工资待遇
        /// </summary>		
        public int JobPayId
        {
            get;
            set;
        }
        /// <summary>
        /// 学历
        /// </summary>		
        public int SchoolExpId
        {
            get;
            set;
        }
        /// <summary>
        /// 创建用户
        /// </summary>		
        public string CreateUser
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 联系人姓名
        /// </summary>		
        public string ContactName
        {
            get;
            set;
        }
        /// <summary>
        /// 联系人QQ
        /// </summary>		
        public string ContactQQ
        {
            get;
            set;
        }
        /// <summary>
        /// 联系人邮箱
        /// </summary>		
        public string ContactEmail
        {
            get;
            set;
        }
        /// <summary>
        /// 联系人电话
        /// </summary>		
        public string ContactTell
        {
            get;
            set;
        }
        /// <summary>
        /// 联系人手机
        /// </summary>		
        public string ContactPhone
        {
            get;
            set;
        }
        /// <summary>
        /// 职位类别
        /// </summary>		
        public decimal JobTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 关联商家ID
        /// </summary>		
        public decimal MerchantId
        {
            get;
            set;
        }
        /// <summary>
        /// 是否置顶
        /// </summary>		
        public bool IsTop
        {
            get;
            set;
        }
        /// <summary>
        /// 推荐系数
        /// </summary>		
        public int RecommendLv
        {
            get;
            set;
        }
        /// <summary>
        /// 工作地点
        /// </summary>		
        public decimal TownId
        {
            get;
            set;
        }
        /// <summary>
        /// JobLng
        /// </summary>		
        public decimal JobLng
        {
            get;
            set;
        }
        /// <summary>
        /// JobLat
        /// </summary>		
        public decimal JobLat
        {
            get;
            set;
        }

    }
}


