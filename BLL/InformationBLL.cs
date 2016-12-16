using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;
using System.Web.Caching;

namespace BLL
{
    public class InformationBLL
    {



        public DataSet GetKeyWord()
        { 
              DataSet dsKeyWord = (DataSet) HttpRuntime. Cache["dsKeyWord"];
            DAL.KeyWordDAL dal=new DAL.KeyWordDAL();
            if (dsKeyWord == null)
            {
                dsKeyWord= dal.GetList(" KeyWordType='information' order by orderNo desc ");
            }

            return dsKeyWord;
        
        }

        /// <summary>
        /// 保存一条分类信息
        /// </summary>
        /// <param name="model"></param>
        public void SaveInformation(Model.InformationModel model)
        {

            DAL.InformationDAL dal = new DAL.InformationDAL();


            if (model.InformationId <= 0)
            {

                //如果是新发布的话,那就用当前用户作为创建人.
                model.CreateUserId = Common.CookieSings.GetCurrentUserId();
                model.CreateTime = DateTime.Now;
                dal.Add(model);
            }
            else
            {
                if (model.CreateUserId == Common.CookieSings.GetCurrentUserId())
                {



                }
                else
                { 
                    BLL.UserBLL bll=new UserBLL();

                    if (!bll.IsAdministrator())
                    {
                        throw new Exception("您不具备修改此条信息的权限!");
                    }
                
                }
                //如果是修改,无需修改创建人
                model.CreateTime = DateTime.Now;
                dal.Update(model);


            }


        }

        public void SaveInformationImg(Model.InformationVsImgModel model)
        {
            DAL.InformationVsImgDAL dal = new DAL.InformationVsImgDAL();
            dal.Add(model);

        }
        public void DeleteInformationImg(string strwhere)
        {
            DAL.InformationVsImgDAL dal = new DAL.InformationVsImgDAL();
            dal.DeleteList(strwhere);

        }

        public DataSet GetInformationPageList(string strWhere, int currentPage, int i)
        {
            DAL.InformationDAL dal = new DAL.InformationDAL();
            return dal.GetPageList(strWhere, currentPage, i);
        }

        public DataSet GetInformationVsCommentPageList(string strWhere, int currentPage, int i)
        {
            DAL.InformationVsCommentDAL dal = new DAL.InformationVsCommentDAL();
            return dal.GetPageList(strWhere, currentPage, i);
        }


        /// <summary>
        /// 根据ID来获得分类信息主体内容
        /// </summary>
        /// <param name="InformationId"></param>
        /// <returns></returns>
        public DataSet GetInformationInfoById(decimal InformationId)
        {
            DAL.InformationDAL dal = new DAL.InformationDAL();
            return dal.GetInformationInfoById(InformationId);
        }

        /// <summary>
        /// 获得一条分类信息
        /// </summary>
        /// <param name="InformationId"></param>
        /// <returns></returns>
        public Model.InformationModel GetInformationModel(decimal InformationId)
        {
            DAL.InformationDAL dal = new DAL.InformationDAL();
            return dal.GetModel(InformationId);

        }
    }
}
