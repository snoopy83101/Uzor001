


using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model{
	 	//GroupBuy
 	
			public class GroupBuyModel
	{
	
	      public GroupBuyModel()
	    {
	     
	    }
	
	
	   
   		     
      	/// <summary>
		/// GroupBuyId
        /// </summary>		
                        public string GroupBuyId
        {
            get;
            set;
        }        
		/// <summary>
		/// GroupBuyName
        /// </summary>		
                        public string GroupBuyName
        {
            get;
            set;
        }        
		/// <summary>
		/// TotalPrice
        /// </summary>		
                        public decimal TotalPrice
        {
            get;
            set;
        }        
		/// <summary>
		/// BeginTime
        /// </summary>		
                        public DateTime BeginTime
        {
            get;
            set;
        }        
		/// <summary>
		/// EndTime
        /// </summary>		
                        public DateTime EndTime
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
		/// GroupBuyTitle
        /// </summary>		
                        public string GroupBuyTitle
        {
            get;
            set;
        }        
		/// <summary>
		/// GroupBuyMemo
        /// </summary>		
                        public string GroupBuyMemo
        {
            get;
            set;
        }        
		/// <summary>
		/// 所关联的商品库存ID
        /// </summary>		
                        public decimal StockId
        {
            get;
            set;
        }        
		   
	}
}


