


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //ChouJiang

    public class ChouJiangModel
    {

        public ChouJiangModel()
        {

        }




        /// <summary>
        /// ChouJiangId
        /// </summary>		
        public decimal ChouJiangId
        {
            get;
            set;
        }
        /// <summary>
        /// 抽奖编号
        /// </summary>		
        public string ChouJiangCode
        {
            get;
            set;
        }
        /// <summary>
        /// 抽奖名称
        /// </summary>		
        public string ChouJiangName
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
        /// BgTime
        /// </summary>		
        public DateTime BgTime
        {
            get;
            set;
        }
        /// <summary>
        /// EndTime
        /// </summary>		
        public DateTime EndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 限制抽奖次数
        /// </summary>		
        public int LimitNum
        {
            get;
            set;
        }
        /// <summary>
        /// WxPtId
        /// </summary>		
        public decimal WxPtId
        {
            get;
            set;
        }
        /// <summary>
        /// ChouJiangTypeId
        /// </summary>		
        public int ChouJiangTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// ChouJiangClassId
        /// </summary>		
        public int ChouJiangClassId
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
        /// Invalid
        /// </summary>		
        public bool Invalid
        {
            get;
            set;
        }

    }
}


