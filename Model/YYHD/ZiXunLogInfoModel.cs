


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //ZiXunLogInfo

    public class ZiXunLogInfoModel
    {

        public ZiXunLogInfoModel()
        {

        }




        /// <summary>
        /// ZiXunLogId
        /// </summary>		
        public decimal ZiXunLogId
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
        /// ZiXunLogTypeId
        /// </summary>		
        public int ZiXunLogTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// ZiXunLogReKey
        /// </summary>		
        public string ZiXunLogReKey
        {
            get;
            set;
        }
        /// <summary>
        /// ZiXunLogJson
        /// </summary>		
        public string ZiXunLogJson
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
        /// Memo
        /// </summary>		
        public string Memo
        {
            get;
            set;
        }

    }
}


