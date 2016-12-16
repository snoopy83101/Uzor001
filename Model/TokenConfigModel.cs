


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //TokenConfig

    public class TokenConfigModel
    {

        public TokenConfigModel()
        {

        }




        /// <summary>
        /// 主键ID,同时也是内容
        /// </summary>		
        public string AccessToken
        {
            get;
            set;
        }
        /// <summary>
        /// 更新时间
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 介绍
        /// </summary>		
        public string Memo
        {
            get;
            set;
        }
        /// <summary>
        /// 所属公众号
        /// </summary>		
        public string Gzh
        {
            get;
            set;
        }
        /// <summary>
        /// 密钥的类别
        /// </summary>		
        public string TokenType
        {
            get;
            set;
        }

    }
}


