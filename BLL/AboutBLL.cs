using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace BLL
{
    public class AboutBLL
    {

        DAL.AboutDAL dal = new DAL.AboutDAL();

        public DataTable GetAboutList(string StrWhere)
        {

            return dal.GetList(StrWhere).Tables[0];
        }


        public DataSet GetAboutPageList(string strWhere,int CurrentPage)
        {
         return   dal.GetPageList(strWhere, CurrentPage, 20);
        }

        /// <summary>
        /// 获取一条网站内部信息
        /// </summary>
        /// <param name="AboutId"></param>
        /// <returns></returns>
        public DataTable GetAbout(decimal AboutId)
        {
            return dal.GetList(" AboutId='" + AboutId + "' ").Tables[0];
        }


        /// <summary>
        /// 保存一个站内信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveAbout(Model.AboutModel model)
        {
            return dal.Update(model);
             
        }
    }
}
