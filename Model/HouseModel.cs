


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //House

    public class HouseModel
    {

        public HouseModel()
        {

        }




        /// <summary>
        /// HouseId
        /// </summary>		
        public string HouseId
        {
            get;
            set;
        }
        /// <summary>
        /// 房屋信息标题
        /// </summary>		
        public string HouseTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 房屋信息地址（文本描述）
        /// </summary>		
        public string HouseAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 室
        /// </summary>		
        public int Hshi
        {
            get;
            set;
        }
        /// <summary>
        /// 厅
        /// </summary>		
        public int Hting
        {
            get;
            set;
        }
        /// <summary>
        /// 厨
        /// </summary>		
        public int Hchu
        {
            get;
            set;
        }
        /// <summary>
        /// 卫
        /// </summary>		
        public int Hwei
        {
            get;
            set;
        }
        /// <summary>
        /// 阳
        /// </summary>		
        public int Hyangtai
        {
            get;
            set;
        }
        /// <summary>
        /// 房屋产权,房屋类别
        /// </summary>		
        public int HouseModelId
        {
            get;
            set;
        }
        /// <summary>
        /// DecorationId
        /// </summary>		
        public int DecorationId
        {
            get;
            set;
        }
        /// <summary>
        /// PropertyTypeId
        /// </summary>		
        public int PropertyTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 预留字段
        /// </summary>		
        public int HouseClassId
        {
            get;
            set;
        }
        /// <summary>
        /// Floor
        /// </summary>		
        public int Floor
        {
            get;
            set;
        }
        /// <summary>
        /// FloorALL
        /// </summary>		
        public int FloorALL
        {
            get;
            set;
        }
        /// <summary>
        /// 1出租，2出售，3出租也出售
        /// </summary>		
        public int HouseTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// Rent
        /// </summary>		
        public decimal Rent
        {
            get;
            set;
        }
        /// <summary>
        /// Price
        /// </summary>		
        public decimal Price
        {
            get;
            set;
        }
        /// <summary>
        /// IsAgency
        /// </summary>		
        public bool IsAgency
        {
            get;
            set;
        }
        /// <summary>
        /// Device
        /// </summary>		
        public string Device
        {
            get;
            set;
        }
        /// <summary>
        /// Memo
        /// </summary>		
        public string Memo
        {
            get;
            set;
        }
        /// <summary>
        /// CreateTime
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// CreateUser
        /// </summary>		
        public string CreateUser
        {
            get;
            set;
        }
        /// <summary>
        /// PingFang
        /// </summary>		
        public decimal PingFang
        {
            get;
            set;
        }
        /// <summary>
        /// 东,南,东南,西北,通透
        /// </summary>		
        public int ChaoXiangId
        {
            get;
            set;
        }
        /// <summary>
        /// 房屋展示图片
        /// </summary>		
        public string HouseImgId
        {
            get;
            set;
        }
        /// <summary>
        /// 姓名
        /// </summary>		
        public string ContactName
        {
            get;
            set;
        }
        /// <summary>
        /// 座机
        /// </summary>		
        public string ContactTell
        {
            get;
            set;
        }
        /// <summary>
        /// 手机
        /// </summary>		
        public string ContactPhone
        {
            get;
            set;
        }
        /// <summary>
        /// 邮件
        /// </summary>		
        public string ContactEmail
        {
            get;
            set;
        }
        /// <summary>
        /// qq
        /// </summary>		
        public string ContactQQ
        {
            get;
            set;
        }
        /// <summary>
        /// 小区名称
        /// </summary>		
        public string CommunityTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 暂时默认为0,因为无法收录小区数据
        /// </summary>		
        public int CommunityId
        {
            get;
            set;
        }
        /// <summary>
        /// TownId
        /// </summary>		
        public decimal TownId
        {
            get;
            set;
        }
        /// <summary>
        /// 中介机构编号
        /// </summary>		
        public decimal MerchantId
        {
            get;
            set;
        }
        /// <summary>
        /// HouseLng
        /// </summary>		
        public decimal HouseLng
        {
            get;
            set;
        }
        /// <summary>
        /// HouseLat
        /// </summary>		
        public decimal HouseLat
        {
            get;
            set;
        }
        /// <summary>
        /// 推荐系数
        /// </summary>		
        public int RecommendLv
        {
            get;
            set;
        }

    }
}


