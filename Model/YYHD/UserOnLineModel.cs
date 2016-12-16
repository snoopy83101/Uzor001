


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //UserOnLine

    public class UserOnLineModel
    {

        public UserOnLineModel()
        {

        }




        /// <summary>
        /// UserId
        /// </summary>		
        public string UserId
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
        /// 10在线,20,忙碌,30,暂离,40离线
        /// </summary>		
        public int UserOnlineStatusId
        {
            get;
            set;
        }
        /// <summary>
        /// 1.推送电脑和手机10推送电脑,20推送手机
        /// </summary>		
        public int PushTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 最后改动日期
        /// </summary>		
        public DateTime LastTime
        {
            get;
            set;
        }

    }
}


