


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //ThInfo

    public class ThInfoModel
    {

        public ThInfoModel()
        {

        }




        /// <summary>
        /// ThId
        /// </summary>		
        public string ThId
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
        /// ThTitle
        /// </summary>		
        public string ThTitle
        {
            get;
            set;
        }
        /// <summary>
        /// ThJiFen
        /// </summary>		
        public decimal ThJiFen
        {
            get;
            set;
        }
        /// <summary>
        /// ThAmount
        /// </summary>		
        public decimal ThAmount
        {
            get;
            set;
        }
        /// <summary>
        /// ThTypeId
        /// </summary>		
        public int ThTypeId
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
        /// ThCreateTime
        /// </summary>		
        public DateTime ThCreateTime
        {
            get;
            set;
        }

    }
}


