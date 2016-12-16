


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //MerRoleVsDingDan

    public class MerRoleVsDingDanModel
    {

        public MerRoleVsDingDanModel()
        {

        }




        /// <summary>
        /// MerRoleId
        /// </summary>		
        public decimal MerRoleId
        {
            get;
            set;
        }
        /// <summary>
        /// DingDanId
        /// </summary>		
        public string DingDanId
        {
            get;
            set;
        }
        /// <summary>
        /// VsType
        /// </summary>		
        public string VsType
        {
            get;
            set;
        }
        /// <summary>
        /// VsTime
        /// </summary>		
        public DateTime VsTime
        {
            get;
            set;
        }

    }
}


