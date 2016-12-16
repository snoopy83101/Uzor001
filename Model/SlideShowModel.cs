


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //SlideShow

    public class SlideShowModel
    {

        public SlideShowModel()
        {

        }




        /// <summary>
        /// SlideshowId
        /// </summary>		
        public decimal SlideshowId
        {
            get;
            set;
        }
        /// <summary>
        /// SlideshowTitle
        /// </summary>		
        public string SlideshowTitle
        {
            get;
            set;
        }
        /// <summary>
        /// SlideshowMemo
        /// </summary>		
        public string SlideshowMemo
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
        /// OrderNo
        /// </summary>		
        public int OrderNo
        {
            get;
            set;
        }
        /// <summary>
        /// lv
        /// </summary>		
        public int lv
        {
            get;
            set;
        }
        /// <summary>
        /// SlideshowType
        /// </summary>		
        public string SlideshowType
        {
            get;
            set;
        }
        /// <summary>
        /// SlideshowImgId
        /// </summary>		
        public string SlideshowImgId
        {
            get;
            set;
        }
        /// <summary>
        /// 幻灯片/广告地址
        /// </summary>		
        public string Url
        {
            get;
            set;
        }
        /// <summary>
        /// 单击事件如果留空则无效
        /// </summary>		
        public string Event
        {
            get;
            set;
        }

    }
}


