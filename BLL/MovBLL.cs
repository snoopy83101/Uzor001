using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Transactions;
namespace BLL
{
    public class MovBLL
    {
        public Model.MovInfoModel GetMovInfoModel(string MovId)
        {
            DAL.MovInfoDAL dal = new DAL.MovInfoDAL();
            return dal.GetModel(MovId);
        }

        public void SaveMovInfo(Model.MovInfoModel model)
        {
            DAL.MovInfoDAL dal = new DAL.MovInfoDAL();


            if (dal.ExInt(" MovId='" + model.MovId + "' ") == 0)
            {
                dal.Add(model);
            }
            else
            {

                dal.Update(model);
            }

        }

        public void DelMovEvent(string strWhere)
        {
            DAL.MovEventDAL dal = new DAL.MovEventDAL();
            dal.DeleteList(strWhere);
        }


        /// <summary>
        /// 添加一场上映场次
        /// </summary>
        /// <param name="model"></param>
        public void AddMovEvent(Model.MovEventModel model)
        {

            DAL.MovEventDAL dal = new DAL.MovEventDAL();
            dal.Add(model);
        }
    }
}
