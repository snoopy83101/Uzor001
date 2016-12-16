


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //TieZi

    public class TieZiModel
    {

        public TieZiModel()
        {

        }




        /// <summary>
        /// TieZiId
        /// </summary>		
        public decimal TieZiId
        {
            get;
            set;
        }
        /// <summary>
        /// TieZiTitle
        /// </summary>		
        public string TieZiTitle
        {
            get;
            set;
        }
        /// <summary>
        /// TieZiSummary
        /// </summary>		
        public string TieZiSummary
        {
            get;
            set;
        }
        /// <summary>
        /// TieZiContent
        /// </summary>		
        public string TieZiContent
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
        /// UpdateTime
        /// </summary>		
        public DateTime UpdateTime
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
        /// TieZiImgId
        /// </summary>		
        public string TieZiImgId
        {
            get;
            set;
        }
        /// <summary>
        /// MiniImgUrl
        /// </summary>		
        public string MiniImgUrl
        {
            get;
            set;
        }
        /// <summary>
        /// RecommendLv
        /// </summary>		
        public int RecommendLv
        {
            get;
            set;
        }
        /// <summary>
        /// 所属板块的ID
        /// </summary>		
        public decimal ForumId
        {
            get;
            set;
        }
        /// <summary>
        /// 帖子的类型
        /// </summary>		
        public string TieZiType
        {
            get;
            set;
        }
        /// <summary>
        /// 帖子的类别
        /// </summary>		
        public string TieZiClass
        {
            get;
            set;
        }
        /// <summary>
        /// 帖子发布的IP地址
        /// </summary>		
        public string Ip
        {
            get;
            set;
        }
        /// <summary>
        /// 掌上麻城,南麻街
        /// </summary>		
        public string Source
        {
            get;
            set;
        }
        /// <summary>
        /// 如果是回帖的话,这就是主贴的ID, 如果是主贴, 此ID就是0
        /// </summary>		
        public decimal ParentTieZiId
        {
            get;
            set;
        }
        /// <summary>
        /// HideUser
        /// </summary>		
        public bool HideUser
        {
            get;
            set;
        }
        /// <summary>
        /// 回帖数
        /// </summary>		
        public int RepCount
        {
            get;
            set;
        }
        /// <summary>
        /// 如果是微信发帖的话,微信的openId
        /// </summary>		
        public string WxOpenId
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
        /// <summary>
        /// 最后回复
        /// </summary>		
        public string RepLastUser
        {
            get;
            set;
        }
        /// <summary>
        /// 热度
        /// </summary>		
        public int HotCount
        {
            get;
            set;
        }
        /// <summary>
        /// 填写数字(例如:43)
        /// </summary>		
        public string YueMingZhong
        {
            get;
            set;
        }
        /// <summary>
        /// 是否上首页.
        /// </summary>		
        public bool IsIndex
        {
            get;
            set;
        }
        /// <summary>
        /// DingNum
        /// </summary>		
        public int DingNum
        {
            get;
            set;
        }
        /// <summary>
        /// JingHua
        /// </summary>		
        public int JingHua
        {
            get;
            set;
        }

    }
}


