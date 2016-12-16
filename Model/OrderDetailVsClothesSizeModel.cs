


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //OrderDetailVsClothesSize

    public class OrderDetailVsClothesSizeModel
    {

        public OrderDetailVsClothesSizeModel()
        {

        }




        /// <summary>
        /// OrderDetailId
        /// </summary>		
        public decimal OrderDetailId
        {
            get;
            set;
        }
        /// <summary>
        /// ClothesSizeId
        /// </summary>		
        public int ClothesSizeId
        {
            get;
            set;
        }
        /// <summary>
        /// Num
        /// </summary>		
        public decimal Num
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

    }
}


