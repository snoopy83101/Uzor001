


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //用户可以自定义的产品类型列表

    public class ProductClassModel
    {

        public ProductClassModel()
        {

        }




        /// <summary>
        /// ProductClassId
        /// </summary>		
        public decimal ProductClassId
        {
            get;
            set;
        }
        /// <summary>
        /// 产品大类ID
        /// </summary>		
        public decimal ParentProductClassId
        {
            get;
            set;
        }
        /// <summary>
        /// 产品类别名称
        /// </summary>		
        public string ProductClassName
        {
            get;
            set;
        }
        /// <summary>
        /// ProductClassAppName
        /// </summary>		
        public string ProductClassAppName
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
        /// 备注
        /// </summary>		
        public string Memo
        {
            get;
            set;
        }
        /// <summary>
        /// AppMemo
        /// </summary>		
        public string AppMemo
        {
            get;
            set;
        }
        /// <summary>
        /// 所属的商家编号
        /// </summary>		
        public decimal MerchantId
        {
            get;
            set;
        }
        /// <summary>
        /// ProductClassImgId
        /// </summary>		
        public string ProductClassImgId
        {
            get;
            set;
        }
        /// <summary>
        /// Invalid
        /// </summary>		
        public bool Invalid
        {
            get;
            set;
        }
        /// <summary>
        /// AttrSelXml
        /// </summary>		
        public string AttrSelXml
        {
            get;
            set;
        }
        /// <summary>
        /// ProClassColor
        /// </summary>		
        public string ProClassColor
        {
            get;
            set;
        }
        /// <summary>
        /// ProClassKeyWord
        /// </summary>		
        public string ProClassKeyWord
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
        /// 1普通,10生鲜
        /// </summary>		
        public int ProTeXing
        {
            get;
            set;
        }
        /// <summary>
        /// 本来别以及其下类别和产品的积分比率
        /// </summary>		
        public decimal GetJiFenNum
        {
            get;
            set;
        }
        /// <summary>
        /// 是否继承父级类别的积分比率
        /// </summary>		
        public bool InheritJiFenNum
        {
            get;
            set;
        }
        /// <summary>
        /// 这个类别下面所有的子类别ID
        /// </summary>		
        public string CldProClassIds
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
        /// ImgId
        /// </summary>		
        public string ImgId
        {
            get;
            set;
        }

    }
}


