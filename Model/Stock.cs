


using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model{
	 	//Stock
 	
			public class StockModel
	{
	
	      public StockModel()
	    {
	     
	    }
	
	
	   
   		     
      	/// <summary>
		/// StockId
        /// </summary>		
                        public decimal StockId
        {
            get;
            set;
        }        
		/// <summary>
		/// 产品字典编号
        /// </summary>		
                        public decimal ProductId
        {
            get;
            set;
        }        
		/// <summary>
		/// Quatity
        /// </summary>		
                        public decimal Quatity
        {
            get;
            set;
        }        
		/// <summary>
		/// 哪个商家的库存
        /// </summary>		
                        public decimal MerchantId
        {
            get;
            set;
        }        
		/// <summary>
		/// 进货价
        /// </summary>		
                        public decimal PurchasePrice
        {
            get;
            set;
        }        
		/// <summary>
		/// 零售价
        /// </summary>		
                        public decimal RetailPrice
        {
            get;
            set;
        }        
		/// <summary>
		/// 团购价
        /// </summary>		
                        public decimal GroupBuyPrice
        {
            get;
            set;
        }        
		/// <summary>
		/// 批发价
        /// </summary>		
                        public decimal WholesalePrice
        {
            get;
            set;
        }        
		   
	}
}


