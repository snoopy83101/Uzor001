


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //JobType

    public class JobTypeModel
    {

        public JobTypeModel()
        {

        }




        /// <summary>
        /// JobTypeId
        /// </summary>		
        public decimal JobTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 职位类别名称
        /// </summary>		
        public string JobTypeName
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
        /// Memo
        /// </summary>		
        public string Memo
        {
            get;
            set;
        }
        /// <summary>
        /// 上一级类别（当类别递时候有效，否则为0）
        /// </summary>		
        public decimal ParentJobTypeId
        {
            get;
            set;
        }

    }
}


