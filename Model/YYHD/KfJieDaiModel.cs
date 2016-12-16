


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //KfJieDai

    public class KfJieDaiModel
    {

        public KfJieDaiModel()
        {

        }




        /// <summary>
        /// KfJieDaiId
        /// </summary>		
        public decimal KfJieDaiId
        {
            get;
            set;
        }
        /// <summary>
        /// 设备号
        /// </summary>		
        public string DeviceId
        {
            get;
            set;
        }
        /// <summary>
        /// 客服编号
        /// </summary>		
        public string UserId
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 最后更新时间
        /// </summary>		
        public DateTime LastTime
        {
            get;
            set;
        }
        /// <summary>
        /// 接待状态1为正在接待,10为催促, 100为完结,以后为历史接待
        /// </summary>		
        public int JieDaiStatus
        {
            get;
            set;
        }

    }
}


