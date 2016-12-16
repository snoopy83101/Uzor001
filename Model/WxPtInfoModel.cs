


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //WxPtInfo

    public class WxPtInfoModel
    {

        public WxPtInfoModel()
        {

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
        /// 微信公众平台的微信号
        /// </summary>		
        public string WxPtCode
        {
            get;
            set;
        }
        /// <summary>
        /// AccessToken
        /// </summary>		
        public string AccessToken
        {
            get;
            set;
        }
        /// <summary>
        /// AccessTokenCreateTime
        /// </summary>		
        public DateTime AccessTokenCreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// WxPtName
        /// </summary>		
        public string WxPtName
        {
            get;
            set;
        }
        /// <summary>
        /// ReUrl
        /// </summary>		
        public string ReUrl
        {
            get;
            set;
        }
        /// <summary>
        /// ReToken
        /// </summary>		
        public string ReToken
        {
            get;
            set;
        }
        /// <summary>
        /// AppId
        /// </summary>		
        public string AppId
        {
            get;
            set;
        }
        /// <summary>
        /// AppSecret
        /// </summary>		
        public string AppSecret
        {
            get;
            set;
        }
        /// <summary>
        /// 所绑定的商家
        /// </summary>		
        public decimal MerId
        {
            get;
            set;
        }
        /// <summary>
        /// 1服务号,2认证服务号,3订阅号,4认证订阅号
        /// </summary>		
        public int WxPtTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 原始ID
        /// </summary>		
        public string YuanShiId
        {
            get;
            set;
        }
        /// <summary>
        /// 文章系统URL页面
        /// </summary>		
        public string ArticleUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 产品系统Url页面
        /// </summary>		
        public string ProUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 帖子URL页面
        /// </summary>		
        public string TieZiUrl
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

    }
}


