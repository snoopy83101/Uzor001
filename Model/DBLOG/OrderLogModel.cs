


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //OrderLog

    public class OrderLogModel
    {

        public OrderLogModel()
        {

        }




        /// <summary>
        /// OrderLogId
        /// </summary>		
        public decimal OrderLogId
        {
            get;
            set;
        }
        /// <summary>
        /// OrderLogTitle
        /// </summary>		
        public string OrderLogTitle
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
        /// OrderToWorkId
        /// </summary>		
        public decimal OrderToWorkId
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
        /// ReKey
        /// </summary>		
        public string ReKey
        {
            get;
            set;
        }
        /// <summary>
        /// ReKey2
        /// </summary>		
        public string ReKey2
        {
            get;
            set;
        }
        /// <summary>
        /// 订单日志类别
        /// </summary>		
        public int OrderLogClassId
        {
            get;
            set;
        }
        /// <summary>
        /// UserId
        /// </summary>		
        public string UserId
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

    }
}


