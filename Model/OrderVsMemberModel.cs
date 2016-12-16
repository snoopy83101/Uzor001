


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //OrderVsMember

    public class OrderVsMemberModel
    {

        public OrderVsMemberModel()
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
        /// 状态10等待派单 20 已经派单,   -1为自动放弃, -10 被淘汰
        /// </summary>		
        public int VsStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 是否作废
        /// </summary>		
        public bool Invalid
        {
            get;
            set;
        }
        /// <summary>
        /// 10用户抢单,20系统指派
        /// </summary>		
        public int VsType
        {
            get;
            set;
        }
        /// <summary>
        /// 被淘汰原因
        /// </summary>		
        public string Memo
        {
            get;
            set;
        }
        /// <summary>
        /// 登记人数, 如果是团队负责人登记, 则为团队人数, 否则为1
        /// </summary>		
        public int VsPlaces
        {
            get;
            set;
        }

    }
}


