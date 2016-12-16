


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Team

    public class TeamModel
    {

        public TeamModel()
        {

        }




        /// <summary>
        /// TeamId
        /// </summary>		
        public decimal TeamId
        {
            get;
            set;
        }
        /// <summary>
        /// 团队名称
        /// </summary>		
        public string TeamName
        {
            get;
            set;
        }
        /// <summary>
        /// 团队名额
        /// </summary>		
        public int TeamPlaces
        {
            get;
            set;
        }

    }
}


