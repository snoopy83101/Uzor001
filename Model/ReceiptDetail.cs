


using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model{
	 	//入库单明细表
 	
			public class ReceiptDetailModel
	{
	
	      public ReceiptDetailModel()
	    {
	     
	    }
	
	
	   
   		     
      	/// <summary>
		/// 入库单明细编号
        /// </summary>		
                        public string ReceiptDetailId
        {
            get;
            set;
        }        
		/// <summary>
		/// 所属入库单编号
        /// </summary>		
                        public string ReceiptId
        {
            get;
            set;
        }        
		/// <summary>
		/// 数量
        /// </summary>		
                        public decimal Quatity
        {
            get;
            set;
        }        
		/// <summary>
		/// 备注
        /// </summary>		
                        public string ReceiptMemo
        {
            get;
            set;
        }        
		/// <summary>
		/// 所影响的库存编号(不论记账与否,肯定会关联一个库存,如果不几张也创建为0的库存)
        /// </summary>		
                        public decimal StockId
        {
            get;
            set;
        }        
		   
	}
}


