


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //PingJiaInfo

    public class PingJiaInfoModel
    {

        public PingJiaInfoModel()
        {

        }




        /// <summary>
        /// PingJiaId
        /// </summary>		
        public decimal PingJiaId
        {
            get;
            set;
        }
        /// <summary>
        /// PingJiaContent
        /// </summary>		
        public string PingJiaContent
        {
            get;
            set;
        }
        /// <summary>
        /// HuiPingContent
        /// </summary>		
        public string HuiPingContent
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
        /// HuiPingTime
        /// </summary>		
        public DateTime HuiPingTime
        {
            get;
            set;
        }
        /// <summary>
        /// HuiPingUser
        /// </summary>		
        public string HuiPingUser
        {
            get;
            set;
        }
        /// <summary>
        /// DingDanDetailId
        /// </summary>		
        public decimal DingDanDetailId
        {
            get;
            set;
        }
        /// <summary>
        /// PingJiaLv
        /// </summary>		
        public decimal PingJiaLv
        {
            get;
            set;
        }
        /// <summary>
        /// Invalid
        /// </summary>		
        public bool Invalid
        {
            get;
            set;
        }

    }
}


