


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //NoticeTarget

    public class NoticeTargetModel
    {

        public NoticeTargetModel()
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
        /// TargetId
        /// </summary>		
        public string TargetId
        {
            get;
            set;
        }
        /// <summary>
        /// 0未发送,10已发送,20已读
        /// </summary>		
        public int NoticeStatus
        {
            get;
            set;
        }

    }
}


