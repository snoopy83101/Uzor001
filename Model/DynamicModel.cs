


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //整站动态表

    public class DynamicModel
    {

        public DynamicModel()
        {

        }




        /// <summary>
        /// DynamicId
        /// </summary>		
        public decimal DynamicId
        {
            get;
            set;
        }
        /// <summary>
        /// DynamicTitle
        /// </summary>		
        public string DynamicTitle
        {
            get;
            set;
        }
        /// <summary>
        /// DynamicType
        /// </summary>		
        public string DynamicType
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间.
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// FlagInvalid
        /// </summary>		
        public bool FlagInvalid
        {
            get;
            set;
        }
        /// <summary>
        /// 动态属于哪个用户(如果是用户动态的话)
        /// </summary>		
        public string DynamicUserId
        {
            get;
            set;
        }
        /// <summary>
        /// 动态数据哪个商家(如果是商家动态的话)
        /// </summary>		
        public decimal DynamicMerId
        {
            get;
            set;
        }
        /// <summary>
        /// 动态级别,根据关注程度方便用户查询(默认100)
        /// </summary>		
        public int DynamicLv
        {
            get;
            set;
        }
        /// <summary>
        /// 标识前台如何解析这条动态
        /// </summary>		
        public string JsonType
        {
            get;
            set;
        }
        /// <summary>
        /// 转为JSON的XML字符串
        /// </summary>		
        public string JsonMemo
        {
            get;
            set;
        }

    }
}


