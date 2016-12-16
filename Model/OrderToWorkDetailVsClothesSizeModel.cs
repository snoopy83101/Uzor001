


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //OrderToWorkDetailVsClothesSize

    public class OrderToWorkDetailVsClothesSizeModel
    {

        public OrderToWorkDetailVsClothesSizeModel()
        {

        }




        /// <summary>
        /// OrderToWorkDetailId
        /// </summary>		
        public decimal OrderToWorkDetailId
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
        /// <summary>
        /// CheckNum
        /// </summary>		
        public decimal CheckNum
        {
            get;
            set;
        }
        /// <summary>
        /// DoneNum
        /// </summary>		
        public decimal DoneNum
        {
            get;
            set;
        }
        /// <summary>
        /// ChangeTime
        /// </summary>		
        public DateTime ChangeTime
        {
            get;
            set;
        }

    }
}


