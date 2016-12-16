


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //AddressInfo

    public class AddressInfoModel
    {

        public AddressInfoModel()
        {

        }




        /// <summary>
        /// AddressId
        /// </summary>		
        public decimal AddressId
        {
            get;
            set;
        }
        /// <summary>
        /// 所属乡镇
        /// </summary>		
        public decimal TownId
        {
            get;
            set;
        }
        /// <summary>
        /// 详细地址信息
        /// </summary>		
        public string Memo
        {
            get;
            set;
        }
        /// <summary>
        /// 排序编号
        /// </summary>		
        public int OrderNo
        {
            get;
            set;
        }
        /// <summary>
        /// 联系电话
        /// </summary>		
        public string Tel
        {
            get;
            set;
        }
        /// <summary>
        /// 联系人姓名
        /// </summary>		
        public string ContactName
        {
            get;
            set;
        }
        /// <summary>
        /// 是否作废
        /// </summary>		
        public bool Invalid
        {
            get;
            set;
        }
        /// <summary>
        /// MemberId
        /// </summary>		
        public decimal MemberId
        {
            get;
            set;
        }
        /// <summary>
        /// IsDefault
        /// </summary>		
        public bool IsDefault
        {
            get;
            set;
        }
        /// <summary>
        /// 地理位置编号
        /// </summary>		
        public decimal SiteId
        {
            get;
            set;
        }
        /// <summary>
        /// 附加内容
        /// </summary>		
        public string Attach
        {
            get;
            set;
        }
        /// <summary>
        /// LastTime
        /// </summary>		
        public DateTime LastTime
        {
            get;
            set;
        }

    }
}


