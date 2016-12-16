using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Common;
using Model;
namespace BLL
{

    /// <summary>
    /// 房产逻辑层
    /// </summary>
    public class HouseBLL
    {




        /// <summary>
        /// 根据主键ID取得房源需求
        /// </summary>
        /// <param name="HouseDemandId"></param>
        /// <returns></returns>
        public DataSet GetHouseDemandInfoById(string HouseDemandId)
        {
            DAL.HouseDemandDAL dal = new DAL.HouseDemandDAL();
            return dal.GetHouseDemandInfoById(HouseDemandId);

        }


        /// <summary>
        /// 获得房源信息的列表
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public DataSet GetHouseDemandPageList(string s, int c)
        {

            DAL.HouseDemandDAL dal = new DAL.HouseDemandDAL();
          return  dal.GetPageList(s, c, 20);

        }


        /// <summary>
        /// 保存一条房源需求(求租和求购)
        /// </summary>
        /// <param name="model"></param>
        public void SaveHouseDemand(HouseDemandModel model)
        {

            DAL.HouseDemandDAL dal = new DAL.HouseDemandDAL();
            model.CreateTime = DateTime.Now;
            if (model.HouseDemandId == "")
            {
                model.HouseDemandId = TimeString.GetNow_ff();
                dal.Add(model);
            }
            else
            {
                dal.Update(model);
            }


        }

        /// <summary>
        /// 获得房源信息的列表
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public DataSet GetHousePageList(string  s, int c)
        {

            DAL.HouseDAL dal = new DAL.HouseDAL();
            return dal.GetPageList(s, c, 30);

        }


        /// <summary>
        /// 获得房源点评列表
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public DataSet GetHouseVsCommentPageList(string s,int c)
        {
            DAL.HouseVsCommentDAL dal = new DAL.HouseVsCommentDAL();

            return dal.GetPageList(s, c,10);
        
        }

        /// <summary>
        /// 获得一个房源的主体内容
        /// </summary>
        /// <param name="HouseId"></param>
        /// <returns></returns>
        public DataSet GetHouseByHouseId(string HouseId)
        {
            DAL.HouseDAL dal = new DAL.HouseDAL();
            return dal.GetHouseListByHouseId(HouseId);
        }


        public Model.HouseModel GetHouseModel(string HouseId)
        {
            DAL.HouseDAL dal = new DAL.HouseDAL();
            return dal.GetModel(HouseId);
        
        }




        /// <summary>
        /// 删除一条房源下所有的图片
        /// </summary>
        /// <param name="HouseId"></param>
        public void DeleteHouseByHouseId(string HouseId)
        {

            DAL.HouseVsImgDAL dal = new DAL.HouseVsImgDAL();
            dal.DeleteList(" HouseId='" + HouseId + "' ");
        }

        /// <summary>
        /// 添加房源图片
        /// </summary>
        /// <param name="model"></param>
        public void AddHouseVsImg(HouseVsImgModel model)
        {
            DAL.HouseVsImgDAL dal = new DAL.HouseVsImgDAL();
            dal.Add(model);
        }

        /// <summary>
        /// 保存一个房屋信息
        /// </summary>
        /// <param name="model">房屋信息类</param>
        public void SaveHouse(HouseModel model)
        {
            DAL.HouseDAL dal = new DAL.HouseDAL();
            model.CreateTime = DateTime.Now;
            if (model.HouseId == "")
            {
                //新增
                model.HouseId = TimeString.GetNowDifString();
                dal.Add(model);
            }
            else
            {   //修改
                dal.Update(model);
            }

        }

        /// <summary>
        /// 获得房屋属性
        /// </summary>
        /// <returns></returns>
        public DataSet GetHouseSetting()
        {
            DAL.HouseDAL dal = new DAL.HouseDAL();

            return dal.GetHouseSetting();

        }

    }
}
