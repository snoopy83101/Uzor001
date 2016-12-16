


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //WxSuCaiDetail

    public class WxSuCaiDetailModel
    {

        public WxSuCaiDetailModel()
        {

        }




        /// <summary>
        /// WxSuCaiDetailId
        /// </summary>		
        public decimal WxSuCaiDetailId
        {
            get;
            set;
        }
        /// <summary>
        /// WxSuCaiInfoId
        /// </summary>		
        public decimal WxSuCaiInfoId
        {
            get;
            set;
        }
        /// <summary>
        /// WxSuCaiDetailTitle
        /// </summary>		
        public string WxSuCaiDetailTitle
        {
            get;
            set;
        }
        /// <summary>
        /// WxSuCaiDetailMemo
        /// </summary>		
        public string WxSuCaiDetailMemo
        {
            get;
            set;
        }
        /// <summary>
        /// WxSuCaiDetailContent
        /// </summary>		
        public string WxSuCaiDetailContent
        {
            get;
            set;
        }
        /// <summary>
        /// WxSuCaiDetailClassId
        /// </summary>		
        public int WxSuCaiDetailClassId
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
        /// <summary>
        /// 指向的URL
        /// </summary>		
        public string Url
        {
            get;
            set;
        }
        /// <summary>
        /// 关联内容的主键
        /// </summary>		
        public string ReKey
        {
            get;
            set;
        }
        /// <summary>
        /// OtherPara
        /// </summary>		
        public string OtherPara
        {
            get;
            set;
        }
        /// <summary>
        /// OrderNo
        /// </summary>		
        public int OrderNo
        {
            get;
            set;
        }

    }
}


