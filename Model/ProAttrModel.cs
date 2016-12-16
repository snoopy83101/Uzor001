


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //扩展属性表

    public class ProAttrModel
    {

        public ProAttrModel()
        {

        }




        /// <summary>
        /// ProAttrId
        /// </summary>		
        public decimal ProAttrId
        {
            get;
            set;
        }
        /// <summary>
        /// ProClassId
        /// </summary>		
        public decimal ProClassId
        {
            get;
            set;
        }
        /// <summary>
        /// ProAttrName
        /// </summary>		
        public string ProAttrName
        {
            get;
            set;
        }
        /// <summary>
        /// ProAttrMemo
        /// </summary>		
        public string ProAttrMemo
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
        /// <summary>
        /// AttrUnit
        /// </summary>		
        public string AttrUnit
        {
            get;
            set;
        }

    }
}


