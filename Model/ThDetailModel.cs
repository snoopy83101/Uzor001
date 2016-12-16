


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //ThDetail

    public class ThDetailModel
    {

        public ThDetailModel()
        {

        }




        /// <summary>
        /// ThDetailId
        /// </summary>		
        public decimal ThDetailId
        {
            get;
            set;
        }
        /// <summary>
        /// 退货单ID
        /// </summary>		
        public string ThId
        {
            get;
            set;
        }
        /// <summary>
        /// 属于退货单的哪个明细的明细ID
        /// </summary>		
        public decimal DingDanDetailId
        {
            get;
            set;
        }
        /// <summary>
        /// ThDetailTypeId
        /// </summary>		
        public int ThDetailTypeId
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
        /// ThDetailAttr
        /// </summary>		
        public string ThDetailAttr
        {
            get;
            set;
        }
        /// <summary>
        /// ThQuantity
        /// </summary>		
        public decimal ThQuantity
        {
            get;
            set;
        }

    }
}


