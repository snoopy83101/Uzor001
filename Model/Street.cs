


using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model{
	 	//Street
 	
			public class StreetModel
	{
	
	      public StreetModel()
	    {
	     
	    }
	
	
	   
   		     
      	/// <summary>
		/// StreetId
        /// </summary>		
                        public decimal StreetId
        {
            get;
            set;
        }        
		/// <summary>
		/// 街道名称
        /// </summary>		
                        public string StreetName
        {
            get;
            set;
        }        
		/// <summary>
		/// 街道简介(标题)
        /// </summary>		
                        public string StreetTitle
        {
            get;
            set;
        }        
		/// <summary>
		/// 街道简介
        /// </summary>		
                        public string StreetContent
        {
            get;
            set;
        }        
		/// <summary>
		/// 排序编号
        /// </summary>		
                        public int OrderId
        {
            get;
            set;
        }        
		/// <summary>
		/// 所属的乡镇ID
        /// </summary>		
                        public decimal TownId
        {
            get;
            set;
        }        
		   
	}
}


