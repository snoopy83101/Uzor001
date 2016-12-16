


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Payment

    public class PaymentModel
    {

        public PaymentModel()
        {

        }




        /// <summary>
        /// PaymentId
        /// </summary>		
        public decimal PaymentId
        {
            get;
            set;
        }
        /// <summary>
        /// PaymentTitle
        /// </summary>		
        public string PaymentTitle
        {
            get;
            set;
        }
        /// <summary>
        /// PaymentMemo
        /// </summary>		
        public string PaymentMemo
        {
            get;
            set;
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
        /// CreateTime
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 支付类别(例如:为订单支付则为'DingDan')
        /// </summary>		
        public string PaymentType
        {
            get;
            set;
        }
        /// <summary>
        /// 关联主键, 例如为订单支付, 则为订单编号
        /// </summary>		
        public string ReKey
        {
            get;
            set;
        }
        /// <summary>
        /// 支付金额
        /// </summary>		
        public decimal PayAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 支付渠道
        /// </summary>		
        public int PayTypeId
        {
            get;
            set;
        }

    }
}


