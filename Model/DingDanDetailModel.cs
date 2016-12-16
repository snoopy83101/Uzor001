


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //DingDanDetail

    public class DingDanDetailModel
    {

        public DingDanDetailModel()
        {

        }




        /// <summary>
        /// DingDanDetailId
        /// </summary>		
        public decimal DingDanDetailId
        {
            get;
            set;
        }
        /// <summary>
        /// DingDanId
        /// </summary>		
        public string DingDanId
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
        /// Quantity
        /// </summary>		
        public decimal Quantity
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
        /// Memo
        /// </summary>		
        public string Memo
        {
            get;
            set;
        }
        /// <summary>
        /// DingDanDetailAttr
        /// </summary>		
        public string DingDanDetailAttr
        {
            get;
            set;
        }
        /// <summary>
        /// 花费积分
        /// </summary>		
        public decimal JiFen
        {
            get;
            set;
        }
        /// <summary>
        /// 1商品明细,10运费明细
        /// </summary>		
        public int DingDanDetailTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 购买意向, 在生鲜等不确定价格的商品中插入,默认为0
        /// </summary>		
        public decimal Hope
        {
            get;
            set;
        }
        /// <summary>
        /// 产品特性
        /// </summary>		
        public int ProTeXing
        {
            get;
            set;
        }
        /// <summary>
        /// 最小质量
        /// </summary>		
        public decimal MinZl
        {
            get;
            set;
        }
        /// <summary>
        /// 最小质量倍数(生鲜数量)
        /// </summary>		
        public decimal Zlbs
        {
            get;
            set;
        }
        /// <summary>
        /// 下达订单时的积分比率
        /// </summary>		
        public decimal GetJifenNum
        {
            get;
            set;
        }

    }
}


