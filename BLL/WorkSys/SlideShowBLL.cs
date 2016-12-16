using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;
using System.Text;
using System.Data;
namespace BLL.WorkSys
{
    public class SlideShowBLL
    {

        /// <summary>
        /// 保存一个幻灯片
        /// </summary>
        /// <param name="model"></param>
        public void SaveSlideShowInfo(Model.SlideShowModel model)
        {
            DAL.SlideShowDAL dal = new DAL.SlideShowDAL();
            if (model.SlideshowId > 0)
            {
                //修改
                dal.Update(model);
            }
            else
            { //新增
                dal.Add(model);
            }
        }



        /// <summary>
        /// 查询一组幻灯片分页
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="CurrentPage"></param>
        /// <returns></returns>
        public DataSet GetSlideShowList(string strWhere, int CurrentPage)
        {

            DAL.SlideShowDAL dal = new DAL.SlideShowDAL();
            return dal.GetPageList(strWhere, CurrentPage, 20);
        }

    }
}
