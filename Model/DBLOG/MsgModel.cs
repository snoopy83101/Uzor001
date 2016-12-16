


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Msg

    public class MsgModel
    {

        public MsgModel()
        {

        }




        /// <summary>
        /// MsgId
        /// </summary>		
        public decimal MsgId
        {
            get;
            set;
        }
        /// <summary>
        /// SendDeviceId
        /// </summary>		
        public string SendDeviceId
        {
            get;
            set;
        }
        /// <summary>
        /// TargetDeviceId
        /// </summary>		
        public string TargetDeviceId
        {
            get;
            set;
        }
        /// <summary>
        /// MsgTextId
        /// </summary>		
        public decimal MsgTextId
        {
            get;
            set;
        }
        /// <summary>
        /// ZoneId
        /// </summary>		
        public string ZoneId
        {
            get;
            set;
        }
        /// <summary>
        /// SiteId
        /// </summary>		
        public decimal SiteId
        {
            get;
            set;
        }
        /// <summary>
        /// 10未读, 20已读
        /// </summary>		
        public int MsgStatusId
        {
            get;
            set;
        }

    }
}


