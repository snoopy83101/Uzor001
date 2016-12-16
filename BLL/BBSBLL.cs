using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Data;
namespace BLL
{
    public class BBSBLL
    {

        #region 帖子相关

        public int Ding(decimal TieZiId)
        {
            DAL.DingDAL dal = new DAL.DingDAL();
            Model.DingModel model = new Model.DingModel();
            int i = DAL.DalComm.ExInt(" select count(0) from  BBS.dbo.ding where UserId='" + Common.CookieSings.GetCurrentUserId() + "' and TieZiId='" + TieZiId + "' ");
            if (i > 0)
            {//已经顶过了

                if (HasTieZiPower(TieZiId))
                {

                }
                else
                {
                    throw new Exception("你已经顶过此贴");
                }

            }
            else
            {
                //没有顶过

                model.CreateTime = DateTime.Now;
                model.TieZiId = TieZiId;
                model.UserId = Common.CookieSings.GetCurrentUserId();
                dal.Add(model);

            }
            var y = dal.ExInt(" TieZiId='" + TieZiId + "'  ");
            y = y + 1;
            DAL.DalComm.ExReInt(" update bbs.dbo.tiezi set dingNum='" + y + "',UpdateTime='" + DateTime.Now + "'  where tieziid='" + TieZiId + "'   ");


            return y;


        }

        public Model.TieZiModel GetTieZiModel(decimal TieZiId)
        {

            DAL.TieZiDAL dal = new DAL.TieZiDAL();
            return dal.GetModel(TieZiId);
        }

        public void AddHot(decimal TieZiId)
        {

            DAL.DalComm.ExReInt(" update BBS.dbo.TieZi set  HotCount = HotCount+1 where TieZiId='" + TieZiId + "' ");

        }


        public bool HasTieZiPower(decimal TieZiId)
        {
            BLL.UserBLL ubll = new UserBLL();
            if (ubll.IsAdministrator())
            {
                return true;
            }
            else
            {

                StringBuilder s = new StringBuilder();

                string uid = ubll.CkUserLv();//如果被禁言则不能发表言论
                if (TieZiId == 0)
                {
                    return true;
                }
                s.Append(" SELECT * FROM BBS.dbo.FormsVsUser WHERE UserId='" + uid + "' AND FormId= ");
                s.Append(" (SELECT TOP 1 FormId FROM BBS.dbo.TieZi WHERE TieZiId='" + TieZiId + "') ");
                DataSet ds = DAL.DalComm.BackData(s.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {//如果也不是版主
                    s.Clear();
                    s.Append("SELECT TOP 1 FormId FROM BBS.dbo.TieZi WHERE CreateUser='" + uid + "'");

                    ds = DAL.DalComm.BackData(s.ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //如果是我的帖子
                        return true;
                    }
                    else
                    {
                        //如果不是管理员,不是版主, 也不是我的帖子, 那么就不行了
                        return false;

                    }

                }

                //是否版主




                return false;
            }

        }

        public DataSet GetTieZiInfoByTieZiId(decimal TieZiId)
        {
            DAL.TieZiDAL dal = new DAL.TieZiDAL();

            return dal.GetTieZiInfoByTieZiId(TieZiId);
        }

        /// <summary>
        /// 删除一个帖子的所有图片关联信息
        /// </summary>
        /// <param name="TieziId"></param>
        public void DeleteTieZiVsImgByTieZiId(decimal TieziId)
        {

            DAL.TieZiVsImgDAL dal = new DAL.TieZiVsImgDAL();

            dal.DeleteList(" TieZiId='" + TieziId + "' ");

        }

        /// <summary>
        /// 添加一条帖子和图片的关联信息
        /// </summary>
        /// <param name="model"></param>
        public void AddTieZiVsImg(Model.TieZiVsImgModel model)
        {
            DAL.TieZiVsImgDAL dal = new DAL.TieZiVsImgDAL();
            dal.Add(model);

        }

        /// <summary>
        /// 保存一张帖子 
        /// </summary>
        /// <param name="model"></param>
        public void SaveTieZi(Model.TieZiModel model)
        {
            if (model.TieZiTitle.Trim() == "")
            {
                model.TieZiTitle = "无题";
            }
            DAL.TieZiDAL dal = new DAL.TieZiDAL();
            if (model.TieZiId == 0)
            {

                //添加
                model.CreateTime = DateTime.Now;
                model.UpdateTime = model.CreateTime;
                dal.Add(model);
            }
            else
            {

                //修改
                model.UpdateTime = DateTime.Now;
            
                dal.Update(model);
            }
            if (model.ParentTieZiId > 0)
            {
                dal.ChangeRepCount(model.ParentTieZiId);
            }
        }


        /// <summary>
        /// 获得帖子的分页列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public DataSet GetTieZiPageList(string strWhere, int currentpage, int pagesize, string cols)
        {
            DAL.TieZiDAL dal = new DAL.TieZiDAL();
            return dal.GetPageList(strWhere, currentpage, pagesize, cols);
        }




        #endregion


    }
}
