


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Product

    public class ProductModel
    {

        public ProductModel()
        {

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
        /// 产品名称
        /// </summary>		
        public string ProName
        {
            get;
            set;
        }
        /// <summary>
        /// 产品属于哪个商家?
        /// </summary>		
        public decimal MerchantId
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
        /// 创建用户
        /// </summary>		
        public string CreateUser
        {
            get;
            set;
        }
        /// <summary>
        /// 网站指定产品类型
        /// </summary>		
        public decimal ProTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 商家自定义产品类型
        /// </summary>		
        public decimal ProClassId
        {
            get;
            set;
        }
        /// <summary>
        /// 产品详细介绍
        /// </summary>		
        public string ProContent
        {
            get;
            set;
        }
        /// <summary>
        /// 产品简介
        /// </summary>		
        public string ProTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 推荐级别
        /// </summary>		
        public int RecommendLv
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
        /// 点击数
        /// </summary>		
        public int ClickLv
        {
            get;
            set;
        }
        /// <summary>
        /// 购买数
        /// </summary>		
        public int BuyLv
        {
            get;
            set;
        }
        /// <summary>
        /// 街道ID,预留
        /// </summary>		
        public decimal StreetId
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
        /// 上限指数,越大权重越大,小于0则下线
        /// </summary>		
        public int OnLineLv
        {
            get;
            set;
        }
        /// <summary>
        /// 计量单位
        /// </summary>		
        public string Units
        {
            get;
            set;
        }
        /// <summary>
        /// 产品首页展示图片
        /// </summary>		
        public string IndexImgId
        {
            get;
            set;
        }
        /// <summary>
        /// 默认缩略图
        /// </summary>		
        public string ProductImgId
        {
            get;
            set;
        }
        /// <summary>
        /// 当前商品的实际执行价格
        /// </summary>		
        public decimal RePrice
        {
            get;
            set;
        }
        /// <summary>
        /// 对比价格, 打折,优惠 之前的价格
        /// </summary>		
        public decimal RePrice2
        {
            get;
            set;
        }
        /// <summary>
        /// 批发价格,预留
        /// </summary>		
        public decimal RePrice3
        {
            get;
            set;
        }
        /// <summary>
        /// CommentCount
        /// </summary>		
        public int CommentCount
        {
            get;
            set;
        }
        /// <summary>
        /// 作者
        /// </summary>		
        public string AuthorId
        {
            get;
            set;
        }
        /// <summary>
        /// Spec
        /// </summary>		
        public string Spec
        {
            get;
            set;
        }
        /// <summary>
        /// ProCode
        /// </summary>		
        public string ProCode
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
        /// 配送参数(0可配送,10县城内可配送,20全县境内可配送,30全国可配送)
        /// </summary>		
        public int SendPara
        {
            get;
            set;
        }
        /// <summary>
        /// Attr
        /// </summary>		
        public string Attr
        {
            get;
            set;
        }
        /// <summary>
        /// PinPaiId
        /// </summary>		
        public decimal PinPaiId
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
        /// (1普通产品,10生鲜产品)
        /// </summary>		
        public int ProTeXing
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
        /// 最小起售数量
        /// </summary>		
        public decimal MinQuantity
        {
            get;
            set;
        }
        /// <summary>
        /// 起售质量
        /// </summary>		
        public decimal Zl
        {
            get;
            set;
        }
        /// <summary>
        /// 最小递增质量
        /// </summary>		
        public decimal MinZl
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
        /// KeyWord
        /// </summary>		
        public string KeyWord
        {
            get;
            set;
        }
        /// <summary>
        /// PyCode
        /// </summary>		
        public string PyCode
        {
            get;
            set;
        }
        /// <summary>
        /// 库存商品条码. 用于大包装商品
        /// </summary>		
        public string ProNumCode
        {
            get;
            set;
        }

    }
}


