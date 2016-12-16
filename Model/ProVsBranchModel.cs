


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //ProVsBranch

    public class ProVsBranchModel
    {

        public ProVsBranchModel()
        {

        }




        /// <summary>
        /// BranchId
        /// </summary>		
        public string BranchId
        {
            get;
            set;
        }
        /// <summary>
        /// ProId
        /// </summary>		
        public string ProId
        {
            get;
            set;
        }
        /// <summary>
        /// RePrice
        /// </summary>		
        public decimal RePrice
        {
            get;
            set;
        }
        /// <summary>
        /// RePrice2
        /// </summary>		
        public decimal RePrice2
        {
            get;
            set;
        }
        /// <summary>
        /// RePrice3
        /// </summary>		
        public decimal RePrice3
        {
            get;
            set;
        }
        /// <summary>
        /// Status
        /// </summary>		
        public int Status
        {
            get;
            set;
        }
        /// <summary>
        /// MinQuantity
        /// </summary>		
        public decimal MinQuantity
        {
            get;
            set;
        }
        /// <summary>
        /// Zl
        /// </summary>		
        public decimal Zl
        {
            get;
            set;
        }
        /// <summary>
        /// MinZl
        /// </summary>		
        public decimal MinZl
        {
            get;
            set;
        }
        /// <summary>
        /// OnLineLv
        /// </summary>		
        public int OnLineLv
        {
            get;
            set;
        }
        /// <summary>
        /// ProNum
        /// </summary>		
        public decimal ProNum
        {
            get;
            set;
        }
        /// <summary>
        /// 积分比率
        /// </summary>		
        public decimal GetJiFenNum
        {
            get;
            set;
        }
        /// <summary>
        /// 是否继承上级类别的配送方式
        /// </summary>		
        public bool InheritPeiSongType
        {
            get;
            set;
        }
        /// <summary>
        /// InheritProTeXing
        /// </summary>		
        public bool InheritProTeXing
        {
            get;
            set;
        }
        /// <summary>
        /// 是否无限库存,当选中时,库存如论如何为10000
        /// </summary>		
        public bool IsInfiniteNum
        {
            get;
            set;
        }
        /// <summary>
        /// 是否允许价格接口
        /// </summary>		
        public bool AllowPriceInterface
        {
            get;
            set;
        }
        /// <summary>
        /// 是否允许库存接口
        /// </summary>		
        public bool AllowProNumInterface
        {
            get;
            set;
        }
        /// <summary>
        /// 是否继承类别的积分比率
        /// </summary>		
        public bool InheritJiFenNum
        {
            get;
            set;
        }
        /// <summary>
        /// 商品包装(接口根据条码对接价格时,会与本字段乘积)
        /// </summary>		
        public int InterfaceBaoZhuangNum
        {
            get;
            set;
        }
        /// <summary>
        /// 是否继承上级折扣
        /// </summary>		
        public bool InheritDiscount
        {
            get;
            set;
        }
        /// <summary>
        /// 折扣, 默认为1(100%)
        /// </summary>		
        public decimal Discount
        {
            get;
            set;
        }
        /// <summary>
        /// ProName
        /// </summary>		
        public string ProName
        {
            get;
            set;
        }
        /// <summary>
        /// KeyWord
        /// </summary>		
        public string KeyWord
        {
            get;
            set;
        }

    }
}


