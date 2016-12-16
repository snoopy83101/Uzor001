


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Merchant

    public class MerchantModel
    {

        public MerchantModel()
        {

        }




        /// <summary>
        /// 商家编号
        /// </summary>		
        public decimal MerchantId
        {
            get;
            set;
        }
        /// <summary>
        /// 拼音简码
        /// </summary>		
        public string InputCode
        {
            get;
            set;
        }
        /// <summary>
        /// 商家名称
        /// </summary>		
        public string MerchantName
        {
            get;
            set;
        }
        /// <summary>
        /// 商家简介
        /// </summary>		
        public string MerchantMemo
        {
            get;
            set;
        }
        /// <summary>
        /// 详细介绍
        /// </summary>		
        public string MerchantContent
        {
            get;
            set;
        }
        /// <summary>
        /// 商家类别(预留)
        /// </summary>		
        public decimal MerchantClassId
        {
            get;
            set;
        }
        /// <summary>
        /// 商家类型
        /// </summary>		
        public decimal MerchantTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 推荐指数
        /// </summary>		
        public int Recommendlv
        {
            get;
            set;
        }
        /// <summary>
        /// 热度
        /// </summary>		
        public int HotLv
        {
            get;
            set;
        }
        /// <summary>
        /// 经度
        /// </summary>		
        public decimal Lng
        {
            get;
            set;
        }
        /// <summary>
        /// 纬度
        /// </summary>		
        public decimal Lat
        {
            get;
            set;
        }
        /// <summary>
        /// 网站
        /// </summary>		
        public string WebSite
        {
            get;
            set;
        }
        /// <summary>
        /// 商标或缩略图
        /// </summary>		
        public string Logo
        {
            get;
            set;
        }
        /// <summary>
        /// 乡镇编号
        /// </summary>		
        public decimal TownId
        {
            get;
            set;
        }
        /// <summary>
        /// 是否作废
        /// </summary>		
        public bool FlagInvalid
        {
            get;
            set;
        }
        /// <summary>
        /// 联系电话,可以为多个
        /// </summary>		
        public string Tell
        {
            get;
            set;
        }
        /// <summary>
        /// 联系QQ号
        /// </summary>		
        public string qq
        {
            get;
            set;
        }
        /// <summary>
        /// 客服邮箱
        /// </summary>		
        public string Email
        {
            get;
            set;
        }
        /// <summary>
        /// 文本地址
        /// </summary>		
        public string Address
        {
            get;
            set;
        }
        /// <summary>
        /// Phone
        /// </summary>		
        public string Phone
        {
            get;
            set;
        }
        /// <summary>
        /// 联系人
        /// </summary>		
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 创建日期
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
        /// <summary>
        /// 商家行业的冗余字段
        /// </summary>		
        public string MerchantTypeTarget
        {
            get;
            set;
        }
        /// <summary>
        /// ToWebSite
        /// </summary>		
        public bool ToWebSite
        {
            get;
            set;
        }

    }
}


