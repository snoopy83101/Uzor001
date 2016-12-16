


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //PeiSongTimeSolt

    public class PeiSongTimeSoltModel
    {

        public PeiSongTimeSoltModel()
        {

        }




        /// <summary>
        /// PeiSongTimeSoltId
        /// </summary>		
        public decimal PeiSongTimeSoltId
        {
            get;
            set;
        }
        /// <summary>
        /// PeiSongTypeId
        /// </summary>		
        public decimal PeiSongTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// BgTime
        /// </summary>		
        public TimeSpan BgTime
        {
            get;
            set;
        }
        /// <summary>
        /// EndTime
        /// </summary>		
        public TimeSpan EndTime
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
        /// OrderNo
        /// </summary>		
        public int OrderNo
        {
            get;
            set;
        }

    }
}


