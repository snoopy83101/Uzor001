


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //DingDanInfo

    public class DingDanInfoModel
    {

        public DingDanInfoModel()
        {

        }




        /// <summary>
        /// DingDanId
        /// </summary>		
        public string DingDanId
        {
            get;
            set;
        }
        /// <summary>
        /// 订单名称
        /// </summary>		
        public string DingDanTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 创建人
        /// </summary>		
        public decimal CreateMember
        {
            get;
            set;
        }
        /// <summary>
        /// 卖家ID
        /// </summary>		
        public decimal MerchantId
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
        /// EnTime
        /// </summary>		
        public DateTime EnTime
        {
            get;
            set;
        }
        /// <summary>
        /// -1作废,0下单,10确认,20派送,30送达
        /// </summary>		
        public int Status
        {
            get;
            set;
        }
        /// <summary>
        /// 是否完结
        /// </summary>		
        public bool IsDone
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
        /// 收货地址ID
        /// </summary>		
        public decimal AddressId
        {
            get;
            set;
        }
        /// <summary>
        /// 使用了多少积分
        /// </summary>		
        public decimal UseJiFen
        {
            get;
            set;
        }
        /// <summary>
        /// DingDanAttr
        /// </summary>		
        public string DingDanAttr
        {
            get;
            set;
        }
        /// <summary>
        /// PeiSongTime1
        /// </summary>		
        public DateTime PeiSongTime1
        {
            get;
            set;
        }
        /// <summary>
        /// PeiSongTime2
        /// </summary>		
        public DateTime PeiSongTime2
        {
            get;
            set;
        }
        /// <summary>
        /// PeiSongTypeId
        /// </summary>		
        public decimal PeiSongTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 派送员ID
        /// </summary>		
        public string PaiSongUserId
        {
            get;
            set;
        }
        /// <summary>
        /// 配货员ID
        /// </summary>		
        public string PeiHuoUserId
        {
            get;
            set;
        }
        /// <summary>
        /// 客户在下达订单时选择的支付方式
        /// </summary>		
        public int PayTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 1网站客户端, 2手机app客户端, 3微信客户端,4电话
        /// </summary>		
        public int SourseTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// PeiHuoTime
        /// </summary>		
        public DateTime PeiHuoTime
        {
            get;
            set;
        }
        /// <summary>
        /// 分部ID
        /// </summary>		
        public string BranchId
        {
            get;
            set;
        }

    }
}


