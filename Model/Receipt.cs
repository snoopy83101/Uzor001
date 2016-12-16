


using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model{
	 	//入库单主表
 	
			public class ReceiptModel
	{
	
	      public ReceiptModel()
	    {
	     
	    }
	
	
	   
   		     
      	/// <summary>
		/// 入库单编号
        /// </summary>		
                        public string ReceiptId
        {
            get;
            set;
        }        
		/// <summary>
		/// 入库单备注
        /// </summary>		
                        public string ReceiptMemo
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
		/// 入库类别
        /// </summary>		
                        public decimal ReceiptType
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


