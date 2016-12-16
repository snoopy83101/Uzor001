using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
namespace BLL
{
    public class ImageBLL
    {
        DAL.ImageInfoDAL dal = new DAL.ImageInfoDAL();


        /// <summary>
        /// 添加一张新的图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void AddNewImages(Model.ImageInfoModel model)
        {
            model.ImgId = Common.TimeString.GetNowDifString();
            model.IsBind = false;
            dal.Add(model);
        }


        public void DoBind(List<string> ImgUrl, string UserId)
        {
            DAL.ImageInfoDAL dal = new DAL.ImageInfoDAL();
            dal.DoBind(ImgUrl, UserId);
        }

        public bool BindImage(List<string> ImgIdList)
        {
            string ImgIds = Common.StringPlus.StrArrayByList(ImgIdList);
            return dal.BindImage(ImgIds);
        }

        public DataSet GetList(string whereStr)
        {

            return dal.GetList(whereStr);
        }

        public void ClearImg(string ImgId)
        {

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
            #endregion

                string ImgUrl = DAL.DalComm.ExStr(" SELECT ImgUrl FROM dbo.ImageInfo WHERE ImgId='" + ImgId + "' ");
                DAL.ImageInfoDAL dal = new DAL.ImageInfoDAL();
                dal.DeleteList(" ImgId='" + ImgId + "' ");
                Common.FileString.FileDel(ImgUrl);

                #region 事务关闭

                transactionScope.Complete();


            }
                #endregion


        }



    }
}
