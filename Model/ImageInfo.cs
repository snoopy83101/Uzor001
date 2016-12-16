


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //图片表

    public class ImageInfoModel
    {

        public ImageInfoModel()
        {

        }




        /// <summary>
        /// 主键ID
        /// </summary>		
        public string ImgId
        {
            get;
            set;
        }
        /// <summary>
        /// 图片类型
        /// </summary>		
        public string ImgType
        {
            get;
            set;
        }
        /// <summary>
        /// 图片地址
        /// </summary>		
        public string ImgUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 图片是否绑定相关内容(否则有可能被清理)
        /// </summary>		
        public bool IsBind
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

    }
}


