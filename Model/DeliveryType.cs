


using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model{
	 	//DeliveryType
 	
			public class DeliveryTypeModel
	{
	
	      public DeliveryTypeModel()
	    {
	     
	    }
	
	
	   
   		     
      	/// <summary>
		/// DeliveryTypeId
        /// </summary>		
                        public decimal DeliveryTypeId
        {
            get;
            set;
        }        
		/// <summary>
		/// DeliveryTypeName
        /// </summary>		
                        public string DeliveryTypeName
        {
            get;
            set;
        }        
		/// <summary>
		/// Memo
        /// </summary>		
                        public string Memo
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
		   
	}
}


