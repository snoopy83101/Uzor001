


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //ProClassVsPinPai

    public class ProClassVsPinPaiModel
    {

        public ProClassVsPinPaiModel()
        {

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
        /// PinPaiId
        /// </summary>		
        public decimal PinPaiId
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
        /// VsType
        /// </summary>		
        public string VsType
        {
            get;
            set;
        }

    }
}


