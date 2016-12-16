


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //OrderToWork

    public class OrderToWorkModel
    {

        public OrderToWorkModel()
        {

        }




        /// <summary>
        /// OrderToWorkId
        /// </summary>		
        public decimal OrderToWorkId
        {
            get;
            set;
        }
        /// <summary>
        /// OrderToWorkTitle
        /// </summary>		
        public string OrderToWorkTitle
        {
            get;
            set;
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
        /// 实际完成时间
        /// </summary>		
        public DateTime DoneTime
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
        /// ReceivedImgId
        /// </summary>		
        public string ReceivedImgId
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
        /// Invalid
        /// </summary>		
        public bool Invalid
        {
            get;
            set;
        }
        /// <summary>
        /// OrderToWorkStatusId
        /// </summary>		
        public int OrderToWorkStatusId
        {
            get;
            set;
        }
        /// <summary>
        /// Wages
        /// </summary>		
        public decimal Wages
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
        /// 要求完成时间(时间限制)
        /// </summary>		
        public DateTime LimitTime
        {
            get;
            set;
        }
        /// <summary>
        /// ReceivedTime
        /// </summary>		
        public DateTime ReceivedTime
        {
            get;
            set;
        }

    }
}


