


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //OrderToWorkDoneLog

    public class OrderToWorkDoneLogModel
    {

        public OrderToWorkDoneLogModel()
        {

        }




        /// <summary>
        /// OrderToWorkDoneLogId
        /// </summary>		
        public decimal OrderToWorkDoneLogId
        {
            get;
            set;
        }
        /// <summary>
        /// OrderToWorkDetailId
        /// </summary>		
        public decimal OrderToWorkDetailId
        {
            get;
            set;
        }
        /// <summary>
        /// ClothesSizeId
        /// </summary>		
        public int ClothesSizeId
        {
            get;
            set;
        }
        /// <summary>
        /// ChangeDoneNum
        /// </summary>		
        public decimal ChangeDoneNum
        {
            get;
            set;
        }
        /// <summary>
        /// 变更之前是多少
        /// </summary>		
        public decimal OldDoneNum
        {
            get;
            set;
        }
        /// <summary>
        /// CreatTime
        /// </summary>		
        public DateTime CreatTime
        {
            get;
            set;
        }
        /// <summary>
        /// 10是用户提交,20是后台人员更改
        /// </summary>		
        public int DoneLogTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 预留关联字段
        /// </summary>		
        public string ReKey
        {
            get;
            set;
        }

    }
}


