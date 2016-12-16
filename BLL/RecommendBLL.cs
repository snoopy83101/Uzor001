using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RecommendBLL
    {


        public void doRecommend(Model.RecommendModel model)
        {

            DAL.RecommendDAL dal = new DAL.RecommendDAL();
            if (model.RecommendId == "")
            {




                dal.DeleteList(" ReKey='" + model.ReKey + "'  and  RecommendType='" + model.RecommendType + "' ");

                //已经存在了,就删除之前的, 按照此次为准.



                model.RecommendId = Common.TimeString.GetNow_ff();

                dal.Add(model);
            }
            else
            {

                dal.Update(model);
            }



        }

    }
}
