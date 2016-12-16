


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Recommend

    public class RecommendModel
    {

        public RecommendModel()
        {

        }




        /// <summary>
        /// RecommendId
        /// </summary>		
        public string RecommendId
        {
            get;
            set;
        }
        /// <summary>
        /// RecommendMemo
        /// </summary>		
        public string RecommendMemo
        {
            get;
            set;
        }
        /// <summary>
        /// ReKey
        /// </summary>		
        public string ReKey
        {
            get;
            set;
        }
        /// <summary>
        /// RecommendType
        /// </summary>		
        public string RecommendType
        {
            get;
            set;
        }
        /// <summary>
        /// BgTime
        /// </summary>		
        public DateTime BgTime
        {
            get;
            set;
        }
        /// <summary>
        /// EndTime
        /// </summary>		
        public DateTime EndTime
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


