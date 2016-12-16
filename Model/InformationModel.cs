


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Information

    public class InformationModel
    {

        public InformationModel()
        {

        }




        /// <summary>
        /// InformationId
        /// </summary>		
        public decimal InformationId
        {
            get;
            set;
        }
        /// <summary>
        /// InformationClassId
        /// </summary>		
        public decimal InformationClassId
        {
            get;
            set;
        }
        /// <summary>
        /// InformationTypeId
        /// </summary>		
        public decimal InformationTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// InformationTitle
        /// </summary>		
        public string InformationTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 供求内容
        /// </summary>		
        public string InformationContent
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>		
        public string InformationMemo
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
        /// 发布人ID
        /// </summary>		
        public string CreateUserId
        {
            get;
            set;
        }
        /// <summary>
        /// 扩展属性
        /// </summary>		
        public string Property
        {
            get;
            set;
        }
        /// <summary>
        /// 电子邮件
        /// </summary>		
        public string Email
        {
            get;
            set;
        }
        /// <summary>
        /// 联系QQ
        /// </summary>		
        public string QQ
        {
            get;
            set;
        }
        /// <summary>
        /// 联系电话
        /// </summary>		
        public string Tel
        {
            get;
            set;
        }
        /// <summary>
        /// 主图
        /// </summary>		
        public string InformationImgId
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

    }
}


