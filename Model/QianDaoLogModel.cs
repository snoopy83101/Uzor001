


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //QianDaoLog

    public class QianDaoLogModel
    {

        public QianDaoLogModel()
        {

        }




        /// <summary>
        /// QianDaoLogId
        /// </summary>		
        public decimal QianDaoLogId
        {
            get;
            set;
        }
        /// <summary>
        /// QianDaoMemberId
        /// </summary>		
        public decimal QianDaoMemberId
        {
            get;
            set;
        }
        /// <summary>
        /// QianDaoMemo
        /// </summary>		
        public string QianDaoMemo
        {
            get;
            set;
        }
        /// <summary>
        /// 连续签到的天数
        /// </summary>		
        public int DayNum
        {
            get;
            set;
        }
        /// <summary>
        /// 积分变更的记录主键
        /// </summary>		
        public decimal JiFenChangeId
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


