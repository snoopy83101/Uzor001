


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //PeiSongType

    public class PeiSongTypeModel
    {

        public PeiSongTypeModel()
        {

        }




        /// <summary>
        /// PeiSongTypeId
        /// </summary>		
        public decimal PeiSongTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// PeiSongTypeName
        /// </summary>		
        public string PeiSongTypeName
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
        /// 默认价格
        /// </summary>		
        public decimal DefaultPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 满多少包邮
        /// </summary>		
        public decimal FullPrice
        {
            get;
            set;
        }
        /// <summary>
        /// PeiSongTypeMemo
        /// </summary>		
        public string PeiSongTypeMemo
        {
            get;
            set;
        }
        /// <summary>
        /// OrderNo
        /// </summary>		
        public int OrderNo
        {
            get;
            set;
        }
        /// <summary>
        /// 特性值(1限时急速配送,神速达),(2,定时达,此时SelDay有效),(3,一般配送,极速达)
        /// </summary>		
        public int PeiSongTeXing
        {
            get;
            set;
        }
        /// <summary>
        /// 可以选择的配送天数(定时达专用)
        /// </summary>		
        public int SelDay
        {
            get;
            set;
        }
        /// <summary>
        /// 需要配送的大约准备时间
        /// </summary>		
        public TimeSpan PeiSongTime
        {
            get;
            set;
        }
        /// <summary>
        /// 需要配货的准备时间
        /// </summary>		
        public TimeSpan PeiHuoTime
        {
            get;
            set;
        }
        /// <summary>
        /// 每天的结束时间,配送特性为30时,即时配送有效
        /// </summary>		
        public TimeSpan EndTimeForDay
        {
            get;
            set;
        }
        /// <summary>
        /// 每天的启用时间, 为30即时配送时有效
        /// </summary>		
        public TimeSpan BgTimeForDay
        {
            get;
            set;
        }
        /// <summary>
        /// BranchId
        /// </summary>		
        public string BranchId
        {
            get;
            set;
        }

    }
}


