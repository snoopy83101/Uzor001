


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //ErroLogInfo

    public class ErroLogInfoModel
    {

        public ErroLogInfoModel()
        {

        }




        /// <summary>
        /// ErroLogId
        /// </summary>		
        public decimal ErroLogId
        {
            get;
            set;
        }
        /// <summary>
        /// ErroLogContent
        /// </summary>		
        public string ErroLogContent
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
        /// MemberId
        /// </summary>		
        public decimal MemberId
        {
            get;
            set;
        }
        /// <summary>
        /// UserId
        /// </summary>		
        public string UserId
        {
            get;
            set;
        }
        /// <summary>
        /// Message
        /// </summary>		
        public string Message
        {
            get;
            set;
        }

    }
}


