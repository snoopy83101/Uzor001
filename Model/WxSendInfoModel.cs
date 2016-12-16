


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //回复列表

    public class WxSendInfoModel
    {

        public WxSendInfoModel()
        {

        }




        /// <summary>
        /// WxSendId
        /// </summary>		
        public decimal WxSendId
        {
            get;
            set;
        }
        /// <summary>
        /// InputCode
        /// </summary>		
        public string InputCode
        {
            get;
            set;
        }
        /// <summary>
        /// WxSendTitle
        /// </summary>		
        public string WxSendTitle
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
        /// CreateUser
        /// </summary>		
        public string CreateUser
        {
            get;
            set;
        }
        /// <summary>
        /// WxSendType
        /// </summary>		
        public string WxSendType
        {
            get;
            set;
        }
        /// <summary>
        /// WxSendClassId
        /// </summary>		
        public int WxSendClassId
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
        /// Memo
        /// </summary>		
        public string Memo
        {
            get;
            set;
        }
        /// <summary>
        /// FmImgId
        /// </summary>		
        public string FmImgId
        {
            get;
            set;
        }
        /// <summary>
        /// 素材ID,可为空
        /// </summary>		
        public decimal WxSuCaiId
        {
            get;
            set;
        }
        /// <summary>
        /// SendContent
        /// </summary>		
        public string SendContent
        {
            get;
            set;
        }

    }
}


