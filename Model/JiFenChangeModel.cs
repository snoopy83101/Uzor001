


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //JiFenChange

    public class JiFenChangeModel
    {

        public JiFenChangeModel()
        {

        }




        /// <summary>
        /// JifenChangeId
        /// </summary>		
        public decimal JifenChangeId
        {
            get;
            set;
        }
        /// <summary>
        /// 1借方,2贷方
        /// </summary>		
        public int JifenChangeTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 1购买产品增加(减少)
        /// </summary>		
        public int JiFenChangeClassId
        {
            get;
            set;
        }
        /// <summary>
        /// JiFenChangeMemo
        /// </summary>		
        public string JiFenChangeMemo
        {
            get;
            set;
        }
        /// <summary>
        /// JiFenChangeNum
        /// </summary>		
        public decimal JiFenChangeNum
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
        /// CreateTime
        /// </summary>		
        public DateTime CreateTime
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

    }
}


