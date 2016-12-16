


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //OrderDetail

    public class OrderDetailModel
    {

        public OrderDetailModel()
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
        /// OrderId
        /// </summary>		
        public string OrderId
        {
            get;
            set;
        }
        /// <summary>
        /// Color
        /// </summary>		
        public string Color
        {
            get;
            set;
        }
        /// <summary>
        /// Invalid
        /// </summary>		
        public bool Invalid
        {
            get;
            set;
        }

    }
}


