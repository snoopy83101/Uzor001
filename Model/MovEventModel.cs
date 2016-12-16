


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //MovEvent

    public class MovEventModel
    {

        public MovEventModel()
        {

        }




        /// <summary>
        /// 主键
        /// </summary>		
        public string MovEventId
        {
            get;
            set;
        }
        /// <summary>
        /// 关联电影
        /// </summary>		
        public string MovId
        {
            get;
            set;
        }
        /// <summary>
        /// 场次票价
        /// </summary>		
        public decimal MovEventRePrice
        {
            get;
            set;
        }
        /// <summary>
        /// 场次介绍
        /// </summary>		
        public string MovEventMemo
        {
            get;
            set;
        }
        /// <summary>
        /// 开场时间
        /// </summary>		
        public DateTime MovEventBgTime
        {
            get;
            set;
        }

    }
}


