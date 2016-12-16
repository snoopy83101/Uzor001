


using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model{
	 	//MerchantType
 	
			public class MerchantTypeModel
	{
	
	      public MerchantTypeModel()
	    {
	     
	    }
	
	
	   
   		     
      	/// <summary>
		/// 商家类型编号
        /// </summary>		
                        public decimal MerchantTypeId
        {
            get;
            set;
        }        
		/// <summary>
		/// 商家类型名称
        /// </summary>		
                        public string MerchantTypeName
        {
            get;
            set;
        }        
		/// <summary>
		/// 商家类型简介
        /// </summary>		
                        public string MerchantTypeMemo
        {
            get;
            set;
        }        
		/// <summary>
		/// 图标文件名(图标在根目录下的images文件夹下)
        /// </summary>		
                        public string MapPng
        {
            get;
            set;
        }        
		/// <summary>
		/// 排序序号
        /// </summary>		
                        public int OrderNo
        {
            get;
            set;
        }        
		   
	}
}


