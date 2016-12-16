


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //SiteDetail

    public class SiteDetailModel
    {

        public SiteDetailModel()
        {

        }




        /// <summary>
        /// SiteDetailId
        /// </summary>		
        public decimal SiteDetailId
        {
            get;
            set;
        }
        /// <summary>
        /// SiteDetailName
        /// </summary>		
        public string SiteDetailName
        {
            get;
            set;
        }
        /// <summary>
        /// 数值越大越靠上
        /// </summary>		
        public int OrderNo
        {
            get;
            set;
        }
        /// <summary>
        /// SiteDetaiMemo
        /// </summary>		
        public string SiteDetaiMemo
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
        /// select或者text, 选择还是文本
        /// </summary>		
        public string SiteDetailType
        {
            get;
            set;
        }
        /// <summary>
        /// Extra
        /// </summary>		
        public string Extra
        {
            get;
            set;
        }

    }
}


