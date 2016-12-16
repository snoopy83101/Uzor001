


using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model{
	 	//NewInfo
 	
			public class NewInfoModel
	{
	
	      public NewInfoModel()
	    {
	     
	    }
	
	
	   
   		     
      	/// <summary>
		/// 新闻编号
        /// </summary>		
                        public decimal NewId
        {
            get;
            set;
        }        
		/// <summary>
		/// 新闻简介
        /// </summary>		
                        public string NewSummary
        {
            get;
            set;
        }        
		/// <summary>
		/// 新闻标题
        /// </summary>		
                        public string NewTitle
        {
            get;
            set;
        }        
		/// <summary>
		/// 新闻内容
        /// </summary>		
                        public string NewContent
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
		/// 创建人
        /// </summary>		
                        public decimal CreateUser
        {
            get;
            set;
        }        
		/// <summary>
		/// 来源
        /// </summary>		
                        public string NewSourse
        {
            get;
            set;
        }        
		/// <summary>
		/// 热度
        /// </summary>		
                        public int HotLv
        {
            get;
            set;
        }        
		/// <summary>
		/// 推荐指数
        /// </summary>		
                        public int RecommendLv
        {
            get;
            set;
        }        
		/// <summary>
		/// 点击数
        /// </summary>		
                        public int ClickLv
        {
            get;
            set;
        }        
		/// <summary>
		/// 新闻类别
        /// </summary>		
                        public decimal NewClassId
        {
            get;
            set;
        }        
		/// <summary>
		/// 是否显示
        /// </summary>		
                        public bool FlagInvalid
        {
            get;
            set;
        }        
		   
	}
}


