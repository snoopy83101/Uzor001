namespace Model
{
    //关注表,关注人或者关注商家

    public class AttentionModel
    {
        public AttentionModel()
        {
        }

        /// <summary>
        /// AutoId
        /// </summary>
        public decimal AutoId
        {
            get;
            set;
        }

        /// <summary>
        /// 属于谁的关注
        /// </summary>
        public string UserId
        {
            get;
            set;
        }

        /// <summary>
        /// 关注的用户ID
        /// </summary>
        public string AttentionUserId
        {
            get;
            set;
        }

        /// <summary>
        /// 关注的商家ID
        /// </summary>
        public decimal AttentionMerId
        {
            get;
            set;
        }

        /// <summary>
        /// 关注类别(1为关注用户,2为关注商家)
        /// </summary>
        public int AttentionType
        {
            get;
            set;
        }

        /// <summary>
        /// 关注的动态级别
        /// </summary>
        public int DynamicLv
        {
            get;
            set;
        }

        /// <summary>
        /// FlagInvalid
        /// </summary>
        public bool FlagInvalid
        {
            get;
            set;
        }
    }
}