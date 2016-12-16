


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //网站指定的产品类型列表

    public class ProductTypeModel
    {

        public ProductTypeModel()
        {

        }




        /// <summary>
        /// ProductTypeId
        /// </summary>		
        public decimal ProductTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// ParentProductTypeId
        /// </summary>		
        public decimal ParentProductTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 产品大类名称
        /// </summary>		
        public string ProductTypeName
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
        /// 排序编号
        /// </summary>		
        public int OrderNo
        {
            get;
            set;
        }

    }
}


