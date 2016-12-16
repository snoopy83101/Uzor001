


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Site

    public class SiteModel
    {

        public SiteModel()
        {

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
        /// SiteName
        /// </summary>		
        public string SiteName
        {
            get;
            set;
        }
        /// <summary>
        /// SiteMemo
        /// </summary>		
        public string SiteMemo
        {
            get;
            set;
        }
        /// <summary>
        /// SiteLng
        /// </summary>		
        public decimal SiteLng
        {
            get;
            set;
        }
        /// <summary>
        /// SiteLat
        /// </summary>		
        public decimal SiteLat
        {
            get;
            set;
        }
        /// <summary>
        /// ParentSiteId
        /// </summary>		
        public decimal ParentSiteId
        {
            get;
            set;
        }
        /// <summary>
        /// Unit
        /// </summary>		
        public string Unit
        {
            get;
            set;
        }
        /// <summary>
        /// select或者text, 选择还是文本
        /// </summary>		
        public string SiteType
        {
            get;
            set;
        }
        /// <summary>
        /// ZoneId
        /// </summary>		
        public decimal ZoneId
        {
            get;
            set;
        }

    }
}


