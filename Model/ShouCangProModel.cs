


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //产品收藏

    public class ShouCangProModel
    {

        public ShouCangProModel()
        {

        }




        /// <summary>
        /// ShouCangId
        /// </summary>		
        public decimal ShouCangId
        {
            get;
            set;
        }
        /// <summary>
        /// CreateTime
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// MemberId
        /// </summary>		
        public decimal MemberId
        {
            get;
            set;
        }
        /// <summary>
        /// ProId
        /// </summary>		
        public string ProId
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


