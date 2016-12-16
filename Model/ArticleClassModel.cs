namespace Model
{
    //ArticleClass

    public class ArticleClassModel
    {
        public ArticleClassModel()
        {
        }

        /// <summary>
        /// ArticleClassId
        /// </summary>
        public int ArticleClassId
        {
            get;
            set;
        }

        /// <summary>
        /// ArticleClassName
        /// </summary>
        public string ArticleClassName
        {
            get;
            set;
        }

        /// <summary>
        /// MerId
        /// </summary>
        public decimal MerId
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

        /// <summary>
        /// 属于几级类别
        /// </summary>
        public int ParentArticleClassId
        {
            get;
            set;
        }

        /// <summary>
        /// ArticleClassMemo
        /// </summary>
        public string ArticleClassMemo
        {
            get;
            set;
        }

        /// <summary>
        /// ArticleClassImgId
        /// </summary>
        public string ArticleClassImgId
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