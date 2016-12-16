


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //BranchVsSite

    public class BranchVsSiteModel
    {

        public BranchVsSiteModel()
        {

        }




        /// <summary>
        /// BranchId
        /// </summary>		
        public string BranchId
        {
            get;
            set;
        }
        /// <summary>
        /// SiteId
        /// </summary>		
        public decimal SiteId
        {
            get;
            set;
        }
        /// <summary>
        /// VsOrder
        /// </summary>		
        public int VsOrder
        {
            get;
            set;
        }

    }
}


