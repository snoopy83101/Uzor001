


using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model{
	 	//Delivery
 	
			public class DeliveryModel
	{
	
	      public DeliveryModel()
	    {
	     
	    }
	
	
	   
   		     
      	/// <summary>
		/// 出库单编号
        /// </summary>		
                        public string DeliveryId
        {
            get;
            set;
        }        
		/// <summary>
		/// 出库单内容
        /// </summary>		
                        public string DeliveryMemo
        {
            get;
            set;
        }        
		/// <summary>
		/// 出库单创建时间
        /// </summary>		
                        public DateTime CreateTime
        {
            get;
            set;
        }        
		/// <summary>
		/// 出库类别
        /// </summary>		
                        public decimal DeliveryType
        {
            get;
            set;
        }        
		/// <summary>
		/// 制单人
        /// </summary>		
                        public string CreateUser
        {
            get;
            set;
        }        
		/// <summary>
		/// 出库单状态
        /// </summary>		
                        public string Status
        {
            get;
            set;
        }        
		   
	}
}


