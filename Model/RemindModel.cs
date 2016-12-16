


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Remind

    public class RemindModel
    {

        public RemindModel()
        {

        }




        /// <summary>
        /// RemindId
        /// </summary>		
        public string RemindId
        {
            get;
            set;
        }
        /// <summary>
        /// RemindTitle
        /// </summary>		
        public string RemindTitle
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
        /// RemindTypeId
        /// </summary>		
        public string RemindTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// Url
        /// </summary>		
        public string Url
        {
            get;
            set;
        }
        /// <summary>
        /// UserLook
        /// </summary>		
        public bool UserLook
        {
            get;
            set;
        }
        /// <summary>
        /// MerLook
        /// </summary>		
        public bool MerLook
        {
            get;
            set;
        }
        /// <summary>
        /// 被提醒的用户
        /// </summary>		
        public string ReUserId
        {
            get;
            set;
        }
        /// <summary>
        /// ReMerchantId
        /// </summary>		
        public decimal ReMerchantId
        {
            get;
            set;
        }
        /// <summary>
        /// 相关主键
        /// </summary>		
        public string ReKey
        {
            get;
            set;
        }

    }
}


