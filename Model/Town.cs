


using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model{
	 	//Town
 	
			public class TownModel
	{
	
	      public TownModel()
	    {
	     
	    }
	
	
	   
   		     
      	/// <summary>
		/// 乡镇名称
        /// </summary>		
                        public decimal TownId
        {
            get;
            set;
        }        
		/// <summary>
		/// TownName
        /// </summary>		
                        public string TownName
        {
            get;
            set;
        }        
		/// <summary>
		/// 乡镇标题(副标题  xxx之乡)
        /// </summary>		
                        public string TownTitle
        {
            get;
            set;
        }        
		/// <summary>
		/// 乡镇备注
        /// </summary>		
                        public string TownMemo
        {
            get;
            set;
        }        
		/// <summary>
		/// 排序编号
        /// </summary>		
                        public decimal OrderNo
        {
            get;
            set;
        }        
		   
	}
}


