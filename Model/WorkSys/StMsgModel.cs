


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //短信息表

    public class StMsgModel
    {

        public StMsgModel()
        {

        }




        /// <summary>
        /// StMsgId
        /// </summary>		
        public decimal StMsgId
        {
            get;
            set;
        }
        /// <summary>
        /// StMsgCode
        /// </summary>		
        public string StMsgCode
        {
            get;
            set;
        }
        /// <summary>
        /// StMsgTypeId
        /// </summary>		
        public decimal StMsgTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// StMsgClassId
        /// </summary>		
        public decimal StMsgClassId
        {
            get;
            set;
        }
        /// <summary>
        /// StMsgContent
        /// </summary>		
        public string StMsgContent
        {
            get;
            set;
        }
        /// <summary>
        /// PhoneNo
        /// </summary>		
        public string PhoneNo
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
        /// ReKey
        /// </summary>		
        public string ReKey
        {
            get;
            set;
        }
        /// <summary>
        /// MerId
        /// </summary>		
        public decimal MerId
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
        /// <summary>
        /// Ip
        /// </summary>		
        public string Ip
        {
            get;
            set;
        }

    }
}


