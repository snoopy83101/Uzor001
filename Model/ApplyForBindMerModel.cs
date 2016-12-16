using System;

namespace Model
{
    //testg
    //ApplyForBindMer

    public class ApplyForBindMerModel
    {
        public ApplyForBindMerModel()
        {
        }

        /// <summary>
        /// AutoId
        /// </summary>
        public decimal AutoId
        {
            get;
            set;
        }

        /// <summary>
        /// 绑定的用户编号
        /// </summary>
        public string BindUserId
        {
            get;
            set;
        }

        /// <summary>
        /// 绑定的商家编号
        /// </summary>
        public decimal BindMerchantId
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
        /// 0为提交, 10为在审核 100为审核通过
        /// </summary>
        public int Status
        {
            get;
            set;
        }

        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName
        {
            get;
            set;
        }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string OrganizationName
        {
            get;
            set;
        }

        /// <summary>
        /// 营业执照编号
        /// </summary>
        public string BusinessNo
        {
            get;
            set;
        }

        /// <summary>
        /// 法人姓名
        /// </summary>
        public string LegalName
        {
            get;
            set;
        }

        /// <summary>
        /// 组织机构编号
        /// </summary>
        public string OrganizationNo
        {
            get;
            set;
        }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tell
        {
            get;
            set;
        }

        /// <summary>
        /// 联系qq
        /// </summary>
        public string qq
        {
            get;
            set;
        }

        /// <summary>
        /// 营业执照复印件
        /// </summary>
        public string BusinessImgId
        {
            get;
            set;
        }

        /// <summary>
        /// email
        /// </summary>
        public string email
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get;
            set;
        }
    }
}