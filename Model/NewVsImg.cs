


using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model{
	 	//NewVsImg
 	
			public class NewVsImgModel
	{
	
	      public NewVsImgModel()
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
		/// 图片ID
        /// </summary>		
                        public string ImgId
        {
            get;
            set;
        }        
		/// <summary>
		/// 新闻ID
        /// </summary>		
                        public decimal NewId
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
		   
	}
}


