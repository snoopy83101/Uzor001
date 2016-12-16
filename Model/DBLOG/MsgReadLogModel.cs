


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //MsgReadLog

    public class MsgReadLogModel
    {

        public MsgReadLogModel()
        {

        }




        /// <summary>
        /// DeviceId
        /// </summary>		
        public string DeviceId
        {
            get;
            set;
        }
        /// <summary>
        /// MsgTextId
        /// </summary>		
        public decimal MsgTextId
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

    }
}


