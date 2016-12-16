


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //WxKey

    public class WxKeyModel
    {

        public WxKeyModel()
        {

        }




        /// <summary>
        /// KeyId
        /// </summary>		
        public decimal KeyId
        {
            get;
            set;
        }
        /// <summary>
        /// WxSendId
        /// </summary>		
        public decimal WxSendId
        {
            get;
            set;
        }
        /// <summary>
        /// KeyTitle
        /// </summary>		
        public string KeyTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 微信的激发时间名
        /// </summary>		
        public string KeyTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 有时候微信也需要事件明细(EventKey), 例如在关注时
        /// </summary>		
        public string KeyTypeDetailId
        {
            get;
            set;
        }

    }
}


