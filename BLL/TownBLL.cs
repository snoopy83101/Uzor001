using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace BLL
{
    public class TownBLL
    {


        DAL.TownDAL dal = new DAL.TownDAL();


        /// <summary>
        /// 获得乡镇列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTownList()
        {

            return dal.GetList(" 1=1 Order by OrderNo ").Tables[0];
          
             
        }



    }
}
