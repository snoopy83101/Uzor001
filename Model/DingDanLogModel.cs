


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //DingDanLog

    public class DingDanLogModel
    {

        public DingDanLogModel()
        {

        }




        /// <summary>
        /// DingDanLogId
        /// </summary>		
        public decimal DingDanLogId
        {
            get;
            set;
        }
        /// <summary>
        /// DingDanId
        /// </summary>		
        public string DingDanId
        {
            get;
            set;
        }
        /// <summary>
        /// DingDanTypeId
        /// </summary>		
        public int DingDanLogTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// DingDanClassId
        /// </summary>		
        public int DingDanClassId
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
        /// <summary>
        /// CreateTime
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }

    }
}


