


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //TargetIds

    public class TargetIdsModel
    {

        public TargetIdsModel()
        {

        }




        /// <summary>
        /// RongMsgLogId
        /// </summary>		
        public decimal RongMsgLogId
        {
            get;
            set;
        }
        /// <summary>
        /// 要发送给的用户
        /// </summary>		
        public string TargetId
        {
            get;
            set;
        }
        /// <summary>
        /// 0未读消息,1已读消息
        /// </summary>		
        public int MsgStatus
        {
            get;
            set;
        }

    }
}


