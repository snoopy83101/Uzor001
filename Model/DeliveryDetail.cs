


using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model{
	 	//DeliveryDetail
 	
			public class DeliveryDetailModel
	{
	
	      public DeliveryDetailModel()
	    {
	     
	    }
	
	
	   
   		     
      	/// <summary>
		/// 出库单明细编号
        /// </summary>		
                        public string DeliveryDetailId
        {
            get;
            set;
        }        
		/// <summary>
		/// 所属出库单号
        /// </summary>		
                        public string DeliveryId
        {
            get;
            set;
        }        
		/// <summary>
		/// 所属库存编号
        /// </summary>		
                        public decimal StockId
        {
            get;
            set;
        }        
		/// <summary>
		/// 实际出库的零售价格
        /// </summary>		
                        public decimal ActualPrice
        {
            get;
            set;
        }        
		/// <summary>
		/// 出库明细备注
        /// </summary>		
                        public string DeliveryDetailMemo
        {
            get;
            set;
        }        
		   
	}
}


