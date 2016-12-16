


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //RongMsgLogInfo

    public class RongMsgLogInfoModel
    {

        public RongMsgLogInfoModel()
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
        /// 匿名设备,用户,客服
        /// </summary>		
        public string SendRole
        {
            get;
            set;
        }
        /// <summary>
        /// 匿名设备,用户,客服
        /// </summary>		
        public string ReRole
        {
            get;
            set;
        }
        /// <summary>
        /// 设备ID,有可能有
        /// </summary>		
        public string ReDeviceId
        {
            get;
            set;
        }
        /// <summary>
        /// 用户ID,有可能有
        /// </summary>		
        public decimal ReMemberId
        {
            get;
            set;
        }
        /// <summary>
        /// 后台用户ID,有可能有
        /// </summary>		
        public string ReUserId
        {
            get;
            set;
        }
        /// <summary>
        /// ContentText
        /// </summary>		
        public string ContentText
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
        /// 设备号
        /// </summary>		
        public string RongUserId
        {
            get;
            set;
        }
        /// <summary>
        /// 融云的消息ID
        /// </summary>		
        public string MessageId
        {
            get;
            set;
        }
        /// <summary>
        /// 消息类型, 默认为RC:TxtMsg 文本消息
        /// </summary>		
        public string RongMsgLogTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// ImgUrl
        /// </summary>		
        public string ImgUrl
        {
            get;
            set;
        }
        /// <summary>
        /// ''
        /// </summary>		
        public string Extra
        {
            get;
            set;
        }
        /// <summary>
        /// 图文消息时的title
        /// </summary>		
        public string Title
        {
            get;
            set;
        }

    }
}


