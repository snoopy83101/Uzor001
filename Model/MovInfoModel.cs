


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //MovInfo

    public class MovInfoModel
    {

        public MovInfoModel()
        {

        }




        /// <summary>
        /// MovId
        /// </summary>		
        public string MovId
        {
            get;
            set;
        }
        /// <summary>
        /// MovTitle
        /// </summary>		
        public string MovTitle
        {
            get;
            set;
        }
        /// <summary>
        /// MovContent
        /// </summary>		
        public string MovContent
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
        /// MovType
        /// </summary>		
        public string MovType
        {
            get;
            set;
        }
        /// <summary>
        /// MovBiaoQian
        /// </summary>		
        public string MovBiaoQian
        {
            get;
            set;
        }
        /// <summary>
        /// MovImgUrl
        /// </summary>		
        public string MovImgUrl
        {
            get;
            set;
        }
        /// <summary>
        /// ShiGuangId
        /// </summary>		
        public string ShiGuangId
        {
            get;
            set;
        }
        /// <summary>
        /// ShangYingTime
        /// </summary>		
        public DateTime ShangYingTime
        {
            get;
            set;
        }
        /// <summary>
        /// 片长(分钟)
        /// </summary>		
        public int PianChang
        {
            get;
            set;
        }
        /// <summary>
        /// MovClass
        /// </summary>		
        public string MovClass
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


