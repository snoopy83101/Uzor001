


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //AdInfo

    public class AdInfoModel
    {

        public AdInfoModel()
        {

        }




        /// <summary>
        /// AdId
        /// </summary>		
        public decimal AdId
        {
            get;
            set;
        }
        /// <summary>
        /// 广告标题
        /// </summary>		
        public string AdTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 广告内容
        /// </summary>		
        public string AdContent
        {
            get;
            set;
        }
        /// <summary>
        /// 广告备注
        /// </summary>		
        public string AdMemo
        {
            get;
            set;
        }
        /// <summary>
        /// 绑定链接
        /// </summary>		
        public string Url
        {
            get;
            set;
        }
        /// <summary>
        /// 绑定事件
        /// </summary>		
        public string BindEvent
        {
            get;
            set;
        }
        /// <summary>
        /// 类型: 链接, 事件
        /// </summary>		
        public string AdType
        {
            get;
            set;
        }
        /// <summary>
        /// 类别
        /// </summary>		
        public string AdClass
        {
            get;
            set;
        }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 创建人
        /// </summary>		
        public string CreateUser
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
        /// <summary>
        /// 结束时间
        /// </summary>		
        public DateTime EndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 是否作废
        /// </summary>		
        public bool Invalid
        {
            get;
            set;
        }
        /// <summary>
        /// 位置
        /// </summary>		
        public string Location
        {
            get;
            set;
        }
        /// <summary>
        /// W
        /// </summary>		
        public int W
        {
            get;
            set;
        }
        /// <summary>
        /// H
        /// </summary>		
        public int H
        {
            get;
            set;
        }
        /// <summary>
        /// ImgId
        /// </summary>		
        public string ImgId
        {
            get;
            set;
        }
        /// <summary>
        /// StyleStr
        /// </summary>		
        public string StyleStr
        {
            get;
            set;
        }
        /// <summary>
        /// AdLabel
        /// </summary>		
        public string AdLabel
        {
            get;
            set;
        }

    }
}


