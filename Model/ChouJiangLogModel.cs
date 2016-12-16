


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //抽奖记录表

    public class ChouJiangLogModel
    {

        public ChouJiangLogModel()
        {

        }




        /// <summary>
        /// ChouJiangLogId
        /// </summary>		
        public decimal ChouJiangLogId
        {
            get;
            set;
        }
        /// <summary>
        /// ChouJiangId
        /// </summary>		
        public decimal ChouJiangId
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
        /// JiangPin
        /// </summary>		
        public decimal JiangPin
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

    }
}


