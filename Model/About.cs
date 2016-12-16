namespace Model
{
    //备注
    //About

    public class AboutModel
    {
        //beizhu

        public AboutModel()
        {
        }

        /// <summary>
        /// 关于我们编码
        /// </summary>
        public decimal AboutId
        {
            get;
            set;
        }

        /// <summary>
        /// 关于我们标题
        /// </summary>
        public string AboutTitle
        {
            get;
            set;
        }

        /// <summary>
        /// 关于我们内容
        /// </summary>
        public string AboutContent
        {
            get;
            set;
        }

        /// <summary>
        /// 排序编号
        /// </summary>
        public int OrderId
        {
            get;
            set;
        }

        /// <summary>
        /// 预留,类型
        /// </summary>
        public string AboutType
        {
            get;
            set;
        }
    }
}