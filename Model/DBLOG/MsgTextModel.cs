


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //MsgText

    public class MsgTextModel
    {

        public MsgTextModel()
        {

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
        /// sys是系统通知
        /// </summary>		
        public string MsgTitle
        {
            get;
            set;
        }
        /// <summary>
        /// MsgContent
        /// </summary>		
        public string MsgContent
        {
            get;
            set;
        }
        /// <summary>
        /// MsgType
        /// </summary>		
        public string MsgType
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
        /// EndTime
        /// </summary>		
        public DateTime EndTime
        {
            get;
            set;
        }
        /// <summary>
        /// Extra
        /// </summary>		
        public string Extra
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
        /// ''
        /// </summary>		
        public string ZoneId
        {
            get;
            set;
        }
        /// <summary>
        /// 默认10为用户的交易信息
        /// </summary>		
        public int MsgClassId
        {
            get;
            set;
        }

    }
}


