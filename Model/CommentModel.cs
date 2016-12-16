


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //用户点评表

    public class CommentModel
    {

        public CommentModel()
        {

        }




        /// <summary>
        /// CommentId
        /// </summary>		
        public decimal CommentId
        {
            get;
            set;
        }
        /// <summary>
        /// 回复类别(Information为回复的分类信息,Product为点评产品可以为多个,用逗号分隔)
        /// </summary>		
        public string CommentType
        {
            get;
            set;
        }
        /// <summary>
        /// CommentTitle
        /// </summary>		
        public string CommentTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 留言的主体内容
        /// </summary>		
        public string CommentContent
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
        /// 创建留言的人
        /// </summary>		
        public string CreateUser
        {
            get;
            set;
        }
        /// <summary>
        /// 被留言人(接收人)
        /// </summary>		
        public string ReceiveUser
        {
            get;
            set;
        }
        /// <summary>
        /// 是否作废的标识
        /// </summary>		
        public bool FlagInvalid
        {
            get;
            set;
        }
        /// <summary>
        /// 回复留言的ID,如果是首发留言,则为0
        /// </summary>		
        public decimal ParentCommenId
        {
            get;
            set;
        }
        /// <summary>
        /// 回复次数
        /// </summary>		
        public int RepCount
        {
            get;
            set;
        }
        /// <summary>
        /// json缓存(在关联点评时用到)
        /// </summary>		
        public string JsonMemo
        {
            get;
            set;
        }

    }
}


