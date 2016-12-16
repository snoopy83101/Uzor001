


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //IndexData

    public class IndexDataModel
    {

        public IndexDataModel()
        {

        }




        /// <summary>
        /// AutoId
        /// </summary>		
        public decimal AutoId
        {
            get;
            set;
        }
        /// <summary>
        /// 数据标题
        /// </summary>		
        public string ItemTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 数据简介
        /// </summary>		
        public string ItemContent
        {
            get;
            set;
        }
        /// <summary>
        /// 数据类型 首页商家, 首页产品
        /// </summary>		
        public string ItemType
        {
            get;
            set;
        }
        /// <summary>
        /// ItemClass
        /// </summary>		
        public string ItemClass
        {
            get;
            set;
        }
        /// <summary>
        /// 排序编号
        /// </summary>		
        public int OrderNo
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
        /// CreateUser
        /// </summary>		
        public string CreateUser
        {
            get;
            set;
        }
        /// <summary>
        /// ImgId
        /// </summary>		
        public string ImgId
        {
            get;
            set;
        }
        /// <summary>
        /// 相关主键
        /// </summary>		
        public string ReKey
        {
            get;
            set;
        }
        /// <summary>
        /// 点击网址
        /// </summary>		
        public string Url
        {
            get;
            set;
        }
        /// <summary>
        /// 其他属性
        /// </summary>		
        public string JsonMemo
        {
            get;
            set;
        }
        /// <summary>
        /// EventName
        /// </summary>		
        public string EventName
        {
            get;
            set;
        }

    }
}


