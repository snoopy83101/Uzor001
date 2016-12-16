using System;

namespace Model
{
    //AuthorInfo

    public class AuthorInfoModel
    {
        public AuthorInfoModel()
        {
        }

        /// <summary>
        /// AuthorId
        /// </summary>
        public string AuthorId
        {
            get;
            set;
        }

        /// <summary>
        /// AuthorName
        /// </summary>
        public string AuthorName
        {
            get;
            set;
        }

        /// <summary>
        /// 拼音简码
        /// </summary>
        public string InputCode
        {
            get;
            set;
        }

        /// <summary>
        /// AuthorMemo
        /// </summary>
        public string AuthorMemo
        {
            get;
            set;
        }

        /// <summary>
        /// MerchantId
        /// </summary>
        public decimal MerchantId
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
        /// PicImgId
        /// </summary>
        public string PicImgId
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