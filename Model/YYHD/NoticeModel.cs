


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Notice

    public class NoticeModel
    {

        public NoticeModel()
        {

        }




        /// <summary>
        /// NoticeId
        /// </summary>		
        public decimal NoticeId
        {
            get;
            set;
        }
        /// <summary>
        /// NoticeTitle
        /// </summary>		
        public string NoticeTitle
        {
            get;
            set;
        }
        /// <summary>
        /// NoticeContent
        /// </summary>		
        public string NoticeContent
        {
            get;
            set;
        }
        /// <summary>
        /// NoticeType
        /// </summary>		
        public string NoticeType
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
        /// RongUserId
        /// </summary>		
        public string RongUserId
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

    }
}


