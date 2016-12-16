


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //预估产量

    public class OrderExpectModel
    {

        public OrderExpectModel()
        {

        }




        /// <summary>
        /// 第几天
        /// </summary>		
        public int OrderExpectDay
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
        /// 产量
        /// </summary>		
        public decimal Num
        {
            get;
            set;
        }

    }
}


