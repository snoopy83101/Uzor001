using System;

namespace Model
{
    //Article
    //备注
    public class ArticleModel
    {
        public ArticleModel()
        {
        }

        /// <summary>
        /// ArticleId
        /// </summary>
        public string ArticleId
        {
            get;
            set;
        }

        /// <summary>
        /// ArticleTitle
        /// </summary>
        public string ArticleTitle
        {
            get;
            set;
        }

        /// <summary>
        /// 摘要
        /// </summary>
        public string ArticleSummary
        {
            get;
            set;
        }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string ArticleContent
        {
            get;
            set;
        }

        /// <summary>
        /// 文章出处
        /// </summary>
        public string ArticleSource
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 新闻添加人(不是作者)
        /// </summary>
        public string CreateUser
        {
            get;
            set;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public int ArticleTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// 类别
        /// </summary>
        public int ArticleClassId
        {
            get;
            set;
        }

        /// <summary>
        /// 坐着
        /// </summary>
        public string Author
        {
            get;
            set;
        }

        /// <summary>
        /// 图片ID
        /// </summary>
        public string ArticleImgId
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
        /// Invalid
        /// </summary>
        public bool Invalid
        {
            get;
            set;
        }
    }
}