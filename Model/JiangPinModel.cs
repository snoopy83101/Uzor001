


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //奖品字典表

    public class JiangPinModel
    {

        public JiangPinModel()
        {

        }




        /// <summary>
        /// JiangPinId
        /// </summary>		
        public decimal JiangPinId
        {
            get;
            set;
        }
        /// <summary>
        /// JiangPinName
        /// </summary>		
        public string JiangPinName
        {
            get;
            set;
        }
        /// <summary>
        /// ChouJiangId
        /// </summary>		
        public decimal ChouJiangId
        {
            get;
            set;
        }
        /// <summary>
        /// 是几等奖?
        /// </summary>		
        public int JiangPinLv
        {
            get;
            set;
        }
        /// <summary>
        /// 奖品描述
        /// </summary>		
        public string JiangPinMemo
        {
            get;
            set;
        }
        /// <summary>
        /// 关联主键ID
        /// </summary>		
        public string ReKey
        {
            get;
            set;
        }
        /// <summary>
        /// JiangPinTypeId
        /// </summary>		
        public int JiangPinTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// JiangPinClassId
        /// </summary>		
        public int JiangPinClassId
        {
            get;
            set;
        }
        /// <summary>
        /// GaiLv
        /// </summary>		
        public decimal GaiLv
        {
            get;
            set;
        }
        /// <summary>
        /// 奖品个数
        /// </summary>		
        public decimal Num
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


