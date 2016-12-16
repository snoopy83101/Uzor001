


using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model{
	 	//NewClass
 	
			public class NewClassModel
	{
	
	      public NewClassModel()
	    {
	     
	    }
	
	
	   
   		     
      	/// <summary>
		/// 新闻类别编号
        /// </summary>		
                        public decimal NewClassId
        {
            get;
            set;
        }        
		/// <summary>
		/// 新闻类别名称
        /// </summary>		
                        public string NewClassName
        {
            get;
            set;
        }        
		/// <summary>
		/// 所属类别ID
        /// </summary>		
                        public decimal ParentId
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


