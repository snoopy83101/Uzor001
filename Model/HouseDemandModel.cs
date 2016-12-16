


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //HouseDemand

    public class HouseDemandModel
    {

        public HouseDemandModel()
        {

        }




        /// <summary>
        /// HouseDemandId
        /// </summary>		
        public string HouseDemandId
        {
            get;
            set;
        }
        /// <summary>
        /// 求租求购信息标题
        /// </summary>		
        public string HouseDemandTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 1,求租,2求购
        /// </summary>		
        public int HouseDemandTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// BeginPrice
        /// </summary>		
        public decimal BeginPrice
        {
            get;
            set;
        }
        /// <summary>
        /// EndPrice
        /// </summary>		
        public decimal EndPrice
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
        /// HouseDemandMemo
        /// </summary>		
        public string HouseDemandMemo
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
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 创建人
        /// </summary>		
        public string CreateUser
        {
            get;
            set;
        }

    }
}


