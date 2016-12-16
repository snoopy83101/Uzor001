


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //ProVsImg

    public class ProVsImgModel
    {

        public ProVsImgModel()
        {

        }




        /// <summary>
        /// 图片编号
        /// </summary>		
        public string ImgId
        {
            get;
            set;
        }
        /// <summary>
        /// 产品编号
        /// </summary>		
        public string ProId
        {
            get;
            set;
        }
        /// <summary>
        /// 产品图片的分类,ProImg是产品图片,ConImg是产品的内容图片(从HTML编辑器中上传的图片)
        /// </summary>		
        public string VsType
        {
            get;
            set;
        }
        /// <summary>
        /// 排序编号
        /// </summary>		
        public int OrderNo
        {
            get;
            set;
        }

    }
}


