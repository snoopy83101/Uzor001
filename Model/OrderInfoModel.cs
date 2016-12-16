


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //OrderInfo

    public class OrderInfoModel
    {

        public OrderInfoModel()
        {

        }




        /// <summary>
        /// OrderId
        /// </summary>		
        public string OrderId
        {
            get;
            set;
        }
        /// <summary>
        /// OrderTitle
        /// </summary>		
        public string OrderTitle
        {
            get;
            set;
        }
        /// <summary>
        /// OrderContent
        /// </summary>		
        public string OrderContent
        {
            get;
            set;
        }
        /// <summary>
        /// 加工质量等级
        /// </summary>		
        public int ProcessLvId
        {
            get;
            set;
        }
        /// <summary>
        /// 自定义订单编号
        /// </summary>		
        public string OrderCode
        {
            get;
            set;
        }
        /// <summary>
        /// 客户款号
        /// </summary>		
        public string ClientsCode
        {
            get;
            set;
        }
        /// <summary>
        /// 订单总数(冗余字段, 根据订单明细统计得出)
        /// </summary>		
        public decimal OrderQuantity
        {
            get;
            set;
        }
        /// <summary>
        /// 正在进行生产的数量(冗余字段, 根据工单明细统计得出)
        /// </summary>		
        public decimal CarryOnQuantity
        {
            get;
            set;
        }
        /// <summary>
        /// 单件薪酬
        /// </summary>		
        public decimal OrderWages
        {
            get;
            set;
        }
        /// <summary>
        /// OrderContacts
        /// </summary>		
        public string OrderContacts
        {
            get;
            set;
        }
        /// <summary>
        /// 联系电话
        /// </summary>		
        public string OrderTel
        {
            get;
            set;
        }
        /// <summary>
        /// 联系地址
        /// </summary>		
        public string OrderAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 计量单位, 直接填写中文
        /// </summary>		
        public string Unit
        {
            get;
            set;
        }
        /// <summary>
        /// 10为快工场指定,20为接单人自定
        /// </summary>		
        public int ProcessLocationTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 100为缝制,其他待定
        /// </summary>		
        public int OrderClassId
        {
            get;
            set;
        }
        /// <summary>
        /// OrderImgId
        /// </summary>		
        public string OrderImgId
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
        /// OrderStatusId
        /// </summary>		
        public int OrderStatusId
        {
            get;
            set;
        }
        /// <summary>
        /// CreateUserId
        /// </summary>		
        public string CreateUserId
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
        /// 实际完成时间
        /// </summary>		
        public DateTime DoneTime
        {
            get;
            set;
        }
        /// <summary>
        /// 要求完成时间(时间限制)
        /// </summary>		
        public DateTime LimitTime
        {
            get;
            set;
        }
        /// <summary>
        /// 预计交货时间
        /// </summary>		
        public DateTime PlanningTime
        {
            get;
            set;
        }
        /// <summary>
        /// 起接数量
        /// </summary>		
        public decimal MinQuantity
        {
            get;
            set;
        }
        /// <summary>
        /// 实际用工人数
        /// </summary>		
        public int Places
        {
            get;
            set;
        }
        /// <summary>
        /// 仅存名额, OutPlaces-登记人数
        /// </summary>		
        public int OnlyPlaces
        {
            get;
            set;
        }
        /// <summary>
        /// 默认领取裁片辅料时间
        /// </summary>		
        public DateTime ReceivedTime
        {
            get;
            set;
        }
        /// <summary>
        /// 已经抢单的用户MemberId的集合,中间逗号分隔,例如: 0,2,3,4,5
        /// </summary>		
        public string VsMemberArray
        {
            get;
            set;
        }
        /// <summary>
        /// CheckQuantity
        /// </summary>		
        public decimal CheckQuantity
        {
            get;
            set;
        }
        /// <summary>
        /// 10为拆单发布,20为整单发布
        /// </summary>		
        public int ReleaseTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 每天预计的最高产量,冗余字段, 从OrderExpect改动中自动写入
        /// </summary>		
        public decimal MaxExpectNum
        {
            get;
            set;
        }
        /// <summary>
        /// 分派了多少
        /// </summary>		
        public decimal WorkQuantity
        {
            get;
            set;
        }
        /// <summary>
        /// 预计结算天数,默认20
        /// </summary>		
        public int PendingDay
        {
            get;
            set;
        }
        /// <summary>
        /// 允许抢单登记人数,默认是实际用工人数Places*2
        /// </summary>		
        public int OutPlaces
        {
            get;
            set;
        }
        /// <summary>
        /// 货期,领取裁片到交货时间的周期,单位是天
        /// </summary>		
        public int PlanningDay
        {
            get;
            set;
        }

    }
}


