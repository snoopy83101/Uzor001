


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //CuXiao

    public class CuXiaoModel
    {

        public CuXiaoModel()
        {

        }




        /// <summary>
        /// CuXiaoId
        /// </summary>		
        public decimal CuXiaoId
        {
            get;
            set;
        }
        /// <summary>
        /// CuXiaoName
        /// </summary>		
        public string CuXiaoName
        {
            get;
            set;
        }
        /// <summary>
        /// CuXiaoContent
        /// </summary>		
        public string CuXiaoContent
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
        /// <summary>
        /// PyCode
        /// </summary>		
        public string PyCode
        {
            get;
            set;
        }
        /// <summary>
        /// 上线时间
        /// </summary>		
        public DateTime BgTime
        {
            get;
            set;
        }
        /// <summary>
        /// 下线时间
        /// </summary>		
        public DateTime EndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 0未上线, 10,上线,完成20下线
        /// </summary>		
        public int Status
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
        /// BranchId
        /// </summary>		
        public string BranchId
        {
            get;
            set;
        }
        /// <summary>
        /// CuXiaoLabel
        /// </summary>		
        public string CuXiaoLabel
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
        /// ZoneId
        /// </summary>		
        public string ZoneId
        {
            get;
            set;
        }

    }
}


