


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Member

    public class MemberModel
    {

        public MemberModel()
        {

        }




        /// <summary>
        /// MemberId
        /// </summary>		
        public decimal MemberId
        {
            get;
            set;
        }
        /// <summary>
        /// MemberCode
        /// </summary>		
        public string MemberCode
        {
            get;
            set;
        }
        /// <summary>
        /// 昵称
        /// </summary>		
        public string NickName
        {
            get;
            set;
        }
        /// <summary>
        /// Sex
        /// </summary>		
        public string Sex
        {
            get;
            set;
        }
        /// <summary>
        /// Pwd
        /// </summary>		
        public string Pwd
        {
            get;
            set;
        }
        /// <summary>
        /// PwdMd5
        /// </summary>		
        public string PwdMd5
        {
            get;
            set;
        }
        /// <summary>
        /// MemberName
        /// </summary>		
        public string MemberName
        {
            get;
            set;
        }
        /// <summary>
        /// RealName
        /// </summary>		
        public string RealName
        {
            get;
            set;
        }
        /// <summary>
        /// Birthday
        /// </summary>		
        public DateTime Birthday
        {
            get;
            set;
        }
        /// <summary>
        /// Memo
        /// </summary>		
        public string Memo
        {
            get;
            set;
        }
        /// <summary>
        /// WxPtId
        /// </summary>		
        public decimal WxPtId
        {
            get;
            set;
        }
        /// <summary>
        /// WxOpenId
        /// </summary>		
        public string WxOpenId
        {
            get;
            set;
        }
        /// <summary>
        /// Invalid
        /// </summary>		
        public bool Invalid
        {
            get;
            set;
        }
        /// <summary>
        /// Email
        /// </summary>		
        public string Email
        {
            get;
            set;
        }
        /// <summary>
        /// Phone
        /// </summary>		
        public string Phone
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
        /// 最后访问时间
        /// </summary>		
        public DateTime LastTime
        {
            get;
            set;
        }
        /// <summary>
        /// MemberLv
        /// </summary>		
        public int MemberLv
        {
            get;
            set;
        }
        /// <summary>
        /// -1为禁止登陆,0为刚刚注册,1是正式会员
        /// </summary>		
        public int Status
        {
            get;
            set;
        }
        /// <summary>
        /// MerId
        /// </summary>		
        public decimal MerId
        {
            get;
            set;
        }
        /// <summary>
        /// PicImgId
        /// </summary>		
        public string PicImgId
        {
            get;
            set;
        }
        /// <summary>
        /// Integral
        /// </summary>		
        public decimal Integral
        {
            get;
            set;
        }
        /// <summary>
        /// MemoName
        /// </summary>		
        public string MemoName
        {
            get;
            set;
        }
        /// <summary>
        /// LastBuyTime
        /// </summary>		
        public DateTime LastBuyTime
        {
            get;
            set;
        }
        /// <summary>
        /// 是否推广员
        /// </summary>		
        public bool IsPromoter
        {
            get;
            set;
        }
        /// <summary>
        /// 推广员编号,没有为0
        /// </summary>		
        public decimal ExtMemberId
        {
            get;
            set;
        }
        /// <summary>
        /// 推广员级别, 0为不是推广员,10为推广员
        /// </summary>		
        public int ExtMemberLv
        {
            get;
            set;
        }
        /// <summary>
        /// LastSiteId
        /// </summary>		
        public decimal LastSiteId
        {
            get;
            set;
        }
        /// <summary>
        /// 最高级别的缝制技能认证,0为未认证
        /// </summary>		
        public int ProcessLvId
        {
            get;
            set;
        }
        /// <summary>
        /// 技能认证状态,0为从未提交认证或者打回认证, 10为提交了正在认证, 20为通过认证
        /// </summary>		
        public int ProcessLvStatusId
        {
            get;
            set;
        }
        /// <summary>
        /// 身份证号
        /// </summary>		
        public string SfzNo
        {
            get;
            set;
        }
        /// <summary>
        /// 身份证正面
        /// </summary>		
        public string SfzImg1
        {
            get;
            set;
        }
        /// <summary>
        /// 身份证反面
        /// </summary>		
        public string SfzImg2
        {
            get;
            set;
        }
        /// <summary>
        /// 所属区县
        /// </summary>		
        public string AreaId
        {
            get;
            set;
        }
        /// <summary>
        /// 详细地址
        /// </summary>		
        public string Address
        {
            get;
            set;
        }
        /// <summary>
        /// 我的余额
        /// </summary>		
        public decimal Amount
        {
            get;
            set;
        }
        /// <summary>
        /// 历史余额
        /// </summary>		
        public decimal OldAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 最大同时接单数
        /// </summary>		
        public int OrderWorkMaxNum
        {
            get;
            set;
        }
        /// <summary>
        /// 订单档期,在此期间内, 不得登记新订单
        /// </summary>		
        public DateTime MaxOrderPlanningTime
        {
            get;
            set;
        }
        /// <summary>
        /// 完成订单数量(通过质检)
        /// </summary>		
        public decimal CheckOrderNum
        {
            get;
            set;
        }
        /// <summary>
        /// 所在团队
        /// </summary>		
        public decimal TeamId
        {
            get;
            set;
        }
        /// <summary>
        /// 所在团队的级别,100是团队负责人
        /// </summary>		
        public int TeamLvId
        {
            get;
            set;
        }

    }
}


